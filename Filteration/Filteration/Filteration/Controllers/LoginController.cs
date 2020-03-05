using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filteration.Models;
using System.Web.Security;

namespace Filteration.Controllers
{
    public class LoginController : Controller
    {
        TpaManagerEntities db = new TpaManagerEntities();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            var data = (from p in db.Users
                        where p.UserName == UserName && p.Password == Password
                        select p).SingleOrDefault();
            if (data == null)
            {
                ViewBag.error = "Wrong user name or password";
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(UserName,false);

                HttpCookie Uname = new HttpCookie("TPAUserNameCookie", data.UserName);
                Uname.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(Uname);

                HttpCookie AdminRole = new HttpCookie("RoleCookie", data.Role);
                AdminRole.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(AdminRole);

                HttpCookie UserID = new HttpCookie("IDCookie",data.Id.ToString());
                UserID.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(UserID);

                HttpCookie Utype = new HttpCookie("UserTypeCookie", data.Type);
                Utype.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(Utype);
                if (data.Type == "TPAadmin" || data.Type == "TPA" || data.Role == "ITAdmin")
                {
                   
                    return RedirectToAction("Dashboard", "Request");

                }
                else
                {
                    
                    return RedirectToAction("QualityHome", "Request");
                }
            }
        }
        public ActionResult Logout()
        {

            //Response.Cookies.Clear();
            //FormsAuthentication.SignOut();
            //Session.Clear();
            string[] myCookies = Request.Cookies.AllKeys;
            foreach(string cookie in myCookies)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Login", "Login");
            //if (Request.Cookies["TPAUserNameCookie"] !=null)
            //{
            //    HttpCookie UserCookie = new HttpCookie("TPAUserNameCookie");
            //    UserCookie.Expires = DateTime.Now.AddDays(-1);
            //    Response.Cookies.Add(UserCookie);
            //    Session.Clear();

            //}
           // HttpContext.Response.Cookies.Set(new HttpCookie("TPAUserNameCookie") { Value = string.Empty });
            //Response.Cookies.Remove("TPAUserNameCookie");

            //if (Request.Cookies["RoleCookie"] !=null)
            //{
            //    HttpCookie roleCookie = new HttpCookie("RoleCookie");
            //    roleCookie.Expires = DateTime.Now.AddDays(-1);
            //    Response.Cookies.Add(roleCookie);

            //}
           // Response.Cookies.Remove("RoleCookie");
            
            //if (Request.Cookies["IDCookie"] != null)
            //{
            //    HttpCookie idCookie = new HttpCookie("IDCookie");
            //    idCookie.Expires = DateTime.Now.AddDays(-1);
            //    Response.Cookies.Add(idCookie);
            //}
            //Response.Cookies.Remove("IDCookie");

            //if (Request.Cookies["UserTypeCookie"] != null)
            //{
            //    HttpCookie UsertypeCookie = new HttpCookie("UserTypeCookie");
            //    UsertypeCookie.Expires = DateTime.Now.AddDays(-1);
            //    Response.Cookies.Add(UsertypeCookie);
            //}
            //Response.Cookies.Remove("UserTypeCookie");
            //if (Request.Cookies.Get("TPAUserNameCookie") != null)
            //{
            //    return RedirectToAction("Login", "Login");

            //}
            //else
            //{
            //    return RedirectToAction("Login", "Login");
            //}
            

        }

    }
}
