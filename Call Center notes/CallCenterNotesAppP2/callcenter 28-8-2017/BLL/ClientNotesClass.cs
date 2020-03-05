using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using CallCenterNotesApp.DLL;

namespace CallCenterNotesApp.BLL
{
    public class ClientNotesClass
    {
        PhNetworkEntities db = new PhNetworkEntities();

        public void addClientNotes(string RequestDate, string ClientNamestr, string ClientTypestr, string Notes, string AddClientNotesName)
        {

            ClientNote req = new ClientNote();


            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();

            req.CreationTime = DateTime.Now;
            req.Creator = AddClientNotesName;
            req.ClientName = ClientNamestr;
            req.ClientType = ClientTypestr;
            req.Notes = Notes;
            db.ClientNotes.Add(req);
            db.SaveChanges();
        }

        public bool addUserData(string UserNamestr, string UserTypestr, string AddUserNamestr , out string  ValidationMessage)
        {
            CallCenterAppUser req = new CallCenterAppUser();
            string IfUSerExist = null;
            IfUSerExist = (from u in db.CallCenterAppUsers where u.UserName == UserNamestr.ToLower() select u.UserName).SingleOrDefault();

            if (IfUSerExist==null)
            {
                System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
                req.UserName = UserNamestr.ToLower();
                req.Password = "prime@123";
                req.UserType = UserTypestr;
                req.AddedBy = AddUserNamestr;
                req.AdditionDate = DateTime.Now;
                req.IsFirstLogin = true;
                req.IsActive = false;
                req.MACAddress = null;
                req.IsActive = false;
                req.IsFirstLogin = true;
                
                db.CallCenterAppUsers.Add(req);
                db.SaveChanges();

                ValidationMessage = "User Added Successfully";
                return true;

            }
            if (IfUSerExist != null)
            {
                ValidationMessage = "The user you are trying to add is already exist";
                return false;
            }
            else
            {
                ValidationMessage = "Somthing went wrong ,Please try again";
                return false;
            }
            
        }


        #region Pages Functions
        public List<ClientNote> GetAllClientNotes(string type)
        {
            List<ClientNote> data = (from p in db.ClientNotes
                                     where p.ClientType == type
                                  orderby p.ClientID descending
                                  select p).ToList();
            return data;
        }
        public List<CallCenterAppUser> GetAllUsers()
        {
            List<CallCenterAppUser > data = (from p in db.CallCenterAppUsers
                                     orderby p.UserID descending
                                     select p).ToList();
            return data;
        }

        public void GetDetailByid(int id, ref string CreationDate, ref string Creator, ref string ClientName, ref string Notes)
        {
            var data = from p in db.ClientNotes where p.ClientID == id select p;

            CreationDate = data.First().CreationTime.ToString();
            Creator = data.First().Creator;
            ClientName = data.First().ClientName;
            Notes = data.First().Notes;
        }
        public void GetUserDataByid(int id, ref string UserNamestr)
        {
            var data = from p in db.CallCenterAppUsers where p.UserID == id select p;

            UserNamestr = data.First().UserName;
           
        }

        public void UpdateClientNotes(int id, string CName, string CNotes)
        {
            ClientNote acc = db.ClientNotes.Single(p => p.ClientID == id);
            acc.ClientName = CName;
            acc.Notes = CNotes;
            db.SaveChanges();
        }
        public void UpdateUserData(int id, string Password)
        {
            CallCenterAppUser acc = db.CallCenterAppUsers.Single(p => p.UserID == id);
            acc.Password = Password;
            db.SaveChanges();
        }

        public void DeleteClient(int id)
        {
            ClientNote acc = db.ClientNotes.Single(p => p.ClientID == id);
            db.ClientNotes.Remove(acc);
            db.SaveChanges();
        }
        public void DeleteUserData(int id)
        {
            CallCenterAppUser acc = db.CallCenterAppUsers.Single(p => p.UserID == id);
            db.CallCenterAppUsers.Remove(acc);
            db.SaveChanges();
        }

        #endregion
    }
}