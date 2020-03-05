using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.BLL.Logic.Helpers;
using HRMS.BLL.StaticData;
using HRMS.DTOs.Business;
using HRMS.Entities;

namespace HRMS.BLL.Logic.Tables
{
    public class UserLoginBLL
    {
        UserBLL userBLL;
        ApprovalFlowDetailsBLL approvalFlowDetailsBLL;
        ManagerBLL managerBLL;

        public UserLoginBLL(HRMSEntities Context, DateTime CreationDate)
        {
            userBLL = new UserBLL(Context, CreationDate);
            approvalFlowDetailsBLL = new ApprovalFlowDetailsBLL(Context, CreationDate);
            managerBLL = new ManagerBLL(Context, CreationDate);
        }

        public LoginUserDTO Login(LoginDTO LoginDTO, out bool Success, string CheckLDAPAuthentication)
        {
            DirectoryIdentity LDAP = new DirectoryIdentity(LoginDTO.UserName, LoginDTO.UserPassword);
            LoginUserDTO UserDTO = new LoginUserDTO();
            Success = false;
            if (
               CheckLDAPAuthentication == "1" || CheckLDAPAuthentication == null)
            {
                if (LDAP.IsAuthenticated)
                {
                    User User = userBLL.Find(x => x.LDAPUserName == LoginDTO.UserName)?.FirstOrDefault();

                    if (User != null)
                    {
                        Success = true;
                        UserDTO = new LoginUserDTO
                        {
                            UserID = User.UserID,
                            UserName = User.LDAPUserName,
                            ApprovalFlowFK = User.ApprovalFlowFK,
                            UserImgURL = User.ProfilePictureURL,
                            HasCompletedUserProfileData = User.HasCompletedUserProfileData == null ? false : User.HasCompletedUserProfileData.Value,
                            IsHR = User.IsHR == null ? false : User.IsHR.Value,
                            IsAdmin = User.IsAdmin == null ? false : User.IsAdmin.Value,
                            IsManager = CheckIsManager(User.UserID)

                        };
                        User.IsOnline = true;
                        userBLL.Edit(User);
                        //
                        //String XMLBeforeUpdate = User.Serialize(); //XMLObjectConverter.Serialize<User>(User);
                        Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.Login, UserDTO.UserID, "Not Logged In", "Logged In", LoginDTO.IPAddress);
                        return UserDTO;
                    }
                    else
                    {
                        Success = false;
                        return UserDTO;
                    }

                }
                else if (userBLL.Find(x => x.LDAPUserName == LoginDTO.UserName && x.IsOutDomainUser == true && x.OutDomainPassword == LoginDTO.UserPassword).Count() > 0)
                {
                    User User = userBLL.Find(x => x.LDAPUserName == LoginDTO.UserName && x.IsOutDomainUser == true && x.OutDomainPassword == LoginDTO.UserPassword).FirstOrDefault();
                    if (User != null)
                    {
                        Success = true;
                        UserDTO = new LoginUserDTO
                        {
                            UserID = User.UserID,
                            UserName = User.LDAPUserName,
                            ApprovalFlowFK = User.ApprovalFlowFK,
                            UserImgURL = User.ProfilePictureURL,
                            HasCompletedUserProfileData = User.HasCompletedUserProfileData == null ? false : User.HasCompletedUserProfileData.Value,
                            IsHR = User.IsHR == null ? false : User.IsHR.Value,
                            IsAdmin = User.IsAdmin == null ? false : User.IsAdmin.Value,
                            IsManager = CheckIsManager(User.UserID)

                        };
                        User.IsOnline = true;
                        userBLL.Edit(User);
                        //
                        //String XMLBeforeUpdate = User.Serialize(); //XMLObjectConverter.Serialize<User>(User);
                        Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.Login, UserDTO.UserID, "Not Logged In", "Logged In", LoginDTO.IPAddress);
                        return UserDTO;
                    }
                    else
                    {
                        Success = false;
                        return UserDTO;
                    }
                }
                else
                {
                    Success = false;
                    return UserDTO;
                }
            }
            else
            {
                User User = userBLL.Find(x => x.LDAPUserName == LoginDTO.UserName)?.FirstOrDefault();

                if (User != null)
                {
                    Success = true;
                    UserDTO = new LoginUserDTO
                    {
                        UserID = User.UserID,
                        UserName = User.LDAPUserName,
                        ApprovalFlowFK = User.ApprovalFlowFK,
                        UserImgURL = User.ProfilePictureURL,
                        HasCompletedUserProfileData = User.HasCompletedUserProfileData == null ? false : User.HasCompletedUserProfileData.Value,
                        IsHR = User.IsHR == null ? false : User.IsHR.Value,
                        IsAdmin = User.IsAdmin == null ? false : User.IsAdmin.Value,
                        IsManager = CheckIsManager(User.UserID)

                    };
                    User.IsOnline = true;
                    userBLL.Edit(User);
                    //
                    //String XMLBeforeUpdate = User.Serialize(); //XMLObjectConverter.Serialize<User>(User);
                    Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.Login, UserDTO.UserID, "Not Logged In", "Logged In", LoginDTO.IPAddress);
                    return UserDTO;
                }
                else
                {
                    Success = false;
                    return UserDTO;
                }
            }

        }
        public bool CheckIsManager(int UserID)
        {
            if (approvalFlowDetailsBLL.Find(x => x.ApprovalFlowPersonFK == UserID).Count() > 0 || managerBLL.Find(x => x.ManagerUserFK == UserID).Count() > 0)
                return true;
            else
                return false;
        }

    }
}
