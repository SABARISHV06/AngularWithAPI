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

namespace ServiceLibrary.StoredProcedures
{
    /// <summary>
    /// This class is implemented for new commission slips related stroredprocedures.
    /// </summary>
    internal static class NewCommissionSlipStoredProcedures
    {
        #region Sales Commission Slip

        /// <summary>
        /// To get all commission lists based on RoleID and UID.
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        internal static DataTable GetAllCommissionsbyUID(EmployeeComponant emp)
        {
            DataTable datatable = new DataTable();
            try
            {
                SqlParameter[] Params = new SqlParameter[2];
                Params[0] = new SqlParameter("@UID", SqlDbType.Int);
                Params[0].Value =emp.UID;
                Params[1] = new SqlParameter("@RoleID", SqlDbType.Int);
                Params[1].Value =emp.RoleID;
                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure,StringConstants.M_SP_GETALLACTIVECOMMISSIONBYUID, Params);
                datatable = ds.Tables[0];
                datatable.Columns.Add("PlanID");
                datatable.Columns.Add("UID");
                if (datatable.Rows.Count > 0)
                {
                    if (ds.Tables.Count > 1)
                    {
                        if (emp.RoleID == 6)
                        {
                            datatable.Rows[0]["UID"] = ds.Tables[1].Rows.Count > 0 ? ds.Tables[1].Rows[0]["UID"] : string.Empty;
                        }
                        else
                        {
                            datatable.Rows[0]["PlanID"] = ds.Tables[1].Rows.Count > 0 ? ds.Tables[1].Rows[0]["PlanID"] : string.Empty;
                        }
                    }
                    if (ds.Tables.Count > 2)
                    {
                        datatable.Rows[0]["UID"] = ds.Tables[2].Rows.Count > 0 ? ds.Tables[2].Rows[0]["UID"] : string.Empty;
                    }
                }
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllCommissionsbyUID : Failed to get all Commission componants" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get all Commission data.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }

        /// <summary>
        /// To get commission list based on its ID
        /// </summary>
        /// <param name="CommissionID"></param>
        /// <returns></returns>
        internal static DataTable GetCommissionbyID(int CommissionID)
        {
            DataTable datatable = new DataTable();
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@ID", SqlDbType.Int);
            Params[0].Value = CommissionID;
            try
            {
                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure,StringConstants.M_SP_GETCOMMISSIONBYID, Params);
                datatable = ds.Tables[0];
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetCommissionbyID : Failed to get Commission componant by ID" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Commission data by ID.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }

        /// <summary>
        /// To create commission detail.
        /// </summary>
        /// <param name="comm"></param>
        /// <returns></returns>
        internal static int CreateCommission(CommissionComponent comm)
        {
            int intResult = -1;
            TipLeadSlipCollection Tipleadcollection = new TipLeadSlipCollection();
            if (comm.TipLeadSliplist != null && comm.TipLeadSliplist.Count > 0)
            {
                foreach (TipLeadSlip Tipleadslip in comm.TipLeadSliplist)
                {
                    Tipleadcollection.Add(Tipleadslip);
                }
            }
            SqlParameter[] Params = new SqlParameter[41];
            Params[0] = new SqlParameter("@SalespersonName", SqlDbType.NVarChar);
            Params[0].Value = comm.SalesPerson;
            Params[1] = new SqlParameter("@Dateofsale", SqlDbType.DateTime);
            Params[1].Value = comm.DateofSale;
            Params[2] = new SqlParameter("@EntryDate", SqlDbType.DateTime);
            Params[2].Value = comm.EntryDate;
            Params[3] = new SqlParameter("@InvoiceNumber", SqlDbType.NVarChar);
            Params[3].Value = comm.InvoiceNumber;
            Params[4] = new SqlParameter("@AccountingID", SqlDbType.Int);
            Params[4].Value = comm.AccountPeriodID;
            Params[5] = new SqlParameter("@Customertype", SqlDbType.Int);
            Params[5].Value = comm.CustomerType;
            Params[6] = new SqlParameter("@CustomerName", SqlDbType.NVarChar);
            Params[6].Value = comm.CustomerName;
            Params[7] = new SqlParameter("@CustomerNumber", SqlDbType.NVarChar);
            Params[7].Value = string.IsNullOrEmpty(comm.CustomerNumber) ? string.Empty : comm.CustomerNumber; //Convert.ToInt64(comm.CustomerNumber);
            Params[8] = new SqlParameter("@Solditem", SqlDbType.NVarChar);
            Params[8].Value = comm.CommentSold != null ? comm.CommentSold : string.Empty;
            Params[9] = new SqlParameter("@Splitsalepersonname", SqlDbType.NVarChar);
            Params[9].Value = comm.SplitSalePerson != null ? comm.SplitSalePerson : string.Empty;
            Params[10] = new SqlParameter("@SplitsalepersonnameID", SqlDbType.NVarChar);
            Params[10].Value = comm.SplitSalePersonID != null ? comm.SplitSalePersonID : string.Empty;
            Params[11] = new SqlParameter("@Amountofsale", SqlDbType.Decimal);
            Params[11].Value = comm.AmountofSale;
            Params[12] = new SqlParameter("@Costofgoods", SqlDbType.Decimal);
            Params[12].Value = comm.CostofGoods;
            Params[13] = new SqlParameter("@BranchID", SqlDbType.Int);
            Params[13].Value = comm.BranchID;
            Params[14] = new SqlParameter("@ProductLine", SqlDbType.Int);
            Params[14].Value = comm.ProductLine;
            Params[15] = new SqlParameter("@Saletype", SqlDbType.Int);
            Params[15].Value = comm.SaleType;
            Params[16] = new SqlParameter("@Dollarvalue", SqlDbType.Decimal);
            Params[16].Value = comm.DollarVolume;
            Params[17] = new SqlParameter("@Base", SqlDbType.Decimal);
            Params[17].Value = comm.BaseCommission;
            Params[18] = new SqlParameter("@Lease", SqlDbType.Decimal);
            Params[18].Value = comm.LeaseCommission;
            Params[19] = new SqlParameter("@Service", SqlDbType.Decimal);
            Params[19].Value = comm.ServiceCommission;
            Params[20] = new SqlParameter("@Travel", SqlDbType.Decimal);
            Params[20].Value = comm.TravelCommission;
            Params[21] = new SqlParameter("@Cash", SqlDbType.Decimal);
            Params[21].Value = comm.CashCommission;
            Params[22] = new SqlParameter("@Special", SqlDbType.Decimal);
            Params[22].Value = comm.SpecialCommission;
            Params[23] = new SqlParameter("@TradeIn", SqlDbType.Decimal);
            Params[23].Value = comm.TradeIn;
            Params[24] = new SqlParameter("@SlipType", SqlDbType.Int);
            Params[24].Value = comm.SlipType;
            Params[25] = new SqlParameter("@MainCommissionID", SqlDbType.Int);
            Params[25].Value = comm.MainCommissionID;
            Params[26] = new SqlParameter("@TipLeadID", SqlDbType.Int);
            Params[26].Value = comm.TipLeadID;
            Params[27] = new SqlParameter("@TipLeadEmpID", SqlDbType.NVarChar);
            Params[27].Value = comm.TipLeadEmpID != null ? comm.TipLeadEmpID : string.Empty;
            Params[28] = new SqlParameter("@TipLeadName", SqlDbType.NVarChar);
            Params[28].Value = comm.TipLeadName != null ? comm.TipLeadName : string.Empty;
            Params[29] = new SqlParameter("@TipLeadAmount", SqlDbType.Decimal);
            Params[29].Value = comm.TipLeadAmount;
            Params[30] = new SqlParameter("@PositiveAdjustments", SqlDbType.Decimal);
            Params[30].Value = comm.PositiveAdjustments;
            Params[31] = new SqlParameter("@NegativeAdjustments", SqlDbType.Decimal);
            Params[31].Value = comm.NegativeAdjustments;
            Params[32] = new SqlParameter("@CompanyContribution", SqlDbType.Decimal);
            Params[32].Value = comm.CompanyContribution;
            Params[33] = new SqlParameter("@TotalCEarned", SqlDbType.Decimal);
            Params[33].Value = comm.TotalCEarned;
            Params[34] = new SqlParameter("@Status", SqlDbType.Int);
            Params[34].Value = comm.Status;
            Params[35] = new SqlParameter("@IsActive", SqlDbType.Bit);
            Params[35].Value = comm.IsActive;
            Params[36] = new SqlParameter("@AddedOn", SqlDbType.DateTime);
            Params[36].Value = comm.CreatedOn; 
            Params[37] = new SqlParameter("@AddedBy", SqlDbType.Int);
            Params[37].Value = comm.CreatedBy;
            Params[38] = new SqlParameter("@Tipleadslip", SqlDbType.Structured);
            Params[38].Value = Tipleadcollection.Count>0?Tipleadcollection:null;
            Params[39] = new SqlParameter("@Result", SqlDbType.Int);
            Params[39].Value = string.Empty;
            Params[39].Direction = ParameterDirection.Output;
            Params[40] = new SqlParameter("@ID", SqlDbType.Int);
            Params[40].Value = 0;
            Params[40].Direction = ParameterDirection.Output;
            try
            {
                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure,StringConstants.M_SP_ADDCOMMISSION, Params);

                intResult = Convert.ToInt16(Params[39].Value.ToString());
                //Get Return Parameter value
                if (intResult != 0)
                {
                    throw new Exception("AddCommission stored procedure returned a failure.");
                }
                else
                {
                    //Its returning commission ID
                    intResult = Convert.ToInt16(Params[40].Value);
                    if (comm.Status == 1)
                    {
                        Utility.AuditLogHelper.InfoLogMessage("Commission Slip - Draft", comm.CreatedBy.ToString());
                    }
                    else if (comm.Status == 2)
                    {
                        Utility.AuditLogHelper.InfoLogMessage("Commission Slip Submitted", comm.CreatedBy.ToString());
                    }
                    else if (comm.Status == 7)
                    {
                        Utility.AuditLogHelper.InfoLogMessage("Commission Slip accepted", comm.CreatedBy.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => CreateCommission : Failed to Create Commission componants" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Insert Commission data.", TErrorMessageType.ERROR, e);
            }
            return intResult;
        }

        /// <summary>
        /// To update commission list based on ID.
        /// </summary>
        /// <param name="comm"></param>
        /// <returns></returns>
        internal static bool UpdateCommission(CommissionComponent comm)
        {
            bool boolResult = true;
            TipLeadSlipCollection Tipleadcollection = new TipLeadSlipCollection();
            if (comm.TipLeadSliplist != null && comm.TipLeadSliplist.Count > 0)
            {
                foreach (TipLeadSlip Tipleadslip in comm.TipLeadSliplist)
                {
                    Tipleadcollection.Add(Tipleadslip);
                }
            }

            SqlParameter[] Params = new SqlParameter[42];
            Params[0] = new SqlParameter("@ID", SqlDbType.Int);
            Params[0].Value = comm.ID;
            Params[1] = new SqlParameter("@SalespersonName", SqlDbType.NVarChar);
            Params[1].Value = comm.SalesPerson;
            Params[2] = new SqlParameter("@Dateofsale", SqlDbType.DateTime);
            Params[2].Value = comm.DateofSale;
            Params[3] = new SqlParameter("@EntryDate", SqlDbType.DateTime);
            Params[3].Value = comm.EntryDate;
            Params[4] = new SqlParameter("@AccountingID", SqlDbType.Int);
            Params[4].Value = comm.AccountPeriodID;
            Params[5] = new SqlParameter("@Customertype", SqlDbType.Int);
            Params[5].Value = comm.CustomerType;
            Params[6] = new SqlParameter("@CustomerName", SqlDbType.NVarChar);
            Params[6].Value = comm.CustomerName;
            Params[7] = new SqlParameter("@CustomerNumber", SqlDbType.NVarChar);
            Params[7].Value = string.IsNullOrEmpty(comm.CustomerNumber) ? "" : comm.CustomerNumber;
            Params[8] = new SqlParameter("@InvoiceNumber", SqlDbType.NVarChar);
            Params[8].Value = comm.InvoiceNumber;
            Params[9] = new SqlParameter("@BranchID", SqlDbType.Int);
            Params[9].Value = comm.BranchID;
            Params[10] = new SqlParameter("@ProductLine", SqlDbType.NVarChar);
            Params[10].Value = comm.ProductLine;
            Params[11] = new SqlParameter("@Saletype", SqlDbType.NVarChar);
            Params[11].Value = comm.SaleType;
            Params[12] = new SqlParameter("@Amountofsale", SqlDbType.Decimal);
            Params[12].Value = comm.AmountofSale;
            Params[13] = new SqlParameter("@Costofgoods", SqlDbType.Decimal);
            Params[13].Value = comm.CostofGoods;
            Params[14] = new SqlParameter("@Solditem", SqlDbType.NVarChar);
            Params[14].Value = comm.CommentSold != null ? comm.CommentSold : "";
            Params[15] = new SqlParameter("@Splitsalepersonname", SqlDbType.NVarChar);
            Params[15].Value = comm.SplitSalePerson != null ? comm.SplitSalePerson : "";
            Params[16] = new SqlParameter("@SplitsalepersonnameID", SqlDbType.NVarChar);
            Params[16].Value = comm.SplitSalePersonID != null ? comm.SplitSalePersonID : "";
            Params[17] = new SqlParameter("@SlipType", SqlDbType.Int);
            Params[17].Value = comm.SlipType;
            Params[18] = new SqlParameter("@MainCommissionID", SqlDbType.Int);
            Params[18].Value = comm.MainCommissionID;
            Params[19] = new SqlParameter("@TipLeadID", SqlDbType.Int);
            Params[19].Value = comm.TipLeadID;
            Params[20] = new SqlParameter("@TipLeadEmpID", SqlDbType.NVarChar);
            Params[20].Value = comm.TipLeadEmpID != null ? comm.TipLeadEmpID : string.Empty;
            Params[21] = new SqlParameter("@TipLeadName", SqlDbType.NVarChar);
            Params[21].Value = comm.TipLeadName != null ? comm.TipLeadName : string.Empty;
            Params[22] = new SqlParameter("@TipLeadAmount", SqlDbType.Decimal);
            Params[22].Value = comm.TipLeadAmount;
            Params[23] = new SqlParameter("@PositiveAdjustments", SqlDbType.Decimal);
            Params[23].Value = comm.PositiveAdjustments;
            Params[24] = new SqlParameter("@NegativeAdjustments", SqlDbType.Decimal);
            Params[24].Value = comm.NegativeAdjustments;
            Params[25] = new SqlParameter("@CompanyContribution", SqlDbType.Decimal);
            Params[25].Value = comm.CompanyContribution;
            Params[26] = new SqlParameter("@Dollarvalue", SqlDbType.Decimal);
            Params[26].Value = comm.DollarVolume;
            Params[27] = new SqlParameter("@Base", SqlDbType.Decimal);
            Params[27].Value = comm.BaseCommission;
            Params[28] = new SqlParameter("@Lease", SqlDbType.Decimal);
            Params[28].Value = comm.LeaseCommission;
            Params[29] = new SqlParameter("@Service", SqlDbType.Decimal);
            Params[29].Value = comm.ServiceCommission;
            Params[30] = new SqlParameter("@Travel", SqlDbType.Decimal);
            Params[30].Value = comm.TravelCommission;
            Params[31] = new SqlParameter("@Cash", SqlDbType.Decimal);
            Params[31].Value = comm.CashCommission;
            Params[32] = new SqlParameter("@Special", SqlDbType.Decimal);
            Params[32].Value = comm.SpecialCommission;
            Params[33] = new SqlParameter("@TotalCEarned", SqlDbType.Decimal);
            Params[33].Value = comm.TotalCEarned;
            Params[34] = new SqlParameter("@TradeIn", SqlDbType.Decimal);
            Params[34].Value = comm.TradeIn;
            Params[35] = new SqlParameter("@Tipleadslip", SqlDbType.Structured);
            Params[35].Value = Tipleadcollection.Count>0?Tipleadcollection:null;
            Params[36] = new SqlParameter("@Status", SqlDbType.Int);
            Params[36].Value = comm.Status;
            Params[37] = new SqlParameter("@IsActive", SqlDbType.Bit);
            Params[37].Value = true;
            Params[38] = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
            Params[38].Value = System.DateTime.Now;
            Params[39] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
            Params[39].Value = comm.ModifiedBy;
            Params[40] = new SqlParameter("@Comments", SqlDbType.Xml);
            Params[40].Value = comm.Comments;
            Params[41] = new SqlParameter("@Result", SqlDbType.Int);
            Params[41].Value = string.Empty;
            Params[41].Direction = ParameterDirection.Output;
            
            
            try
            {
                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure,StringConstants.M_SP_SETCOMMISSION, Params);
                
                //Get Return Parameter value
                if (Convert.ToInt32(Params[41].Value.ToString()) != 0)
                {
                    boolResult = false;
                    throw new Exception("SetCommission stored procedure returned a failure.");
                }

                Utility.AuditLogHelper.InfoLogMessage("Commission Slip Modified", comm.ModifiedBy.ToString());
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => UpdateCommission : Failed to Update commission componants" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Update commission data.", TErrorMessageType.ERROR, e);
            }
            return boolResult;
        }
        /// <summary>
        /// To update Tip lead slip details based on its ID
        /// </summary>
        /// <param name="comm"></param>
        /// <returns></returns>
        internal static bool UpdateTipLeadSlip(CommissionComponent comm)
        {
            bool boolResult = true;

            SqlParameter[] Params = new SqlParameter[41];
            Params[0] = new SqlParameter("@ID", SqlDbType.Int);
            Params[0].Value = comm.ID;
            Params[1] = new SqlParameter("@SalespersonName", SqlDbType.NVarChar);
            Params[1].Value = comm.SalesPerson;
            Params[2] = new SqlParameter("@Dateofsale", SqlDbType.DateTime);
            Params[2].Value = comm.DateofSale;
            Params[3] = new SqlParameter("@EntryDate", SqlDbType.DateTime);
            Params[3].Value = comm.EntryDate;
            Params[4] = new SqlParameter("@AccountingID", SqlDbType.Int);
            Params[4].Value = comm.AccountPeriodID;
            Params[5] = new SqlParameter("@Customertype", SqlDbType.Int);
            Params[5].Value = comm.CustomerType;
            Params[6] = new SqlParameter("@CustomerName", SqlDbType.NVarChar);
            Params[6].Value = comm.CustomerName;
            Params[7] = new SqlParameter("@CustomerNumber", SqlDbType.NVarChar);
            Params[7].Value = string.IsNullOrEmpty(comm.CustomerNumber) ? "" : comm.CustomerNumber;
            Params[8] = new SqlParameter("@InvoiceNumber", SqlDbType.NVarChar);
            Params[8].Value = comm.InvoiceNumber;
            Params[9] = new SqlParameter("@BranchID", SqlDbType.Int);
            Params[9].Value = comm.BranchID;
            Params[10] = new SqlParameter("@ProductLine", SqlDbType.NVarChar);
            Params[10].Value = comm.ProductLine;
            Params[11] = new SqlParameter("@Saletype", SqlDbType.NVarChar);
            Params[11].Value = comm.SaleType;
            Params[12] = new SqlParameter("@Amountofsale", SqlDbType.Decimal);
            Params[12].Value = comm.AmountofSale;
            Params[13] = new SqlParameter("@Costofgoods", SqlDbType.Decimal);
            Params[13].Value = comm.CostofGoods;
            Params[14] = new SqlParameter("@Solditem", SqlDbType.NVarChar);
            Params[14].Value = comm.CommentSold != null ? comm.CommentSold : "";
            Params[15] = new SqlParameter("@Splitsalepersonname", SqlDbType.NVarChar);
            Params[15].Value = comm.SplitSalePerson != null ? comm.SplitSalePerson : "";
            Params[16] = new SqlParameter("@SplitsalepersonnameID", SqlDbType.NVarChar);
            Params[16].Value = comm.SplitSalePersonID != null ? comm.SplitSalePersonID : "";
            Params[17] = new SqlParameter("@SlipType", SqlDbType.Int);
            Params[17].Value = comm.SlipType;
            Params[18] = new SqlParameter("@MainCommissionID", SqlDbType.Int);
            Params[18].Value = comm.MainCommissionID;
            Params[19] = new SqlParameter("@TipLeadID", SqlDbType.Int);
            Params[19].Value = comm.TipLeadID;
            Params[20] = new SqlParameter("@TipLeadEmpID", SqlDbType.NVarChar);
            Params[20].Value = comm.TipLeadEmpID != null ? comm.TipLeadEmpID : string.Empty;
            Params[21] = new SqlParameter("@TipLeadName", SqlDbType.NVarChar);
            Params[21].Value = comm.TipLeadName != null ? comm.TipLeadName : string.Empty;
            Params[22] = new SqlParameter("@TipLeadAmount", SqlDbType.Decimal);
            Params[22].Value = comm.TipLeadAmount;
            Params[23] = new SqlParameter("@PositiveAdjustments", SqlDbType.Decimal);
            Params[23].Value = comm.PositiveAdjustments;
            Params[24] = new SqlParameter("@NegativeAdjustments", SqlDbType.Decimal);
            Params[24].Value = comm.NegativeAdjustments;
            Params[25] = new SqlParameter("@CompanyContribution", SqlDbType.Decimal);
            Params[25].Value = comm.CompanyContribution;
            Params[26] = new SqlParameter("@Dollarvalue", SqlDbType.Decimal);
            Params[26].Value = comm.DollarVolume;
            Params[27] = new SqlParameter("@Base", SqlDbType.Decimal);
            Params[27].Value = comm.BaseCommission;
            Params[28] = new SqlParameter("@Lease", SqlDbType.Decimal);
            Params[28].Value = comm.LeaseCommission;
            Params[29] = new SqlParameter("@Service", SqlDbType.Decimal);
            Params[29].Value = comm.ServiceCommission;
            Params[30] = new SqlParameter("@Travel", SqlDbType.Decimal);
            Params[30].Value = comm.TravelCommission;
            Params[31] = new SqlParameter("@Cash", SqlDbType.Decimal);
            Params[31].Value = comm.CashCommission;
            Params[32] = new SqlParameter("@Special", SqlDbType.Decimal);
            Params[32].Value = comm.SpecialCommission;
            Params[33] = new SqlParameter("@TotalCEarned", SqlDbType.Decimal);
            Params[33].Value = comm.TotalCEarned;
            Params[34] = new SqlParameter("@TradeIn", SqlDbType.Decimal);
            Params[34].Value = comm.TradeIn;
            Params[35] = new SqlParameter("@Status", SqlDbType.Int);
            Params[35].Value = comm.Status;
            Params[36] = new SqlParameter("@IsActive", SqlDbType.Bit);
            Params[36].Value = true;
            Params[37] = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
            Params[37].Value = System.DateTime.Now;
            Params[38] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
            Params[38].Value = comm.ModifiedBy;
            Params[39] = new SqlParameter("@Comments", SqlDbType.Xml);
            Params[39].Value = comm.Comments;
            Params[40] = new SqlParameter("@Result", SqlDbType.Int);
            Params[40].Value = string.Empty;
            Params[40].Direction = ParameterDirection.Output;


            try
            {
                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_SETTIPLEADSLIP, Params);

                //Get Return Parameter value
                if (Convert.ToInt32(Params[40].Value.ToString()) != 0)
                {
                    boolResult = false;
                    throw new Exception("SetTipLeadslip stored procedure returned a failure.");
                }

                Utility.AuditLogHelper.InfoLogMessage("Commission Slip Modified", comm.ModifiedBy.ToString());
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => UpdateTipLeadSlip : Failed to Update commission componants" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Update commission data.", TErrorMessageType.ERROR, e);
            }
            return boolResult;
        }

