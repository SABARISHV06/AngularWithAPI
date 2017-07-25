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
    var Plancomponent = (function () {
        function Plancomponent() {
            this.rows = [];
        }
        return Plancomponent;
    }());
    mtisalescom.Plancomponent = Plancomponent;
    var TGPCustomerInfo = (function () {
        function TGPCustomerInfo() {
            this.rows = [];
        }
        return TGPCustomerInfo;
    }());
    mtisalescom.TGPCustomerInfo = TGPCustomerInfo;
    var BIMonthlyBonusInfo = (function () {
        function BIMonthlyBonusInfo() {
            this.rows = [];
        }
        return BIMonthlyBonusInfo;
    }());
    mtisalescom.BIMonthlyBonusInfo = BIMonthlyBonusInfo;
    var QuarterlyTenureBonus = (function () {
        function QuarterlyTenureBonus() {
            this.rows = [];
        }
        return QuarterlyTenureBonus;
    }());
    mtisalescom.QuarterlyTenureBonus = QuarterlyTenureBonus;
    (function (Basistype) {
        Basistype[Basistype["Dollar Volume"] = 1] = "Dollar Volume";
        Basistype[Basistype["Gross Profit"] = 2] = "Gross Profit";
    })(mtisalescom.Basistype || (mtisalescom.Basistype = {}));
    var Basistype = mtisalescom.Basistype;
    (function (Booleanvalues) {
        Booleanvalues[Booleanvalues["Yes"] = 1] = "Yes";
        Booleanvalues[Booleanvalues["No"] = 0] = "No";
    })(mtisalescom.Booleanvalues || (mtisalescom.Booleanvalues = {}));
    var Booleanvalues = mtisalescom.Booleanvalues;
    //export enum SaleType {
    //    "Cash" = 1,
    //    "Lease" = 2,
    //    "Rent" = 3,
    //    "Lease renewal" = 4,
    //}
    (function (Customertype) {
        Customertype[Customertype["New Customer"] = 1] = "New Customer";
        Customertype[Customertype["Existing Customer"] = 2] = "Existing Customer";
    })(mtisalescom.Customertype || (mtisalescom.Customertype = {}));
    var Customertype = mtisalescom.Customertype;
    (function (CustomerTypes) {
        CustomerTypes[CustomerTypes["NewCustomer"] = 1] = "NewCustomer";
        CustomerTypes[CustomerTypes["ExistingCustomer"] = 2] = "ExistingCustomer";
    })(mtisalescom.CustomerTypes || (mtisalescom.CustomerTypes = {}));
    var CustomerTypes = mtisalescom.CustomerTypes;
    (function (SlipTypes) {
        SlipTypes[SlipTypes["CommissionSlip"] = 1] = "CommissionSlip";
        SlipTypes[SlipTypes["TipLeadSlip"] = 2] = "TipLeadSlip";
    })(mtisalescom.SlipTypes || (mtisalescom.SlipTypes = {}));
    var SlipTypes = mtisalescom.SlipTypes;
    var TipLeadSlip = (function () {
        function TipLeadSlip() {
        }
        return TipLeadSlip;
    }());
    mtisalescom.TipLeadSlip = TipLeadSlip;
    var CommissionComponent = (function () {
        function CommissionComponent() {
            this.rows = [];
        }
        return CommissionComponent;
    }());
    mtisalescom.CommissionComponent = CommissionComponent;
    (function (CommissionStatus) {
        CommissionStatus[CommissionStatus["Draft"] = 1] = "Draft";
        CommissionStatus[CommissionStatus["Waiting for approval"] = 2] = "Waiting for approval";
        CommissionStatus[CommissionStatus["SM-Accepted"] = 3] = "SM-Accepted";
        CommissionStatus[CommissionStatus["SM-Rejected"] = 4] = "SM-Rejected";
        CommissionStatus[CommissionStatus["GM-Accepted"] = 5] = "GM-Accepted";
        CommissionStatus[CommissionStatus["GM-Rejected"] = 6] = "GM-Rejected";
        CommissionStatus[CommissionStatus["Payroll Accepted"] = 7] = "Payroll Accepted"; /*Payroll Accept and Notify*/
    })(mtisalescom.CommissionStatus || (mtisalescom.CommissionStatus = {}));
    var CommissionStatus = mtisalescom.CommissionStatus;
    (function (ProductLine) {
        ProductLine[ProductLine["Copy"] = 1] = "Copy";
        ProductLine[ProductLine["Milner Software"] = 2] = "Milner Software";
        ProductLine[ProductLine["Third Party Software"] = 3] = "Third Party Software";
        ProductLine[ProductLine["IT Services"] = 4] = "IT Services";
        ProductLine[ProductLine["Other Third Party Products"] = 5] = "Other Third Party Products";
    })(mtisalescom.ProductLine || (mtisalescom.ProductLine = {}));
    var ProductLine = mtisalescom.ProductLine;
    var Branches = (function () {
        function Branches() {
            this.rows = [];
        }
        return Branches;
    }());
    mtisalescom.Branches = Branches;
    var EmployeeComponant = (function () {
        function EmployeeComponant() {
            this.rows = [];
        }
        return EmployeeComponant;
    }());
    mtisalescom.EmployeeComponant = EmployeeComponant;
    var Roles = (function () {
        function Roles() {
            this.rows = [];
        }
        return Roles;
    }());
    mtisalescom.Roles = Roles;
    (function (RoleTypes) {
        RoleTypes[RoleTypes["Administrator"] = 1] = "Administrator";
        RoleTypes[RoleTypes["SalesManager"] = 2] = "SalesManager";
        RoleTypes[RoleTypes["GeneralManager"] = 3] = "GeneralManager";
        RoleTypes[RoleTypes["PayRoll"] = 4] = "PayRoll";
        RoleTypes[RoleTypes["SalesRep"] = 5] = "SalesRep";
        RoleTypes[RoleTypes["NonSalesEmployee"] = 6] = "NonSalesEmployee";
    })(mtisalescom.RoleTypes || (mtisalescom.RoleTypes = {}));
    var RoleTypes = mtisalescom.RoleTypes;
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
    var DBdType = (function () {
        function DBdType() {
        }
        return DBdType;
    }());
    mtisalescom.DBdType = DBdType;
    var EmailMessage = (function () {
        function EmailMessage() {
        }
        return EmailMessage;
    }());
    mtisalescom.EmailMessage = EmailMessage;
    var ReportParameters = (function () {
        function ReportParameters() {
        }
        return ReportParameters;
    }());
    mtisalescom.ReportParameters = ReportParameters;
    var PaylocityReports = (function () {
        function PaylocityReports() {
        }
        return PaylocityReports;
    }());
    mtisalescom.PaylocityReports = PaylocityReports;
    var GLReports = (function () {
        function GLReports() {
        }
        return GLReports;
    }());
    mtisalescom.GLReports = GLReports;
    var SaleType = (function () {
        function SaleType() {
            this.rows = [];
        }
        return SaleType;
    }());
    mtisalescom.SaleType = SaleType;
    var CheckSaleType = (function () {
        function CheckSaleType() {
            this.rows = [];
        }
        return CheckSaleType;
    }());
    mtisalescom.CheckSaleType = CheckSaleType;
    var Payroll = (function () {
        function Payroll() {
            this.rows = [];
        }
        return Payroll;
    }());
    mtisalescom.Payroll = Payroll;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Define Sales Commission Services                                                                     //
    /////////////////////////////////////////////////////////////////////////////////////////////////////////
    var SalesComService = (function () {
        function SalesComService($q, $http, SessionHandler) {
            this.$q = $q;
            this.$http = $http;
            this.SessionHandler = SessionHandler;
            var parentScope = this;
        }
        SalesComService.prototype.GetPlans = function () {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewPlanComponent/GetPlans'),
                method: "Get",
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetPlanbyID = function (plan) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewPlanComponent/GetPlanbyID'),
                method: "Post",
                data: plan,
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetDeActivatePlanEmployees = function () {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('EmployeeComponent/GetDeActivatePlanEmployees'),
                method: "Get",
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetTGP = function (plan) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewPlanComponent/GetAllTGP'),
                method: "Post",
                data: plan,
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetBimonthly = function (plan) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewPlanComponent/GetAllBimonthly'),
                method: "Post",
                data: plan,
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetTenure = function (plan) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewPlanComponent/GetAllTenure'),
                method: "Post",
                data: plan,
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.ActivateDeactivatePlan = function (plan) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewPlanComponent/DeletePlan'),
                method: "Post",
                data: plan,
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.AddPlan = function (plan) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewPlanComponent/AddPlan'),
                method: "Post",
                data: plan,
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.EditPlan = function (plan) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewPlanComponent/EditPlan'),
                method: "Post",
                data: plan,
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetAllCommissionsbyUID = function (emp) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewCommission/GetAllCommissionsbyUID'),
                method: "Post",
                data: emp,
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetCommissionbyID = function (commissionid) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewCommission/GetCommissionbyID'),
                method: "Post",
                data: commissionid,
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.AddCommission = function (commission) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewCommission/CreateCommission'),
                method: "Post",
                data: commission,
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.EditCommission = function (commission) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewCommission/EditCommission'),
                method: "Post",
                data: commission,
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.EditTipLeadSlip = function (commission) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewCommission/EditTipLeadSlip'),
                method: "Post",
                data: commission,
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.SendMail = function (mail) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/SendMail'),
                method: "Post",
                data: mail,
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.CommissionCreatedNotification = function (UID, status) {
            var email = { 'ID': UID, 'Status': status };
            var parentScope = this;
            return parentScope.$http({
                url: preurl('EmployeeComponent/GetCommissionCreatedMailIDs'),
                data: email,
                method: "Post",
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.DeleteCommission = function (Commission) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewCommission/DeleteCommission'),
                method: "Post",
                data: Commission,
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetRoles = function () {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/GetRoles'),
                method: "Get",
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetAllManager = function () {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/GetAllManager'),
                method: "Get",
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetBranches = function () {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/GetAllBranch'),
                method: "Get",
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.AddEmployee = function (employee) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('EmployeeComponent/CreateEmployee'),
                method: "POST",
                data: employee,
                withCredentials: true
            }).then(function (response) {
                var result = response.data;
                return result;
            });
        };
        SalesComService.prototype.EditEmployee = function (employee) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('EmployeeComponent/EditEmployee'),
                method: "POST",
                data: employee,
                withCredentials: true
            }).then(function (response) {
                var result = response.data;
                return result;
            });
        };
        SalesComService.prototype.ActivateDeactivateEmployee = function (employee) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('EmployeeComponent/ActiveDeactiveEmployee'),
                method: "Post",
                data: employee,
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetEmployeebyUID = function (employee) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('EmployeeComponent/GetEmployeesByID'),
                data: employee,
                method: "Post",
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetAllEmployeeDetailsbyRoleID = function (roleID) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('EmployeeComponent/GetAllEmployeesbyRoleID'),
                data: roleID,
                method: "Post",
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetSalesReplistforReport = function (UserandRoleID) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Reporting/GetSalesReplistforReport'),
                method: "Post",
                data: UserandRoleID,
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetCommissionReport = function (reportparameter) {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Reporting/GetCommissionReport'),
                method: "Post",
                data: reportparameter,
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetCommissionslipReportDetails = function (reportparameter) {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Reporting/GetCommissionReportDetails'),
                method: "Post",
                data: reportparameter,
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.ViewPaylocityReport = function (Paylocity) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Reporting/GetPaylocityReport'),
                data: Paylocity,
                method: "Post",
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetTotalEarningReport = function (reportparameter) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Reporting/GetTotalEarningReport'),
                data: reportparameter,
                method: "Post",
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetIncentiveTripReport = function (reportparameter) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Reporting/GetIncentiveTripReport'),
                data: reportparameter,
                method: "Post",
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.ViewGLReport = function (GeneralLedger) {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Reporting/GetGeneralLedgerReport'),
                data: GeneralLedger,
                method: "Post",
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetCountforPaylocity = function (reportparameter) {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Reporting/GetCountforPaylocity'),
                data: reportparameter,
                method: "Post",
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.SaveReportdetails = function (reportparameter) {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Reporting/SaveReportdetails'),
                data: reportparameter,
                method: "Post",
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetDeactiveGMandPlanID = function (employee) {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('NewCommission/GetDeactiveGMandPlanID'),
                data: employee,
                method: "Post",
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.SetBranch = function (branch) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/SaveBranch'),
                method: "Post",
                data: branch,
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetAllSaleType = function () {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/GetAllSaleType'),
                method: "Get",
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.SetSaleType = function (saleType) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/SaveSaleType'),
                method: "Post",
                data: saleType,
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetDeActivateBranch = function (branchid) {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/CheckBranch'),
                data: branchid,
                method: "Post",
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetDeActivateSaletype = function (saleType) {
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
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetPayrollForDashBoard = function () {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/GetAllPayrollConfig'),
                method: "Get",
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetPayrollForEdit = function (year) {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/GetAllPayrollConfigByYear'),
                data: year,
                method: "POST",
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.AddPayroll = function (payRoll) {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/AddPayroll'),
                data: payRoll,
                method: "POST",
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.UpdatePayroll = function (payRoll) {
            debugger;
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/UpdatePayroll'),
                data: payRoll,
                method: "POST",
                withCredentials: true
            }).then(function (response) {
                return response.data;
            });
        };
        SalesComService.prototype.GetAccountingMonth = function (currentDate) {
            var parentScope = this;
            return parentScope.$http({
                url: preurl('Security/GetAccountingMonth'),
                method: "POST",
                data: currentDate,
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
//# sourceMappingURL=E:/SVN/MilnerSalesCommission/Source/Application/SalesCom//Scripts/TSBuild/services/app.salescom.services.js.map