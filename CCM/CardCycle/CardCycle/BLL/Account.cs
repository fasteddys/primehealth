using CardCycle.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace CardCycle.BLL
{
    public class Account
    {
        DataContextDataContext db = new DataContextDataContext();
        public  bool LogiAuth(string name,string pass,ref string type,ref int id)
        {
            try
                {
                var data=from p in  db.accountTBs
                 where p.name == name
                 select p;
            string n= data.First().name;
            string pa=data.First().password;
            id = data.First().id;
            //string t=data.First().type;
            type = data.First().type;
                // check user authentecation
                if(n==name && pa==pass)
                {
                return true;
               // type = data.First().type; ;
                }
                else
                return false;
            }
            catch(Exception)
            {
            return false;
            }
        
        
        }
        public void updateON(int id)
        {
            accountTB acc = db.accountTBs.Single(p => p.id == id);
            acc.isOnline = "true".Trim();
            db.SubmitChanges();

        }
        public void updateOFF(int id)
        {
            accountTB acc = db.accountTBs.Single(p => p.id == id);
            acc.isOnline = "false".Trim();
            db.SubmitChanges();

        }
        // user Methods
        public void BindDDLUser(DropDownList ddl)
        {
            var data = from p in db.accountTBs
                       orderby p.name
                       select new { p.name };
            ddl.DataSource = data;
            ddl.DataTextField = "name";
            ddl.DataBind();
            //return data;
        }
        public List<accountTB> getQualityUser()
        {
            var data = (from p in db.accountTBs
                       orderby p.name
                       where p.type =="QualityControl"
                       select p).ToList();
            //ddl.DataSource = data;
            //ddl.DataTextField = "name";
            //ddl.DataBind();
            return data;
        }
        public void BAckAllUser(string user, ref string name, ref string pass, ref string type)
        {
            var data = from p in db.accountTBs
                       where p.name == user
                       select p;
            name = data.First().name;
            pass = data.First().password;
            type = data.First().type;

        }
        public void UpdateUser(string user,string name,string pass,string type)
        {
            accountTB ac = db.accountTBs.Single(p => p.name == user);
            ac.name = name;
            ac.password = pass;
            ac.type = type;
            db.SubmitChanges();
        }
        public void UpdateUserBYUser(string user, string name, string pass)
        {
            accountTB ac = db.accountTBs.Single(p => p.name == user);
           // ac.name = name;
            ac.password = pass;
           
            db.SubmitChanges();
        }
        public void DeleteUser(string user)
        {
            accountTB ac = db.accountTBs.Single(p => p.name == user);
            //ac.name = name;
            //ac.password = pass;
            //ac.type = type;
            db.accountTBs.DeleteOnSubmit(ac);
            db.SubmitChanges();
        }
        public void adduser(string name,string pass,string type)
        {
            accountTB acc = new accountTB();
            acc.name = name;
            acc.type = type;
            acc.password = pass;
            db.accountTBs.InsertOnSubmit(acc);
            db.SubmitChanges();

        }
        public void BindDDlReport(DropDownList ddl)
        {
            var data = from p in db.accountTBs
                       where p.type == "It" || p.type == "Issuance" || p.type == "Quality Control"
                       select p;
            ddl.DataSource = data;
            ddl.DataTextField = "name";
            ddl.DataBind();
        }


       

    }
}