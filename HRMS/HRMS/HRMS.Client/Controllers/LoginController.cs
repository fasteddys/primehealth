using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS.BLL.Logic.Tables;
using HRMS.DTOs.Business;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Client.Controllers
{
    public class LoginController :Controller
    {
        // GET: Login
        public ActionResult Index(string ReturUrl)
        {

            ViewBag.ReturnUrl = ReturUrl;
            return View();
            
        }
        public ActionResult Logout()
        {
            HttpCookie currentUserIDCookie = HttpContext.Request.Cookies["CypressUserID"];
            HttpContext.Response.Cookies.Remove("CypressUserID");
            currentUserIDCookie.Expires = DateTime.Now.AddDays(-1);
            currentUserIDCookie.Value = null;
            HttpContext.Response.SetCookie(currentUserIDCookie);

            HttpCookie currentUserNameCookie = HttpContext.Request.Cookies["CypressUserName"];
            HttpContext.Response.Cookies.Remove("CypressUserName");
            currentUserNameCookie.Expires = DateTime.Now.AddDays(-1);
            currentUserNameCookie.Value = null;
            HttpContext.Response.SetCookie(currentUserNameCookie);

            HttpCookie currentProfilePictureURLCookie = HttpContext.Request.Cookies["ProfilePictureURL"];
            HttpContext.Response.Cookies.Remove("ProfilePictureURL");
            currentProfilePictureURLCookie.Expires = DateTime.Now.AddDays(-1);
            currentProfilePictureURLCookie.Value = null;
            HttpContext.Response.SetCookie(currentProfilePictureURLCookie);

            HttpCookie HasCompletedUserProfileData = HttpContext.Request.Cookies["HasCompletedUserProfileData"];
            HttpContext.Response.Cookies.Remove("HasCompletedUserProfileData");
            HasCompletedUserProfileData.Expires = DateTime.Now.AddDays(-1);
            HasCompletedUserProfileData.Value = null;
            HttpContext.Response.SetCookie(HasCompletedUserProfileData);

            HttpCookie IsHr = HttpContext.Request.Cookies["IsHr"];
            HttpContext.Response.Cookies.Remove("IsHr");
            IsHr.Expires = DateTime.Now.AddDays(-1);
            IsHr.Value = null;
            HttpContext.Response.SetCookie(IsHr);

            HttpCookie IsAdmin = HttpContext.Request.Cookies["IsAdmin"];
            HttpContext.Response.Cookies.Remove("IsAdmin");
            IsAdmin.Expires = DateTime.Now.AddDays(-1);
            IsAdmin.Value = null;
            HttpContext.Response.SetCookie(IsAdmin);

            HttpCookie IsManager = HttpContext.Request.Cookies["IsManager"];
            HttpContext.Response.Cookies.Remove("IsManager");
            IsManager.Expires = DateTime.Now.AddDays(-1);
            IsManager.Value = null;
            HttpContext.Response.SetCookie(IsManager);

            return RedirectToAction("Index", "Login");






        }

        public ActionResult UnAuthorized()
        {
            return View("UnAuthorizedLogin");
        }

        




    }
}
