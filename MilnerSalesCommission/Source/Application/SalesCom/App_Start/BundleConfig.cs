// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//


using System.Web.Optimization;

namespace SalesCommission
{
    /// <summary>
    /// Bundle angularjs views.
    /// </summary>
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterStyleBundles(bundles);
            RegisterJavascriptBundles(bundles);
            RegisterViewBundles(bundles);
        }

        /// <summary>
        /// Bundle angularjs views.  This is done to provide automatic versioning of the views but it
        /// also reduces the number of file downloads.
        /// </summary>
        /// <param name="bundles">The collection to which this bundle is added.</param>
        private static void RegisterViewBundles(BundleCollection bundles)
        {
            bundles.Add(new PartialsBundle("mtisalescom", "~/appviews").Include(
                            "~/App/views/m/AreYouSure.html",
                            "~/App/views/m/Progress.html",
                            "~/App/views/master.html",
                            "~/App/views/employee.html",
                            "~/App/views/newplan.html",
                            "~/App/views/view.html"));
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            #if DEBUG
                        bundles.Add(new StyleBundle("~/Css")
                            .Include("~/Content/bootstrap.css")
                            .Include("~/Content/bootstrap-datetimepicker.min.css")
                            .Include("~/Content/style.nested.css")
                            .Include("~/Content/font-awesome.css")
                            .Include("~/Content/bootstrap-select.min.css")
                           // .Include("~/Content/bootstrap.min.css")

            );
#else
                          bundles.Add(new StyleBundle("~/Css")
                            .Include("~/Content/bootstrap.css")
                            .Include("~/Content/bootstrap-datetimepicker.min.css")
                            .Include("~/Content/style.nested.css")
                            .Include("~/Content/font-awesome.css")
                            .Include("~/Content/bootstrap-select.min.css")
                                        
                            );
#endif
        }

        private static void RegisterJavascriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Js")
                             .Include("~/Scripts/jquery-{version}.js")
                             .Include("~/Scripts/jquery.bootstrap-growl.js")
                             .Include("~/Scripts/jquery.maskedinput.js")
                             .Include("~/Scripts/jquery.maskMoney.js")
                             .Include("~/Scripts/jquery.maskedinput.js")
                             .Include("~/Scripts/jquery.maskMoney.js")
                             .Include("~/Scripts/bootstrap.js")
                             .Include("~/Scripts/bootstrap-datetimepicker.js")
                             .Include("~/Scripts/bootstrap-select.min.js")
                             .Include("~/Scripts/angular.min.js")
                             .Include("~/Scripts/angular-route.js")
                             .Include("~/Scripts/angular-sanitize.js")
                             .Include("~/Scripts/angular-cookies.js")
                             .Include("~/Scripts/angular-strap.js")
                             .Include("~/Scripts/angular-strap.tpl.js")
                             .Include("~/Scripts/angular-resource.js")
                             .Include("~/Scripts/dirPagination.js")
                             .Include("~/Scripts/angular-ui/ui-bootstrap.js")
                             .Include("~/Scripts/angular-ui/ui-bootstrap-tpls.js")
                             .Include("~/Scripts/angular-sprintf.js")
                             .Include("~/Scripts/angular-touch.js")
                             .Include("~/Scripts/angular-animate.js")
                             .Include("~/Scripts/angular-sortable-view.js")
                             .Include("~/Scripts/ui-utils.js")
                             .Include("~/Scripts/filesaver.js")
                             .Include("~/Scripts/ng-device-detector.js")
                             .Include("~/Scripts/moment.js")
                             .Include("~/Scripts/underscore.js")
                             .Include("~/Scripts/date.format.js")
                             .Include("~/Scripts/datatable/angular-datatables.js")
                             .Include("~/Scripts/xeditable.js")
                             .Include("~/Scripts/csv.js")
                             .Include("~/Scripts/newitems.js")
                             .Include("~/Scripts/viewitems.js")
                             .Include("~/Scripts/viewitems.min.js")
                             .Include("~/Scripts/ui-utils.js")
                             .Include("~/Scripts/splitter.js")
                             .Include("~/Scripts/ng-currency.js")
                             .Include("~/Scripts/resizer.js")
                             .Include("~/Scripts/popBox1.3.0.js")
                             .Include("~/Scripts/angular-export.js")
                             .Include("~/Scripts/isteven-multi-select.js")
                             .Include("~/Scripts/kendo.all.min.js")
                             .Include("~/Scripts/TSBuild/app.module.js")
                             .Include("~/Scripts/TSBuild/controller/app.header.controller.js")
                             .Include("~/Scripts/TSBuild/controller/app.menunavigation.controller.js")
                             .Include("~/Scripts/TSBuild/controller/app.salescom.controller.js")
                             .Include("~/Scripts/TSBuild/services/app.initialize.service.js")
                             .Include("~/Scripts/TSBuild/services/app.salescom.services.js")
                             .Include("~/Scripts/TSBuild/services/app.sessionhandler.services.js")
                             );
        }
    }
}
