﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ibrasOneriAnket
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Soru", action = "Index", id = UrlParameter.Optional }
            //);

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
           
   routes.MapRoute(
          name: "KategoriSorulari",
          url: "{area}/{controller}/{action}/{id}/{soruId}",
          defaults: new { area = "Anket", controller = "Soru", action = "Index2", soruId = UrlParameter.Optional }
      );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Soru", action = "Index", id = UrlParameter.Optional }
            );

         
        }
    }
}
