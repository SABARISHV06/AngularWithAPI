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
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Utility;
using ViewModels;
using ServiceLibrary.StoredProcedures;

namespace ServiceLibrary
{
    /// <summary>
    /// This class is implemented for new commission related components.
    /// </summary>
    public partial class Servicelibrary : IDisposable
    {

        #region Commission component
        /// <summary>
        /// To get commission list based on role and UID
        /// </summary>
        /// <param name="user">User values</param>
        /// <param name="emp">Employee details</param>
        /// <returns>Commission details in list</returns>
       public List<CommissionComponent> GetAllCommissionsbyUID(IPrincipal user,EmployeeComponant emp)
        {
            DateTime start = DateTime.Now;

            string message = $"GetAllCommissionsbyUID\t{user.Identity.Name}";

            try
            {
                List<CommissionComponent> commissionList = new List<CommissionComponent>();
                
                using (DataTable dt = NewCommissionSlipStoredProcedures.GetAllCommissionsbyUID(emp))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        commissionList.Add(new CommissionComponent
                        {
                            ID =Convert.ToInt32(dr["ID"]),
                            DateofSale = Convert.ToDateTime(dr["DateofSale"]),
                            CustomerName = dr["CustomerName"].ToString(),
                            TotalCEarned = Convert.ToDecimal(dr["TotalCEarned"]),
                            Status = Convert.ToInt32(dr["Status"]),
                            ProcesByPayroll=Convert.ToBoolean(dr["ProcesByPayroll"]),
                            SlipType = Convert.ToInt32(dr["SlipType"]),
                            IsPlanMapped = String.IsNullOrEmpty(dr["PlanID"].ToString())? true : false,
                            IsGMActive = String.IsNullOrEmpty(dr["UID"].ToString()) ? true : false
                        });
                    }

                    return commissionList;
                }
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllCommissionsbyUID : Failed to Get Commission Componant" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get Commission Componant. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To get commission details based on ID
        /// </summary>
        /// <param name="user">User values</param>
        /// <param name="CommissionID">Commission ID</param>
        /// <returns>Commission details</returns>
        public List<CommissionComponent> GetCommissionbyID(IPrincipal user, int CommissionID)
        {
            DateTime start = DateTime.Now;

            string message = $"GetCommissionbyID\t{user.Identity.Name}";

            try
            {
                List<CommissionComponent> commissionlist = new List<CommissionComponent>();

                using (DataTable dt = NewCommissionSlipStoredProcedures.GetCommissionbyID(CommissionID))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        CommissionComponent commission = new CommissionComponent();
                        commission.ID = Convert.ToInt32(dr["ID"]);
                        commission.SalesPerson = dr["SalesPerson"].ToString();
                        commission.DateofSale = Convert.ToDateTime(dr["DateofSale"]);
                        commission.EntryDate = Convert.ToDateTime(dr["EntryDate"]);
                        commission.AccountPeriod = dr["AccountPeriod"].ToString();
                        commission.AccountPeriodID = Convert.ToInt32(dr["AccountPeriodID"]);
                        commission.CustomerType = Convert.ToInt32(dr["CustomerType"]);
                        commission.CustomerName = dr["CustomerName"].ToString();
                        commission.CustomerNumber = dr["CustomerNumber"].ToString();
                        commission.InvoiceNumber = dr["InvoiceNumber"].ToString();
                        commission.ProductLine =Convert.ToInt32(dr["ProductLine"]);
                        commission.SaleType =Convert.ToInt32(dr["SaleType"]);
                        commission.AmountofSale = Convert.ToDecimal(dr["AmountofSale"]);
                        commission.CostofGoods = Convert.ToDecimal(dr["CostofGoods"]);
                        commission.CommentSold = dr["CommentSold"].ToString();
                        commission.SplitSalePerson = dr["SplitSalePerson"].ToString();
                        commission.SplitSalePersonID = dr["SplitSalePersonID"].ToString();
                        commission.TipLeadID = Convert.ToInt32(dr["TipLeadID"]);
                        commission.TipLeadName = dr["TipLeadName"].ToString();
                        commission.TipLeadEmpID = dr["TipLeadEmpID"].ToString();
                        commission.TipLeadAmount = Convert.ToDecimal(dr["TipLeadAmount"]);
                        commission.PositiveAdjustments = Convert.ToDecimal(dr["PositiveAdjustments"]);
                        commission.NegativeAdjustments = Convert.ToDecimal(dr["NegativeAdjustments"]);
                        commission.CompanyContribution = Convert.ToDecimal(dr["CompanyContribution"]);
                        commission.DollarVolume = Convert.ToDecimal(dr["DollarVolume"]);
                        commission.BaseCommission = Convert.ToDecimal(dr["BaseCommission"]);
                        commission.LeaseCommission = Convert.ToDecimal(dr["LeaseCommission"]);
                        commission.ServiceCommission = Convert.ToDecimal(dr["ServiceCommission"]);
                        commission.TravelCommission = Convert.ToDecimal(dr["TravelCommission"]);
                        commission.CashCommission = Convert.ToDecimal(dr["CashCommission"]);
                        commission.SpecialCommission = Convert.ToDecimal(dr["SpecialCommission"]);
                        commission.TotalCEarned = Convert.ToDecimal(dr["TotalCEarned"]);
                        commission.TradeIn = Convert.ToDecimal(dr["TradeIn"]);
                        commission.Status = Convert.ToInt32(dr["Status"]);
                        commission.ProcesByPayroll = Convert.ToBoolean(dr["ProcesByPayroll"]);
                        commission.SlipType = Convert.ToInt32(dr["SlipType"]);
                        commission.MainCommissionID = Convert.ToInt32(dr["MainCommissionID"]);
                        commission.BranchID = Convert.ToInt32(dr["BranchID"]);
                        commission.BranchName = dr["BranchName"].ToString();
                        commission.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                        commission.Comments = dr["Comments"].ToString();
                        commissionlist.Add(commission);
                    }

                    return commissionlist;
                }
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetCommissionbyID : Failed to Get Commission Componant by ID" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get Commission Componant by ID. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To add commission detials component.
        /// </summary>
        /// <param name="user">User value</param>
        /// <param name="comm">Commission details</param>
        /// <returns>Success/error</returns>
        public int CreateCommissionComponant(IPrincipal user, CommissionComponent comm)
        {
            DateTime start = DateTime.Now;

            string message = $"CreateCommissionComponant\t{user.Identity.Name}";

            try
            {
                return NewCommissionSlipStoredProcedures.CreateCommission(comm);
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => CreateCommissionComponant : Failed to Create Commission Componant" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Create Commission Componant. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To edit commission details component.
        /// </summary>
        /// <param name="user">User value</param>
        /// <param name="comm">Commission details</param>
        /// <returns>Success/error</returns>
        public bool EditCommissionComponant(IPrincipal user, CommissionComponent comm)
        {
            DateTime start = DateTime.Now;

            string message = $"EditCommissionComponant\t{user.Identity.Name}";

            try
            {
                return NewCommissionSlipStoredProcedures.UpdateCommission(comm);
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => EditCommissionComponant : Failed to Edit Commission Componant" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Edit Commission Componant. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }
        /// <summary>
        /// To edit Tip lead slip
        /// </summary>
        /// <param name="user">User value</param>
        /// <param name="comm">Tiplead slip details </param>
        /// <returns>Success/error</returns>
        public bool EditTipLeadSlip(IPrincipal user, CommissionComponent comm)
        {
            DateTime start = DateTime.Now;

            string message = $"EditTipLeadSlip\t{user.Identity.Name}";

            try
            {
                return NewCommissionSlipStoredProcedures.UpdateTipLeadSlip(comm);
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => EditTipLeadSlip : Failed to Edit Commission Componant" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Edit Commission Componant. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To delete commission componant based on ID
        /// </summary>
        /// <param name="user">User value</param>
        /// <param name="comm">Commission details</param>
        /// <returns>Success/error</returns>
        public bool DeleteCommissionComponant(IPrincipal user, CommissionComponent comm)
        {
            DateTime start = DateTime.Now;

            string message = $"DeleteCommissionComponant\t{user.Identity.Name}";

            try
            {
                return NewCommissionSlipStoredProcedures.DeleteCommission(comm);
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => DeleteCommissionComponant : Failed to Delete Commission Componant" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Delete Commission Componant. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }
        /// <summary>
        /// To get deactivated GM and Pay PlanID for Employee
        /// </summary>
        /// <param name="user"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        public int[] GetDeactiveGMandPlanID(IPrincipal user, EmployeeComponant employee)
        {
            DateTime start = DateTime.Now;

            string message = $"GetDeactiveGMandPlanID\t{user.Identity.Name}";

            try
            {
               return NewCommissionSlipStoredProcedures.GetDeactiveGMandPlanID(employee);

            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetDeactiveGMandPlanID: Failed to Get deactivated GM and Pay PlanID" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get deactivated GM and Pay PlanID" + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        #endregion
    }
}
