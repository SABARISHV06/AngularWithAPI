// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//
/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />


module mtisalescom {
    'use strict';

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Define Sales Commission Menu Navigation Controler                                                     //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    class MenuNavigationControl {

        static $inject = ['$rootScope', '$scope', '$location', '$q', '$http', 'InitializeService', 'mtisalescom.SalesComService'];
        constructor($rootScope, private $scope: ISalesCommissionScope, private $location: ng.ILocationService,
            private $q: ng.IQService, public $http: ng.IHttpService, private InitializeService: InitializeService, private SessionService: ISessionHandlerService,
            public SalesComService: SalesComService, public HeaderFactory: HeaderFactoryClass) {
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
            }
                            
            
            $scope.toggleSidebarState();
        }
            logout = (): void => {
            debugger;
            //HeaderFactory.SetTitle("Log-out");
            //this.SessionService.LogOut().success((data: ng.IHttpPromiseCallbackArg<any>): void => {
            //    debugger;
            //    $.bootstrapGrowl("Logout successful.", { type: 'success' });
            //});
        }
    }

    angular
        .module('mtisalescom')
        .controller('MenuNavigationControl', MenuNavigationControl)
}