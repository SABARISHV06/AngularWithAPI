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
        .factory('mtisalescom.SalesComService', SalesCommissionFactory);
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
//# sourceMappingURL=E:/Projects/SalesCommission/Source/Application/SalesCommission//Scripts/TSBuild/services/salescom.services.js.map