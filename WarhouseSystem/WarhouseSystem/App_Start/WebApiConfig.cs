﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WarhouseSystem
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            config.MapHttpAttributeRoutes();
            // Web API routes
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
          
        }
      
    }
}
