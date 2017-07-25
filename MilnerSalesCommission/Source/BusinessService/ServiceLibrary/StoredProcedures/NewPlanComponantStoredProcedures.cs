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
using System.Threading;
using System.Threading.Tasks;
using Utility;
using System.Security.Principal;
using ViewModels;

namespace ServiceLibrary.StoredProcedures
{
    /// <summary>
    /// This class implemented New Plan component related stored procedures
    /// </summary>
    internal static class NewPlanComponantStoredProcedures
    {
        #region New Plan Commission Componants

        /// <summary>
        ///To get all Plans.
        /// </summary>
        internal static DataTable GetAllPlans()
        {
            DataTable datatable = new DataTable();
            try
            {
                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, StringConstants.M_SP_GETPLANCOMPONENTS, null);
                datatable = ds.Tables[0];
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllPlans : Failed to Get all Plans" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get all Plans data.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }

        /// <summary>
        /// To get Plan details by ID
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        internal static DataTable GetPlanbyID(PlanComponent plan)
        {
            DataTable datatable = new DataTable();
            try
            {
                SqlParameter[] Params = new SqlParameter[2];

                Params[0] = new SqlParameter("@PlanID", SqlDbType.Int);
                Params[0].Value = plan.PlanID;

                Params[1] = new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, ((Byte)(0)), ((Byte)(0)), "", DataRowVersion.Current, null);
                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure,StringConstants.M_SP_GETPLANCOMPONENTSBYID, Params);

                datatable = ds.Tables[0];
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetPlanbyID : Failed to Get Plan by ID" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Plan data by ID.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }
        /// <summary>
        /// To create Plan details
        /// </summary>
        /// <param name="plancomponent"></param>
        /// <param name="TGPCustomerInfolist"></param>
        /// <param name="TenureBonuslist"></param>
        /// <param name="BIMonthlyBonusInfolist"></param>
        /// <returns></returns>
        internal static int AddPlans(PlanComponent plancomponent, TGPCustomerInfoCollection TGPcollection, TenureBonusCollection Tenurecollection, BiMonthlyBonusCollection Bimonthlycollection)
        {
            int PlanID = 0;
            try
            {
                SqlParameter[] Params = new SqlParameter[13];

                Params[0] = new SqlParameter("@PlanName", SqlDbType.NVarChar,255);
                Params[0].Value = plancomponent.PlanName;
                Params[1] = new SqlParameter("@BasisType", SqlDbType.Int);
                Params[1].Value = plancomponent.BasisType;
                Params[2] = new SqlParameter("@BMQuotaBonus", SqlDbType.Bit);
                Params[2].Value = plancomponent.BMQuotaBonus;
                Params[3] = new SqlParameter("@SMEligible", SqlDbType.Bit);
                Params[3].Value = plancomponent.SMEligible;
                Params[4] = new SqlParameter("@TenureBonus", SqlDbType.Bit);
                Params[4].Value = plancomponent.TenureBonus;
                Params[5] = new SqlParameter("@CreatedOn", SqlDbType.DateTime);
                Params[5].Value = DateTime.Now;
                Params[6] = new SqlParameter("@CreatedBy", SqlDbType.Int);
                Params[6].Value = plancomponent.CreatedBy;
                Params[7] = new SqlParameter("@IsActive", SqlDbType.Bit);
                Params[7].Value = plancomponent.IsActive;
                Params[8] = new SqlParameter("@TGPCustomerInfo", SqlDbType.Structured);
                Params[8].Value = TGPcollection.Count > 0 ? TGPcollection : null;
                Params[9] = new SqlParameter("@Tenure", SqlDbType.Structured);
                Params[9].Value = Tenurecollection.Count > 0 ? Tenurecollection : null;
                Params[10] = new SqlParameter("@BIMonthlyBonusInfo", SqlDbType.Structured);
                Params[10].Value = Bimonthlycollection.Count > 0 ? Bimonthlycollection : null;
                Params[11] = new SqlParameter("@PlanID", SqlDbType.Int, 4, ParameterDirection.Output, false, ((Byte)(0)), ((Byte)(0)), "", DataRowVersion.Current, null);
                
                //@RETURN_VALUE Return Value Parameter
                Params[12] = new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, ((Byte)(0)), ((Byte)(0)), "", DataRowVersion.Current, null);
                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure,StringConstants.M_SP_ADDPLANCOMPONENTS, Params);
               
                //Get Return Parameter value
                if (Convert.ToInt16(Params[12].Value) != 0)
                {
                   throw new Exception("Unable to add Plan Components.");
                }
                else
                {
                    PlanID = Convert.ToInt32(Params[11].Value);
                }
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => Addplans : Failed to Add Plans" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Add Plans data.", TErrorMessageType.ERROR, e);
            }

