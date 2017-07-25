// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//

using DBContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using ViewModels;

namespace ServiceLibrary.StoredProcedures
{
    /// <summary>
    /// String Constants for Stored Procedures Names
    /// </summary>
    internal static class StringConstants
    {
        public const string M_SP_DELETEEMPLOYEEBYID = "DeleteEmployeeByID";
        public const string M_SP_SETAUDITLOGS = "SetAuditLogs";
        public const string M_SP_GETALLROLES = "GetAllRoles";
        public const string M_SP_GETROLEOPERATIONS = "GetRoleOperations";
        public const string M_SP_GETALLROLEOPERATIONS = "GetAllRoleOperations";
        public const string M_SP_GETROLEOPERATIONSBYROLEID = "GetRoleOperationsByRoleID";
        public const string M_SP_ADDPLANCOMPONENTS = "AddPlanComponents";
        public const string M_SP_SETPLANCOMPONENTS = "SetPlanComponents";
        public const string M_SP_GETPLANCOMPONENTS = "GetPlanComponents";
        public const string M_SP_GETPLANCOMPONENTSBYID = "GetPlanComponentsByID";
        public const string M_SP_DELETEPLANCOMPONENTSBYID = "DeletePlanComponentsByID";
        public const string M_SP_GETTGPCUSTOMERINFO = "GetTGPCustomerInfo";
        public const string M_SP_GETTENUREBONUS = "GetTenureBonus";
        public const string M_SP_GETBIMONTHLYBONUSINFO = "GetBiMonthlyBonusInfo";
        public const string M_SP_ADDCOMMISSION = "AddCommission";
        public const string M_SP_ADDEMPLOYEE = "AddEmployee";
        public const string M_SP_DELETECOMMISSIONBYID = "DeleteCommissionByID";
        public const string M_SP_ACTIVEDEACTIVEEMPLOYEE = "ActiveDeactiveEmployee";
        public const string M_SP_GETALLACTIVECOMMISSION = "GetAllActiveCommission";
        public const string M_SP_GETALLBRANCH = "GetAllBranch";
        public const string M_SP_GETALLMANAGER = "GetAllManager";
        public const string M_SP_GETALLACTIVECOMMISSIONBYUID = "GetAllActiveCommissionbyUID";
        public const string M_SP_GETCOMMISSIONBYID = "GetCommissionByID";
        public const string M_SP_GETEMPLOYEEBYID = "GetEmployeeByID";
        public const string M_SP_SETCOMMISSION = "SetCommission";
        public const string M_SP_SETEMPLOYEE = "SetEmployee";
        public const string M_SP_GETEMPLOYEEDEACTPLAN = "GetEmployeeDeactPlan";
        public const string M_SP_GETNAMESBYROLEID = "GetNamesbyRoleID";
        public const string M_SP_ADDUSERSLOG = "AddUsersLog";
        public const string M_SP_SETUSERSLOG = "SetUsersLog";
        public const string M_SP_ADDFIRSTEMPLOYEE = "AddFirstEmployee";
        public const string M_SP_BIMONTHLYREPORT = "BiMonthlyReport";
        public const string M_SP_GETALLEMPLOYEEDETAILSBYROLEID = "GetAllEmployeeDetailsbyRoleID";
        public const string M_SP_GETALLEMPLOYEENAMES = "GetAllEmployeeNames";
        public const string M_SP_GETCOMMISSIONREPORT = "GetCommissionReport";
        public const string M_SP_COMMISSIONCREATEDNOTIFICATION = "CommissionCreatedNotification";
        public const string M_SP_GETNETDVFORREPORT = "GetNetDVforReport";
        public const string M_SP_GETBIMONTHLYPERCENTAGE = "GetBiMonthlyPercentage";
        public const string M_SP_GETTENUREPERCENTAGE = "GetTenurePercentage";
        public const string M_SP_GETCOMMISSIONSLIPREPORT = "GetCommissionslipreport";
        public const string M_SP_GETREPORTSALESREPLIST = "GetReportSalesReplist";
        public const string M_SP_GETPAYLOCITYREPORT = "GetUID4PaylocityReport";
        public const string M_SP_GETINCENTIVETRIPREPORT = "GetIncentiveTripReport";
        public const string M_SP_GETGENERALLEDGERREPORT = "GeneralLedgerReport";
        public const string M_SP_SAVEREPORTDETAILS = "SaveReportDetails";
        public const string M_SP_SETTIPLEADSLIP = "SetTipLeadSlip";
        public const string M_SP_GETBRANCHTOTALCOMMISSION = "GetBranchTotalCommission";
        public const string M_SP_GETCOUNTFORPAYLOCITY = "GetCountforPaylocity";
        public const string M_SP_GETDEACTIVEGMANDPLANID = "GetDeactiveGMandPlanID";
        public const string M_SP_SETBRANCH = "SetBranches";
        public const string M_SP_GETALLSALETYPE = "GetAllSaleType";
        public const string M_SP_SETSALETYPE = "SetSaleType";
        public const string M_SP_CHECKBRANCH = "CheckDeactivateBranch";
        public const string M_SP_CHECKSALETYPE = "CheckDeactivateSaleType";
        public const string M_SP_GETALLPAYROLLCONFIG = "GetAllPayrollConfigDetails";
        public const string M_SP_GETALLPAYROLLCONFIGBYYEAR = "GetAllPayrollConfigDetailsByYear";
        public const string M_SP_SETPAYROLL = "SetPayroll";
        public const string M_SP_ADDPAYROLL = "AddPayroll";
        public const string M_SP_GETALLACCOUNTINGMONTH = "GetAccountingMonth";
        public const string M_SP_GETPAYROLLCONFIG4RUN = "GetPayrollConfig4Run";
        public const string M_SP_GETPAYROLLCONFIG4TOTEARNING = "GetPayrollConfig4TotEarning";
    }
    /// <summary>
    /// To implement Common stored procedures
    /// </summary>
    internal static class CommonStoredProcedures
    {
        #region Enum Declarations

