using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.BLL.Logic.Tables;
using HRMS.Entities;
using HRMS.DTOs.Business;
using static HRMS.BLL.Logic.Tables.UserBLL;
using HRMS.BLL.StaticData;

namespace HRMS.BLL.Logic.Shared_Logic
{
    public class UserPositionBLL
    {
        UserBLL userBLL;
        PositionBLL PositionBLL;
        SubDepartmentBLL subDepartmentBLL;
        ManagerBLL managerBLL;
        DateTime creationDate;
        ApprovalFlowDetailsBLL approvalFlowDetailsBLL;
        ConfigurationBLL configurationBLL;
        public UserPositionBLL(HRMSEntities Context,DateTime CreationDate)
        {
            userBLL = new UserBLL(Context, CreationDate);
            PositionBLL = new PositionBLL(Context, CreationDate);
            subDepartmentBLL = new SubDepartmentBLL(Context, CreationDate);
            managerBLL = new ManagerBLL(Context, CreationDate);
            approvalFlowDetailsBLL = new ApprovalFlowDetailsBLL(Context, CreationDate);
            configurationBLL = new ConfigurationBLL(Context, creationDate);
            creationDate = CreationDate;

        }

        public void AddNewUserToPosition(List<User> ListOfUsers, int PositionID)
        {
            foreach (var item in ListOfUsers)
            {
                var user = userBLL.Find(x => x.UserID == item.UserID).FirstOrDefault();
                user.PositionFK = PositionID;
               
            }
        }
        public void RemoveUserFromPosition(List<User> ListOfUsers, int PositionID)
        {
            foreach (var item in ListOfUsers)
            {
                var user = userBLL.Find(x => x.UserID == item.UserID).FirstOrDefault();
                user.PositionFK = null;

            }
        }
        public List<User> GetAllUserByPosition(int PositionID)
        {
            return userBLL.Find(x => x.PositionFK == PositionID).ToList();
        }
        public List<PositionDTO> GetPositionsView()
        {
            List<PositionDTO> Positions = new List<PositionDTO>().ToList();

            var PositionList =PositionBLL.GetAll().ToList();

            foreach (var item in PositionList)
            {
                PositionDTO Positionitem = new PositionDTO();
                Positionitem.PositionName = item.PositionName;
                Positionitem.PositionID = item.PositionID;

                Positionitem.UsersCount = userBLL.Find(x => x.PositionFK == item.PositionID).Count();
                Positions.Add(Positionitem);
            }
            return Positions;
        }

        public void AddPersonsToPosition(List<User> ListOfUsers, int PositionID)
        {
            List<User> ListOfNewUsers = new List<User>();
            List<User> ListOfDeletedUsers = new List<User>();
            List<User> OldListOfUsers = userBLL.GetAll().Where(x => x.PositionFK == PositionID).ToList();

            foreach (var item in OldListOfUsers)
            {
                if (OldListOfUsers.Where(x => x.PositionFK == PositionID).Count() == 1 &&
                    ListOfUsers.Where(x => x.PositionFK == PositionID).Count() == 1
                    )
                {
                    continue;
                }
                if (OldListOfUsers.Where(x => x.PositionFK == PositionID).Count() == 1 &&
                    ListOfUsers.Where(x => x.PositionFK == PositionID).Count() == 0
                    )

                {
                    ListOfDeletedUsers.Add(item);
                    //add new
                }
                else if (OldListOfUsers.Where(x => x.PositionFK == PositionID).Count() == 0 &&
                      ListOfUsers.Where(x => x.PositionFK == PositionID).Count() == 1
                      )
                {
                    ListOfNewUsers.Add(item);
                }

            }

            AddNewUserToPosition(ListOfNewUsers, PositionID);
            RemoveUserFromPosition(ListOfDeletedUsers, PositionID);


        }


        public void AddNewPersonsToPosition(List<User> ListOfUsers, int PositionID)
        {
            AddNewUserToPosition(ListOfUsers, PositionID);


        }
        public void DeletePersonsPosition(List<User> ListOfUsers, int PositionID)
        {
            RemoveUserFromPosition(ListOfUsers, PositionID);


        }
        public bool CheckIfOnBehalfOfUsers(int UserID)
        {
         if(this.GetOnBehalfOfUsers(UserID).Count > 0)
            {
                return true;
            }
            else
            {
                return false;


            }
        }


            public List<UserDTO> GetOnBehalfOfUsers(int UserID)
        {
            List<User> ListUser = new List<User>();
           User UserOnBehalf = userBLL.Find(x => x.UserID == UserID).FirstOrDefault();
            int? ManagerID = managerBLL.Find(x => x.ManagerUserFK == UserOnBehalf.UserID).FirstOrDefault()?.ManagerID;
           if (UserOnBehalf.IsHR==true)
            {
                ListUser = userBLL.GetAll().ToList();
            }
            /*else
            {
                if (ManagerID != null)
                {
                    ListUser.AddRange(userBLL.Find(x => x.DirectManagerFK == ManagerID));
                }
                List<int> ListSubDepartmentID=  subDepartmentBLL.Find(x => x.TeamManagerFK == ManagerID).Select(x=>x.SubDepartmentID).ToList();
                foreach(var item in ListSubDepartmentID)
                    ListUser.AddRange(userBLL.Find(x=>x.SubDepartmentFK== item));

            }*/
            List<UserDTO> UserPosition = new List<UserDTO>();
            foreach (var item in ListUser)
            {
                UserPosition.Add( new UserDTO
                {
                    UserID = item.UserID,
                    UserName = item.LDAPUserName,
                    UserEmail = item.UserEmail,
                });
            }
            return UserPosition;

        }

            
        public List<UserPositionDTO> SelectUserPosition()
        {
            var ListUser = userBLL.GetAll().Where(x=> x.IsDeleted != true).ToList();
            List<UserPositionDTO> ListUserPosition = new List<UserPositionDTO>();
            foreach (var item in ListUser)
            {
                try
                {
                    UserPositionDTO UserPosition;
                    int active ;
                    if (item.IsActive == true)
                    {
                        active = 0;
                    }
                    else if(item.IsActive==false)
                    {
                        active = 1;

                    }
                    else
                    {
                        active = -1;
                    }
                    if (item.PositionFK == null)
                    {
                       
                        UserPosition = new UserPositionDTO
                        {
                            UserID = item.UserID,
                            UserName = item.UserName,
                            UserEmail = item.UserEmail,
                            isActive = active,
                        };

                    }
                    else
                    {
                        UserPosition = new UserPositionDTO
                        {
                            UserID = item.UserID,
                            UserName = item.UserName,
                            UserEmail = item.UserEmail,
                            isActive = active,
                            PositionName = PositionBLL.Find(x => x.PositionID == item.PositionFK).FirstOrDefault().PositionName
                        };
                    }

                    ListUserPosition.Add(UserPosition);
                }



                catch (Exception ex)
                {


                }
            
            }
            return ListUserPosition;

        }

    }
}