using System.Web;
using System.Web.Optimization;

namespace PersonalWebsite
{
    public class BundleConfig
    {        
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/foundation").Include(
                "~/Content/javascripts/jquery.js",
                "~/Content/javascripts/foundation.min.js",
                "~/Content/javascripts/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Content/javascripts/modernizr.foundation.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/stylesheets/foundation.css",
                "~/Content/stylesheets/fonts.css",
                "~/Content/stylesheets/app.css"));
        }
    }
}

