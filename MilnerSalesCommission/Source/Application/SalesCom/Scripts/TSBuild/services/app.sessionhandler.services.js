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
    //Define Sales Commission Services for Session Handler Service                                        //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    var SessionHandler = (function () {
        function SessionHandler($http) {
            this.$http = $http;
            this.AccessTimeout = new Date().getTime();
            this.AccessTimeout = 15;
            this.GetSessionTimeout();
            this.count = 0;
        }
        SessionHandler.prototype.GetSessionTimeout = function () {
            var parentScope = this;
            parentScope.$http({
                url: preurl('Home/GetSessionTimeout'),
                method: "GET",
                withCredentials: true
            }).then(function (response) {
                parentScope.AccessTimeout = response.data;
            });
        };
        SessionHandler.prototype.KeepAlive = function (self) {
            var currentTime = new Date().getTime();
            var timeDiff = currentTime - self.lastAccess;
            var minutesElapsed = Math.floor(timeDiff / 60000);
            if (minutesElapsed >= self.AccessTimeout) {
                self.$http({
                    url: preurl('Home/KeepAlive'),
                    method: "GET",
                    withCredentials: true
                }).success(function (r) {
                    self.lastAccess = new Date().getTime();
                    self.count++;
                    setTimeout(self.KeepAlive, self.AccessTimeout * 60000, self);
                }).error(function (xhr, status, err) {
                    setTimeout(self.KeepAlive, self.AccessTimeout * 60000, self);
                });
            }
            else {
                setTimeout(self.KeepAlive, self.AccessTimeout * 60000, self);
            }
        };
        SessionHandler.prototype.SetLastAccess = function (value) {
            this.lastAccess = value;
        };
        SessionHandler.$inject = ['$http'];
        return SessionHandler;
    }());
    mtisalescom.SessionHandler = SessionHandler;
    sessionActivityfactory.$inject = ['$http'];
    function sessionActivityfactory($http) {
        return new SessionHandler($http);
    }
    mtisalescom.sessionActivityfactory = sessionActivityfactory;
    angular
        .module('mtisalescom')
        .factory('mtisalescom.SessionHandler', sessionActivityfactory);
})(mtisalescom || (mtisalescom = {}));
//# sourceMappingURL=E:/SVN/MilnerSalesCommission/Source/Application/SalesCom//Scripts/TSBuild/services/app.sessionhandler.services.js.map