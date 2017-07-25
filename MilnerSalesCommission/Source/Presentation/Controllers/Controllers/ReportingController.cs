// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ViewModels;
using ServiceLibrary;

namespace ApiControllers.Controllers
{
    /// <summary>
    /// To handle Reports related actions and behaviours
    /// </summary>
    public sealed class ReportingController : ApiBaseController
    {
        /// <summary>
        /// To call GetCommissionReport method with session ID.
        /// </summary>
        /// <param name="Reportparameter"></param>
        /// <returns></returns>
        [HttpPost]
        public List<CommissionComponent> GetCommissionReport(ReportParameter Reportparameter)
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[ApiControllers.Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }
            return GetCommissionReport(sessionid, Reportparameter);
        }

        /// <summary>
        /// To Get Commission details for Commission Report
        /// </summary>
        /// <param name="s"></param>
        /// <param name="Reportparameter"></param>
        /// <returns></returns>
        [HttpPost]
        public List<CommissionComponent> GetCommissionReport(string s, ReportParameter Reportparameter)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                List<PayrollConfig> runlist = servicelibrary.GetPayrollConfig4Run(User, Reportparameter.ReportYear, Reportparameter.ReportMonth);

                List<CommissionComponent> Bimonthlyreportlist = new List<CommissionComponent>();

