using HRMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using HRMS.DAL.Repositories;
using HRMS.DTOs.Business;
using HRMS.BLL.StaticData;
using System.Security.Principal;
using System.Data.Entity;
using System.Reflection;
using HRMS.Utilities;
using HRMS.BLL.Logic.Shared_Logic;

namespace HRMS.BLL.Logic.Tables
{
    public class UserBLL : Repository<User>
    {
        SubDepartmentBLL subDepartmentBLL;
        DepartmentBLL departmentBLL;
        TimeAttendanceBLL timeAttendanceBLL;
        UserEntitlementChangesBLL userEntitlementChangesBLL;
        UserEntitlementBLL userEntitlementBLL;
        ServiceBLL ServiceBLL;
        DateTime creationDate;
        WorkingWeekBLL workingWeekBLL;
        GenderTypeBLL genderTypeBLL;
        ContractTypeBLL contractTypeBLL;
        ConfigurationBLL configurationBLL;

        //ApprovalFlowDetailsBLL approvalFlowDetail;
        //UserPositionBLL userPositionBLL;
        public UserBLL(HRMSEntities Context, DateTime CreationDate) : base(Context)
        {
            subDepartmentBLL = new SubDepartmentBLL(Context, CreationDate);
            departmentBLL = new DepartmentBLL(Context, CreationDate);
            timeAttendanceBLL = new TimeAttendanceBLL(Context, CreationDate);
            userEntitlementChangesBLL = new UserEntitlementChangesBLL(Context, CreationDate);
            userEntitlementBLL = new UserEntitlementBLL(Context, CreationDate);
            ServiceBLL = new ServiceBLL(Context, CreationDate);
            creationDate = CreationDate;
            workingWeekBLL = new WorkingWeekBLL(Context, CreationDate);
            genderTypeBLL = new GenderTypeBLL(Context, CreationDate);
            contractTypeBLL = new ContractTypeBLL(Context, CreationDate);
            configurationBLL = new ConfigurationBLL(Context, creationDate);

            //approvalFlowDetail = new ApprovalFlowDetailsBLL(Context, creationDate);
            //userPositionBLL = new UserPositionBLL(Context, creationDate);
        }

        public List<WorkingWeekCountDTO> GetWorkingWeekView()
        {
            var WorkingWeekList = workingWeekBLL.GetAll().ToList();

            List<WorkingWeekCountDTO> WorkingWeek = new List<WorkingWeekCountDTO>().ToList();

            foreach (var item in WorkingWeekList)
            {
                WorkingWeekCountDTO WorkingWeekitem = new WorkingWeekCountDTO() {
                    WorkingWeekID = item.WorkingWeekID,
                    WorkingWeekName = item.WorkingWeekName
                };
                WorkingWeekitem.UsersCount = Find(x => x.WorkingWeekFK == item.WorkingWeekID).Count();
                WorkingWeek.Add(WorkingWeekitem);
            }
            return WorkingWeek;
        }

        public List<UserSubDepartmentDTO> SelectUserSubDepartment()
        {
            var ListUser = GetAll().Where(x => x.SubDepartmentFK == null && (x.IsActive == true||x.IsActive==null)).ToList();
            List<UserSubDepartmentDTO> ListUserSubDepartment = new List<UserSubDepartmentDTO>();

            foreach (var item in ListUser)
            {
                UserSubDepartmentDTO UserSubDepartment;             
                
                    UserSubDepartment = new UserSubDepartmentDTO
                    {
                        UserID = item.UserID,
                        UserName = item.LDAPUserName,
                        UserEmail = item.UserEmail,
                    };
                
               
                ListUserSubDepartment.Add(UserSubDepartment);
            }
            return ListUserSubDepartment;
        }

