using EasyAuth;
using EasyAuth.Storage;
using PersonalWebsite.Models;
using System;
using System.Diagnostics;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PersonalWebsite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        // EasyAuth user storage. Entity is good, we'll use that!
        static IUserStore UserStore = EntityUserStore.Instance;

        protected void Application_Start()
        {
            // setup EasyAuth
            EntityUserStore.Instance.ContextType = typeof(WebsiteContext);
            Authentication.UserStore = UserStore;

            // ASP.NET generated code
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);            
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            // pass this to EasyAuth so that it can use cookies etc.
            Authentication.HttpContext = HttpContext.Current;

            // timing for the `page rendered` section
            var stopwatch = new Stopwatch();
            HttpContext.Current.Items["Stopwatch"] = stopwatch;
            stopwatch.Start();
        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {
            //
        }
    }
}