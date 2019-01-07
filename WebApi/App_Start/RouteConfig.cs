using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            string[] NS;
            NS = "WebApi.Controllers.OpenMvc".Split('|');
            routes.MapRoute(
              "OpenMvc", 
              "OpenMvc/{controller}/{action}/{id}",
              new { controller = "TaoBao", action = "Index", id = UrlParameter.Optional }, NS
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

