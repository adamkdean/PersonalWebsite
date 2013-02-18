using EasyAuth;
using EasyAuth.Storage;
using PersonalWebsite.Models;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PersonalWebsite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        static IUserStore UserStore = EntityUserStore.Instance;

        protected void Application_Start()
        {
            EntityUserStore.Instance.ContextType = typeof(WebsiteContext);
            Authentication.UserStore = UserStore;

            // ASP.NET generated code
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            Authentication.HttpContext = HttpContext.Current;
        }
    }
}