                for (var i = 0; i < runlist.Count; i++)
                {
                    Reportparameter.PayrollConfigID = runlist[i].ID;

                    List<CommissionComponent> tempreportlist = new List<CommissionComponent>();

                    tempreportlist = servicelibrary.GetCommissionReport(User, Reportparameter);

                    Bimonthlyreportlist.AddRange(tempreportlist);
                }
                CommissionComponent TotalCommission = new CommissionComponent();
                foreach (CommissionComponent commission in Bimonthlyreportlist)
                {
                    TotalCommission.TradeIn += commission.TradeIn;
                    TotalCommission.DollarVolume += commission.DollarVolume;
                    TotalCommission.BaseCommission += commission.BaseCommission;
                    TotalCommission.LeaseCommission += commission.LeaseCommission;
                    TotalCommission.ServiceCommission += commission.ServiceCommission;
                    TotalCommission.TravelCommission += commission.TravelCommission;
                    TotalCommission.SpecialCommission += commission.SpecialCommission;
                    TotalCommission.CashCommission += commission.CashCommission;
                    TotalCommission.TipLeadAmount += commission.TipLeadAmount;
                    TotalCommission.PositiveAdjustments += commission.PositiveAdjustments;
                    TotalCommission.NegativeAdjustments += commission.NegativeAdjustments;
                    TotalCommission.CompanyContribution += commission.CompanyContribution;
                    TotalCommission.TotalCEarned += commission.TotalCEarned;
                }
                Bimonthlyreportlist.Add(TotalCommission);
                return Bimonthlyreportlist;
            }
            catch (Exception e)
            {
                string message = "Get Commission report could not be retrieved. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Get Commission report could not be retrieved", e);
            }

            return null;
        }
        /// <summary>
        /// To call GetCommissionReportDetails method with session ID.
        /// </summary>
        /// <param name="reportparameter"></param>
        /// <returns></returns>
        [HttpPost]
        public List<ReportParameter> GetCommissionReportDetails(ReportParameter reportparameter)
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[ApiControllers.Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }
            return GetCommissionReportDetails(sessionid, reportparameter);
        }

        /// <summary>
        /// To Get Commission details for Commission Report
        /// </summary>
        /// <param name="s"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost]
        public List<ReportParameter> GetCommissionReportDetails(string s, ReportParameter reportparameter)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                List<PayrollConfig> runlist = servicelibrary.GetPayrollConfig4Run(User, reportparameter.ReportYear, reportparameter.ReportMonth);

                List<ReportParameter> runValues = new List<ReportParameter>();

                int finalRunRow = runlist.Count - 1;

                for (var i = 0; i < runlist.Count; i++)
                {
                    if (i == finalRunRow)
                    {
                        reportparameter.IsFinalRun = true;
                    }
                    reportparameter.RunNo = i + 1;
                    reportparameter.ReportMonth = runlist[i].DateTo.Month;
                    reportparameter.ReportYear = runlist[i].DateTo.Year;
                    reportparameter.PayrollConfigID = runlist[i].ID;

                    runValues.Add(servicelibrary.GetCommissionReportDetails(User, reportparameter));
                }

                //ReportParameter TotalValues = new ReportParameter();
                //foreach (ReportParameter run in runValues)
                //{
                //    TotalValues.TotalCommission += run.TotalCommission;
                //    TotalValues.DrawPaid += run.DrawPaid;
                //    TotalValues.DrawRecovered += run.DrawRecovered;
                //    TotalValues.CommissionDue += run.CommissionDue;
                //    TotalValues.Salary += run.Salary;
                //    TotalValues.Bimonthreport += run.Bimonthreport;
                //    TotalValues.Tenurereport += run.Tenurereport;
                //    TotalValues.TotalEarning += run.TotalEarning;
                //}
                //runValues.Add(TotalValues);

                return runValues; //servicelibrary.GetCommissionReportDetails(User, reportparameter);
            }
            catch (Exception e)
            {
                string message = "Get Commission Report Details could not be retrieved. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Get Commission Report Details could not be retrieved", e);
            }
            return null;

        }
        /// <summary>
        /// To call GetSalesReplistforReport method with session ID.
        /// </summary>
        /// <param name="UserandRoleID"></param>
        /// <returns></returns>
        [HttpPost]
        public List<EmployeeComponant> GetSalesReplistforReport(int[] UserandRoleID)
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[ApiControllers.Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }
            return GetSalesReplistforReport(sessionid, UserandRoleID);
        }

        /// <summary>
        /// To list all employees details for Report.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="UserandRoleID"></param>
        /// <returns></returns>
        [HttpPost]
        public List<EmployeeComponant> GetSalesReplistforReport(string s, int[] UserandRoleID)
        {
            try
            {
                Utility.AuditLogHelper.InfoLogMessage("Start to get employees info");

                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                List<EmployeeComponant> e = servicelibrary.GetSalesReplistforReport(User, UserandRoleID);

                Utility.AuditLogHelper.InfoLogMessage("End to get employees info");

                return e;
            }
            catch (Exception e)
            {
                string message = "Get Employees could not be retrieved. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Get Employees could not be retrieved", e);
            }

            return null;
        }
        /// <summary>
        /// To call GetPaylocityReport method with session ID.
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPost]
        public List<PaylocityReports> GetPaylocityReport(ReportParameter report)
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[ApiControllers.Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }
            return GetPaylocityReport(sessionid, report);
        }
        /// <summary>
        /// To get Paylocity report details.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        [HttpPost]
        public List<PaylocityReports> GetPaylocityReport(string s, ReportParameter report)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                List<ReportParameter> GetUIDlist = servicelibrary.GetPaylocityReport(User, report);

                ReportParameter reportParam = new ReportParameter();

                List<PaylocityReports> Paylocityreportlist = new List<PaylocityReports>();

                List<PayrollConfig> runlist = servicelibrary.GetPayrollConfig4Run(User, report.ReportPeriod.Year, report.ReportPeriod.Month);

                int RunNo = 1;
                foreach (PayrollConfig obj in runlist)
                {
                    if (obj.ID == report.PayrollConfigID)
                    {
                        report.RunNo = RunNo;
                    }
                    RunNo++;
                }

                if (report.RunNo == runlist.Count)
                {
                    report.IsFinalRun = true;
                }

                for (var i = 0; i < GetUIDlist.Count; i++)
                {
                    int PrimaryBranch = GetUIDlist[i].BranchID;
                    reportParam.SalesPerson = GetUIDlist[i].SalesPerson;
                    reportParam.MonthsofExp = GetUIDlist[i].MonthsofExp;
                    reportParam.PlanID = GetUIDlist[i].PlanID;
                    reportParam.BranchID = report.BranchID;
                    reportParam.ReportMonth = report.ReportPeriod.Month;
                    reportParam.ReportYear = report.ReportPeriod.Year;
                    reportParam.PayrollConfigID = report.PayrollConfigID;
                    reportParam.IsFinalRun = report.IsFinalRun;
                    reportParam.RunNo = report.RunNo;

                    Paylocityreportlist.Add(servicelibrary.GetCommissionReportDetails4Paylocity(User, reportParam, PrimaryBranch));
                }


                return Paylocityreportlist;
            }
            catch (Exception e)
            {
                string message = "Get Paylocity report could not be retrieved. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Get Paylocity report could not be retrieved.", e);
            }

            return null;
        }
        /// <summary>
        /// To call GetTotalEarningReport method with session ID.
        /// </summary>
        /// <param name="reportparameter"></param>
        /// <returns></returns>
        [HttpPost]
        public List<ReportParameter> GetTotalEarningReport(ReportParameter reportparameter)
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[ApiControllers.Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }
            return GetTotalEarningReport(sessionid, reportparameter);
        }

        /// <summary>
        /// To get Total Earning report details.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="reportparameter"></param>
        /// <returns></returns>
        [HttpPost]
        public List<ReportParameter> GetTotalEarningReport(string s, ReportParameter reportparameter)
        {
            try
            {
                Utility.AuditLogHelper.InfoLogMessage("Start to get total earnings report info");

                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);
                List<ReportParameter> Reportlist = new List<ReportParameter>();
                EmployeeComponant employeedetail = new EmployeeComponant();
                employeedetail.UID = reportparameter.SalesPerson;
                employeedetail = servicelibrary.GetEmployeeByID(User, employeedetail);
                ReportParameter reportinput = new ReportParameter();

                List<PayrollConfig> monthlist = servicelibrary.GetPayrollConfig4TotEarning(User, reportparameter.ReportYear, reportparameter.ReportMonth);

                //((date1.Year - date2.Year) * 12) + date1.Month - date2.Month
                //var reporttill = ((reportparameter.ReportTo.Year - reportparameter.ReportFrom.Year) * 12) + (reportparameter.ReportTo.Month - reportparameter.ReportFrom.Month);
                //reportparameter.ReportPeriod = reportparameter.ReportFrom;

                for (var i = 0; i <= monthlist.Count - 1; i++)
                {
                    //reportinput.ReportPeriod = reportparameter.ReportPeriod.AddMonths(i);
                    //int lastReportNo = Reportlist.Count - 1;
                    DateTime Today = new DateTime(reportparameter.ReportPeriod.Year, reportparameter.ReportPeriod.Month, 1);
                    DateTime reportdate = new DateTime(monthlist[i].DateTo.Year, monthlist[i].DateTo.Month, 1);  //new DateTime(reportinput.ReportPeriod.Year, reportinput.ReportPeriod.Month, 1);
                    if (reportdate <= Today)
                    {
                        if (employeedetail.DateInPosition > monthlist[i].DateTo)
                        {
                            //if (Reportlist[lastReportNo].ReportMonth != monthlist[i].DateTo.Month)
                            //{
                            ReportParameter reportvalue = new ReportParameter();
                            reportvalue.SalesPerson = reportparameter.SalesPerson;
                            reportvalue.ReportMonth = monthlist[i].DateTo.Month;
                            reportvalue.ReportYear = monthlist[i].DateTo.Year;
                            reportvalue.DrawPaid = Convert.ToDecimal(0);
                            reportvalue.CommissionDue = Convert.ToDecimal(0);
                            reportvalue.Salary = Convert.ToDecimal(0);
                            reportvalue.Bimonthreport = Convert.ToDecimal(0);
                            reportvalue.Tenurereport = Convert.ToDecimal(0);
                            reportvalue.TotalEarning = Convert.ToDecimal(0);
                            Reportlist.Add(reportvalue);
                            //}
                        }
                        else
                        {
                            //reportinput.MonthsofExp = (reportinput.ReportPeriod.Year - employeedetail.DateInPosition.Year) * 12 + (reportinput.ReportPeriod.Month - employeedetail.DateInPosition.Month) + 1;
                            reportinput.MonthsofExp = (monthlist[i].DateTo.Year - employeedetail.DateInPosition.Year) * 12 + (monthlist[i].DateTo.Month - employeedetail.DateInPosition.Month) + 1;
                            if (reportinput.MonthsofExp > 0 && employeedetail.DateInPosition.Day > 5)
                            {
                                reportinput.MonthsofExp = reportinput.MonthsofExp - 1;
                            }

                            reportinput.SalesPerson = reportparameter.SalesPerson;
                            reportinput.ReportMonth = monthlist[i].DateTo.Month; //reportinput.ReportPeriod.Month;
                            reportinput.ReportYear = monthlist[i].DateTo.Year; // reportinput.ReportPeriod.Year;
                            reportinput.PlanID = Convert.ToInt32(employeedetail.PayPlanID);
                            reportinput.PayrollConfigID = monthlist[i].ID;
                            //reportinput.IsFinalRun = true;

                            //List<PayrollConfig> runlist = servicelibrary.GetPayrollConfig4Run(User, reportinput.ReportYear, reportinput.ReportMonth);

                            //reportinput.RunNo = runlist.Count;

                            //ReportParameter reportvalue = servicelibrary.GetCommissionReportDetails(User, reportinput);

                            ReportParameter reportvalue1 = new ReportParameter();

                            List<ReportParameter> reportvalueList = GetCommissionReportDetails(reportinput);

                            //if (lastReportNo != -1 && Reportlist[lastReportNo].ReportMonth == monthlist[i].DateTo.Month)
                            //{
                            foreach (ReportParameter reportvalue in reportvalueList)
                            {
                                reportvalue1.DrawPaid = reportvalue.DrawPaid;
                                reportvalue1.CommissionDue = reportvalue1.CommissionDue + reportvalue.CommissionDue;
                                reportvalue1.Salary = reportvalue.Salary;
                                reportvalue1.Bimonthreport = reportvalue.Bimonthreport;
                                reportvalue1.Tenurereport = reportvalue.Tenurereport;
                            }
                            reportvalue1.TotalEarning = reportvalue1.DrawPaid + reportvalue1.CommissionDue + reportvalue1.Salary + reportvalue1.Bimonthreport + reportvalue1.Tenurereport;

                            reportvalue1.SalesPerson = reportparameter.SalesPerson;
                            reportvalue1.ReportMonth = reportinput.ReportMonth;
                            reportvalue1.ReportYear = reportinput.ReportYear;
                            Reportlist.Add(reportvalue1);
                        }
                    }
                }
                Utility.AuditLogHelper.InfoLogMessage("End to get total earnings report info");

            return Reportlist;
        }
            catch (Exception e)
            {
                string message = "Get Total earning report could not be retrieved. " + e.Message;
                OnHttpResponseMessage(message);
        Utility.AuditLogHelper.ErrorLog("Get Total earning report could not be retrieved", e);
            }

            return null;
        }

