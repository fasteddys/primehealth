using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace CallCenterProviderApprovals.Controllers
{
    public class LoginController : Controller
    {
        PhNetworkEntities db = new PhNetworkEntities();
        // GET: Login
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        public JsonResult LogIn (string UserName, string Password)
        {
            string message;
            var User = (from u in db.CallCenterAppUsers where u.UserName == UserName select u).SingleOrDefault();
            if (User!=null)
            {
                 if (User.Password != Password)
                {
                    message = "Sorry.. incorrect Password ,please check the password";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
                else if(User.Password == Password && User.IsActive == true)
                {
                    var userName = User.UserName;
                    string UserType =(from t in db.EmailApprovalsTechnicalDestinationDims where t.DepartmentName==User.UserType select t.ID).SingleOrDefault().ToString();

                    FormsAuthentication.SetAuthCookie(userName, false);
                    HttpCookie Username = new HttpCookie("UserNameCookie", userName);
                    Username.Expires = DateTime.Today.AddMonths(12);
                    Response.Cookies.Add(Username);
                    HttpCookie Usertype = new HttpCookie("UserTypeCookie", UserType);
                    Usertype.Expires = DateTime.Today.AddMonths(12);
                    Response.Cookies.Add(Usertype);
                    HttpCookie UserID = new HttpCookie("UserIDCookie", User.UserID.ToString());
                    UserID.Expires = DateTime.Today.AddMonths(12);
                    Response.Cookies.Add(UserID);
                    message = "sucess";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
                else if (User.Password == Password && User.IsActive == false)
                {
                    message = "Sorry.. your user is in active ,please contact Development team for more info";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
                
                else
                {
                    message = "Sorry.. Somthing went wrong Please try again";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                message = "Sorry.. user not found Please check your user name";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            
        }


        public ActionResult LogOut ()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "UserNameCookie");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);
            HttpCookie cookie2 = new HttpCookie(FormsAuthentication.FormsCookieName, "UserTypeCookie");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);
            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            //RBACAttribute.CookieUserID = null;
            return RedirectToAction("Index");

        }
    }
}