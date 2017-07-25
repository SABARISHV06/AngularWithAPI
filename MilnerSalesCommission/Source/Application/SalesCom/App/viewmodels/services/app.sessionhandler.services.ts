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
    //Define Sales Commission Interfaces for Session Handler Service                                        //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    export interface ISessionHandlerService {
        lastAccess: any;
        parentScope: any;
        AccessTimeout: any;
        count: number;
        GetSessionTimeout(): void;
        KeepAlive(self: any): void;
        SetLastAccess(value: any): void;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Define Sales Commission Services for Session Handler Service                                        //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    export class SessionHandler implements ISessionHandlerService {
        lastAccess: any;
        AccessTimeout: any;
        static $inject = ['$http'];
        count: number;
        parentScope: any;

        constructor(private $http: ng.IHttpService) {
            this.AccessTimeout = new Date().getTime();
            this.AccessTimeout = 15;
            this.GetSessionTimeout();
            this.count = 0;
        }

        GetSessionTimeout(): void {
            var parentScope = this;
            parentScope.$http({
                url: preurl('Home/GetSessionTimeout'),
                method: "GET",
                withCredentials: true
            }).then((response: any): any => {
                parentScope.AccessTimeout = response.data;
            })
        }

        KeepAlive(self): void {
            var currentTime = new Date().getTime();
            var timeDiff = currentTime - self.lastAccess;
            var minutesElapsed = Math.floor(timeDiff / 60000);

            if (minutesElapsed >= self.AccessTimeout) {
                self.$http({
                    url: preurl('Home/KeepAlive'),
                    method: "GET",
                    withCredentials: true
                }).success((r): void => {
                    self.lastAccess = new Date().getTime();
                    self.count++;
                    setTimeout(self.KeepAlive, self.AccessTimeout * 60000, self);
                }).error(function (xhr, status, err) {
                    setTimeout(self.KeepAlive, self.AccessTimeout * 60000, self);
                });
            } else {
                setTimeout(self.KeepAlive, self.AccessTimeout * 60000, self);
            }
        }

        SetLastAccess(value): void {
            this.lastAccess = value;
        }


    }

    sessionActivityfactory.$inject = ['$http'];

    export function sessionActivityfactory($http: ng.IHttpService): SessionHandler {
        return new SessionHandler($http);
    }

    angular
        .module('mtisalescom')
        .factory('mtisalescom.SessionHandler', sessionActivityfactory)

}