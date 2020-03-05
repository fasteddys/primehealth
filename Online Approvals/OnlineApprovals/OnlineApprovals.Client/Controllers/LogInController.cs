using OnlineApprovals.BLL.Logic;
using OnlineApprovals.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineApprovals.BLL.UnitOfWork;
using OnlineApprovals.BLL.CommonFunctions;
using static OnlineApprovals.BLL.StaticData.StaticEnums;

namespace OnlineApprovals.Client.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult LoginAuthentication(LoginDTO loginDTO)
        {
            try
            {
                int ProviderTypeID = 0;
                int ProviderID = 0;
                string LoginKey = string.Empty;
                var LoginTry = UnitOfWork.OnlineApprovalsProviderBLL.LoginAsPharmacyProvider(loginDTO, out ProviderTypeID, out ProviderID, out LoginKey);
                if (LoginTry)
                {
                    HttpCookie cProviderID = new HttpCookie("ProviderID");
                    cProviderID.Expires = DateTime.Now.AddDays(30);
                    cProviderID.Value = ProviderID.ToString();
                    Response.Cookies.Add(cProviderID);

                    HttpCookie cProviderTypeID = new HttpCookie("ProviderTypeID");
                    cProviderTypeID.Expires = DateTime.Now.AddDays(30);
                    cProviderTypeID.Value = ProviderTypeID.ToString();
                    Response.Cookies.Add(cProviderTypeID);


                    HttpCookie cBrowserLgGenerated = new HttpCookie("BrowserLgGenerated");
                    cBrowserLgGenerated.Expires = DateTime.Now.AddDays(30);
                    cBrowserLgGenerated.Value = LoginKey;
                    Response.Cookies.Add(cBrowserLgGenerated);



                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Failure", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [ClientAuthorization]
        public JsonResult ChangePassword(ChangePasswordDTO ChangeObj)
        {
            ChangeObj.ProviderID = ProviderID;
            ChangeObj.ProviderTypeID = ProviderTypeID;

            var Success = UnitOfWork.OnlineApprovalsProviderBLL.ChangePassword(ChangeObj);
            if (Success == true)
            {
                var SuccessSubmit = UnitOfWork.Submit();
                if (SuccessSubmit == true)
                {
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(2, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Logout()
        {
            HttpCookie currentProviderIDCookie = HttpContext.Request.Cookies["ProviderID"];
            HttpContext.Response.Cookies.Remove("ProviderID");
            currentProviderIDCookie.Expires = DateTime.Now.AddDays(-1);
            currentProviderIDCookie.Value = null;
            HttpContext.Response.SetCookie(currentProviderIDCookie);


            HttpCookie currentProviderTypeIDCookie = HttpContext.Request.Cookies["ProviderTypeID"];
            HttpContext.Response.Cookies.Remove("ProviderTypeID");
            currentProviderTypeIDCookie.Expires = DateTime.Now.AddDays(-1);
            currentProviderTypeIDCookie.Value = null;
            HttpContext.Response.SetCookie(currentProviderTypeIDCookie);



            HttpCookie currentLoginKeyCookie = HttpContext.Request.Cookies["BrowserLgGenerated"];
            HttpContext.Response.Cookies.Remove("BrowserLgGenerated");
            currentLoginKeyCookie.Expires = DateTime.Now.AddDays(-1);
            currentLoginKeyCookie.Value = null;
            HttpContext.Response.SetCookie(currentLoginKeyCookie);

            return RedirectToAction("Index", "Login");
        }

        public ActionResult UnAuthorized()
        {
            return View("UnAuthorizedPage");
        }
    }
}