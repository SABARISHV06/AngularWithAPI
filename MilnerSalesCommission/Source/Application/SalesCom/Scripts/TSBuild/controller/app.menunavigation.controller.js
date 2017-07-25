// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//
/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
var mtisalescom;
(function (mtisalescom) {
    'use strict';
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Define Sales Commission Menu Navigation Controler                                                     //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    var MenuNavigationControl = (function () {
        function MenuNavigationControl($rootScope, $scope, $location, $q, $http, InitializeService, SessionService, SalesComService, HeaderFactory) {
            this.$scope = $scope;
            this.$location = $location;
            this.$q = $q;
            this.$http = $http;
            this.InitializeService = InitializeService;
            this.SessionService = SessionService;
            this.SalesComService = SalesComService;
            this.HeaderFactory = HeaderFactory;
            this.logout = function () {
                debugger;
                //HeaderFactory.SetTitle("Log-out");
                //this.SessionService.LogOut().success((data: ng.IHttpPromiseCallbackArg<any>): void => {
                //    debugger;
                //    $.bootstrapGrowl("Logout successful.", { type: 'success' });
                //});
            };
            var nav = this;
            $scope.navCollapsed = false;
            $scope.CheckUserLogin();
            $scope.toggleSidebarState = function () {
                $scope.navCollapsed = $scope.navCollapsed;
                $scope.navCollapseTitle = $scope.navCollapsed ? "Show Left Panel" : "Hide Left Panel";
                if ($scope.navCollapsed == false) {
                    $('.row-offcanvas').toggleClass('sidebar-offcanvas');
                    $('.right-side').toggleClass('strech');
                    $('.left-side').toggleClass('collapse-left');
                }
                else {
                    $('.sidebar-offcanvas').toggleClass('row-offcanvas');
                    $('.strech').toggleClass('right-side');
                    $('.collapse-left').toggleClass('left-side');
                }
            };
            $scope.toggleSidebarState();
        }
        MenuNavigationControl.$inject = ['$rootScope', '$scope', '$location', '$q', '$http', 'InitializeService', 'mtisalescom.SalesComService'];
        return MenuNavigationControl;
    }());
    angular
        .module('mtisalescom')
        .controller('MenuNavigationControl', MenuNavigationControl);
})(mtisalescom || (mtisalescom = {}));
//# sourceMappingURL=E:/SVN/MilnerSalesCommission/Source/Application/SalesCom//Scripts/TSBuild/controller/app.menunavigation.controller.js.map