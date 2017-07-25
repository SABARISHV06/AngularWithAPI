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
using Security;

namespace ServiceLibrary
{
    /// <summary>
    /// This class implemented Common Components
    /// </summary>
    public partial class Servicelibrary : IDisposable
    {
        /// <summary>
        /// Check login user is a valid user or not
        /// </summary>
        /// <param name="user"></param>
        /// <param name="AccountName"></param>
        /// <returns></returns>
        public EmployeeComponant CheckUser(IPrincipal user, string AccountName)
        {
            DateTime start = DateTime.Now;

            string message = $"CheckUser\t{user.Identity.Name}";
            try
            {
                return CommonStoredProcedures.CheckUser(AccountName);
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => CheckUser : Failed to Check User details" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Check User details. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }
        /// <summary>
        /// To Create User log details
        /// </summary>
        /// <param name="user"></param>
        /// <param name="UID"></param>
        /// <returns></returns>
        public int AddUserLog(IPrincipal user, int UID)
        {
            DateTime start = DateTime.Now;

            string message = $"AddUserLog\t{user.Identity.Name}";
            try
            {
                return CommonStoredProcedures.AddUserLog(UID);
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => AddUserLog : Failed to Add Userlog details" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Add Userlog details. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }
        /// <summary>
        /// To Edit User log details
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ID"></param>
        /// <param name="UID"></param>
        /// <returns></returns>
        public bool EditUserLog(IPrincipal user, int ID, int UID)
        {
            DateTime start = DateTime.Now;

            string message = $"EditUserLog\t{user.Identity.Name}";
            try
            {
                return CommonStoredProcedures.EditUserLog(ID, UID);
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => EditUserLog : Failed to Edit Userlog details" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Edit Userlog details. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }
        /// <summary>
        /// To get All Roles
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<Roles> GetAllRoles(IPrincipal user)
        {
            DateTime start = DateTime.Now;

            string message = $"GetAllRoles\t{user.Identity.Name}";

            List<Roles> rolesList = new List<Roles>();

            try
            {
                DataTable dt = CommonStoredProcedures.GetAllRoles();
                foreach (DataRow dr in dt.Rows)
                {
                    rolesList.Add(new Roles
                    {
                        RoleID = dr["RoleID"].ToString(),
                        Name = dr["Name"].ToString()
                    });
                }

                return rolesList;
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllRoles : Failed to Get All Roles" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Roles data. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }
        /// <summary>
        /// To get All Users
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<string> GetListOfUsers(IPrincipal user)
        {
            DateTime start = DateTime.Now;

            string message = $"GetListOfUsers\t{user.Identity.Name}";

            List<string> usersList = new List<string>();

            try
            {
                Security.LDAPUsers ldap = new Security.LDAPUsers();
                usersList = ldap.GetUserAccounts();
                return usersList;
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetListOfUsers : Failed to Get List Of Users" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get List Of Users. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To get all Branches
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<Branches> GetAllBranches(IPrincipal user)
        {
            DateTime start = DateTime.Now;

            string message = $"GetAllBranches\t{user.Identity.Name}";

            List<Branches> branchList = new List<Branches>();

            try
            {
                DataTable dt = CommonStoredProcedures.GetAllBranches();
                foreach (DataRow dr in dt.Rows)
                {
                    branchList.Add(new Branches
                    {
                        BranchID = dr["BranchID"].ToString(),
                        BranchName = dr["BranchName"].ToString(),
                        BranchCode = dr["BranchCode"].ToString(),
                        IsActive = Convert.ToBoolean(dr["IsActive"])
                    });
                }

                return branchList;
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllBranches : Failed to Get All Branches" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Branch data. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To get all Employees
        /// </summary>
        /// <param name="user"></param>
        /// <param name="rol"></param>
        /// <returns></returns>
        public List<EmployeeComponant> GetNamesbyRoleID(IPrincipal user, Roles rol)
        {
            DateTime start = DateTime.Now;

            string message = $"GetNamesbyRoleID\t{user.Identity.Name}";

            List<EmployeeComponant> userList = new List<EmployeeComponant>();

            try
            {
                DataTable dt = CommonStoredProcedures.GetNamesbyRoleID(rol);
                foreach (DataRow dr in dt.Rows)
                {
                    userList.Add(new EmployeeComponant
                    {
                        EmployeeID = dr["EmployeeID"].ToString(),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString()
                    });
                }

                return userList;
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetNamesbyRoleID : Failed to Get Employee names by RoleID" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Employee names by RoleID. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To get all Managers
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<EmployeeComponant> GetAllManagers(IPrincipal user)
        {
            DateTime start = DateTime.Now;

            string message = $"GetAllManagers\t{user.Identity.Name}";

            List<EmployeeComponant> managerList = new List<EmployeeComponant>();

            try
            {
                DataTable dt = CommonStoredProcedures.GetAllManagers();
                foreach (DataRow dr in dt.Rows)
                {
                    managerList.Add(new EmployeeComponant
                    {
                        UID = Convert.ToInt32(dr["UID"]),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        RoleID = Convert.ToInt32(dr["RoleID"])
                    });
                }

                return managerList;
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllManagers : Failed to Get All Manager" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get Manager data. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To send email to the following recipient 
        /// </summary>
        /// <param name="user">session id</param>
        /// <param name="emailModal">email modal</param>
        /// <returns></returns>
        public bool SendMail(IPrincipal user, EmailMessage emailModal)
        {
            DateTime start = DateTime.Now;

            string message = $"SendMail\t{user.Identity.Name}";

            try
            {
                return emailModal.Send(emailModal);
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => SendMail : Failed to send mail" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't sent mail to the recipiant " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To add/update branches
        /// </summary>
        /// <param name="user">session id</param>
        /// <param name="emailModal">email modal</param>
        /// <returns></returns>
        public bool SaveBranch(IPrincipal user, Branches branch)
        {
            DateTime start = DateTime.Now;

            string message = $"SaveBranch\t{user.Identity.Name}";

            try
            {
                return CommonStoredProcedures.SaveBranch(branch);
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SaveBranch", "Method => SaveBranch : Failed to add/update branch" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't add/update branch " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To get all SaleType
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<SaleType> GetAllSaleType(IPrincipal user)
        {
            DateTime start = DateTime.Now;

            string message = $"GetAllSaleType\t{user.Identity.Name}";

            List<SaleType> saletypeList = new List<SaleType>();

            try
            {
                DataTable dt = CommonStoredProcedures.GetAllSaleType();
                foreach (DataRow dr in dt.Rows)
                {
                    saletypeList.Add(new SaleType
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        SaleTypeName = dr["SalesTypeName"].ToString(),
                        IsNewCustomer = Convert.ToBoolean(dr["IsNewCustomer"]),
                        IsExistingCustomer = Convert.ToBoolean(dr["IsExistingCustomer"]),
                        IsBiMonthlyBonus = Convert.ToBoolean(dr["IsBiMonthlyBonus"]),
                        IsTenureBonus = Convert.ToBoolean(dr["IsTenureBonus"]),
                        IsActive = Convert.ToBoolean(dr["IsActive"])
                    });
                }

                return saletypeList;
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SalesCommission", "Method => GetAllSaleType : Failed to Get All SaleType" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't get SaleType data. " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To add/update Saletype
        /// </summary>
        /// <param name="user">session id</param>
        /// <param name="emailModal">email modal</param>
        /// <returns></returns>
        public bool SaveSaleType(IPrincipal user, SaleType saleType)
        {
            DateTime start = DateTime.Now;

            string message = $"SaveSaleType\t{user.Identity.Name}";

            try
            {
                return CommonStoredProcedures.SaveSaleType(saleType);
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("SaveBranch", "Method => SaveSaleType : Failed to add/update SaleType" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't add/update SaleType " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To check the branch is de-active
        /// </summary>
        /// <param name="user">session id</param>
        /// <param name="branchid">branch id</param>
        /// <returns></returns>
        public bool CheckBranch(IPrincipal user, int branchid)
        {
            DateTime start = DateTime.Now;

            string message = $"CheckBranch\t{user.Identity.Name}";

            try
            {
                return CommonStoredProcedures.CheckBranch(branchid);
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("CheckBranch", "Method => CheckBranch : Failed to Check Branches" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Check Branch Status " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To check the Saletype is de-active
        /// </summary>
        /// <param name="user">session id</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public bool CheckSaletype(IPrincipal user, CheckSaleType saleType)
        {
            DateTime start = DateTime.Now;

            string message = $"CheckSaletype\t{user.Identity.Name}";

            try
            {
                return CommonStoredProcedures.CheckSaletype(saleType);
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("CheckSaletype", "Method => CheckSaletype : Failed to Check Saletype" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Check Saletype Status " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To check the Saletype is de-active
        /// </summary>
        /// <param name="user">session id</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public List<PayrollConfig> GetAllPayrollConfigByYear(IPrincipal user, int Year)
        {
            List<PayrollConfig> returnPayroll = new List<PayrollConfig>();

            DateTime start = DateTime.Now;

            string message = $"GetAllPayrollConfig\t{user.Identity.Name}";

            try
            {
                DataTable dt = CommonStoredProcedures.GetAllPayrollConfigByYear(Year);
                foreach (DataRow dr in dt.Rows)
                {
                    PayrollConfig payroll = new PayrollConfig();
                    payroll.ID = Convert.ToInt32(dr["ID"]);
                    payroll.Year = Convert.ToInt32(dr["Year"]);
                    payroll.Month = dr["Month"].ToString();
                    payroll.Period = Convert.ToInt32(dr["Period"]);
                    payroll.DateFrom = Convert.ToDateTime(dr["DateFrom"]);
                    payroll.DateTo = Convert.ToDateTime(dr["DateTo"]);
                    payroll.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    payroll.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                    payroll.CreatedByName = dr["CreatedByName"].ToString();
                    payroll.ModifiedOn = Convert.ToDateTime(dr["ModifiedOn"]);
                    payroll.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"]);
                    payroll.ModifiedByName = dr["ModifiedByName"].ToString();
                    payroll.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    payroll.ProcessByPayroll = Convert.ToInt32(dr["ProcessByPayroll"]);

                    returnPayroll.Add(payroll);
                }

                return returnPayroll;
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("GetAllPayrollConfig", "Method => GetAllPayrollConfig : Failed to Check Saletype" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get All PayrollConfig " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To check the Saletype is de-active
        /// </summary>
        /// <param name="user">session id</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public List<PayrollConfig> GetAllPayrollConfig(IPrincipal user)
        {
            List<PayrollConfig> returnPayroll = new List<PayrollConfig>();

            DateTime start = DateTime.Now;

            string message = $"GetAllPayrollConfig\t{user.Identity.Name}";

            try
            {
                DataTable dt = CommonStoredProcedures.GetAllPayrollConfig();
                foreach (DataRow dr in dt.Rows)
                {
                    PayrollConfig payroll = new PayrollConfig();
                    payroll.Year = Convert.ToInt32(dr["Year"]);
                    payroll.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                    payroll.CreatedBy = Convert.ToInt32(dr["CreatedBy"]);
                    payroll.CreatedByName = dr["CreatedByName"].ToString();
                    payroll.ModifiedBy = Convert.ToInt32(dr["ModifiedBy"]);
                    payroll.ModifiedByName = payroll.ModifiedBy != 0 ? dr["ModifiedByName"].ToString() : dr["CreatedByName"].ToString();

                    returnPayroll.Add(payroll);
                }

                return returnPayroll;
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("GetAllPayrollConfig", "Method => GetAllPayrollConfig : Failed to Check Saletype" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get All PayrollConfig " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To update Payroll
        /// </summary>
        /// <param name="user">session id</param>
        /// <param name="payRoll">payroll modal</param>
        /// <returns></returns>
        public bool UpdatePayroll(IPrincipal user, PayrollConfig payRoll)
        {
            DateTime start = DateTime.Now;

            string message = $"UpdatePayroll\t{user.Identity.Name}";

            try
            {
                return CommonStoredProcedures.UpdatePayroll(payRoll);
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("UpdatePayroll", "Method => UpdatePayroll : Failed to update Payroll" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't update Payroll " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To Insert Payroll
        /// </summary>
        /// <param name="user">session id</param>
        /// <param name="payRoll">payroll modal</param>
        /// <returns></returns>
        public bool AddPayroll(IPrincipal user, PayrollConfig payRoll)
        {
            DateTime start = DateTime.Now;

            string message = $"InsertPayroll\t{user.Identity.Name}";

            try
            {
                return CommonStoredProcedures.InsertPayroll(payRoll);
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("InsertPayroll", "Method => InsertPayroll : Failed to Insert Payroll" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't insert Payroll " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To Get Accounting Month
        /// </summary>
        /// <param name="user">session id</param>
        /// <returns></returns>
        public PayrollConfig GetAccountingMonth(IPrincipal user, DateTime currentDate)
        {
            PayrollConfig returnPayroll = new PayrollConfig();

            DateTime start = DateTime.Now;

            string message = $"GetAllPayrollConfig\t{user.Identity.Name}";

            try
            {
                DataTable dt = CommonStoredProcedures.GetAccountingMonth(currentDate);
                foreach (DataRow dr in dt.Rows)
                {
                    returnPayroll.ID = Convert.ToInt32(dr["ID"]);
                    returnPayroll.Month = dr["MONTH"].ToString();
                }

                return returnPayroll;
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("GetAccountingMonth", "Method => GetAccountingMonth : Failed to Get Accounting Month" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get Accounting Month " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }

        /// <summary>
        /// To check the Saletype is de-active
        /// </summary>
        /// <param name="user">session id</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public bool CheckPayrollConfigYear(IPrincipal user, int Year)
        {


            DateTime start = DateTime.Now;

            string message = $"CheckPayrollConfigYear\t{user.Identity.Name}";

            try
            {
                return CommonStoredProcedures.CheckPayrollConfigYear(Year);

                //return returnPayroll;
            }
            catch (Exception e)
            {
                UtilityLog.EventLogWriteEntry("GetAllPayrollConfig", "Method => GetAllPayrollConfig : Failed to Check Saletype" + e, System.Diagnostics.EventLogEntryType.Error);
                throw new ExceptionLog("Can't Get All PayrollConfig " + message, TErrorMessageType.ERROR, e, SessionID, UserIdentity.Name);
            }
        }
    }
}
