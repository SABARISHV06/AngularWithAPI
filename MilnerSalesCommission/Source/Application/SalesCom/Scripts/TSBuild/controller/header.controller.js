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
    angular
        .module('mtisalescom')
        .controller('HeaderController', HeaderController);
})(mtisalescom || (mtisalescom = {}));
//# sourceMappingURL=E:/Projects/SalesCommission/Source/Application/SalesCommission//Scripts/TSBuild/controller/header.controller.js.map