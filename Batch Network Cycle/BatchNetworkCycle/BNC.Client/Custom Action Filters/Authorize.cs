using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BNC.Client.Authorize
{
    public class BNCAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {

            if (filterContext.HttpContext.Request.Cookies["UserData"] != null)
            {
                //var routeValues = new RouteValueDictionary(new
                //{
                //    controller = "Users",
                //    action = "logIn",
                //});
                //filterContext.Result = new RedirectToRouteResult(routeValues);
                //base.HandleUnauthorizedRequest(filterContext);

            }
            else
            {
                var routeValues = new RouteValueDictionary(new
                {
                    controller = "Users",
                    action = "AnAuthorized",
                });
                filterContext.Result = new RedirectToRouteResult(routeValues);
            }
        }

        private bool IsProfileCompleted(string user)
        {
            // You know what to do here => go hit your database to verify if the
            // current user has already completed his profile by checking
            // the corresponding field
            throw new NotImplementedException();
        }
    }
}