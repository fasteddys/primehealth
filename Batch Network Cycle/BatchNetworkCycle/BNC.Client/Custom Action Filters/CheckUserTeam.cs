using BNC.BLL.Logic.Tables;
using BNC.BLL.UnitOfWork;
using System;
using BNC.DAL.Factory;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BNC.DTOs.Business;
using System.Web.Routing;

namespace BNC.Client.Custom_Action_Filters
{
    public class CheckUserTeam : FilterAttribute, IActionFilter
    {
        public int TeamID { get; set; }
        private BaseController BaseController;

        public CheckUserTeam()
        {
            BaseController = new BaseController();
        }
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var json = filterContext.HttpContext.Request.Cookies["UserData"].Value;
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var result = serializer.Deserialize <UserDTO> (json);
            bool check = BaseController.UnitOfWork.UserBLL.CheckUserTeam(result.UserID, TeamID);
            if (!check)
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "User",
                        action = "UnAuthorized"
                    })
                );
            }
        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //throw new NotImplementedException();
        }        
    }
}