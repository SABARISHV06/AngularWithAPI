// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//

using System;
using DBContext;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utility;
using System.Security.Principal;
using ViewModels;

namespace ServiceLibrary.StoredProcedures
{
    /// <summary>
    /// This class implemented Reports related stored procedures
    /// </summary>
    internal static class ReportingStoredProcedures
    {

        /// <summary>
        /// To get Commission details for Commision Report
        /// </summary>
        /// <param name="Reportparameter"></param>
        /// <returns></returns>

        internal static DataTable GetCommissionReport(ReportParameter Reportparameter)
        {
            DataTable datatable = new DataTable();
            try
            {
                SqlParameter[] Params = new SqlParameter[2];

                Params[0] = new SqlParameter("@UID", SqlDbType.Int);
                Params[0].Value = Reportparameter.SalesPerson;
                Params[1] = new SqlParameter("@PayrollConfigID", SqlDbType.Int);
                Params[1].Value = Reportparameter.PayrollConfigID;

                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_GETCOMMISSIONREPORT, Params);

                datatable = ds.Tables[0];
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetCommissionReport : Failed to Get Commission Report" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Commission Report.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }


        /// <summary>
        /// To get Commission details for Commision Report
        /// </summary>
        /// <param name="reportparameter"></param>
        /// <returns></returns>
        internal static DataTable GetCommissionReportDetails(ReportParameter reportparameter)
        {
            DataTable datatable = new DataTable();
            // DataTable dt = new DataTable();
            datatable.Columns.AddRange(new DataColumn[14] { new DataColumn("Totalcommission", typeof(decimal)),
                            new DataColumn("Commissiondue", typeof(decimal)),
                            new DataColumn("Drawpaid", typeof(decimal)),
                            new DataColumn("Drawrecovered", typeof(decimal)),
                            new DataColumn("Drawdificit", typeof(decimal)),
                            new DataColumn("Applicabletill",typeof(string)),
                            new DataColumn("DrawAmount",typeof(decimal)),
                            new DataColumn("DrawType",typeof(int)),
                            new DataColumn("RecoverablePercent",typeof(decimal)),
                            new DataColumn("Bimonthreport", typeof(decimal)),
                            new DataColumn("Tenurereport", typeof(decimal)),
                            new DataColumn("TotalEarning", typeof(decimal)),
                            new DataColumn("Salary",typeof(decimal)),
                            new DataColumn("EmployeeID",typeof(string))});
            // int Percentage = 0;
            try
            {
                SqlParameter[] Params = new SqlParameter[22];

                Params[0] = new SqlParameter("@UID", SqlDbType.Int);
                Params[0].Value = reportparameter.SalesPerson;
                Params[1] = new SqlParameter("@Month", SqlDbType.Int);
                Params[1].Value = reportparameter.ReportMonth;
                Params[2] = new SqlParameter("@Year", SqlDbType.Int);
                Params[2].Value = reportparameter.ReportYear;
                Params[3] = new SqlParameter("@PlanID", SqlDbType.Int);
                Params[3].Value = reportparameter.PlanID;
                Params[4] = new SqlParameter("@Monthsofexp", SqlDbType.Int);
                Params[4].Value = reportparameter.MonthsofExp;
                Params[5] = new SqlParameter("@totalcommission", SqlDbType.Decimal);
                Params[5].Value = 0;
                Params[5].Precision = 20;
                Params[5].Scale = 2;
                Params[5].Direction = ParameterDirection.Output;
                Params[6] = new SqlParameter("@commissiondue", SqlDbType.Decimal);
                Params[6].Value = 0;
                Params[6].Precision = 20;
                Params[6].Scale = 2;
                Params[6].Direction = ParameterDirection.Output;
                Params[7] = new SqlParameter("@drawpaid", SqlDbType.Decimal);
                Params[7].Value = 0;
                Params[7].Precision = 20;
                Params[7].Scale = 2;
                Params[7].Direction = ParameterDirection.Output;
                Params[8] = new SqlParameter("@drawrecoverable", SqlDbType.Decimal);
                Params[8].Value = 0;
                Params[8].Precision = 20;
                Params[8].Scale = 2;
                Params[8].Direction = ParameterDirection.Output;
                Params[9] = new SqlParameter("@drawdificit", SqlDbType.Decimal);
                Params[9].Value = 0;
                Params[9].Precision = 20;
                Params[9].Scale = 2;
                Params[9].Direction = ParameterDirection.Output;
                Params[10] = new SqlParameter("@Applicableperiod", SqlDbType.NVarChar, 255);
                Params[10].Value = string.Empty;
                Params[10].Direction = ParameterDirection.Output;
                Params[11] = new SqlParameter("@Drawamount", SqlDbType.Decimal);
                Params[11].Value = 0;
                Params[11].Precision = 20;
                Params[11].Scale = 2;
                Params[11].Direction = ParameterDirection.Output;
                Params[12] = new SqlParameter("@Drawtype", SqlDbType.Int);
                Params[12].Value = 0;
                Params[12].Direction = ParameterDirection.Output;
                Params[13] = new SqlParameter("@Recoverablepercent", SqlDbType.Decimal);
                Params[13].Value = 0;
                Params[13].Precision = 20;
                Params[13].Scale = 2;
                Params[13].Direction = ParameterDirection.Output;
                Params[14] = new SqlParameter("@Bimonthreport", SqlDbType.Decimal);
                Params[14].Value = 0;
                Params[14].Precision = 20;
                Params[14].Scale = 2;
                Params[14].Direction = ParameterDirection.Output;
                Params[15] = new SqlParameter("@Tenurereport", SqlDbType.Decimal);
                Params[15].Value = 0;
                Params[15].Precision = 20;
                Params[15].Scale = 2;
                Params[15].Direction = ParameterDirection.Output;
                Params[16] = new SqlParameter("@TotalEarning", SqlDbType.Decimal);
                Params[16].Value = 0;
                Params[16].Precision = 20;
                Params[16].Scale = 2;
                Params[16].Direction = ParameterDirection.Output;
                Params[17] = new SqlParameter("@salary", SqlDbType.Decimal);
                Params[17].Value = 0;
                Params[17].Precision = 20;
                Params[17].Scale = 2;
                Params[17].Direction = ParameterDirection.Output;
                Params[18] = new SqlParameter("@EmployeeID", SqlDbType.NVarChar, 255);
                Params[18].Value = string.Empty;
                Params[18].Direction = ParameterDirection.Output;
                Params[19] = new SqlParameter("@PayrollConfigID", SqlDbType.Int);
                Params[19].Value = reportparameter.PayrollConfigID;
                Params[20] = new SqlParameter("@IsFinalRun", SqlDbType.Bit);
                Params[20].Value = reportparameter.IsFinalRun == true ? reportparameter.IsFinalRun: false;
                Params[21] = new SqlParameter("@RunNo", SqlDbType.Int);
                Params[21].Value = reportparameter.RunNo;
                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_GETCOMMISSIONSLIPREPORT, Params);

                datatable.Rows.Add(Convert.ToDecimal(Params[5].Value), Convert.ToDecimal(Params[6].Value), Convert.ToDecimal(Params[7].Value), Convert.ToDecimal(Params[8].Value), Convert.ToDecimal(Params[9].Value),
                   Params[10].Value.ToString(), Convert.ToDecimal(Params[11].Value), Convert.ToInt32(Params[12].Value), Convert.ToDecimal(Params[13].Value), Convert.ToDecimal(Params[14].Value), 
                   Convert.ToDecimal(Params[15].Value), Convert.ToDecimal(Params[16].Value), Convert.ToDecimal(Params[17].Value), Params[18].Value.ToString());
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetCommissionReportDetails : Failed to Get GetCommission slip Report Details" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get GetCommission slip Report Details.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }
        /// <summary>
        /// To get Employee lists for Reports.
        /// </summary>
        /// <param name="UserandRoleID"></param>
        /// <returns></returns>
        internal static DataTable GetSalesReplistforReport(int[] UserandRoleID)
        {
            DataTable datatable = new DataTable();
            try
            {
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@UID", SqlDbType.Int);
                Params[0].Value = UserandRoleID[0];
                Params[1] = new SqlParameter("@RoleID", SqlDbType.Int);
                Params[1].Value = UserandRoleID[1];
                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_GETREPORTSALESREPLIST, Params);
                datatable = ds.Tables[0];
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetSalesReplistforReport : Failed to get all Employee details for report" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get all Employee details for report.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }
        /// <summary>
        /// To get Paylocity report details.
        /// </summary>
        /// <param name="Report"></param>
        /// <returns></returns>
        internal static DataTable GetPaylocityReport(ReportParameter Report)
        {
            DataTable datatable = new DataTable();
            try
            {
                SqlParameter[] Params = new SqlParameter[3];

                Params[0] = new SqlParameter("@SelectedDate", SqlDbType.DateTime);
                Params[0].Value = Report.ReportPeriod;
                Params[1] = new SqlParameter("@PayrollConfigID", SqlDbType.Int);
                Params[1].Value = Report.PayrollConfigID;
                Params[2] = new SqlParameter("@BranchID", SqlDbType.Int);
                Params[2].Value = Report.BranchID;

                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_GETPAYLOCITYREPORT, Params);

                datatable = ds.Tables[0];
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetPaylocityReport : Failed to Get Paylocity Report" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Paylocity Report.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }
        /// <summary>
        /// To get Total Commission for selected branch for Paylocity report.
        /// </summary>
        /// <param name="Reportparameter"></param>
        /// <returns></returns>
        internal static decimal GetBranchTotalCommission(ReportParameter Reportparameter)
        {
            // DataTable datatable = new DataTable();
            Decimal Totalcommission = new Decimal();
            try
            {
                SqlParameter[] Params = new SqlParameter[4];

                Params[0] = new SqlParameter("@UID", SqlDbType.Int);
                Params[0].Value = Reportparameter.SalesPerson;
                Params[1] = new SqlParameter("@PayrollConfigID", SqlDbType.Int);
                Params[1].Value = Reportparameter.@PayrollConfigID;
                Params[2] = new SqlParameter("@BranchID", SqlDbType.Int);
                Params[2].Value = Reportparameter.BranchID;
                Params[3] = new SqlParameter("@Totalcommission", SqlDbType.Decimal);
                Params[3].Value = 0;
                Params[3].Precision = 20;
                Params[3].Scale = 2;
                Params[3].Direction = ParameterDirection.Output;

                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_GETBRANCHTOTALCOMMISSION, Params);

                Totalcommission= Convert.ToDecimal(Params[3].Value);
               
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetBranchTotalCommission : Failed to Get Total Commission" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Total Commission .", TErrorMessageType.ERROR, e);
            }

            return Totalcommission;
        }
        /// <summary>
        /// To get Incentive trip report details.
        /// </summary>
        /// <param name="reportparameter"></param>
        /// <returns></returns>
        internal static DataTable GetIncentiveTripReport(int[] reportparameter)
        {
            DataTable datatable = new DataTable();
            try
            {
                SqlParameter[] Params = new SqlParameter[3];

                Params[0] = new SqlParameter("@UID", SqlDbType.Int);
                Params[0].Value = reportparameter[0];
                Params[1] = new SqlParameter("@RoleID", SqlDbType.Int);
                Params[1].Value = reportparameter[1];
                Params[2] = new SqlParameter("@StartYear", SqlDbType.Int);
                Params[2].Value = reportparameter[2];

                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_GETINCENTIVETRIPREPORT, Params);

                datatable = ds.Tables[0];
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetIncentiveTripReport : Failed to Get IncentiveTrip Report" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get IncentiveTrip Report.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }
        /// <summary>
        /// To get General Ledger Report details.
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        internal static DataTable GetGeneralLedgerReport(ReportParameter report)
        {
            DataTable datatable = new DataTable();
            try
            {
                SqlParameter[] Params = new SqlParameter[1];

                Params[0] = new SqlParameter("@PayrollConfigID", SqlDbType.Int);
                Params[0].Value = report.PayrollConfigID;

                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_GETGENERALLEDGERREPORT, Params);

                datatable = ds.Tables[0];
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetGeneralLedgerReport : Failed to General Ledger Report" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get GeneralLedger Report.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }
        /// <summary>
        /// To save report details
        /// </summary>
        /// <param name="reportparameter"></param>
        /// <returns></returns>
        internal static bool SaveReportdetails(ReportParameter reportparameter)
        {
            bool boolResult = false;
            try
            {
                SqlParameter[] Params = new SqlParameter[10];

                Params[0] = new SqlParameter("@UID", SqlDbType.Int);
                Params[0].Value = reportparameter.SalesPerson;
                Params[1] = new SqlParameter("@Month", SqlDbType.Int);
                Params[1].Value = reportparameter.ReportMonth;
                Params[2] = new SqlParameter("@Year", SqlDbType.Int);
                Params[2].Value = reportparameter.ReportYear;
                Params[3] = new SqlParameter("@PlanID", SqlDbType.Int);
                Params[3].Value = reportparameter.PlanID;
                Params[4] = new SqlParameter("@Monthsofexp", SqlDbType.Int);
                Params[4].Value = reportparameter.MonthsofExp;
                Params[5] = new SqlParameter("@BranchID", SqlDbType.Int);
                Params[5].Value = reportparameter.BranchID;
                Params[6] = new SqlParameter("@IsFinalRun", SqlDbType.Bit);
                Params[6].Value = reportparameter.IsFinalRun;
                Params[7] = new SqlParameter("@PayrollConfigID", SqlDbType.Int);
                Params[7].Value = reportparameter.PayrollConfigID;
                Params[8] = new SqlParameter("@RunNo", SqlDbType.Int);
                Params[8].Value = reportparameter.RunNo;
                Params[9] = new SqlParameter("@RETURN_VALUE", SqlDbType.Int);
                Params[9].Value = string.Empty;
                Params[9].Direction = ParameterDirection.Output;

                //Params[8] = new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, ((Byte)(0)), ((Byte)(0)), "", DataRowVersion.Current, null);

                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_SAVEREPORTDETAILS, Params);

                if (Convert.ToInt16(Params[9].Value) != 0)
                {
                    throw new Exception("Unable to Save Report details.");
                }
                else
                {
                    boolResult = true;
                }
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => SaveReportdetails : Failed to Save Report details" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Save Report details.", TErrorMessageType.ERROR, e);
            }

            return boolResult;
        }
        /// <summary>
        /// To get Payroll accepted commission slip count for Paylocity report.
        /// </summary>
        /// <param name="Report"></param>
        /// <returns></returns>
        internal static int[] GetCountforPaylocity(ReportParameter Report)
        {
            // DataTable datatable = new DataTable();
            int[] Statuscount = new int[2];
            try
            {
                SqlParameter[] Params = new SqlParameter[3];

                Params[0] = new SqlParameter("@PayrollConfigID", SqlDbType.Int);
                Params[0].Value = Report.PayrollConfigID; //Report.ReportMonth;
                Params[1] = new SqlParameter("@Acceptedcount", SqlDbType.Int);
                Params[1].Value = 0;
                Params[1].Direction = ParameterDirection.Output;
                Params[2] = new SqlParameter("@Pendingcount", SqlDbType.Int);
                Params[2].Value = 0;
                Params[2].Direction = ParameterDirection.Output;

                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_GETCOUNTFORPAYLOCITY, Params);
                Statuscount[0] = Convert.ToInt32(Params[1].Value);
                Statuscount[1] = Convert.ToInt32(Params[2].Value);
                
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetCountforPaylocity : Failed to Get Commission count for Paylocity report" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Commission count for Paylocity report.", TErrorMessageType.ERROR, e);
            }

            return Statuscount;
        }

