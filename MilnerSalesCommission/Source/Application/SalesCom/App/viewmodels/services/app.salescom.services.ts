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
    export class Plancomponent {
        rows: DBdRow[];
        PlanID: number;
        PlanName: string;
        BasisType: any;
        BMQuotaBonus: any;
        SMEligible: any;
        TenureBonus: any;
        IsActive: boolean;
        CreatedBy: number;
        ModifiedBy: number;
        TGPcustomerlist: TGPCustomerInfo[];
        TenureBonuslist: QuarterlyTenureBonus[];
        Bimonthlylist: BIMonthlyBonusInfo[];
        constructor() {
            this.rows = [];
        }
    }
    export class TGPCustomerInfo {
        rows: DBdRow[];
        ID: number;
        PlanID: number;
        SalesType: any;
        SalesTypeName: any;
        Percentage: any;
        CustomerType: number;
        constructor() {
            this.rows = [];
        }
    }
    export class BIMonthlyBonusInfo {
        rows: DBdRow[];
        ID: number;
        PlanID: number;
        Months: string;
        EntryPointA: number;
        EntryPointB: number;
        Percentage: any;
        Tier: string;
        constructor() {
            this.rows = [];
        }
    }
    export class QuarterlyTenureBonus {
        rows: DBdRow[];
        ID: number;
        PlanID: number;
        Months: string;
        EntryPointA: number;
        EntryPointB: number;
        Percentage: any;
        Tier: string;
        constructor() {
            this.rows = [];
        }
    }
    export enum Basistype {
        "Dollar Volume" = 1,
        "Gross Profit" = 2,
    }
    export enum Booleanvalues {
        "Yes" = 1,
        "No" = 0,
    }

    //export enum SaleType {
    //    "Cash" = 1,
    //    "Lease" = 2,
    //    "Rent" = 3,
    //    "Lease renewal" = 4,
    //}

    export enum Customertype {
        "New Customer" = 1,
        "Existing Customer" = 2,
    }
    export enum CustomerTypes {
        NewCustomer = 1,
        ExistingCustomer = 2,
    }
    export enum SlipTypes {
        CommissionSlip = 1,
        TipLeadSlip = 2,
    }
    export class TipLeadSlip {
        rows: DBdRow[];
        ID: number;
        MainCommissionID: number;
        TipLeadID: number;
        TipLeadEmpID: string;
        TipLeadName: string;
        TipLeadAmount: any;
        PositiveAdjustments: any;
        NegativeAdjustments: any;
        CompanyContribution: any;
        SlipType: number;
        TotalCEarned: any;
        Status: any;
        ProcesByPayroll: boolean;
    }
    export class CommissionComponent {
        rows: DBdRow[];
        ID: number;
        SalesPerson: string;
        DateofSale: any;
        EntryDate: any;
        InvoiceNumber: string;
        CustomerNumber: string;
        CustomerName: string;
        CommentSold: string;
        CustomerType: any;
        SplitSalePerson: string;
        SplitSalePersonID: string;
        AmountofSale: any;
        CostofGoods: any;
        DollarVolume: any;
        BaseCommission: any;
        LeaseCommission: any;
        ServiceCommission: any;
        TravelCommission: any;
        CashCommission: any;
        SpecialCommission: any;
        MainCommissionID: number;
        TipLeadID: number;
        TipLeadEmpID: string;
        TipLeadName: string;
        TipLeadAmount: any;
        PositiveAdjustments: any;
        NegativeAdjustments: any;
        CompanyContribution: any;
        SlipType: number;
        TotalCEarned: any;
        TradeIn: any;
        Status: any;
        AccountPeriod: any;
        AccountPeriodID: number;
        ProcesByPayroll: boolean;
        BranchID: any;
        BranchName: string;
        SaleType: any;
        ProductLine: any;
        Comments: string;
        CommentedBy: string;
        CommentList: CommissionComponent[];
        CreatedBy: number;
        CreatedOn: Date;
        ModifiedBy: number;
        IsActive: boolean;
        IsNotified: boolean;
        IsPlanMapped: boolean;
        IsGMActive: boolean;
        TipLeadSliplist: TipLeadSlip[];
        constructor() {
            this.rows = [];
        }
    }

    export enum CommissionStatus {
        "Draft" = 1,
        "Waiting for approval" = 2,
        "SM-Accepted" = 3,
        "SM-Rejected" = 4,
        "GM-Accepted" = 5,
        "GM-Rejected" = 6,
        "Payroll Accepted" = 7 /*Payroll Accept and Notify*/
    }
    export enum ProductLine {
        "Copy" = 1,
        "Milner Software" = 2,
        "Third Party Software" = 3,
        "IT Services" = 4,
        "Other Third Party Products" = 5,
    }
    export class Branches {
        rows: DBdRow[];
        BranchID: number;
        BranchName: string;
        BranchCode: string;
        IsActive: boolean;
        CreatedOn: any;
        CreatedBy: number;
        ModifiedOn: any;
        ModifiedBy: number;
        ticked: boolean;
        constructor() {
            this.rows = [];
        }
    }
    export class EmployeeComponant {
        rows: DBdRow[];
        UID: number;
        EmployeeID: string;
        AccountName: string;
        FirstName: string;
        LastName: string;
        MiddleName: string;
        RoleID: any;
        RoleName: string;
        DateofHire: any;
        DateInPosition: any;
        PrimaryBranch: string;
        PrimaryBranchName: string;
        SecondaryBranch: any[];
        SecondaryBranchList: Branches[];
        SecondaryBranchName: string;
        PayPlanID: string;
        PlanName: string;
        PayPlanName: string;
        BPSalary: any;
        BPDraw: any;
        MonthAmount: number;
        TypeofDraw: any;
        DRPercentage: any;
        DDPeriod: any;
        DDAmount: number;
        ReportMgr: any;
        ApproveMgr: any;
        Email: string;
        DrawTerm: number;
        CreatedOn: any;
        CreatedBy: number;
        ModifiedOn: any;
        ModifiedBy: number;
        IsActive: boolean;
        LastLogin: any;
        UserLogID: number;
        ReportMgrName: string;
        ApproveMgrName: string;
        constructor() {
            this.rows = [];
        }
    }

    export class Roles {
        rows: DBdRow[];
        RoleID: number;
        Name: string;
        constructor() {
            this.rows = [];
        }
    }
    export enum RoleTypes {
        Administrator = 1,
        SalesManager= 2,
        GeneralManager= 3,
        PayRoll = 4,
        SalesRep= 5,
        NonSalesEmployee= 6,
    }

    export enum TAdministrator {
        Yes = 1,
        No = 2,
    }

    export class KeyValuePair {
        Key: string;
        Value: string;
    }

    export class DBdRow {
        columns: DBdColumn[];
        constructor() {
            this.columns = [];
        }
    }

    export class DBdColumn {
        widgets: DBdType[];
        styleClass: string;
        constructor() {
            this.widgets = [];
        }
    }

    export class DBdType {
        type: string;
        title: string;
        config: KeyValuePair;
    }
    export class EmailMessage {
        Sender: string;
        RecipientTo: string;
        RecipientCC: string;
        Subject: string;
        Body: string;
        AttachmentFile: string;
        IsBodyHtml: boolean;
    }
    export class ReportParameters {
        rows: DBdRow[];
        BranchID: number;
        SalesPerson: number;
        ReportMonth: number;
        ReportYear: number;
        ReportPeriod: any;
        ReportType: any;
        PlanID: number;
        MonthsofExp: number;
        DrawType: any;
        Applicabletill: any;
        DrawAmount: any;
        RecoverablePercent: any;
        TotalCommission: any;
        CommissionDue: any;
        DrawPaid: any;
        DrawRecovered: any;
        DrawDificit: any;
        Bimonthreport: any;
        Tenurereport: any;
        TotalEarning: any;
        Salary: any;
        EmployeeID: string;
        Type: any;
        ReportFrom: any;
        ReportTo: any;
        PayrollConfigID: number;
    }

    export class PaylocityReports {
        EmployeeID: number;
        Earnings: 'E';
        Commission: 'COMM';
        Amount: any;
    }

    export class GLReports {
        Month: any;
        BranchName: string;
        TotalCommissionPaid: any;
    }

    export class SaleType {
        rows: DBdRow[];
        ID: number;
        SaleTypeName: string;
        IsNewCustomer: boolean;
        IsExistingCustomer: boolean;
        IsBiMonthlyBonus: boolean;
        IsTenureBonus: boolean;
        IsActive: boolean;
        CreatedOn: any;
        CreatedBy: number;
        ModifiedOn: any;
        ModifiedBy: number;
        constructor() {
            this.rows = [];
        }        
    }

    export class CheckSaleType {
        rows: DBdRow[];
        ID: number;
        reason: number;
        constructor() {
            this.rows = [];
        }
    }

    export class Payroll {
        rows: DBdRow[];
        ID: number;
        Year: number;
        Day: number;
        Period: number;
        Month: string;
        DateFrom: any;
        DateTo: any;
        CreatedOn: any;
        CreatedBy: number;
        CreatedByName: string;
        ModifiedOn: any;
        ModifiedBy: number;
        ModifiedByName: string;
        IsActive: boolean;
        ProcessByPayroll: boolean;
        constructor() {
            this.rows = [];
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Define Sales Commission Interfaces Service                                                           //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    export interface ISalesComService {
        GetPlans(): ng.IPromise<Plancomponent[]>;
        GetPlanbyID(plan: Plancomponent): ng.IPromise<Plancomponent>;
        GetTGP(plan: Plancomponent): ng.IPromise<TGPCustomerInfo[]>;
        GetBimonthly(plan: Plancomponent): ng.IPromise<BIMonthlyBonusInfo[]>;
        GetTenure(plan: Plancomponent): ng.IPromise<QuarterlyTenureBonus[]>;
        ActivateDeactivatePlan(plan: Plancomponent): ng.IPromise<Boolean>;
        AddPlan(plan: Plancomponent): ng.IPromise<Number>;
        EditPlan(plan: Plancomponent): ng.IPromise<Boolean>;
        GetAllCommissionsbyUID(emp: EmployeeComponant): ng.IPromise<CommissionComponent[]>;
        GetCommissionbyID(commissionid: number): ng.IPromise<CommissionComponent[]>;
        AddCommission(commission: CommissionComponent): ng.IPromise<Number>;
        EditCommission(commission: CommissionComponent): ng.IPromise<Boolean>;
        EditTipLeadSlip(commission: CommissionComponent): ng.IPromise<Boolean>;
        SendMail(mail: EmailMessage): ng.IPromise<Boolean>;
        CommissionCreatedNotification(UID: number, status: number): ng.IPromise<string>;
        DeleteCommission(Commission: CommissionComponent): ng.IPromise<Boolean>;

        GetRoles(): ng.IPromise<Roles[]>;
        GetBranches(): ng.IPromise<Branches[]>;
        GetAllManager(): ng.IPromise<EmployeeComponant[]>;
        AddEmployee(employee: EmployeeComponant): ng.IPromise<any>;
        EditEmployee(employee: EmployeeComponant): ng.IPromise<any>;
        ActivateDeactivateEmployee(employee: EmployeeComponant): ng.IPromise<any>;
        GetEmployeebyUID(employee: EmployeeComponant): ng.IPromise<EmployeeComponant>;
        GetAllEmployeeDetailsbyRoleID(roleID: number): ng.IPromise<EmployeeComponant[]>;
        GetSalesReplistforReport(UserandRoleID: number[]): ng.IPromise<EmployeeComponant[]>;
        GetCommissionReport(reportparameter: ReportParameters): ng.IPromise<CommissionComponent[]>;
        GetCommissionslipReportDetails(reportparameter: ReportParameters): ng.IPromise<ReportParameters[]>;
        ViewPaylocityReport(Paylocity: ReportParameters): ng.IPromise<PaylocityReports[]>;
        GetTotalEarningReport(reportparameter: ReportParameters): ng.IPromise<ReportParameters[]>;
        GetIncentiveTripReport(reportparameter: number[]): ng.IPromise<CommissionComponent[]>;
        ViewGLReport(GeneralLedger: ReportParameters): ng.IPromise<GLReports[]>;
        SaveReportdetails(reportparameter: ReportParameters): ng.IPromise<Boolean>;
        GetCountforPaylocity(reportparameter: ReportParameters): ng.IPromise<number[]>;
        GetDeactiveGMandPlanID(employee: EmployeeComponant): ng.IPromise<number[]>;
        SetBranch(branch: Branches[]): ng.IPromise<any>;
        GetAllSaleType(): ng.IPromise<SaleType[]>;
        SetSaleType(saleType: SaleType[]): ng.IPromise<any>;
        GetDeActivateBranch(branchid: number): ng.IPromise<boolean>;
        GetDeActivateSaletype(saleType: CheckSaleType): ng.IPromise<boolean>;
        GetPayrollForDashBoard(): ng.IPromise<Payroll[]>;
        GetPayrollForEdit(year): ng.IPromise<Payroll[]>;
        AddPayroll(payRoll: Payroll[]): ng.IPromise<any>;
        UpdatePayroll(payRoll: Payroll[]): ng.IPromise<any>;
        GetAccountingMonth(currentDate: Date): ng.IPromise<any>;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Define Sales Commission Services                                                                     //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    export class SalesComService implements ISalesComService {
        static $inject = ['$q', '$http', 'mtisalescom.SessionHandler'];
        constructor(private $q: ng.IQService, private $http: ng.IHttpService, private SessionHandler: mtisalescom.SessionHandler) {
            var parentScope = this;
        }

        GetPlans(): ng.IPromise<Plancomponent[]> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewPlanComponent/GetPlans'),
                method: "Get",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Plancomponent[] => {
                return <Plancomponent[]>response.data;
            });
        }
        GetPlanbyID(plan: Plancomponent): ng.IPromise<Plancomponent> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewPlanComponent/GetPlanbyID'),
                method: "Post",
                data: plan,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Plancomponent => {
                return <Plancomponent>response.data;
            });
        }
        GetDeActivatePlanEmployees(): ng.IPromise<EmployeeComponant> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('EmployeeComponent/GetDeActivatePlanEmployees'),
                method: "Get",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): EmployeeComponant => {
                return <EmployeeComponant>response.data;
            });
        }
        GetTGP(plan: Plancomponent): ng.IPromise<TGPCustomerInfo[]> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewPlanComponent/GetAllTGP'),
                method: "Post",
                data: plan,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): TGPCustomerInfo[] => {
                return <TGPCustomerInfo[]>response.data;
            });
        }
        GetBimonthly(plan: Plancomponent): ng.IPromise<BIMonthlyBonusInfo[]> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewPlanComponent/GetAllBimonthly'),
                method: "Post",
                data: plan,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): BIMonthlyBonusInfo[] => {
                return <BIMonthlyBonusInfo[]>response.data;
            });
        }
        GetTenure(plan: Plancomponent): ng.IPromise<QuarterlyTenureBonus[]> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewPlanComponent/GetAllTenure'),
                method: "Post",
                data: plan,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): QuarterlyTenureBonus[] => {
                return <QuarterlyTenureBonus[]>response.data;
            });
        }
        ActivateDeactivatePlan(plan: Plancomponent): ng.IPromise<Boolean> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewPlanComponent/DeletePlan'),
                method: "Post",
                data: plan,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Boolean => {
                return <Boolean>response.data;
            });
        }
        AddPlan(plan: Plancomponent): ng.IPromise<Number> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewPlanComponent/AddPlan'),
                method: "Post",
                data: plan,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Number => {
                return <Number>response.data;
            });
        }
        EditPlan(plan: Plancomponent): ng.IPromise<Boolean> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewPlanComponent/EditPlan'),
                method: "Post",
                data: plan,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Boolean => {
                return <Boolean>response.data;
            });
        }
        GetAllCommissionsbyUID(emp: EmployeeComponant): ng.IPromise<CommissionComponent[]> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewCommission/GetAllCommissionsbyUID'),
                method: "Post",
                data: emp,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): CommissionComponent[] => {
                return <CommissionComponent[]>response.data;
            });
        }
        GetCommissionbyID(commissionid: number): ng.IPromise<CommissionComponent[]> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewCommission/GetCommissionbyID'),
                method: "Post",
                data: commissionid,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): CommissionComponent[] => {
                return <CommissionComponent[]>response.data;
            });
        }
        AddCommission(commission: CommissionComponent): ng.IPromise<Number> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewCommission/CreateCommission'),
                method: "Post",
                data: commission,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Number => {
                return <Number>response.data;
            });
        }
        EditCommission(commission: CommissionComponent): ng.IPromise<Boolean> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewCommission/EditCommission'),
                method: "Post",
                data: commission,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Boolean => {
                return <Boolean>response.data;
            });
        }
        EditTipLeadSlip(commission: CommissionComponent): ng.IPromise<Boolean> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewCommission/EditTipLeadSlip'),
                method: "Post",
                data: commission,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Boolean => {
                return <Boolean>response.data;
            });
        }
        SendMail(mail: EmailMessage): ng.IPromise<Boolean> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/SendMail'),
                method: "Post",
                data: mail,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Boolean => {
                return <Boolean>response.data;
            });
        }
        CommissionCreatedNotification(UID: number, status: number): ng.IPromise<string> {
            var email = { 'ID': UID, 'Status': status };
            var parentScope = this;
            return parentScope.$http({
                url: preurl('EmployeeComponent/GetCommissionCreatedMailIDs'),
                data: email,
                method: "Post",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): string => {
                return <string>response.data;
            });
        }
        DeleteCommission(Commission: CommissionComponent): ng.IPromise<Boolean> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewCommission/DeleteCommission'),
                method: "Post",
                data: Commission,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Boolean => {
                return <Boolean>response.data;
            });
        }

        GetRoles(): ng.IPromise<Roles[]> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/GetRoles'),
                method: "Get",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Roles[] => {
                return <Roles[]>response.data;
            });
        }

        GetAllManager(): ng.IPromise<EmployeeComponant[]> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/GetAllManager'),
                method: "Get",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): EmployeeComponant[] => {
                return <EmployeeComponant[]>response.data;
            });
        }
        GetBranches(): ng.IPromise<Branches[]> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/GetAllBranch'),
                method: "Get",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Branches[] => {
                return <Branches[]>response.data;
            });
        }

        AddEmployee(employee: EmployeeComponant): ng.IPromise<any> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('EmployeeComponent/CreateEmployee'),
                method: "POST",
                data: employee,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Number => {
                var result = <Number>response.data;
                return result;
            });
        }

        EditEmployee(employee: EmployeeComponant): ng.IPromise<any> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('EmployeeComponent/EditEmployee'),
                method: "POST",
                data: employee,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Boolean => {
                var result = <Boolean>response.data;
                return result;
            });
        }

        ActivateDeactivateEmployee(employee: EmployeeComponant): ng.IPromise<Boolean> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('EmployeeComponent/ActiveDeactiveEmployee'),
                method: "Post",
                data: employee,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Boolean => {
                return <Boolean>response.data;
            });
        }

        GetEmployeebyUID(employee): ng.IPromise<EmployeeComponant> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('EmployeeComponent/GetEmployeesByID'),
                data: employee,
                method: "Post",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): EmployeeComponant => {
                return <EmployeeComponant>response.data;
            });
        }

        GetAllEmployeeDetailsbyRoleID(roleID: number): ng.IPromise<EmployeeComponant[]> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('EmployeeComponent/GetAllEmployeesbyRoleID'),
                data: roleID,
                method: "Post",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): EmployeeComponant[] => {
                return <EmployeeComponant[]>response.data;
            });
        }
        GetSalesReplistforReport(UserandRoleID: number[]): ng.IPromise<EmployeeComponant[]> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Reporting/GetSalesReplistforReport'),
                method: "Post",
                data: UserandRoleID,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): EmployeeComponant[] => {
                return <EmployeeComponant[]>response.data;
            });
        }
        GetCommissionReport(reportparameter: ReportParameters): ng.IPromise<CommissionComponent[]> {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Reporting/GetCommissionReport'),
                method: "Post",
                data: reportparameter,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): CommissionComponent[] => {

                return <CommissionComponent[]>response.data;
            });
        }
        GetCommissionslipReportDetails(reportparameter: ReportParameters): ng.IPromise<ReportParameters[]> {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Reporting/GetCommissionReportDetails'),
                method: "Post",
                data: reportparameter,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): ReportParameters[] => {

                return <ReportParameters[]>response.data;
            });
        }
        ViewPaylocityReport(Paylocity): ng.IPromise<PaylocityReports[]> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Reporting/GetPaylocityReport'),
                data: Paylocity,
                method: "Post",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): PaylocityReports[] => {
                return response.data;
            });
        }
        GetTotalEarningReport(reportparameter: ReportParameters): ng.IPromise<ReportParameters[]> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Reporting/GetTotalEarningReport'),
                data: reportparameter,
                method: "Post",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): ReportParameters[] => {
                return <ReportParameters[]>response.data;
            });
        }
        GetIncentiveTripReport(reportparameter: number[]): ng.IPromise<CommissionComponent[]> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Reporting/GetIncentiveTripReport'),
                data: reportparameter,
                method: "Post",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): CommissionComponent[] => {
                return <CommissionComponent[]>response.data;
            });
        }
        ViewGLReport(GeneralLedger): ng.IPromise<GLReports[]> {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Reporting/GetGeneralLedgerReport'),
                data: GeneralLedger,
                method: "Post",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): GLReports[] => {
                return response.data;
            });
        }
        GetCountforPaylocity(reportparameter): ng.IPromise<number[]> {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Reporting/GetCountforPaylocity'),
                data: reportparameter,
                method: "Post",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): number[] => {
                return <number[]>response.data;
            });
        }
        SaveReportdetails(reportparameter): ng.IPromise<Boolean> {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Reporting/SaveReportdetails'),
                data: reportparameter,
                method: "Post",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Boolean => {
                return <Boolean>response.data;
            });
        }
        GetDeactiveGMandPlanID(employee): ng.IPromise<number[]> {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewCommission/GetDeactiveGMandPlanID'),
                data: employee,
                method: "Post",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): number[] => {
                return <number[]>response.data;
            });
        }

        SetBranch(branch: Branches[]): ng.IPromise<any> {

            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/SaveBranch'),
                method: "Post",
                data: branch,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Boolean => {
                return <Boolean>response.data;
            });
        }

        GetAllSaleType(): ng.IPromise<SaleType[]> {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/GetAllSaleType'),
                method: "Get",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): SaleType[] => {
                return <SaleType[]>response.data;
            });
        }

        SetSaleType(saleType: SaleType[]): ng.IPromise<any> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/SaveSaleType'),
                method: "Post",
                data: saleType,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Boolean => {
                return <Boolean>response.data;
            });
        }

        GetDeActivateBranch(branchid): ng.IPromise<boolean> {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/CheckBranch'),
                data:  branchid,
                method: "Post",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): boolean => {
                return <boolean>response.data;
            });
        }

        GetDeActivateSaletype(saleType): ng.IPromise<boolean> {
            debugger;
            //let InData = new CheckSaleType();
            //InData.ID = id;
            //InData.reason = type;
            //var InData = { 'ID': id, 'reason': type };            
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/CheckSaletype'),
                data: saleType,
                method: "Post",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): boolean => {
                return <boolean>response.data;
            });
        }

        GetPayrollForDashBoard(): ng.IPromise<Payroll[]> {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/GetAllPayrollConfig'),
                method: "Get",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Payroll[] => {
                return <Payroll[]>response.data;
            });
        }

        GetPayrollForEdit(year): ng.IPromise<Payroll[]> {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/GetAllPayrollConfigByYear'),
                data: year,
                method: "POST",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Payroll[] => {
                return <Payroll[]>response.data;
            });
        }

        AddPayroll(payRoll: Payroll[]): ng.IPromise<any> {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/AddPayroll'),
                data: payRoll,
                method: "POST",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Payroll[] => {
                return <Payroll[]>response.data;
            });
        }

        UpdatePayroll(payRoll: Payroll[]): ng.IPromise<any> {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/UpdatePayroll'),
                data: payRoll,
                method: "POST",
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Payroll[] => {
                return <Payroll[]>response.data;
            });
        }

        GetAccountingMonth(currentDate): ng.IPromise<any> {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/GetAccountingMonth'),
                method: "POST",
                data: currentDate,
                withCredentials: true
            }).then((response: ng.IHttpPromiseCallbackArg<any>): Payroll[] => {
                return <Payroll[]>response.data;
            });
        }
    }

    SalesCommissionFactory.$inject = ['$q', '$http', 'mtisalescom.SessionHandler'];
    
    function SalesCommissionFactory($q: ng.IQService, $http: ng.IHttpService,
        SessionHandler: mtisalescom.SessionHandler):
        ISalesComService {
        return new SalesComService($q, $http, SessionHandler);
    }

    angular
        .module('mtisalescom')
        .factory('mtisalescom.SalesComService', SalesCommissionFactory)
}

function ajaxError(xmlreq, tstat, e) {
    if (xmlreq.responseText) {
        try {
            var err = JSON.parse(xmlreq.responseText);
            alert("A problem occurred while performing your request. The technical details follow. \n\n" + err.Message + "\n\n" + err.MessageDetail);
        }
        catch (ex) {
            alert("A problem occurred while performing your request. The technical reason follows.\n\n" + xmlreq.responseText);
        }
    } else if (xmlreq.ExceptionMessage) {
        alert("A problem occurred while performing your request. Make sure the server is running.  The technical code was " + xmlreq.ExceptionMessage.toString() + ".");
    } else if (xmlreq.Message) {
        alert("A problem occurred while performing your request. Make sure the server is running.  The technical code was " + xmlreq.Message.toString() + ".");

    } else {
        alert("A problem occurred while performing your request. Make sure the server is running.  The technical code was " + xmlreq + ".");
    }
}