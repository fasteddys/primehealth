using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DispatchingMVCVer1.Models;

namespace DispatchingMVCVer1.Controllers
{
    public class LoginController : Controller
    {
        DisPatchingDBEntities DB = new DisPatchingDBEntities();

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

      

        [HttpPost]
        public ActionResult Login(string UserName, string password)
        {
            var data = (from p in DB.Users
                        where p.UserName == UserName && p.Password == password
                        select p).SingleOrDefault();

            if (data == null || data.UserType==null)
            {
                ViewBag.error = "Wrong user name or password";
                return View();              
            }
            else
            {
                FormsAuthentication.SetAuthCookie(UserName, false);

                HttpCookie Uname = new HttpCookie("UserNameCookie", data.UserName);
                Uname.Expires = DateTime.Today.AddMonths(12);
                Response.Cookies.Add(Uname);

                HttpCookie UID = new HttpCookie("ActiveUserIDCookie", data.UserID.ToString());
                UID.Expires = DateTime.Today.AddMonths(12);
                Response.Cookies.Add(UID);

                HttpCookie Utype = new HttpCookie("UserTypeCookie", data.UserType);
                Utype.Expires = DateTime.Today.AddMonths(12);
                Response.Cookies.Add(Utype);

                HttpCookie Role = new HttpCookie("UserRoleCookie", data.Role);
                Role.Expires = DateTime.Today.AddMonths(12);
                Response.Cookies.Add(Role);

                HttpCookie Umail = new HttpCookie("UserEmailCookie", data.Email);
                Umail.Expires = DateTime.Today.AddMonths(12);
                Response.Cookies.Add(Umail);

                HttpCookie ERole = new HttpCookie("UserERoleCookie", data.EditRole);
                Umail.Expires = DateTime.Today.AddMonths(12);
                Response.Cookies.Add(ERole);

                return RedirectToAction("DashboardResults", "RequestFunction");
            }
        }
        // Logut from the account and delete the authentication cookie that was saved on log in

        public ActionResult Logout()
        {
    
            return RedirectToAction("Login", "Login");
        }
    }
}