/// <summary>
/// To call GetIncentiveTripReport method with session ID.
/// </summary>
/// <param name="reportparameter"></param>
/// <returns></returns>
[HttpPost]
public List<CommissionComponent> GetIncentiveTripReport(int[] reportparameter)
{
    string sessionid = null;
    try
    {
        sessionid = Request.Properties[ApiControllers.Handlers.ApiSessionHandler.SessionIdToken] as string;
    }
    catch
    {
    }

    if (sessionid == null)
    {
        sessionid = System.Web.HttpContext.Current.Session.SessionID;
    }
    return GetIncentiveTripReport(sessionid, reportparameter);
}

/// <summary>
/// To get Incentive Trip report details
/// </summary>
/// <param name="s"></param>
/// <param name="reportparameter"></param>
/// <returns></returns>
[HttpPost]
public List<CommissionComponent> GetIncentiveTripReport(string s, int[] reportparameter)
{
    try
    {
        Utility.AuditLogHelper.InfoLogMessage("Start to get incentive trip report info");

        Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

        List<CommissionComponent> e = servicelibrary.GetIncentiveTripReport(User, reportparameter);

        Utility.AuditLogHelper.InfoLogMessage("End to get incentive trip report info");

        return e;
    }
    catch (Exception e)
    {
        string message = "Get incentive trip report could not be retrieved. " + e.Message;
        OnHttpResponseMessage(message);
        Utility.AuditLogHelper.ErrorLog("Get incentive trip report could not be retrieved", e);
    }

    return null;
}

