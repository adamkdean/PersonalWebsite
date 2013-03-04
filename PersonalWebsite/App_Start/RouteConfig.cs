using System.Web.Mvc;
using System.Web.Routing;
using T4MVC;

namespace PersonalWebsite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.LowercaseUrls = true;
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{slug}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, slug = UrlParameter.Optional }
            );
        }
    }
}