        /// <summary>
        /// To delete commission detail based on its ID.
        /// </summary>
        /// <param name="comm"></param>
        /// <returns></returns>
        internal static bool DeleteCommission(CommissionComponent comm)
        {
            bool boolResult = true;
            SqlParameter[] Params = new SqlParameter[4];
            Params[0] = new SqlParameter("@ID", SqlDbType.Int);
            Params[0].Value = comm.ID;
            Params[1] = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
            Params[1].Value = System.DateTime.Now;
            Params[2] = new SqlParameter("@ModifiedBy", SqlDbType.Bit);
            Params[2].Value = comm.ModifiedBy;
            Params[3] = new SqlParameter("@Result", SqlDbType.Int);
            Params[3].Value = string.Empty;
            Params[3].Direction = ParameterDirection.Output;
            try
            {
                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure,StringConstants.M_SP_DELETECOMMISSIONBYID, Params);
                
                //Get Return Parameter value
                if (Convert.ToInt32(Params[3].Value.ToString()) != 0)
                {
                    boolResult = false;
                    throw new Exception("DeleteCommissionByID stored procedure returned a failure.");
                }
                Utility.AuditLogHelper.InfoLogMessage("Commission Slip Deleted", comm.ModifiedBy.ToString());
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => DeleteCommission : Failed to Delete commission componants" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Delete commission data.", TErrorMessageType.ERROR, e);
            }
            return boolResult;
        }
        /// <summary>
        /// To get Deactivated GM and Pay PlanID for Employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        internal static int[] GetDeactiveGMandPlanID(EmployeeComponant employee)
        {
            // DataTable datatable = new DataTable();
            int[] DeactivatedGMandPlanID = new int[2];
            try
            {
                SqlParameter[] Params = new SqlParameter[4];

                Params[0] = new SqlParameter("@RoleID", SqlDbType.Int);
                Params[0].Value = employee.RoleID;
                Params[1] = new SqlParameter("@UID", SqlDbType.Int);
                Params[1].Value = employee.UID;
                Params[2] = new SqlParameter("@DeactivatedPlanID", SqlDbType.Int);
                Params[2].Value = 0;
                Params[2].Direction = ParameterDirection.Output;
                Params[3] = new SqlParameter("@DeactivatedGM", SqlDbType.Int);
                Params[3].Value = 0;
                Params[3].Direction = ParameterDirection.Output;

                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_GETDEACTIVEGMANDPLANID, Params);
                DeactivatedGMandPlanID[0] = Convert.ToInt32(Params[2].Value);
                DeactivatedGMandPlanID[1] = Convert.ToInt32(Params[3].Value);

            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetDeactiveGMandPlanID : Failed to Get deactivated GM and Pay PlanID" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get deactivated GM and Pay PlanID", TErrorMessageType.ERROR, e);
            }

            return DeactivatedGMandPlanID;
        }

        #endregion
    }
}