        /// <summary>
        /// CheckUser enum values defined for stored procedure.
        /// </summary>
        private enum EnumCheckUser : int
        {
            IsNotAValidUser = 2
        }

        #endregion
        #region Common Store Procedures
        /// <summary>
        /// To check login user is a valid user or not
        /// </summary>
        /// <param name="AccountName"></param>
        /// <returns></returns>
        internal static EmployeeComponant CheckUser(string AccountName)
        {
           EmployeeComponant User = new EmployeeComponant();
           //int UserID = 0;
            try
            {
                SqlParameter[] Params = new SqlParameter[5];

                Params[0] = new SqlParameter("@AccountName", SqlDbType.NVarChar,255);
                Params[0].Value = AccountName;
                Params[1] = new SqlParameter("@LoggedIn", SqlDbType.DateTime);
                Params[1].Value = DateTime.Now;
                Params[2] = new SqlParameter("@UID", SqlDbType.Int, 4, ParameterDirection.Output, false, ((Byte)(0)), ((Byte)(0)), "", DataRowVersion.Current, null);
                Params[3] = new SqlParameter("@Result", SqlDbType.Int, 4, ParameterDirection.Output, false, ((Byte)(0)), ((Byte)(0)), "", DataRowVersion.Current, null);


                Params[4] = new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, ((Byte)(0)), ((Byte)(0)), "", DataRowVersion.Current, null);
                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure,StringConstants.M_SP_ADDFIRSTEMPLOYEE, Params);