        public List<UsersDepartmentDTO> SelectUserDepartment()
        {
            var ListUser = GetAll().Where(x => x.DepartmentFK == null && x.IsActive == true).ToList();
            List<UsersDepartmentDTO> ListUserDepartment = new List<UsersDepartmentDTO>();

            foreach (var item in ListUser)
            {

                UsersDepartmentDTO UserDepartment = new UsersDepartmentDTO();
                //SubDepartment SubDepartment = new SubDepartment();
                //Department Department = new Department();
                string SubDepartmentName = subDepartmentBLL.Find(x => x.SubDepartmentID == item.SubDepartmentFK)?.FirstOrDefault()?.SubDepartmentName;
                string DepartmentName = departmentBLL.Find(x => x.DepartmentID == item.DepartmentFK)?.FirstOrDefault()?.DepartmentName;



                UserDepartment = new UsersDepartmentDTO
                {
                    UserID = item.UserID,
                    UserName = item.LDAPUserName,
                    UserEmail = item.UserEmail,
                    SubDepartmentName = SubDepartmentName,
                    DepartmentName = DepartmentName,
                };

                ListUserDepartment.Add(UserDepartment);
            }
            return ListUserDepartment;
        }

        public List<SubDepartmentCountDTO> GetSubDepartmentsView()
        {
            var SubDepartmentList = subDepartmentBLL.GetAll().ToList();

            List<SubDepartmentCountDTO> SubDepartments = new List<SubDepartmentCountDTO>().ToList();
            foreach (var item in SubDepartmentList)
            {
                SubDepartmentCountDTO SubDepartmentitem = new SubDepartmentCountDTO();
                SubDepartmentitem.SubDepartmentID = item.SubDepartmentID;
                SubDepartmentitem.SubDepartmentName = item.SubDepartmentName;

                SubDepartmentitem.UsersCount = Find(x => x.SubDepartmentFK == item.SubDepartmentID && x.IsActive == true).Count();
                SubDepartments.Add(SubDepartmentitem);
            }
            return SubDepartments;
        }


        public List<DeparmentCountDTO> GetDepartmentsView()
        {
            var DepartmentList = departmentBLL.GetAll().ToList();

            List<DeparmentCountDTO> Departments = new List<DeparmentCountDTO>().ToList();
            foreach (var item in DepartmentList)
            {
                DeparmentCountDTO Departmentitem = new DeparmentCountDTO();
                Departmentitem.DepartmentID = item.DepartmentID;
                Departmentitem.DepartmentName = item.DepartmentName;

                Departmentitem.SubDepartmentCount = subDepartmentBLL.Find(x => x.DepartmentFK == item.DepartmentID).Count();
                Departments.Add(Departmentitem);
            }
            return Departments;
        }



        public List<UserDTO> GetAllUserBySubDepartment(int SubDepartmentID)
        {
            List<UserDTO> UserList = new List<UserDTO>();
            foreach (var item in Find(x => x.SubDepartmentFK == SubDepartmentID && x.IsActive == true).ToList())
            {
                // UserList.Add(new UserDTO
                UserDTO user = new UserDTO();
                user=new UserDTO
                {
                    UserID = item.UserID,
                    UserName = item.LDAPUserName,

                };
                UserList.Add(user); 

            }

            return UserList;
        }

        public List<UserDTO> GetAllUserInSameDeparment(int UserID)
        {
            List<UserDTO> UserList = new List<UserDTO>();
            int? DepartmentID = Find(x => x.UserID == UserID).FirstOrDefault()?.DepartmentFK;
            foreach (var item in Find(x => x.DepartmentFK == DepartmentID).ToList())
            {
                // UserList.Add(new UserDTO
                UserDTO user = new UserDTO();
                user = new UserDTO
                {
                    UserID = item.UserID,
                    UserName = item.LDAPUserName,

                };
                UserList.Add(user);

            }

            return UserList;
        }

