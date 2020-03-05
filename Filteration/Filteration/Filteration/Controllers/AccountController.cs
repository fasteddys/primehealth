using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filteration.Models;
using System.Data;
using System.Web.Routing;

namespace Filteration.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        TpaManagerEntities db = new TpaManagerEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateUser(string status)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            Response.AddHeader("pragma", "no-cache");
            Response.AppendHeader("Expires", "0");

            if (Request.Cookies.Get("TPAUserNameCookie") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.Status = status;
            return View();
        }
        [HttpPost]
        public JsonResult CreateNewUser(string NewUsername, string newPass, string Newmail, string type, string Role)
        {

            User user = new User();
            try
            {
                user.UserName = NewUsername;
                user.Password = newPass;
                user.Email = Newmail;
                user.Type = type;
                user.Role = Role;

                db.Users.Add(user);
                db.SaveChanges();
                return Json(user, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("Some error happened",JsonRequestBehavior.AllowGet);
            }
        }

    }
}
