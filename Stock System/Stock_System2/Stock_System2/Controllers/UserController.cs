using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stock_System2.Models;
using PagedList.Mvc;
using PagedList;

namespace Stock_System2.Controllers
{
    public class UserController : Controller
    {
        StockDB DB = new StockDB();
        Mailing mail = new Mailing();
        public ActionResult index()
        {
            return View();
        }
        //
        // GET: /User/
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Login(string UserName, string Password)
        {
            string mess;
            int login_count = (from db in DB.User
                               where db.user_name == UserName && db.password == Password
                               select db).Count();
            if (login_count == 1)
            {
                var data = (from db in DB.User
                            where db.user_name == UserName && db.password == Password
                            select db).SingleOrDefault();
                HttpCookie UID = new HttpCookie("userid", data.user_id.ToString());
                UID.Expires = DateTime.Today.AddMonths(12);
                Response.Cookies.Add(UID);
                HttpCookie UserName2 = new HttpCookie("username", data.user_name);
                UserName2.Expires = DateTime.Today.AddMonths(12);
                Response.Cookies.Add(UserName2);
                HttpCookie password = new HttpCookie("password", data.password.ToString());
                password.Expires = DateTime.Today.AddMonths(12);
                Response.Cookies.Add(password);
                HttpCookie type = new HttpCookie("usertype", data.user_type.ToString());
                type.Expires = DateTime.Today.AddMonths(12);
                Response.Cookies.Add(type);
                HttpCookie role = new HttpCookie("userrole", data.user_role.ToString());
                role.Expires = DateTime.Today.AddMonths(12);
                Response.Cookies.Add(role);
                mess = "LoginSuccess";
            }
            else
            {
                mess = "Login Faild";
            }
            return Json(mess, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ChangePassword(string Password)
        {
            string message;
            int UserId = Convert.ToInt32(Request.Cookies["userid"].Value.ToString());
            User user = DB.User.Where(p => p.user_id == UserId).Single();
             user.password = Password;
             DB.SaveChanges();
             if (user != null)
             {
                 message = "Success";
             }
             else
             {
                 message = "faild";
             }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public  ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(string UserName,string Password,string UserType,string Email,string UserRole)
        {
            string message;
            User user = new User();
            user.user_name = UserName;
            user.password = Password;
            user.user_type = UserType;
            user.user_email = Email;
            user.user_role = UserRole;
            if(UserType=="Engineer")
            {
                user.department = "IT";
            }
            else if(UserType=="Issuance")
            {
                user.department = "Issuance";
            }
            else if(UserType=="TPA")
            {
                user.department = "TPA";
            }
            else if(UserType=="Archiving")
            {
                user.department = "Archiving";
            }
            else if(UserType=="Normal")
            {
                user.department = "Others";
            }
            else
            {
                user.department = "IT";
            }
            DB.User.Add(user);
            DB.SaveChanges();
            if(user!=null)
            {
                message = "success";
               mail.SendMail_AddUser(UserName, Password, Email);
            }
            else
            {
                message = "faild";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
      
        public ActionResult List_Of_Users()
        {
           List<User> user = DB.User.ToList();
            return View(user);
        }
       
        public ActionResult Edit(int UserId)
        {
            var Users_Edit = (from p in DB.User
                              where p.user_id == UserId
                              select p).ToList();
            Session["userid"] = UserId;
            return View(Users_Edit);
        }
        [HttpPost]
        public JsonResult Edit(string UserName,string Password,string UserType,string UserRole,string Email)
        {
            string message;
            int userid=Convert.ToInt32(Session["userid"]);
            User user = DB.User.Where(p => p.user_id == userid).Single();
            user.user_name = UserName;
            user.password = Password;
            user.user_type = UserType;
            user.user_role = UserRole;
            user.user_email = Email;
            DB.SaveChanges();
            if(user!=null)
            {
                message = "EditSuccessfully";
            }
            else
            {
                message = "faild";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Delete(int UserId)
        {
            Session["userid2"] = UserId;
            return View();
        }
        [HttpPost]
        public JsonResult Delete()
        {
            string message;
            int UserId = Convert.ToInt32(Session["userid2"]);
            User us = DB.User.Find(UserId);
            DB.User.Remove(us);
            DB.SaveChanges();
            if(us!=null)
            {
                message = "success";
            }
            else
            {
                message = "faild";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logout()
        {
            return RedirectToAction("Login","User");
        }

    }
}