/// <summary>
/// To call GetGeneralLedgerReport method with session ID.
/// </summary>
/// <param name="report"></param>
/// <returns></returns>
[HttpPost]
public List<GeneralLedgerReports> GetGeneralLedgerReport(ReportParameter report)
{
    string sessionid = null;
    try
    {
        sessionid = Request.Properties[ApiControllers.Handlers.ApiSessionHandler.SessionIdToken] as string;
    }
    catch
    {
    }

    if (sessionid == null)
    {
        sessionid = System.Web.HttpContext.Current.Session.SessionID;
    }
    return GetGeneralLedgerReport(sessionid, report);
}

/// <summary>
/// To get General Ledger report details.
/// </summary>
/// <param name="s"></param>
/// <param name="report"></param>
/// <returns></returns>
[HttpPost]
public List<GeneralLedgerReports> GetGeneralLedgerReport(string s, ReportParameter report)
{
    try
    {
        Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

        List<GeneralLedgerReports> GeneralLedgerlist = servicelibrary.GetGeneralLedgerReport(User, report);

        return GeneralLedgerlist;
    }
    catch (Exception e)
    {
        string message = "Get General Ledger report could not be retrieved. " + e.Message;
        OnHttpResponseMessage(message);
        Utility.AuditLogHelper.ErrorLog("Get General Ledger report could not be retrieved", e);
    }

    return null;
}
/// <summary>
/// To call SaveReportdetails method with session ID.
/// </summary>
/// <param name="reportparameter"></param>
/// <returns></returns>
public bool SaveReportdetails(ReportParameter reportparameter)
{
    string sessionid = null;
    try
    {
        sessionid = Request.Properties[ApiControllers.Handlers.ApiSessionHandler.SessionIdToken] as string;
    }
    catch
    {
    }

    if (sessionid == null)
    {
        sessionid = System.Web.HttpContext.Current.Session.SessionID;
    }
    return SaveReportdetails(sessionid, reportparameter);
}

