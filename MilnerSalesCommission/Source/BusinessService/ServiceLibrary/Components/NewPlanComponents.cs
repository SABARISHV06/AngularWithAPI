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
    /// This Class is implemented Pay Plan related components
    /// </summary>
    public partial class Servicelibrary : IDisposable
    {
        /// <summary>
        /// To Get All Plan details
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<PlanComponent> GetAllPlans(IPrincipal user)
        {
            DateTime start = DateTime.Now;

            string message = $"GetAllPlans\t{user.Identity.Name}";

            try
            {
                List<PlanComponent> PlanList = new List<PlanComponent>();

                using (DataTable dt = NewPlanComponantStoredProcedures.GetAllPlans())
                {
                foreach (DataRow dr in dt.Rows)
                {
                    PlanList.Add(new PlanComponent
                    {
                        PlanID = Convert.ToInt32(dr["PlanID"]),
                        PlanName = dr["PlanName"].ToString(),
                        BasisType =Convert.ToInt32(dr["BasisType"]),
                        BMQuotaBonus = Convert.ToBoolean(dr["BMQuotaBonus"]),
                        SMEligible = Convert.ToBoolean(dr["SMEligible"]),
                        TenureBonus = Convert.ToBoolean(dr["TenureBonus"]),
                        IsActive = Convert.ToBoolean(dr["IsActive"])
                    });
                }
                }
                return PlanList;
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllPlans : Failed to Get all Plan Componant" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get all Plan Componant. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To Get Plan detail by ID
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Plan"></param>
        /// <returns></returns>
        public PlanComponent GetPlanbyID(IPrincipal user, PlanComponent Plan)
        {
            DateTime start = DateTime.Now;

            string message = $"GetPlanbyID\t{user.Identity.Name}";

            try
            {
                PlanComponent Plandetail = new PlanComponent();

                using (DataTable dt = NewPlanComponantStoredProcedures.GetPlanbyID(Plan))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Plandetail.PlanID = Convert.ToInt32(dr["PlanID"]);
                        Plandetail.PlanName = dr["PlanName"].ToString();
                        Plandetail.BasisType = Convert.ToInt32(dr["BasisType"]);
                        Plandetail.BMQuotaBonus = Convert.ToBoolean(dr["BMQuotaBonus"]);
                        Plandetail.SMEligible = Convert.ToBoolean(dr["SMEligible"]);
                        Plandetail.TenureBonus = Convert.ToBoolean(dr["TenureBonus"]);
                        Plandetail.IsActive = Convert.ToBoolean(dr["IsActive"]);

                    }
                }
                using (DataTable dt = NewPlanComponantStoredProcedures.GetAllTGPCustomerInfo(Plan))
                {
                    Plandetail.TGPcustomerlist = new List<TGPCustomerInfo>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Plandetail.TGPcustomerlist.Add(new TGPCustomerInfo
                        {
                            ID = Convert.ToInt32(dr["ID"]),
                            PlanID = Convert.ToInt32(dr["PlanID"]),
                            SalesType = Convert.ToInt32(dr["SalesType"]),
                            SalesTypeName = dr["SalesTypeName"].ToString(),
                            Percentage = Convert.ToDecimal(dr["Percentage"]),
                            CustomerType = Convert.ToInt32(dr["CustomerType"])
                        });

                    }
                }
                using (DataTable dt = NewPlanComponantStoredProcedures.GetAllBiMonthlyBonusInfo(Plan))
                {
                    Plandetail.Bimonthlylist = new List<BIMonthlyBonusInfo>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Plandetail.Bimonthlylist.Add(new BIMonthlyBonusInfo
                        {
                            ID = Convert.ToInt32(dr["ID"]),
                            PlanID = Convert.ToInt32(dr["PlanID"]),
                            Months = dr["Months"].ToString(),
                            EntryPointA = Convert.ToInt32(dr["EntryPointA"]),
                            EntryPointB = Convert.ToInt32(dr["EntryPointB"]),
                            Percentage = Convert.ToDecimal(dr["Percentage"]),
                            Tier = dr["Tier"].ToString()
                        });

                    }
                }
                using (DataTable dt = NewPlanComponantStoredProcedures.GetAllTenureBonus(Plan))
                {
                    Plandetail.TenureBonuslist = new List<TenureBonus>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Plandetail.TenureBonuslist.Add(new TenureBonus
                        {
                            ID = Convert.ToInt32(dr["ID"]),
                            PlanID = Convert.ToInt32(dr["PlanID"]),
                            Months = dr["Months"].ToString(),
                            EntryPointA = Convert.ToInt32(dr["EntryPointA"]),
                            EntryPointB = Convert.ToInt32(dr["EntryPointB"]),
                            Percentage = Convert.ToDecimal(dr["Percentage"]),
                            Tier = dr["Tier"].ToString()
                        });

                    }
                }
                return Plandetail;
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetPlanbyID : Failed to Get Plan component by ID" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get Plan component by ID. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To Add Plan Details
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Plan"></param>
        /// <param name="TGPcollection"></param>
        /// <param name="Tenurecollection"></param>
        /// <param name="Bimonthlycollection"></param>
        /// <returns></returns>
        public int AddPlan(IPrincipal user, PlanComponent Plan, TGPCustomerInfoCollection TGPcollection, TenureBonusCollection Tenurecollection, BiMonthlyBonusCollection Bimonthlycollection)
        {
            DateTime start = DateTime.Now;

            string message = $"AddPlan\t{user.Identity.Name}";
            try
            {
               return NewPlanComponantStoredProcedures.AddPlans(Plan, TGPcollection, Tenurecollection, Bimonthlycollection);
                
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => AddPlan : Failed to Add Plan Componant" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Add Plan Componant. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To Edit Plan details
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Plan"></param>
        /// <param name="TGPcollection"></param>
        /// <param name="Tenurecollection"></param>
        /// <param name="Bimonthlycollection"></param>
        /// <returns></returns>
        public bool EditPlan(IPrincipal user, PlanComponent Plan, TGPCustomerInfoCollection TGPcollection, TenureBonusCollection Tenurecollection, BiMonthlyBonusCollection Bimonthlycollection)
        {
            DateTime start = DateTime.Now;

            string message = $"EditPlan\t{user.Identity.Name}";
          
            try
            {
                return NewPlanComponantStoredProcedures.EditPlans(Plan, TGPcollection, Tenurecollection, Bimonthlycollection);
               
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => EditPlant : Failed to Edit Plan Componant" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Edit Plan Componant. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To delete Plan details
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Plan"></param>
        /// <returns></returns>
        public bool DeletePlan(IPrincipal user, PlanComponent Plan)
        {
            DateTime start = DateTime.Now;

            string message = $"DeletePlan\t{user.Identity.Name}";
            try
            {
                return NewPlanComponantStoredProcedures.DeletePlan(Plan);
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => DeletePlan : Failed to Delete Plan details" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Delete Plan details. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To Get TGP Customer Info details by PlanID
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Plan"></param>
        /// <returns></returns>
        public List<TGPCustomerInfo> GetAllTGP(IPrincipal user,PlanComponent Plan)
        {
            DateTime start = DateTime.Now;

            string message = $"GetAllTGP\t{user.Identity.Name}";

            try
            {
                List<TGPCustomerInfo> TGPlist = new List<TGPCustomerInfo>();

                using (DataTable dt = NewPlanComponantStoredProcedures.GetAllTGPCustomerInfo(Plan))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TGPlist.Add(new TGPCustomerInfo
                        {
                            ID = Convert.ToInt32(dr["ID"]),
                            PlanID = Convert.ToInt32(dr["PlanID"]),
                            SalesType = Convert.ToInt32(dr["SalesType"]),
                            Percentage = Convert.ToDecimal(dr["Percentage"]),
                            CustomerType = Convert.ToInt32(dr["CustomerType"])
                        });

                    }
                }
                return TGPlist;
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllTGP : Failed to Get all TGP Componant" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get all TGP Componant. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To Get BiMonthly Bonus Info by PlanID
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Plan"></param>
        /// <returns></returns>
        public List<BIMonthlyBonusInfo> GetAllBimonthlyBonus(IPrincipal user, PlanComponent Plan)
        {
            DateTime start = DateTime.Now;

            string message = $"GetAllBimonthlyBonus\t{user.Identity.Name}";

            try
            {
                List<BIMonthlyBonusInfo> Bimonthlylist = new List<BIMonthlyBonusInfo>();

                using (DataTable dt = NewPlanComponantStoredProcedures.GetAllBiMonthlyBonusInfo(Plan))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Bimonthlylist.Add(new BIMonthlyBonusInfo
                        {
                            ID = Convert.ToInt32(dr["ID"]),
                            PlanID = Convert.ToInt32(dr["PlanID"]),
                            Months = dr["Months"].ToString(),
                            EntryPointA = Convert.ToInt32(dr["EntryPointA"]),
                            EntryPointB = Convert.ToInt32(dr["EntryPointB"]),
                            Percentage = Convert.ToDecimal(dr["Percentage"]),
                            Tier = dr["Tier"].ToString()
                        });

                    }
                }
                return Bimonthlylist;
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllBimonthlyBonus : Failed to Get all Bimonthly Bonus Info" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get all Bimonthly Bonus Info. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To Get Tenure Bonus details by PlanID
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Plan"></param>
        /// <returns></returns>
        public List<TenureBonus> GetAllTenureBonus(IPrincipal user, PlanComponent Plan)
        {
            DateTime start = DateTime.Now;

            string message = $"GetAllTenureBonus\t{user.Identity.Name}";

            try
            {
                List<TenureBonus> Tenurelist = new List<TenureBonus>();

                using (DataTable dt = NewPlanComponantStoredProcedures.GetAllTenureBonus(Plan))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Tenurelist.Add(new TenureBonus
                        {
                            ID = Convert.ToInt32(dr["ID"]),
                            PlanID = Convert.ToInt32(dr["PlanID"]),
                            Months = dr["Months"].ToString(),
                            EntryPointA = Convert.ToInt32(dr["EntryPointA"]),
                            EntryPointB = Convert.ToInt32(dr["EntryPointB"]),
                            Percentage = Convert.ToDecimal(dr["Percentage"]),
                            Tier = dr["Tier"].ToString()
                        });

                    }
                }
                return Tenurelist;
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllTenureBonus : Failed to Get all Tenure Bonus" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get all Tenure Bonus. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }
    }
}