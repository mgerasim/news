using System.Web;
using System.Web.Optimization;

namespace News
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/metro").Include(
                        "~/Scripts/metro.js"));

            bundles.Add(new ScriptBundle("~/bundles/clipboard").Include(
                        "~/Scripts/clipboard.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include(
                      "~/Scripts/ckeditor.js"));

            bundles.Add(new ScriptBundle("~/bundles/kingnews").Include(
                      "~/Scripts/core.min.js",
                      "~/Scripts/html5shiv.js",
                      "~/Scripts/pointer-events.js",
                      "~/Scripts/script.js"));

            bundles.Add(new StyleBundle("~/Content/kingnews").Include("~/Content/style.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/metro").Include(
                "~/Content/metro.css",
                "~/Content/metro-icons.css",
                "~/Content/metro-responsive.css"
                ));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            bundles.Add(new StyleBundle("~/Content/datepicker").Include(
                      "~/Content/bootstrap-datepicker.css",
                      "~/Content/bootstrap-datepicker3.css"));


            bundles.Add(new StyleBundle("~/Content/selectpicker").Include(
                         "~/Content/bootstrap-select.css")
                         );



            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                        "~/Scripts/bootstrap-datepicker.js",
                        "~/Scripts/locales/bootstrap-datepicker.ru.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/selectpicker").Include(
                        "~/Scripts/bootstrap-select.js"));


        }
    }
}