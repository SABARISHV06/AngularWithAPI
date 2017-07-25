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
            $routeProvider.
                when('/', {
                controller: SalesCommissionControl, templateUrl: 'App/views/master.html',
                controllerAs: 'salescom',
            });
            $routeProvider.when("/payplandetails", {
                controller: SalesCommissionControl, templateUrl: 'App/views/payplandetails.html',
                controllerAs: 'salescom'
            });
            $routeProvider.when("/employeedetails", {
                controller: SalesCommissionControl, templateUrl: 'App/views/employeedetails.html',
                controllerAs: 'salescom'
            });
            $routeProvider.when("/commissionslipdetails", {
                controller: SalesCommissionControl, templateUrl: 'App/views/commissionslipdetails.html',
                controllerAs: 'salescom'
            });
            $routeProvider.when("/salesrepreports", {
                controller: SalesCommissionControl, templateUrl: 'App/views/salesrepreports.html',
                controllerAs: 'salescom'
            });
            $routeProvider.when("/paylocityglreports", {
                controller: SalesCommissionControl, templateUrl: 'App/views/paylocityglreports.html',
                controllerAs: 'salescom'
            });
            $routeProvider.when("/branchdetails", {
                controller: SalesCommissionControl, templateUrl: 'App/views/branchdetails.html',
                controllerAs: 'salescom'
            });
            $routeProvider.when("/saletypedetails", {
                controller: SalesCommissionControl, templateUrl: 'App/views/saletypedetails.html',
                controllerAs: 'salescom'
            });
            $routeProvider.when("/payrollconfigurationdetails", {
                controller: SalesCommissionControl, templateUrl: 'App/views/payrollconfigurationdetails.html',
                controllerAs: 'salescom'
            });
            $routeProvider.otherwise({ redirectTo: '/' });
            delete $httpProvider.defaults.headers.common['X-Requested-With'];
        }
        Config.$inject = ['$routeProvider', '$httpProvider'];
        return Config;
    }());
    mtisalescom.Config = Config;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Define Sales Commission Controller                                                              //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    var SalesCommissionControl = (function () {
        function SalesCommissionControl($location, $rootScope, $scope, $q, $http, $routeParams, SalesComService, InitializeService, $uibModal, HeaderFactory, $compile, deviceDetector, $templateCache, $timeout, $route, $interval, $cookies, $filter) {
            var _this = this;
            this.$location = $location;
            this.$scope = $scope;
            this.$q = $q;
            this.$http = $http;
            this.SalesComService = SalesComService;
            this.InitializeService = InitializeService;
            this.$uibModal = $uibModal;
            this.HeaderFactory = HeaderFactory;
            var mainController = angular.module('mtisalescom', ['ngRoute', 'ngResource', 'ui.bootstrap', 'ngCookies', 'mgcrea.ngStrap.datepicker',
                'mgcrea.ngStrap.timepicker', 'mgcrea.ngStrap.tooltip', 'mgcrea.ngStrap.helpers.parseOptions', 'mgcrea.ngStrap.select', 'textAngular',
                'angular-sortable-view', 'angularUtils.directives.dirPagination', 'ngAnimate', 'ngTouch',
                'ui.mask', 'ui.utils', 'ng-currency', 'ng.deviceDetector', '$uibModalInstance', 'isteven-multi-select']);
            $scope.initialized = false;
            $scope.pageSize = 10;
            var paginationOptions = {
                sort: null
            };
            $scope.MonthsArr = $.map($(Array(12)), function (val, i) { return i + 1; });
            $scope.Months = [];
            angular.forEach($scope.MonthsArr, function (val) {
                val = (val < 10) ? ("0" + val) : val;
                $scope.Months.push(val);
            });
            $scope.DaysArr = $.map($(Array(31)), function (val, i) { return i + 1; });
            $scope.Days = [];
            angular.forEach($scope.DaysArr, function (val) {
                val = (val < 10) ? ("0" + val) : val;
                $scope.Days.push(val);
            });
            $scope.MailUrl = "http://192.168.6.111/SalesWeb";
            $scope.pages =
                {
                    AddPayPlan: 'App/views/addpayplan.html',
                    EditPayPlan: 'App/views/editpayplan.html',
                    PayPlanDetails: 'App/views/payplandetails.html',
                    ViewPayPlan: 'App/views/viewpayplan.html',
                    AddEmployee: 'App/views/addemployee.html',
                    EditEmployee: 'App/views/editemployee.html',
                    EmployeeDetails: 'App/views/employeedetails.html',
                    ViewEmployee: 'App/views/viewemployee.html',
                    AddCommissionslip: 'App/views/addcommissionslip.html',
                    EditCommissionslip: 'App/views/editcommissionslip.html',
                    CommissionslipDetails: 'App/views/commissionslipdetails.html',
                    ViewCommissionslip: 'App/views/viewcommissionslip.html',
                    ResubmitCommissionslip: 'App/views/resubmitcommissionslip.html',
                    SalesRepReports: 'App/views/salesrepreports.html',
                    PaylocityGlReports: 'App/views/paylocityglreports.html',
                    BranchDetails: 'App/views/branchdetails.html',
                    SaleTypeDetails: 'App/views/saletypedetails.html',
                    PayrollConfigurationDetails: 'App/views/payrollconfigurationdetails.html',
                    ViewPayrollConfiguration: 'App/views/viewpayrollconfiguration.html',
                    EditPayrollConfiguration: 'App/views/editpayrollconfiguration.html',
                    AddPayrollConfiguration: 'App/views/addpayrollconfiguration.html'
                };
            $scope.IsCurrentPage = function (page) {
                return ($scope.page === page);
            };
            $scope.Sort = function (keyname) {
                $scope.SortKey = keyname; //set the sortKey to the param passed
                $scope.Order = !$scope.Order; //if true make it false and vice versa
            };
            $scope.onSelect = function ($item, $model, $label) {
                $scope.Commissionslip.SplitSalePerson = $label;
                $scope.Commissionslip.SplitSalePersonID = $item.EmployeeID;
            };
            $scope.splitchange = function () {
                $scope.Commissionslip.SplitSalePerson = "";
                $scope.Commissionslip.SplitSalePersonID = "";
            };
            $scope.onTipleadSelect = function ($item, $model, $label, index) {
                $scope.TipLeadRow[index].TipLeadName = $label;
                $scope.TipLeadRow[index].TipLeadEmpID = $item.EmployeeID;
                $scope.TipLeadRow[index].TipLeadID = $item.UID;
            };
            $scope.Tipleadchange = function (index) {
                $scope.TipLeadRow[index].TipLeadName = "";
                $scope.TipLeadRow[index].TipLeadEmpID = "";
                $scope.TipLeadRow[index].TipLeadID = 0;
            };
            //$scope.Saletype = [{ value: 1, name: "Cash" }, { value: 2, name: "Lease" }, { value: 3, name: "Rent" }, { value: 4, name: "Lease renewal" }];
            $scope.currentDt = new Date(); //Get Current Date
            $scope.mm = $scope.currentDt.getMonth() + 1;
            $scope.mm = ($scope.mm < 10) ? '0' + $scope.mm : $scope.mm;
            $scope.dd = $scope.currentDt.getDate();
            $scope.yyyy = $scope.currentDt.getFullYear();
            $scope.currentDtTime = $filter('date')(new Date(), 'yyyy-MM-dd HH:mm:ss Z');
            $scope.currentDt = $scope.mm + '/' + $scope.dd + '/' + $scope.yyyy;
            $scope.currentMnth = $scope.mm + '/' + $scope.yyyy;
            $scope.RolesType = mtisalescom.RoleTypes;
            $scope.SlipsType = mtisalescom.SlipTypes;
            $scope.Productline = [{ value: 1, name: "Copy" }, { value: 2, name: "Milner Software" }, { value: 3, name: "Third Party Software" }, { value: 4, name: "IT Services" }, { value: 5, name: "Other Third Party Products" }];
            $scope.BimonthsOptions = ["1-12", "13+"];
            $scope.TenureOptions = ["13-24", "25-36", "37+"];
            $scope.NewCustomerRow = [];
            $scope.AddNewCustomerRow = function () {
                $scope.NewCustomerRow.push(new mtisalescom.TGPCustomerInfo);
            };
            $scope.RemoveNewCustomerRow = function (index) {
                $scope.ShowAYSDialog("Are You Sure?", "Do you really want to delete the line Item?", function () { $scope.NewCustomerRow.splice(index, 1); });
            };
            $scope.ExistingCustomerRow = [];
            $scope.AddExistingCustomerRow = function () {
                $scope.ExistingCustomerRow.push(new mtisalescom.TGPCustomerInfo);
            };
            $scope.RemoveExistingCustomerRow = function (index) {
                $scope.ShowAYSDialog("Are You Sure?", "Do you really want to delete the line Item?", function () {
                    $scope.ExistingCustomerRow.splice(index, 1);
                });
            };
            $scope.Payrollconfigrow = [];
            $scope.AddNewPayrollConfigRow = function () {
                $scope.PlusPayroll = new mtisalescom.Payroll();
                $scope.PlusPayroll.CreatedBy = $scope.LoggedinEmployee.UID;
                $scope.Payrollconfigrow.push($scope.PlusPayroll);
            };
            $scope.EditNewPayrollConfigRow = function () {
                $scope.PlusPayroll = new mtisalescom.Payroll();
                $scope.PlusPayroll.CreatedBy = $scope.LoggedinEmployee.UID;
                $scope.EditPayroll.push($scope.PlusPayroll);
            };
            $scope.RemoveNewPayrollConfigRow = function (index) {
                $scope.ShowAYSDialog("Are You Sure?", "Do you really want to delete the line Item?", function () {
                    $scope.Payrollconfigrow.splice(index, 1);
                });
            };
            $scope.RemoveEditNewPayrollConfigRow = function (index, row) {
                $scope.ShowAYSDialog("Are You Sure?", "Do you really want to delete the line Item?", function () {
                    $scope.EditPayroll.splice(index, 1);
                    row.IsActive = false;
                    row.ModifiedBy = $scope.LoggedinEmployee.UID;
                    $scope.NewPayrollconfigrow.push(row);
                });
            };
            $scope.BimonthsRow = [];
            $scope.AddBimonthsRow = function (index) {
                if ($scope.BimonthsRow[index].EntryPointB == undefined || String($scope.BimonthsRow[index].EntryPointB) == "") {
                    $scope.Requiredfield = function (index) {
                        return true;
                    };
                }
                else {
                    $scope.BimonthsRow.push(new mtisalescom.BIMonthlyBonusInfo);
                    $scope.Requiredfield = function (index) {
                        return false;
                    };
                }
            };
            $scope.RemoveBimonthsRow = function (index) {
                $scope.ShowAYSDialog("Are You Sure?", "Do you really want to delete the line Item?", function () {
                    $scope.BimonthsRow.splice(index, 1);
                    $scope.Requiredfield = function (index) {
                        return false;
                    };
                });
            };
            $scope.TenureRow = [];
            $scope.AddTenureRow = function () {
                $scope.TenureRow.push(new mtisalescom.QuarterlyTenureBonus);
            };
            $scope.RemoveTenureRow = function (index) {
                $scope.ShowAYSDialog("Are You Sure?", "Do you really want to delete the line Item?", function () {
                    $scope.TenureRow.splice(index, 1);
                });
            };
            $scope.BimonthlyShow = function () {
                if ($scope.BimonthsRow == [] || $scope.BimonthsRow.length == 0) {
                    $scope.BimonthsRow.push(new mtisalescom.BIMonthlyBonusInfo);
                }
            };
            $scope.BimonthlyHide = function () {
                $scope.Plandetail.BMQuotaBonus = true;
                $scope.ShowAYSDialog("Are You Sure?", "All the values given will be erased, you sure to proceed?", function () {
                    $scope.Plandetail.BMQuotaBonus = false;
                    $scope.BimonthsRow = [];
                });
            };
            $scope.TenureShow = function () {
                if ($scope.TenureRow == [] || $scope.TenureRow.length == 0) {
                    $scope.TenureRow.push(new mtisalescom.QuarterlyTenureBonus);
                }
            };
            $scope.TenureHide = function () {
                $scope.Plandetail.TenureBonus = true;
                $scope.ShowAYSDialog("Are You Sure?", "All the values given will be erased, you sure to proceed?", function () {
                    $scope.Plandetail.TenureBonus = false;
                    $scope.TenureRow = [];
                });
            };
            $scope.GoToAddPayPlan = function () {
                HeaderFactory.SetTitle("Add PayPlan");
                $scope.SelectedTab = 'payplan';
                $scope.page = $scope.pages.AddPayPlan;
                $scope.NewCustomerRow = [];
                $scope.ExistingCustomerRow = [];
                $scope.BimonthsRow = [];
                $scope.TenureRow = [];
                $scope.Plandetail = new mtisalescom.Plancomponent;
                $scope.NewCustomerRow.push(new mtisalescom.TGPCustomerInfo);
                $scope.ExistingCustomerRow.push(new mtisalescom.TGPCustomerInfo);
                $scope.GetSaletypelist();
                $scope.Plandetail.BasisType = 1; // Default value of Basis Type is Doller Volume
            };
            $scope.Entrycheck = function (index) {
                $scope.Entrypointscheck = [];
                $.each($scope.BimonthsRow, function (key, value) {
                    var A = parseInt(value.EntryPointA.toString());
                    var B = parseInt(value.EntryPointB.toString());
                    if (A > B || A == B) {
                        $scope.Entrypointscheck[key] = true;
                    }
                    else {
                        $scope.Entrypointscheck[key] = false;
                    }
                });
            };
            $scope.NumberOfDays = [];
            $scope.UpdateNumberOfDays = function (index) {
                var isLeapYear = function () {
                    var year = $scope.TempPayroll.Year || 0;
                    return ((year % 400 === 0 || year % 100 !== 0) && (year % 4 === 0)) ? 1 : 0;
                };
                var selectedMonth = $scope.Payrollconfigrow[index].Period || 0;
                selectedMonth = parseInt(selectedMonth.toString());
                $scope.NumberOfDays[index] = 31 - ((selectedMonth === 2) ? (3 - isLeapYear()) : ((selectedMonth - 1) % 7 % 2));
            };
            $scope.EditNumberOfDays = [];
            $scope.UpdateEditNumberOfDays = function (index, cval) {
                var isLeapYear = function () {
                    var year = $scope.TempPayroll.Year || 0;
                    return ((year % 400 === 0 || year % 100 !== 0) && (year % 4 === 0)) ? 1 : 0;
                };
                var selectedMonth = cval;
                selectedMonth = parseInt(selectedMonth.toString());
                $scope.EditNumberOfDays[index] = 31 - ((selectedMonth === 2) ? (3 - isLeapYear()) : ((selectedMonth - 1) % 7 % 2));
            };
            $scope.SetDateTo = function (index) {
                var currPeriod = $scope.Payrollconfigrow[index].Period;
                var isexist = false;
                var CurrDay;
                var CurrDayNum;
                $scope.Entrypointscheck = [];
                $.each($scope.Payrollconfigrow, function (key, value) {
                    if (value.Period == currPeriod && key != index) {
                        isexist = true;
                        CurrDayNum = parseInt($scope.Payrollconfigrow[index - 1].Day.toString()) + 1;
                        if (CurrDayNum.toString().length < 2) {
                            CurrDay = "0" + CurrDayNum;
                        }
                        else {
                            CurrDay = CurrDayNum.toString();
                        }
                    }
                });
                if (isexist == true) {
                    $scope.Payrollconfigrow[index].DateFrom = $scope.Payrollconfigrow[index].Period + '/' + CurrDay;
                }
                else {
                    $scope.Payrollconfigrow[index].DateFrom = $scope.Payrollconfigrow[index].Period + '/01';
                }
                if ($scope.Payrollconfigrow[index].Day)
                    $scope.Payrollconfigrow[index].DateTo = $scope.Payrollconfigrow[index].Period + '/' + $scope.Payrollconfigrow[index].Day;
            };
            $scope.SetEditDateTo = function (index) {
                var currPeriod = $scope.EditPayroll[index].Period;
                var isexist = false;
                var CurrDay;
                var CurrDayNum;
                $scope.Entrypointscheck = [];
                $.each($scope.EditPayroll, function (key, value) {
                    if (value.Period == currPeriod && key != index) {
                        isexist = true;
                        CurrDayNum = parseInt($scope.EditPayroll[index - 1].Day.toString()) + 1;
                        if (CurrDayNum.toString().length < 2) {
                            CurrDay = "0" + CurrDayNum;
                        }
                        else {
                            CurrDay = CurrDayNum.toString();
                        }
                    }
                });
                if (isexist == true) {
                    $scope.EditPayroll[index].DateFrom = $scope.EditPayroll[index].Period + '/' + CurrDay;
                }
                else {
                    $scope.EditPayroll[index].DateFrom = $scope.EditPayroll[index].Period + '/01';
                }
                if ($scope.EditPayroll[index].Day)
                    $scope.EditPayroll[index].DateTo = $scope.EditPayroll[index].Period + '/' + $scope.EditPayroll[index].Day;
            };
            $scope.AddPlan = function () {
                $scope.CustomerInfo = [];
                $scope.Plandetail.CreatedBy = $scope.LoggedinEmployee.UID;
                $scope.Plandetail.IsActive = true;
                $.each($scope.NewCustomerRow, function (key, value) {
                    value.CustomerType = mtisalescom.CustomerTypes.NewCustomer;
                });
                $.each($scope.ExistingCustomerRow, function (key, value) {
                    value.CustomerType = mtisalescom.CustomerTypes.ExistingCustomer;
                });
                $scope.CustomerInfo = $scope.NewCustomerRow.concat($scope.ExistingCustomerRow);
                $scope.Plandetail.TGPcustomerlist = $scope.CustomerInfo;
                $scope.Plandetail.Bimonthlylist = $scope.BimonthsRow;
                $scope.Plandetail.TenureBonuslist = $scope.TenureRow;
                $.each($scope.Plandetail.TenureBonuslist, function (key, value) {
                    value.EntryPointB = 0;
                });
                _this.SalesComService.AddPlan($scope.Plandetail).then(function (data) {
                    if (data != 0) {
                        $.bootstrapGrowl("Plan details added successfully.", { type: 'success' });
                        $scope.GoToPayPlanDetails();
                    }
                });
            };
            $scope.GoToEditPayPlan = function (plan) {
                HeaderFactory.SetTitle("Edit PayPlan");
                $scope.page = $scope.pages.EditPayPlan;
                $scope.SelectedTab = 'payplan';
                $scope.Plandetail = new mtisalescom.Plancomponent;
                $scope.NewCustomerRow = [];
                $scope.ExistingCustomerRow = [];
                $scope.BimonthsRow = [];
                $scope.TenureRow = [];
                _this.SalesComService.GetPlanbyID(plan).then(function (data) {
                    debugger;
                    $scope.Plandetail = data;
                    $scope.BimonthsRow = $scope.Plandetail.Bimonthlylist;
                    $scope.TenureRow = $scope.Plandetail.TenureBonuslist;
                    $scope.NewCustomerRow = $scope.Plandetail.TGPcustomerlist.filter(function (Tgp) {
                        return (Tgp.CustomerType == mtisalescom.CustomerTypes.NewCustomer);
                    });
                    $scope.ExistingCustomerRow = $scope.Plandetail.TGPcustomerlist.filter(function (Tgp) {
                        return (Tgp.CustomerType == mtisalescom.CustomerTypes.ExistingCustomer);
                    });
                    debugger;
                    if ($scope.NewCustomerRow.length == 0) {
                        $scope.NewCustomerRow.push(new mtisalescom.TGPCustomerInfo);
                    }
                    if ($scope.ExistingCustomerRow.length == 0) {
                        $scope.ExistingCustomerRow.push(new mtisalescom.TGPCustomerInfo);
                    }
                });
                $scope.GetSaletypelist();
            };
            $scope.EditPlan = function () {
                $scope.CustomerInfo = [];
                $scope.Plandetail.ModifiedBy = $scope.LoggedinEmployee.UID;
                $scope.Plandetail.IsActive = true;
                $.each($scope.NewCustomerRow, function (key, value) {
                    value.CustomerType = mtisalescom.CustomerTypes.NewCustomer;
                });
                $.each($scope.ExistingCustomerRow, function (key, value) {
                    value.CustomerType = mtisalescom.CustomerTypes.ExistingCustomer;
                });
                $scope.CustomerInfo = $scope.NewCustomerRow.concat($scope.ExistingCustomerRow);
                $scope.Plandetail.TGPcustomerlist = $scope.CustomerInfo;
                $scope.Plandetail.Bimonthlylist = $scope.BimonthsRow;
                $scope.Plandetail.TenureBonuslist = $scope.TenureRow;
                $.each($scope.Plandetail.TenureBonuslist, function (key, value) {
                    value.EntryPointB = 0;
                });
                _this.SalesComService.EditPlan($scope.Plandetail).then(function (data) {
                    if (data == true) {
                        $.bootstrapGrowl("Plan details updated successfully.", { type: 'success' });
                        $scope.GoToPayPlanDetails();
                    }
                });
            };
            //To display Pay plan details in grid view
            $scope.GoToPayPlanDetails = function () {
                HeaderFactory.SetTitle("Pay Plan Details");
                $scope.page = $scope.pages.PayPlanDetails;
                $scope.SelectedTab = 'payplan';
                $scope.pageSize = 10;
                _this.SalesComService.GetPlans().then(function (data) {
                    $scope.Planlists = data;
                    $.each($scope.Planlists, function (key, value) {
                        value.BasisType = mtisalescom.Basistype[value.BasisType];
                        value.BMQuotaBonus = mtisalescom.Booleanvalues[value.BMQuotaBonus == true ? 1 : 0];
                        value.TenureBonus = mtisalescom.Booleanvalues[value.TenureBonus == true ? 1 : 0];
                    });
                });
            };
            $scope.GoToViewPayPlan = function (plan) {
                HeaderFactory.SetTitle("View PayPlan");
                $scope.page = $scope.pages.ViewPayPlan;
                $scope.SelectedTab = 'payplan';
                _this.SalesComService.GetPlanbyID(plan).then(function (data) {
                    debugger;
                    $scope.Plandetail = data;
                    $scope.Plandetail.BasisType = mtisalescom.Basistype[$scope.Plandetail.BasisType];
                    $scope.Plandetail.BMQuotaBonus = mtisalescom.Booleanvalues[$scope.Plandetail.BMQuotaBonus == true ? 1 : 0];
                    $scope.Plandetail.TenureBonus = mtisalescom.Booleanvalues[$scope.Plandetail.TenureBonus == true ? 1 : 0];
                    $scope.Plandetail.SMEligible = mtisalescom.Booleanvalues[$scope.Plandetail.SMEligible == true ? 1 : 0];
                });
            };
            $scope.PlanActivateDeactivateConfirmation = function (plan) {
                if (plan.IsActive == true) {
                    $scope.ActivationMessage = "Deactivated";
                    $scope.ShowAYSDialog("Are You Sure?", "A Pay Plan will be mapped with many employees, are you sure to deactivate?", function () { $scope.GoToActivateDeactivatePayPlan(plan); });
                }
                else {
                    $scope.ActivationMessage = "Activated";
                    $scope.ShowAYSDialog("Are You Sure?", "Do you want to activate the Payplan?", function () { $scope.GoToActivateDeactivatePayPlan(plan); });
                }
            };
            $scope.NewEmployeeCancelConfirmation = function () {
                $scope.ActivationMessage = "Cancel";
                $scope.ShowAYSDialog("Cancel Employee Detail", "Are you sure, you do not want to save the Employee?", function () { $scope.GoToEmployeeDetails(); });
            };
            $scope.NewPayPlanCancelConfirmation = function () {
                $scope.ActivationMessage = "Cancel";
                $scope.ShowAYSDialog("Cancel PayPlan Detail", "Are you sure, you do not want to save the payplan?", function () { $scope.GoToPayPlanDetails(); });
            };
            $scope.GoToActivateDeactivatePayPlan = function (plan) {
                HeaderFactory.SetTitle("Pay Plan Details");
                $scope.page = $scope.pages.PayPlanDetails;
                $scope.SelectedTab = 'payplan';
                plan.IsActive = (plan.IsActive == true ? false : true);
                plan.ModifiedBy = $scope.LoggedinEmployee.UID;
                _this.SalesComService.ActivateDeactivatePlan(plan).then(function (data) {
                    if (data == true) {
                        $.bootstrapGrowl("Plan " + $scope.ActivationMessage + " successfully.", { type: 'success' });
                        $scope.GoToPayPlanDetails();
                    }
                });
            };
            $scope.Typeonchange = function () {
                $scope.Employeedetail.PrimaryBranch = "";
                $scope.Employeedetail.SecondaryBranch = [];
                $scope.Employeedetail.ApproveMgr = null;
                $scope.Employeedetail.ReportMgr = null;
                $scope.Employeedetail.PayPlanID = "";
                $scope.Employeedetail.BPDraw = false;
                $scope.Employeedetail.BPSalary = false;
            };
            $scope.BPSalaryonclick = function () {
                $scope.Employeedetail.MonthAmount = null;
            };
            $scope.BPDrawonclick = function () {
                $scope.Employeedetail.DDAmount = null;
                $scope.Employeedetail.TypeofDraw = 1;
                $scope.Employeedetail.DrawTerm = null;
            };
            $scope.Drawtypeonclick = function () {
                $scope.Employeedetail.DRPercentage = null;
                $scope.Employeedetail.DrawTerm = null;
                // $scope.Employeedetail.DDPeriod = new Date;
            };
            $scope.GoToEmployeeDetails = function () {
                HeaderFactory.SetTitle("Employee Details");
                $scope.page = $scope.pages.EmployeeDetails;
                $scope.SelectedTab = 'employee';
                $scope.pageSize = 10;
                var roleID = $scope.LoggedinEmployee.RoleID;
                _this.SalesComService.GetAllEmployeeDetailsbyRoleID(roleID).then(function (data) {
                    $scope.Employees = data;
                });
                _this.SalesComService.GetDeActivatePlanEmployees().then(function (data) {
                    $scope.Employeedetail = data;
                });
            };
            $scope.GoToAddEmployee = function () {
                $scope.Employeedetail = new mtisalescom.EmployeeComponant;
                HeaderFactory.SetTitle("Add Employee");
                $scope.page = $scope.pages.AddEmployee;
                $scope.SelectedTab = 'employee';
                $scope.Employeedetail.DateofHire = $scope.Employeedetail.DateInPosition = $scope.currentDt;
                $scope.Employeedetail.DDPeriod = $scope.currentMnth;
                $scope.Employeedetail.BPDraw = false;
                $scope.Employeedetail.BPSalary = false;
                $scope.Employeedetail.TypeofDraw = 1;
                $scope.GetRoleslist();
                $scope.GetBranchlist();
                $scope.GetAllManagerlist();
                $scope.GetPlanlist();
                _this.InitializeService.GetAllUsers().then(function (data) {
                    $scope.Userslist = data;
                });
            };
            $scope.CreateEmployeeConfirmation = function () {
                $scope.ShowAYSDialog("Are You Sure?", "Account Name mapped to an employee cannot be changed, are you sure to save the details?", function () {
                    $scope.CreateEmployee();
                });
            };
            $scope.CreateEmployee = function () {
                var acctname = $scope.Employeedetail.AccountName;
                if ($scope.Userslist.indexOf(acctname) == -1) {
                    $.bootstrapGrowl('Please select account name from LDAP user list');
                    return false;
                }
                var dateofhire = new Date($scope.Employeedetail.DateofHire);
                var dateinposition = new Date($scope.Employeedetail.DateInPosition);
                if (dateofhire > dateinposition) {
                    $.bootstrapGrowl("Date of hire should be greater or equal to Date in position", { type: 'danger' });
                    return false;
                }
                var branchcheck = 0;
                $scope.Employeedetail.SecondaryBranch = [];
                angular.forEach($scope.Employeedetail.SecondaryBranchList, function (value, index) {
                    $scope.Employeedetail.SecondaryBranch.push(value.BranchID);
                    if ($scope.Employeedetail.PrimaryBranch == String(value.BranchID)) {
                        branchcheck = 1;
                    }
                });
                if (branchcheck == 1) {
                    $.bootstrapGrowl("Both Primary and Secondary branches cannot be same", { type: 'danger' });
                    return false;
                }
                $scope.Employeedetail.CreatedBy = $scope.LoggedinEmployee.UID;
                $scope.Employeedetail.IsActive = true;
                if ($scope.Employeedetail.DDPeriod == undefined) {
                    $scope.Employeedetail.DDPeriod = $scope.currentDt;
                }
                _this.SalesComService.AddEmployee($scope.Employeedetail).then(function (data) {
                    if (data == 2) {
                        $.bootstrapGrowl("Please map different Account Name to create employee.", { type: 'danger' });
                        return;
                    }
                    else if (data == 3) {
                        $.bootstrapGrowl("Please enter different EmployeeID to create employee.", { type: 'danger' });
                        return;
                    }
                    else if (data != 0) {
                        $.bootstrapGrowl("Employee details added successfully.", { type: 'success' });
                        $scope.GoToEmployeeDetails();
                    }
                    else {
                        $.bootstrapGrowl("Unfortunately employee details cannot be inserted.Please retry.", { type: 'danger' });
                    }
                });
            };
            $scope.GoToEditEmployee = function (emp) {
                HeaderFactory.SetTitle("Edit Employee");
                $scope.page = $scope.pages.EditEmployee;
                $scope.SelectedTab = 'employee';
                $scope.GetRoleslist();
                //$scope.GetBranchlist();
                $scope.Brancheslists = [];
                $scope.GetAllManagerlist();
                $scope.GetPlanlist();
                $scope.Employeedetail = new mtisalescom.EmployeeComponant;
                _this.SalesComService.GetEmployeebyUID(emp).then(function (data) {
                    $scope.Employeedetail = data;
                    $scope.Employeedetail.DateofHire = $scope.Employeedetail.DateofHire.split("-");
                    $scope.Employeedetail.DateofHire = $scope.Employeedetail.DateofHire[1] + '/' + $scope.Employeedetail.DateofHire[2].substring(0, 2) + '/' + $scope.Employeedetail.DateofHire[0];
                    $scope.Employeedetail.DateInPosition = $scope.Employeedetail.DateInPosition.split("-");
                    $scope.Employeedetail.DateInPosition = $scope.Employeedetail.DateInPosition[1] + '/' + $scope.Employeedetail.DateInPosition[2].substring(0, 2) + '/' + $scope.Employeedetail.DateInPosition[0];
                    $scope.Employeedetail.DDPeriod = $scope.Employeedetail.DDPeriod.split("-");
                    $scope.Employeedetail.DDPeriod = $scope.Employeedetail.DDPeriod[1] + '/' + $scope.Employeedetail.DDPeriod[0];
                    $scope.Employeedetail.RoleID = String($scope.Employeedetail.RoleID);
                    $scope.Employeedetail.ReportMgr = String($scope.Employeedetail.ReportMgr);
                    $scope.Employeedetail.ApproveMgr = String($scope.Employeedetail.ApproveMgr);
                    var splitedBranch;
                    splitedBranch = $scope.Employeedetail.SecondaryBranch[0].split(',');
                    var secBranchString = '';
                    _this.SalesComService.GetBranches().then(function (data) {
                        $scope.Brancheslists = data;
                        $scope.SecondaryBrancheslists = $scope.Brancheslists;
                        $scope.SecondaryBrancheslists = $scope.SecondaryBrancheslists.filter(function (branch) {
                            return (branch.IsActive == true);
                        });
                        angular.forEach($scope.SecondaryBrancheslists, function (value, index) {
                            for (var i = 0; i < splitedBranch.length - 1; i++) {
                                if (splitedBranch[i] == value.BranchID) {
                                    secBranchString += value.BranchName + ',';
                                    value.ticked = true;
                                }
                            }
                        });
                    });
                });
            };
            $scope.EditEmployee = function () {
                var dateofhire = new Date($scope.Employeedetail.DateofHire);
                var dateinposition = new Date($scope.Employeedetail.DateInPosition);
                if (dateofhire > dateinposition) {
                    $.bootstrapGrowl('Date of hire should be greater or equal to Date in position');
                    return false;
                }
                $scope.Employeedetail.SecondaryBranch = [];
                var branchcheck = 0;
                angular.forEach($scope.Employeedetail.SecondaryBranchList, function (value, index) {
                    $scope.Employeedetail.SecondaryBranch.push(value.BranchID);
                    if ($scope.Employeedetail.PrimaryBranch == String(value.BranchID)) {
                        branchcheck = 1;
                    }
                });
                if (branchcheck == 1) {
                    $.bootstrapGrowl("Both Primary and Secondary branches cannot be same", { type: 'danger' });
                    return false;
                }
                $scope.Employeedetail.ModifiedBy = $scope.LoggedinEmployee.UID;
                if ($scope.Employeedetail.DDPeriod == undefined) {
                    $scope.Employeedetail.DDPeriod = new Date;
                }
                _this.SalesComService.EditEmployee($scope.Employeedetail).then(function (data) {
                    if (data == true) {
                        $.bootstrapGrowl("Emoloyee details updated successfully.", { type: 'success' });
                        $scope.GoToEmployeeDetails();
                    }
                });
            };
            $scope.GoToViewEmployee = function (emp) {
                //$scope.GetBranchlist();
                $scope.Brancheslists = [];
                HeaderFactory.SetTitle("View Employee");
                $scope.page = $scope.pages.ViewEmployee;
                $scope.SelectedTab = 'employee';
                _this.SalesComService.GetEmployeebyUID(emp).then(function (data) {
                    debugger;
                    $scope.Employeedetail = data;
                    $scope.Employeedetail.BPDraw = mtisalescom.Booleanvalues[$scope.Employeedetail.BPDraw == true ? 1 : 0];
                    $scope.Employeedetail.BPSalary = mtisalescom.Booleanvalues[$scope.Employeedetail.BPSalary == true ? 1 : 0];
                    $scope.Employeedetail.TypeofDraw = $scope.Employeedetail.TypeofDraw == 1 ? "Guaranteed" : "Recoverable";
                    var splitedBranch;
                    splitedBranch = $scope.Employeedetail.SecondaryBranch[0].split(',');
                    var secBranch;
                    secBranch = [];
                    var secBranchString = "";
                    _this.SalesComService.GetBranches().then(function (data) {
                        $scope.Brancheslists = data;
                        angular.forEach($scope.Brancheslists, function (value, index) {
                            for (var i = 0; i < splitedBranch.length - 1; i++) {
                                if (splitedBranch[i] == value.BranchID) {
                                    secBranchString += value.BranchName + ',';
                                }
                            }
                        });
                        secBranchString = secBranchString.substring(0, secBranchString.length - 1);
                        $scope.Employeedetail.SecondaryBranchName = secBranchString;
                    });
                });
            };
            $scope.ActivateDeactivateConfirmation = function (emp) {
                if (emp.IsActive == true) {
                    $scope.ShowAYSDialog("Are You Sure?", "All the employee details will be deactivated and employee will not be allowed to access the system, are you sure to de activate?", function () { $scope.GoToActivateDeactivateEmployee(emp); });
                }
                else {
                    $scope.ShowAYSDialog("Are You Sure?", "Do you want to activate the Employee?", function () { $scope.GoToActivateDeactivateEmployee(emp); });
                }
            };
            $scope.GoToActivateDeactivateEmployee = function (emp) {
                HeaderFactory.SetTitle("Employee Details");
                $scope.page = $scope.pages.EmployeeDetails;
                $scope.SelectedTab = 'employee';
                emp.IsActive = (emp.IsActive == true ? false : true);
                emp.ModifiedBy = $scope.LoggedinEmployee.UID;
                _this.SalesComService.ActivateDeactivateEmployee(emp).then(function (data) {
                    $scope.GoToEmployeeDetails();
                });
            };
            $scope.GoToCommissionslipDetailsConfirmation = function () {
                $scope.ShowAYSDialog("Are You Sure?", "Are you sure, you want to cancel?", function () {
                    $scope.GoToCommissionslipDetails();
                });
            };
            $scope.GoToCommissionslipDetails = function () {
                HeaderFactory.SetTitle("Sales Rep - Commission Slip Details");
                $scope.page = $scope.pages.CommissionslipDetails;
                $scope.SelectedTab = 'commission';
                $scope.IsGMDeactivated = false;
                $scope.ISPlanDeactivated = false;
                _this.SalesComService.GetAllCommissionsbyUID($scope.LoggedinEmployee).then(function (data) {
                    $scope.Commissionslips = data;
                    $scope.Commissionslips[0].CreatedBy;
                    $.each($scope.Commissionslips, function (key, value) {
                        value.Status = value.ProcesByPayroll == 1 ? "Payroll Processed" : mtisalescom.CommissionStatus[value.Status];
                    });
                });
                if ($scope.LoggedinEmployee.RoleID == 5 || $scope.LoggedinEmployee.RoleID == 6) {
                    _this.SalesComService.GetDeactiveGMandPlanID($scope.LoggedinEmployee).then(function (data) {
                        var Deactivatedlist = data;
                        $scope.ISPlanDeactivated = Deactivatedlist[0] == 0 ? false : true;
                        $scope.IsGMDeactivated = Deactivatedlist[1] == 0 ? false : true;
                    });
                }
            };
            $scope.TipLeadRow = [];
            $scope.AddTipLeadRow = function () {
                $scope.TipLeadRow.push(new mtisalescom.TipLeadSlip);
            };
            $scope.RemoveTipLeadRow = function (index) {
                $scope.ShowAYSDialog("Are You Sure?", "All the Tip lead details associated will be deleted", function () {
                    $scope.TipLeadRow.splice(index, 1);
                    $scope.TotalCommissions();
                });
            };
            var Parentnode = this;
            $scope.onSalespersonselect = function ($item, $model, $label) {
                $scope.Employeedetail = new mtisalescom.EmployeeComponant;
                $scope.Commissionslip.SalesPerson = $label;
                $scope.Employeedetail.UID = $item.UID;
                $scope.Commissionslip.CreatedBy = $item.UID;
                $scope.Brancheslists = [];
                Parentnode.SalesComService.GetEmployeebyUID($scope.Employeedetail).then(function (data) {
                    $scope.Employeedetail = data;
                    $scope.Commissionslip.SalesPerson = $scope.Employeedetail.FirstName + ' ' + $scope.Employeedetail.LastName;
                    $scope.Commissionslip.BranchID = $scope.Employeedetail.PrimaryBranch;
                    Parentnode.SalesComService.GetBranches().then(function (data) {
                        $scope.Brancheslists = data;
                        var splitedBranch;
                        splitedBranch = [];
                        $scope.EmployeeBranch = [];
                        splitedBranch = $scope.Employeedetail.SecondaryBranch[0].split(',');
                        splitedBranch.push($scope.Employeedetail.PrimaryBranch);
                        $.each($scope.Brancheslists, function (key, value) {
                            for (var i = 0; i <= splitedBranch.length - 1; i++) {
                                if (splitedBranch[i] == value.BranchID) {
                                    if (value.IsActive == true) {
                                        $scope.EmployeeBranch.push(value);
                                    }
                                }
                            }
                        });
                    });
                });
            };
            $scope.GoToAddCommissionslip = function () {
                HeaderFactory.SetTitle("Sales Rep - Add Commission Slip");
                $scope.page = $scope.pages.AddCommissionslip;
                $scope.SelectedSalesperson = undefined;
                $scope.Salespersonslist = [];
                $scope.SRSalespersons = [];
                $scope.TipLeadlist = [];
                $scope.SelectedTab = 'commission';
                $scope.Commissionslip = new mtisalescom.CommissionComponent;
                // $scope.Commissionslip.SalesPerson = $scope.LoggedinEmployee.FirstName + ' ' + $scope.LoggedinEmployee.LastName;
                $scope.Commissionslip.EntryDate = $scope.Commissionslip.DateofSale = $scope.currentDt;
                //$scope.Commissionslip.AccountPeriod = $scope.currentMnth;
                $scope.Commissionslip.TipLeadAmount = 0.00;
                $scope.Commissionslip.BaseCommission = 0.00;
                $scope.Commissionslip.LeaseCommission = 0.00;
                $scope.Commissionslip.ServiceCommission = 0.00;
                $scope.Commissionslip.TravelCommission = 0.00;
                $scope.Commissionslip.CashCommission = 0.00;
                $scope.Commissionslip.SpecialCommission = 0.00;
                $scope.Commissionslip.TotalCEarned = 0.00;
                $scope.Commissionslip.CustomerType = mtisalescom.CustomerTypes.NewCustomer;
                $scope.SaleTypeList = [];
                // $scope.Commissionslip.BranchID = $scope.LoggedinEmployee.PrimaryBranch;
                $scope.Brancheslists = [];
                if ($scope.LoggedinEmployee.RoleID == mtisalescom.RoleTypes.SalesRep) {
                    $scope.Commissionslip.SalesPerson = $scope.LoggedinEmployee.FirstName + ' ' + $scope.LoggedinEmployee.LastName;
                    $scope.Commissionslip.BranchID = $scope.LoggedinEmployee.PrimaryBranch;
                    _this.SalesComService.GetBranches().then(function (data) {
                        $scope.Brancheslists = data;
                        var splitedBranch;
                        splitedBranch = [];
                        $scope.EmployeeBranch = [];
                        splitedBranch = $scope.LoggedinEmployee.SecondaryBranch[0].split(',');
                        splitedBranch.push($scope.LoggedinEmployee.PrimaryBranch);
                        $.each($scope.Brancheslists, function (key, value) {
                            for (var i = 0; i <= splitedBranch.length - 1; i++) {
                                if (splitedBranch[i] == value.BranchID) {
                                    if (value.IsActive == true) {
                                        $scope.EmployeeBranch.push(value);
                                    }
                                }
                            }
                        });
                    });
                }
                $scope.TipLeadRow = [];
                $scope.TipLeadRow.push(new mtisalescom.TipLeadSlip);
                var roleID = mtisalescom.RoleTypes.PayRoll;
                _this.SalesComService.GetAllEmployeeDetailsbyRoleID(roleID).then(function (data) {
                    $scope.Salespersonslist = data;
                    $scope.SRSalespersons = $scope.Salespersonslist.filter(function (person) {
                        return (person.RoleID == mtisalescom.RoleTypes.SalesRep && person.IsActive == true);
                    });
                    $scope.TipLeadlist = $scope.Salespersonslist.filter(function (person) {
                        return ((person.RoleID == mtisalescom.RoleTypes.SalesRep || person.RoleID == mtisalescom.RoleTypes.NonSalesEmployee) && person.IsActive == true);
                    });
                });
                $.each($scope.SRSalespersons, function (key, value) {
                    $scope.splitnamelist.push(value.LastName);
                });
                $scope.GetSaletypelist();
            };
            $scope.createddatechange = function () {
                var temp = $scope.Commissionslip.CreatedOn.toString();
                var currentdate = new Date(temp);
                currentdate.getDate();
                _this.SalesComService.GetAccountingMonth(currentdate).then(function (data) {
                    $scope.AccountPayroll = data;
                    $scope.Commissionslip.AccountPeriod = $scope.AccountPayroll.Month;
                    $scope.Commissionslip.AccountPeriodID = $scope.AccountPayroll.ID;
                });
            };
            $scope.TotalTipslip = function () {
                $.each($scope.TipLeadRow, function (key, value) {
                    var tipamount = parseFloat(value.TipLeadAmount != undefined && value.TipLeadAmount != "" ? value.TipLeadAmount : 0);
                    var positive = parseFloat(value.PositiveAdjustments != undefined && value.PositiveAdjustments != "" ? value.PositiveAdjustments : 0);
                    var negative = parseFloat(value.NegativeAdjustments != undefined && value.NegativeAdjustments != "" ? value.NegativeAdjustments : 0);
                    var companycontribution = parseFloat(value.CompanyContribution != undefined && value.CompanyContribution != "" ? value.CompanyContribution : 0);
                    value.TotalCEarned = (tipamount + companycontribution + positive) - negative;
                    value.TotalCEarned = parseFloat(value.TotalCEarned).toFixed(2);
                });
            };
            $scope.TotalCommissions = function () {
                var tipleadamount = 0.00, positiveadj = 0.00, negativeadj = 0.00;
                $.each($scope.TipLeadRow, function (key, value) {
                    var tipamount = parseFloat(value.TipLeadAmount != undefined && value.TipLeadAmount != "" ? value.TipLeadAmount : 0);
                    var positive = parseFloat(value.PositiveAdjustments != undefined && value.PositiveAdjustments != "" ? value.PositiveAdjustments : 0);
                    var negative = parseFloat(value.NegativeAdjustments != undefined && value.NegativeAdjustments != "" ? value.NegativeAdjustments : 0);
                    var companycontribution = parseFloat(value.CompanyContribution != undefined && value.CompanyContribution != "" ? value.CompanyContribution : 0);
                    tipleadamount += tipamount;
                    positiveadj += positive;
                    negativeadj += negative;
                    value.TotalCEarned = (tipamount + companycontribution + positive) - negative;
                    value.TotalCEarned = parseFloat(value.TotalCEarned).toFixed(2);
                });
                var base = parseFloat($scope.Commissionslip.BaseCommission != undefined && $scope.Commissionslip.BaseCommission != "" ? $scope.Commissionslip.BaseCommission : 0);
                var lease = parseFloat($scope.Commissionslip.LeaseCommission != undefined && $scope.Commissionslip.LeaseCommission != "" ? $scope.Commissionslip.LeaseCommission : 0);
                var service = parseFloat($scope.Commissionslip.ServiceCommission != undefined && $scope.Commissionslip.ServiceCommission != "" ? $scope.Commissionslip.ServiceCommission : 0);
                var travel = parseFloat($scope.Commissionslip.TravelCommission != undefined && $scope.Commissionslip.TravelCommission != "" ? $scope.Commissionslip.TravelCommission : 0);
                var special = parseFloat($scope.Commissionslip.SpecialCommission != undefined && $scope.Commissionslip.SpecialCommission != "" ? $scope.Commissionslip.SpecialCommission : 0);
                var cash = parseFloat($scope.Commissionslip.CashCommission != undefined && $scope.Commissionslip.CashCommission != "" ? $scope.Commissionslip.CashCommission : 0);
                $scope.Commissionslip.TotalCEarned = (base + lease + service + travel + special + negativeadj + cash - (tipleadamount + positiveadj));
                $scope.Commissionslip.TipLeadAmount = parseFloat(tipleadamount).toFixed(2);
                $scope.Commissionslip.PositiveAdjustments = parseFloat(positiveadj).toFixed(2);
                $scope.Commissionslip.NegativeAdjustments = parseFloat(negativeadj).toFixed(2);
                $scope.Commissionslip.TotalCEarned = parseFloat($scope.Commissionslip.TotalCEarned).toFixed(3);
            };
            $scope.AddCommission = function (status) {
                if ($scope.Commissionslip.SplitSalePerson != null && $scope.Commissionslip.SplitSalePersonID == undefined) {
                    $.bootstrapGrowl("Please select Sales person name from registered user list.", { type: 'danger' });
                    return false;
                }
                var dateofsale = new Date($scope.Commissionslip.DateofSale);
                var entrydate = new Date($scope.Commissionslip.EntryDate);
                if (dateofsale > entrydate) {
                    $.bootstrapGrowl("Date of entry should be greater or equal to Date of sale", { type: 'danger' });
                    return false;
                }
                $scope.Commissionslip.Status = status;
                if ($scope.LoggedinEmployee.RoleID == mtisalescom.RoleTypes.SalesRep) {
                    $scope.Commissionslip.CreatedBy = $scope.LoggedinEmployee.UID;
                }
                if ($scope.Commissionslip.CreatedBy == undefined) {
                    $.bootstrapGrowl("Please select Sales Person", { type: 'danger' });
                    return false;
                }
                $scope.Commissionslip.IsActive = true;
                $scope.Commissionslip.SlipType = mtisalescom.SlipTypes.CommissionSlip;
                var error = 0;
                $.each($scope.TipLeadRow, function (key, value) {
                    if ((value.TipLeadID == undefined || value.TipLeadID == 0) && value.TipLeadAmount != 0 && value.TipLeadAmount != undefined) {
                        error = 1;
                    }
                    else if ((value.TipLeadAmount == undefined || value.TipLeadAmount == 0) && value.TipLeadID != undefined && value.TipLeadID != 0) {
                        error = 2;
                    }
                    else if ((value.TipLeadID == undefined || value.TipLeadID == 0) && ((value.PositiveAdjustments != 0 && value.PositiveAdjustments != undefined) || (value.NegativeAdjustments != 0 && value.NegativeAdjustments != undefined))) {
                        error = 1;
                    }
                    else if ((value.TipLeadAmount == undefined || value.TipLeadAmount == 0) && ((value.PositiveAdjustments != 0 && value.PositiveAdjustments != undefined) || (value.NegativeAdjustments != 0 && value.NegativeAdjustments != undefined))) {
                        error = 2;
                    }
                });
                if (error == 1) {
                    $.bootstrapGrowl("Please select Tip Lead", { type: 'danger' });
                    return false;
                }
                else if (error == 2) {
                    $.bootstrapGrowl("Please enter Tip Lead Amount", { type: 'danger' });
                    return false;
                }
                $scope.Commissionslip.TipLeadSliplist = $scope.TipLeadRow.filter(function (Tiplead) {
                    return (Tiplead.TipLeadID != undefined && Tiplead.TipLeadID != 0);
                });
                $.each($scope.Commissionslip.TipLeadSliplist, function (key, value) {
                    value.SlipType = mtisalescom.SlipTypes.TipLeadSlip;
                });
                //$scope.Commissionslip.CreatedOn = $scope.currentDtTime;
                _this.SalesComService.AddCommission($scope.Commissionslip).then(function (data) {
                    if (data != -1) {
                        if (status == 1) {
                            $.bootstrapGrowl("Commission details saved successfully.", { type: 'success' });
                        }
                        else if (status == 2) {
                            $.bootstrapGrowl("Commission details submitted successfully.", { type: 'success' });
                            $scope.MailContent = new mtisalescom.EmailMessage;
                            var uid = $scope.LoggedinEmployee.ReportMgr != null && $scope.LoggedinEmployee.ReportMgr != 0 ? $scope.LoggedinEmployee.ReportMgr : $scope.LoggedinEmployee.ApproveMgr;
                            _this.SalesComService.CommissionCreatedNotification(uid, 0).then(function (data) {
                                $scope.MailContent.RecipientTo = $scope.LoggedinEmployee.Email + ',' + data;
                                $scope.MailContent.Subject = "New Commission Slip";
                                $scope.MailContent.Body = "A new commission slip created. Please click the link to view the <a href=\"" + $scope.MailUrl + "\"> New commission slip created.</a>";
                                $scope.SendMail();
                            });
                            var parentnode = _this;
                            $.each($scope.Commissionslip.TipLeadSliplist, function (key, value) {
                                var uid = value.TipLeadID;
                                parentnode.SalesComService.CommissionCreatedNotification(uid, status).then(function (data) {
                                    $scope.MailContent.RecipientTo = data;
                                    $scope.MailContent.Subject = "New Tip Lead Slip";
                                    $scope.MailContent.Body = "A new Tip Lead Slip created. Please click the link to <a href=\"" + $scope.MailUrl + "\"> view the New Tip Lead slip.</a>";
                                    $scope.SendMail();
                                });
                            });
                        }
                        if (status == 7) {
                            $.bootstrapGrowl("Commission slip accepted successfully.", { type: 'success' });
                        }
                        $scope.GoToCommissionslipDetails();
                    }
                });
            };
            $scope.GoToEditCommissionslip = function (commissionid) {
                HeaderFactory.SetTitle("Sales Rep - Edit Commission Slip");
                $scope.SelectedTab = 'commission';
                $scope.page = $scope.pages.EditCommissionslip;
                $scope.Brancheslists = [];
                $scope.Salespersonslist = [];
                $scope.SRSalespersons = [];
                $scope.TipLeadlist = [];
                $scope.Commissionslips = [];
                $scope.Commissionslip = new mtisalescom.CommissionComponent();
                $scope.Commissionslip.CustomerType = mtisalescom.CustomerTypes.NewCustomer;
                $scope.Commissionslip.TipLeadAmount = 0.00;
                $scope.Commissionslip.BaseCommission = 0.00;
                $scope.Commissionslip.LeaseCommission = 0.00;
                $scope.Commissionslip.ServiceCommission = 0.00;
                $scope.Commissionslip.TravelCommission = 0.00;
                $scope.Commissionslip.CashCommission = 0.00;
                $scope.Commissionslip.SpecialCommission = 0.00;
                $scope.TipLeadRow = [];
                var roleID = mtisalescom.RoleTypes.PayRoll;
                _this.SalesComService.GetCommissionbyID(commissionid).then(function (data) {
                    $scope.Commissionslips = data;
                    $scope.Commissionslip = $scope.Commissionslips[0];
                    $scope.Commissionslip.Comments = ""; // Empty for new comment
                    $scope.Commissionslip.EntryDate = $scope.Commissionslip.EntryDate.split("-");
                    $scope.Commissionslip.EntryDate = $scope.Commissionslip.EntryDate[1] + '/' + $scope.Commissionslip.EntryDate[2].substring(0, 2) + '/' + $scope.Commissionslip.EntryDate[0];
                    $scope.Commissionslip.DateofSale = $scope.Commissionslip.DateofSale.split("-");
                    $scope.Commissionslip.DateofSale = $scope.Commissionslip.DateofSale[1] + '/' + $scope.Commissionslip.DateofSale[2].substring(0, 2) + '/' + $scope.Commissionslip.DateofSale[0];
                    //$scope.Commissionslip.AccountPeriod = $scope.Commissionslip.AccountPeriod.split("-");
                    //$scope.Commissionslip.AccountPeriod = $scope.Commissionslip.AccountPeriod[1] + '/' + $scope.Commissionslip.AccountPeriod[0];
                    $scope.Commissionslip.BranchID = String($scope.Commissionslip.BranchID);
                    var splitedBranch;
                    splitedBranch = [];
                    $scope.EmployeeBranch = [];
                    $scope.Employeedetail = new mtisalescom.EmployeeComponant;
                    $scope.Employeedetail.UID = $scope.Commissionslip.CreatedBy;
                    _this.SalesComService.GetEmployeebyUID($scope.Employeedetail).then(function (data) {
                        $scope.Employeedetail = data;
                        splitedBranch = $scope.Employeedetail.SecondaryBranch[0].split(',');
                        splitedBranch.push($scope.Employeedetail.PrimaryBranch);
                        _this.SalesComService.GetBranches().then(function (data) {
                            $scope.Brancheslists = data;
                            $.each($scope.Brancheslists, function (key, value) {
                                for (var i = 0; i <= splitedBranch.length - 1; i++) {
                                    if (splitedBranch[i] == value.BranchID) {
                                        if (value.IsActive == true) {
                                            $scope.EmployeeBranch.push(value);
                                        }
                                    }
                                }
                            });
                        });
                    });
                    _this.SalesComService.GetAllEmployeeDetailsbyRoleID(roleID).then(function (data) {
                        $scope.Salespersonslist = data;
                        $scope.SRSalespersons = $scope.Salespersonslist.filter(function (person) {
                            return (person.RoleID == mtisalescom.RoleTypes.SalesRep);
                        });
                        $scope.TipLeadlist = $scope.Salespersonslist.filter(function (person) {
                            return (person.RoleID == mtisalescom.RoleTypes.SalesRep || person.RoleID == mtisalescom.RoleTypes.NonSalesEmployee);
                        });
                    });
                    $.each($scope.SRSalespersons, function (key, value) {
                        $scope.splitnamelist.push(value.LastName);
                    });
                    var Commissionlist;
                    $scope.Commissionslips = $scope.Commissionslips.filter(function (commission) {
                        return (commission.SlipType == mtisalescom.SlipTypes.TipLeadSlip);
                    });
                    var Tipslip;
                    if ($scope.Commissionslips.length > 0) {
                        $.each($scope.Commissionslips, function (key, value) {
                            Tipslip = new mtisalescom.TipLeadSlip;
                            Tipslip.ID = value.ID;
                            Tipslip.MainCommissionID = value.MainCommissionID;
                            Tipslip.TipLeadID = value.TipLeadID;
                            Tipslip.TipLeadEmpID = value.TipLeadEmpID;
                            Tipslip.TipLeadName = value.TipLeadName;
                            Tipslip.TipLeadAmount = value.TipLeadAmount;
                            Tipslip.PositiveAdjustments = value.PositiveAdjustments;
                            Tipslip.NegativeAdjustments = value.NegativeAdjustments;
                            Tipslip.CompanyContribution = value.CompanyContribution;
                            Tipslip.SlipType = value.SlipType;
                            Tipslip.TotalCEarned = value.TotalCEarned;
                            Tipslip.Status = value.Status;
                            Tipslip.ProcesByPayroll = value.ProcesByPayroll;
                            $scope.TipLeadRow.push(Tipslip);
                        });
                    }
                    else {
                        Tipslip = new mtisalescom.TipLeadSlip;
                        Tipslip.TipLeadAmount = 0.00;
                        Tipslip.PositiveAdjustments = 0.00;
                        Tipslip.NegativeAdjustments = 0.00;
                        Tipslip.CompanyContribution = 0.00;
                        Tipslip.TotalCEarned = 0.00;
                        $scope.TipLeadRow.push(Tipslip);
                    }
                    $scope.Brancheslists = [];
                    $scope.MainCommissionStatus = 0;
                    if ($scope.Commissionslip.SlipType == mtisalescom.SlipTypes.TipLeadSlip) {
                        var Commissionslips;
                        _this.SalesComService.GetCommissionbyID($scope.Commissionslip.MainCommissionID).then(function (data) {
                            Commissionslips = data;
                            if (Commissionslips[0].ProcesByPayroll == true) {
                                $scope.MainCommissionStatus = 1;
                            }
                            else if (Commissionslips[0].Status == 7) {
                                $scope.MainCommissionStatus = 2;
                            }
                        });
                    }
                });
                $scope.GetSaletypelist();
            };
            $scope.EditCommission = function (status) {
                var dateofsale = new Date($scope.Commissionslip.DateofSale);
                var entrydate = new Date($scope.Commissionslip.EntryDate);
                if (dateofsale > entrydate) {
                    $.bootstrapGrowl("Date of entry should be greater or equal to Date of sale", { type: 'danger' });
                    return false;
                }
                if ((status == 4 || status == 6) && $scope.Commissionslip.Comments == "") {
                    $.bootstrapGrowl("Please provide reason for rejection in comment.", { type: 'danger' });
                    return false;
                }
                var MailNotificationstatus = $scope.Commissionslip.Status;
                $scope.Commissionslip.Status = status == 8 ? 7 : status; //To change, Payroll accept & notify to Payroll accepted w.r.t FRS for display in dashboard.
                $scope.Commissionslip.ModifiedBy = $scope.LoggedinEmployee.UID;
                $scope.Commissionslip.CreatedOn = $scope.currentDtTime;
                $scope.Commissionslip.SlipType = mtisalescom.SlipTypes.CommissionSlip;
                var error = 0;
                $.each($scope.TipLeadRow, function (key, value) {
                    if ((value.TipLeadID == undefined || value.TipLeadID == 0) && value.TipLeadAmount != 0 && value.TipLeadAmount != undefined) {
                        error = 1;
                    }
                    else if ((value.TipLeadAmount == undefined || value.TipLeadAmount == 0) && value.TipLeadID != undefined && value.TipLeadID != 0) {
                        error = 2;
                    }
                    else if ((value.TipLeadID == undefined || value.TipLeadID == 0) && ((value.PositiveAdjustments != 0 && value.PositiveAdjustments != undefined) || (value.NegativeAdjustments != 0 && value.NegativeAdjustments != undefined))) {
                        error = 1;
                    }
                    else if ((value.TipLeadAmount == undefined || value.TipLeadAmount == 0) && ((value.PositiveAdjustments != 0 && value.PositiveAdjustments != undefined) || (value.NegativeAdjustments != 0 && value.NegativeAdjustments != undefined))) {
                        error = 2;
                    }
                });
                if (error == 1) {
                    $.bootstrapGrowl("Please select Tip Lead", { type: 'danger' });
                    return false;
                }
                else if (error == 2) {
                    $.bootstrapGrowl("Please enter Tip Lead Amount", { type: 'danger' });
                    return false;
                }
                $scope.Commissionslip.TipLeadSliplist = $scope.TipLeadRow.filter(function (Tiplead) {
                    return (Tiplead.TipLeadID != undefined && Tiplead.TipLeadID != 0);
                });
                var tipleadstatus = [];
                $.each($scope.Commissionslip.TipLeadSliplist, function (key, value) {
                    value.SlipType = mtisalescom.SlipTypes.TipLeadSlip;
                    tipleadstatus[key] = value.Status;
                    value.Status = ((value.Status != 7 && value.Status != 5 && !value.ProcesByPayroll) && ($scope.Commissionslip.Status == 2 || $scope.Commissionslip.Status == 1)) ? $scope.Commissionslip.Status : value.Status;
                });
                $scope.Commissionslip.CommentedBy = $scope.LoggedinEmployee.FirstName + ' ' + $scope.LoggedinEmployee.LastName;
                _this.SalesComService.EditCommission($scope.Commissionslip).then(function (data) {
                    if (data == true) {
                        switch (status) {
                            case 1:
                                $.bootstrapGrowl("Commission details saved successfully.", { type: 'success' });
                                break;
                            case 2:
                                $.bootstrapGrowl("Commission details updated successfully.", { type: 'success' });
                                $scope.MailContent = new mtisalescom.EmailMessage;
                                if (MailNotificationstatus == 1) {
                                    var uid = $scope.LoggedinEmployee.ReportMgr != null && $scope.LoggedinEmployee.ReportMgr > 0 ? $scope.LoggedinEmployee.ReportMgr : $scope.LoggedinEmployee.ApproveMgr;
                                    _this.SalesComService.CommissionCreatedNotification(uid, 0).then(function (data) {
                                        $scope.MailContent.RecipientTo = $scope.LoggedinEmployee.Email + ',' + data;
                                        $scope.MailContent.Subject = "New Commission Slip";
                                        $scope.MailContent.Body = "A new commission slip created. Please click the link to view the <a href=\"" + $scope.MailUrl + "\"> New commission slip created.</a>";
                                        $scope.SendMail();
                                    });
                                }
                                var parentnode = _this;
                                $.each($scope.Commissionslip.TipLeadSliplist, function (key, value) {
                                    if (value.ID == undefined || tipleadstatus[key] == 1) {
                                        var uid = value.TipLeadID;
                                        parentnode.SalesComService.CommissionCreatedNotification(uid, status).then(function (data) {
                                            $scope.MailContent.RecipientTo = data;
                                            $scope.MailContent.Subject = "New Tip Lead Slip";
                                            $scope.MailContent.Body = "A new Tip Lead Slip created. Please click the link to <a href=\"" + $scope.MailUrl + "\"> view the New Tip Lead slip.</a>";
                                            $scope.SendMail();
                                        });
                                    }
                                });
                                break;
                            case 3:
                                $.bootstrapGrowl("You have accepted the commission slip.", { type: 'success' });
                                $scope.MailContent = new mtisalescom.EmailMessage;
                                var uid = $scope.Commissionslip.CreatedBy;
                                _this.SalesComService.CommissionCreatedNotification(uid, status).then(function (data) {
                                    $scope.MailContent.RecipientTo = data;
                                    $scope.MailContent.Subject = "MSC-Commission Slip";
                                    $scope.MailContent.Body = "Commission slip has been accepted by SM. Please click the link to view the <a href=\"" + $scope.MailUrl + "\"> Accepted Commission slip.</a>";
                                    $scope.SendMail();
                                });
                                break;
                            case 4:
                                $.bootstrapGrowl("You have Rejected the commission slip.", { type: 'success' });
                                $scope.MailContent = new mtisalescom.EmailMessage;
                                var uid = $scope.Commissionslip.CreatedBy;
                                _this.SalesComService.CommissionCreatedNotification(uid, status).then(function (data) {
                                    $scope.MailContent.RecipientTo = data;
                                    $scope.MailContent.Subject = "MSC-Commission Slip";
                                    $scope.MailContent.Body = "Commission slip has been Rejected by SM. Please click the link to view the <a href=\"" + $scope.MailUrl + "\"> Rejected Commission slip.</a>";
                                    $scope.SendMail();
                                });
                                break;
                            case 5:
                                $.bootstrapGrowl("You have accepted the commission slip.", { type: 'success' });
                                $scope.MailContent = new mtisalescom.EmailMessage;
                                _this.SalesComService.CommissionCreatedNotification(0, status).then(function (data) {
                                    $scope.MailContent.RecipientTo = data;
                                    $scope.MailContent.Subject = "MSC-Commission Slip";
                                    $scope.MailContent.Body = "Commission slip has been accepted by GM. Please click the link to view the <a href=\"" + $scope.MailUrl + "\"> Accepted Commission slip.</a>";
                                    $scope.SendMail();
                                });
                                break;
                            case 6:
                                $.bootstrapGrowl("You have Rejected the commission slip.", { type: 'success' });
                                $scope.MailContent = new mtisalescom.EmailMessage;
                                _this.SalesComService.CommissionCreatedNotification($scope.Commissionslip.CreatedBy, status).then(function (data) {
                                    $scope.MailContent.RecipientTo = data;
                                    $scope.MailContent.Subject = "MSC-Commission Slip";
                                    $scope.MailContent.Body = "Commission slip has been Rejected by GM. Please click the link to view the <a href=\"" + $scope.MailUrl + "\"> Rejected Commission slip.</a>";
                                    $scope.SendMail();
                                });
                                break;
                            case 7:
                                $.bootstrapGrowl("You have accepted the commission slip.", { type: 'success' });
                                break;
                            case 8:
                                $.bootstrapGrowl("You have accepted the commission slip.", { type: 'success' });
                                $scope.MailContent = new mtisalescom.EmailMessage;
                                status = 7; /* "Changed to Payroll Accepted" = 7*/
                                _this.SalesComService.CommissionCreatedNotification($scope.Commissionslip.CreatedBy, status).then(function (data) {
                                    $scope.MailContent.RecipientTo = data;
                                    $scope.MailContent.Subject = "MSC-Commission Slip";
                                    $scope.MailContent.Body = "Commission slip has been accepted by Payroll. Please click the link to view the <a href=\"" + $scope.MailUrl + "\"> Accepted Commission slip.</a>";
                                    $scope.SendMail();
                                });
                                break;
                        }
                    }
                    else {
                        $.bootstrapGrowl("Unfortunately Commission details cannot be processed.Please retry.", { type: 'danger' });
                    }
                    $scope.GoToCommissionslipDetails();
                });
                return true;
            };
            $scope.EditTipLeadslip = function (status) {
                var dateofsale = new Date($scope.Commissionslip.DateofSale);
                var entrydate = new Date($scope.Commissionslip.EntryDate);
                if (dateofsale > entrydate) {
                    $.bootstrapGrowl("Date of entry should be greater or equal to Date of sale", { type: 'danger' });
                    return false;
                }
                $scope.Commissionslip.ModifiedBy = $scope.LoggedinEmployee.UID;
                $scope.Commissionslip.TipLeadSliplist = $scope.TipLeadRow;
                $.each($scope.Commissionslip.TipLeadSliplist, function (key, value) {
                    $scope.Commissionslip.TipLeadAmount = value.TipLeadAmount;
                    $scope.Commissionslip.PositiveAdjustments = value.PositiveAdjustments;
                    $scope.Commissionslip.NegativeAdjustments = value.NegativeAdjustments;
                    $scope.Commissionslip.CompanyContribution = value.CompanyContribution;
                    $scope.Commissionslip.TotalCEarned = value.TotalCEarned;
                    $scope.Commissionslip.Status = status;
                });
                if ($scope.Commissionslip.TotalCEarned < 0) {
                    $.bootstrapGrowl("Totalcommission cannot be negative", { type: 'danger' });
                    return false;
                }
                _this.SalesComService.EditTipLeadSlip($scope.Commissionslip).then(function (data) {
                    if (data == true) {
                        switch (status) {
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                            case 5:
                                $.bootstrapGrowl("You have accepted the Tip Lead slip.", { type: 'success' });
                                $scope.MailContent = new mtisalescom.EmailMessage;
                                _this.SalesComService.CommissionCreatedNotification(0, status).then(function (data) {
                                    $scope.MailContent.RecipientTo = data;
                                    $scope.MailContent.Subject = "MSC-Tip Lead Slip";
                                    $scope.MailContent.Body = " A Tip Lead Slip has been accepted by the GM. Please click the link to <a href=\"" + $scope.MailUrl + "\"> view the Tip Lead slip.</a>";
                                    $scope.SendMail();
                                });
                                break;
                            case 6:
                                break;
                            case 7:
                                $.bootstrapGrowl("You have accepted the Tip Lead slip.", { type: 'success' });
                                break;
                            case 8:
                                break;
                        }
                    }
                    else {
                        $.bootstrapGrowl("Unfortunately Tip Lead slip details cannot be processed.Please retry.", { type: 'danger' });
                    }
                    $scope.GoToCommissionslipDetails();
                });
                return true;
            };
            $scope.GoToResubmitCommissionslip = function (commissionid) {
                HeaderFactory.SetTitle("Sales Rep - Resubmit Commission Slip");
                $scope.SelectedTab = 'commission';
                $scope.page = $scope.pages.ResubmitCommissionslip;
                //$scope.GetBranchlist();
                $scope.Salespersonslist = [];
                $scope.SRSalespersons = [];
                $scope.TipLeadRow = [];
                $scope.TipLeadlist = [];
                $scope.Commissionslips = [];
                $scope.Commissionslip = new mtisalescom.CommissionComponent();
                $scope.Commissionslip.TipLeadAmount = 0.00;
                $scope.Commissionslip.BaseCommission = 0.00;
                $scope.Commissionslip.LeaseCommission = 0.00;
                $scope.Commissionslip.ServiceCommission = 0.00;
                $scope.Commissionslip.TravelCommission = 0.00;
                $scope.Commissionslip.CashCommission = 0.00;
                $scope.Commissionslip.SpecialCommission = 0.00;
                var roleID = mtisalescom.RoleTypes.PayRoll;
                _this.SalesComService.GetCommissionbyID(commissionid).then(function (data) {
                    $scope.Commissionslips = data;
                    $scope.Commissionslip = $scope.Commissionslips[0];
                    $scope.Commissionslip.Comments = ""; // Empty for new comment
                    $scope.Commissionslip.EntryDate = $scope.Commissionslip.EntryDate.split("-");
                    $scope.Commissionslip.EntryDate = $scope.Commissionslip.EntryDate[1] + '/' + $scope.Commissionslip.EntryDate[2].substring(0, 2) + '/' + $scope.Commissionslip.EntryDate[0];
                    $scope.Commissionslip.DateofSale = $scope.Commissionslip.DateofSale.split("-");
                    $scope.Commissionslip.DateofSale = $scope.Commissionslip.DateofSale[1] + '/' + $scope.Commissionslip.DateofSale[2].substring(0, 2) + '/' + $scope.Commissionslip.DateofSale[0];
                    //$scope.Commissionslip.AccountPeriod = $scope.Commissionslip.AccountPeriod.split("-");
                    //$scope.Commissionslip.AccountPeriod = $scope.Commissionslip.AccountPeriod[1] + '/' + $scope.Commissionslip.AccountPeriod[0];
                    $scope.Commissionslip.BranchID = String($scope.Commissionslip.BranchID);
                    _this.SalesComService.GetAllEmployeeDetailsbyRoleID(roleID).then(function (data) {
                        $scope.Salespersonslist = data;
                        $scope.SRSalespersons = $scope.Salespersonslist.filter(function (person) {
                            return (person.RoleID == mtisalescom.RoleTypes.SalesRep);
                        });
                        $scope.TipLeadlist = $scope.Salespersonslist.filter(function (person) {
                            return (person.RoleID == mtisalescom.RoleTypes.SalesRep || person.RoleID == mtisalescom.RoleTypes.NonSalesEmployee);
                        });
                    });
                    $.each($scope.SRSalespersons, function (key, value) {
                        $scope.splitnamelist.push(value.LastName);
                    });
                    $.each($scope.SRSalespersons, function (key, value) {
                        $scope.splitnamelist.push(value.LastName);
                    });
                    var Commissionlist;
                    $scope.Commissionslips = $scope.Commissionslips.filter(function (commission) {
                        return (commission.SlipType == mtisalescom.SlipTypes.TipLeadSlip);
                    });
                    var Tipslip;
                    if ($scope.Commissionslips.length > 0) {
                        $.each($scope.Commissionslips, function (key, value) {
                            Tipslip = new mtisalescom.TipLeadSlip;
                            Tipslip.ID = value.ID;
                            Tipslip.MainCommissionID = value.MainCommissionID;
                            Tipslip.TipLeadID = value.TipLeadID;
                            Tipslip.TipLeadEmpID = value.TipLeadEmpID;
                            Tipslip.TipLeadName = value.TipLeadName;
                            Tipslip.TipLeadAmount = value.TipLeadAmount;
                            Tipslip.PositiveAdjustments = value.PositiveAdjustments;
                            Tipslip.NegativeAdjustments = value.NegativeAdjustments;
                            Tipslip.CompanyContribution = value.CompanyContribution;
                            Tipslip.SlipType = value.SlipType;
                            Tipslip.TotalCEarned = value.TotalCEarned;
                            Tipslip.Status = value.Status;
                            Tipslip.ProcesByPayroll = value.ProcesByPayroll;
                            $scope.TipLeadRow.push(Tipslip);
                        });
                    }
                    else {
                        Tipslip = new mtisalescom.TipLeadSlip;
                        Tipslip.TipLeadAmount = 0.00;
                        Tipslip.PositiveAdjustments = 0.00;
                        Tipslip.NegativeAdjustments = 0.00;
                        Tipslip.CompanyContribution = 0.00;
                        Tipslip.TotalCEarned = 0.00;
                        $scope.TipLeadRow.push(Tipslip);
                    }
                });
                $scope.GetSaletypelist();
                _this.SalesComService.GetBranches().then(function (data) {
                    $scope.Brancheslists = data;
                    var splitedBranch;
                    splitedBranch = [];
                    $scope.EmployeeBranch = [];
                    splitedBranch = $scope.LoggedinEmployee.SecondaryBranch[0].split(',');
                    splitedBranch.push($scope.LoggedinEmployee.PrimaryBranch);
                    $.each($scope.Brancheslists, function (key, value) {
                        for (var i = 0; i <= splitedBranch.length - 1; i++) {
                            if (splitedBranch[i] == value.BranchID) {
                                if (value.IsActive == true) {
                                    $scope.EmployeeBranch.push(value);
                                }
                            }
                        }
                    });
                });
            };
            $scope.ResubmitCommission = function (status) {
                var dateofsale = new Date($scope.Commissionslip.DateofSale);
                var entrydate = new Date($scope.Commissionslip.EntryDate);
                if (dateofsale > entrydate) {
                    $.bootstrapGrowl("Date of entry should be greater or equal to Date of sale", { type: 'danger' });
                    return false;
                }
                $scope.Commissionslip.Status = status;
                $scope.Commissionslip.ModifiedBy = $scope.LoggedinEmployee.UID;
                var error = 0;
                $.each($scope.TipLeadRow, function (key, value) {
                    if ((value.TipLeadID == undefined || value.TipLeadID == 0) && value.TipLeadAmount != 0 && value.TipLeadAmount != undefined) {
                        error = 1;
                    }
                    else if ((value.TipLeadAmount == undefined || value.TipLeadAmount == 0) && value.TipLeadID != undefined && value.TipLeadID != 0) {
                        error = 2;
                    }
                    else if ((value.TipLeadID == undefined || value.TipLeadID == 0) && ((value.PositiveAdjustments != 0 && value.PositiveAdjustments != undefined) || (value.NegativeAdjustments != 0 && value.NegativeAdjustments != undefined))) {
                        error = 1;
                    }
                    else if ((value.TipLeadAmount == undefined || value.TipLeadAmount == 0) && ((value.PositiveAdjustments != 0 && value.PositiveAdjustments != undefined) || (value.NegativeAdjustments != 0 && value.NegativeAdjustments != undefined))) {
                        error = 2;
                    }
                });
                if (error == 1) {
                    $.bootstrapGrowl("Please select Tip Lead", { type: 'danger' });
                    return false;
                }
                else if (error == 2) {
                    $.bootstrapGrowl("Please enter Tip Lead Amount", { type: 'danger' });
                    return false;
                }
                $scope.Commissionslip.TipLeadSliplist = $scope.TipLeadRow.filter(function (Tiplead) {
                    return (Tiplead.TipLeadID != undefined && Tiplead.TipLeadID != 0);
                });
                var tipleadstatus = [];
                $.each($scope.Commissionslip.TipLeadSliplist, function (key, value) {
                    value.SlipType = mtisalescom.SlipTypes.TipLeadSlip;
                    tipleadstatus[key] = value.Status;
                    value.Status = ((value.Status != 7 && value.Status != 5 && !value.ProcesByPayroll) && ($scope.Commissionslip.Status == 2 || $scope.Commissionslip.Status == 1)) ? $scope.Commissionslip.Status : value.Status;
                });
                _this.SalesComService.EditCommission($scope.Commissionslip).then(function (data) {
                    if (data == true) {
                        if (status == 2) {
                            $.bootstrapGrowl("Commission slip Re-submitted successfully.", { type: 'success' });
                            $scope.MailContent = new mtisalescom.EmailMessage;
                            var uid = $scope.LoggedinEmployee.ReportMgr != null && $scope.LoggedinEmployee.ReportMgr > 0 ? $scope.LoggedinEmployee.ReportMgr : $scope.LoggedinEmployee.ApproveMgr;
                            _this.SalesComService.CommissionCreatedNotification(uid, 0).then(function (data) {
                                $scope.MailContent.RecipientTo = $scope.LoggedinEmployee.Email + ',' + data;
                                $scope.MailContent.Subject = "Commission Slip Resubmitted";
                                $scope.MailContent.Body = "A commission slip Resubmitted. Please click the link to view the <a href=\"" + $scope.MailUrl + "\"> New commission slip created.</a>";
                                $scope.SendMail();
                            });
                            var parentnode = _this;
                            $.each($scope.Commissionslip.TipLeadSliplist, function (key, value) {
                                if (value.ID == undefined || tipleadstatus[key] == 1) {
                                    var uid = value.TipLeadID;
                                    parentnode.SalesComService.CommissionCreatedNotification(uid, status).then(function (data) {
                                        $scope.MailContent.RecipientTo = data;
                                        $scope.MailContent.Subject = "New Tip Lead Slip";
                                        $scope.MailContent.Body = "A new Tip Lead Slip created. Please click the link to <a href=\"" + $scope.MailUrl + "\"> view the New Tip Lead slip.</a>";
                                        $scope.SendMail();
                                    });
                                }
                            });
                        }
                        $scope.GoToCommissionslipDetails();
                    }
                    else {
                        $.bootstrapGrowl("Unfortunately Commission details cannot be processed.Please retry.", { type: 'danger' });
                    }
                });
                return true;
            };
            $scope.GoToViewCommissionslip = function (commissionid, edit) {
                HeaderFactory.SetTitle("Sales Rep - View Commission Slip");
                $scope.SelectedTab = 'commission';
                $scope.Iseditable = edit;
                $scope.page = $scope.pages.ViewCommissionslip;
                $scope.TipLeadRow = [];
                _this.SalesComService.GetCommissionbyID(commissionid).then(function (data) {
                    $scope.Commissionslips = data;
                    $scope.Commissionslip = $scope.Commissionslips[0];
                    $scope.Commissionslip.Comments = ""; // Empty for new comment
                    $scope.Commissionslip.EntryDate = $scope.Commissionslip.EntryDate.split("-");
                    $scope.Commissionslip.EntryDate = $scope.Commissionslip.EntryDate[1] + '/' + $scope.Commissionslip.EntryDate[2].substring(0, 2) + '/' + $scope.Commissionslip.EntryDate[0];
                    $scope.Commissionslip.DateofSale = $scope.Commissionslip.DateofSale.split("-");
                    $scope.Commissionslip.DateofSale = $scope.Commissionslip.DateofSale[1] + '/' + $scope.Commissionslip.DateofSale[2].substring(0, 2) + '/' + $scope.Commissionslip.DateofSale[0];
                    //$scope.Commissionslip.AccountPeriod = $scope.Commissionslip.AccountPeriod.split("-");
                    //$scope.Commissionslip.AccountPeriod = $scope.Commissionslip.AccountPeriod[1] + '/' + $scope.Commissionslip.AccountPeriod[0];
                    var Commissionlist;
                    $scope.Commissionslips = $scope.Commissionslips.filter(function (commission) {
                        return (commission.SlipType == mtisalescom.SlipTypes.TipLeadSlip);
                    });
                    var Tipslip;
                    if ($scope.Commissionslips.length > 0) {
                        $.each($scope.Commissionslips, function (key, value) {
                            Tipslip = new mtisalescom.TipLeadSlip;
                            Tipslip.ID = value.ID;
                            Tipslip.MainCommissionID = value.MainCommissionID;
                            Tipslip.TipLeadID = value.TipLeadID;
                            Tipslip.TipLeadEmpID = value.TipLeadEmpID;
                            Tipslip.TipLeadName = value.TipLeadName;
                            Tipslip.TipLeadAmount = value.TipLeadAmount;
                            Tipslip.PositiveAdjustments = value.PositiveAdjustments;
                            Tipslip.NegativeAdjustments = value.NegativeAdjustments;
                            Tipslip.CompanyContribution = value.CompanyContribution;
                            Tipslip.SlipType = value.SlipType;
                            Tipslip.TotalCEarned = value.TotalCEarned;
                            Tipslip.Status = value.Status;
                            Tipslip.ProcesByPayroll = value.ProcesByPayroll;
                            $scope.TipLeadRow.push(Tipslip);
                        });
                    }
                    else {
                        Tipslip = new mtisalescom.TipLeadSlip;
                        Tipslip.TipLeadAmount = 0.00;
                        Tipslip.PositiveAdjustments = 0.00;
                        Tipslip.NegativeAdjustments = 0.00;
                        Tipslip.CompanyContribution = 0.00;
                        Tipslip.TotalCEarned = 0.00;
                        $scope.TipLeadRow.push(Tipslip);
                    }
                });
                $scope.GetSaletypelist();
            };
            $scope.SendMail = function () {
                if ($scope.MailContent.RecipientTo != '' || $scope.MailContent.RecipientTo != null) {
                    _this.SalesComService.SendMail($scope.MailContent).then(function (data) {
                    });
                }
            };
            $scope.DeleteMessage = function (Commission) {
                $scope.ShowAYSDialog("Are You Sure?", "All the commission slip details will be erased, are you sure to delete?", function () {
                    $scope.DeleteCommission(Commission);
                });
            };
            $scope.onReportYearChange = function (year) {
                $scope.EditPayroll = [];
                if (year == 0) {
                    $scope.EditPayroll = [];
                }
                else {
                    _this.SalesComService.GetPayrollForEdit(year).then(function (data) {
                        $scope.EditPayroll = data;
                        $scope.EditPayroll.sort();
                    });
                }
            };
            //$scope.onReportPeriodChange = (): void => {
            //    debugger;
            //    var example = $scope.EditPayroll.filter(function (key, index) {
            //        if (key.ID = $scope.ReportInputparameter.ReportPeriod) {
            //            return key.DateFrom;
            //        }
            //    });
            //    alert(example);
            //}
            $scope.GoToSalesRepReports = function () {
                HeaderFactory.SetTitle("Sales Representative - Reports");
                $scope.SelectedTab = 'reports';
                $scope.page = $scope.pages.SalesRepReports;
                $scope.Commissionslips = [];
                $scope.Salespersonslist = [];
                $scope.SRSalespersons = [];
                $scope.TipLeadlist = [];
                $scope.ReportInputparameter = new mtisalescom.ReportParameters;
                if ($scope.LoggedinEmployee.RoleID == mtisalescom.RoleTypes.SalesManager || $scope.LoggedinEmployee.RoleID == mtisalescom.RoleTypes.GeneralManager || $scope.LoggedinEmployee.RoleID == mtisalescom.RoleTypes.PayRoll) {
                    $scope.parameter = [$scope.LoggedinEmployee.UID, $scope.LoggedinEmployee.RoleID];
                    _this.SalesComService.GetSalesReplistforReport($scope.parameter).then(function (data) {
                        $scope.Salespersonslist = data;
                        $scope.SRSalespersons = $scope.Salespersonslist.filter(function (person) {
                            return (person.RoleID == mtisalescom.RoleTypes.SalesRep);
                        });
                        $scope.TipLeadlist = $scope.Salespersonslist.filter(function (person) {
                            return (person.RoleID == mtisalescom.RoleTypes.SalesRep || person.RoleID == mtisalescom.RoleTypes.NonSalesEmployee);
                        });
                    });
                }
                var currentdate = new Date();
                var currentyear = currentdate.getFullYear();
                $scope.PayrollDS = [];
                $scope.ReportdropdownConfigyear = [];
                _this.SalesComService.GetPayrollForDashBoard().then(function (data) {
                    debugger;
                    $scope.PayrollDS = data;
                    for (var i = 0; i <= $scope.PayrollDS.length - 1; i++) {
                        if ($scope.PayrollDS[i].Year <= currentyear) {
                            $scope.ReportdropdownConfigyear.push($scope.PayrollDS[i]);
                        }
                    }
                });
                var nextyear = currentyear + 1;
                var prevyear = currentyear - 1;
                $scope.Reportdropdownyear = [{ value: prevyear - 1, name: prevyear - 1 + "-" + prevyear }, { value: currentyear - 1, name: currentyear - 1 + "-" + currentyear }, { value: currentyear, name: currentyear + "-" + nextyear }];
                $scope.ReportType = 0;
                //$scope.Reportdropdownyear[0] = currentyear;
                $scope.Reportdropdownyear.sort();
            };
            $scope.GoToPaylocityGlReports = function () {
                HeaderFactory.SetTitle("Paylocity/GL - Reports");
                $scope.SelectedTab = 'reports';
                $scope.page = $scope.pages.PaylocityGlReports;
                $scope.Reports = new mtisalescom.ReportParameters();
                $scope.Reports.Type = 1;
                $scope.PendingStatus = 0;
                $scope.PaylocityExportList = [];
                $scope.GLExportList = [];
                $scope.GetBranchlist();
                var currentdate = new Date();
                var currentyear = currentdate.getFullYear();
                $scope.ReportdropdownConfigyear = [];
                _this.SalesComService.GetPayrollForDashBoard().then(function (data) {
                    debugger;
                    $scope.PayrollDS = data;
                    for (var i = 0; i <= $scope.PayrollDS.length - 1; i++) {
                        if ($scope.PayrollDS[i].Year <= currentyear) {
                            $scope.ReportdropdownConfigyear.push($scope.PayrollDS[i]);
                        }
                    }
                });
            };
            $scope.GetCountforPaylocity = function () {
                //var reportperiod = $scope.Reports.ReportPeriod.split("/");
                //$scope.Reports.ReportMonth = reportperiod[0];//2;
                //$scope.Reports.ReportYear = reportperiod[1];
                var count;
                _this.SalesComService.GetCountforPaylocity($scope.Reports).then(function (data) {
                    count = data;
                    if (count[0] == 0 && count[1] == 0) {
                        $scope.PendingStatus = 0;
                    }
                    else if (count[1] > 0) {
                        $scope.PendingStatus = 1;
                    }
                    else if (count[0] > 0 && count[1] == 0) {
                        $scope.PendingStatus = 2;
                    }
                    $scope.Pendingcount = count[1];
                });
            };
            $scope.ViewPaylocityGLReport = function () {
                debugger;
                $scope.ReportType = 0;
                $scope.PaylocityExportList = [];
                $scope.GLExportList = [];
                if ($scope.Reports.PayrollConfigID == undefined) {
                    $.bootstrapGrowl("Please select Period.", { type: 'danger' });
                }
                else if ($scope.Reports.Type == 1) {
                    var selecteddate;
                    var periodMonthDetail;
                    $.each($scope.EditPayroll, function (key, value) {
                        if (value.ID == $scope.Reports.PayrollConfigID) {
                            selecteddate = new Date(value.DateTo);
                            periodMonthDetail = value.Month;
                        }
                    });
                    //var reportperiod = $scope.Reports.ReportPeriod.split("/");
                    $scope.ReportPeriodNote = periodMonthDetail;
                    $scope.Reports.ReportPeriod = selecteddate;
                    $scope.Reports.PayrollConfigID; //= reportperiod[0];//2;
                    $scope.Reports.ReportYear; //= reportperiod[1];//not needed
                    if ($scope.Reports.BranchID == undefined) {
                        $.bootstrapGrowl("Please select Location.", { type: 'danger' });
                    }
                    else {
                        $scope.ReportType = 1;
                        _this.SalesComService.ViewPaylocityReport($scope.Reports).then(function (data) {
                            $scope.PaylocityExportList = data;
                        });
                    }
                }
                else if ($scope.Reports.Type == 2) {
                    var selecteddate;
                    var periodMonthDetail;
                    $.each($scope.EditPayroll, function (key, value) {
                        if (value.ID == $scope.Reports.PayrollConfigID) {
                            selecteddate = new Date(value.DateTo);
                            periodMonthDetail = value.Month;
                        }
                    });
                    $scope.ReportPeriodNote = periodMonthDetail;
                    $scope.Reports.ReportPeriod = selecteddate;
                    $scope.ReportType = 2;
                    _this.SalesComService.ViewGLReport($scope.Reports).then(function (data) {
                        $scope.GLExportList = data;
                        $scope.Reports.ReportMonth = selecteddate.getMonth() + 1;
                        //$scope.Reports.ReportYear = selecteddate.getFullYear();
                        $.each($scope.GLExportList, function (key, value) {
                            value.Month = $scope.Reports.ReportMonth + '' + $scope.Reports.ReportYear;
                        });
                    });
                }
            };
            $scope.DeleteCommission = function (Commission) {
                HeaderFactory.SetTitle("Pay Plan Details");
                $scope.page = $scope.pages.PayPlanDetails;
                Commission.IsActive = false;
                Commission.ModifiedBy = $scope.LoggedinEmployee.UID;
                _this.SalesComService.DeleteCommission(Commission).then(function (data) {
                    if (data == true) {
                        $.bootstrapGrowl("Commission deleted successfully.", { type: 'success' });
                        $scope.GoToCommissionslipDetails();
                    }
                });
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
            $scope.ShowPayPlan = function (title) {
                var modalInstance = $uibModal.open({
                    templateUrl: 'App/views/m/ModalPayPlan.html',
                    controller: 'PayPlanDialogController',
                    backdrop: 'static',
                    scope: $scope,
                    resolve: {
                        title: function () {
                            return title;
                        },
                    }
                });
                modalInstance.result.then(function (success) {
                    if (success) {
                    }
                }, function () {
                });
            };
            $scope.ModalViewPayPlan = function (planID) {
                $scope.Plandetail = new mtisalescom.Plancomponent();
                $scope.Plandetail.PlanID = planID;
                _this.SalesComService.GetPlanbyID($scope.Plandetail).then(function (data) {
                    $scope.Plandetail = data;
                    $scope.Plandetail.BasisType = mtisalescom.Basistype[$scope.Plandetail.BasisType];
                    $scope.Plandetail.BMQuotaBonus = mtisalescom.Booleanvalues[$scope.Plandetail.BMQuotaBonus == true ? 1 : 0];
                    $scope.Plandetail.TenureBonus = mtisalescom.Booleanvalues[$scope.Plandetail.TenureBonus == true ? 1 : 0];
                    $scope.Plandetail.SMEligible = mtisalescom.Booleanvalues[$scope.Plandetail.SMEligible == true ? 1 : 0];
                });
                $scope.ShowPayPlan("View Pay Plan");
            };
            $scope.ModalDeactivatedPayPlan = function () {
                $scope.Employeedetail = new mtisalescom.EmployeeComponant();
                _this.SalesComService.GetDeActivatePlanEmployees().then(function (data) {
                    $scope.Employeedetail = data;
                });
                $scope.ShowDeactivatedPayPlan("View Deactivated Pay Plan");
            };
            $scope.ShowDeactivatedPayPlan = function (title) {
                var modalInstance = $uibModal.open({
                    templateUrl: 'App/views/m/DeactivatedPayPlan.html',
                    controller: 'PayPlanDialogController',
                    backdrop: 'static',
                    scope: $scope,
                    resolve: {
                        title: function () {
                            return title;
                        },
                    }
                });
                modalInstance.result.then(function (success) {
                    if (success) {
                    }
                }, function () {
                });
            };
            $scope.ViewRolesItems = function () {
                $scope.GetRoleslist();
            };
            $scope.GetRoleslist = function () {
                $scope.Roleslists = [];
                $scope.rolelist = [];
                _this.SalesComService.GetRoles().then(function (data) {
                    $scope.Roleslists = data;
                    if ($scope.LoggedinEmployee.RoleID == mtisalescom.RoleTypes.Administrator) {
                        $scope.rolelist = $scope.Roleslists.filter(function (roles) {
                            return (roles.RoleID == mtisalescom.RoleTypes.Administrator || roles.RoleID == mtisalescom.RoleTypes.PayRoll);
                        });
                    }
                    else if ($scope.LoggedinEmployee.RoleID == mtisalescom.RoleTypes.PayRoll) {
                        $scope.rolelist = $scope.Roleslists.filter(function (roles) {
                            return (roles.RoleID == mtisalescom.RoleTypes.SalesManager || roles.RoleID == mtisalescom.RoleTypes.GeneralManager || roles.RoleID == mtisalescom.RoleTypes.SalesRep || roles.RoleID == mtisalescom.RoleTypes.NonSalesEmployee);
                        });
                    }
                });
            };
            $scope.GetBranchlist = function () {
                $scope.Brancheslists = [];
                $scope.SecondaryBrancheslists = [];
                _this.SalesComService.GetBranches().then(function (data) {
                    $scope.Brancheslists = data;
                    $scope.SecondaryBrancheslists = $scope.Brancheslists;
                    $scope.SecondaryBrancheslists = $scope.SecondaryBrancheslists.filter(function (branch) {
                        return (branch.IsActive == true);
                    });
                });
            };
            $scope.GetSaletypelist = function () {
                $scope.SaleTypeList = [];
                _this.SalesComService.GetAllSaleType().then(function (data) {
                    $scope.SaleTypeList = data;
                });
            };
            $scope.GetAllManagerlist = function () {
                $scope.Managerslists = [];
                $scope.SalesManagerslists = [];
                $scope.GeneralManagerslists = [];
                _this.SalesComService.GetAllManager().then(function (data) {
                    $scope.Managerslists = data;
                    $scope.SalesManagerslists = $scope.Managerslists.filter(function (employee) {
                        return (employee.RoleID == mtisalescom.RoleTypes.SalesManager);
                    });
                    $scope.GeneralManagerslists = $scope.Managerslists.filter(function (employee) {
                        return (employee.RoleID == mtisalescom.RoleTypes.GeneralManager);
                    });
                });
            };
            $scope.GetPlanlist = function () {
                $scope.Planlists = [];
                _this.SalesComService.GetPlans().then(function (data) {
                    var planList = $scope.Planlists;
                    planList = data;
                    $scope.Planlists = planList.filter(function (plan) {
                        return (plan.IsActive == true);
                    });
                });
            };
            $scope.SRReportTypes = [{ value: 1, name: "Commission Details" }, { value: 2, name: "Total Earnings" }];
            $scope.SMGMReportTypes = [{ value: 1, name: "Commission Details" }, { value: 2, name: "Total Earnings" }, { value: 3, name: "Incentive Trips" }];
            $scope.Getcommissionreport = function () {
                debugger;
                HeaderFactory.SetTitle("Sales Representative - Reports");
                $scope.SelectedTab = 'reports';
                $scope.page = $scope.pages.SalesRepReports;
                $scope.ReportType = $scope.ReportInputparameter.ReportType;
                var months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
                var Months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
                $scope.Commissionslips = [];
                $scope.ReportTotalEarnings = [];
                var selectedperiod = $scope.ReportInputparameter.ReportPeriod;
                $scope.Employeedetail = new mtisalescom.EmployeeComponant;
                var MonthsofExperience = 0;
                if ($scope.LoggedinEmployee.RoleID == mtisalescom.RoleTypes.SalesRep || $scope.LoggedinEmployee.RoleID == mtisalescom.RoleTypes.NonSalesEmployee) {
                    $scope.ReportInputparameter.SalesPerson = $scope.LoggedinEmployee.UID; //need to change
                    $scope.Employeedetail.UID = $scope.LoggedinEmployee.UID; //need to change
                }
                else {
                    $scope.Employeedetail.UID = $scope.ReportInputparameter.SalesPerson;
                }
                if ($scope.ReportInputparameter.ReportType == undefined) {
                    $.bootstrapGrowl("Please select Report Type.", { type: 'danger' });
                    $scope.ReportError = -1;
                }
                else {
                    if ($scope.Employeedetail.UID == undefined && $scope.ReportInputparameter.ReportType != 3) {
                        $.bootstrapGrowl("Please select valid Employee.", { type: 'danger' });
                        $scope.ReportError = -1;
                    }
                    else {
                        if ($scope.ReportInputparameter.ReportType == 1) {
                            if ($scope.ReportInputparameter.ReportPeriod == undefined) {
                                $.bootstrapGrowl("Please select Period.", { type: 'danger' });
                                $scope.ReportError = -1;
                            }
                            else {
                                //var selecteddate;
                                //var periodMonthDetail;
                                //$.each($scope.EditPayroll, function (key, value) {
                                //    if (value.ID == $scope.ReportInputparameter.PayrollConfigID) {
                                //        selecteddate = new Date(value.DateTo);
                                //        periodMonthDetail = value.Month;
                                //    }
                                //});
                                var reportperiod = $scope.ReportInputparameter.ReportPeriod.split("/");
                                $scope.ReportInputparameter.ReportMonth = reportperiod[0]; //2;
                                $scope.ReportInputparameter.ReportYear = reportperiod[1];
                                _this.SalesComService.GetEmployeebyUID($scope.Employeedetail).then(function (data) {
                                    $scope.Employeedetail = data;
                                    var dateinposition = new Date($scope.Employeedetail.DateInPosition);
                                    //var selectedMonth = selecteddate.getMonth() + 1;
                                    //$scope.ReportInputparameter.ReportMonth = selectedMonth;
                                    debugger;
                                    var selecteddate = new Date($scope.ReportInputparameter.ReportYear, $scope.ReportInputparameter.ReportMonth - 1, 1);
                                    var positiondate = new Date(dateinposition.getFullYear(), dateinposition.getMonth(), 1);
                                    $scope.ReportPeriodNote = Months[selecteddate.getMonth()] + '/' + selecteddate.getFullYear();
                                    if (positiondate > selecteddate) {
                                        var dateinpositionmonth = dateinposition.getMonth() + 1;
                                        $scope.Empdateinposition = dateinposition.getDate() + '/' + dateinpositionmonth + '/' + dateinposition.getFullYear();
                                        ;
                                        $scope.ReportError = 1;
                                    }
                                    else {
                                        debugger;
                                        $scope.ReportError = 0;
                                        MonthsofExperience = (selecteddate.getFullYear() - dateinposition.getFullYear()) * 12 + (selecteddate.getMonth() - dateinposition.getMonth()) + 1;
                                        if (MonthsofExperience > 0 && dateinposition.getDate() > 5) {
                                            MonthsofExperience = MonthsofExperience - 1;
                                        }
                                        $scope.ReportInputparameter.PlanID = parseInt($scope.Employeedetail.PayPlanID);
                                        $scope.ReportInputparameter.MonthsofExp = MonthsofExperience;
                                        _this.SalesComService.GetCommissionReport($scope.ReportInputparameter).then(function (data) {
                                            $scope.Commissionslips = data;
                                        });
                                        _this.SalesComService.GetCommissionslipReportDetails($scope.ReportInputparameter).then(function (data) {
                                            $scope.CommissionReportDetails = data;
                                            var finalRunNo = $scope.CommissionReportDetails.length - 1;
                                            $scope.DrawReportDetails = new mtisalescom.ReportParameters();
                                            $scope.DrawReportDetails = $scope.CommissionReportDetails[finalRunNo];
                                            //$scope.CommissionReportDetails.DrawType = ($scope.CommissionReportDetails.DrawType == 1 ? "Guaranteed" : ($scope.CommissionReportDetails.DrawType == 2 ?"Recoverable":"-"));
                                            //$scope.CommissionReportDetails.RecoverablePercent = $scope.CommissionReportDetails.RecoverablePercent * 100;
                                            $scope.DrawReportDetails.DrawType = ($scope.DrawReportDetails.DrawType == 1 ? "Guaranteed" : ($scope.DrawReportDetails.DrawType == 2 ? "Recoverable" : "-"));
                                            $scope.DrawReportDetails.RecoverablePercent = $scope.DrawReportDetails.RecoverablePercent * 100;
                                            $scope.TotalReportValue = new mtisalescom.ReportParameters();
                                            //------------Assign Zero intially since append not supported in Typescript------------
                                            $scope.TotalReportValue.TotalCommission = 0;
                                            $scope.TotalReportValue.DrawPaid = 0;
                                            $scope.TotalReportValue.DrawRecovered = 0;
                                            $scope.TotalReportValue.CommissionDue = 0;
                                            $scope.TotalReportValue.Salary = 0;
                                            $scope.TotalReportValue.Bimonthreport = 0;
                                            $scope.TotalReportValue.Tenurereport = 0;
                                            $scope.TotalReportValue.TotalEarning = 0;
                                            //--------------------------------------------------------------------------------------
                                            $.each($scope.CommissionReportDetails, function (key, value) {
                                                $scope.TotalReportValue.TotalCommission = value.TotalCommission + $scope.TotalReportValue.TotalCommission;
                                                $scope.TotalReportValue.DrawPaid = value.DrawPaid + $scope.TotalReportValue.DrawPaid;
                                                $scope.TotalReportValue.DrawRecovered = value.DrawRecovered + $scope.TotalReportValue.DrawRecovered;
                                                $scope.TotalReportValue.CommissionDue = value.CommissionDue + $scope.TotalReportValue.CommissionDue;
                                                $scope.TotalReportValue.Salary = +value.Salary + $scope.TotalReportValue.Salary;
                                                $scope.TotalReportValue.Bimonthreport = value.Bimonthreport + $scope.TotalReportValue.Bimonthreport;
                                                $scope.TotalReportValue.Tenurereport = value.Tenurereport + $scope.TotalReportValue.Tenurereport;
                                                $scope.TotalReportValue.TotalEarning = value.TotalEarning + $scope.TotalReportValue.TotalEarning;
                                            });
                                        });
                                    }
                                });
                            }
                        }
                        else if ($scope.ReportInputparameter.ReportType == 2) {
                            debugger;
                            //var reportfromperiod = $scope.ReportInputparameter.ReportFrom.split("/");
                            //var reportfrommonth = reportfromperiod[0];
                            //var reportfromyear = reportfromperiod[1];
                            //var ReportFrom = new Date(reportfromyear, reportfrommonth - 1, 1);
                            //var reporttoperiod = $scope.ReportInputparameter.ReportTo.split("/");
                            //var reporttomonth = reporttoperiod[0];
                            //var reporttoyear = reporttoperiod[1];
                            //var ReportTo = new Date(reporttoyear, reporttomonth - 1, 1);
                            $scope.ReportInputparameter.ReportMonth = 9; //Initially set the report month value to September
                            if ($scope.ReportInputparameter.ReportYear == undefined) {
                                $.bootstrapGrowl("Please select From Period.", { type: 'danger' });
                                $scope.ReportError = -1;
                            }
                            else {
                                $scope.ReportError = 0;
                                _this.SalesComService.GetEmployeebyUID($scope.Employeedetail).then(function (data) {
                                    $scope.Employeedetail = data;
                                });
                                var nextyear_1 = $scope.ReportInputparameter.ReportYear;
                                nextyear_1++;
                                $scope.ReportPeriodNote = "Sep/" + $scope.ReportInputparameter.ReportYear + " - Aug/" + nextyear_1;
                                $scope.ReportInputparameter.ReportPeriod = $scope.currentDt;
                                //  $scope.ReportInputparameter.ReportPeriod = new Date($scope.ReportInputparameter.ReportFrom);
                                //  var reportdate = new Date($scope.ReportInputparameter.ReportPeriod);
                                //  var nextyear = reportdate.getFullYear() + 1;
                                //$scope.ReportPeriodNote = "Sep/" + $scope.ReportInputparameter.ReportYear + " - Aug/" + nextyear;
                                //$scope.ReportPeriodNote = periodMonthDetail;// $scope.ReportInputparameter.ReportMonth;// Months[ReportFrom.getMonth()] + "/" + ReportFrom.getFullYear() + " - " + Months[ReportTo.getMonth()] + "/" + ReportTo.getFullYear();
                                _this.SalesComService.GetTotalEarningReport($scope.ReportInputparameter).then(function (data) {
                                    $scope.ReportTotalEarnings = data;
                                });
                            }
                        }
                        else if ($scope.ReportInputparameter.ReportType == 3) {
                            if ($scope.ReportInputparameter.ReportYear == undefined) {
                                $.bootstrapGrowl("Please select Period.", { type: 'danger' });
                                $scope.ReportError = -1;
                            }
                            else {
                                $scope.ReportError = 0;
                                $scope.ReportInputparameter.ReportPeriod = new Date($scope.ReportInputparameter.ReportYear, 9, 0);
                                var reportdate = new Date($scope.ReportInputparameter.ReportPeriod);
                                var nextyear = reportdate.getFullYear() + 1;
                                $scope.ReportPeriodNote = "Sep/" + $scope.ReportInputparameter.ReportYear + " - Sep/" + nextyear;
                                var inputparameter;
                                inputparameter = [$scope.LoggedinEmployee.UID, $scope.LoggedinEmployee.RoleID, $scope.ReportInputparameter.ReportYear];
                                _this.SalesComService.GetIncentiveTripReport(inputparameter).then(function (data) {
                                    $scope.Commissionslips = data;
                                });
                            }
                        }
                    }
                }
            };
            $scope.SaveReportConfirmation = function (ReportInputparameter) {
                $scope.ShowAYSDialog("Are You Sure?", "Exporting paylocity report will change all the related commission slip status as ‘Processed’, Do you want to continue?", function () {
                    angular.element('#Paylocitybutton').trigger('click');
                    $scope.SaveReport(ReportInputparameter);
                });
            };
            $scope.SaveReport = function (ReportInputparameter) {
                var periodMonthDetail;
                $.each($scope.EditPayroll, function (key, value) {
                    if (value.ID == ReportInputparameter.PayrollConfigID) {
                        ReportInputparameter.ReportPeriod = new Date(value.DateTo);
                        periodMonthDetail = value.Month;
                    }
                });
                _this.SalesComService.SaveReportdetails(ReportInputparameter).then(function (data) {
                    return data;
                });
            };
            $scope.ModalEditReportCommission = function (commissionID) {
                $scope.Brancheslists = [];
                $scope.Salespersonslist = [];
                $scope.SRSalespersons = [];
                $scope.TipLeadRow = [];
                $scope.Commissionslip = new mtisalescom.CommissionComponent();
                $scope.Commissionslip.CustomerType = mtisalescom.CustomerTypes.NewCustomer;
                $scope.Commissionslip.TipLeadAmount = 0.00;
                $scope.Commissionslip.BaseCommission = 0.00;
                $scope.Commissionslip.LeaseCommission = 0.00;
                $scope.Commissionslip.ServiceCommission = 0.00;
                $scope.Commissionslip.TravelCommission = 0.00;
                $scope.Commissionslip.CashCommission = 0.00;
                $scope.Commissionslip.SpecialCommission = 0.00;
                var commissionslips;
                var roleID = mtisalescom.RoleTypes.PayRoll;
                _this.SalesComService.GetCommissionbyID(commissionID).then(function (data) {
                    commissionslips = data;
                    $scope.Commissionslip = commissionslips[0];
                    $scope.Commissionslip.Comments = ""; // Empty for new comment
                    $scope.Commissionslip.EntryDate = $scope.Commissionslip.EntryDate.split("-");
                    $scope.Commissionslip.EntryDate = $scope.Commissionslip.EntryDate[1] + '/' + $scope.Commissionslip.EntryDate[2].substring(0, 2) + '/' + $scope.Commissionslip.EntryDate[0];
                    $scope.Commissionslip.DateofSale = $scope.Commissionslip.DateofSale.split("-");
                    $scope.Commissionslip.DateofSale = $scope.Commissionslip.DateofSale[1] + '/' + $scope.Commissionslip.DateofSale[2].substring(0, 2) + '/' + $scope.Commissionslip.DateofSale[0];
                    //$scope.Commissionslip.AccountPeriod = $scope.Commissionslip.AccountPeriod.split("-");
                    //$scope.Commissionslip.AccountPeriod = $scope.Commissionslip.AccountPeriod[1] + '/' + $scope.Commissionslip.AccountPeriod[0];
                    $scope.Commissionslip.BranchID = String($scope.Commissionslip.BranchID);
                    var splitedBranch;
                    splitedBranch = [];
                    $scope.EmployeeBranch = [];
                    $scope.Employeedetail = new mtisalescom.EmployeeComponant;
                    $scope.Employeedetail.UID = $scope.Commissionslip.CreatedBy;
                    _this.SalesComService.GetEmployeebyUID($scope.Employeedetail).then(function (data) {
                        $scope.Employeedetail = data;
                        splitedBranch = $scope.Employeedetail.SecondaryBranch[0].split(',');
                        splitedBranch.push($scope.Employeedetail.PrimaryBranch);
                        _this.SalesComService.GetBranches().then(function (data) {
                            $scope.Brancheslists = data;
                            $.each($scope.Brancheslists, function (key, value) {
                                for (var i = 0; i <= splitedBranch.length - 1; i++) {
                                    if (splitedBranch[i] == value.BranchID) {
                                        if (value.IsActive == true) {
                                            $scope.EmployeeBranch.push(value);
                                        }
                                    }
                                }
                            });
                        });
                    });
                    _this.SalesComService.GetAllEmployeeDetailsbyRoleID(roleID).then(function (data) {
                        $scope.Salespersonslist = data;
                        $scope.SRSalespersons = $scope.Salespersonslist.filter(function (person) {
                            return (person.RoleID == mtisalescom.RoleTypes.SalesRep);
                        });
                    });
                    $.each($scope.SRSalespersons, function (key, value) {
                        $scope.splitnamelist.push(value.LastName);
                    });
                    var Commissionlist;
                    commissionslips = commissionslips.filter(function (commission) {
                        return (commission.SlipType == mtisalescom.SlipTypes.TipLeadSlip);
                    });
                    var Tipslip;
                    if (commissionslips.length > 0) {
                        $.each(commissionslips, function (key, value) {
                            Tipslip = new mtisalescom.TipLeadSlip;
                            Tipslip.ID = value.ID;
                            Tipslip.MainCommissionID = value.MainCommissionID;
                            Tipslip.TipLeadID = value.TipLeadID;
                            Tipslip.TipLeadEmpID = value.TipLeadEmpID;
                            Tipslip.TipLeadName = value.TipLeadName;
                            Tipslip.TipLeadAmount = value.TipLeadAmount;
                            Tipslip.PositiveAdjustments = value.PositiveAdjustments;
                            Tipslip.NegativeAdjustments = value.NegativeAdjustments;
                            Tipslip.CompanyContribution = value.CompanyContribution;
                            Tipslip.SlipType = value.SlipType;
                            Tipslip.TotalCEarned = value.TotalCEarned;
                            Tipslip.Status = value.Status;
                            Tipslip.ProcesByPayroll = value.ProcesByPayroll;
                            $scope.TipLeadRow.push(Tipslip);
                        });
                    }
                    else {
                        Tipslip = new mtisalescom.TipLeadSlip;
                        Tipslip.TipLeadAmount = 0.00;
                        Tipslip.PositiveAdjustments = 0.00;
                        Tipslip.NegativeAdjustments = 0.00;
                        Tipslip.CompanyContribution = 0.00;
                        Tipslip.TotalCEarned = 0.00;
                        $scope.TipLeadRow.push(Tipslip);
                    }
                    $scope.MainCommissionStatus = 0;
                    if ($scope.Commissionslip.SlipType == mtisalescom.SlipTypes.TipLeadSlip) {
                        var Commissionslips;
                        _this.SalesComService.GetCommissionbyID($scope.Commissionslip.MainCommissionID).then(function (data) {
                            Commissionslips = data;
                            if (Commissionslips[0].ProcesByPayroll == true) {
                                $scope.MainCommissionStatus = 1;
                            }
                            else if (Commissionslips[0].Status == 7) {
                                $scope.MainCommissionStatus = 2;
                            }
                        });
                    }
                });
                $scope.ShowEditReportCommission("Edit Commission Details", function () { $scope.EditReportCommission(); });
            };
            $scope.ShowEditReportCommission = function (title, callback) {
                var modalInstance = $uibModal.open({
                    templateUrl: 'App/views/m/ModalReportCommission.html',
                    controller: 'ReportEditCommissionController',
                    backdrop: 'static',
                    scope: $scope,
                    resolve: {
                        title: function () {
                            return title;
                        },
                    }
                });
                modalInstance.result.then(function (success) {
                    if (success) {
                        callback();
                    }
                }, function () {
                });
            };
            $scope.EditReportCommission = function () {
                var dateofsale = new Date($scope.Commissionslip.DateofSale);
                var entrydate = new Date($scope.Commissionslip.EntryDate);
                if (dateofsale > entrydate) {
                    $.bootstrapGrowl("Date of entry should be greater or equal to Date of sale", { type: 'danger' });
                    $scope.ShowEditReportCommission("Edit Commission Details", function () { $scope.EditReportCommission(); });
                    return false;
                }
                else {
                    $scope.Commissionslip.ModifiedBy = $scope.LoggedinEmployee.UID;
                    $scope.Commissionslip.CreatedOn = $scope.currentDtTime;
                    if ($scope.Commissionslip.SlipType == mtisalescom.SlipTypes.CommissionSlip) {
                        var error = 0;
                        $.each($scope.TipLeadRow, function (key, value) {
                            if ((value.TipLeadID == undefined || value.TipLeadID == 0) && value.TipLeadAmount != 0 && value.TipLeadAmount != undefined) {
                                error = 1;
                            }
                            else if ((value.TipLeadAmount == undefined || value.TipLeadAmount == 0) && value.TipLeadID != undefined && value.TipLeadID != 0) {
                                error = 2;
                            }
                            else if ((value.TipLeadID == undefined || value.TipLeadID == 0) && ((value.PositiveAdjustments != 0 && value.PositiveAdjustments != undefined) || (value.NegativeAdjustments != 0 && value.NegativeAdjustments != undefined))) {
                                error = 1;
                            }
                            else if ((value.TipLeadAmount == undefined || value.TipLeadAmount == 0) && ((value.PositiveAdjustments != 0 && value.PositiveAdjustments != undefined) || (value.NegativeAdjustments != 0 && value.NegativeAdjustments != undefined))) {
                                error = 2;
                            }
                        });
                        if (error == 1) {
                            $.bootstrapGrowl("Please select Tip Lead", { type: 'danger' });
                            $scope.ShowEditReportCommission("Edit Commission Details", function () { $scope.EditReportCommission(); });
                            return false;
                        }
                        else if (error == 2) {
                            $.bootstrapGrowl("Please enter Tip Lead Amount", { type: 'danger' });
                            $scope.ShowEditReportCommission("Edit Commission Details", function () { $scope.EditReportCommission(); });
                            return false;
                        }
                        $scope.Commissionslip.TipLeadSliplist = $scope.TipLeadRow.filter(function (Tiplead) {
                            return (Tiplead.TipLeadID != undefined && Tiplead.TipLeadID != 0);
                        });
                        $.each($scope.Commissionslip.TipLeadSliplist, function (key, value) {
                            value.SlipType = mtisalescom.SlipTypes.TipLeadSlip;
                            value.Status = ((value.Status != 7 && value.Status != 5 && !value.ProcesByPayroll) && ($scope.Commissionslip.Status == 2 || $scope.Commissionslip.Status == 1)) ? $scope.Commissionslip.Status : value.Status;
                        });
                        $scope.Commissionslip.CommentedBy = $scope.LoggedinEmployee.FirstName + ' ' + $scope.LoggedinEmployee.LastName;
                        _this.SalesComService.EditCommission($scope.Commissionslip).then(function (data) {
                            if (data == true) {
                                $.bootstrapGrowl("Commission details saved successfully.", { type: 'success' });
                            }
                            else {
                                $.bootstrapGrowl("Unfortunately Commission details cannot be processed.Please retry.", { type: 'danger' });
                            }
                            $scope.Getcommissionreport();
                        });
                        return true;
                    }
                    else {
                        $scope.Commissionslip.TipLeadSliplist = $scope.TipLeadRow;
                        $.each($scope.Commissionslip.TipLeadSliplist, function (key, value) {
                            $scope.Commissionslip.TipLeadAmount = value.TipLeadAmount;
                            $scope.Commissionslip.PositiveAdjustments = value.PositiveAdjustments;
                            $scope.Commissionslip.NegativeAdjustments = value.NegativeAdjustments;
                            $scope.Commissionslip.CompanyContribution = value.CompanyContribution;
                            $scope.Commissionslip.TotalCEarned = value.TotalCEarned;
                        });
                        if ($scope.Commissionslip.TotalCEarned < 0) {
                            $.bootstrapGrowl("Totalcommission cannot be negative", { type: 'danger' });
                            $scope.ShowEditReportCommission("Edit Commission Details", function () { $scope.EditReportCommission(); });
                            return false;
                        }
                        _this.SalesComService.EditTipLeadSlip($scope.Commissionslip).then(function (data) {
                            if (data == true) {
                                $.bootstrapGrowl("Tip Lead details saved successfully.", { type: 'success' });
                            }
                            else {
                                $.bootstrapGrowl("Unfortunately Tip Lead details cannot be processed.Please retry.", { type: 'danger' });
                            }
                            $scope.Getcommissionreport();
                        });
                        return true;
                    }
                }
            };
            $scope.CheckUserLogin = function () {
                //Check user only if userid is zero
                if ($scope.LoggedinEmployee == null || $scope.LoggedinEmployee.UID == 0) {
                    _this.InitializeService.CheckUserLogin().then(function (data) {
                        $scope.LoggedinEmployee = data;
                        if ($scope.LoggedinEmployee.UID == 0) {
                            $.bootstrapGrowl("You are not authorized to log in to the System, contact Payroll.", { type: 'danger' });
                        }
                        else {
                            $scope.GetEmployeebyUID($scope.LoggedinEmployee);
                        }
                    });
                }
            };
            $scope.GetEmployeebyUID = function (emp) {
                _this.SalesComService.GetEmployeebyUID(emp).then(function (data) {
                    $scope.LoggedinEmployee = data;
                    if ($scope.LoggedinEmployee.RoleID != 0) {
                        switch ($scope.LoggedinEmployee.RoleID) {
                            case 1:
                                $scope.GoToPayPlanDetails();
                                break;
                            case 2:
                                $scope.GoToCommissionslipDetails();
                                break;
                            case 3:
                                $scope.GoToCommissionslipDetails();
                                break;
                            case 4:
                                $scope.GoToCommissionslipDetails();
                                break;
                            case 5:
                                $scope.GoToCommissionslipDetails();
                                break;
                            case 6:
                                $scope.GoToCommissionslipDetails();
                                break;
                            default:
                                $scope.GoToPayPlanDetails();
                        }
                    }
                });
            };
            //To display Branch details in grid view
            $scope.GoToBranchDetails = function () {
                debugger;
                HeaderFactory.SetTitle("Branch Details");
                $scope.active = "active";
                $scope.page = $scope.pages.BranchDetails;
                $scope.SelectedTab = 'branch';
                $scope.NewBranchRow = [];
                _this.SalesComService.GetBranches().then(function (data) {
                    $scope.NewBranchRow = data;
                });
            };
            $scope.AddBranchRow = function () {
                debugger;
                $scope.Branch = new mtisalescom.Branches();
                $scope.Branch.IsActive = true;
                //$scope.NewBranchRow.push(new Branches);
                $scope.NewBranchRow.unshift($scope.Branch);
                // angular.element('#BranchName_' + ($scope.NewBranchRow.length-1)).focus();
            };
            $scope.SetBranch = function () {
                _this.SalesComService.SetBranch($scope.NewBranchRow).then(function (data) {
                    if (data == true) {
                        $.bootstrapGrowl("Branch details saved successfully.", { type: 'success' });
                    }
                    $scope.GoToBranchDetails();
                });
            };
            //To display PayrollConfiguration
            $scope.GoToPayrollConfigurationDetails = function () {
                HeaderFactory.SetTitle("Payroll Configuration");
                $scope.page = $scope.pages.PayrollConfigurationDetails;
                $scope.SelectedTab = 'payrollconfiguration';
                $scope.PayrollDS = [];
                _this.SalesComService.GetPayrollForDashBoard().then(function (data) {
                    debugger;
                    $scope.PayrollDS = data;
                });
            };
            //To display Add PayrollConfiguration
            $scope.GoToAddPayrollConfiguration = function () {
                $scope.Payrollconfigrow = [];
                $scope.TempPayroll = new mtisalescom.Payroll;
                $scope.TempPayroll.CreatedBy = $scope.LoggedinEmployee.UID;
                HeaderFactory.SetTitle("Add Payroll Configuration");
                $scope.page = $scope.pages.AddPayrollConfiguration;
                $scope.SelectedTab = 'payrollconfiguration';
                $scope.Payrollconfigrow.push($scope.TempPayroll);
                $scope.Reportdropdownyear = [];
                var currentdate = new Date();
                var currentyear = currentdate.getFullYear();
                var nextyear = currentyear + 1;
                var prevyear = currentyear - 1;
                $scope.Reportdropdownyear = [{ value: prevyear - 1, name: prevyear - 1 }, { value: currentyear - 1, name: currentyear - 1 }, { value: currentyear, name: currentyear }, { value: nextyear, name: nextyear }, { value: nextyear + 1, name: nextyear + 1 }];
            };
            $scope.AddPayroll = function () {
                $.each($scope.Payrollconfigrow, function (key, value) {
                    value.Year = $scope.TempPayroll.Year;
                });
                _this.SalesComService.AddPayroll($scope.Payrollconfigrow).then(function (data) {
                    if (data == true) {
                        $.bootstrapGrowl("Accounting period details added successfully.", { type: 'success' });
                    }
                    $scope.GoToPayrollConfigurationDetails();
                });
            };
            //To display Edit PayrollConfiguration
            $scope.GoToEditPayrollConfiguration = function (year) {
                $scope.EditPayroll = [];
                $scope.TempPayroll = new mtisalescom.Payroll();
                HeaderFactory.SetTitle("Edit Payroll Configuration");
                $scope.page = $scope.pages.EditPayrollConfiguration;
                $scope.SelectedTab = 'payrollconfiguration';
                var currentdate = new Date();
                var currentyear = currentdate.getFullYear();
                var nextyear = currentyear + 1;
                var prevyear = currentyear - 1;
                $scope.Reportdropdownyear = [{ value: prevyear - 1, name: prevyear - 1 }, { value: currentyear - 1, name: currentyear - 1 }, { value: currentyear, name: currentyear }, { value: nextyear, name: nextyear }, { value: nextyear + 1, name: nextyear + 1 }];
                _this.SalesComService.GetPayrollForEdit(year).then(function (data) {
                    $scope.EditPayroll = data;
                    $scope.TempPayroll.Year = $scope.EditPayroll[0].Year;
                    $scope.TempPayroll.ProcessByPayroll = $scope.EditPayroll[0].ProcessByPayroll;
                    $scope.TempPayroll.CreatedByName = $scope.EditPayroll[0].CreatedByName;
                    if ($scope.EditPayroll[0].ModifiedByName == "") {
                        $scope.TempPayroll.ModifiedByName = $scope.EditPayroll[0].CreatedByName;
                    }
                    else {
                        $scope.TempPayroll.ModifiedByName = $scope.EditPayroll[0].ModifiedByName;
                    }
                    //$.each($scope.EditPayroll, function (key, value) {
                    //    //alert(value.DateTo.getDay());
                    //    value.Period = 1;
                    //    value.Day = 10;
                    //});   
                });
                $scope.NewPayrollconfigrow = new Array();
            };
            $scope.UpdatePayroll = function () {
                //To set modified ID
                $.each($scope.EditPayroll, function (key, value) {
                    value.ModifiedBy = $scope.LoggedinEmployee.UID;
                });
                $.each($scope.EditPayroll, function (key, value) {
                    value.IsActive = true;
                    $scope.NewPayrollconfigrow.push(value);
                });
                $.each($scope.NewPayrollconfigrow, function (key, value) {
                    value.Year = $scope.TempPayroll.Year;
                });
                _this.SalesComService.UpdatePayroll($scope.NewPayrollconfigrow).then(function (data) {
                    if (data == true) {
                        $.bootstrapGrowl("Payroll updated successfully.", { type: 'success' });
                    }
                    $scope.GoToPayrollConfigurationDetails();
                });
            };
            //To display Edit PayrollConfiguration
            $scope.GoToViewPayrollConfiguration = function (year) {
                $scope.ViewPayroll = [];
                $scope.TempPayroll = new mtisalescom.Payroll();
                HeaderFactory.SetTitle("View Payroll Configuration");
                $scope.page = $scope.pages.ViewPayrollConfiguration;
                $scope.SelectedTab = 'payrollconfiguration';
                _this.SalesComService.GetPayrollForEdit(year).then(function (data) {
                    $scope.ViewPayroll = data;
                    $scope.TempPayroll.Year = $scope.ViewPayroll[0].Year;
                    $scope.TempPayroll.CreatedByName = $scope.ViewPayroll[0].CreatedByName;
                    $scope.TempPayroll.ProcessByPayroll = $scope.ViewPayroll[0].ProcessByPayroll;
                    if ($scope.ViewPayroll[0].ModifiedByName == "") {
                        $scope.TempPayroll.ModifiedByName = $scope.ViewPayroll[0].CreatedByName;
                    }
                    else {
                        $scope.TempPayroll.ModifiedByName = $scope.ViewPayroll[0].ModifiedByName;
                    }
                });
            };
            //To display Sale Type details in grid view
            $scope.GoToSaleTypeDetails = function () {
                HeaderFactory.SetTitle("Sale Type Details");
                $scope.page = $scope.pages.SaleTypeDetails;
                $scope.SelectedTab = 'saletype';
                $scope.SaleTypeList = [];
                $scope.GetSaletypelist();
                //this.SalesComService.GetAllSaleType().then((data: ng.IHttpPromiseCallbackArg<SaleType[]>): void => {
                //    $scope.SaleTypeList = <SaleType[]>data;
                //});
            };
            $scope.AddSaleTypeRow = function () {
                $scope.Sale = new mtisalescom.SaleType();
                $scope.Sale.IsActive = true;
                //$scope.NewBranchRow.push(new Branches);
                $scope.SaleTypeList.unshift($scope.Sale);
            };
            $scope.SetSaleType = function () {
                debugger;
                _this.SalesComService.SetSaleType($scope.SaleTypeList).then(function (data) {
                    if (data == true) {
                        $.bootstrapGrowl("SaleType details saved successfully.", { type: 'success' });
                    }
                    $scope.GoToSaleTypeDetails();
                });
            };
            $scope.ModalDeactivatedBranch = function (branchid, index) {
                debugger;
                _this.SalesComService.GetDeActivateBranch(branchid).then(function (data) {
                    if (data == true) {
                        $scope.ShowAlertDialog("Location Deactivate Alert", "This location has associated commission slips for the current accounting period. Until Paylocity and General Ledger report generated, deactivation cannot occur", function () { });
                    }
                    else {
                        $scope.ShowAYSDialog("Location Deactivate Alert", "This Location will not be available for Commission Slip Entry and Report generation. You want to De Activate?", function () {
                            angular.element('#branch' + branchid).removeClass("active").addClass("deactive");
                            $scope.NewBranchRow[index].IsActive = false;
                        });
                    }
                });
            };
            $scope.ShowAlertDialog = function (title, message, callback) {
                var modalInstance = $uibModal.open({
                    templateUrl: 'App/views/m/ModalAlert.html',
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
                    debugger;
                    if (success) {
                        callback();
                    }
                }, function () {
                });
            };
            $scope.ValidateBranch = function (branchName, index) {
                debugger;
                for (var i = 0; i < $scope.NewBranchRow.length; i++) {
                    if (i != index && ($scope.NewBranchRow[i].BranchName.toString().toUpperCase()) == branchName.toString().toUpperCase()) {
                        $scope.NewBranchRow[index].BranchName = '';
                        $.bootstrapGrowl('Location Name already exist', { type: "danger" });
                    }
                }
            };
            $scope.ModalDeactivatedSaletype = function (id, index) {
                debugger;
                $scope.checkSaleType = new mtisalescom.CheckSaleType();
                $scope.checkSaleType.ID = id;
                $scope.checkSaleType.reason = 0;
                _this.SalesComService.GetDeActivateSaletype($scope.checkSaleType).then(function (data) {
                    if (data == true) {
                        $scope.ShowAlertDialog("Saletype Deactivate Alert", "This Saletype has associated commission slips for the current accounting period. Until Paylocity and General Ledger report generated, deactivation cannot occur", function () { $scope.active = 'deactive'; });
                    }
                    else {
                        $scope.ShowAYSDialog("Saletype Deactivate Alert", "All the commission slips mapped to the Sale Type will not be considered for Bonus Calculations.You want to De Activate?", function () {
                            angular.element('#saletype' + id).removeClass("active").addClass("deactive");
                            debugger;
                            $scope.SaleTypeList[index].IsActive = false;
                        });
                    }
                });
            };
            $scope.ValidateSaletype = function (saletypeName, index) {
                debugger;
                for (var i = 0; i < $scope.SaleTypeList.length; i++) {
                    if (i != index && ($scope.SaleTypeList[i].SaleTypeName.toString().toUpperCase()) == saletypeName.toString().toUpperCase()) {
                        $scope.SaleTypeList[index].SaleTypeName = '';
                        $.bootstrapGrowl('Sale Type Name already exist', { type: "danger" });
                    }
                }
            };
            $scope.IsNewCustomerClick = function (saleType, index) {
                debugger;
                if (saleType.IsActive == true && saleType.IsNewCustomer == false) {
                    $scope.checkSaleType = new mtisalescom.CheckSaleType();
                    $scope.checkSaleType.ID = saleType.ID;
                    $scope.checkSaleType.reason = 1;
                    _this.SalesComService.GetDeActivateSaletype($scope.checkSaleType).then(function (data) {
                        if (data == true) {
                            $scope.ShowAlertDialog("Saletype Deactivate Alert", "This Saletype has associated commission slips for the current accounting period. Until Paylocity and General Ledger report generated, uncheck cannot occur", function () {
                                debugger;
                                $scope.SaleTypeList[index].IsNewCustomer = true;
                            });
                        }
                        else {
                            $scope.SaleTypeList[index].IsNewCustomer = false;
                        }
                    });
                }
            };
            $scope.IsExistingCustomerClick = function (saleType, index) {
                debugger;
                if (saleType.IsActive == true && saleType.IsExistingCustomer == false) {
                    $scope.checkSaleType = new mtisalescom.CheckSaleType();
                    $scope.checkSaleType.ID = saleType.ID;
                    $scope.checkSaleType.reason = 2;
                    _this.SalesComService.GetDeActivateSaletype($scope.checkSaleType).then(function (data) {
                        if (data == true) {
                            $scope.ShowAlertDialog("Saletype Deactivate Alert", "This Saletype has associated commission slips for the current accounting period. Until Paylocity and General Ledger report generated, uncheck cannot occur", function () {
                                debugger;
                                $scope.SaleTypeList[index].IsExistingCustomer = true;
                            });
                        }
                        else {
                            $scope.SaleTypeList[index].IsExistingCustomer = false;
                        }
                    });
                }
            };
            $scope.NewPayrollCancelConfirmation = function () {
                $scope.ActivationMessage = "Cancel";
                $scope.ShowAYSDialog("Cancel Payroll Detail", " Are you sure to cancel?", function () { $scope.GoToPayrollConfigurationDetails(); });
            };
        }
        SalesCommissionControl.$inject = ['$location', '$rootScope', '$scope', '$q', '$http', '$routeParams', 'mtisalescom.SalesComService', 'InitializeService', '$uibModal', 'HeaderFactoryClass', '$compile', 'deviceDetector', '$templateCache', '$timeout', '$route', '$interval', '$cookies', '$filter'];
        return SalesCommissionControl;
    }());
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Define Sales Commission Yes/No Dialog Controller                                                      //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    var AYSDialogController = (function () {
        function AYSDialogController($scope, $uibModalInstance, title, message) {
            this.$scope = $scope;
            this.$uibModalInstance = $uibModalInstance;
            $scope.title = title;
            $scope.message = message;
            $scope.ok = function () {
                $uibModalInstance.close('ok');
            };
            $scope.cancel = function () {
                $uibModalInstance.close(false);
                debugger;
                //$uibModalInstance.dismiss('cancel');
            };
        }
        AYSDialogController.$inject = ['$scope', '$uibModalInstance', 'title', 'message'];
        return AYSDialogController;
    }());
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Define Sales Commission Pay plan Dialog Controller                                                      //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    var PayPlanDialogController = (function () {
        function PayPlanDialogController($scope, $uibModalInstance, title) {
            this.$scope = $scope;
            this.$uibModalInstance = $uibModalInstance;
            $scope.title = title;
            $scope.close = function () {
                $uibModalInstance.close(false);
            };
        }
        PayPlanDialogController.$inject = ['$scope', '$uibModalInstance', 'title'];
        return PayPlanDialogController;
    }());
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Define  Report Edit commission slip Controller                                                      //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    var ReportEditCommissionController = (function () {
        function ReportEditCommissionController($scope, $uibModalInstance, title) {
            this.$scope = $scope;
            this.$uibModalInstance = $uibModalInstance;
            $scope.title = title;
            $scope.save = function () {
                $uibModalInstance.close('ok');
            };
            $scope.cancel = function () {
                $uibModalInstance.close(false);
            };
        }
        ReportEditCommissionController.$inject = ['$scope', '$uibModalInstance', 'title'];
        return ReportEditCommissionController;
    }());
    angular
        .module('mtisalescom')
        .config(Config)
        .controller('AYSDialogController', AYSDialogController)
        .controller('PayPlanDialogController', PayPlanDialogController)
        .controller('SalesCommissionControl', SalesCommissionControl)
        .controller('ReportEditCommissionController', ReportEditCommissionController);
})(mtisalescom || (mtisalescom = {}));
//# sourceMappingURL=E:/SVN/MilnerSalesCommission/Source/Application/SalesCom//Scripts/TSBuild/controller/app.salescom.controller.js.map