                if (Convert.ToInt16(Params[4].Value) != 0)
                {
                 throw new Exception("Unable to Check User details.");
                }
                else
                {
                    if (Convert.ToInt32(Params[3].Value) != (int)EnumCheckUser.IsNotAValidUser)
                    {
                        User.UID = Convert.ToInt32(Params[2].Value);
                        User.UserLogID = AddUserLog(User.UID);
                    }
                }
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => CheckUser : Failed to Check User" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Check User data.", TErrorMessageType.ERROR, e);
            }

            return User;
        }
        /// <summary>
        /// To create User Loggin detail.
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        internal static int AddUserLog(int UID)
        {
            int intResult = 0;
            try
            {
                SqlParameter[] Params = new SqlParameter[4];

                Params[0] = new SqlParameter("@UID", SqlDbType.Int);
                Params[0].Value = UID;
                Params[1] = new SqlParameter("@LoggedIn", SqlDbType.DateTime);
                Params[1].Value = DateTime.Now;
                
                Params[2] = new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, ((Byte)(0)), ((Byte)(0)), "", DataRowVersion.Current, null);
                Params[3] = new SqlParameter("@ID", SqlDbType.Int);
                Params[3].Direction = ParameterDirection.Output;

                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_ADDUSERSLOG, Params);

                if (Convert.ToInt16(Params[2].Value) != 0)
                {
                    throw new Exception("Unable to Add User Log details.");
                }
                else
                {
                    intResult = Convert.ToInt32(Params[3].Value);
                }
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => AddUserLog : Failed to Add User Log" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Add User Log data.", TErrorMessageType.ERROR, e);
            }

            return intResult;
        }
        /// <summary>
        /// To edit User Log details
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UID"></param>
        /// <returns></returns>
        internal static bool EditUserLog(int ID,int UID)
        {
            bool boolResult = false;
            try
            {
                SqlParameter[] Params = new SqlParameter[4];

                Params[0] = new SqlParameter("@ID", SqlDbType.Int);
                Params[0].Value = ID;
                Params[1] = new SqlParameter("@UID", SqlDbType.Int);
                Params[1].Value = UID;
                Params[2] = new SqlParameter("@LoggedOut", SqlDbType.DateTime);
                Params[2].Value = DateTime.Now;

                Params[3] = new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, ((Byte)(0)), ((Byte)(0)), "", DataRowVersion.Current, null);
                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_SETUSERSLOG, Params);

                if (Convert.ToInt16(Params[3].Value) != 0)
                {
                    throw new Exception("Unable to Edit User Log details.");
                }
                else
                {
                    boolResult = true;
                }
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => EditUserLog : Failed to Edit User Log" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Edit User Log  data.", TErrorMessageType.ERROR, e);
            }

            return boolResult;
        }

        /// <summary>
        /// To get all Role details.
        /// </summary>
        internal static DataTable GetRoles()
        {
            DataTable datatable = new DataTable();
            try
            {
                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure,StringConstants.M_SP_GETALLROLES, null);
                datatable = ds.Tables[0];
            }
            catch(Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetRoles : Failed to Get Roles" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Roles data.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }

        /// <summary>
        /// To get all Role details.
        /// </summary>
        internal static DataTable GetAllRoles()
        {
            DataTable datatable = new DataTable();
            try
            {
                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure,StringConstants.M_SP_GETALLROLES, null);
                datatable = ds.Tables[0];
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllRoles : Failed to Get All Roles" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Roles data.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }

        /// <summary>
        /// To get all Branches
        /// </summary>
        /// <returns></returns>
        internal static DataTable GetAllBranches()
        {
            DataTable datatable = new DataTable();
            try
            {
                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure,StringConstants.M_SP_GETALLBRANCH, null);
                datatable = ds.Tables[0];
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllBranches : Failed to Get Branches" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Branch data.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }

        /// <summary>
        /// To get all managers
        /// </summary>
        /// <returns></returns>
        internal static DataTable GetAllManagers()
        {
            DataTable datatable = new DataTable();
            try
            {
                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_GETALLMANAGER, null);
                datatable = ds.Tables[0];
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllManagers : Failed to Get Managers" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Manager data.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }

        /// <summary>
        /// To get all User details with respect to Role ID
        /// </summary>
        /// <returns></returns>
        internal static DataTable GetNamesbyRoleID(Roles role)
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@RoleID", SqlDbType.Int);
            Params[0].Value = role.RoleID;

            DataTable datatable = new DataTable();
            try
            {
                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure,StringConstants.M_SP_GETNAMESBYROLEID, Params);
                datatable = ds.Tables[0];
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetNamesbyRoleID : Failed to Get Names by role id" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Users data.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }

        /// <summary>
        /// To add/edit Branch details
        /// </summary>
        /// <param name="branch">branch values</param>
        /// <returns></returns>
        internal static bool SaveBranch(Branches branch)
        {
            bool boolResult = false;
            try
            {
                SqlParameter[] Params = new SqlParameter[9];

                Params[0] = new SqlParameter("@BranchID", SqlDbType.Int);
                Params[0].Value = branch.BranchID;
                Params[1] = new SqlParameter("@BranchName", SqlDbType.NVarChar);
                Params[1].Value = branch.BranchName;
                Params[2] = new SqlParameter("@BranchCode", SqlDbType.NVarChar);
                Params[2].Value = branch.BranchCode;
                Params[3] = new SqlParameter("@IsActive", SqlDbType.Bit);
                Params[3].Value = branch.IsActive;
                Params[4] = new SqlParameter("@AddedOn", SqlDbType.DateTime);
                Params[4].Value = DateTime.Now;
                Params[5] = new SqlParameter("@AddedBy", SqlDbType.Bit);
                Params[5].Value = branch.CreatedBy;
                Params[6] = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
                Params[6].Value = DateTime.Now;
                Params[7] = new SqlParameter("@ModifiedBy", SqlDbType.Bit);
                Params[7].Value = branch.ModifiedBy;
                Params[8] = new SqlParameter("@Result", SqlDbType.Int);
                Params[8].Value = string.Empty;
                Params[8].Direction = ParameterDirection.Output;

                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_SETBRANCH, Params);

                if (Convert.ToInt16(Params[8].Value) != 0)
                {
                    throw new Exception("Unable to Edit User Log details.");
                }
                else
                {
                    boolResult = true;
                }
                Utility.AuditLogHelper.InfoLogMessage("Location added/modified", branch.CreatedBy.ToString());
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => SaveBranch : Failed to Save Branch" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Save Branch data.", TErrorMessageType.ERROR, e);
            }

            return boolResult;
        }

        /// <summary>
        /// To get all Sale Type
        /// </summary>
        /// <returns></returns>
        internal static DataTable GetAllSaleType()
        {
            DataTable datatable = new DataTable();
            try
            {
                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_GETALLSALETYPE, null);
                datatable = ds.Tables[0];
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllSaleType : Failed to Get SaleType" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Saletype data.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }

        /// <summary>
        /// To add/update SaleType
        /// </summary>
        /// <param name="saleType">SaleType values</param>
        /// <returns></returns>
        internal static bool SaveSaleType(SaleType saleType)
        {
            bool boolResult = false;
            try
            {
                SqlParameter[] Params = new SqlParameter[12];

                Params[0] = new SqlParameter("@ID", SqlDbType.Int);
                Params[0].Value = saleType.ID;
                Params[1] = new SqlParameter("@SaleTypeName", SqlDbType.NVarChar);
                Params[1].Value = saleType.SaleTypeName;
                Params[2] = new SqlParameter("@IsNewCustomer", SqlDbType.Bit);
                Params[2].Value = saleType.IsNewCustomer;
                Params[3] = new SqlParameter("@IsExistingCustomer", SqlDbType.Bit);
                Params[3].Value = saleType.IsExistingCustomer;
                Params[4] = new SqlParameter("@IsBiMonthlyBonus", SqlDbType.Bit);
                Params[4].Value = saleType.IsBiMonthlyBonus;
                Params[5] = new SqlParameter("@IsTenureBonus", SqlDbType.Bit);
                Params[5].Value = saleType.IsTenureBonus;
                Params[6] = new SqlParameter("@IsActive", SqlDbType.Bit);
                Params[6].Value = saleType.IsActive;
                Params[7] = new SqlParameter("@AddedOn", SqlDbType.DateTime);
                Params[7].Value = DateTime.Now;
                Params[8] = new SqlParameter("@AddedBy", SqlDbType.Bit);
                Params[8].Value = saleType.CreatedBy;
                Params[9] = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
                Params[9].Value = DateTime.Now;
                Params[10] = new SqlParameter("@ModifiedBy", SqlDbType.Bit);
                Params[10].Value = saleType.ModifiedBy;
                Params[11] = new SqlParameter("@Result", SqlDbType.Int);
                Params[11].Value = string.Empty;
                Params[11].Direction = ParameterDirection.Output;

                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_SETSALETYPE, Params);

                if (Convert.ToInt16(Params[11].Value) != 0)
                {
                    throw new Exception("Unable to add/edit Saletype details.");
                }
                else
                {
                    boolResult = true;
                }
                Utility.AuditLogHelper.InfoLogMessage("Saletype added/modified", saleType.CreatedBy.ToString());
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => SaveSaleType : Failed to Save SaleType" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Save SaleType data.", TErrorMessageType.ERROR, e);
            }

            return boolResult;
        }
        internal static bool CheckBranch(int branchid)
        {
            DataTable datatable = new DataTable();
            //int UserID = 0;
            try
            {
                SqlParameter[] Params = new SqlParameter[1];

                Params[0] = new SqlParameter("@BranchID", SqlDbType.Int);
                Params[0].Value = branchid;

                DataSet ds =  DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_CHECKBRANCH, Params);
                datatable = ds.Tables[0];

                if ( datatable.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => CheckBranch : Failed to Check Branches" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Check Branch data.", TErrorMessageType.ERROR, e);
            }

            return false;
        }
        internal static bool CheckSaletype(CheckSaleType saleType)
        {
            DataTable datatable = new DataTable();
            //int UserID = 0;
            try
            {
                SqlParameter[] Params = new SqlParameter[1];

                Params[0] = new SqlParameter("@SaleType", SqlDbType.Int);
                Params[0].Value = saleType.ID;

                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_CHECKSALETYPE, Params);
                datatable = ds.Tables[0];

                if (saleType.reason != 0) {

                    datatable = datatable.AsEnumerable()
                                .Where(r => r.Field<int>("CustomerType") == saleType.reason)
                                .CopyToDataTable();
                }


                if (datatable.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => CheckSaletype : Failed to Check Saletype" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Check Saletype data.", TErrorMessageType.ERROR, e);
            }

            return false;
        }
        internal static DataTable GetAllPayrollConfigByYear(int Year)
        {
            DataTable datatable = new DataTable();
            SqlParameter[] Params = new SqlParameter[1];

            Params[0] = new SqlParameter("@Year", SqlDbType.Int, 255);
            Params[0].Value = Year;
            try
            {
                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_GETALLPAYROLLCONFIGBYYEAR, Params);
                datatable = ds.Tables[0];
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllPayrollConfig : Failed to Get All Payroll Config" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Payroll data.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }
        internal static DataTable GetAllPayrollConfig()
        {
            DataTable datatable = new DataTable();
            try
            {
                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_GETALLPAYROLLCONFIG, null);
                datatable = ds.Tables[0];
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllPayrollConfig : Failed to Get All Payroll Config" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Payroll data.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }

        /// <summary>
        /// To Add PayRoll
        /// </summary>
        /// <param name="payRoll">PayRoll values</param>
        /// <returns></returns>
        internal static bool InsertPayroll(PayrollConfig payRoll)
        {
            bool boolResult = false;
            try
            {
                SqlParameter[] Params = new SqlParameter[10];

                Params[0] = new SqlParameter("@ID", SqlDbType.Int);
                Params[0].Value = payRoll.ID;
                Params[1] = new SqlParameter("@Year", SqlDbType.Int);
                Params[1].Value = payRoll.Year;
                Params[2] = new SqlParameter("@Period", SqlDbType.Int);
                Params[2].Value = payRoll.Period;
                Params[3] = new SqlParameter("@Month", SqlDbType.NVarChar);
                Params[3].Value = payRoll.Month;
                Params[4] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                Params[4].Value = payRoll.DateFrom;
                Params[5] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                Params[5].Value = payRoll.DateTo;
                Params[6] = new SqlParameter("@IsActive", SqlDbType.Bit);
                Params[6].Value = true;
                Params[7] = new SqlParameter("@AddedOn", SqlDbType.DateTime);
                Params[7].Value = DateTime.Now;
                Params[8] = new SqlParameter("@AddedBy", SqlDbType.Int);
                Params[8].Value = payRoll.CreatedBy;
                Params[9] = new SqlParameter("@Result", SqlDbType.Int);
                Params[9].Value = string.Empty;
                Params[9].Direction = ParameterDirection.Output;

                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_ADDPAYROLL, Params);

                if (Convert.ToInt16(Params[9].Value) != 0)
                {
                    throw new Exception("Unable to Insert Payroll details.");
                }
                else
                {
                    boolResult = true;
                }
                Utility.AuditLogHelper.InfoLogMessage("Payroll Config Inserted", payRoll.CreatedBy.ToString());
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => InsertPayroll : Failed to insert Payroll" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't add Payroll data.", TErrorMessageType.ERROR, e);
            }

            return boolResult;
        }

        /// <summary>
        /// To update PayRoll
        /// </summary>
        /// <param name="payRoll">PayRoll values</param>
        /// <returns></returns>
        internal static bool UpdatePayroll(PayrollConfig payRoll)
        {
            bool boolResult = false;
            try
            {
                SqlParameter[] Params = new SqlParameter[10];

                Params[0] = new SqlParameter("@ID", SqlDbType.Int);
                Params[0].Value = payRoll.ID;
                Params[1] = new SqlParameter("@Year", SqlDbType.Int);
                Params[1].Value = payRoll.Year;
                Params[2] = new SqlParameter("@Period", SqlDbType.Int);
                Params[2].Value = payRoll.Period;
                Params[3] = new SqlParameter("@Month", SqlDbType.NVarChar);
                Params[3].Value = payRoll.Month;
                Params[4] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                Params[4].Value = payRoll.DateFrom;
                Params[5] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                Params[5].Value = payRoll.DateTo;
                Params[6] = new SqlParameter("@IsActive", SqlDbType.Bit);
                Params[6].Value = payRoll.IsActive;
                Params[7] = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
                Params[7].Value = DateTime.Now;
                Params[8] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                Params[8].Value = payRoll.ModifiedBy;
                Params[9] = new SqlParameter("@Result", SqlDbType.Int);
                Params[9].Value = string.Empty;
                Params[9].Direction = ParameterDirection.Output;

                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_SETPAYROLL, Params);

                if (Convert.ToInt16(Params[9].Value) != 0)
                {
                    throw new Exception("Unable to edit Payroll details.");
                }
                else
                {
                    boolResult = true;
                }
                Utility.AuditLogHelper.InfoLogMessage("Payroll modified", payRoll.CreatedBy.ToString());
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => UpdatePayroll : Failed to Update Payroll" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't update Payroll data.", TErrorMessageType.ERROR, e);
            }

            return boolResult;
        }

        internal static DataTable GetAccountingMonth(DateTime currentDate)
        {
            DataTable datatable = new DataTable();
            try
            {
                SqlParameter[] param = new SqlParameter[1];

                param[0] = new SqlParameter("@CurrentDate", SqlDbType.Date);
                param[0].Value = currentDate;

                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_GETALLACCOUNTINGMONTH, param);
                datatable = ds.Tables[0];
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAccountingMonth : Failed to Get Accounting Month" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get Accounting Month.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }

        internal static bool CheckPayrollConfigYear(int Year)
        {
            bool boolResult = false;
            DataTable datatable = new DataTable();
            SqlParameter[] Params = new SqlParameter[1];

            Params[0] = new SqlParameter("@Year", SqlDbType.Int, 255);
            Params[0].Value = Year;
            try
            {
                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_GETALLPAYROLLCONFIGBYYEAR, Params);
                datatable = ds.Tables[0];

                if (datatable.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllPayrollConfig : Failed to Get All Payroll Config" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Payroll data.", TErrorMessageType.ERROR, e);
            }

            return boolResult;
        }
        #endregion
    }
}
