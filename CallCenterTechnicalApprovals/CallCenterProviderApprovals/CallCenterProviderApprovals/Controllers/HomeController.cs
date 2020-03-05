using OnlineApprovals.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CallCenterProviderApprovals.Controllers
{
    public class HomeController : Controller
    {
        PhNetworkEntities db = new PhNetworkEntities();
        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult ChangePassword (ChangePasswordDTO ChangeObj)
        {
            ChangeObj.UserName= @Request.Cookies["UserNameCookie"].Value;

            var User = (from u in db.CallCenterAppUsers where u.UserName == ChangeObj.UserName select u).SingleOrDefault();

            if (User.Password==ChangeObj.OldPassword)
            {
                try
                {
                    User.Password = ChangeObj.NewPassword;
                    db.SaveChanges();
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(2, JsonRequestBehavior.AllowGet);
                    throw;
                }
                
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
    }
}