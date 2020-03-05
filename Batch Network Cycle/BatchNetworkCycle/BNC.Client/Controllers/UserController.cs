using BNC.DTOs.Business;
using BNC.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BNC.Client.Controllers
{
    public class UserController : BaseController
    {
        public ActionResult Login()
        {

            return View();
        }
        public ActionResult UnAuthorized()
        {
            return View();
        }
        [HttpPost]
        public JsonResult UserLogIn(UserDTO UserLogin)
        {
            ResultViewModel Result = new ResultViewModel();

            try
            {

                UserDTO user = UnitOfWork.UserBLL.Login(UserLogin);
                if (user != null)
                {
                    Response.Cookies["UserData"].Value = new JavaScriptSerializer().Serialize(user);
                    Result = new ResultViewModel
                    {
                        Message = "",
                        Success = true,
                        Data = null,
                    };

                }
                else
                {
                    Result = new ResultViewModel
                    {
                        Message = "User Name Or Password is Wrong",
                        Success = false,
                        Data = null,
                    };
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "BNCApplication-Client");

                Result = new ResultViewModel
                {
                    Message = ex.Message,
                    Success = false,
                    Data = null,

                };
            }
            return Json(Result);

        }
        public ActionResult LogOut()
        {
            if (Request.Cookies["UserData"] != null)
            {
                var c = new HttpCookie("UserData");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
            return RedirectToAction("Login");
        }
    }
}