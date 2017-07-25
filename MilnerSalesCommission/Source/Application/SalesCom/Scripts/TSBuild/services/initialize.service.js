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
    var HeaderFactoryClass = (function () {
        function HeaderFactoryClass() {
            this.version = '';
            this.title = 'Sales Commission';
        }
        HeaderFactoryClass.prototype.GetTitle = function () {
            if (!this.version) {
                this.version = GetAppVersion();
            }
            return this.title + ' - ' + this.version;
        };
        HeaderFactoryClass.prototype.SetTitle = function (value) {
            this.title = value;
        };
        return HeaderFactoryClass;
    }());
    mtisalescom.HeaderFactoryClass = HeaderFactoryClass;
    function HeaderFactory() {
        return new HeaderFactoryClass();
    }
    mtisalescom.HeaderFactory = HeaderFactory;
    var InitializeService = (function () {
        function InitializeService($http, $q, SessionHandler) {
            this.$http = $http;
            this.$q = $q;
            this.SessionHandler = SessionHandler;
            debugger;
        }
        InitializeService.prototype.GetRoles = function () {
            var deferred = this.$q.defer();
            var parentScope = this;
            parentScope.$http({
                url: preurl('Home/GetRoles'),
                method: "Get",
                withCredentials: true
            }).success(function (data) {
                deferred.resolve(data);
            })
                .error(function () {
                deferred.reject();
            });
            return deferred.promise;
        };
        ;
        InitializeService.$inject = ['$http', '$q', 'mtisalescom.SessionHandler'];
        return InitializeService;
    }());
    mtisalescom.InitializeService = InitializeService;
    InitializeFactory.$inject = ['$http', '$q', 'mtisalescom.SessionHandler'];
    function InitializeFactory($http, $q, SessionHandler) {
        return new InitializeService($http, $q, SessionHandler);
    }
    angular
        .module('mtisalescom')
        .factory('HeaderFactoryClass', HeaderFactory)
        .factory('InitializeService', InitializeFactory);
})(mtisalescom || (mtisalescom = {}));
//# sourceMappingURL=E:/Projects/SalesCommission/Source/Application/SalesCommission//Scripts/TSBuild/services/initialize.service.js.map