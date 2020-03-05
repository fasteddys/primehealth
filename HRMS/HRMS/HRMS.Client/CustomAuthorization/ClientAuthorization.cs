using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HRMS.Client
{
    public class ClientAuthorization : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            try
            {
                bool IsAuthorized = false;
                using (BaseController baseController = new BaseController())
                {
                    if (baseController.UserID == -1)
                    {
                        return false;

                    }
                    else
                    {
                        IsAuthorized = true;
                        return IsAuthorized;

                    }
                }        
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "Login",
                                action = "UnAuthorized"
                            })
                        );
        }
    }
}