        /// <summary>
        /// To get Payroll config list for RUN calculation.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        internal static DataTable GetPayrollConfig4Run(int Year, int Month)
        {
            DataTable datatable = new DataTable();

            try
            {
                SqlParameter[] Params = new SqlParameter[2];

                Params[0] = new SqlParameter("@Year", SqlDbType.Int);
                Params[0].Value = Year;
                Params[1] = new SqlParameter("@Month", SqlDbType.Int);
                Params[1].Value = Month;

                DataSet ds = new DataSet();

                ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_GETPAYROLLCONFIG4RUN, Params);

                datatable = ds.Tables[0];

            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetPayrollConfig4Run : Failed to get Payroll config list for RUN calculation." + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Payroll config list for RUN calculation.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }

        internal static DataTable GetPayrollConfig4TotEarning(int year, int month)
        {
            DataTable datatable = new DataTable();

            try
            {
                SqlParameter[] Params = new SqlParameter[2];

                Params[0] = new SqlParameter("@Year", SqlDbType.Int);
                Params[0].Value = year;
                Params[1] = new SqlParameter("@Month", SqlDbType.Int);
                Params[1].Value = month;

                DataSet ds = new DataSet();

                ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_GETPAYROLLCONFIG4TOTEARNING, Params);

                datatable = ds.Tables[0];

            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetPayrollConfig4TotEarning : Failed to get Payroll config list for Total earning." + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Payroll config list for RUN calculation.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }

    }
}
