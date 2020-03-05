using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DispatchingMVCVer1.Models;

namespace DispatchingMVCVer1.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        //
        // GET: /User/
        DisPatchingDBEntities DB = new DisPatchingDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        //call when we want to view page design
        public ActionResult Adduser()
        {
            return View();
        }
        //call when we want to Add user in Database using jquery
        [HttpPost]
        public JsonResult Adduser(User insertuser)
        {
            string message = "";
            try
            {
               
                DB.Users.Add(insertuser);
                DB.SaveChanges();
                message = "Inserted Successfully";
                
            }
            catch
            { message = "Nooooot"; }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        // Select the user's row who want to change password from the table users
        public ActionResult ChangePassword()
        {
            string Name = Request.Cookies["UserNameCookie"].Value;
            var UserID = (from i in DB.Users
                          where i.UserName == Name
                          select i).SingleOrDefault();
            return View("ChangePasswordPartial",UserID);
        }
        [HttpPost]
        // Takes the new password from the jquery and update in in database
        public JsonResult UpdatePassword(string password , string id)
        {
            int UserId = int.Parse(id);
            User U = DB.Users.Find(UserId);
            string message = "";
            try
            {
                U.Password = password;
                DB.SaveChanges();
                message = "Password Changed";
            }
            catch
            {
                message = "Somthing went wrong";
                
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

	}
}