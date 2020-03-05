using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using OnlineApprovals.Entities;

namespace OnlineApprovals.Client
{
    public class ClientAuthorization : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            try
            {
                bool IsAuthorized = false;
                using (PhNetworkEntities entities = new PhNetworkEntities())
                {
                    BaseController baseController = new BaseController();
             
                        if (baseController.ProviderID == -1 || baseController.ProviderTypeID == -1 || baseController.LoginKey == "0")
                        {
                            return false;

                        }
                        else
                        {
                            var ProviderID = baseController.ProviderID;
                            var ProviderTypeID = baseController.ProviderTypeID;
                            var LoginKeys = entities.OnlineApprovals_LoginKeys.Where(p => p.ProviderTypeID == ProviderTypeID && p.ProviderID == ProviderID && p.IsActive == true).Select(p => p.ProviderKey).ToList();
                            if (LoginKeys.Contains(baseController.LoginKey))
                            {
                                IsAuthorized = true;
                            }
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