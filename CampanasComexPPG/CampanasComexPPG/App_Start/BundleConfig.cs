using System.Web;
using System.Web.Optimization;

namespace CampanasPPG
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.

            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(                      
                      "~/Scripts/jquery-3.3.1.min.js",
                      "~/Scripts/jquery.treegrid.js",
                      "~/Scripts/jquery.smartWizard.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularJs").Include(
                      "~/Scripts/angular.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapJs").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/bootstrap-treefy.min.js",
                      "~/Scripts/bootstrap-notify.js",
                      "~/Scripts/bootstrap-datepicker.min.js",
                      "~/Scripts/locales/bootstrap-datepicker.es.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/loadersJs").Include(
                      "~/Scripts/loaders.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryUI").Include(
                      "~/Scripts/jquery-ui.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-treefy.min.css",
                      //"~/Content/font-awesome.min.css",
                      "~/Content/FontAwesome/all.css",
                      "~/Content/Loaders/loaders.css",
                      "~/Content/jquery.treegrid.css",
                      "~/Content/smart_wizard.min.css",
                      "~/Content/smart_wizard_theme_arrows.css",
                      "~/Content/bootstrap-datepicker.css",
                      "~/Content/Shared/_Layout.css",
                      "~/Content/Shared/LoaderLayout.css",
                      "~/Content/jquery-ui.css"));

            bundles.Add(new StyleBundle("~/Content/Campana/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-treefy.min.css",
                      //"~/Content/font-awesome.min.css",
                      "~/Content/FontAwesome/all.css",
                      "~/Content/Loaders/loaders.css",
                      "~/Content/jquery.treegrid.css",
                      "~/Content/smart_wizard.min.css",
                      "~/Content/smart_wizard_theme_arrows.css",
                      "~/Content/bootstrap-datepicker.css",
                      "~/Content/Shared/_LayoutCampana.css",
                      "~/Content/Shared/LoaderLayout.css",
                      "~/Content/jquery-ui.css"));
        }
    }
}
