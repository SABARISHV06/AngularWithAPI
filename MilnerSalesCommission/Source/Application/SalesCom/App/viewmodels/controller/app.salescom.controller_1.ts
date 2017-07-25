// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//
/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />

interface JQueryStatic {
    bootstrapGrowl: any;
}

interface Event {
    value: any;
}

interface JQuery {
    modal: any;
    datepicket: any;
    setSelection(selectionStart: any, selectionEnd: any): any;
    getCursorPosition($event: any): any;
    setCursorPosition(position: any): any;
}

declare function preurl(url: string): string;
declare function posturl(url: string): string;
declare function moment(time: any): any;
declare function moment(): any;
declare function GetAppVersion(): string;
declare function sprintf(string, any): string;

module mtisalescom {
    'use strict';

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Define Route Configuration                                                                           //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    export class Config {

        static $inject = ['$routeProvider', '$httpProvider'];

        constructor($routeProvider: ng.route.IRouteProvider, $httpProvider: ng.IHttpProvider) {
            
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

    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Define sales Commission Interfaces Scope                                                              //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    export interface ISalesCommissionScope extends ng.IScope {
       
        Header: HeaderFactoryClass;
        page: string;
        Iseditable: boolean;
        Logoff(): void;
        pages: { /*ManageNewPlan: string; ManageEmployee: string; ManageViewAccess: string;*/
            AddPayPlan: string; EditPayPlan: string; PayPlanDetails: string; ViewPayPlan: string;
            AddEmployee: string; EditEmployee: string; EmployeeDetails: string; ViewEmployee: string;
            AddCommissionslip: string; EditCommissionslip: string; CommissionslipDetails: string; ViewCommissionslip: string; ResubmitCommissionslip: string;
            SalesRepReports: string; PaylocityGlReports: string;
            BranchDetails: string; SaleTypeDetails: string;
            PayrollConfigurationDetails: string; ViewPayrollConfiguration: string; EditPayrollConfiguration: string; AddPayrollConfiguration: string;
        };

        editing: boolean;
        editDirty: boolean;
        IsCurrentPage(page: string): boolean;
        ready: boolean;
        initialized: boolean;
        model: any;
        name: string;
        id: number;
        SelectedTab: string;

        currentDt: any;
        currentMnth: any;
        mm: any;
        dd: number;
        yyyy: number;
        currentDtTime: any;

        ok(): void;
        cancel(): void;
        save(): void;
        clear(): void;
        title: string;
        close(): void;
        message: string;
        ShowAYSDialog(title: string, message: string, callback: any): void;
        ShowAlertDialog(title: string, message: string, callback: any): void;
        ShowPayPlan(title: string): void;
        ModalViewPayPlan(planID: number): void;
        ModalDeactivatedPayPlan(): void;
        ShowDeactivatedPayPlan(title: string): void;

        navClass(page: number): string;
        navCollapsed: boolean;
        navCollapseTitle: string;
        toggleSidebarState(): void;

        GoToPayPlanDetails(): void;
        GoToAddPayPlan(): void;
        GoToEditPayPlan(plan: Plancomponent): void;
        GoToViewPayPlan(plan: Plancomponent): void;
        GoToActivateDeactivatePayPlan(plan: Plancomponent): void;
        PlanActivateDeactivateConfirmation(plan: Plancomponent): void;
        NewEmployeeCancelConfirmation(): void;
        NewPayrollCancelConfirmation(): void;
        NewPayPlanCancelConfirmation(): void;
        RolesType: any;
        SlipsType: any;
        GoToEmployeeDetails(): void;
        GoToAddEmployee(): void;
        CreateEmployeeConfirmation(): void;
        CreateEmployee(): void;
        EditEmployee(): void;
        GoToEditEmployee(emp: EmployeeComponant): void;
        GoToViewEmployee(emp: EmployeeComponant): void;
        ActivateDeactivateConfirmation(emp: EmployeeComponant): void;
        GoToActivateDeactivateEmployee(emp: EmployeeComponant): void;
        DeActivatePlanEmployees: EmployeeComponant;

        GoToCommissionslipDetails(): void;
        GoToCommissionslipDetailsConfirmation(): void;
        GoToAddCommissionslip(): void;
        GoToEditCommissionslip(commissionid: number): void;
        GoToResubmitCommissionslip(commissionid: number): void;
        GoToViewCommissionslip(commissionid: number, edit: boolean): void;
        onReportYearChange(year: number): void;
        //onReportPeriodChange(): void;
        PeriodDetail: any;
        GoToSalesRepReports(): void;
        GoToPaylocityGlReports(): void;

        GoToBranchDetails(): void;
        AddBranchRow(): void;
        NewBranchRow: Branches[];
        SetBranch(): void;
        Branch: Branches;

        GoToSaleTypeDetails(): void;
        AddSaleTypeRow(): void;
        SaleTypeList: SaleType[];
        SetSaleType(): void;
        Sale: SaleType;

        GoToPayrollConfigurationDetails(): void;
        GoToAddPayrollConfiguration(): void;
        GoToEditPayrollConfiguration(year: number): void;
        GoToViewPayrollConfiguration(year: number): void;
        

        AddCommission(status: number): Boolean;
        EditCommission(status: number): Boolean;
        EditTipLeadslip(status: number): Boolean;
        ResubmitCommission(status: number): Boolean;

        Planlists: Plancomponent[];
        Plandetail: Plancomponent;
        TGPlist: TGPCustomerInfo[];
        Bimonthlylist: BIMonthlyBonusInfo[];
        Tenurelist: QuarterlyTenureBonus[];
       

        Roleslists: Roles[];
        Brancheslists: Branches[];
        SecondaryBrancheslists: Branches[];
        SecondaryBranchlist: any[];
        Managerslists: EmployeeComponant[];
        SalesManagerslists: EmployeeComponant[];
        GeneralManagerslists: EmployeeComponant[];
        ViewRolesItems: any;
        GetRoleslist(): void;
        GetBranchlist(): void;
        GetSaletypelist(): void;
        GetAllManagerlist(): void;
        GetPlanlist(): void;
        CheckUserLogin(): void;
        LoggedinEmployee: EmployeeComponant;
        Employees: EmployeeComponant[];
        GetEmployeebyUID(emp: EmployeeComponant): void;
        Employeedetail: EmployeeComponant;
        Typeonchange(): void;
        BPSalaryonclick(): void;
        BPDrawonclick(): void;
        Drawtypeonclick(): void;
        

        Saletype: any[];
        NewCustomerRow: TGPCustomerInfo[];
        AddNewCustomerRow(): void;
        RemoveNewCustomerRow(index: number): void;
        ExistingCustomerRow: TGPCustomerInfo[];
        AddExistingCustomerRow(): void;
        RemoveExistingCustomerRow(index: number): void;
        CustomerInfo: TGPCustomerInfo[];
        ActivationMessage: string;

        BimonthsOptions: string[];
        BimonthsRow: BIMonthlyBonusInfo[];
        AddBimonthsRow(index: number): void;
        RemoveBimonthsRow(index: number): void;
        TenureOptions: string[];
        TenureRow: QuarterlyTenureBonus[];
        AddTenureRow(): void;
        RemoveTenureRow(index: number): void;
        AddPlan(): void;
        EditPlan(): void;
        BimonthlyShow(): void;
        BimonthlyHide(): void;
        TenureShow(): void;
        TenureHide(): void;

        Commissionslips: CommissionComponent[];
        Commissionslip: CommissionComponent;
        Commissiondetail: CommissionComponent;
        TipLeadRow: TipLeadSlip[];
        AddTipLeadRow(): void;
        RemoveTipLeadRow(index: number): void;
        TipLeadlist: EmployeeComponant[];
        TotalCommissions(): void;
        TotalTipslip(): void;
        Productline: any[];
        DeleteCommission(Commission: CommissionComponent): void;
        DeleteMessage(Commission: CommissionComponent): void;
        SendMail(): void;
        MailContent: EmailMessage;
        MainCommissionStatus: number;

        Sort(keyname: string): void;
        onSelect($item, $model, $label): void;
        onSalespersonselect($item, $model, $label): void;
        splitchange(): void;
        onTipleadSelect($item, $model, $label, index: number): void;
        Tipleadchange(index: number): void;
        SortKey: string;
        Order: boolean;
        rolelist: Roles[];

        SelectedSalesperson: EmployeeComponant;
        Salespersonslist: EmployeeComponant[];
        SRSalespersons: EmployeeComponant[];
        splitnamelist: string[];

        ViewPaylocityGLReport(): void;
        Reports: ReportParameters;
        PaylocityList: ReportParameters[];
        PaylocityExportList: PaylocityReports[];
        GLExportList: GLReports[];
        Userslist: any[];
        SelectedUser: any;

        Getcommissionreport(): void;
        ReportInputparameter: ReportParameters;
        SRReportTypes: any[];
        SMGMReportTypes: any[];
        ReportType: number;
        CommissionReportDetails: ReportParameters[];
        DrawReportDetails: ReportParameters;
        TotalReportValue: ReportParameters;
        parameter: number[];
        Empdateinposition: any;
        ReportError: any;
        Reportdropdownyear: any[];
        ReportdropdownConfigyear: Payroll[];
        Commissionreportperiod: any;
        TotalearningFromperiod: any;
        SelectedYear: number;
        TotalearningToperiod: any;
        EmployeeBranch: Branches[];
        ReportTotalEarnings: ReportParameters[];
        ReportPeriodNote: any;
        ShowEditReportCommission(title: string, callback: any): void;
        ModalEditReportCommission(commissionID: number): void;
        EditReportCommission(): Boolean;
        Entrycheck(index: number): void;
        SetDateTo(index: number): void;
        SetEditDateTo(index: number): void;
        UpdateNumberOfDays(index: number): void;

        Entrypointvalidation(index: number): Boolean;
        Entrypointscheck: boolean[];
        Requiredfield(index: number): Boolean;
        SaveReportConfirmation(ReportInputparameter: ReportParameters): void;
        SaveReport(ReportInputparameter: ReportParameters): void;
        GetCountforPaylocity(): void;
        Pendingcount: number;
        PendingStatus: number;
        pageSize: number;
        MailUrl: any;
        IsGMDeactivated: boolean;
        ISPlanDeactivated: boolean;

        ModalDeactivatedBranch(branchid: number, index: number): void;
        DeActivateBranch: Branches;
      //  active: any;
        ValidateBranch(branchName: string, index: number): void;
        brancherror: any;
        ModalDeactivatedSaletype(id: number, index: number): void;
        ValidateSaletype(saletypeName: string, index: number): void;
        active: string;
        IsNewCustomerClick(saleType: SaleType, index: number): void;
        IsExistingCustomerClick(saleType: SaleType, index: number): void;
        checkSaleType: CheckSaleType;
        PayrollDS: Payroll[];
        ViewPayroll: Payroll[];
        EditPayroll: Payroll[];
        AddPayroll(): void;
        UpdatePayroll(): void;
        AccountPayroll: Payroll;
        GetAccountingMonth(currentDate: Date): void;
        AddNewPayrollConfigRow(): void;
        EditNewPayrollConfigRow(): void;
        RemoveNewPayrollConfigRow(index: number): void;
        RemoveEditNewPayrollConfigRow(index: number): void;
        Payrollconfigrow: Payroll[];
        TempPayroll: Payroll;
        PlusPayroll: Payroll;
        buildDateTo: any;
		DaysArr: any[];
        Days: any[];
        MonthsArr: any[];
        Months: any[];
        NumberOfDays: any[];
    }



    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Define Sales Commission Controller                                                              //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    class SalesCommissionControl {

        static $inject = ['$location', '$rootScope', '$scope', '$q', '$http', '$routeParams', 'mtisalescom.SalesComService', 'InitializeService', '$uibModal', 'HeaderFactoryClass', '$compile', 'deviceDetector', '$templateCache', '$timeout', '$route', '$interval', '$cookies', '$filter'];

