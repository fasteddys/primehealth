using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CallCenterNotesApp.DLL;

namespace CallCenterNotesApp.BLL
{
    public class Account
    {
        PhNetworkEntities _db = new PhNetworkEntities();
        
        //Check if username and password is correct or not
        public bool LogiAuth(string name, string pass, ref string type, ref int id,ref bool? IsFirstLogin)
        {
            try
            {
                var data = from p in _db.CallCenterAppUsers where p.UserName == name.ToLower() && p.IsActive==true select p;
                
                string n = data.First().UserName;
                string pa = data.First().Password;
                
                id = data.First().UserID;
                type = data.First().UserType;
                IsFirstLogin=data.FirstOrDefault().IsFirstLogin;

                if (n.ToLower() == name.ToLower() && pa == pass) {

                    return true; }
                else { return false; }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}