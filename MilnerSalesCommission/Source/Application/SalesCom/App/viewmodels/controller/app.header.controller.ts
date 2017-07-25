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
    //Define Sales Commission Header Controller                                                            //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    class HeaderController {

        static $inject = ['$scope', 'HeaderFactoryClass'];
        constructor(private $scope: ISalesCommissionScope, private HeaderFactory: HeaderFactoryClass) {
            $scope.Header = HeaderFactory;
            $scope.Header.SetTitle("Sales Commission");
        }
    }

    angular
        .module('mtisalescom')
        .controller('HeaderController', HeaderController)
}