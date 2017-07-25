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
    /// This class is implemented for new employees related components.
    /// </summary>
    public partial class Servicelibrary : IDisposable
    {
        #region Employee components
        /// <summary>
        /// To get all employees list.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="RoleID">Role ID</param>
        /// <returns></returns>
        public List<EmployeeComponant> GetAllEmployeesbyRoleID(IPrincipal user, int RoleID)
        {
            DateTime start = DateTime.Now;

            string message = $"GetAllEmployeesbyRoleID\t{user.Identity.Name}";

            try
            {
                List<EmployeeComponant> EmployeeList = new List<EmployeeComponant>();

                using (DataTable dt = EmployeeComStoredProcedures.GetAllEmployeesbyRoleID(RoleID))
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
                            RoleName = dr["RoleName"].ToString(),
                            RoleID = Convert.ToInt32(dr["RoleID"]),
                            ReportMgr = Convert.ToInt32(dr["ReportMgrID"].ToString()),
                            ApproveMgr = Convert.ToInt32(dr["ApproveMgrID"].ToString()),
                            ReportMgrName = dr["ReportMgrName"].ToString(),
                            ApproveMgrName = dr["ApproveMgrName"].ToString(),
                            IsActive = Convert.ToBoolean(dr["IsActive"])
                        });
                    }

                    return EmployeeList;
                }
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllEmployeesbyRoleID: Failed to Get Employee Componant" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get Employee Componant. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To get employee details based on UID.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="emp"></param>
        /// <returns></returns>
        public EmployeeComponant GetEmployeeByID(IPrincipal user, EmployeeComponant emp)
        {
            DateTime start = DateTime.Now;

            string message = $"GetEmployeeByID\t{user.Identity.Name}";

            try
            {
                EmployeeComponant Employee = new EmployeeComponant();
                using (DataTable dt = EmployeeComStoredProcedures.GetEmployeebyID(emp))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Employee.UID = Convert.ToInt32(dr["UID"].ToString());
                        Employee.EmployeeID = dr["EmployeeID"].ToString();
                        Employee.FirstName = dr["FirstName"].ToString();
                        Employee.LastName = dr["LastName"].ToString();
                        Employee.RoleID = Convert.ToInt32(dr["RoleID"]);
                        Employee.RoleName = dr["RoleName"].ToString();
                        Employee.Email = dr["Email"].ToString();
                        Employee.AccountName = dr["AccountName"].ToString();
                        if (!dr.IsNull("DateofHire")){ Employee.DateofHire =  Convert.ToDateTime(dr["DateofHire"]).Date;  }
                        if (!dr.IsNull("DateInPosition")) { Employee.DateInPosition = Convert.ToDateTime(dr["DateInPosition"]).Date; }
                        Employee.PrimaryBranch = dr["PrimaryBranch"].ToString();
                        Employee.PrimaryBranchName = dr["PrimaryBranchName"].ToString();
                        Employee.SecondaryBranch = new String[] { dr["SecondaryBranch"].ToString() };
                        Employee.SecondaryBranchName = dr["SecondaryBranchName"].ToString();
                        Employee.ReportMgr = Convert.ToInt32(dr["ReportMgr"].ToString());
                        Employee.ReportMgrName = dr["ReportMgrName"].ToString();
                        Employee.ApproveMgr = Convert.ToInt32(dr["ApproveMgr"].ToString());
                        Employee.ApproveMgrName = dr["ApproveMgrName"].ToString();
                        Employee.PayPlanID = dr["PayPlanID"].ToString();
                        Employee.PayPlanName = dr["PlanName"].ToString();
                        Employee.BPSalary = Convert.ToBoolean(dr["BPSalary"]);
                        Employee.BPDraw = Convert.ToBoolean(dr["BPDraw"]);
                        Employee.MonthAmount = Convert.ToDecimal(dr["MonthAmount"]);
                        Employee.TypeofDraw = Convert.ToInt32(dr["TypeofDraw"]);
                        Employee.DRPercentage = Convert.ToDecimal(dr["DRPercentage"]);
                        Employee.DrawTerm = Convert.ToInt32(dr["DrawTerm"].ToString());
                        if (!dr.IsNull("DDPeriod")) { Employee.DDPeriod = Convert.ToDateTime(dr["DDPeriod"]).Date; }
                        Employee.DDAmount = Convert.ToDecimal(dr["DDAmount"]);
                    }
                return Employee;
                }
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetEmployeeByID: Failed to Get Employee Componant by its ID" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get Employee Componant by ID. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To create employee details.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="emp"></param>
        /// <returns></returns>
        public int CreateEmployeeComponant(IPrincipal user, EmployeeComponant emp)
        {
            DateTime start = DateTime.Now;

            string message = $"CreateEmployeeComponant\t{user.Identity.Name}";

            try
            {
                return EmployeeComStoredProcedures.CreateEmployee(emp);
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => CreateEmployeeComponant : Failed to Create Employee Componant" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Create Employee Componant. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To edit employee details.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="emp"></param>
        /// <returns></returns>
        public bool EditEmployeeComponant(IPrincipal user, EmployeeComponant emp)
        {
            DateTime start = DateTime.Now;

            string message = $"EditEmployeeComponant\t{user.Identity.Name}";

            try
            {
                return EmployeeComStoredProcedures.UpdateEmployee(emp);
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => EditEmployeeComponant : Failed to Edit Employee Componant" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Edit Employee Componant. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }
        
        /// <summary>
        /// To activate/deactivate employee componant
        /// </summary>
        /// <param name="user"></param>
        /// <param name="emp"></param>
        /// <returns></returns>
        public bool ActiveDeActiveEmployeeComponant(IPrincipal user, EmployeeComponant emp)
        {
            DateTime start = DateTime.Now;

            string message = $"ActiveDeActiveEmployeeComponant\t{user.Identity.Name}";

            try
            {
                return EmployeeComStoredProcedures.ActiveDeActiveEmployee(emp);
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => ActiveDeActiveEmployeeComponant : Failed to Active/Deactive Employee Componant" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Active/Deactive Employee Componant. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To get all employees linked with deactivated plans
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<EmployeeComponant> GetDeActivatePlanEmployees(IPrincipal user)
        {
            DateTime start = DateTime.Now;

            string message = $"GetDeActivatePlanEmployees\t{user.Identity.Name}";

            try
            {
                List<EmployeeComponant> EmployeeList = new List<EmployeeComponant>();

                using (DataTable dt = EmployeeComStoredProcedures.GetDeActivatePlanEmployees())
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        EmployeeComponant emp = new EmployeeComponant();
                        emp.UID = Convert.ToInt32(dr["UID"]);
                        emp.AccountName = dr["AccountName"].ToString();
                        emp.EmployeeID = dr["EmployeeID"].ToString();
                        emp.FirstName = dr["FirstName"].ToString();
                        emp.LastName = dr["LastName"].ToString();
                        emp.PayPlanID = dr["PlanID"].ToString();
                        emp.PayPlanName = dr["PlanName"].ToString();
                        EmployeeList.Add(emp);
                    }

                    return EmployeeList;
                }
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetDeactivatePlanEmployees : Failed to Get Employees with deactivated Plans" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get Employees with deactivated plans. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }
        /// <summary>
        /// To Get all Mail Ids to send Mail Notification about Commission creation
        /// </summary>
        /// <param name="user"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public string GetCommissionCreatedMailIDs(IPrincipal user, CommissionComponent email)
        {
            DateTime start = DateTime.Now;

            string message = $"GetCommissionCreatedMailIDs\t{user.Identity.Name}";

            try
            {
                return EmployeeComStoredProcedures.GetCommissionCreatedMailIDs(email);
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetCommissionCreatedMailIDs : Failed to get Mail IDs" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Mail IDs. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }
        #endregion
    }
}
