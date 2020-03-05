using DAL.CRUD;
using DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


//public class RBACAttributes : ActionFilterAttribute
//{


//  //  Permission_DAL permission = new Permission_DAL();
//  //  //string requiredPermission = String.Format("{0}-{1}", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName);

//  ////  if (permission.CheckIfUserHasPermission(requiredPermission, CookieUserID))







//  //  public override void OnActionExecuted(ActionExecutedContext filterContext)
//  //  {
//  //      RBAC("OnActionExecuted", filterContext.RouteData);
//  //  }

//  //  public override void OnActionExecuting(ActionExecutingContext filterContext)
//  //  {
//  //      RBAC("OnActionExecuting", filterContext.RouteData);
//  //  }
//  //  public override void OnResultExecuting(ResultExecutingContext filterContext)
//  //  {
//  //      RBAC("OnResultExecuting ", filterContext.RouteData);
//  //  }
//  //  public override void OnResultExecuted(ResultExecutedContext filterContext)
//  //  {
//  //      RBAC("OnResultExecuted", filterContext.RouteData);
//  //  }



//  //  private void RBAC(string methodName, RouteData routeData)
//  //  {
//  //      var controllerName = routeData.Values["controller"];
//  //      var actionName = routeData.Values["action"];

//  //     // var message = String.Format("{0}- controller:{1} action:{2}", methodName,controllerName,actionName);
//  //      //Debug.WriteLine(message);
//  //  }
//}










public class RBACAttribute : AuthorizeAttribute
{
    public override void OnAuthorization(AuthorizationContext filterContext)
    {
        Permission_DAL permission = new Permission_DAL();

        string requiredPermission = String.Format("{0}-{1}", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName);


        if (!permission.CheckIfUserHasPermission(requiredPermission))
        {

            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "Index" }, { "controller", "Error" } });
        }

    }
}











//    Permission_DAL permission = new Permission_DAL();

//    public static int? CookieUserID;


//    Permission_DAL permission = new Permission_DAL();
//    string requiredPermission = String.Format("{0}-{1}", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext.ActionDescriptor.ActionName);

//    if (permission.CheckIfUserHasPermission(requiredPermission, CookieUserID))
//    {
//        // filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "Push" }, { "controller", "Notifications" } });
//       // return ViewResult();
//}
//    else
//    {
//        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "Index" }, { "controller", "empty" } });


//        }
//    }