/// <summary>
/// To save report commission report details
/// </summary>
/// <param name="s"></param>
/// <param name="report"></param>
/// <returns></returns>
[HttpPost]
public bool SaveReportdetails(string s, ReportParameter report)
{
    Boolean status = new Boolean();
    try
    {
        Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

        List<ReportParameter> GetUIDlist = servicelibrary.GetPaylocityReport(User, report);

        ReportParameter reportParam = new ReportParameter();

        List<PaylocityReports> Paylocityreportlist = new List<PaylocityReports>();

        List<PayrollConfig> runlist = servicelibrary.GetPayrollConfig4Run(User, report.ReportPeriod.Year, report.ReportPeriod.Month);

        int RunNo = 1;
        foreach (PayrollConfig obj in runlist)
        {
            if (obj.ID == report.PayrollConfigID)
            {
                report.RunNo = RunNo;
            }
            RunNo++;
        }

        if (report.RunNo == runlist.Count)
        {
            report.IsFinalRun = true;
        }

        for (var i = 0; i < GetUIDlist.Count; i++)
        {
            reportParam.SalesPerson = GetUIDlist[i].SalesPerson;
            reportParam.ReportMonth = report.ReportPeriod.Month;
            reportParam.ReportYear = report.ReportPeriod.Year;
            reportParam.PlanID = GetUIDlist[i].PlanID;
            reportParam.MonthsofExp = GetUIDlist[i].MonthsofExp;
            reportParam.BranchID = report.BranchID;
            reportParam.PayrollConfigID = report.PayrollConfigID;
            reportParam.IsFinalRun = report.IsFinalRun;
            reportParam.RunNo = report.RunNo;
            status = servicelibrary.SaveReportdetails(User, reportParam);
        }

        return status;
    }
    catch (Exception e)
    {
        string message = "Save Report details could not be executed." + e.Message;
        OnHttpResponseMessage(message);
        Utility.AuditLogHelper.ErrorLog("Save Report details could not be executed", e);
    }

    return false;
}
/// <summary>
/// To call GetCountforPaylocity method with session ID.
/// </summary>
/// <param name="Reportparameter"></param>
/// <returns></returns>
[HttpPost]
public int[] GetCountforPaylocity(ReportParameter Reportparameter)
{
    string sessionid = null;
    try
    {
        sessionid = Request.Properties[ApiControllers.Handlers.ApiSessionHandler.SessionIdToken] as string;
    }
    catch
    {
    }

    if (sessionid == null)
    {
        sessionid = System.Web.HttpContext.Current.Session.SessionID;
    }
    return GetCountforPaylocity(sessionid, Reportparameter);
}