        public List<UserDTO> GetSubDepartmentByDepartmrntIDs(List< int> SubDepartmentIDs)
        {
            List<UserDTO> UserList = new List<UserDTO>();
            foreach (var itemID in SubDepartmentIDs)
            {
                foreach (var item in Find(x => x.SubDepartmentFK == itemID && x.IsActive == true).ToList())
                {
                    // UserList.Add(new UserDTO
                    UserDTO user = new UserDTO();
                    user = new UserDTO
                    {
                        UserID = item.UserID,
                        UserName = item.UserName,

                    };
                    UserList.Add(user);

                }
            }

            return UserList;
        }

        public List<UserDTO> GetUsersByDepartmrntID(List<int> DepartmrntID)
        {
            List<UserDTO> UserList = new List<UserDTO>();
            foreach (var itemID in DepartmrntID)
            {
                foreach (var item in Find(x => x.DepartmentFK == itemID).ToList())
                {
                    // UserList.Add(new UserDTO
                    UserDTO user = new UserDTO();
                    user = new UserDTO
                    {
                        UserID = item.UserID,
                        UserName = item.UserName,

                    };
                    UserList.Add(user);

                }
            }

            return UserList;
        }


        public void SaveUserProfilePicture(ProfilePictureDTO ProfilePicture)
        {
            User User = Find(x => x.UserID == ProfilePicture.UserID).FirstOrDefault();
            User.ProfilePictureURL = ProfilePicture.Path;
            User.ModificationDate = creationDate;
            Edit(User);

        }



        public UserDTO GetUserByID(int UserID)
        {

            User User = Find(x => x.UserID == UserID).FirstOrDefault();
            return new UserDTO
            {
                UserID = User.UserID,
                UserEmail = User.UserEmail,
                UserName = User.UserName

            };
        }

        //public LoginUserDTO Login(LoginDTO LoginDTO, out bool Success)
        //{
        //    DirectoryIdentity LDAP = new DirectoryIdentity(LoginDTO.UserName, LoginDTO.UserPassword);
        //    LoginUserDTO UserDTO = new LoginUserDTO();
        //    Success = false;
        //    if (configurationBLL.Find(x=>x.ConfigurationKey== "CheckLDAPAuthentication").FirstOrDefault().ConfigurationValue=="1")
        //    {
        //        if (LDAP.IsAuthenticated)
        //        {
        //            User User = Find(x => x.LDAPUserName == LoginDTO.UserName)?.FirstOrDefault();

        //            if (User != null)
        //            { 
        //                Success = true;
        //                UserDTO = new LoginUserDTO
        //                {
        //                    UserID = User.UserID,
        //                    UserName = User.LDAPUserName,
        //                    ApprovalFlowFK = User.ApprovalFlowFK,
        //                    UserImgURL = User.ProfilePictureURL,
        //                    HasCompletedUserProfileData = User.HasCompletedUserProfileData == null ? false : User.HasCompletedUserProfileData.Value,
        //                    IsHR = User.IsHR == null ? false : User.IsHR.Value,
        //                    IsAdmin = User.IsAdmin == null ? false : User.IsAdmin.Value,
        //                    IsManager = userPositionBLL.CheckIsManager(User.UserID)

        //                };
        //                User.IsOnline = true;
        //                Edit(User);
        //                
        //                //String XMLBeforeUpdate = User.Serialize(); //XMLObjectConverter.Serialize<User>(User);
        //                Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.Login, UserDTO.UserID, UserDTO.UserName, "Not Logged In", "Logged In", LoginDTO.IPAddress);
        //                return UserDTO;
        //            }
        //            else
        //            {
        //                Success = false;
        //                return UserDTO;
        //            }

