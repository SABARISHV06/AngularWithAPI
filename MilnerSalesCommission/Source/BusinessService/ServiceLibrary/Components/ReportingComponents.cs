// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//
using DBContext;
using ServiceLibrary.StoredProcedures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Utility;
using ViewModels;

namespace ServiceLibrary
{
    /// <summary>
    /// This Class is implemented Report related components
    /// </summary>
    public partial class Servicelibrary : IDisposable
    {
        
        /// <summary>
        /// To get Commission details for Commission Report
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Reportparameter"></param>
        /// <returns></returns>
        public List<CommissionComponent> GetCommissionReport(IPrincipal user, ReportParameter Reportparameter)
        {
            DateTime start = DateTime.Now;

            string message = $"GetCommissionReport\t{user.Identity.Name}";

            try
            {
                List<CommissionComponent> Commissionlist = new List<CommissionComponent>();

                using (DataTable dt = ReportingStoredProcedures.GetCommissionReport(Reportparameter))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Commissionlist.Add(new CommissionComponent
                        {
                            ID = Convert.ToInt32(dr["ID"]),
                            InvoiceNumber = dr["InvoiceNumber"].ToString(),
                            CustomerName = dr["CustomerName"].ToString(),
                            CommentSold = dr["CommentSold"].ToString(),
                            SplitSalePerson = dr["SplitSalePerson"].ToString(),
                            TradeIn = Convert.ToDecimal(dr["TradeIn"]),
                            DollarVolume = Convert.ToInt32(dr["SlipType"]) == 1 ? Convert.ToDecimal(dr["DollarVolume"]): Convert.ToDecimal(0.00),
                            BaseCommission = Convert.ToDecimal(dr["BaseCommission"]),
                            LeaseCommission = Convert.ToDecimal(dr["LeaseCommission"]),
                            ServiceCommission = Convert.ToDecimal(dr["ServiceCommission"]),
                            TravelCommission = Convert.ToDecimal(dr["TravelCommission"]),
                            SpecialCommission = Convert.ToDecimal(dr["SpecialCommission"]),
                            CashCommission = Convert.ToDecimal(dr["CashCommission"]),
                            TipLeadAmount = Convert.ToInt32(dr["SlipType"]) == 1 ? Convert.ToDecimal(dr["TipLeadAmount"]) : Convert.ToDecimal(0.00),
                            PositiveAdjustments = Convert.ToDecimal(dr["PositiveAdjustments"]),
                            NegativeAdjustments = Convert.ToDecimal(dr["NegativeAdjustments"]),
                            CompanyContribution = Convert.ToInt32(dr["SlipType"]) == 2 ? Convert.ToDecimal(dr["CompanyContribution"]) + Convert.ToDecimal(dr["TipLeadAmount"]) : Convert.ToDecimal(0.00),
                            TotalCEarned = Convert.ToDecimal(dr["TotalCEarned"]),
                            SlipType = Convert.ToInt32(dr["SlipType"]),
                            ProcesByPayroll = Convert.ToBoolean(dr["ProcesByPayroll"]),
                        });
                    }
                }
                
