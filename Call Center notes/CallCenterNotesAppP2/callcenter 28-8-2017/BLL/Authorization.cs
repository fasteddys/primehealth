using CallCenterNotesApp.DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterNotesApp.BLL
{
    public class Authorization
    {
        PhNetworkEntities Db = new PhNetworkEntities();
        public CallCenterAppUser GetUser (string UserName)
        {
            var User = (from u in Db.CallCenterAppUsers where u.UserName == UserName select u).FirstOrDefault();
            return User;
        }
        public List<CallCenterAppUser> GetUserByMacAddress(string MAcAddress)
        {
            var Users = (from u in Db.CallCenterAppUsers where u.MACAddress == MAcAddress select u).ToList();
            return Users;
        }
        public bool ChangePassword ( string UserName , string OldPassword , string NewPassword , out string ValidationMessage)
        {
            try
            {
               
               var User = GetUser(UserName);
                if (User.Password!=OldPassword)
                {
                    ValidationMessage = "Old Password dont match ,Please check ypur old password and try gain later";
                    return false;
                }
                if (User.Password == OldPassword)
                {
                    if (User.Password == NewPassword)
                    {
                        ValidationMessage = "Sorry.. you can't enter the same password";
                        return true;
                    }
                    else
                    {
                        User.Password = NewPassword;
                        User.IsFirstLogin = false;
                        Db.SaveChanges();
                        ValidationMessage = "Password Changed Successfully";
                        return true;
                    }
                    
                }
                else
                {
                    ValidationMessage = "Somthing went wrong ,Please try again";
                    return false;
                }

            }
            catch (Exception)
            {
                ValidationMessage = "Somthing went wrong ,Please try again";
                return false;
                throw;
                
            }
        }
        public bool CheckOtherLogin (string UserName , out string ValidationMessage)
        {
            try
            {
                var User = GetUser(UserName);
                var LogingMachineMacAdress = "" /*Helpers.GetPCName().ToString()*/;

                if (User.MACAddress == "" && User.IsActive == false)
                {
                    User.IsActive = true;
                    User.MACAddress = LogingMachineMacAdress;
                    Db.SaveChanges();
                    ValidationMessage = "Success Login";
                    return true;
                }
                else if (User.MACAddress != "" && User.IsActive == true)
                {
                    if (User.MACAddress==LogingMachineMacAdress)
                    {
                        var UsersWithTheSameMacs = GetUserByMacAddress(LogingMachineMacAdress).Where(p=>p.UserID!=User.UserID);
                        if (UsersWithTheSameMacs.Count() != 0)
                        {
                            foreach (var item in UsersWithTheSameMacs)
                            {
                                item.IsActive = false;
                                item.MACAddress = "";
                            }

                            Db.SaveChanges();

                            ValidationMessage = "Success Login";
                            return true;
                        }
                        else
                        {
                            ValidationMessage = "Success Login";
                            return true;
                        }
                        
                    }
                   else if (User.MACAddress != LogingMachineMacAdress)
                    {
                       
                        ValidationMessage = "Sorry!!.. you Can't Login with this User , this User is already Logged in from another machine";
                        return false;
                    }
                    else
                    {
                        ValidationMessage = "Somthing went wrong ,Please try again";
                        return false;
                    }
                }


                else
                {
                    ValidationMessage = "Somthing went wrong ,Please try again";
                    return false;
                }
            }
            catch (Exception)
            {
                ValidationMessage = "Somthing went wrong ,Please try again";
                return false;
                throw;
            }
        }

        public void LogOutInActiveUsers ()
        {
            //waiating development 
        }

    }
}