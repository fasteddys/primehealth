using EmaTours.AdminPanel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EmaTours.AdminPanel.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string UserName , string Password)
        {
            string Message="";
           var logresult=UnitOfWork.EMAUsersBLL.Login(UserName, Password,out Message);
            if (logresult==true)
            {
                var userdata = UnitOfWork.EMAUsersBLL.Find(x=>x.UserName== UserName).FirstOrDefault();

                string username = userdata.UserName;
                FormsAuthentication.SetAuthCookie(username, false);
                HttpCookie Uname = new HttpCookie("UserName", username);
                Uname.Expires = DateTime.Today.AddMonths(12);
                Response.Cookies.Add(Uname);
                HttpCookie UID = new HttpCookie("UserID", userdata.UserID.ToString());
                UID.Expires = DateTime.Today.AddMonths(12);
                Response.Cookies.Add(UID);


                HttpCookie UserType = new HttpCookie("UserType", userdata.UserTypeFK.ToString());
                UID.Expires = DateTime.Today.AddMonths(12);
                Response.Cookies.Add(UserType);

                FormsAuthentication.SetAuthCookie(username, true);

                return RedirectToAction("Index", "Home");
            }
         
            else
            {
                ViewBag.error = Message;
                return View();
            }       

        }
        public ActionResult LogOut()
        {
            // FormsAuthentication.SignOut();

            try
            {
                var UserNameCookie = HttpContext.Request.Cookies.Get("UserName");

                UserNameCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(UserNameCookie);


                var UserIDCookie = HttpContext.Request.Cookies.Get("UserID");
                UserIDCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(UserIDCookie);
            }
            catch
            {

            }

            return RedirectToAction("Login");
        }

    }
}