        //        }
        //        else if (Find(x => x.LDAPUserName == LoginDTO.UserName&&x.IsOutDomainUser==true&&x.OutDomainPassword== LoginDTO.UserPassword).Count()>0)
        //        {
        //            User User = Find(x => x.LDAPUserName == LoginDTO.UserName && x.IsOutDomainUser == true && x.OutDomainPassword == LoginDTO.UserPassword).FirstOrDefault();
        //            if (User != null)
        //            {
        //                Success = true;
        //                UserDTO = new LoginUserDTO
        //                {
        //                    UserID = User.UserID,
        //                    UserName = User.LDAPUserName,
        //                    ApprovalFlowFK = User.ApprovalFlowFK,
        //                    UserImgURL = User.ProfilePictureURL,
        //                    HasCompletedUserProfileData = User.HasCompletedUserProfileData == null ? false : User.HasCompletedUserProfileData.Value,
        //                    IsHR = User.IsHR == null ? false : User.IsHR.Value,
        //                    IsAdmin = User.IsAdmin == null ? false : User.IsAdmin.Value,
        //                    IsManager = userPositionBLL.CheckIsManager(User.UserID)

        //                };
        //                User.IsOnline = true;
        //                Edit(User);
        //                
        //                //String XMLBeforeUpdate = User.Serialize(); //XMLObjectConverter.Serialize<User>(User);
        //                Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.Login, UserDTO.UserID, UserDTO.UserName, "Not Logged In", "Logged In", LoginDTO.IPAddress);
        //                return UserDTO;
        //            }
        //            else
        //            {
        //                Success = false;
        //                return UserDTO;
        //            }
        //        }
        //        else
        //        {
        //            Success = false;
        //            return UserDTO;
        //        }
        //    }
        //    else
        //    {
        //        User User = Find(x => x.LDAPUserName == LoginDTO.UserName)?.FirstOrDefault();

        //        if (User != null)
        //        {
        //            Success = true;
        //            UserDTO = new LoginUserDTO
        //            {
        //                UserID = User.UserID,
        //                UserName = User.LDAPUserName,
        //                ApprovalFlowFK = User.ApprovalFlowFK,
        //                UserImgURL = User.ProfilePictureURL,
        //                HasCompletedUserProfileData = User.HasCompletedUserProfileData == null ? false : User.HasCompletedUserProfileData.Value,
        //                IsHR = User.IsHR == null ? false : User.IsHR.Value,
        //                IsAdmin = User.IsAdmin == null ? false : User.IsAdmin.Value,
        //                IsManager = userPositionBLL.CheckIsManager(User.UserID)

        //            };
        //            User.IsOnline = true;
        //            Edit(User);
        //            
        //            //String XMLBeforeUpdate = User.Serialize(); //XMLObjectConverter.Serialize<User>(User);
        //            Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.Login, UserDTO.UserID, UserDTO.UserName, "Not Logged In", "Logged In", LoginDTO.IPAddress);
        //            return UserDTO;
        //        }
        //        else
        //        {
        //            Success = false;
        //            return UserDTO;
        //        }
        //    }

        //}