/// <summary>
/// To get Payroll accepted commission count for Paylocity report.
/// </summary>
/// <param name="s"></param>
/// <param name="Reportparameter"></param>
/// <returns></returns>
[HttpPost]
public int[] GetCountforPaylocity(string s, ReportParameter Reportparameter)
{
    try
    {
        Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

        return servicelibrary.GetCountforPaylocity(User, Reportparameter);
    }
    catch (Exception e)
    {
        string message = "Get Commission count for Paylocity report could not be retrieved. " + e.Message;
        OnHttpResponseMessage(message);
        Utility.AuditLogHelper.ErrorLog("Get Commission count for Paylocity report could not be retrieved", e);
    }

    return null;
}

        //public List<ReportParameter> GetDrawDeficit(string s, ReportParameter reportparameter, int PrimaryBranch)
        //{
        //    Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

        //    ReportParameter report = new ReportParameter();

        //    List<ReportParameter> Reportlist = new List<ReportParameter>();

        //    EmployeeComponant employeedetail = new EmployeeComponant();
        //    employeedetail.UID = reportparameter.SalesPerson;
        //    employeedetail = servicelibrary.GetEmployeeByID(User, employeedetail);

        //    employeedetail.DRPercentage = employeedetail.DRPercentage * Convert.ToDecimal(0.01);

        //    DateTime applicableTill = new DateTime();

        //    applicableTill = employeedetail.DDPeriod.AddMonths(employeedetail.DrawTerm); 

        //    if (employeedetail.TypeofDraw == 1)
        //    {

        //    }
        //    else
        //    {
        //        //if drawdeficit 
        //    }

        //    try
        //    {
        //        List<PayrollConfig> runList = new List<PayrollConfig>();

        //        //using (DataTable dt = ReportingStoredProcedures.GetPayrollConfig4TotEarning(year, month))
        //        //{
        //        //    foreach (DataRow dr in dt.Rows)
        //        //    {
        //        //        runList.Add(new PayrollConfig
        //        //        {
        //        //            ID = Convert.ToInt32(dr["ID"].ToString()),
        //        //            DateFrom = Convert.ToDateTime(dr["DateFrom"].ToString()),
        //        //            DateTo = Convert.ToDateTime(dr["DateTo"].ToString())
        //        //        });
        //        //    }

        //        //    return runList;
        //        //}
        //    }
        //    catch (Exception e)
        //    {
        //        UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetPayrollConfig4TotEarning: Failed to Get payroll config details" + e, System.Diagnostics.EventLogEntryType.Error);
        //        throw new ExceptionLog("Can't Get payroll config details." + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
        //    }
        //}
    }
}
