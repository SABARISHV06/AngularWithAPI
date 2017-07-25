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
    //Define Sales Commission Header Factory Class                                                           //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    export class HeaderFactoryClass {
        version: string = ''
        title: string = 'Sales Commission';

        constructor() {
        }

        GetTitle(): string {

            if (!this.version) {
                this.version = GetAppVersion();
            }

            return this.title + ' - ' + this.version;
        }

        SetTitle(value: string): void {
            this.title = value;
        }
    }

    export function HeaderFactory() {
        return new HeaderFactoryClass();
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Define Sales Commission Interfaces Initialize Service                                                //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    export interface IInitializeService {
        GetRoles(): ng.IPromise<any>;
        GetAllUsers(): ng.IPromise<any>;
        CheckUserLogin(): ng.IPromise<EmployeeComponant>;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Define Sales Commission Initialize Service                                                            //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    export class InitializeService implements IInitializeService {

        static $inject = ['$http', '$q', 'mtisalescom.SessionHandler'];

        constructor(private $http: ng.IHttpService, private $q: ng.IQService, private SessionHandler: mtisalescom.SessionHandler) {
            //debugger;
        }

        GetRoles(): ng.IPromise<any> {
            
            var deferred = this.$q.defer();
            var parentScope = this;
            parentScope.$http({
                url: preurl('Security/GetRoles'),
                method: "Get",
                withCredentials: true
            }).success(function (data: any) {
                deferred.resolve(data);
            })
                .error(function () {
                    deferred.reject();
                });
            return deferred.promise;
        };

        GetAllUsers(): ng.IPromise<any> {
            
            var deferred = this.$q.defer();
            var parentScope = this;
            parentScope.$http({
                url: preurl('Security/GetAllUsers'),
                method: "Get",
                withCredentials: true
            }).success(function (data: any) {
                deferred.resolve(data);
            })
                .error(function () {
                    deferred.reject();
                });
            return deferred.promise;
        };

        CheckUserLogin(): ng.IPromise<EmployeeComponant> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/CheckUser'),
                method: "Post",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): EmployeeComponant => {
                return <EmployeeComponant>response.data;
            });
        }       
    }

    InitializeFactory.$inject = ['$http', '$q', 'mtisalescom.SessionHandler'];

    function InitializeFactory($http: ng.IHttpService, $q: ng.IQService, SessionHandler: mtisalescom.SessionHandler): IInitializeService {
        return new InitializeService($http, $q, SessionHandler);
    }

    angular
        .module('mtisalescom')
        .factory('HeaderFactoryClass', HeaderFactory)
        .factory('InitializeService', InitializeFactory)

}

