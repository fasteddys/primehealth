using DAL.CRUD;
using DAL.DataBase;
using Harkos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace TickitingSystem.Controllers
{
    public class LoginController : Controller
    {
        User_DAL userDAL = new User_DAL();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string name , string Password)
        {
           // DirectoryIdentity ldap = new DirectoryIdentity(name,Password);
           // if (ldap.IsAuthenticated==true)
         //   {
                     User user = userDAL.getuserbyname(name);
                     Permission_DAL.Userid = user.User_ID;
                    var logresult = userDAL.Login(user.User_ID, Password);
                    var userdata = userDAL.GetUserById(user.User_ID);
                    //RBACAttribute.CookieUserID = userdata.User_ID;
                    string username = userdata.User_Name;
                    FormsAuthentication.SetAuthCookie(username,false);
                    HttpCookie Uname = new HttpCookie("UserNameCookie", userdata.User_Name);
                    Uname.Expires = DateTime.Today.AddMonths(12);
                    Response.Cookies.Add(Uname);
                    HttpCookie UID = new HttpCookie("UserIDCookie", userdata.User_ID.ToString());
                    UID.Expires = DateTime.Today.AddMonths(12);
                    Response.Cookies.Add(UID);
                    HttpCookie DEpID = new HttpCookie("DepIDCookie", userdata.Dept_ID.ToString());
                    DEpID.Expires = DateTime.Today.AddMonths(12);
                    Response.Cookies.Add(DEpID);

                    return RedirectToAction("Index", "Dashboard");
          //  }
           // else
           // {
            //    ViewBag.error = "Not Registerd User";
            //    return View();
            //}
            //her is comment end

            //User_DAL user = new User_DAL();
            //var logresult = user.Login(id, Password);
            //if (logresult== "success")
            //{
            //    var userdata = user.GetUserById(id);
            //    string username = userdata.User_Name;
            //    FormsAuthentication.SetAuthCookie(username, false);
            //    HttpCookie Uname = new HttpCookie("UserNameCookie", userdata.User_Name);
            //    Uname.Expires = DateTime.Today.AddMonths(12);
            //    Response.Cookies.Add(Uname);
            //    HttpCookie UID = new HttpCookie("UserIDCookie", userdata.User_ID.ToString());
            //    UID.Expires = DateTime.Today.AddMonths(12);
            //    Response.Cookies.Add(UID);
            //    HttpCookie DEpID = new HttpCookie("DepIDCookie", userdata.Dept_ID.ToString());
            //    DEpID.Expires = DateTime.Today.AddMonths(12);
            //    Response.Cookies.Add(DEpID);

            //    return RedirectToAction("Index", "Dashboard");
            //}
            //if (logresult=="wrongpassword")
            //{
            //    ViewBag.error = "Please Check password";
            //    return View();
            //}
            //if (logresult== "deleteduser")
            //{
            //    ViewBag.error = "Access Denied !! Deleted User ... Contact System Admin";
            //    return View();
            //}
            //else
            //{
            //    ViewBag.error = "Not Registerd User";
            //    return View();
            //}       
        }
        [Authorize]
        public ActionResult LogOut()
        {


            //    var UserNameCookie = new HttpCookie("UserNameCookie");
            //var UserIDCookie = new HttpCookie("UserIDCookie");
            //

            //UserNameCookie.Expires = DateTime.Now.AddDays(-1);
            //UserIDCookie.Expires = DateTime.Now.AddDays(-1);
            //DepIDCookie.Expires = DateTime.Now.AddDays(-1);



            //Response.Cookies.Add(UserNameCookie);
            //Response.Cookies.Add(UserIDCookie);
            //Response.Cookies.Add(DepIDCookie);
            FormsAuthentication.SignOut();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "UserNameCookie");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);
            HttpCookie cookie2 = new HttpCookie(FormsAuthentication.FormsCookieName, "UserIDCookie");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);
            HttpCookie cookie3 = new HttpCookie(FormsAuthentication.FormsCookieName, "DepIDCookie");
            cookie3.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie3);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            //RBACAttribute.CookieUserID = null;

            return RedirectToAction("Login");
        }

    }
}