        public bool LogOut(LoginDTO LoginDTO)
        {
            try
            {
                User User = Find(x => x.LDAPUserName == LoginDTO.UserName)?.FirstOrDefault();
                User.IsOnline = false;
                Edit(User);
                //
                Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.Logout, LoginDTO.UserID, "Logged In", "Logged Out", LoginDTO.IPAddress);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
                return false;
            }
        }
        public bool ForceLogOut(LoginDTO loginDTO)
        {
            try
            {
                User User = Find(x => x.LDAPUserName == loginDTO.UserName)?.FirstOrDefault();
                User.IsOnline = false;
                Edit(User);
               // 
                Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.ForceLogOut, loginDTO.UserID, "Logged In", "Force Log Out", loginDTO.IPAddress);
                return true;

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-BLL");
                return false;
            }
        }
        public void EditUser(User user)
        {
            base.Edit(user);



        }
        public void LinkUsersToAccessControl(UserDTO user)
        {

            User Newuser = Find(x => x.UserID == user.UserID).FirstOrDefault();
            var OldUser =  XMLObjectConverter.Serialize(Newuser);
            //Newuser.UserName = user.UserName;
            Newuser.AccessControlUserFK = user.AccessControlUserID;

           Edit(Newuser);
            Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.EditUserData, (int)user.ModifiedByUserId, OldUser, XMLObjectConverter.Serialize(Newuser), "Edit User SubDepartment");

        }

        public void SumbitUsersToSubDepartment(List<SubDepartmentIDListDTO> UsersIDs)
        {
            foreach (var item in UsersIDs)
            {
                User User = Find(x => x.UserID == item.ID).FirstOrDefault();
                var Olduser =  XMLObjectConverter.Serialize(User);

                User.SubDepartmentFK = item.SubDepartmentID;
               Edit(User);
                Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.EditUserData, (int)UsersIDs.FirstOrDefault().ModifiedByUserId, Olduser, XMLObjectConverter.Serialize(User), "Edit User");

            }


        }
        public void SumbitUsersToDepartment(List<DepartmentIDListDTO> UsersIDs)
        {
            foreach (var item in UsersIDs)
            {

                User User =Find(x => x.UserID == item.ID).FirstOrDefault();
                var Olduser =   XMLObjectConverter.Serialize(User);
                User.DepartmentFK = item.DepartmentID;
                Edit(User);
                Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.EditUserData, (int)UsersIDs.FirstOrDefault().ModifiedByUserId, Olduser, XMLObjectConverter.Serialize(User), "Edit User");

            }

        }
        public void GetUserListIDs(List<ListIDDTO> UsersIDs)
        {
            foreach (var item in UsersIDs)
            {
                User user = new User();
                user = Find(x => x.UserID == item.ID).FirstOrDefault();
                var Olduser =  XMLObjectConverter.Serialize(user);
                user.PositionFK = item.PositionFK;
               Edit(user);

                Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.EditUserData,(int) UsersIDs.FirstOrDefault().ModifiedByUserId, Olduser, XMLObjectConverter.Serialize(user), "Edit User");

            }
        }
        //user that will be active from hr or admin and use it to display for notifaction
        public List<UserDTO> GetAllUserThatWillBeActive()
        {
          var users=  this.Find(u => u.IsActive == null);
            List<UserDTO> UsersThatWillBeActive = new List<UserDTO>();
            UserDTO tempUserThatWillBeActive;
            foreach (var user in users)
            {
                tempUserThatWillBeActive = new UserDTO();
                tempUserThatWillBeActive.UserName = user.UserName;
                tempUserThatWillBeActive.UserID = user.UserID;
                UsersThatWillBeActive.Add(tempUserThatWillBeActive);
            }
            return UsersThatWillBeActive;
        }
        public int GetNumberOfAllUserThatWillBeActive()
        {
           return Find(u => u.IsActive == null).Count();
        }

        //------------------------------------------------------------------

        public int GetEmployeesNumberDeserveMedicalInsurance()
        {
            int count = 0;
            List<User> users = Find(x => (x.MedicalID == null || x.MedicalID == "") && x.ContractTypeFK == (int)StaticEnums.ContractTypes.FullTime && x.IsActive == true).ToList();
            foreach(var items in users)
            {
                if(DateTime.Today.CompareTo(items.HireDate.Value.AddMonths(3)) >= 0)
                {
                    count++;
                }
            }
            return count;
        }

        public List<UserDTO> GetEmployeesDeserveMedicalInsurance()
        {
            List<UserDTO> userDTO = new List<UserDTO>();

            List<User> users = Find(x => (x.MedicalID == null || x.MedicalID == "") && x.ContractTypeFK == (int)StaticEnums.ContractTypes.FullTime && x.IsActive == true).ToList();
            foreach(var items in users)
            {
                if (DateTime.Today.CompareTo(items.HireDate.Value.AddMonths(3)) >= 0)
                {
                    userDTO.Add(new UserDTO
                    {
                        UserID = items.UserID,
                        UserName = items.UserName
                    });
                }
                
            }
            return userDTO;
        }
        //------------------------------------------------------------
      
}
}



