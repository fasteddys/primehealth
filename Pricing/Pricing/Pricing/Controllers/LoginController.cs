using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Pricing.Models;

namespace Pricing.Controllers
{
    
    public class LoginController : Controller
    {
        PricingDBEntities1 DB = new PricingDBEntities1();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        /* Takes the name and password sent from the view
         * Check the username and password from the users table in database 
         * if wrong return to the login page with viewbag error
         * if right set the authentication cookie t the name of the logged in user 
         * takes the username , user type and user email each in a cookie to use them in the application
         */

        [HttpPost]
        public ActionResult Login(string UserName, string password)
        {
            var data = (from p in DB.LoginTBs
                        where p.UserName == UserName && p.Password == password
                        select p).SingleOrDefault();

            if (data == null)
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

                HttpCookie Utype = new HttpCookie("UserTypeCookie", data.UserType);
                Utype.Expires = DateTime.Today.AddMonths(12);
                Response.Cookies.Add(Utype);
                return RedirectToAction("Create","Home");
            }
        }
        // Logut from the account and delete the authentication cookie that was saved on log in

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}