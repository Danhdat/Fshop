using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FashionShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Điều hướng về product

            routes.MapRoute("Robots.txt", "robots.txt", new { Controller = "Home", action = "Robots" });

            routes.MapRoute(
               name: "About",
               url: "Home/gioi-thieu",
               namespaces: new[] { "FashionShop.Controllers" },
               defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Contact",
               url: "Home/lien-he",
               namespaces: new[] { "FashionShop.Controllers" },
               defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "Cart",
              url: "gio-hang",
              namespaces: new[] { "FashionShop.Controllers" },
              defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "News",
               url: "Home/tin-tuc",
               namespaces: new[] { "FashionShop.Controllers" },
               defaults: new { controller = "News", action = "News", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "NewsDetail",
               url: "Home/tin-tuc/{meta}-{id}",
               namespaces: new[] { "FashionShop.Controllers" },
               defaults: new { controller = "News", action = "NewsDetail", id = UrlParameter.Optional }
            );


            routes.MapRoute(
               name: "ProductDetail",
               url: "Home/san-pham/{meta}-{id}",
               namespaces: new[] { "FashionShop.Controllers" },
               defaults: new { controller = "Product", action = "Product", id = UrlParameter.Optional }
           );

           routes.MapRoute(
               name: "Product",
               url: "Home/san-pham/{meta}",
               defaults: new { controller = "Product", action = "Product", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Success",
               url: "thanh-cong",
               namespaces: new[] { "FashionShop.Controllers" },
               defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "ProductCates",
               url: "{metatitle}-{ID}",
               namespaces: new[] { "FashionShop.Controllers" },
               defaults: new { controller = "Product", action = "ProductDetail", id = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "Home",
              url: "Home",
              namespaces: new[] { "FashionShop.Controllers" },
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                namespaces: new[] { "FashionShop.Controllers" },
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
