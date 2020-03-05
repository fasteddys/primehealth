using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.DLL;

namespace WebApplication1.BLL
{
    public class Accounts
    {
        TPASystemDataContext _db = new TPASystemDataContext();
        public bool LogiAuth(string name, string pass, ref string type, ref int id)
        {
            try
            {
                var data = from p in _db.userTBs
                           where p.UserName == name
                           select p;
                string n = data.First().UserName;
                string pa = data.First().Password;
                id = data.First().id;

                type = data.First().Type;

                if (n == name && pa == pass)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void updateON(int id)
        {
            userTB acc = _db.userTBs.Single(p => p.id == id);
            acc.IsOnline = "true".Trim();
            _db.SubmitChanges();

        }
        public void updateOFF(int id)
        {
            userTB acc = _db.userTBs.Single(p => p.id == id);
            acc.IsOnline = "false".Trim();
            _db.SubmitChanges();
        }
        #region add user
        public bool adduser(string name, string pass, string type)
        {
            bool IsExists = _db.userTBs.Any(pa => pa.UserName.Equals(name));
            if (IsExists)
            {
                return false;
            }
            else
            {
                userTB acc = new userTB();
                acc.UserName = name;
                acc.Type = type;
                acc.Password = pass;
                _db.userTBs.InsertOnSubmit(acc);
                _db.SubmitChanges();
                return true;
            }
        }
        #endregion

        #region UpdateUser
        public void UpdateUser(int id,string name, string pass, string type)
        {
            userTB ac = _db.userTBs.Single(p => p.id == id);
            ac.UserName = name;
            ac.Password = pass;
            ac.Type = type;
            _db.SubmitChanges();
        }
        #endregion

        #region Passwords
        public string GetPassword(string name)
        {
            userTB ac = _db.userTBs.Single(p => p.UserName == name);
            return ac.Password;
        }
        public void UpdatePassword(string name ,string pass)
        {
            userTB ac = _db.userTBs.Single(p => p.UserName == name);
            ac.Password = pass;
            _db.SubmitChanges();
        }
        #endregion
        public void DeleteUser(int id)
        {
            userTB ac = _db.userTBs.Single(p => p.id == id);
            _db.userTBs.DeleteOnSubmit(ac);
            _db.SubmitChanges();
        }
        public IEnumerable<userTB> GetData()
        {
            List<userTB> data = (from p in _db.userTBs
                                    orderby p.id descending
                                    select p).ToList();
            return data;
        }
    }
}