                return Commissionlist;
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetCommissionReport  : Failed to Get Commission Report" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get Commission report." + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }
        /// <summary>
        /// To get Commission details for Commission report.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="reportparameter"></param>
        /// <returns></returns>
        public ReportParameter GetCommissionReportDetails(IPrincipal user, ReportParameter reportparameter)
        {
            DateTime start = DateTime.Now;

            string message = $"GetCommissionReportDetails\t{user.Identity.Name}";

            try
            {
                ReportParameter Report = new ReportParameter();

                using (DataTable dt = ReportingStoredProcedures.GetCommissionReportDetails(reportparameter))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Report.TotalCommission = Convert.ToDecimal(dr["Totalcommission"]);
                        Report.CommissionDue = Convert.ToDecimal(dr["Commissiondue"]);
                        Report.DrawPaid = Convert.ToDecimal(dr["Drawpaid"]);
                        Report.DrawRecovered = Convert.ToDecimal(dr["Drawrecovered"]);
                        Report.DrawDificit = Convert.ToDecimal(dr["Drawdificit"]);
                        Report.Applicabletill = dr["Applicabletill"].ToString();
                        Report.DrawAmount = Convert.ToDecimal(dr["DrawAmount"]);
                        Report.DrawType = Convert.ToInt32(dr["DrawType"]);
                        Report.RecoverablePercent = Convert.ToDecimal(dr["RecoverablePercent"]);
                        Report.Bimonthreport = Convert.ToDecimal(dr["Bimonthreport"]);
                        Report.Tenurereport = Convert.ToDecimal(dr["Tenurereport"]);
                        Report.TotalEarning = Convert.ToDecimal(dr["TotalEarning"]);
                        Report.Salary = Convert.ToDecimal(dr["Salary"]);
                        Report.EmployeeID = dr["EmployeeID"].ToString();
                    }
                }
                return Report;
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetCommissionReportDetails  : Failed to Get Commission details for Commission report" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get Commission details for Commission report." + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }

        }

        /// <summary>
        /// To get Employee list for Reports.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="UserandRoleID"></param>
        /// <returns></returns>
        public List<EmployeeComponant> GetSalesReplistforReport(IPrincipal user, int[] UserandRoleID)
        {
            DateTime start = DateTime.Now;

            string message = $"GetSalesReplistforReport\t{user.Identity.Name}";

            try
            {
                List<EmployeeComponant> EmployeeList = new List<EmployeeComponant>();

                using (DataTable dt = ReportingStoredProcedures.GetSalesReplistforReport(UserandRoleID))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EmployeeList.Add(new EmployeeComponant
                        {
                            UID = Convert.ToInt32(dr["UID"].ToString()),
                            AccountName = dr["AccountName"].ToString(),
                            EmployeeID = dr["EmployeeID"].ToString(),
                            FirstName = dr["FirstName"].ToString(),
                            LastName = dr["LastName"].ToString(),
                            RoleID = Convert.ToInt32(dr["RoleID"])
                        });
                    }

                    return EmployeeList;
                }
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetSalesReplistforReport: Failed to Get Employee details for report" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get Employee details for report. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To get Commission slip details for Paylocity report.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="reportparameter"></param>
        /// <param name="PrimaryBranch"></param>
        /// <returns></returns>
        public PaylocityReports GetCommissionReportDetails4Paylocity(IPrincipal user, ReportParameter reportparameter, int PrimaryBranch)
        {
            DateTime start = DateTime.Now;

            string message = $"GetCommissionReportDetails4Paylocity\t{user.Identity.Name}";

            try
            {
                Decimal TotalCommission = new Decimal();
                PaylocityReports Report = new PaylocityReports();
                TotalCommission = ReportingStoredProcedures.GetBranchTotalCommission(reportparameter);

                using (DataTable dt = ReportingStoredProcedures.GetCommissionReportDetails(reportparameter))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Report.EmployeeID = dr["EmployeeID"].ToString();
                        Report.Earnings = "E";
                        Report.Commission = "COMM";
                        if (reportparameter.BranchID == PrimaryBranch)
                        {
                            if (reportparameter.IsFinalRun == true)
                            {
                                Report.Amount = (TotalCommission - Convert.ToDecimal(dr["DrawRecovered"])) + Convert.ToDecimal(dr["Drawpaid"]) + Convert.ToDecimal(dr["Bimonthreport"]) + Convert.ToDecimal(dr["Tenurereport"]) + Convert.ToDecimal(dr["Salary"]);
                            }
                            else
                            {
                                Report.Amount = (TotalCommission - Convert.ToDecimal(dr["DrawRecovered"]));
                            }
                        }
                        else
                        {
                            Report.Amount = TotalCommission;
                        }
                    }
                }
                return Report;
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetCommissionReportDetails4Paylocity  : Failed to Get commission report details for Paylocity" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get Commission report details for Paylocity." + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To get Paylocity report details.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Report"></param>
        /// <returns></returns>
        public List<ReportParameter> GetPaylocityReport(IPrincipal user, ReportParameter Report)
        {
            DateTime start = DateTime.Now;

            string message = $"GetPaylocityReport\t{user.Identity.Name}";

            try
            {
                List<ReportParameter> PaylocityList = new List<ReportParameter>();

                using (DataTable dt = ReportingStoredProcedures.GetPaylocityReport(Report))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        PaylocityList.Add(new ReportParameter
                        {
                            SalesPerson = Convert.ToInt16(dr["UID"]),
                            MonthsofExp = Convert.ToInt16(dr["MonthsinExp"]),
                            PlanID = Convert.ToInt16(dr["PayPlanID"]),
                            BranchID = Convert.ToInt32(dr["BranchID"]),
                            //IsFinalRun = String.IsNullOrEmpty(dr["IsFinalRunDate"].ToString()) ? false : true,
                        });
                    }
                }
                return PaylocityList;
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetPaylocityReport  : Failed to Get Paylocity Report" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get Paylocity report." + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }
        /// <summary>
        /// To get Incentive trip report details.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="reportparameter"></param>
        /// <returns></returns>
        public List<CommissionComponent> GetIncentiveTripReport(IPrincipal user, int[] reportparameter)
        {
            DateTime start = DateTime.Now;

            string message = $"GetIncentiveTripReport\t{user.Identity.Name}";

            try
            {
                List<CommissionComponent> Incentivelist = new List<CommissionComponent>();

                using (DataTable dt = ReportingStoredProcedures.GetIncentiveTripReport(reportparameter))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Incentivelist.Add(new CommissionComponent
                        {
                            SalesPerson = (dr["Name"]).ToString(),
                            DollarVolume = Convert.ToDecimal(dr["DollarVolume"]),

                        });
                    }
                }
                return Incentivelist;
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetIncentiveTripReport  : Failed to Get IncentiveTrip Report" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get IncentiveTrip report." + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }
        /// <summary>
        /// To get General Ledger Report details.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="report"></param>
        /// <returns></returns>
        public List<GeneralLedgerReports> GetGeneralLedgerReport(IPrincipal user, ReportParameter report)
        {
            DateTime start = DateTime.Now;

            string message = $"GetGeneralLedgerReport\t{user.Identity.Name}";

            try
            {
                List<GeneralLedgerReports> GeneralLedgerReportsList = new List<GeneralLedgerReports>();

                using (DataTable dt = ReportingStoredProcedures.GetGeneralLedgerReport(report))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        GeneralLedgerReportsList.Add(new GeneralLedgerReports
                        {
                            BranchName = dr["BranchName"].ToString(),
                            TotalCommissionPaid = Convert.ToDecimal(dr["TotalCommissionPaid"])
                        });
                    }
                }
                return GeneralLedgerReportsList;
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetGeneralLedgerReport  : Failed to Get General Ledger Report" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get General Ledger report." + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To save report details
        /// </summary>
        /// <param name="user"></param>
        /// <param name="reportparameter"></param>
        /// <returns></returns>
        public bool SaveReportdetails(IPrincipal user, ReportParameter reportparameter)
        {
            DateTime start = DateTime.Now;

            string message = $"SaveReportdetails\t{user.Identity.Name}";
            try
            {
                return ReportingStoredProcedures.SaveReportdetails(reportparameter);
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => SaveReportdetails : Failed to Save Report details" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Save Report details. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To get Payroll accepted commission slip count for Paylocity report.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Report"></param>
        /// <returns></returns>
        public int[] GetCountforPaylocity(IPrincipal user, ReportParameter Report)
        {
            DateTime start = DateTime.Now;

            string message = $"GetCountforPaylocity\t{user.Identity.Name}";

            try
            {
                List<EmployeeComponant> EmployeeList = new List<EmployeeComponant>();

                return ReportingStoredProcedures.GetCountforPaylocity(Report);

            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetCountforPaylocity: Failed to Get Commissionslip count for Paylocity report" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get Commission count for Paylocity report. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To get Payroll config list for RUN calculation.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<PayrollConfig> GetPayrollConfig4Run(IPrincipal user, int Year, int Month)
        {
            DateTime start = DateTime.Now;

            string message = $"GetPayrollConfig4Run\t{user.Identity.Name}";

            try
            {
                List<PayrollConfig> runList = new List<PayrollConfig>();

                using (DataTable dt = ReportingStoredProcedures.GetPayrollConfig4Run(Year,Month))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        runList.Add(new PayrollConfig
                        {
                            ID = Convert.ToInt32(dr["ID"].ToString()),
                            DateFrom = Convert.ToDateTime(dr["DateFrom"].ToString()),
                            DateTo = Convert.ToDateTime(dr["DateTo"].ToString())
                        });
                    }

                    return runList;
                }
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetSalesReplistforReport: Failed to Get Employee details for report" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get Employee details for report. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        public List<PayrollConfig> GetPayrollConfig4TotEarning(IPrincipal user, int year, int month)
        {
            DateTime start = DateTime.Now;

            string message = $"GetPayrollConfig4Run\t{user.Identity.Name}";

            try
            {
                List<PayrollConfig> runList = new List<PayrollConfig>();

                using (DataTable dt = ReportingStoredProcedures.GetPayrollConfig4TotEarning(year, month))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        runList.Add(new PayrollConfig
                        {
                            //ID = Convert.ToInt32(dr["ID"].ToString()),
                            //DateFrom = Convert.ToDateTime(dr["DateFrom"].ToString()),
                            DateTo = Convert.ToDateTime(dr["DateTo"].ToString())
                        });
                    }

                    return runList;
                }
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetPayrollConfig4TotEarning: Failed to Get payroll config details" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get payroll config details." + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }
    }
}
