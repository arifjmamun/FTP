using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/bundles/adminlteJs").Include(
                        "~/admin-lte/js/app.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/css/font-awesome.min.css"));
            
            bundles.Add(new StyleBundle("~/bundles/custom").Include(
                      "~/Content/site.css"));
            
            bundles.Add(new StyleBundle("~/bundles/adminlteCss").Include(
                    "~/admin-lte/css/AdminLTE.*",
                    "~/admin-lte/css/skins/_all-skins.min.css"));
        }
    }
}
