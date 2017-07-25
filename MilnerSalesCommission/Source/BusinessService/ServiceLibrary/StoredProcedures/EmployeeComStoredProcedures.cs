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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using Utility;
using ViewModels;
using System.Data.Common;

namespace ServiceLibrary.StoredProcedures
{
    /// <summary>
    /// This class is implemented for new employees related stroredprocedures.
    /// </summary>
    internal static class EmployeeComStoredProcedures
    {
        #region Enum Declarations

        /// <summary>
        /// CreateEmployee enum values defined for stored procedure.
        /// </summary>
        private enum EnumCreateEmployee : int
        {
            IsDuplicateEmployeeID=3,
            IsDuplicateUser = 2,
            IsError = -1
        }
        #endregion

        #region Employee Componants

        /// <summary>
        /// To create employee and returns employee ID.
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        internal static int CreateEmployee(EmployeeComponant emp)
        {
            int intResult = -1;
            SqlParameter[] Params = new SqlParameter[26];
            Params[0] = new SqlParameter("@FName", SqlDbType.NVarChar);
            Params[0].Value = emp.FirstName;
            Params[1] = new SqlParameter("@LName", SqlDbType.NVarChar);
            Params[1].Value = emp.LastName;
            Params[2] = new SqlParameter("@EmployeeID", SqlDbType.NVarChar);
            Params[2].Value = emp.EmployeeID;
            Params[3] = new SqlParameter("@RoleID", SqlDbType.Int);
            Params[3].Value =  emp.RoleID;
            Params[4] = new SqlParameter("@Email", SqlDbType.NVarChar);
            Params[4].Value = emp.Email;
            Params[5] = new SqlParameter("@DateofHire", SqlDbType.DateTime);
            Params[5].Value = emp.DateofHire;
            Params[6] = new SqlParameter("@DateInPosition", SqlDbType.DateTime);
            Params[6].Value = emp.DateInPosition;
            Params[7] = new SqlParameter("@PrimaryBranch", SqlDbType.NVarChar);
            Params[7].Value = String.IsNullOrEmpty(emp.PrimaryBranch) ? "" : emp.PrimaryBranch;
            Params[8] = new SqlParameter("@SecondaryBranch", SqlDbType.NVarChar);
            Params[8].Value = String.IsNullOrEmpty(emp.SecondaryBranchName) ? "" : emp.SecondaryBranchName;  
            Params[9] = new SqlParameter("@ReportMgr", SqlDbType.Int);
            Params[9].Value =  emp.ReportMgr;
            Params[10] = new SqlParameter("@ApproveMgr", SqlDbType.Int);
            Params[10].Value =  emp.ApproveMgr;
            Params[11] = new SqlParameter("@PlanID", SqlDbType.Int);
            Params[11].Value = String.IsNullOrEmpty(emp.PayPlanID) ? "0" : emp.PayPlanID;
            Params[12] = new SqlParameter("@BPSalary", SqlDbType.Bit);
            Params[12].Value =  emp.BPSalary;
            Params[13] = new SqlParameter("@BPDraw", SqlDbType.Bit);
            Params[13].Value =  emp.BPDraw;
            Params[14] = new SqlParameter("@MonthAmount", SqlDbType.Decimal);
            Params[14].Value =  emp.MonthAmount;
            Params[15] = new SqlParameter("@DrawType", SqlDbType.Int);
            Params[15].Value =  emp.TypeofDraw;
            Params[16] = new SqlParameter("@DRPercentage", SqlDbType.Decimal);
            Params[16].Value =  emp.DRPercentage;
            Params[17] = new SqlParameter("@DDPeriod", SqlDbType.DateTime);
            Params[17].Value = emp.DDPeriod;
            Params[18] = new SqlParameter("@DrawTerm", SqlDbType.Int);
            Params[18].Value =  emp.DrawTerm;
            Params[19] = new SqlParameter("@DDAmount", SqlDbType.Decimal);
            Params[19].Value = emp.DDAmount;
            Params[20] = new SqlParameter("@IsActive", SqlDbType.Bit);
            Params[20].Value =  emp.IsActive;
            Params[21] = new SqlParameter("@AddedOn", SqlDbType.DateTime);
            Params[21].Value = System.DateTime.Now;
            Params[22] = new SqlParameter("@AddedBy", SqlDbType.Int);
            Params[22].Value = emp.CreatedBy;
            Params[23] = new SqlParameter("@AccountName", SqlDbType.NVarChar);
            Params[23].Value = emp.AccountName;
            Params[24] = new SqlParameter("@Result", SqlDbType.Int);
            Params[24].Value = string.Empty;
            Params[24].Direction = ParameterDirection.Output;
            Params[25] = new SqlParameter("@UID", SqlDbType.Int);
            Params[25].Direction = ParameterDirection.Output;
            try
            {
                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure,StringConstants.M_SP_ADDEMPLOYEE, Params);

                intResult = Convert.ToInt16(Params[24].Value.ToString());
                //Get Return Parameter value
                if (intResult == (int)EnumCreateEmployee.IsError)
                {
                    throw new Exception("AddEmployee stored procedure returned a failure.");
                }
                else if (intResult == (int)EnumCreateEmployee.IsDuplicateUser)
                {
                    intResult = Convert.ToInt16(Params[24].Value);
                }
                else if (intResult == (int)EnumCreateEmployee.IsDuplicateEmployeeID)
                {
                    intResult = Convert.ToInt16(Params[24].Value);
                }
                else
                {
                    //Its returning employee ID
                    intResult = Convert.ToInt16(Params[25].Value);

                    Utility.AuditLogHelper.InfoLogMessage("Employee Created", emp.CreatedBy.ToString());
                }
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => CreateEmployee : Failed to Create Employee componants" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Insert Employee data.", TErrorMessageType.ERROR, e);
            }
            return intResult;
        }

        /// <summary>
        /// To update employee based on UID.
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        internal static bool UpdateEmployee(EmployeeComponant emp)
        {
            bool boolResult = true;
            SqlParameter[] Params = new SqlParameter[25];
            Params[0] = new SqlParameter("@EmployeeID", SqlDbType.NVarChar);
            Params[0].Value = emp.EmployeeID;
            Params[1] = new SqlParameter("@FName", SqlDbType.NVarChar);
            Params[1].Value = emp.FirstName;
            Params[2] = new SqlParameter("@LName", SqlDbType.NVarChar);
            Params[2].Value = emp.LastName;
            Params[3] = new SqlParameter("@AccountName", SqlDbType.NVarChar);
            Params[3].Value = emp.AccountName;
            Params[4] = new SqlParameter("@RoleID", SqlDbType.Int);
            Params[4].Value = emp.RoleID;
            Params[5] = new SqlParameter("@Email", SqlDbType.NVarChar);
            Params[5].Value = emp.Email;
            Params[6] = new SqlParameter("@DateofHire", SqlDbType.DateTime);
            Params[6].Value = emp.DateofHire;
            Params[7] = new SqlParameter("@DateInPosition", SqlDbType.DateTime);
            Params[7].Value = emp.DateInPosition;
            Params[8] = new SqlParameter("@PrimaryBranch", SqlDbType.NVarChar);
            Params[8].Value = String.IsNullOrEmpty(emp.PrimaryBranch) ? "" : emp.PrimaryBranch;
            Params[9] = new SqlParameter("@SecondaryBranch", SqlDbType.NVarChar);
            Params[9].Value = String.IsNullOrEmpty(emp.SecondaryBranchName) ? "" : emp.SecondaryBranchName; //emp.SecondaryBranch.Length ==0 ? "" : emp.SecondaryBranch.ToString();
            Params[10] = new SqlParameter("@ReportMgr", SqlDbType.Int);
            Params[10].Value = emp.ReportMgr;
            Params[11] = new SqlParameter("@ApproveMgr", SqlDbType.Int);
            Params[11].Value = emp.ApproveMgr;
            Params[12] = new SqlParameter("@PlanID", SqlDbType.Int);
            Params[12].Value = String.IsNullOrEmpty(emp.PayPlanID) ? "0" : emp.PayPlanID;
            Params[13] = new SqlParameter("@BPSalary", SqlDbType.Bit);
            Params[13].Value = emp.BPSalary;
            Params[14] = new SqlParameter("@BPDraw", SqlDbType.Bit);
            Params[14].Value = emp.BPDraw;
            Params[15] = new SqlParameter("@MonthAmount", SqlDbType.Decimal);
            Params[15].Value = emp.MonthAmount;
            Params[16] = new SqlParameter("@DrawType", SqlDbType.Int);
            Params[16].Value = emp.TypeofDraw;
            Params[17] = new SqlParameter("@DRPercentage", SqlDbType.Decimal);
            Params[17].Value = emp.DRPercentage;
            Params[18] = new SqlParameter("@DDPeriod", SqlDbType.DateTime);
            Params[18].Value = emp.DDPeriod;
            Params[19] = new SqlParameter("@DrawTerm", SqlDbType.Int);
            Params[19].Value = emp.DrawTerm;
            Params[20] = new SqlParameter("@DDAmount", SqlDbType.Decimal);
            Params[20].Value = emp.DDAmount;
            Params[21] = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
            Params[21].Value = System.DateTime.Now;
            Params[22] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
            Params[22].Value = emp.ModifiedBy;
            Params[23] = new SqlParameter("@UID", SqlDbType.NVarChar);
            Params[23].Value = emp.UID;
            Params[24] = new SqlParameter("@Result", SqlDbType.Int);
            Params[24].Value = string.Empty;
            Params[24].Direction = ParameterDirection.Output;
            try
            {
                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure,StringConstants.M_SP_SETEMPLOYEE, Params);
                
                //Get Return Parameter value
                if (Convert.ToInt16(Params[24].Value.ToString()) != 0)
                {
                    boolResult = false;
                    throw new Exception("SetEmployee stored procedure returned a failure.");
                }
                Utility.AuditLogHelper.InfoLogMessage("Employee Modified", emp.ModifiedBy.ToString());
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => UpdateEmployee : Failed to Update Employee componants" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Update Employee data.", TErrorMessageType.ERROR, e);
            }
            return boolResult;
        }


        /// <summary>
        /// To get all employee details by RoleID
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        internal static DataTable GetAllEmployeesbyRoleID(int RoleID)
        {
            DataTable datatable = new DataTable();
            try
            {
                SqlParameter[] Params = new SqlParameter[1];
                Params[0] = new SqlParameter("@RoleID", SqlDbType.Int);
                Params[0].Value = RoleID;
                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_GETALLEMPLOYEEDETAILSBYROLEID, Params);
                datatable = ds.Tables[0];
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllEmployeesbyRoleID : Failed to get all Employee componants" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get all Employee data.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }

        /// <summary>
        /// To get an employee details based on UID.
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        internal static DataTable GetEmployeebyID(EmployeeComponant emp)
        {
            DataTable datatable = new DataTable();
            try
            {
                SqlParameter[] Params = new SqlParameter[1];
                Params[0] = new SqlParameter("@ID", SqlDbType.Int);
                Params[0].Value = emp.UID;
                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure,StringConstants.M_SP_GETEMPLOYEEBYID, Params);
                datatable = ds.Tables[0];
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetEmployeebyID : Failed to get Employee componant by ID" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Employee data by ID.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }

        /// <summary>
        /// To activate/deactive employee details based on UID.
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        internal static bool ActiveDeActiveEmployee(EmployeeComponant emp)
        {
            bool boolResult = true;
            SqlParameter[] Params = new SqlParameter[5];
            Params[0] = new SqlParameter("@ID", SqlDbType.NVarChar);
            Params[0].Value = emp.UID;
            Params[1] = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
            Params[1].Value = System.DateTime.Now;
            Params[2] = new SqlParameter("@ModifiedBy", SqlDbType.Bit);
            Params[2].Value = emp.ModifiedBy;
            Params[3] = new SqlParameter("@IsActive", SqlDbType.Int);
            Params[3].Value = emp.IsActive;
            Params[4] = new SqlParameter("@Result", SqlDbType.Int);
            Params[4].Value = string.Empty;
            Params[4].Direction = ParameterDirection.Output;
            try
            {
                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure,StringConstants.M_SP_ACTIVEDEACTIVEEMPLOYEE, Params);
                
                //Get Return Parameter value
                if (Convert.ToInt16(Params[4].Value.ToString()) != 0)
                {
                    boolResult = false;
                    throw new Exception("ActiveDeActiveEmployee stored procedure returned a failure.");
                }

                if (emp.IsActive == false)
                {
                    Utility.AuditLogHelper.InfoLogMessage("Employee Deactivated", emp.ModifiedBy.ToString());
                }
                else if (emp.IsActive == true)
                {
                    Utility.AuditLogHelper.InfoLogMessage("Employee Activated", emp.ModifiedBy.ToString());
                }

            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => ActiveDeactiveEmployee : Failed to Active/Deactive Employee componants" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Activate/Deactivate Employee data.", TErrorMessageType.ERROR, e);
            }
            return boolResult;
        }

        /// <summary>
        /// To get deactivate plan linked employee list.
        /// </summary>
        /// <returns></returns>
        internal static DataTable GetDeActivatePlanEmployees()
        {
            DataTable datatable = new DataTable();
            try
            {
                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure,StringConstants.M_SP_GETEMPLOYEEDEACTPLAN, null);
                datatable = ds.Tables[0];
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetDeActivatePlanEmployees : Failed to get Employee componants with dectivated Plans" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Employee data with deactivated Plans.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }

        /// <summary>
        /// To Get all Mail Ids to send Mail Notification about Commission creation
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        internal static string GetCommissionCreatedMailIDs(CommissionComponent email)
        {
            string MailIDs = string.Empty;
            try
            {
                SqlParameter[] Params = new SqlParameter[3];
                Params[0] = new SqlParameter("@UID", SqlDbType.Int);
                Params[0].Value = email.ID;
                Params[1] = new SqlParameter("@Status", SqlDbType.Int);
                Params[1].Value = email.Status;
                Params[2] = new SqlParameter("@Result", SqlDbType.NVarChar,4000);
                Params[2].Value = string.Empty;
                Params[2].Direction = ParameterDirection.Output;
                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_COMMISSIONCREATEDNOTIFICATION, Params);
                MailIDs = Params[2].Value.ToString();
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetCommissionCreatedMailIDs : Failed to get Mail Ids" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get all Mail Ids.", TErrorMessageType.ERROR, e);
            }

            return MailIDs;
        }
        #endregion
    }
}
