using System.Web;
using System.Web.Optimization;

namespace BlogMvcErtan
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

          
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //Anasayfa için css dosyaları
            bundles.Add(new StyleBundle("~/HomePage/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/Site.css"
                ));

            //Anasayfa için script dosyaları
            bundles.Add(new ScriptBundle("~/HomePage/script").Include(
                    "~/Scripts/jquery-3.2.1.min.js",
                    "~/Scripts/bootstrap.min.js"
                ));

            //AdminPanel için Css dosyaları
            bundles.Add(new StyleBundle("~/Admin/css").Include(
                "~/Content/Admin/css/bootstrap.min.css",
                "~/Content/Admin/css/metisMenu.min.css",
                "~/Content/Admin/css/morris.css",
                "~/Content/Admin/css/sb-admin-2.css",
                "~/Content/Admin/font-awesome/css/font-awesome.min.css"
                ));

            //AdminPanel için JS dosyaları
            bundles.Add(new ScriptBundle("~/Admin/script").Include(
               "~/Content/Admin/Scripts/jquery.js",
               "~/Content/Admin/Scripts/metisMenu.min.js",
               "~/Content/Admin/Scripts/morris.js",
               "~/Content/Admin/Scripts/sb-admin-2.js",
               "~/Content/Admin/Scripts/morris-data.js",
               "~/Content/Admin/Scripts/raphael.min.js",
               "~/Scripts/bootstrap.min.js",
               "~/Content/Admin/Scripts/sb-admin-2.js"
               ));

            //validate

            bundles.Add(new ScriptBundle("~/Admin/validate").Include(
                "~/Scripts/jquery.validate-vsdoc.js",
                "~/Scripts/jquery.validate.min.js",
               "~/Scripts/jquery.validate.unobtrusive.min.js"
                
                ));

           
        }
    }
}