            return PlanID;
        }
        /// <summary>
        /// To Edit Plan details
        /// </summary>
        /// <param name="plancomponent"></param>
        /// <param name="TGPCustomerInfolist"></param>
        /// <param name="TenureBonuslist"></param>
        /// <param name="BIMonthlyBonusInfolist"></param>
        /// <returns></returns>
        internal static bool EditPlans(PlanComponent plancomponent, TGPCustomerInfoCollection TGPcollection, TenureBonusCollection Tenurecollection, BiMonthlyBonusCollection Bimonthlycollection)
        {
            bool boolResult = true;
            try
            {
                SqlParameter[] Params = new SqlParameter[12];

                Params[0] = new SqlParameter("@PlanID", SqlDbType.Int);
                Params[0].Value = plancomponent.PlanID;
                Params[1] = new SqlParameter("@PlanName", SqlDbType.NVarChar, 255);
                Params[1].Value = plancomponent.PlanName;
                Params[2] = new SqlParameter("@BasisType", SqlDbType.Int);
                Params[2].Value = plancomponent.BasisType;
                Params[3] = new SqlParameter("@BMQuotaBonus", SqlDbType.Bit);
                Params[3].Value = plancomponent.BMQuotaBonus;
                Params[4] = new SqlParameter("@SMEligible", SqlDbType.Bit);
                Params[4].Value = plancomponent.SMEligible;
                Params[5] = new SqlParameter("@TenureBonus", SqlDbType.Bit);
                Params[5].Value = plancomponent.TenureBonus;
                Params[6] = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
                Params[6].Value = DateTime.Now;
                Params[7] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                Params[7].Value = plancomponent.ModifiedBy;
                Params[8] = new SqlParameter("@TGPCustomerInfo", SqlDbType.Structured);
                Params[8].Value = TGPcollection.Count > 0 ? TGPcollection : null;
                Params[9] = new SqlParameter("@Tenure", SqlDbType.Structured);
                Params[9].Value = Tenurecollection.Count > 0 ? Tenurecollection : null;
                Params[10] = new SqlParameter("@BIMonthlyBonusInfo", SqlDbType.Structured);
                Params[10].Value = Bimonthlycollection.Count > 0 ? Bimonthlycollection : null;


                //@RETURN_VALUE Return Value Parameter
                Params[11] = new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, ((Byte)(0)), ((Byte)(0)), "", DataRowVersion.Current, null);
                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure,StringConstants.M_SP_SETPLANCOMPONENTS, Params);

                //Get Return Parameter value
                if (Convert.ToInt16(Params[11].Value) != 0)
                {
                    boolResult = false;
                    throw new Exception("Unable to Edit Plan Components.");
                }
                
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => EditPlans : Failed to Edit Plans" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Edit Plans data.", TErrorMessageType.ERROR, e);
            }

            return boolResult;
        }
        /// <summary>
        /// To Delete Plan details
        /// </summary>
        /// <param name="Plan"></param>
        /// <returns></returns>
        internal static bool DeletePlan(PlanComponent Plan)
        {
            bool boolResult = true;
            try
            {
                SqlParameter[] Params = new SqlParameter[5];

                Params[0] = new SqlParameter("@PlanID", SqlDbType.Int);
                Params[0].Value = Plan.PlanID;
                Params[1] = new SqlParameter("@ModifiedOn", SqlDbType.DateTime);
                Params[1].Value = DateTime.Now;
                Params[2] = new SqlParameter("@ModifiedBy", SqlDbType.Int);
                Params[2].Value =Plan.ModifiedBy;
                Params[3] = new SqlParameter("@IsActive", SqlDbType.Bit);
                Params[3].Value = Plan.IsActive;

                Params[4] = new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, ((Byte)(0)), ((Byte)(0)), "", DataRowVersion.Current, null);
                DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure,StringConstants.M_SP_DELETEPLANCOMPONENTSBYID, Params);

                if (Convert.ToInt16(Params[4].Value) != 0)
                {
                    boolResult = false;
                    throw new Exception("Unable to Delete Plan Components.");
                }
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => DeletePlan : Failed to Delete Plans" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Delete Plans data.", TErrorMessageType.ERROR, e);
            }

            return boolResult;
        }
        /// <summary>
        /// To Get TGPCustomerInfo details by PlanID
        /// </summary>
        /// <param name="PlanID"></param>
        /// <returns></returns>
        internal static DataTable GetAllTGPCustomerInfo(PlanComponent plan)
        {
            DataTable datatable = new DataTable();
            try
            {
                SqlParameter[] Params = new SqlParameter[2];

                Params[0] = new SqlParameter("@PlanID", SqlDbType.Int);
                Params[0].Value =plan.PlanID;

                Params[1] = new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, ((Byte)(0)), ((Byte)(0)), "", DataRowVersion.Current, null);
                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure,StringConstants.M_SP_GETTGPCUSTOMERINFO, Params);
                datatable = ds.Tables[0];
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllTGPCustomerInfo : Failed to Get TGPCustomerInfo" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get TGPCustomerInfo data.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }
        /// <summary>
        /// To Get BIMonthly Bonus details by PlanID
        /// </summary>
        /// <param name="PlanID"></param>
        /// <returns></returns>

        internal static DataTable GetAllBiMonthlyBonusInfo(PlanComponent Plan)
        {
            DataTable datatable = new DataTable();
            try
            {
                SqlParameter[] Params = new SqlParameter[2];

                Params[0] = new SqlParameter("@PlanID", SqlDbType.Int);
                Params[0].Value =Plan.PlanID;

                Params[1] = new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, ((Byte)(0)), ((Byte)(0)), "", DataRowVersion.Current, null);
                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure,StringConstants.M_SP_GETBIMONTHLYBONUSINFO, Params);
                datatable = ds.Tables[0];
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllBiMonthlyBonusInfo : Failed to Get BiMonthlyBonusInfo" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get BiMonthlyBonusInfo data.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }
        /// <summary>
        /// To get Tenure Bonus details by PlanID
        /// </summary>
        /// <param name="PlanID"></param>
        /// <returns></returns>
        internal static DataTable GetAllTenureBonus(PlanComponent Plan)
        {
            DataTable datatable = new DataTable();
            try
            {
                SqlParameter[] Params = new SqlParameter[2];

                Params[0] = new SqlParameter("@PlanID", SqlDbType.Int);
                Params[0].Value = Plan.PlanID;

                Params[1] = new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, ((Byte)(0)), ((Byte)(0)), "", DataRowVersion.Current, null);
                DataSet ds = DbFactoryHelper.ExecuteDataset(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure,StringConstants.M_SP_GETTENUREBONUS, Params);
                datatable = ds.Tables[0];
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllTenureBonus : Failed to Get TenureBonus" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get TenureBonus data.", TErrorMessageType.ERROR, e);
            }

            return datatable;
        }
        #endregion
    }
}
