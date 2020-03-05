using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using CallCenterNotesApp;

namespace CallCenterNotesApp
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }
        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.AddHeader("pragma", "no-cache");
            Response.AddHeader("Cache-Control", "no-cache");
            Response.CacheControl = "no-cache";
            Response.Expires = -1;
            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            Response.Cache.SetNoStore();

        }
    }
}