        constructor(private $location: ng.ILocationService, $rootScope, public $scope: ISalesCommissionScope, private $q: ng.IQService, public $http: ng.IHttpService, $routeParams,
            public SalesComService: SalesComService, private InitializeService: InitializeService,
            private $uibModal: ng.ui.bootstrap.IModalService, public HeaderFactory: HeaderFactoryClass,
            $compile, deviceDetector, $templateCache, $timeout, $route, $interval, $cookies, $filter) {


            var mainController = angular.module('mtisalescom', ['ngRoute', 'ngResource', 'ui.bootstrap', 'ngCookies', 'mgcrea.ngStrap.datepicker',
                'mgcrea.ngStrap.timepicker', 'mgcrea.ngStrap.tooltip', 'mgcrea.ngStrap.helpers.parseOptions', 'mgcrea.ngStrap.select', 'textAngular',
                'angular-sortable-view', 'angularUtils.directives.dirPagination', 'ngAnimate', 'ngTouch',
                'ui.mask', 'ui.utils', 'ng-currency', 'ng.deviceDetector', '$uibModalInstance','isteven-multi-select'])


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
            
            $scope.IsCurrentPage = (page: string): boolean => {
                return ($scope.page === page);
            }
            $scope.Sort = function (keyname) {
                $scope.SortKey = keyname;   //set the sortKey to the param passed
                $scope.Order = !$scope.Order; //if true make it false and vice versa
            }

            $scope.onSelect = function ($item, $model, $label) {
                $scope.Commissionslip.SplitSalePerson = $label;
                $scope.Commissionslip.SplitSalePersonID = $item.EmployeeID;
            }
            $scope.splitchange = (): void => {
                $scope.Commissionslip.SplitSalePerson = "";
                $scope.Commissionslip.SplitSalePersonID = "";
            }
            $scope.onTipleadSelect = function ($item, $model, $label,index) {
                $scope.TipLeadRow[index].TipLeadName = $label;
                $scope.TipLeadRow[index].TipLeadEmpID = $item.EmployeeID;
                $scope.TipLeadRow[index].TipLeadID = $item.UID;
            }
            $scope.Tipleadchange = (index): void => {
                $scope.TipLeadRow[index].TipLeadName = "";
                $scope.TipLeadRow[index].TipLeadEmpID = "";
                $scope.TipLeadRow[index].TipLeadID = 0;
            }
            //$scope.Saletype = [{ value: 1, name: "Cash" }, { value: 2, name: "Lease" }, { value: 3, name: "Rent" }, { value: 4, name: "Lease renewal" }];
            $scope.currentDt = new Date(); //Get Current Date
            $scope.mm = $scope.currentDt.getMonth() + 1;
            $scope.mm = ($scope.mm < 10) ? '0' + $scope.mm : $scope.mm;
            $scope.dd = $scope.currentDt.getDate();
            $scope.yyyy = $scope.currentDt.getFullYear();
            $scope.currentDtTime = $filter('date')(new Date(), 'yyyy-MM-dd HH:mm:ss Z');
            $scope.currentDt = $scope.mm + '/' + $scope.dd + '/' + $scope.yyyy;
            $scope.currentMnth = $scope.mm + '/' + $scope.yyyy;

            $scope.RolesType = RoleTypes;
            $scope.SlipsType = SlipTypes;
            $scope.Productline = [{ value: 1, name: "Copy" }, { value: 2, name: "Milner Software" }, { value: 3, name: "Third Party Software" }, { value: 4, name: "IT Services" }, { value: 5, name: "Other Third Party Products" }];
            $scope.BimonthsOptions = ["1-12", "13+"];
            $scope.TenureOptions = ["13-24", "25-36", "37+"];

            $scope.NewCustomerRow = [];
            $scope.AddNewCustomerRow = function () {
                
                $scope.NewCustomerRow.push(new TGPCustomerInfo);
            }
            $scope.RemoveNewCustomerRow = function (index) {
                $scope.ShowAYSDialog("Are You Sure?", "Do you really want to delete the line Item?", function () { $scope.NewCustomerRow.splice(index, 1); });

            }
            $scope.ExistingCustomerRow = [];
            $scope.AddExistingCustomerRow = function () {
                
                $scope.ExistingCustomerRow.push(new TGPCustomerInfo);
            }
            $scope.RemoveExistingCustomerRow = function (index) {
                $scope.ShowAYSDialog("Are You Sure?", "Do you really want to delete the line Item?", function () {
                    $scope.ExistingCustomerRow.splice(index, 1);
                });
            }
            $scope.Payrollconfigrow = [];
            $scope.AddNewPayrollConfigRow = function () {
                $scope.PlusPayroll = new Payroll();
                $scope.PlusPayroll.CreatedBy = $scope.LoggedinEmployee.UID;
                $scope.Payrollconfigrow.push($scope.PlusPayroll);
            }
            $scope.EditNewPayrollConfigRow = function () {
                $scope.PlusPayroll = new Payroll();
                $scope.PlusPayroll.CreatedBy = $scope.LoggedinEmployee.UID;
                $scope.EditPayroll.push($scope.PlusPayroll);
            }
            $scope.RemoveNewPayrollConfigRow = function (index) {
                $scope.ShowAYSDialog("Are You Sure?", "Do you really want to delete the line Item?", function () {
                    $scope.Payrollconfigrow.splice(index, 1);
                });
            }
            $scope.RemoveEditNewPayrollConfigRow = function (index) {
                $scope.ShowAYSDialog("Are You Sure?", "Do you really want to delete the line Item?", function () {
                    $scope.EditPayroll.splice(index, 1);
                });
            }

            $scope.BimonthsRow = [];
            $scope.AddBimonthsRow = function (index) {
                if ($scope.BimonthsRow[index].EntryPointB == undefined || String($scope.BimonthsRow[index].EntryPointB) =="") {
                    $scope.Requiredfield = function (index) {
                        return true;
                    }
                } else {
                    $scope.BimonthsRow.push(new BIMonthlyBonusInfo)
                    $scope.Requiredfield = function (index) {
                        return false;
                    }
                }
            }
            $scope.RemoveBimonthsRow = function (index) {
                $scope.ShowAYSDialog("Are You Sure?", "Do you really want to delete the line Item?", function () {
                    $scope.BimonthsRow.splice(index, 1);
                    $scope.Requiredfield = function (index) {
                        return false;
                        }
                });
            }


            $scope.TenureRow = [];
            $scope.AddTenureRow = function () {
                
                $scope.TenureRow.push(new QuarterlyTenureBonus);
            }
            $scope.RemoveTenureRow = function (index) {
                $scope.ShowAYSDialog("Are You Sure?", "Do you really want to delete the line Item?", function () {
                    $scope.TenureRow.splice(index, 1);
                });
            }
            $scope.BimonthlyShow = (): void => {
                if ($scope.BimonthsRow == [] || $scope.BimonthsRow.length == 0) {
                    $scope.BimonthsRow.push(new BIMonthlyBonusInfo);
                }
            }
            $scope.BimonthlyHide = (): void => {
                $scope.Plandetail.BMQuotaBonus = true;
                $scope.ShowAYSDialog("Are You Sure?", "All the values given will be erased, you sure to proceed?", function () {
                    $scope.Plandetail.BMQuotaBonus = false;
                    $scope.BimonthsRow = [];
                });
            }
            $scope.TenureShow = (): void => {
                if ($scope.TenureRow == [] || $scope.TenureRow.length == 0) {
                    $scope.TenureRow.push(new QuarterlyTenureBonus);
                }
            }
            $scope.TenureHide = (): void => {
                $scope.Plandetail.TenureBonus = true;
                $scope.ShowAYSDialog("Are You Sure?", "All the values given will be erased, you sure to proceed?", function () {
                    $scope.Plandetail.TenureBonus = false;
                    $scope.TenureRow = [];
                });
            }
            $scope.GoToAddPayPlan = (): void => {
                
                HeaderFactory.SetTitle("Add PayPlan");
                $scope.SelectedTab = 'payplan';
                $scope.page = $scope.pages.AddPayPlan;
                $scope.NewCustomerRow = [];
                $scope.ExistingCustomerRow = [];
                $scope.BimonthsRow = [];
                $scope.TenureRow = [];
                $scope.Plandetail = new Plancomponent;
                $scope.NewCustomerRow.push(new TGPCustomerInfo);
                $scope.ExistingCustomerRow.push(new TGPCustomerInfo);
                $scope.GetSaletypelist();
                $scope.Plandetail.BasisType = 1;// Default value of Basis Type is Doller Volume
            }
            $scope.Entrycheck = (index): void => {
                $scope.Entrypointscheck = [];
                $.each($scope.BimonthsRow, function (key, value) {
                    var A: number = parseInt(value.EntryPointA.toString());
                    var B: number = parseInt(value.EntryPointB.toString());
                    if (A > B || A == B) {
                        $scope.Entrypointscheck[key] = true;
                    }
                    else {
                        $scope.Entrypointscheck[key] = false;
                    }
                });
            }
			
			$scope.NumberOfDays = [];
            $scope.UpdateNumberOfDays = (index): void => {
                var isLeapYear = function () {
                    var year = $scope.TempPayroll.Year || 0;
                    return ((year % 400 === 0 || year % 100 !== 0) && (year % 4 === 0)) ? 1 : 0;
                }

                var selectedMonth = $scope.Payrollconfigrow[index].Period || $scope.EditPayroll[index].Period || 0;
                selectedMonth = parseInt(selectedMonth.toString());
                $scope.NumberOfDays[index] = 31 - ((selectedMonth === 2) ? (3 - isLeapYear()) : ((selectedMonth - 1) % 7 % 2));
                //alert("NO OF DAYs=" + noofdays);
                // $scope.NumberOfDays[index]
              
            }
			
			
            $scope.SetDateTo = (index): void => {
                var currPeriod = $scope.Payrollconfigrow[index].Period;
                var isexist = false;
                var CurrDay: string;
                var CurrDayNum: number;
                $scope.Entrypointscheck = [];
                $.each($scope.Payrollconfigrow, function (key, value) {
                    if (value.Period == currPeriod && key != index) {
                        isexist = true;
                        CurrDayNum = parseInt($scope.Payrollconfigrow[index-1].Day.toString()) + 1;
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

                if ($scope.Payrollconfigrow[index].Day) $scope.Payrollconfigrow[index].DateTo = $scope.Payrollconfigrow[index].Period + '/' + $scope.Payrollconfigrow[index].Day;

            }

            $scope.SetEditDateTo = (index): void => {
                var currPeriod = $scope.EditPayroll[index].Period;
                var isexist = false;
                var CurrDay: string;
                var CurrDayNum: number;
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

                if ($scope.EditPayroll[index].Day) $scope.EditPayroll[index].DateTo = $scope.EditPayroll[index].Period + '/' + $scope.EditPayroll[index].Day;

            }
            $scope.AddPlan = (): void => {
                
                $scope.CustomerInfo = [];
                $scope.Plandetail.CreatedBy = $scope.LoggedinEmployee.UID;
                $scope.Plandetail.IsActive = true;
                $.each($scope.NewCustomerRow, function (key, value) {
                    value.CustomerType = CustomerTypes.NewCustomer;
                });
                $.each($scope.ExistingCustomerRow, function (key, value) {
                    value.CustomerType = CustomerTypes.ExistingCustomer;
                });
                $scope.CustomerInfo = $scope.NewCustomerRow.concat($scope.ExistingCustomerRow);
                $scope.Plandetail.TGPcustomerlist = $scope.CustomerInfo;
                $scope.Plandetail.Bimonthlylist = $scope.BimonthsRow;
                $scope.Plandetail.TenureBonuslist = $scope.TenureRow;
                $.each($scope.Plandetail.TenureBonuslist, function (key, value) {
                    value.EntryPointB = 0;
                });
                this.SalesComService.AddPlan($scope.Plandetail).then((data: ng.IHttpPromiseCallbackArg<Number>): void => {
                    if (<Number>data != 0) {
                        $.bootstrapGrowl("Plan details added successfully.", { type: 'success' });
                        $scope.GoToPayPlanDetails();
                    }
                });
            }

            $scope.GoToEditPayPlan = (plan): void => {
                
                HeaderFactory.SetTitle("Edit PayPlan");
                $scope.page = $scope.pages.EditPayPlan;
                $scope.SelectedTab = 'payplan';
                $scope.Plandetail = new Plancomponent;
                $scope.NewCustomerRow = [];
                $scope.ExistingCustomerRow = [];
                $scope.BimonthsRow = [];
                $scope.TenureRow = [];
                this.SalesComService.GetPlanbyID(plan).then((data: ng.IHttpPromiseCallbackArg<Plancomponent>): void => {
                    debugger;
                    $scope.Plandetail = <Plancomponent>data;
                    $scope.BimonthsRow = $scope.Plandetail.Bimonthlylist;
                    $scope.TenureRow = $scope.Plandetail.TenureBonuslist;
                    $scope.NewCustomerRow = $scope.Plandetail.TGPcustomerlist.filter(function (Tgp) {
                        return (Tgp.CustomerType == CustomerTypes.NewCustomer);
                    });
                    $scope.ExistingCustomerRow = $scope.Plandetail.TGPcustomerlist.filter(function (Tgp) {
                        return (Tgp.CustomerType == CustomerTypes.ExistingCustomer);
                    });
                    debugger;
                    if ($scope.NewCustomerRow.length == 0) {
                        $scope.NewCustomerRow.push(new TGPCustomerInfo);
                    }
                    if ($scope.ExistingCustomerRow.length == 0) {
                        $scope.ExistingCustomerRow.push(new TGPCustomerInfo);
                    }
                });
                $scope.GetSaletypelist();
            }
            $scope.EditPlan = (): void => {
                
                $scope.CustomerInfo = [];
                $scope.Plandetail.ModifiedBy = $scope.LoggedinEmployee.UID;
                $scope.Plandetail.IsActive = true;
                $.each($scope.NewCustomerRow, function (key, value) {
                    value.CustomerType = CustomerTypes.NewCustomer;
                });
                $.each($scope.ExistingCustomerRow, function (key, value) {
                    value.CustomerType = CustomerTypes.ExistingCustomer;
                });

                $scope.CustomerInfo = $scope.NewCustomerRow.concat($scope.ExistingCustomerRow);
                $scope.Plandetail.TGPcustomerlist = $scope.CustomerInfo;
                $scope.Plandetail.Bimonthlylist = $scope.BimonthsRow;
                $scope.Plandetail.TenureBonuslist = $scope.TenureRow;
                $.each($scope.Plandetail.TenureBonuslist, function (key, value) {
                    value.EntryPointB = 0;
                });
                this.SalesComService.EditPlan($scope.Plandetail).then((data: ng.IHttpPromiseCallbackArg<Boolean>): void => {
                    if (<Boolean>data == true) {
                        $.bootstrapGrowl("Plan details updated successfully.", { type: 'success' });
                        $scope.GoToPayPlanDetails();
                    }
                });

            }


            //To display Pay plan details in grid view
            $scope.GoToPayPlanDetails = (): void => {

                HeaderFactory.SetTitle("Pay Plan Details");
                $scope.page = $scope.pages.PayPlanDetails;
                $scope.SelectedTab = 'payplan';
                $scope.pageSize = 10;
                this.SalesComService.GetPlans().then((data: ng.IHttpPromiseCallbackArg<Plancomponent[]>): void => {
                    $scope.Planlists = <Plancomponent[]>data;
                    $.each($scope.Planlists, function (key, value: any) {

                        value.BasisType = Basistype[value.BasisType];
                        value.BMQuotaBonus = Booleanvalues[value.BMQuotaBonus == true ? 1 : 0];
                        value.TenureBonus = Booleanvalues[value.TenureBonus == true ? 1 : 0];
                    });
                });
            }



           $scope.GoToViewPayPlan = (plan): void => {
                
                HeaderFactory.SetTitle("View PayPlan");
                $scope.page = $scope.pages.ViewPayPlan;
                $scope.SelectedTab = 'payplan';
                
                this.SalesComService.GetPlanbyID(plan).then((data: ng.IHttpPromiseCallbackArg<Plancomponent>): void => {
                    debugger;
                    $scope.Plandetail = <Plancomponent>data;
                    $scope.Plandetail.BasisType = Basistype[$scope.Plandetail.BasisType];
                    
                    $scope.Plandetail.BMQuotaBonus = Booleanvalues[$scope.Plandetail.BMQuotaBonus == true ? 1 : 0];
                    $scope.Plandetail.TenureBonus = Booleanvalues[$scope.Plandetail.TenureBonus == true ? 1 : 0];
                    $scope.Plandetail.SMEligible = Booleanvalues[$scope.Plandetail.SMEligible == true ? 1 : 0];
                });

            }

           $scope.PlanActivateDeactivateConfirmation = (plan): void => {
                if (plan.IsActive == true) {
                    $scope.ActivationMessage = "Deactivated";
                    $scope.ShowAYSDialog("Are You Sure?", "A Pay Plan will be mapped with many employees, are you sure to deactivate?", function () { $scope.GoToActivateDeactivatePayPlan(plan); });
                }
                else {
                    $scope.ActivationMessage = "Activated";
                    $scope.ShowAYSDialog("Are You Sure?", "Do you want to activate the Payplan?", function () { $scope.GoToActivateDeactivatePayPlan(plan); });
                }
            }

           $scope.NewEmployeeCancelConfirmation = (): void => {
                    $scope.ActivationMessage = "Cancel";
                    $scope.ShowAYSDialog("Cancel Employee Detail", "Are you sure, you do not want to save the Employee?", function () { $scope.GoToEmployeeDetails(); });
               
           }
           $scope.NewPayPlanCancelConfirmation = (): void => {
               $scope.ActivationMessage = "Cancel";
               $scope.ShowAYSDialog("Cancel PayPlan Detail", "Are you sure, you do not want to save the payplan?", function () { $scope.GoToPayPlanDetails(); });

           }
           $scope.GoToActivateDeactivatePayPlan = (plan): void => {
                
                HeaderFactory.SetTitle("Pay Plan Details");
                $scope.page = $scope.pages.PayPlanDetails;
                $scope.SelectedTab = 'payplan';
                plan.IsActive = (plan.IsActive == true ? false : true);
                plan.ModifiedBy = $scope.LoggedinEmployee.UID;
                this.SalesComService.ActivateDeactivatePlan(plan).then((data: ng.IHttpPromiseCallbackArg<Boolean>): void => {
                    if (<boolean>data == true) {
                        $.bootstrapGrowl("Plan " + $scope.ActivationMessage + " successfully.", { type: 'success' });
                        $scope.GoToPayPlanDetails();
                    }
                });
            }

           $scope.Typeonchange = function () {
               $scope.Employeedetail.PrimaryBranch = "";
               $scope.Employeedetail.SecondaryBranch = [];
                $scope.Employeedetail.ApproveMgr = null;
                $scope.Employeedetail.ReportMgr = null;
                $scope.Employeedetail.PayPlanID = "";
                $scope.Employeedetail.BPDraw = false;
                $scope.Employeedetail.BPSalary = false;
            }

           $scope.BPSalaryonclick = function ()
            {
               $scope.Employeedetail.MonthAmount = null;
            }

           $scope.BPDrawonclick = function () {
               $scope.Employeedetail.DDAmount = null;
                $scope.Employeedetail.TypeofDraw = 1;
                $scope.Employeedetail.DrawTerm = null;
            }

           $scope.Drawtypeonclick = function () {
                $scope.Employeedetail.DRPercentage = null;
                $scope.Employeedetail.DrawTerm = null;
               // $scope.Employeedetail.DDPeriod = new Date;
            }

           $scope.GoToEmployeeDetails = (): void => {
                HeaderFactory.SetTitle("Employee Details");
                $scope.page = $scope.pages.EmployeeDetails;
                $scope.SelectedTab = 'employee';
                $scope.pageSize = 10;
                let roleID = $scope.LoggedinEmployee.RoleID;
                this.SalesComService.GetAllEmployeeDetailsbyRoleID(roleID).then((data: ng.IHttpPromiseCallbackArg<EmployeeComponant[]>): void => {
                    $scope.Employees = <EmployeeComponant[]>data;
                });
                this.SalesComService.GetDeActivatePlanEmployees().then((data: ng.IHttpPromiseCallbackArg<EmployeeComponant>): void => {
                    $scope.Employeedetail = <EmployeeComponant>data;
                });
            }

           $scope.GoToAddEmployee = (): void => {
                $scope.Employeedetail = new EmployeeComponant;
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
                this.InitializeService.GetAllUsers().then((data: ng.IHttpPromiseCallbackArg<any[]>): void => {
                    $scope.Userslist = <any[]>data;
                });
                
           }

           $scope.CreateEmployeeConfirmation = (): void => {
               $scope.ShowAYSDialog("Are You Sure?", "Account Name mapped to an employee cannot be changed, are you sure to save the details?", function () {
                   $scope.CreateEmployee();
               });
           }

           $scope.CreateEmployee = (): Boolean => {

                var acctname = $scope.Employeedetail.AccountName;
                if ($scope.Userslist.indexOf(acctname)== -1)
                {
                    $.bootstrapGrowl('Please select account name from LDAP user list');
                    return false;
                }
                var dateofhire = new Date($scope.Employeedetail.DateofHire);
                var dateinposition = new Date($scope.Employeedetail.DateInPosition);
                if (dateofhire > dateinposition) {
                    $.bootstrapGrowl("Date of hire should be greater or equal to Date in position", { type: 'danger' });
                    return false;
                }
                var branchcheck: any = 0;
                $scope.Employeedetail.SecondaryBranch = [];
                angular.forEach($scope.Employeedetail.SecondaryBranchList, function (value, index) {
                    $scope.Employeedetail.SecondaryBranch.push(value.BranchID);
                    if ($scope.Employeedetail.PrimaryBranch ==String(value.BranchID)) {
                        branchcheck = 1;
                    }
                })
                if (branchcheck == 1) {
                    $.bootstrapGrowl("Both Primary and Secondary branches cannot be same", { type: 'danger' });
                    return false;
                }        
                $scope.Employeedetail.CreatedBy = $scope.LoggedinEmployee.UID;
                $scope.Employeedetail.IsActive = true;
                if ($scope.Employeedetail.DDPeriod == undefined) { $scope.Employeedetail.DDPeriod = $scope.currentDt }
                this.SalesComService.AddEmployee($scope.Employeedetail).then((data: ng.IHttpPromiseCallbackArg<Number>): void => {
                    
                    if (<Number>data == 2) {
                        $.bootstrapGrowl("Please map different Account Name to create employee.", { type: 'danger' });
                        return;
                    }
                    else if (<Number>data == 3) {
                        $.bootstrapGrowl("Please enter different EmployeeID to create employee.", { type: 'danger' });
                        return;
                    }
                    else if (<Number>data != 0) {
                        $.bootstrapGrowl("Employee details added successfully.", { type: 'success' });
                        $scope.GoToEmployeeDetails();
                    }
                    else {
                        $.bootstrapGrowl("Unfortunately employee details cannot be inserted.Please retry.", { type: 'danger' });
                    }
                });
            }

           $scope.GoToEditEmployee = (emp): void => {
                
                HeaderFactory.SetTitle("Edit Employee");
                $scope.page = $scope.pages.EditEmployee;
                $scope.SelectedTab = 'employee';
                $scope.GetRoleslist();
                //$scope.GetBranchlist();
                $scope.Brancheslists = [];
                $scope.GetAllManagerlist();
                $scope.GetPlanlist();
                $scope.Employeedetail = new EmployeeComponant;
                this.SalesComService.GetEmployeebyUID(emp).then((data: ng.IHttpPromiseCallbackArg<EmployeeComponant>): void => {
                    $scope.Employeedetail = <EmployeeComponant>data;
                    $scope.Employeedetail.DateofHire = $scope.Employeedetail.DateofHire.split("-");
                    $scope.Employeedetail.DateofHire = $scope.Employeedetail.DateofHire[1] + '/' + $scope.Employeedetail.DateofHire[2].substring(0, 2) + '/' + $scope.Employeedetail.DateofHire[0];
                    $scope.Employeedetail.DateInPosition = $scope.Employeedetail.DateInPosition.split("-");
                    $scope.Employeedetail.DateInPosition = $scope.Employeedetail.DateInPosition[1] + '/' + $scope.Employeedetail.DateInPosition[2].substring(0, 2) + '/' + $scope.Employeedetail.DateInPosition[0];
                    $scope.Employeedetail.DDPeriod = $scope.Employeedetail.DDPeriod.split("-");
                    $scope.Employeedetail.DDPeriod = $scope.Employeedetail.DDPeriod[1] + '/'  + $scope.Employeedetail.DDPeriod[0];
                    $scope.Employeedetail.RoleID = String($scope.Employeedetail.RoleID);
                    $scope.Employeedetail.ReportMgr = String($scope.Employeedetail.ReportMgr);
                    $scope.Employeedetail.ApproveMgr = String($scope.Employeedetail.ApproveMgr);
                    let splitedBranch: any[];
                    splitedBranch = $scope.Employeedetail.SecondaryBranch[0].split(',');
                    var secBranchString: string = '';
                    this.SalesComService.GetBranches().then((data: ng.IHttpPromiseCallbackArg<Branches[]>): void => {
                        $scope.Brancheslists = <Branches[]>data;
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
            }

           $scope.EditEmployee = (): Boolean => {
                var dateofhire = new Date($scope.Employeedetail.DateofHire);
                var dateinposition = new Date($scope.Employeedetail.DateInPosition);
                if (dateofhire > dateinposition) {
                    $.bootstrapGrowl('Date of hire should be greater or equal to Date in position');
                    return false;
                }
                $scope.Employeedetail.SecondaryBranch = [];
                var branchcheck: any = 0;
                angular.forEach($scope.Employeedetail.SecondaryBranchList, function (value, index) {
                    $scope.Employeedetail.SecondaryBranch.push(value.BranchID);
                    if ($scope.Employeedetail.PrimaryBranch == String(value.BranchID)) {
                        branchcheck = 1;
                    }
                })
                if (branchcheck == 1) {
                    $.bootstrapGrowl("Both Primary and Secondary branches cannot be same", { type: 'danger' });
                    return false;
                }    
                
                $scope.Employeedetail.ModifiedBy = $scope.LoggedinEmployee.UID;
                if ($scope.Employeedetail.DDPeriod == undefined)
                {
                    $scope.Employeedetail.DDPeriod = new Date;
                }
                this.SalesComService.EditEmployee($scope.Employeedetail).then((data: ng.IHttpPromiseCallbackArg<Boolean>): void => {
                        if (<Boolean>data == true) {
                            $.bootstrapGrowl("Emoloyee details updated successfully.", { type: 'success' });

                        $scope.GoToEmployeeDetails();
                    }
                });
            }

           $scope.GoToViewEmployee = (emp): void => {
                //$scope.GetBranchlist();
               $scope.Brancheslists = [];
                HeaderFactory.SetTitle("View Employee");
                $scope.page = $scope.pages.ViewEmployee;
                $scope.SelectedTab = 'employee';
                this.SalesComService.GetEmployeebyUID(emp).then((data: ng.IHttpPromiseCallbackArg<EmployeeComponant>): void => {
                    debugger;
                    $scope.Employeedetail = <EmployeeComponant>data;
                    $scope.Employeedetail.BPDraw = Booleanvalues[$scope.Employeedetail.BPDraw == true ? 1 : 0];
                    $scope.Employeedetail.BPSalary = Booleanvalues[$scope.Employeedetail.BPSalary == true ? 1 : 0];
                    $scope.Employeedetail.TypeofDraw = $scope.Employeedetail.TypeofDraw == 1 ? "Guaranteed" : "Recoverable"
                    let splitedBranch: any[];
                    splitedBranch = $scope.Employeedetail.SecondaryBranch[0].split(',');
                    var secBranch: any[];
                    secBranch = [];
                    var secBranchString: string = "";
                    this.SalesComService.GetBranches().then((data: ng.IHttpPromiseCallbackArg<Branches[]>): void => {
                        $scope.Brancheslists = <Branches[]>data;
                        angular.forEach($scope.Brancheslists, function (value, index) {
                            for (var i = 0; i < splitedBranch.length - 1; i++) {
                                if (splitedBranch[i] == value.BranchID) {
                                    secBranchString += value.BranchName + ',';
                                }
                            }
                        })
                        secBranchString = secBranchString.substring(0, secBranchString.length - 1);
                        $scope.Employeedetail.SecondaryBranchName = secBranchString;
                    });

                });
            }

           $scope.ActivateDeactivateConfirmation = (emp): void => {
                
                if (emp.IsActive == true) {
                    $scope.ShowAYSDialog("Are You Sure?", "All the employee details will be deactivated and employee will not be allowed to access the system, are you sure to de activate?", function () { $scope.GoToActivateDeactivateEmployee(emp); });
                }
                else {
                    $scope.ShowAYSDialog("Are You Sure?", "Do you want to activate the Employee?", function () { $scope.GoToActivateDeactivateEmployee(emp); });
                }
            }

           $scope.GoToActivateDeactivateEmployee = (emp): void => {
                
                HeaderFactory.SetTitle("Employee Details");
                $scope.page = $scope.pages.EmployeeDetails;
                $scope.SelectedTab = 'employee';
                emp.IsActive = (emp.IsActive == true ? false : true);
                emp.ModifiedBy = $scope.LoggedinEmployee.UID;
                this.SalesComService.ActivateDeactivateEmployee(emp).then((data: ng.IHttpPromiseCallbackArg<Boolean>): void => {
                    $scope.GoToEmployeeDetails();
                });
           }

           $scope.GoToCommissionslipDetailsConfirmation = (): void => {
               $scope.ShowAYSDialog("Are You Sure?", "Are you sure, you want to cancel?", function () {
                   $scope.GoToCommissionslipDetails();
               });
           }

           $scope.GoToCommissionslipDetails = (): void => {
                HeaderFactory.SetTitle("Sales Rep - Commission Slip Details");
                $scope.page = $scope.pages.CommissionslipDetails;
                $scope.SelectedTab = 'commission';
                $scope.IsGMDeactivated = false;
                $scope.ISPlanDeactivated = false;
                this.SalesComService.GetAllCommissionsbyUID($scope.LoggedinEmployee).then((data: ng.IHttpPromiseCallbackArg<CommissionComponent[]>): void => {
                    
                    $scope.Commissionslips = <CommissionComponent[]>data;
                    $scope.Commissionslips[0].CreatedBy;
                    $.each($scope.Commissionslips, function (key, value: any) {

                        value.Status = value.ProcesByPayroll == 1 ? "Payroll Processed" : CommissionStatus[value.Status];
                    });
                });
                if ($scope.LoggedinEmployee.RoleID == 5 || $scope.LoggedinEmployee.RoleID == 6) {
                    this.SalesComService.GetDeactiveGMandPlanID($scope.LoggedinEmployee).then((data: ng.IHttpPromiseCallbackArg<number[]>): void => {
                        var Deactivatedlist: number[] = <number[]>data;
                        $scope.ISPlanDeactivated = Deactivatedlist[0] == 0 ? false : true;
                        $scope.IsGMDeactivated = Deactivatedlist[1] == 0 ? false : true;
                    });
                }
           }
           $scope.TipLeadRow = [];
           $scope.AddTipLeadRow = function () {

               $scope.TipLeadRow.push(new TipLeadSlip);
           }
           $scope.RemoveTipLeadRow = function (index) {
               $scope.ShowAYSDialog("Are You Sure?", "All the Tip lead details associated will be deleted", function () {
                   $scope.TipLeadRow.splice(index, 1);
                   $scope.TotalCommissions();
               });
           }
           var Parentnode = this;
           $scope.onSalespersonselect = function ($item, $model, $label) {
               $scope.Employeedetail = new EmployeeComponant;
               $scope.Commissionslip.SalesPerson = $label;
               $scope.Employeedetail.UID = $item.UID;
               $scope.Commissionslip.CreatedBy = $item.UID;
               $scope.Brancheslists = [];
               Parentnode.SalesComService.GetEmployeebyUID($scope.Employeedetail).then((data: ng.IHttpPromiseCallbackArg<EmployeeComponant>): void => {
                   $scope.Employeedetail = <EmployeeComponant>data;
                   $scope.Commissionslip.SalesPerson = $scope.Employeedetail.FirstName + ' ' + $scope.Employeedetail.LastName;
                   $scope.Commissionslip.BranchID = $scope.Employeedetail.PrimaryBranch;
                   Parentnode.SalesComService.GetBranches().then((data: ng.IHttpPromiseCallbackArg<Branches[]>): void => {
                       $scope.Brancheslists = <Branches[]>data;
                       let splitedBranch: any[];
                       splitedBranch = [];
                       $scope.EmployeeBranch = [];
                       splitedBranch = $scope.Employeedetail.SecondaryBranch[0].split(',');
                       splitedBranch.push($scope.Employeedetail.PrimaryBranch);
                       $.each($scope.Brancheslists, function (key, value: any) {
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
           }

           $scope.GoToAddCommissionslip = (): void => {
               
                HeaderFactory.SetTitle("Sales Rep - Add Commission Slip");
                $scope.page = $scope.pages.AddCommissionslip;
                $scope.SelectedSalesperson = undefined;
                $scope.Salespersonslist = [];
                $scope.SRSalespersons = [];
                $scope.TipLeadlist = [];
                $scope.SelectedTab = 'commission';
                $scope.Commissionslip = new CommissionComponent;
               // $scope.Commissionslip.SalesPerson = $scope.LoggedinEmployee.FirstName + ' ' + $scope.LoggedinEmployee.LastName;
                $scope.Commissionslip.EntryDate = $scope.Commissionslip.DateofSale = $scope.currentDt;
                $scope.Commissionslip.AccountPeriod = $scope.currentMnth;
                $scope.Commissionslip.TipLeadAmount = 0.00;
                $scope.Commissionslip.BaseCommission = 0.00;
                $scope.Commissionslip.LeaseCommission = 0.00;
                $scope.Commissionslip.ServiceCommission = 0.00;
                $scope.Commissionslip.TravelCommission = 0.00;
                $scope.Commissionslip.CashCommission = 0.00;
                $scope.Commissionslip.SpecialCommission = 0.00;
                $scope.Commissionslip.TotalCEarned = 0.00;
                $scope.Commissionslip.CustomerType = CustomerTypes.NewCustomer;
                $scope.SaleTypeList = [];
               // $scope.Commissionslip.BranchID = $scope.LoggedinEmployee.PrimaryBranch;
                $scope.Brancheslists = [];
                if ($scope.LoggedinEmployee.RoleID == RoleTypes.SalesRep) {
                    $scope.Commissionslip.SalesPerson = $scope.LoggedinEmployee.FirstName + ' ' + $scope.LoggedinEmployee.LastName;
                    $scope.Commissionslip.BranchID = $scope.LoggedinEmployee.PrimaryBranch;
                    this.SalesComService.GetBranches().then((data: ng.IHttpPromiseCallbackArg<Branches[]>): void => {
                        $scope.Brancheslists = <Branches[]>data;
                        let splitedBranch: any[];
                        splitedBranch = [];
                        $scope.EmployeeBranch = [];
                        splitedBranch = $scope.LoggedinEmployee.SecondaryBranch[0].split(',');
                        splitedBranch.push($scope.LoggedinEmployee.PrimaryBranch);
                        $.each($scope.Brancheslists, function (key, value: any) {
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
                $scope.TipLeadRow.push(new TipLeadSlip);
                var roleID = RoleTypes.PayRoll;
                this.SalesComService.GetAllEmployeeDetailsbyRoleID(roleID).then((data: ng.IHttpPromiseCallbackArg<EmployeeComponant[]>): void => {
                    $scope.Salespersonslist = <EmployeeComponant[]>data;
                    $scope.SRSalespersons = $scope.Salespersonslist.filter(function (person) {
                        return (person.RoleID == RoleTypes.SalesRep && person.IsActive == true);
                    });
                    $scope.TipLeadlist = $scope.Salespersonslist.filter(function (person) {
                        return ((person.RoleID == RoleTypes.SalesRep || person.RoleID == RoleTypes.NonSalesEmployee) && person.IsActive == true);
                    });
                });
                $.each($scope.SRSalespersons, function (key, value: any) {
                    $scope.splitnamelist.push(value.LastName);
                });
                $scope.GetSaletypelist();
                let currentdate = new Date();
                currentdate.getDate();
                this.SalesComService.GetAccountingMonth(currentdate).then((data: ng.IHttpPromiseCallbackArg<Payroll[]>): void => {
                    $scope.AccountPayroll = <Payroll>data;
                    $scope.Commissionslip.AccountPeriod = $scope.AccountPayroll.Month;
                    $scope.Commissionslip.AccountPeriodID = $scope.AccountPayroll.ID;
                });
           }
           $scope.TotalTipslip = (): void => {
               $.each($scope.TipLeadRow, function (key, value: any) {
                   var tipamount = parseFloat(value.TipLeadAmount != undefined && value.TipLeadAmount != "" ? value.TipLeadAmount : 0);
                   var positive = parseFloat(value.PositiveAdjustments != undefined && value.PositiveAdjustments != "" ? value.PositiveAdjustments : 0);
                   var negative = parseFloat(value.NegativeAdjustments != undefined && value.NegativeAdjustments != "" ? value.NegativeAdjustments : 0);
                   var companycontribution = parseFloat(value.CompanyContribution != undefined && value.CompanyContribution != "" ? value.CompanyContribution : 0);
                   value.TotalCEarned = (tipamount + companycontribution + positive) - negative;
                   value.TotalCEarned = parseFloat(value.TotalCEarned).toFixed(2);
               });

           }

           $scope.TotalCommissions = (): void => {
               var tipleadamount: any=0.00, positiveadj: any=0.00, negativeadj: any=0.00;
               $.each($scope.TipLeadRow, function (key, value: any) {
                   var tipamount= parseFloat(value.TipLeadAmount != undefined && value.TipLeadAmount != "" ? value.TipLeadAmount : 0);
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
            }

           $scope.AddCommission = (status): Boolean => {
                if ($scope.Commissionslip.SplitSalePerson!=null && $scope.Commissionslip.SplitSalePersonID == undefined) {//check valid salesperson
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
                if ($scope.LoggedinEmployee.RoleID == RoleTypes.SalesRep) {
                    $scope.Commissionslip.CreatedBy = $scope.LoggedinEmployee.UID;
                }
                if ($scope.Commissionslip.CreatedBy == undefined) {
                    $.bootstrapGrowl("Please select Sales Person", { type: 'danger' });
                    return false;
                }
                $scope.Commissionslip.IsActive = true;
                $scope.Commissionslip.SlipType = SlipTypes.CommissionSlip;
                var error: number = 0;
                $.each($scope.TipLeadRow, function (key, value) {
                    if ((value.TipLeadID == undefined || value.TipLeadID == 0) && value.TipLeadAmount != 0 && value.TipLeadAmount != undefined) {
                        error = 1;
                    } else if ((value.TipLeadAmount == undefined || value.TipLeadAmount == 0) && value.TipLeadID != undefined && value.TipLeadID != 0) {
                        error = 2;
                    } else if ((value.TipLeadID == undefined || value.TipLeadID == 0) && ((value.PositiveAdjustments != 0 && value.PositiveAdjustments != undefined) || (value.NegativeAdjustments != 0 && value.NegativeAdjustments != undefined))) {
                        error = 1;
                    } else if ((value.TipLeadAmount == undefined || value.TipLeadAmount == 0) && ((value.PositiveAdjustments != 0 && value.PositiveAdjustments != undefined) || (value.NegativeAdjustments != 0 && value.NegativeAdjustments != undefined))) {
                        error = 2;
                    }
                });
                if (error == 1) {
                    $.bootstrapGrowl("Please select Tip Lead", { type: 'danger' });
                    return false;
                } else if (error == 2) {
                    $.bootstrapGrowl("Please enter Tip Lead Amount", { type: 'danger' });
                    return false;
                }
                $scope.Commissionslip.TipLeadSliplist = $scope.TipLeadRow.filter(function (Tiplead) {
                    return (Tiplead.TipLeadID != undefined && Tiplead.TipLeadID != 0);
                });

                $.each($scope.Commissionslip.TipLeadSliplist, function (key, value) {
                    value.SlipType = SlipTypes.TipLeadSlip;
                });

                $scope.Commissionslip.CreatedOn = $scope.currentDtTime;

                this.SalesComService.AddCommission($scope.Commissionslip).then((data: ng.IHttpPromiseCallbackArg<Number>): void => {
                    if (<Number>data != -1) {
                        if (status == 1) {
                            $.bootstrapGrowl("Commission details saved successfully.", { type: 'success' });
                        }
                        else if (status == 2) {
                            $.bootstrapGrowl("Commission details submitted successfully.", { type: 'success' });
                            $scope.MailContent = new EmailMessage;
                            var uid = $scope.LoggedinEmployee.ReportMgr != null && $scope.LoggedinEmployee.ReportMgr !=0 ? $scope.LoggedinEmployee.ReportMgr : $scope.LoggedinEmployee.ApproveMgr;
                            this.SalesComService.CommissionCreatedNotification(uid, 0).then((data: ng.IHttpPromiseCallbackArg<string>): void => {
                                $scope.MailContent.RecipientTo = $scope.LoggedinEmployee.Email + ',' + <string>data;
                                $scope.MailContent.Subject = "New Commission Slip";
                                $scope.MailContent.Body = "A new commission slip created. Please click the link to view the <a href=\""+ $scope.MailUrl +"\"> New commission slip created.</a>";
                                $scope.SendMail();
                            });
                            var parentnode = this;
                            $.each($scope.Commissionslip.TipLeadSliplist, function (key, value) {
                                var uid = value.TipLeadID;
                                parentnode.SalesComService.CommissionCreatedNotification(uid, status).then((data: ng.IHttpPromiseCallbackArg<string>): void => {
                                    $scope.MailContent.RecipientTo = <string>data;
                                    $scope.MailContent.Subject = "New Tip Lead Slip";
                                    $scope.MailContent.Body = "A new Tip Lead Slip created. Please click the link to <a href=\""+ $scope.MailUrl +"\"> view the New Tip Lead slip.</a>";
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

            }

           $scope.GoToEditCommissionslip = (commissionid): void => {
               
                HeaderFactory.SetTitle("Sales Rep - Edit Commission Slip");
                $scope.SelectedTab = 'commission';
                $scope.page = $scope.pages.EditCommissionslip;
                $scope.Brancheslists = [];
                $scope.Salespersonslist = [];
                $scope.SRSalespersons = [];
                $scope.TipLeadlist = [];
                $scope.Commissionslips = [];
                $scope.Commissionslip = new CommissionComponent();
                $scope.Commissionslip.CustomerType = CustomerTypes.NewCustomer;
                $scope.Commissionslip.TipLeadAmount = 0.00;
                $scope.Commissionslip.BaseCommission = 0.00;
                $scope.Commissionslip.LeaseCommission = 0.00;
                $scope.Commissionslip.ServiceCommission = 0.00;
                $scope.Commissionslip.TravelCommission = 0.00;
                $scope.Commissionslip.CashCommission = 0.00;
                $scope.Commissionslip.SpecialCommission = 0.00;
                $scope.TipLeadRow = [];
                var roleID = RoleTypes.PayRoll;
                this.SalesComService.GetCommissionbyID(commissionid).then((data: ng.IHttpPromiseCallbackArg<CommissionComponent[]>): void => {
                    $scope.Commissionslips = <CommissionComponent[]>data;
                    $scope.Commissionslip = $scope.Commissionslips[0];
                    $scope.Commissionslip.Comments = ""; // Empty for new comment
                    $scope.Commissionslip.EntryDate = $scope.Commissionslip.EntryDate.split("-");
                    $scope.Commissionslip.EntryDate = $scope.Commissionslip.EntryDate[1] + '/' + $scope.Commissionslip.EntryDate[2].substring(0, 2) + '/' + $scope.Commissionslip.EntryDate[0];
                    $scope.Commissionslip.DateofSale = $scope.Commissionslip.DateofSale.split("-");
                    $scope.Commissionslip.DateofSale = $scope.Commissionslip.DateofSale[1] + '/' + $scope.Commissionslip.DateofSale[2].substring(0, 2) + '/' + $scope.Commissionslip.DateofSale[0];
                    //$scope.Commissionslip.AccountPeriod = $scope.Commissionslip.AccountPeriod.split("-");
                    //$scope.Commissionslip.AccountPeriod = $scope.Commissionslip.AccountPeriod[1] + '/' + $scope.Commissionslip.AccountPeriod[0];
                    $scope.Commissionslip.BranchID = String($scope.Commissionslip.BranchID);

                    let splitedBranch: any[];
                    splitedBranch = [];
                    $scope.EmployeeBranch = [];
                    $scope.Employeedetail = new EmployeeComponant;
                    $scope.Employeedetail.UID = $scope.Commissionslip.CreatedBy;
                    this.SalesComService.GetEmployeebyUID($scope.Employeedetail).then((data: ng.IHttpPromiseCallbackArg<EmployeeComponant>): void => {
                        $scope.Employeedetail = <EmployeeComponant>data;
                        splitedBranch = $scope.Employeedetail.SecondaryBranch[0].split(',');
                        splitedBranch.push($scope.Employeedetail.PrimaryBranch);
                        this.SalesComService.GetBranches().then((data: ng.IHttpPromiseCallbackArg<Branches[]>): void => {
                            $scope.Brancheslists = <Branches[]>data;
                            $.each($scope.Brancheslists, function (key, value: any) {
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

                    this.SalesComService.GetAllEmployeeDetailsbyRoleID(roleID).then((data: ng.IHttpPromiseCallbackArg<EmployeeComponant[]>): void => {
                    $scope.Salespersonslist = <EmployeeComponant[]>data;
                    $scope.SRSalespersons = $scope.Salespersonslist.filter(function (person) {
                        return (person.RoleID == RoleTypes.SalesRep);
                    });
                    $scope.TipLeadlist = $scope.Salespersonslist.filter(function (person) {
                        return (person.RoleID == RoleTypes.SalesRep || person.RoleID == RoleTypes.NonSalesEmployee);
                    });
                });
                     $.each($scope.SRSalespersons, function (key, value: any) {
                     $scope.splitnamelist.push(value.LastName);
                    });
                     var Commissionlist: CommissionComponent[];
                     $scope.Commissionslips = $scope.Commissionslips.filter(function (commission) {
                         return (commission.SlipType == SlipTypes.TipLeadSlip);
                     });
                     var Tipslip: TipLeadSlip;
                     if ($scope.Commissionslips.length > 0) {
                         $.each($scope.Commissionslips, function (key, value: any) {
                             Tipslip = new TipLeadSlip;
                             Tipslip.ID = value.ID;
                             Tipslip.MainCommissionID = value.MainCommissionID
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
                         Tipslip = new TipLeadSlip;
                         Tipslip.TipLeadAmount = 0.00;
                         Tipslip.PositiveAdjustments = 0.00;
                         Tipslip.NegativeAdjustments = 0.00;
                         Tipslip.CompanyContribution = 0.00;
                         Tipslip.TotalCEarned = 0.00;
                         $scope.TipLeadRow.push(Tipslip);
                     }
                     $scope.Brancheslists = [];
                     $scope.MainCommissionStatus = 0;
                     if ($scope.Commissionslip.SlipType == SlipTypes.TipLeadSlip) {//To get status of corresponding commission slip
                         var Commissionslips: CommissionComponent[];
                         this.SalesComService.GetCommissionbyID($scope.Commissionslip.MainCommissionID).then((data: ng.IHttpPromiseCallbackArg<CommissionComponent[]>): void => {
                             Commissionslips = <CommissionComponent[]>data;
                             if (Commissionslips[0].ProcesByPayroll == true) {
                                 $scope.MainCommissionStatus = 1;
                             } else if (Commissionslips[0].Status == 7) {
                                 $scope.MainCommissionStatus = 2;
                             }
                         });

                     }
                });
                $scope.GetSaletypelist();
            }

           $scope.EditCommission = (status): Boolean => {
                var dateofsale = new Date($scope.Commissionslip.DateofSale);
                var entrydate = new Date($scope.Commissionslip.EntryDate);
                if (dateofsale > entrydate) {
                    $.bootstrapGrowl("Date of entry should be greater or equal to Date of sale", { type: 'danger' });
                    return false;
                }
                if ((status == 4 || status == 6) && $scope.Commissionslip.Comments == "") { //As per FRS US9 - 8a
                    $.bootstrapGrowl("Please provide reason for rejection in comment.", { type: 'danger' });
                    return false;
                }
                var MailNotificationstatus = $scope.Commissionslip.Status;
                $scope.Commissionslip.Status = status == 8 ? 7 : status; //To change, Payroll accept & notify to Payroll accepted w.r.t FRS for display in dashboard.
                $scope.Commissionslip.ModifiedBy = $scope.LoggedinEmployee.UID;
                $scope.Commissionslip.CreatedOn = $scope.currentDtTime;
                $scope.Commissionslip.SlipType = SlipTypes.CommissionSlip;

                var error: number = 0;
                $.each($scope.TipLeadRow, function (key, value) {
                    if ((value.TipLeadID == undefined || value.TipLeadID == 0) && value.TipLeadAmount != 0 && value.TipLeadAmount != undefined) {
                        error = 1;
                    } else if ((value.TipLeadAmount == undefined || value.TipLeadAmount == 0) && value.TipLeadID != undefined && value.TipLeadID != 0) {
                        error = 2;
                    } else if ((value.TipLeadID == undefined || value.TipLeadID == 0) && ((value.PositiveAdjustments != 0 && value.PositiveAdjustments != undefined) || (value.NegativeAdjustments != 0 && value.NegativeAdjustments != undefined))) {
                        error = 1;
                    } else if ((value.TipLeadAmount == undefined || value.TipLeadAmount == 0) && ((value.PositiveAdjustments != 0 && value.PositiveAdjustments != undefined) || (value.NegativeAdjustments != 0 && value.NegativeAdjustments != undefined))) {
                        error = 2;
                    }
                });
                if (error == 1) {
                    $.bootstrapGrowl("Please select Tip Lead", { type: 'danger' });
                    return false;
                } else if (error == 2) {
                    $.bootstrapGrowl("Please enter Tip Lead Amount", { type: 'danger' });
                    return false;
                }
                $scope.Commissionslip.TipLeadSliplist = $scope.TipLeadRow.filter(function (Tiplead) {
                    return (Tiplead.TipLeadID != undefined && Tiplead.TipLeadID != 0);
                });
                var tipleadstatus: number[]=[];
                $.each($scope.Commissionslip.TipLeadSliplist, function (key, value) {
                    value.SlipType = SlipTypes.TipLeadSlip;
                    tipleadstatus[key] = value.Status;
                    value.Status = ((value.Status != 7 && value.Status != 5 && !value.ProcesByPayroll)&&( $scope.Commissionslip.Status == 2 || $scope.Commissionslip.Status == 1)) ? $scope.Commissionslip.Status : value.Status;
               });
                               
                $scope.Commissionslip.CommentedBy = $scope.LoggedinEmployee.FirstName + ' ' + $scope.LoggedinEmployee.LastName;
                this.SalesComService.EditCommission($scope.Commissionslip).then((data: ng.IHttpPromiseCallbackArg<Boolean>): void => {
                    if (<Boolean>data == true) {
                        switch (status) {
                            case 1:                             /* "Draft" = 1*/
                                $.bootstrapGrowl("Commission details saved successfully.", { type: 'success' });
                                break;

                            case 2:                          /* "Waiting for approval" = 2*/
                                $.bootstrapGrowl("Commission details updated successfully.", { type: 'success' });
                                $scope.MailContent = new EmailMessage;
                                if (MailNotificationstatus == 1) {
                                    var uid = $scope.LoggedinEmployee.ReportMgr != null && $scope.LoggedinEmployee.ReportMgr > 0 ? $scope.LoggedinEmployee.ReportMgr : $scope.LoggedinEmployee.ApproveMgr;
                                    this.SalesComService.CommissionCreatedNotification(uid, 0).then((data: ng.IHttpPromiseCallbackArg<string>): void => {

                                        $scope.MailContent.RecipientTo = $scope.LoggedinEmployee.Email + ',' + <string>data;
                                        $scope.MailContent.Subject = "New Commission Slip";
                                        $scope.MailContent.Body = "A new commission slip created. Please click the link to view the <a href=\"" + $scope.MailUrl + "\"> New commission slip created.</a>";
                                        $scope.SendMail();
                                    });
                                }
                                var parentnode = this;
                                $.each($scope.Commissionslip.TipLeadSliplist, function (key, value) {
                                    if (value.ID == undefined || tipleadstatus[key]==1) {
                                        var uid = value.TipLeadID;
                                        parentnode.SalesComService.CommissionCreatedNotification(uid, status).then((data: ng.IHttpPromiseCallbackArg<string>): void => {
                                            $scope.MailContent.RecipientTo = <string>data;
                                            $scope.MailContent.Subject = "New Tip Lead Slip";
                                            $scope.MailContent.Body = "A new Tip Lead Slip created. Please click the link to <a href=\"" + $scope.MailUrl +"\"> view the New Tip Lead slip.</a>";
                                            $scope.SendMail();
                                        });
                                    }
                                });
                                break;

                            case 3:                          /* "SM-Accepted" = 3*/
                                $.bootstrapGrowl("You have accepted the commission slip.", { type: 'success' });
                                $scope.MailContent = new EmailMessage;
                                var uid: any = $scope.Commissionslip.CreatedBy;
                                this.SalesComService.CommissionCreatedNotification(uid, status).then((data: ng.IHttpPromiseCallbackArg<string>): void => {
                                    
                                    $scope.MailContent.RecipientTo = <string>data;
                                    $scope.MailContent.Subject = "MSC-Commission Slip";
                                    $scope.MailContent.Body = "Commission slip has been accepted by SM. Please click the link to view the <a href=\"" + $scope.MailUrl +"\"> Accepted Commission slip.</a>";
                                    $scope.SendMail();
                                });
                                break;

                            case 4:                          /* "SM-Rejected" = 4*/
                                $.bootstrapGrowl("You have Rejected the commission slip.", { type: 'success' });
                                
                                $scope.MailContent = new EmailMessage;
                                var uid: any = $scope.Commissionslip.CreatedBy;
                                this.SalesComService.CommissionCreatedNotification(uid, status).then((data: ng.IHttpPromiseCallbackArg<string>): void => {
                                    
                                    $scope.MailContent.RecipientTo = <string>data;
                                    $scope.MailContent.Subject = "MSC-Commission Slip";
                                    $scope.MailContent.Body = "Commission slip has been Rejected by SM. Please click the link to view the <a href=\"" + $scope.MailUrl +"\"> Rejected Commission slip.</a>";
                                    $scope.SendMail();
                                });
                                break;

                            case 5:                           /* "GM-Accepted" = 5*/
                                $.bootstrapGrowl("You have accepted the commission slip.", { type: 'success' });
                                $scope.MailContent = new EmailMessage;
                                this.SalesComService.CommissionCreatedNotification(0, status).then((data: ng.IHttpPromiseCallbackArg<string>): void => {
                                    
                                    $scope.MailContent.RecipientTo = <string>data;
                                    $scope.MailContent.Subject = "MSC-Commission Slip";
                                    $scope.MailContent.Body = "Commission slip has been accepted by GM. Please click the link to view the <a href=\"" + $scope.MailUrl +"\"> Accepted Commission slip.</a>";
                                    $scope.SendMail();
                                });
                                break;

                            case 6:                            /* "GM-Rejected" = 6*/
                                $.bootstrapGrowl("You have Rejected the commission slip.", { type: 'success' });
                                $scope.MailContent = new EmailMessage;
                                this.SalesComService.CommissionCreatedNotification($scope.Commissionslip.CreatedBy, status).then((data: ng.IHttpPromiseCallbackArg<string>): void => {
                                    
                                    $scope.MailContent.RecipientTo = <string>data;
                                    $scope.MailContent.Subject = "MSC-Commission Slip";
                                    $scope.MailContent.Body = "Commission slip has been Rejected by GM. Please click the link to view the <a href=\"" + $scope.MailUrl +"\"> Rejected Commission slip.</a>";
                                    $scope.SendMail();
                                });
                                break;

                            case 7:                       /* "Payroll Accepted" = 7*/
                                $.bootstrapGrowl("You have accepted the commission slip.", { type: 'success' });
                                break;

                            case 8:                  /* "Payroll Accept and Notify" = 8*/
                                $.bootstrapGrowl("You have accepted the commission slip.", { type: 'success' });
                                $scope.MailContent = new EmailMessage;
                                status = 7;     /* "Changed to Payroll Accepted" = 7*/
                                this.SalesComService.CommissionCreatedNotification($scope.Commissionslip.CreatedBy, status).then((data: ng.IHttpPromiseCallbackArg<string>): void => {
                                    
                                    $scope.MailContent.RecipientTo = <string>data;
                                    $scope.MailContent.Subject = "MSC-Commission Slip";
                                    $scope.MailContent.Body = "Commission slip has been accepted by Payroll. Please click the link to view the <a href=\"" + $scope.MailUrl +"\"> Accepted Commission slip.</a>";
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
           }
           $scope.EditTipLeadslip = (status): Boolean => {
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
                if ($scope.Commissionslip.TotalCEarned<0)
                {
                    $.bootstrapGrowl("Totalcommission cannot be negative", { type: 'danger' });
                    return false;
                }
                   this.SalesComService.EditTipLeadSlip($scope.Commissionslip).then((data: ng.IHttpPromiseCallbackArg<Boolean>): void => {
                    if (<Boolean>data == true) {
                        switch (status) {
                            case 1:                             /* "Draft" = 1*/
                                    break;

                            case 2:                          /* "Waiting for approval" = 2*/
                                   break;

                            case 3:                          /* "SM-Accepted" = 3*/
                                   break;

                            case 4:                          /* "SM-Rejected" = 4*/
                                  break;

                            case 5:                           /* "GM-Accepted" = 5*/
                                $.bootstrapGrowl("You have accepted the Tip Lead slip.", { type: 'success' });
                                $scope.MailContent = new EmailMessage;
                                this.SalesComService.CommissionCreatedNotification(0, status).then((data: ng.IHttpPromiseCallbackArg<string>): void => {
                                    
                                    $scope.MailContent.RecipientTo = <string>data;
                                    $scope.MailContent.Subject = "MSC-Tip Lead Slip";
                                    $scope.MailContent.Body = " A Tip Lead Slip has been accepted by the GM. Please click the link to <a href=\"" + $scope.MailUrl +"\"> view the Tip Lead slip.</a>";
                                    $scope.SendMail();
                                });
                                break;

                            case 6:                            /* "GM-Rejected" = 6*/
                                 break;

                            case 7:                       /* "Payroll Accepted" = 7*/
                                $.bootstrapGrowl("You have accepted the Tip Lead slip.", { type: 'success' });
                                break;

                            case 8:                  /* "Payroll Accept and Notify" = 8*/
                               break;
                        }
                    }
                        else {
                            $.bootstrapGrowl("Unfortunately Tip Lead slip details cannot be processed.Please retry.", { type: 'danger' });
                        }
                        $scope.GoToCommissionslipDetails();
                });
                return true;
            }
           
           $scope.GoToResubmitCommissionslip = (commissionid): void => {
                
                HeaderFactory.SetTitle("Sales Rep - Resubmit Commission Slip");
                $scope.SelectedTab = 'commission';
                $scope.page = $scope.pages.ResubmitCommissionslip;
                //$scope.GetBranchlist();
                $scope.Salespersonslist = [];
                $scope.SRSalespersons = [];
                $scope.TipLeadRow = [];
                $scope.TipLeadlist = [];
                $scope.Commissionslips = [];
                $scope.Commissionslip = new CommissionComponent();
                $scope.Commissionslip.TipLeadAmount = 0.00;
                $scope.Commissionslip.BaseCommission = 0.00;
                $scope.Commissionslip.LeaseCommission = 0.00;
                $scope.Commissionslip.ServiceCommission = 0.00;
                $scope.Commissionslip.TravelCommission = 0.00;
                $scope.Commissionslip.CashCommission = 0.00;
                $scope.Commissionslip.SpecialCommission = 0.00;
                var roleID = RoleTypes.PayRoll;
                this.SalesComService.GetCommissionbyID(commissionid).then((data: ng.IHttpPromiseCallbackArg<CommissionComponent[]>): void => {
                    $scope.Commissionslips = <CommissionComponent[]>data;
                    $scope.Commissionslip = $scope.Commissionslips[0];
                    $scope.Commissionslip.Comments = "";// Empty for new comment
                    $scope.Commissionslip.EntryDate = $scope.Commissionslip.EntryDate.split("-");
                    $scope.Commissionslip.EntryDate = $scope.Commissionslip.EntryDate[1] + '/' + $scope.Commissionslip.EntryDate[2].substring(0, 2) + '/' + $scope.Commissionslip.EntryDate[0];
                    $scope.Commissionslip.DateofSale = $scope.Commissionslip.DateofSale.split("-");
                    $scope.Commissionslip.DateofSale = $scope.Commissionslip.DateofSale[1] + '/' + $scope.Commissionslip.DateofSale[2].substring(0, 2) + '/' + $scope.Commissionslip.DateofSale[0];
                    //$scope.Commissionslip.AccountPeriod = $scope.Commissionslip.AccountPeriod.split("-");
                    //$scope.Commissionslip.AccountPeriod = $scope.Commissionslip.AccountPeriod[1] + '/' + $scope.Commissionslip.AccountPeriod[0];
                    $scope.Commissionslip.BranchID = String($scope.Commissionslip.BranchID);
                    this.SalesComService.GetAllEmployeeDetailsbyRoleID(roleID).then((data: ng.IHttpPromiseCallbackArg<EmployeeComponant[]>): void => {
                        $scope.Salespersonslist = <EmployeeComponant[]>data;
                        $scope.SRSalespersons = $scope.Salespersonslist.filter(function (person) {
                            return (person.RoleID == RoleTypes.SalesRep);
                        });
                        $scope.TipLeadlist = $scope.Salespersonslist.filter(function (person) {
                            return (person.RoleID == RoleTypes.SalesRep || person.RoleID == RoleTypes.NonSalesEmployee);
                        });
                    });
                    $.each($scope.SRSalespersons, function (key, value: any) {
                        $scope.splitnamelist.push(value.LastName);
                    });
                    $.each($scope.SRSalespersons, function (key, value: any) {
                        $scope.splitnamelist.push(value.LastName);
                    });
                    var Commissionlist: CommissionComponent[];
                    $scope.Commissionslips = $scope.Commissionslips.filter(function (commission) {
                        return (commission.SlipType == SlipTypes.TipLeadSlip);
                    });
                    var Tipslip: TipLeadSlip;
                    if ($scope.Commissionslips.length > 0) {
                        $.each($scope.Commissionslips, function (key, value: any) {
                            Tipslip = new TipLeadSlip;
                            Tipslip.ID = value.ID;
                            Tipslip.MainCommissionID = value.MainCommissionID
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
                    } else {
                        Tipslip = new TipLeadSlip;
                        Tipslip.TipLeadAmount = 0.00;
                        Tipslip.PositiveAdjustments = 0.00;
                        Tipslip.NegativeAdjustments = 0.00;
                        Tipslip.CompanyContribution = 0.00;
                        Tipslip.TotalCEarned = 0.00;
                        $scope.TipLeadRow.push(Tipslip);
                    }
                });
                $scope.GetSaletypelist();
                this.SalesComService.GetBranches().then((data: ng.IHttpPromiseCallbackArg<Branches[]>): void => {
                    $scope.Brancheslists = <Branches[]>data;
                    let splitedBranch: any[];
                    splitedBranch = [];
                    $scope.EmployeeBranch = [];
                    splitedBranch = $scope.LoggedinEmployee.SecondaryBranch[0].split(',');
                    splitedBranch.push($scope.LoggedinEmployee.PrimaryBranch);
                    $.each($scope.Brancheslists, function (key, value: any) {
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

           $scope.ResubmitCommission = (status): Boolean => {
                
                var dateofsale = new Date($scope.Commissionslip.DateofSale);
                var entrydate = new Date($scope.Commissionslip.EntryDate);
                if (dateofsale > entrydate) {
                    $.bootstrapGrowl("Date of entry should be greater or equal to Date of sale", { type: 'danger' });
                    return false;
                }
                $scope.Commissionslip.Status = status;
                $scope.Commissionslip.ModifiedBy = $scope.LoggedinEmployee.UID;

                var error: number = 0;
                $.each($scope.TipLeadRow, function (key, value) {
                    if ((value.TipLeadID == undefined || value.TipLeadID == 0) && value.TipLeadAmount != 0 && value.TipLeadAmount != undefined) {
                        error = 1;
                    } else if ((value.TipLeadAmount == undefined || value.TipLeadAmount == 0) && value.TipLeadID != undefined && value.TipLeadID != 0) {
                        error = 2;
                    } else if ((value.TipLeadID == undefined || value.TipLeadID == 0) && ((value.PositiveAdjustments != 0 && value.PositiveAdjustments != undefined) || (value.NegativeAdjustments != 0 && value.NegativeAdjustments != undefined))) {
                        error = 1;
                    } else if ((value.TipLeadAmount == undefined || value.TipLeadAmount == 0) && ((value.PositiveAdjustments != 0 && value.PositiveAdjustments != undefined) || (value.NegativeAdjustments != 0 && value.NegativeAdjustments != undefined))) {
                        error = 2;
                    }
                });
                if (error == 1) {
                    $.bootstrapGrowl("Please select Tip Lead", { type: 'danger' });
                    return false;
                } else if (error == 2) {
                    $.bootstrapGrowl("Please enter Tip Lead Amount", { type: 'danger' });
                    return false;
                }
                $scope.Commissionslip.TipLeadSliplist = $scope.TipLeadRow.filter(function (Tiplead) {
                    return (Tiplead.TipLeadID != undefined && Tiplead.TipLeadID != 0);
                });
                var tipleadstatus: number[] = [];
                $.each($scope.Commissionslip.TipLeadSliplist, function (key, value) {
                    value.SlipType = SlipTypes.TipLeadSlip;
                    tipleadstatus[key] = value.Status;
                    value.Status = ((value.Status != 7 && value.Status != 5 && !value.ProcesByPayroll) && ($scope.Commissionslip.Status == 2 || $scope.Commissionslip.Status == 1)) ? $scope.Commissionslip.Status : value.Status;
                });
                this.SalesComService.EditCommission($scope.Commissionslip).then((data: ng.IHttpPromiseCallbackArg<Boolean>): void => {
                    if (<Boolean>data == true) {
                        if (status == 2) {
                            $.bootstrapGrowl("Commission slip Re-submitted successfully.", { type: 'success' });
                                $scope.MailContent = new EmailMessage;
                                var uid = $scope.LoggedinEmployee.ReportMgr != null && $scope.LoggedinEmployee.ReportMgr > 0 ? $scope.LoggedinEmployee.ReportMgr : $scope.LoggedinEmployee.ApproveMgr;
                                this.SalesComService.CommissionCreatedNotification(uid, 0).then((data: ng.IHttpPromiseCallbackArg<string>): void => {
                                $scope.MailContent.RecipientTo = $scope.LoggedinEmployee.Email + ',' + <string>data;
                                $scope.MailContent.Subject = "Commission Slip Resubmitted";
                                $scope.MailContent.Body = "A commission slip Resubmitted. Please click the link to view the <a href=\"" + $scope.MailUrl +"\"> New commission slip created.</a>";
                                $scope.SendMail();
                                });
                                var parentnode = this;
                                $.each($scope.Commissionslip.TipLeadSliplist, function (key, value) {
                                    if (value.ID == undefined || tipleadstatus[key]==1) {
                                        var uid = value.TipLeadID;
                                        parentnode.SalesComService.CommissionCreatedNotification(uid, status).then((data: ng.IHttpPromiseCallbackArg<string>): void => {
                                            $scope.MailContent.RecipientTo = <string>data;
                                            $scope.MailContent.Subject = "New Tip Lead Slip";
                                            $scope.MailContent.Body = "A new Tip Lead Slip created. Please click the link to <a href=\"" + $scope.MailUrl +"\"> view the New Tip Lead slip.</a>";
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
            }

           $scope.GoToViewCommissionslip = (commissionid, edit): void => {
                HeaderFactory.SetTitle("Sales Rep - View Commission Slip");
                $scope.SelectedTab = 'commission';
                $scope.Iseditable = edit;
                $scope.page = $scope.pages.ViewCommissionslip;
                $scope.TipLeadRow = [];         
                this.SalesComService.GetCommissionbyID(commissionid).then((data: ng.IHttpPromiseCallbackArg<CommissionComponent[]>): void => {
                    $scope.Commissionslips = <CommissionComponent[]>data;
                    $scope.Commissionslip = $scope.Commissionslips[0];
                    $scope.Commissionslip.Comments = ""; // Empty for new comment
                    $scope.Commissionslip.EntryDate = $scope.Commissionslip.EntryDate.split("-");
                    $scope.Commissionslip.EntryDate = $scope.Commissionslip.EntryDate[1] + '/' + $scope.Commissionslip.EntryDate[2].substring(0, 2) + '/' + $scope.Commissionslip.EntryDate[0];
                    $scope.Commissionslip.DateofSale = $scope.Commissionslip.DateofSale.split("-");
                    $scope.Commissionslip.DateofSale = $scope.Commissionslip.DateofSale[1] + '/' + $scope.Commissionslip.DateofSale[2].substring(0, 2) + '/' + $scope.Commissionslip.DateofSale[0];
                    //$scope.Commissionslip.AccountPeriod = $scope.Commissionslip.AccountPeriod.split("-");
                    //$scope.Commissionslip.AccountPeriod = $scope.Commissionslip.AccountPeriod[1] + '/' + $scope.Commissionslip.AccountPeriod[0];
                   var Commissionlist: CommissionComponent[];
                   $scope.Commissionslips = $scope.Commissionslips.filter(function (commission) {
                       return (commission.SlipType == SlipTypes.TipLeadSlip);
                   });
                   var Tipslip: TipLeadSlip;
                   if ($scope.Commissionslips.length > 0) {
                       $.each($scope.Commissionslips, function (key, value: any) {
                           Tipslip = new TipLeadSlip;
                           Tipslip.ID = value.ID;
                           Tipslip.MainCommissionID = value.MainCommissionID
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
                       Tipslip = new TipLeadSlip;
                       Tipslip.TipLeadAmount = 0.00;
                       Tipslip.PositiveAdjustments = 0.00;
                       Tipslip.NegativeAdjustments = 0.00;
                       Tipslip.CompanyContribution = 0.00;
                       Tipslip.TotalCEarned = 0.00;
                       $scope.TipLeadRow.push(Tipslip);
                   }
                });
                $scope.GetSaletypelist();
            }

           $scope.SendMail = (): void => {
                if ($scope.MailContent.RecipientTo != '' || $scope.MailContent.RecipientTo != null) {
                    this.SalesComService.SendMail($scope.MailContent).then((data: ng.IHttpPromiseCallbackArg<Boolean>): void => {
                    });
                }
            }

           $scope.DeleteMessage = (Commission): void => {
                $scope.ShowAYSDialog("Are You Sure?", "All the commission slip details will be erased, are you sure to delete?", function () {
                    $scope.DeleteCommission(Commission);
                });
           }

           $scope.onReportYearChange = (year): void => {
               $scope.EditPayroll = [];
               if (year == 0) {
                   $scope.EditPayroll = [];
               }
               else {
                   this.SalesComService.GetPayrollForEdit(year).then((data: ng.IHttpPromiseCallbackArg<Payroll[]>): void => {
                       $scope.EditPayroll = <Payroll[]>data;

                       $scope.EditPayroll.sort();

                   });
               }
           }

           //$scope.onReportPeriodChange = (): void => {
           //    debugger;
           //    var example = $scope.EditPayroll.filter(function (key, index) {
           //        if (key.ID = $scope.ReportInputparameter.ReportPeriod) {
           //            return key.DateFrom;
           //        }
           //    });

           //    alert(example);
           //}

           $scope.GoToSalesRepReports = (): void => {
               HeaderFactory.SetTitle("Sales Representative - Reports");
               $scope.SelectedTab = 'reports';
               $scope.page = $scope.pages.SalesRepReports;
               $scope.Commissionslips = [];
               $scope.Salespersonslist = [];
               $scope.SRSalespersons = [];
               $scope.TipLeadlist = [];
               $scope.ReportInputparameter = new ReportParameters;
               if ($scope.LoggedinEmployee.RoleID == RoleTypes.SalesManager || $scope.LoggedinEmployee.RoleID == RoleTypes.GeneralManager || $scope.LoggedinEmployee.RoleID == RoleTypes.PayRoll) {
                   $scope.parameter = [$scope.LoggedinEmployee.UID, $scope.LoggedinEmployee.RoleID];
                   this.SalesComService.GetSalesReplistforReport($scope.parameter).then((data: ng.IHttpPromiseCallbackArg<EmployeeComponant[]>): void => {
                       $scope.Salespersonslist = <EmployeeComponant[]>data;
                       $scope.SRSalespersons = $scope.Salespersonslist.filter(function (person) {
                           return (person.RoleID == RoleTypes.SalesRep);
                       });
                       $scope.TipLeadlist = $scope.Salespersonslist.filter(function (person) {
                           return (person.RoleID == RoleTypes.SalesRep || person.RoleID == RoleTypes.NonSalesEmployee);
                       });
                   });
               }
               var currentdate = new Date();
               var currentyear = currentdate.getFullYear();

               $scope.PayrollDS = [];
               $scope.ReportdropdownConfigyear = [];
               this.SalesComService.GetPayrollForDashBoard().then((data: ng.IHttpPromiseCallbackArg<Payroll[]>): void => {
                   debugger;
                   $scope.PayrollDS = <Payroll[]>data;
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
           }

           $scope.GoToPaylocityGlReports = (): void => {
                HeaderFactory.SetTitle("Paylocity/GL - Reports");
                $scope.SelectedTab = 'reports';
                $scope.page = $scope.pages.PaylocityGlReports;
                $scope.Reports = new ReportParameters();
                $scope.Reports.Type = 1;
                $scope.PendingStatus = 0;
                $scope.PaylocityExportList = [];
                $scope.GLExportList = [];
                $scope.GetBranchlist();

                var currentdate = new Date();
                var currentyear = currentdate.getFullYear();
                $scope.ReportdropdownConfigyear = [];
                this.SalesComService.GetPayrollForDashBoard().then((data: ng.IHttpPromiseCallbackArg<Payroll[]>): void => {
                    debugger;
                    $scope.PayrollDS = <Payroll[]>data;
                    for (var i = 0; i <= $scope.PayrollDS.length - 1; i++) {
                        if ($scope.PayrollDS[i].Year <= currentyear) {
                            $scope.ReportdropdownConfigyear.push($scope.PayrollDS[i]);
                        }
                    }
                });
           }

           $scope.GetCountforPaylocity = (): void => {
               //var reportperiod = $scope.Reports.ReportPeriod.split("/");
               //$scope.Reports.ReportMonth = reportperiod[0];//2;
               //$scope.Reports.ReportYear = reportperiod[1];
               var count: number[];
               this.SalesComService.GetCountforPaylocity($scope.Reports).then((data: ng.IHttpPromiseCallbackArg<number[]>): void => {
                   count = <number[]>data;
                   if (count[0] == 0 && count[1] == 0) {
                       $scope.PendingStatus = 0;
                   }
                   else if (count[1] > 0) {
                       $scope.PendingStatus = 1;
                   }
                   else if(count[0]>0 &&count[1]==0) {
                       $scope.PendingStatus = 2;
                   }
                   $scope.Pendingcount = count[1];
               });
           }

           $scope.ViewPaylocityGLReport = (): void => {
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
                   } else {
                       $scope.ReportType = 1;
                       this.SalesComService.ViewPaylocityReport($scope.Reports).then((data: ng.IHttpPromiseCallbackArg<PaylocityReports[]>): void => {
                           $scope.PaylocityExportList = <PaylocityReports[]>data;
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
                   this.SalesComService.ViewGLReport($scope.Reports).then((data: ng.IHttpPromiseCallbackArg<PaylocityReports[]>): void => {

                       $scope.GLExportList = <GLReports[]>data;
                       $scope.Reports.ReportMonth = selecteddate.getMonth() + 1;
                       //$scope.Reports.ReportYear = selecteddate.getFullYear();
                       $.each($scope.GLExportList, function (key, value) {
                           value.Month = $scope.Reports.ReportMonth + '' + $scope.Reports.ReportYear;
                       });
                       
                   });
               }
           }

           $scope.DeleteCommission = (Commission): void => {
                HeaderFactory.SetTitle("Pay Plan Details");
                $scope.page = $scope.pages.PayPlanDetails;
                Commission.IsActive = false;
                Commission.ModifiedBy = $scope.LoggedinEmployee.UID;
                this.SalesComService.DeleteCommission(Commission).then((data: ng.IHttpPromiseCallbackArg<Boolean>): void => {
                    if (<boolean>data == true) {
                        $.bootstrapGrowl("Commission deleted successfully.", { type: 'success' });
                        $scope.GoToCommissionslipDetails();
                    }
                });
            }

           $scope.ShowAYSDialog = (title, message, callback): void => {
                var modalInstance = $uibModal.open(
                    {
                        templateUrl: 'App/views/m/AreYouSure.html',
                        controller: 'AYSDialogController',
                        backdrop: 'static',
                        scope: $scope,
                        resolve:
                        {
                            title: function () {
                                return title;
                            },
                            message: function () {
                                return message;
                            }
                        }
                    });

                modalInstance.result.then((success) => {
                    if (success) {
                        callback();
                    }
                }, () => {
                });
            }

           $scope.ShowPayPlan = (title): void => {
                
                var modalInstance = $uibModal.open(
                    {
                        templateUrl: 'App/views/m/ModalPayPlan.html',
                        controller: 'PayPlanDialogController',
                        backdrop: 'static',
                        scope: $scope,
                        resolve:
                        {
                            title: function () {
                                return title;
                            },
                        }
                    });

                modalInstance.result.then((success) => {
                    if (success) {
                        
                    }
                }, () => {
                });
            }

           $scope.ModalViewPayPlan = (planID): void => {
                $scope.Plandetail = new Plancomponent();
                $scope.Plandetail.PlanID = planID;
                
                this.SalesComService.GetPlanbyID($scope.Plandetail).then((data: ng.IHttpPromiseCallbackArg<Plancomponent>): void => {
                    
                    $scope.Plandetail = <Plancomponent>data;
                    $scope.Plandetail.BasisType = Basistype[$scope.Plandetail.BasisType];
                    
                    $scope.Plandetail.BMQuotaBonus = Booleanvalues[$scope.Plandetail.BMQuotaBonus == true ? 1 : 0];
                    $scope.Plandetail.TenureBonus = Booleanvalues[$scope.Plandetail.TenureBonus == true ? 1 : 0];
                    $scope.Plandetail.SMEligible = Booleanvalues[$scope.Plandetail.SMEligible == true ? 1 : 0];
                });
                $scope.ShowPayPlan("View Pay Plan");
            }

           $scope.ModalDeactivatedPayPlan = (): void => {
               $scope.Employeedetail = new EmployeeComponant();

               this.SalesComService.GetDeActivatePlanEmployees().then((data: ng.IHttpPromiseCallbackArg<EmployeeComponant>): void => {

                   $scope.Employeedetail = <EmployeeComponant>data;
               });
               $scope.ShowDeactivatedPayPlan("View Deactivated Pay Plan");
           }

           $scope.ShowDeactivatedPayPlan = (title): void => {
               var modalInstance = $uibModal.open(
                   {
                       templateUrl: 'App/views/m/DeactivatedPayPlan.html',
                       controller: 'PayPlanDialogController',
                       backdrop: 'static',
                       scope: $scope,
                       resolve:
                       {
                           title: function () {
                               return title;
                           },
                       }
                   });

               modalInstance.result.then((success) => {
                   if (success) {

                   }
               }, () => {
               });
           }

           $scope.ViewRolesItems = function () {
                    $scope.GetRoleslist();
                }

           $scope.GetRoleslist = (): void => {
                    $scope.Roleslists = [];
                    $scope.rolelist = [];
                    this.SalesComService.GetRoles().then((data: ng.IHttpPromiseCallbackArg<Roles[]>): void => {
                        $scope.Roleslists = <Roles[]>data;
                        if ($scope.LoggedinEmployee.RoleID == RoleTypes.Administrator) {
                            $scope.rolelist = $scope.Roleslists.filter(function (roles) {

                                return (roles.RoleID == RoleTypes.Administrator || roles.RoleID == RoleTypes.PayRoll);
                            });
                        } else if ($scope.LoggedinEmployee.RoleID == RoleTypes.PayRoll) {
                            $scope.rolelist = $scope.Roleslists.filter(function (roles) {

                                return (roles.RoleID == RoleTypes.SalesManager || roles.RoleID == RoleTypes.GeneralManager || roles.RoleID == RoleTypes.SalesRep || roles.RoleID == RoleTypes.NonSalesEmployee);
                            });
                        }
                    });
                }

           $scope.GetBranchlist = (): void => {
               $scope.Brancheslists = [];
               $scope.SecondaryBrancheslists = [];
                    this.SalesComService.GetBranches().then((data: ng.IHttpPromiseCallbackArg<Branches[]>): void => {
                        $scope.Brancheslists = <Branches[]>data;
                        $scope.SecondaryBrancheslists = $scope.Brancheslists;
                        $scope.SecondaryBrancheslists = $scope.SecondaryBrancheslists.filter(function (branch) {
                            return (branch.IsActive == true);
                        });
                    });
           }

           $scope.GetSaletypelist = (): void => {
               $scope.SaleTypeList = [];
               this.SalesComService.GetAllSaleType().then((data: ng.IHttpPromiseCallbackArg<SaleType[]>): void => {
                   $scope.SaleTypeList = <SaleType[]>data;
               });
           }

           $scope.GetAllManagerlist = (): void => {
                    $scope.Managerslists = [];
                    $scope.SalesManagerslists = [];
                    $scope.GeneralManagerslists = [];
                    this.SalesComService.GetAllManager().then((data: ng.IHttpPromiseCallbackArg<EmployeeComponant[]>): void => {
                        $scope.Managerslists = <EmployeeComponant[]>data;
                        $scope.SalesManagerslists = $scope.Managerslists.filter(function (employee) {
                            return (employee.RoleID == RoleTypes.SalesManager);
                        });
                        $scope.GeneralManagerslists = $scope.Managerslists.filter(function (employee) {

                            return (employee.RoleID == RoleTypes.GeneralManager);
                        });
                    });
                }
            
           $scope.GetPlanlist = (): void => {
                    $scope.Planlists = [];
                    this.SalesComService.GetPlans().then((data: ng.IHttpPromiseCallbackArg<Plancomponent[]>): void => {
                        
                        var planList = $scope.Planlists;
                        planList = <Plancomponent[]>data;
                        $scope.Planlists = planList.filter(function (plan) {                            
                            return (plan.IsActive == true);
                        });
                    });
           }

           $scope.SRReportTypes = [{ value: 1, name: "Commission Details" }, { value: 2, name: "Total Earnings" }];
           $scope.SMGMReportTypes = [{ value: 1, name: "Commission Details" }, { value: 2, name: "Total Earnings" }, { value: 3, name: "Incentive Trips" }];
           $scope.Getcommissionreport = (): void => {
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
               $scope.Employeedetail = new EmployeeComponant;
               var MonthsofExperience = 0;
               if ($scope.LoggedinEmployee.RoleID == RoleTypes.SalesRep || $scope.LoggedinEmployee.RoleID == RoleTypes.NonSalesEmployee) {
                   $scope.ReportInputparameter.SalesPerson = $scope.LoggedinEmployee.UID;//need to change
                   $scope.Employeedetail.UID = $scope.LoggedinEmployee.UID;//need to change
               }
               else {
                   $scope.Employeedetail.UID = $scope.ReportInputparameter.SalesPerson;
               }
               if ($scope.ReportInputparameter.ReportType == undefined) {
                   $.bootstrapGrowl("Please select Report Type.", { type: 'danger' });
                   $scope.ReportError = -1;
               } else {
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
                               $scope.ReportInputparameter.ReportMonth = reportperiod[0];//2;
                               $scope.ReportInputparameter.ReportYear = reportperiod[1];
                               this.SalesComService.GetEmployeebyUID($scope.Employeedetail).then((data: ng.IHttpPromiseCallbackArg<EmployeeComponant>): void => {
                                   $scope.Employeedetail = <EmployeeComponant>data;
                                   var dateinposition = new Date($scope.Employeedetail.DateInPosition);
                                   //var selectedMonth = selecteddate.getMonth() + 1;
                                   //$scope.ReportInputparameter.ReportMonth = selectedMonth;
                                   debugger;
                                   var selecteddate = new Date($scope.ReportInputparameter.ReportYear, $scope.ReportInputparameter.ReportMonth - 1, 1);
                                   var positiondate = new Date(dateinposition.getFullYear(), dateinposition.getMonth(), 1);
                                   $scope.ReportPeriodNote = Months[selecteddate.getMonth()] + '/' + selecteddate.getFullYear();
                                   if (positiondate > selecteddate) {//selected date is less than date in position
                                       var dateinpositionmonth = dateinposition.getMonth() + 1;
                                       $scope.Empdateinposition = dateinposition.getDate() + '/' + dateinpositionmonth + '/' + dateinposition.getFullYear();;
                                       $scope.ReportError = 1;
                                   }
                                   else {
                                       debugger;
                                       $scope.ReportError = 0;
                                       MonthsofExperience = (selecteddate.getFullYear() - dateinposition.getFullYear()) * 12 + (selecteddate.getMonth() - dateinposition.getMonth())+1;
                                       if (MonthsofExperience > 0 && dateinposition.getDate() > 5) {
                                           MonthsofExperience = MonthsofExperience - 1;
                                       }
                                       $scope.ReportInputparameter.PlanID = parseInt($scope.Employeedetail.PayPlanID);
                                       $scope.ReportInputparameter.MonthsofExp = MonthsofExperience;

                                       this.SalesComService.GetCommissionReport($scope.ReportInputparameter).then((data: ng.IHttpPromiseCallbackArg<CommissionComponent[]>): void => {
                                           $scope.Commissionslips = <CommissionComponent[]>data;
                                       });
                                       this.SalesComService.GetCommissionslipReportDetails($scope.ReportInputparameter).then((data: ng.IHttpPromiseCallbackArg<ReportParameters[]>): void => {
                                           $scope.CommissionReportDetails = <ReportParameters[]>data;
                                           let finalRunNo = $scope.CommissionReportDetails.length-1;
                                           $scope.DrawReportDetails = new ReportParameters();
                                           $scope.DrawReportDetails = $scope.CommissionReportDetails[finalRunNo];
                                           //$scope.CommissionReportDetails.DrawType = ($scope.CommissionReportDetails.DrawType == 1 ? "Guaranteed" : ($scope.CommissionReportDetails.DrawType == 2 ?"Recoverable":"-"));
                                           //$scope.CommissionReportDetails.RecoverablePercent = $scope.CommissionReportDetails.RecoverablePercent * 100;
                                           $scope.DrawReportDetails.DrawType = ($scope.DrawReportDetails.DrawType == 1 ? "Guaranteed" : ($scope.DrawReportDetails.DrawType == 2 ? "Recoverable" : "-"));
                                           $scope.DrawReportDetails.RecoverablePercent = $scope.DrawReportDetails.RecoverablePercent * 100;
                                           $scope.TotalReportValue = new ReportParameters();
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
                                               $scope.TotalReportValue.Salary = + value.Salary + $scope.TotalReportValue.Salary;
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
                           //else if ($scope.ReportInputparameter.ReportTo == undefined) {
                           //    $.bootstrapGrowl("Please select To Period.", { type: 'danger' });
                           //    $scope.ReportError = -1;
                           //}
                           //else if (ReportFrom > ReportTo) {
                           //    $.bootstrapGrowl("From Period should be less than To Period.", { type: 'danger' });
                           //    $scope.ReportError = -1;
                           //}
                           else {
                               $scope.ReportError = 0;
                               this.SalesComService.GetEmployeebyUID($scope.Employeedetail).then((data: ng.IHttpPromiseCallbackArg<EmployeeComponant>): void => {
                                   $scope.Employeedetail = <EmployeeComponant>data;
                               });
                               let nextyear: number = $scope.ReportInputparameter.ReportYear;
                               nextyear++;
                               $scope.ReportPeriodNote = "Sep/" + $scope.ReportInputparameter.ReportYear + " - Aug/" + nextyear;
                               $scope.ReportInputparameter.ReportPeriod = $scope.currentDt;
                                //  $scope.ReportInputparameter.ReportPeriod = new Date($scope.ReportInputparameter.ReportFrom);
                             //  var reportdate = new Date($scope.ReportInputparameter.ReportPeriod);
                             //  var nextyear = reportdate.getFullYear() + 1;
                               //$scope.ReportPeriodNote = "Sep/" + $scope.ReportInputparameter.ReportYear + " - Aug/" + nextyear;
                               //$scope.ReportPeriodNote = periodMonthDetail;// $scope.ReportInputparameter.ReportMonth;// Months[ReportFrom.getMonth()] + "/" + ReportFrom.getFullYear() + " - " + Months[ReportTo.getMonth()] + "/" + ReportTo.getFullYear();
                               this.SalesComService.GetTotalEarningReport($scope.ReportInputparameter).then((data: ng.IHttpPromiseCallbackArg<ReportParameters[]>): void => {
                                   $scope.ReportTotalEarnings = <ReportParameters[]>data;

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
                               var inputparameter: number[]
                               inputparameter = [$scope.LoggedinEmployee.UID, $scope.LoggedinEmployee.RoleID, $scope.ReportInputparameter.ReportYear];
                               this.SalesComService.GetIncentiveTripReport(inputparameter).then((data: ng.IHttpPromiseCallbackArg<CommissionComponent[]>): void => {
                                   $scope.Commissionslips = <CommissionComponent[]>data;
                               });
                           }
                       }
                   }
               }
           }
           $scope.SaveReportConfirmation = (ReportInputparameter): void => {
               $scope.ShowAYSDialog("Are You Sure?", "Exporting paylocity report will change all the related commission slip status as ‘Processed’, Do you want to continue?", function () {
                   angular.element('#Paylocitybutton').trigger('click');
                   $scope.SaveReport(ReportInputparameter);
               });
           }
           $scope.SaveReport = (ReportInputparameter): void => {
               var periodMonthDetail;
               $.each($scope.EditPayroll, function (key, value) {
                   if (value.ID == ReportInputparameter.PayrollConfigID) {
                       ReportInputparameter.ReportPeriod = new Date(value.DateTo);
                       periodMonthDetail = value.Month;
                   }
               });
               this.SalesComService.SaveReportdetails(ReportInputparameter).then((data: ng.IHttpPromiseCallbackArg<Boolean>): Boolean => {
                   return <Boolean>data;
               });
           }
           $scope.ModalEditReportCommission = (commissionID): void => {
               $scope.Brancheslists = [];
               $scope.Salespersonslist = [];
               $scope.SRSalespersons = [];
               $scope.TipLeadRow = [];
               $scope.Commissionslip = new CommissionComponent();
               $scope.Commissionslip.CustomerType = CustomerTypes.NewCustomer;
               $scope.Commissionslip.TipLeadAmount = 0.00;
               $scope.Commissionslip.BaseCommission = 0.00;
               $scope.Commissionslip.LeaseCommission = 0.00;
               $scope.Commissionslip.ServiceCommission = 0.00;
               $scope.Commissionslip.TravelCommission = 0.00;
               $scope.Commissionslip.CashCommission = 0.00;
               $scope.Commissionslip.SpecialCommission = 0.00;
               var commissionslips: CommissionComponent[];
               var roleID = RoleTypes.PayRoll;
               this.SalesComService.GetCommissionbyID(commissionID).then((data: ng.IHttpPromiseCallbackArg<CommissionComponent[]>): void => {
                   commissionslips = <CommissionComponent[]>data;
                   $scope.Commissionslip = commissionslips[0];
                   $scope.Commissionslip.Comments = ""; // Empty for new comment
                   $scope.Commissionslip.EntryDate = $scope.Commissionslip.EntryDate.split("-");
                   $scope.Commissionslip.EntryDate = $scope.Commissionslip.EntryDate[1] + '/' + $scope.Commissionslip.EntryDate[2].substring(0, 2) + '/' + $scope.Commissionslip.EntryDate[0];
                   $scope.Commissionslip.DateofSale = $scope.Commissionslip.DateofSale.split("-");
                   $scope.Commissionslip.DateofSale = $scope.Commissionslip.DateofSale[1] + '/' + $scope.Commissionslip.DateofSale[2].substring(0, 2) + '/' + $scope.Commissionslip.DateofSale[0];
                   $scope.Commissionslip.AccountPeriod = $scope.Commissionslip.AccountPeriod.split("-");
                   $scope.Commissionslip.AccountPeriod = $scope.Commissionslip.AccountPeriod[1] + '/' + $scope.Commissionslip.AccountPeriod[0];
                   $scope.Commissionslip.BranchID = String($scope.Commissionslip.BranchID);

                   let splitedBranch: any[];
                   splitedBranch = [];
                   $scope.EmployeeBranch = [];
                   $scope.Employeedetail = new EmployeeComponant;
                   $scope.Employeedetail.UID = $scope.Commissionslip.CreatedBy;
                   this.SalesComService.GetEmployeebyUID($scope.Employeedetail).then((data: ng.IHttpPromiseCallbackArg<EmployeeComponant>): void => {
                       $scope.Employeedetail = <EmployeeComponant>data;
                       splitedBranch = $scope.Employeedetail.SecondaryBranch[0].split(',');
                       splitedBranch.push($scope.Employeedetail.PrimaryBranch);
                       this.SalesComService.GetBranches().then((data: ng.IHttpPromiseCallbackArg<Branches[]>): void => {
                           $scope.Brancheslists = <Branches[]>data;
                           $.each($scope.Brancheslists, function (key, value: any) {
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

                   this.SalesComService.GetAllEmployeeDetailsbyRoleID(roleID).then((data: ng.IHttpPromiseCallbackArg<EmployeeComponant[]>): void => {
                       $scope.Salespersonslist = <EmployeeComponant[]>data;
                       $scope.SRSalespersons = $scope.Salespersonslist.filter(function (person) {
                           return (person.RoleID == RoleTypes.SalesRep);
                       });
                   });
                   $.each($scope.SRSalespersons, function (key, value: any) {
                       $scope.splitnamelist.push(value.LastName);
                   });
                   var Commissionlist: CommissionComponent[];
                   commissionslips = commissionslips.filter(function (commission) {
                       return (commission.SlipType == SlipTypes.TipLeadSlip);
                   });
                   var Tipslip: TipLeadSlip;
                   if (commissionslips.length > 0) {
                       $.each(commissionslips, function (key, value: any) {
                           Tipslip = new TipLeadSlip;
                           Tipslip.ID = value.ID;
                           Tipslip.MainCommissionID = value.MainCommissionID
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
                       Tipslip = new TipLeadSlip;
                       Tipslip.TipLeadAmount = 0.00;
                       Tipslip.PositiveAdjustments = 0.00;
                       Tipslip.NegativeAdjustments = 0.00;
                       Tipslip.CompanyContribution = 0.00;
                       Tipslip.TotalCEarned = 0.00;
                       $scope.TipLeadRow.push(Tipslip);
                   }
                   $scope.MainCommissionStatus = 0;
                   if ($scope.Commissionslip.SlipType == SlipTypes.TipLeadSlip) {//To get status of corresponding commission slip
                       var Commissionslips: CommissionComponent[];
                       this.SalesComService.GetCommissionbyID($scope.Commissionslip.MainCommissionID).then((data: ng.IHttpPromiseCallbackArg<CommissionComponent[]>): void => {
                           Commissionslips = <CommissionComponent[]>data;
                           if (Commissionslips[0].ProcesByPayroll == true) {
                               $scope.MainCommissionStatus = 1;
                           } else if (Commissionslips[0].Status == 7) {
                               $scope.MainCommissionStatus = 2;
                           }
                       });

                   }
               });
               $scope.ShowEditReportCommission("Edit Commission Details", function () { $scope.EditReportCommission(); });
           }

           $scope.ShowEditReportCommission = (title,callback): void => {

               var modalInstance = $uibModal.open(
                   {
                       templateUrl: 'App/views/m/ModalReportCommission.html',
                       controller: 'ReportEditCommissionController',
                       backdrop: 'static',
                       scope: $scope,
                       resolve:
                       {
                           title: function () {
                               return title;
                           },
                       }
                   });

               modalInstance.result.then((success) => {
                   if (success) {
                       callback();
                   }
               }, () => {
               });
           }

           $scope.EditReportCommission = (): Boolean => {
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
                   if ($scope.Commissionslip.SlipType == SlipTypes.CommissionSlip) {
                       var error: number = 0;
                       $.each($scope.TipLeadRow, function (key, value) {
                           if ((value.TipLeadID == undefined || value.TipLeadID == 0) && value.TipLeadAmount != 0 && value.TipLeadAmount != undefined) {
                               error = 1;
                           } else if ((value.TipLeadAmount == undefined || value.TipLeadAmount == 0) && value.TipLeadID != undefined && value.TipLeadID != 0) {
                               error = 2;
                           } else if ((value.TipLeadID == undefined || value.TipLeadID == 0) && ((value.PositiveAdjustments != 0 && value.PositiveAdjustments != undefined) || (value.NegativeAdjustments != 0 && value.NegativeAdjustments != undefined))) {
                               error = 1;
                           } else if ((value.TipLeadAmount == undefined || value.TipLeadAmount == 0) && ((value.PositiveAdjustments != 0 && value.PositiveAdjustments != undefined) || (value.NegativeAdjustments != 0 && value.NegativeAdjustments != undefined))) {
                               error = 2;
                           }
                       });
                       if (error == 1) {
                           $.bootstrapGrowl("Please select Tip Lead", { type: 'danger' });
                           $scope.ShowEditReportCommission("Edit Commission Details", function () { $scope.EditReportCommission(); });
                           return false;
                       } else if (error == 2) {
                           $.bootstrapGrowl("Please enter Tip Lead Amount", { type: 'danger' });
                           $scope.ShowEditReportCommission("Edit Commission Details", function () { $scope.EditReportCommission(); });
                           return false;
                       }
                       $scope.Commissionslip.TipLeadSliplist = $scope.TipLeadRow.filter(function (Tiplead) {
                           return (Tiplead.TipLeadID != undefined && Tiplead.TipLeadID != 0);
                       });
                       $.each($scope.Commissionslip.TipLeadSliplist, function (key, value) {
                           value.SlipType = SlipTypes.TipLeadSlip;
                           value.Status = ((value.Status != 7 && value.Status != 5 && !value.ProcesByPayroll) && ($scope.Commissionslip.Status == 2 || $scope.Commissionslip.Status == 1)) ? $scope.Commissionslip.Status : value.Status;
                       });

                       $scope.Commissionslip.CommentedBy = $scope.LoggedinEmployee.FirstName + ' ' + $scope.LoggedinEmployee.LastName;
                       this.SalesComService.EditCommission($scope.Commissionslip).then((data: ng.IHttpPromiseCallbackArg<Boolean>): void => {
                           if (<Boolean>data == true) {
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
                       this.SalesComService.EditTipLeadSlip($scope.Commissionslip).then((data: ng.IHttpPromiseCallbackArg<Boolean>): void => {
                           if (<Boolean>data == true) {
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
           
           }

           $scope.CheckUserLogin = (): any => {
               
                    //Check user only if userid is zero
                    if ($scope.LoggedinEmployee == null || $scope.LoggedinEmployee.UID == 0) {
                        this.InitializeService.CheckUserLogin().then((data: ng.IHttpPromiseCallbackArg<EmployeeComponant>): void => {

                        $scope.LoggedinEmployee = <EmployeeComponant>data;
                        if ($scope.LoggedinEmployee.UID == 0) {
                            $.bootstrapGrowl("You are not authorized to log in to the System, contact Payroll.", { type: 'danger' });
                        }
                        else {
                            $scope.GetEmployeebyUID($scope.LoggedinEmployee)
                        }
                    });
                }
            }

           $scope.GetEmployeebyUID = (emp): void => {

                this.SalesComService.GetEmployeebyUID(emp).then((data: ng.IHttpPromiseCallbackArg<EmployeeComponant>): void => {
                    $scope.LoggedinEmployee = <EmployeeComponant>data;
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
           }


           //To display Branch details in grid view
           $scope.GoToBranchDetails = (): void => {
               debugger;
               HeaderFactory.SetTitle("Branch Details");
               $scope.active = "active";
               $scope.page = $scope.pages.BranchDetails;
               $scope.SelectedTab = 'branch';
               $scope.NewBranchRow = [];
               this.SalesComService.GetBranches().then((data: ng.IHttpPromiseCallbackArg<Branches[]>): void => {
                   $scope.NewBranchRow = <Branches[]>data;
               });
           }
           
           $scope.AddBranchRow = function () {
               debugger;
               $scope.Branch = new Branches();
               $scope.Branch.IsActive = true;
               //$scope.NewBranchRow.push(new Branches);
               $scope.NewBranchRow.unshift($scope.Branch);
              // angular.element('#BranchName_' + ($scope.NewBranchRow.length-1)).focus();
           }

           $scope.SetBranch = (): void => {
               this.SalesComService.SetBranch($scope.NewBranchRow).then((data: ng.IHttpPromiseCallbackArg<Boolean>): void => {
                   if (<Boolean>data == true) {
                       $.bootstrapGrowl("Branch details saved successfully.", { type: 'success' });
                   }
                   $scope.GoToBranchDetails();
               })
           }

           //To display PayrollConfiguration
           $scope.GoToPayrollConfigurationDetails = (): void => {

               HeaderFactory.SetTitle("Payroll Configuration");
               $scope.page = $scope.pages.PayrollConfigurationDetails;
               $scope.SelectedTab = 'payrollconfiguration';
               $scope.PayrollDS = [];
               this.SalesComService.GetPayrollForDashBoard().then((data: ng.IHttpPromiseCallbackArg<Payroll[]>): void => {
                   debugger;
                   $scope.PayrollDS = <Payroll[]>data;
               });
           }


           //To display Add PayrollConfiguration
           $scope.GoToAddPayrollConfiguration = (): void => {
               $scope.Payrollconfigrow = [];
               $scope.TempPayroll = new Payroll;
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
           }

               $scope.AddPayroll = (): void => {
                   this.SalesComService.AddPayroll($scope.Payrollconfigrow).then((data: ng.IHttpPromiseCallbackArg<Boolean>): void => {
                       if (<Boolean>data == true) {
                           $.bootstrapGrowl("Accounting period details added successfully.", { type: 'success' });
                       }
                       $scope.GoToPayrollConfigurationDetails();
                   })
               }
           
           //To display Edit PayrollConfiguration
           $scope.GoToEditPayrollConfiguration = (year): void => {
                 $scope.EditPayroll = [];
               $scope.TempPayroll = new Payroll();
               HeaderFactory.SetTitle("Edit Payroll Configuration");
               $scope.page = $scope.pages.EditPayrollConfiguration;
               $scope.SelectedTab = 'payrollconfiguration';
               var currentdate = new Date();
               var currentyear = currentdate.getFullYear();
               var nextyear = currentyear + 1;
               var prevyear = currentyear - 1;
               $scope.Reportdropdownyear = [{ value: prevyear - 1, name: prevyear - 1 }, { value: currentyear - 1, name: currentyear - 1 }, { value: currentyear, name: currentyear }, { value: nextyear, name: nextyear }, { value: nextyear + 1, name: nextyear + 1 }];
               this.SalesComService.GetPayrollForEdit(year).then((data: ng.IHttpPromiseCallbackArg<Payroll[]>): void => {
                   debugger;
                   $scope.EditPayroll = <Payroll[]>data;
                   $scope.TempPayroll.Year = $scope.EditPayroll[0].Year;
                   $scope.TempPayroll.CreatedByName = $scope.EditPayroll[0].CreatedByName;
                   if ($scope.EditPayroll[0].ModifiedByName == "") {
                       $scope.TempPayroll.ModifiedByName = $scope.EditPayroll[0].CreatedByName;
                   } else {
                       $scope.TempPayroll.ModifiedByName = $scope.EditPayroll[0].ModifiedByName;
                   }
                   //$.each($scope.EditPayroll, function (key, value) {
                   //    //alert(value.DateTo.getDay());
                   //    value.Period = 1;
                   //    value.Day = 10;
                   //});   

               });

           }

           $scope.UpdatePayroll = (): void => {
               //To set modified ID
               $.each($scope.EditPayroll, function (key, value) {
                   value.ModifiedBy = $scope.LoggedinEmployee.UID;
               });
               this.SalesComService.UpdatePayroll($scope.EditPayroll).then((data: ng.IHttpPromiseCallbackArg<Boolean>): void => {
                   if (<Boolean>data == true) {
                       $.bootstrapGrowl("Payroll updated successfully.", { type: 'success' });
                   }
                   $scope.GoToPayrollConfigurationDetails();
               })
           }

           //To display Edit PayrollConfiguration
           $scope.GoToViewPayrollConfiguration = (year): void => {
               $scope.ViewPayroll = [];
               $scope.TempPayroll = new Payroll();
               HeaderFactory.SetTitle("View Payroll Configuration");
               $scope.page = $scope.pages.ViewPayrollConfiguration;
               $scope.SelectedTab = 'payrollconfiguration';
               this.SalesComService.GetPayrollForEdit(year).then((data: ng.IHttpPromiseCallbackArg<Payroll[]>): void => {
                   $scope.ViewPayroll = <Payroll[]>data;
                   $scope.TempPayroll.CreatedByName = $scope.ViewPayroll[0].CreatedByName;
                   if ($scope.ViewPayroll[0].ModifiedByName == "") {
                       $scope.TempPayroll.ModifiedByName = $scope.ViewPayroll[0].CreatedByName;
                   } else {
                       $scope.TempPayroll.ModifiedByName = $scope.ViewPayroll[0].ModifiedByName;
                   }
               });
           }

           //To display Sale Type details in grid view
           $scope.GoToSaleTypeDetails = (): void => {
               
               HeaderFactory.SetTitle("Sale Type Details");
               $scope.page = $scope.pages.SaleTypeDetails;
               $scope.SelectedTab = 'saletype';
               $scope.SaleTypeList = [];
               $scope.GetSaletypelist();
               //this.SalesComService.GetAllSaleType().then((data: ng.IHttpPromiseCallbackArg<SaleType[]>): void => {
               //    $scope.SaleTypeList = <SaleType[]>data;
               //});
           }

           $scope.AddSaleTypeRow = function () {
               $scope.Sale = new SaleType();
               $scope.Sale.IsActive = true;
               //$scope.NewBranchRow.push(new Branches);
               $scope.SaleTypeList.unshift($scope.Sale);
           }


           $scope.SetSaleType = (): void => {
               debugger;
               this.SalesComService.SetSaleType($scope.SaleTypeList).then((data: ng.IHttpPromiseCallbackArg<Boolean>): void => {
                   if (<Boolean>data == true) {
                       $.bootstrapGrowl("SaleType details saved successfully.", { type: 'success' });
                   }
                   $scope.GoToSaleTypeDetails();
               })
           }
           $scope.ModalDeactivatedBranch = (branchid, index): void => {
               debugger;
               this.SalesComService.GetDeActivateBranch(branchid).then((data: ng.IHttpPromiseCallbackArg<boolean>): void => {
                   if (<Boolean>data == true) {
                       $scope.ShowAlertDialog("Location Deactivate Alert", "This location has associated commission slips for the current accounting period. Until Paylocity and General Ledger report generated, deactivation cannot occur", function () {});
                   }
                   else {
                       $scope.ShowAYSDialog("Location Deactivate Alert", "This Location will not be available for Commission Slip Entry and Report generation. You want to De Activate?", function () {
                           angular.element('#branch' + branchid).removeClass("active").addClass("deactive");
                           $scope.NewBranchRow[index].IsActive = false;
                       });
                   } 
               });
           }

           $scope.ShowAlertDialog = (title, message, callback): void => {
               var modalInstance = $uibModal.open(
                   {
                       templateUrl: 'App/views/m/ModalAlert.html',
                       controller: 'AYSDialogController',
                       backdrop: 'static',
                       scope: $scope,
                       resolve:
                       {
                           title: function () {
                               return title;
                           },
                           message: function () {
                               return message;
                           }
                       }
                   });

               modalInstance.result.then((success) => {
                   debugger;
                   if (success) {
                       callback();
                   }
               }, () => {
               });
           }

           $scope.ValidateBranch = (branchName,index): void => {
               debugger;
               for (var i = 0; i < $scope.NewBranchRow.length; i++) {
                   if (i != index && ($scope.NewBranchRow[i].BranchName.toString().toUpperCase()) == branchName.toString().toUpperCase()) {
                       $scope.NewBranchRow[index].BranchName = '';
                       $.bootstrapGrowl('Location Name already exist', { type: "danger" });                       
                   }
               }
           }

           $scope.ModalDeactivatedSaletype = (id, index): void => {
               debugger;
               $scope.checkSaleType = new CheckSaleType();
               $scope.checkSaleType.ID = id;
               $scope.checkSaleType.reason = 0;
               this.SalesComService.GetDeActivateSaletype($scope.checkSaleType).then((data: ng.IHttpPromiseCallbackArg<boolean>): void => {
                   if (<Boolean>data == true) {
                       $scope.ShowAlertDialog("Saletype Deactivate Alert", "This Saletype has associated commission slips for the current accounting period. Until Paylocity and General Ledger report generated, deactivation cannot occur", function () { $scope.active = 'deactive' });
                   }
                   else {
                       $scope.ShowAYSDialog("Saletype Deactivate Alert", "All the commission slips mapped to the Sale Type will not be considered for Bonus Calculations.You want to De Activate?", function () {
                           angular.element('#saletype' + id).removeClass("active").addClass("deactive");
                           debugger;
                           $scope.SaleTypeList[index].IsActive = false;
                       });
                   }
               });
           }

           $scope.ValidateSaletype = (saletypeName, index): void => {
               debugger;
               for (var i = 0; i < $scope.SaleTypeList.length; i++) {
                   if (i != index && ($scope.SaleTypeList[i].SaleTypeName.toString().toUpperCase()) == saletypeName.toString().toUpperCase()) {
                       $scope.SaleTypeList[index].SaleTypeName = '';
                       $.bootstrapGrowl('Sale Type Name already exist', { type: "danger" });
                   }
               }
           }

           $scope.IsNewCustomerClick = (saleType, index): void => {
               debugger;
               if (saleType.IsActive == true && saleType.IsNewCustomer == false) {
                   $scope.checkSaleType = new CheckSaleType();
                   $scope.checkSaleType.ID = saleType.ID;
                   $scope.checkSaleType.reason = 1;
                   this.SalesComService.GetDeActivateSaletype($scope.checkSaleType).then((data: ng.IHttpPromiseCallbackArg<boolean>): void => {
                       if (<Boolean>data == true) {
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
           }

           $scope.IsExistingCustomerClick = (saleType, index): void => {
               debugger;
               if (saleType.IsActive == true && saleType.IsExistingCustomer == false) {
                   $scope.checkSaleType = new CheckSaleType();
                   $scope.checkSaleType.ID = saleType.ID;
                   $scope.checkSaleType.reason = 2;
                   this.SalesComService.GetDeActivateSaletype($scope.checkSaleType).then((data: ng.IHttpPromiseCallbackArg<boolean>): void => {
                       if (<Boolean>data == true) {
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
           }

           $scope.NewPayrollCancelConfirmation = (): void => {
               $scope.ActivationMessage = "Cancel";
               $scope.ShowAYSDialog("Cancel Payroll Detail", " Are you sure to cancel?", function () { $scope.GoToPayrollConfigurationDetails(); });

           }
        }
    }
   
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Define Sales Commission Yes/No Dialog Controller                                                      //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    class AYSDialogController {
        static $inject = ['$scope', '$uibModalInstance', 'title', 'message'];
        constructor(private $scope: ISalesCommissionScope, private $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance, title: string, message: string) {
            $scope.title = title;
            $scope.message = message;

            $scope.ok = () => {
                $uibModalInstance.close('ok');
            }

            $scope.cancel = () => {
                $uibModalInstance.close(false);
                debugger;
                //$uibModalInstance.dismiss('cancel');
            }
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Define Sales Commission Pay plan Dialog Controller                                                      //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    class PayPlanDialogController {
        static $inject = ['$scope', '$uibModalInstance', 'title'];
        constructor(private $scope: ISalesCommissionScope, private $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance, title: string) {
            $scope.title = title;

            $scope.close = () => {
                $uibModalInstance.close(false);
            }
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Define  Report Edit commission slip Controller                                                      //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    class ReportEditCommissionController {
        static $inject = ['$scope', '$uibModalInstance', 'title'];
        constructor(private $scope: ISalesCommissionScope, private $uibModalInstance: ng.ui.bootstrap.IModalServiceInstance, title: string) {
            $scope.title = title;
            $scope.save = () => {
                $uibModalInstance.close('ok');
            }
            $scope.cancel = () => {
                $uibModalInstance.close(false);
            }
        }
    }


    angular
        .module('mtisalescom')
        .config(Config)
        .controller('AYSDialogController', AYSDialogController)
        .controller('PayPlanDialogController', PayPlanDialogController)
        .controller('SalesCommissionControl', SalesCommissionControl)
        .controller('ReportEditCommissionController', ReportEditCommissionController)
}