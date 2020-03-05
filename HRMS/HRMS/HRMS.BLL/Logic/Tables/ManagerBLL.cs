using HRMS.BLL.StaticData;
using HRMS.DAL.Repositories;
using HRMS.DTOs.Business;
using HRMS.Entities;
using HRMS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BLL.Logic.Tables
{
    public class ManagerBLL : Repository<Manager>
    {
        private UserBLL userBLL;
        SubDepartmentBLL subDepartmentBLL;
        ApprovalFlowDetailsBLL approvalFlowDetail;
        DateTime creationDate;
        public ManagerBLL(HRMSEntities Context,DateTime CreationDate) : base(Context)
        {
            subDepartmentBLL = new SubDepartmentBLL(Context, CreationDate);
            userBLL = new UserBLL(Context, CreationDate);
            approvalFlowDetail = new ApprovalFlowDetailsBLL(Context, CreationDate);
            creationDate = CreationDate;

        }
        public void AddPersonsToManager(ManagerToSubordinatesDTO ManagerToSubordinates)
        {
            List<User> ListOfNewUsers = new List<User>();
            List<User> ListOfDeletedUsers = new List<User>();
            List<User> CurrentListOfUsers = userBLL.GetAll().Where(x => x.DirectManagerFK == ManagerToSubordinates.ManagerID).ToList();
            List<User> ListOfUsers = new List<User>();

            foreach (var item in ManagerToSubordinates.ListUsersIDs)
            {
                if (CurrentListOfUsers.Where(x => x.SubDepartmentFK == item).Count() == 1
                    )
                {
                    continue;
                }
                else if (CurrentListOfUsers.Where(x => x.SubDepartmentFK == item).Count() == 0)

                {
                    ListOfNewUsers.Add(userBLL.Find(x => x.UserID == item).FirstOrDefault());
                    //add new
                }
            }

            foreach (var item in CurrentListOfUsers)
            {
                if (ManagerToSubordinates.ListUsersIDs.Where(x => x == item.SubDepartmentFK).Count() == 1
                    )
                {
                    continue;
                }

                if (ManagerToSubordinates.ListUsersIDs.Where(x => x == item.SubDepartmentFK).Count() == 0
                    )
                {
                    ListOfDeletedUsers.Add(userBLL.Find(x => x.UserID == item.UserID).FirstOrDefault());
                    //add new
                }

            }

            AddNewUserToManager(ListOfNewUsers, ManagerToSubordinates.ManagerID);
            RemoveUserFromManager(ListOfDeletedUsers);


        }
        public void AddNewUserToManager(List<User> ListOfUsers, int ManagerID)
        {
            foreach (var item in ListOfUsers)
            {
                var user = userBLL.Find(x => x.UserID == item.UserID).FirstOrDefault();
                user.DirectManagerFK = ManagerID;
                userBLL.Edit(user);

            }
        }

        public void AddNewSubDepartmentToPosition(List<SubDepartment> ListOfSubDepartment, int ManagerID)
        {
            foreach (var item in ListOfSubDepartment)
            {
                var SubDepartment = subDepartmentBLL.Find(x => x.SubDepartmentID == item.SubDepartmentID).FirstOrDefault();
                SubDepartment.TeamManagerFK = ManagerID;
                subDepartmentBLL.Edit(SubDepartment);

            }
        }

        public void AddSubdepartmentToManager(ManagerToSubordinatesDTO ManagerToSubordinates)
        {
            List<SubDepartment> ListOfNewsubDepartment = new List<SubDepartment>();
            List<SubDepartment> ListOfDeletedsubDepartment = new List<SubDepartment>();
            List<SubDepartment> CurrentListOfsubDepartment = subDepartmentBLL.GetAll().Where(x => x.TeamManagerFK == ManagerToSubordinates.ManagerID).ToList();
            List<SubDepartment> ListOfsubDepartment = new List<SubDepartment>();



            foreach (var item in ManagerToSubordinates.ListSubDepartmentIDs)
            {
                if (CurrentListOfsubDepartment.Where(x => x.SubDepartmentID == item).Count() == 1
                    )
                {
                    continue;
                }
                else if (CurrentListOfsubDepartment.Where(x => x.SubDepartmentID == item).Count() == 0)

                {
                    ListOfNewsubDepartment.Add(subDepartmentBLL.Find(x => x.SubDepartmentID == item).FirstOrDefault());
                    //add new
                }
            }

            foreach (var item in CurrentListOfsubDepartment)
            {
                if (ManagerToSubordinates.ListSubDepartmentIDs.Where(x => x == item.SubDepartmentID).Count() == 1
                    )
                {
                    continue;
                }

                if (ManagerToSubordinates.ListSubDepartmentIDs.Where(x => x == item.SubDepartmentID).Count() == 0
                    )
                {
                    ListOfDeletedsubDepartment.Add(subDepartmentBLL.Find(x => x.SubDepartmentID == item.SubDepartmentID).FirstOrDefault());
                    //add new
                }

            }

            AddNewSubDepartmentToPosition(ListOfNewsubDepartment, ManagerToSubordinates.ManagerID);
            RemoveSubDepartmentFromManager(ListOfDeletedsubDepartment);

        }
        public List<ManagerDTO> GetAllManager()
        {
            List<ManagerDTO> ListManager = new List<ManagerDTO>();
            foreach (var item in GetAll())
            {
                ListManager.Add(new ManagerDTO
                {
                    ManagerID = item.ManagerID,
                    ManagerName = item.ManagerName,
                });
            }
            return ListManager;

        }

        //public void AddPersonsToManager()
        //{


        //}

        public void RemoveUserFromManager(List<User> ListOfUsers)
        {
            foreach (var item in ListOfUsers)
            {
                var user = userBLL.Find(x => x.UserID == item.UserID).FirstOrDefault();
                user.DirectManagerFK = null;
                userBLL.Edit(user);

            }
        }
        public void RemoveSubDepartmentFromManager(List<SubDepartment> ListOfSubDepartment)
        {
            foreach (var item in ListOfSubDepartment)
            {
                var subDepartment = subDepartmentBLL.Find(x => x.SubDepartmentID == item.SubDepartmentID).FirstOrDefault();
                subDepartment.TeamManagerFK = null;
                subDepartmentBLL.Edit(subDepartment);

            }
        }

        public ManagerDTO GetALLManagerDetails(int ManagerID)
        {
            Manager Manager = Find(x => x.ManagerID == ManagerID).FirstOrDefault();

            ManagerDTO ManagerDTO = new ManagerDTO()
            {
                ManagerID = Manager.ManagerID,
                ManagerName = Manager.ManagerName,
            };
            ManagerDTO.Users = new List<ManagerUsersDTO>();
            ManagerDTO.SubDepartments = new List<ManagerSubDepartmentsDTO>();

            foreach (var userItem in userBLL.Find(x => x.DirectManagerFK == Manager.ManagerID).ToList())
            {
                ManagerDTO.Users.Add(new ManagerUsersDTO
                {
                    UsersId = userItem.UserID,
                    UsersName = userItem.LDAPUserName

                });
            }
            foreach (var SubDepartmentItem in subDepartmentBLL.Find(x => x.TeamManagerFK == Manager.ManagerID).ToList())
            {
                ManagerDTO.SubDepartments.Add(new ManagerSubDepartmentsDTO
                {
                    SubDepartmentID = SubDepartmentItem.SubDepartmentID,
                    SubDepartmentName = SubDepartmentItem.SubDepartmentName

                });

            }
            return ManagerDTO;

        }

        public UserDTO GetALLUserDetails(int UserID)
        {
            User User = userBLL.Find(x => x.UserID == UserID).FirstOrDefault();
            Manager Manager = Find(c => c.ManagerUserFK == UserID)?.FirstOrDefault()==null?new Manager(): Find(c => c.ManagerUserFK == UserID)?.FirstOrDefault();
            SubDepartment SubDepartment = subDepartmentBLL.Find(s => s.TeamManagerFK == Manager.ManagerID)?.FirstOrDefault()==null?new SubDepartment(): subDepartmentBLL.Find(s => s.TeamManagerFK == Manager.ManagerID)?.FirstOrDefault();   

            UserDTO UserDTO = new UserDTO()
            {
                UserID = User.UserID,
                UserName = User.UserName,

            };
            UserDTO.Users = new List<NormalUsersDTO>();
            UserDTO.SubDepartments = new List<ManagerSubDepartmentsDTO>();
            try
            {
                if(SubDepartment != null)
                {
                    foreach (var userItem in userBLL.Find(x => x.SubDepartmentFK == SubDepartment.SubDepartmentID).ToList())
                    {
                        UserDTO.Users.Add(new NormalUsersDTO
                        {
                            NormalUsersId = userItem.UserID,
                            NormalUsersName = userItem.LDAPUserName

                        });
                    }
                }
                else 
                {
                    foreach (var userItem in userBLL.Find(x => x.DirectManagerFK == Manager.ManagerID).ToList())
                    {
                        UserDTO.Users.Add(new NormalUsersDTO
                        {
                            NormalUsersId = userItem.UserID,
                            NormalUsersName = userItem.LDAPUserName

                        });
                    }
                }


            }
            catch(Exception ex) {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
            }
            try
            {
                foreach (var SubDepartmentItem in subDepartmentBLL.Find(x => x.TeamManagerFK == Manager.ManagerID).ToList())
                {
                    UserDTO.SubDepartments.Add(new ManagerSubDepartmentsDTO
                    {
                        SubDepartmentID = SubDepartmentItem.SubDepartmentID,
                        SubDepartmentName = SubDepartmentItem.SubDepartmentName

                    });

                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
            }
            return UserDTO;

        }

        public Manager AddUserToManagers(User User)
        {
            Manager NewManager = new Manager
            {
                CreationDate = creationDate,
                IsActive = true,
                IsDeleted = false,
                ManagerName = User.UserName,
                ManagerUserFK = User.UserID,
            };
            Add(NewManager);
            return NewManager;
        }

        public List<ManagerDTO> GetALLManagers()
        {
            List<ManagerDTO> Managers = new List<ManagerDTO>();
            foreach (var item in GetAll().ToList())
            {
                Managers.Add(new ManagerDTO
                {
                    ManagerID = item.ManagerID,
                    ManagerName = item.ManagerName

                });
            }
            return Managers;
        }


        public AccessAdministrationDTO CheckUserIsManager(int UserID)
        {
            AccessAdministrationDTO ShowAndHideDTO = new AccessAdministrationDTO();
            ShowAndHideDTO.ShowApproval = false;
            ShowAndHideDTO.SowhConfigrations = false;
            User User =userBLL.Find(x => x.UserID == UserID).FirstOrDefault();
            if(User.IsAdmin==true|| User.IsHR == true)
            {
                ShowAndHideDTO.SowhConfigrations = true;

            }

            if (approvalFlowDetail.Find(x => x.ApprovalFlowPersonFK == UserID).Count() > 0 || Find(x => x.ManagerUserFK == UserID).Count() > 0)
            { ShowAndHideDTO.ShowApproval = true; }

            if(User.IsHR == true)
            {
                ShowAndHideDTO.SowhHRReport = true;
            }

                return ShowAndHideDTO;

        }

        public void AddManager(ManagerDTO managerDTO)
        {
            try
            {
                Manager AddedManger = new Manager() {
                    ManagerName=managerDTO.ManagerName,
                    ManagerUserFK=managerDTO.ManagerUserFK,
                    CreationDate=DateTime.Now,
                    IsActive=true,
                    IsDeleted=false
                };

                Add(AddedManger);
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
            }
        }

        public void EditManager(ManagerDTO Manager)
        {
            var manager = Find(x => x.ManagerID == Manager.ManagerID).FirstOrDefault();
            var oldmanager =  XMLObjectConverter.Serialize(manager);
            manager.ManagerName = manager.ManagerName;
            base.Edit(manager);
            Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.EditUserData, (int)Manager.ModifiedByUserId, oldmanager, XMLObjectConverter.Serialize(manager), "Edit Manager");

        }

        public Manager AddManager(ManagerToSubordinatesDTO ManagerToSubordinatesDTO)
        {
            Manager Manager = Find(x => x.ManagerUserFK == ManagerToSubordinatesDTO.UserID).FirstOrDefault();

            //if (Manager != null)
            //{
            //    ManagerToSubordinatesDTO.ManagerID = Manager.ManagerID;
            //}
            if (Manager == null)
            {
                User User = userBLL.Find(x => x.UserID == ManagerToSubordinatesDTO.UserID).FirstOrDefault();
                Manager = new Manager
                {
                    ManagerUserFK = User.UserID,
                    ManagerName = User.UserName,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.Now

                };
                Add(Manager);
                //Submit(out exOutputSubmit);
            }
            return Manager;
        }

    }
}
