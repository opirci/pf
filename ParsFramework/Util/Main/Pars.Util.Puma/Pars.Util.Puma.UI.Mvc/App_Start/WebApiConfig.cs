using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Pars.Util.Puma.UI.Mvc.App_Start;

namespace Pars.Util.Puma.UI.Mvc
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.Formatters.Add(new BrowserJsonFormatter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "Definition",
            //    routeTemplate: "api/{controller}/{action}"
            //);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            
        }
    }
}
