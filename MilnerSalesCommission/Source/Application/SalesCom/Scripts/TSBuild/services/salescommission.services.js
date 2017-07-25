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
    (function (TAdministrator) {
        TAdministrator[TAdministrator["Yes"] = 1] = "Yes";
        TAdministrator[TAdministrator["No"] = 2] = "No";
    })(mtisalescom.TAdministrator || (mtisalescom.TAdministrator = {}));
    var TAdministrator = mtisalescom.TAdministrator;
    var KeyValuePair = (function () {
        function KeyValuePair() {
        }
        return KeyValuePair;
    }());
    mtisalescom.KeyValuePair = KeyValuePair;
    var roles = (function () {
        function roles() {
            this.rows = [];
        }
        return roles;
    }());
    mtisalescom.roles = roles;
    var DBdRow = (function () {
        function DBdRow() {
            this.columns = [];
        }
        return DBdRow;
    }());
    mtisalescom.DBdRow = DBdRow;
    var DBdColumn = (function () {
        function DBdColumn() {
            this.widgets = [];
        }
        return DBdColumn;
    }());
    mtisalescom.DBdColumn = DBdColumn;
    var DBdWidget = (function () {
        function DBdWidget() {
        }
        return DBdWidget;
    }());
    mtisalescom.DBdWidget = DBdWidget;
    var HeaderFactoryClass = (function () {
        function HeaderFactoryClass() {
            this.version = '';
            this.title = 'Sales Commission';
        }
        HeaderFactoryClass.prototype.GetTitle = function () {
            if (!this.version) {
                this.version = GetProductVersion();
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
    var SalesComService = (function () {
        function SalesComService($q, $http, SessionHandler) {
            this.$q = $q;
            this.$http = $http;
            this.SessionHandler = SessionHandler;
            var parentScope = this;
        }
        SalesComService.prototype.GetRoles = function () {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Home/GetRoles'),
                method: "Get",
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.$inject = ['$q', '$http', 'mtisalescom.SessionHandler'];
        return SalesComService;
    }());
    mtisalescom.SalesComService = SalesComService;
    SalesCommissionFactory.$inject = ['$q', '$http', 'mtisalescom.SessionHandler'];
    function SalesCommissionFactory($q, $http, SessionHandler) {
        return new SalesComService($q, $http, SessionHandler);
    }
    angular
        .module('mtisalescom')
        .factory('mtisalescom.SalesComService', SalesCommissionFactory)
        .factory('HeaderFactoryClass', HeaderFactory)
        .factory('InitializeService', InitializeFactory);
})(mtisalescom || (mtisalescom = {}));
function ajaxError(xmlreq, tstat, e) {
    if (xmlreq.responseText) {
        try {
            var err = JSON.parse(xmlreq.responseText);
            alert("A problem occurred while performing your request. The technical details follow. \n\n" + err.Message + "\n\n" + err.MessageDetail);
        }
        catch (ex) {
            alert("A problem occurred while performing your request. The technical reason follows.\n\n" + xmlreq.responseText);
        }
    }
    else if (xmlreq.ExceptionMessage) {
        alert("A problem occurred while performing your request. Make sure the server is running.  The technical code was " + xmlreq.ExceptionMessage.toString() + ".");
    }
    else if (xmlreq.Message) {
        alert("A problem occurred while performing your request. Make sure the server is running.  The technical code was " + xmlreq.Message.toString() + ".");
    }
    else {
        alert("A problem occurred while performing your request. Make sure the server is running.  The technical code was " + xmlreq + ".");
    }
}
//# sourceMappingURL=E:/Projects/SalesCommission/Source/Application/SalesCommission//Scripts/TSBuild/services/salescommission.services.js.map