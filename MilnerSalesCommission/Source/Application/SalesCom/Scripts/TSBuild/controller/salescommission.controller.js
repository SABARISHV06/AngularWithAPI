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
    //Define Route Configuration                                                                           //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    var Config = (function () {
        function Config($routeProvider, $httpProvider) {
            debugger;
            $routeProvider.
                when('/', {
                controller: SalesCommissionControl, templateUrl: 'App/views/master.html',
                controllerAs: 'salescom',
            });
            $routeProvider.when("/view", {
                controller: SalesCommissionControl, templateUrl: 'App/views/view.html',
                controllerAs: 'salescom'
            });
            delete $httpProvider.defaults.headers.common['X-Requested-With'];
        }
        Config.$inject = ['$routeProvider', '$httpProvider'];
        return Config;
    }());
    mtisalescom.Config = Config;
    var HeaderController = (function () {
        function HeaderController($scope, HeaderFactory) {
            this.$scope = $scope;
            this.HeaderFactory = HeaderFactory;
            debugger;
            $scope.Header = HeaderFactory;
            $scope.Header.SetTitle("Sales Commission");
        }
        HeaderController.$inject = ['$scope', 'HeaderFactoryClass'];
        return HeaderController;
    }());
    var MenuNavigationControl = (function () {
        function MenuNavigationControl($rootScope, $scope, $location, $q, $http, InitializeService, SalesComService) {
            this.$scope = $scope;
            this.$location = $location;
            this.$q = $q;
            this.$http = $http;
            this.InitializeService = InitializeService;
            this.SalesComService = SalesComService;
            debugger;
            var nav = this;
            $scope.navCollapsed = false;
            $scope.GoToManageNewPlan();
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
            $scope.$on('GetRoles', function () {
                nav.InitializeService.GetRoles().then(function (data) {
                    $scope.Roleslists = data;
                });
            });
            $scope.toggleSidebarState();
        }
        MenuNavigationControl.$inject = ['$rootScope', '$scope', '$location', '$q', '$http', 'InitializeService', 'mtisalescom.SalesComService'];
        return MenuNavigationControl;
    }());
    var SalesCommissionControl = (function () {
        function SalesCommissionControl($location, $rootScope, $scope, $q, $http, $routeParams, SalesComService, InitializeService, $uibModal, HeaderFactory, $compile, deviceDetector, $templateCache, $timeout, $route, $interval, ActionRequest) {
            var _this = this;
            this.$location = $location;
            this.$scope = $scope;
            this.$q = $q;
            this.$http = $http;
            this.SalesComService = SalesComService;
            this.InitializeService = InitializeService;
            this.$uibModal = $uibModal;
            this.HeaderFactory = HeaderFactory;
            debugger;
            var mainController = angular.module('mtisalescom', ['ngRoute', 'ngResource', 'ui.bootstrap', 'ngCookies', 'mgcrea.ngStrap.datepicker',
                'mgcrea.ngStrap.timepicker', 'mgcrea.ngStrap.tooltip', 'mgcrea.ngStrap.helpers.parseOptions', 'mgcrea.ngStrap.select', 'textAngular',
                'xeditable', 'angular-sortable-view', 'angularjs-dropdown-multiselect', 'ngAnimate', 'ngTouch',
                'ui.mask', 'ui.utils', 'ng-currency', 'ng.deviceDetector', 'mc.resizer', '$uibModalInstance']);
            debugger;
            $scope.initialized = false;
            var paginationOptions = {
                sort: null
            };
            $scope.pages =
                {
                    ManageNewPlan: 'App/views/newplan.html',
                    ManageEmployee: 'App/views/employee.html',
                    ManageViewAccess: 'App/views/view.html'
                };
            $scope.page = $scope.pages.ManageNewPlan;
            $scope.IsCurrentPage = function (page) {
                return ($scope.page === page);
            };
            $scope.GoToManageNewPlan = function () {
                debugger;
                HeaderFactory.SetTitle("Sales Commission Access");
                $scope.page = $scope.pages.ManageNewPlan;
            };
            $scope.GoToManageEmployee = function () {
                debugger;
                HeaderFactory.SetTitle("Sales Commission Access");
                $scope.page = $scope.pages.ManageEmployee;
            };
            $scope.GoToManageViewAccess = function () {
                debugger;
                HeaderFactory.SetTitle("View Access");
                $scope.page = $scope.pages.ManageViewAccess;
            };
            $scope.ShowAYSDialog = function (title, message, callback) {
                var modalInstance = $uibModal.open({
                    templateUrl: 'App/views/m/AreYouSure.html',
                    controller: 'AYSDialogController',
                    backdrop: 'static',
                    scope: $scope,
                    resolve: {
                        title: function () {
                            return title;
                        },
                        message: function () {
                            return message;
                        }
                    }
                });
                modalInstance.result.then(function (success) {
                    if (success) {
                        callback();
                    }
                }, function () {
                });
            };
            $scope.ViewRolesItems = function () {
                debugger;
                $scope.GetRoleslist();
                $scope.page = $scope.pages.ManageNewPlan;
            };
            $scope.GetRoleslist = function () {
                debugger;
                _this.SalesComService.GetRoles().then(function (data) {
                    $scope.Roleslists = data;
                });
            };
        }
        SalesCommissionControl.$inject = ['$location', '$rootScope', '$scope', '$q', '$http', '$routeParams', 'mtisalescom.SalesComService', 'InitializeService', '$uibModal', 'HeaderFactoryClass', '$compile', 'deviceDetector', '$templateCache', '$timeout', '$route', '$interval', 'ActionRequest'];
        return SalesCommissionControl;
    }());
    var AYSDialogController = (function () {
        function AYSDialogController($scope, $uibModalInstance, title, message) {
            this.$scope = $scope;
            this.$uibModalInstance = $uibModalInstance;
            $scope.title = title;
            $scope.message = message;
            $scope.ok = function () {
                $uibModalInstance.close(true);
            };
            $scope.cancel = function () {
                $uibModalInstance.close(false);
            };
        }
        AYSDialogController.$inject = ['$scope', '$uibModalInstance', 'title', 'message'];
        return AYSDialogController;
    }());
    angular
        .module('mtisalescom')
        .config(Config)
        .controller('MenuNavigationControl', MenuNavigationControl)
        .controller('AYSDialogController', AYSDialogController)
        .controller('HeaderController', HeaderController)
        .controller('SalesCommissionControl', SalesCommissionControl);
})(mtisalescom || (mtisalescom = {}));
//# sourceMappingURL=E:/Projects/SalesCommission/Source/Application/SalesCommission//Scripts/TSBuild/controller/salescommission.controller.js.map