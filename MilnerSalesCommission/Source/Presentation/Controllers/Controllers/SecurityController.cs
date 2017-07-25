// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//
using ServiceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ViewModels;
using Security;

namespace ApiControllers.Controllers
{
    /// <summary>
    /// To handle security controller related actions and behaiours
    /// </summary>
    public sealed class SecurityController : ApiBaseController
    {

        /// <summary>
        /// To call CheckUser method with session ID.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public EmployeeComponant CheckUser()
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }
            return CheckUser(sessionid);
        }

        /// <summary>
        /// To check login user is valid user or not
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpPost]
        public EmployeeComponant CheckUser(string s)
        {
           
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);
                if (User.Identity.IsAuthenticated)
                {
                    string AccountName = User.Identity.Name;                
                //User is Invalid if it returns zero
                return servicelibrary.CheckUser(User, AccountName);
                }
            }
            catch (Exception e)
            {
                string message = "Check Users Could not be executed. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Check Users Could not be executed.", e);
            }
            return null;
        }

        /// <summary>
        /// To call AddUserLog method with session ID.
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public int AddUserLog(int UID)
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[ApiControllers.Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }
            return AddUserLog(sessionid, UID);
        }

        /// <summary>
        /// To Add User Log details
        /// </summary>
        /// <param name="s"></param>
        /// <param name="UID"></param>
        /// <returns></returns>        
        [HttpPost]
        public int AddUserLog(string s, int UID)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

               return servicelibrary.AddUserLog(User, UID);
            }
            catch (Exception e)
            {
                string message = "Add User Log could not be executed." + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Add User Log could not be executed.", e);
            }

            return 0;
        }

        /// <summary>
        /// To call EditUserLog method with session ID.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UID"></param>
        /// <returns></returns>
        [HttpPost]
        public bool EditUserLog(int ID,int UID)
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[ApiControllers.Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }
            return EditUserLog(sessionid, ID, UID);
        }

        /// <summary>
        /// To Edit User Log details
        /// </summary>
        /// <param name="s"></param>
        /// <param name="ID"></param>
        /// <param name="UID"></param>
        /// <returns></returns>
        [HttpPost]
        public bool EditUserLog(string s, int ID, int UID)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                return servicelibrary.EditUserLog(User, ID, UID);
            }
            catch (Exception e)
            {
                string message = "Edit User Log could not be executed." + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Edit User Log could not be executed.", e);
            }

            return false;
        }

        /// <summary>
        /// To call GetAllUsers method with session ID.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<string> GetAllUsers()
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }
            return GetAllUsers(sessionid);
        }

        /// <summary>
        /// To Get all users with SessionID
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpGet]
        public List<string> GetAllUsers(string s)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                List<string> r = servicelibrary.GetListOfUsers(User);

                return r;
            }
            catch (Exception e)
            {
                string message = "Get All Users could not be retrieved. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Get All Users could not be retrieved.", e);
            }

            return null;
        }

        /// <summary>
        /// To call GetRoles method with session ID.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Roles> GetRoles()
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }
            return GetRoles(sessionid);
        }

        /// <summary>
        /// To Get all Roles with sessionID
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpGet]
        public List<Roles> GetRoles(string s)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                List<Roles> r = servicelibrary.GetAllRoles(User);

                return r;
            }
            catch (Exception e)
            {
                string message = "Get Roles could not be retrieved. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Get Roles could not be retrieved.", e);
            }
            return null;
        }

        /// <summary>
        /// To call GetAllBranch method with session ID.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Branches> GetAllBranch()
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }
            return GetAllBranch(sessionid);
        }

        /// <summary>
        /// To Get all Branches with sessionID
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpGet]
        public List<Branches> GetAllBranch(string s)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                List<Branches> r = servicelibrary.GetAllBranches(User);

                return r;
            }
            catch (Exception e)
            {
                string message = "Get All Branches could not be retrieved. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Get All Branches could not be retrieved.", e);
            }
            return null;
        }

        /// <summary>
        /// To call GetNamesbyRoleID method with session ID.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public List<EmployeeComponant> GetNamesbyRoleID(Roles role)
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }
            return GetNamesbyRoleID(sessionid, role);
        }

        /// <summary>
        /// To get Employee names by RoleID with sessionID
        /// </summary>
        /// <param name="s"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public List<EmployeeComponant> GetNamesbyRoleID(string s, Roles role)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                List<EmployeeComponant> r = servicelibrary.GetNamesbyRoleID(User, role);

                return r;
            }
            catch (Exception e)
            {
                string message = "Get Names by RoleID could not be retrieved. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Get Names by RoleID could not be retrieved.", e);
            }
            return null;
        }

        /// <summary>
        /// To call GetAllManager method with session ID.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<EmployeeComponant> GetAllManager()
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }
            return GetAllManager(sessionid);
        }

        /// <summary>
        /// To get all manager details with session ID
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpGet]
        public List<EmployeeComponant> GetAllManager(string s)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                List<EmployeeComponant> r = servicelibrary.GetAllManagers(User);

                return r;
            }
            catch (Exception e)
            {
                string message = "Get all Managers could not be retrieved. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Get all Managers could not be retrieved.", e);
            }
            return null;
        }

        /// <summary>
        /// To call SendMail method with session ID
        /// </summary>
        /// <param name="email">Email viewmodal</param>
        /// <returns></returns>
        [HttpPost]
        public bool SendMail(EmailMessage email)
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }
            return SendMail(sessionid, email);
        }

        /// <summary>
        /// To send email based on parameter details
        /// </summary>
        /// <param name="s">session id</param>
        /// <param name="email">email viewmodal</param>
        /// <returns></returns>
        [HttpPost]
        public bool SendMail(string s, EmailMessage email)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                 return servicelibrary.SendMail(User, email);

            }
            catch (Exception e)
            {
                string message = "Send email based on parameter details is failed. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Send email based on parameter details is failed.", e);
            }
            return false;
        }

        /// <summary>
        /// To call SetBranch method with session id
        /// </summary>
        /// <param name="branch">Branch componant</param>
        /// <returns></returns>
        [HttpPost]
        public bool SaveBranch(List<Branches> branch)
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[ApiControllers.Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }
            return SaveBranch(sessionid, branch);
        }

        /// <summary>
        /// To insert/update the branch details while add/editing
        /// </summary>
        /// <param name="s">session id</param>
        /// <param name="branch">Branch componant</param>
        /// <returns></returns>
        [HttpPost]
        public bool SaveBranch(string s, List<Branches> branch)
        {
            bool result = false;
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                foreach (Branches obj in branch)
                {
                    result= servicelibrary.SaveBranch(User, obj);
                }

            }
            catch (Exception e)
            {
                string message = "Branch could not be added/updated. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Error while add/edit branch", e, branch[0].CreatedBy.ToString());
            }

            return result;
        }

        /// <summary>
        /// To call GetAllSaleType method with session ID.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<SaleType> GetAllSaleType()
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }
            return GetAllSaleType(sessionid);
        }

        /// <summary>
        /// To Get all SaleType with sessionID
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpGet]
        public List<SaleType> GetAllSaleType(string s)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                List<SaleType> r = servicelibrary.GetAllSaleType(User);

                return r;
            }
            catch (Exception e)
            {
                string message = "Get All SaleType could not be retrieved. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Get All SaleType could not be retrieved.", e);
            }
            return null;
        }

        /// <summary>
        /// To call SaveSaleType method with session id
        /// </summary>
        /// <param name="saleType">SaleType componant</param>
        /// <returns></returns>
        [HttpPost]
        public bool SaveSaleType(List<SaleType> saleType)
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[ApiControllers.Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }
            return SaveSaleType(sessionid, saleType);
        }

        /// <summary>
        /// To insert/update the saletype details while add/editing
        /// </summary>
        /// <param name="s">session id</param>
        /// <param name="saleType">SaleType componant</param>
        /// <returns></returns>
        [HttpPost]
        public bool SaveSaleType(string s, List<SaleType> saleType)
        {
            bool result = false;
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                foreach (SaleType obj in saleType)
                {
                    result = servicelibrary.SaveSaleType(User, obj);
                }

            }
            catch (Exception e)
            {
                string message = "SaleType could not be added/updated. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Error while add/edit SaleType", e, saleType[0].CreatedBy.ToString());
            }

            return result;
        }

        /// <summary>
        /// To call CheckBranch method with session ID.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public bool CheckBranch([FromBody]int branchid)
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }
            return CheckBranch(sessionid, branchid);
        }

        /// <summary>
        /// To check branch is de-active
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpPost]
        public bool CheckBranch(string s, int branchid)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                return servicelibrary.CheckBranch(User, branchid);

            }
            catch (Exception e)
            {
                string message = "Check Branch could not be validated. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Check Branch could not be validated.", e);
            }
            return false;
        }

        /// <summary>
        /// To call CheckSaletype method with session ID.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public bool CheckSaletype(CheckSaleType InData)
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }
            return CheckSaletype(sessionid, InData);
        }

        /// <summary>
        /// To check Saletype is de-active
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpPost]
        public bool CheckSaletype(string s, CheckSaleType InData)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                return servicelibrary.CheckSaletype(User, InData);

            }
            catch (Exception e)
            {
                string message = "Check Saletype could not be validated. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Check Saletype could not be validated.", e);
            }
            return false;
        }

        [HttpPost]
        public List<PayrollConfig> GetAllPayrollConfigByYear([FromBody]int Year)
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }

            return GetAllPayrollConfigByYear(sessionid, Year);
        }

        public List<PayrollConfig> GetAllPayrollConfigByYear(string s, int Year)
        {
            List<PayrollConfig> returnPayroll = new List<PayrollConfig>();
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                return servicelibrary.GetAllPayrollConfigByYear(User, Year);

            }
            catch (Exception e)
            {
                string message = "Get All Payroll Config could not be retreived. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Get All PayrollConfig could not be retreived.", e);
            }

            return returnPayroll;
        }

        [HttpGet]
        public List<PayrollConfig> GetAllPayrollConfig()
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }

            return GetAllPayrollConfig(sessionid);
        }

        public List<PayrollConfig> GetAllPayrollConfig(string s)
        {
            List<PayrollConfig> returnPayroll = new List<PayrollConfig>();
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                return servicelibrary.GetAllPayrollConfig(User);

            }
            catch (Exception e)
            {
                string message = "Get All Payroll Config could not be retreived. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Get All PayrollConfig could not be retreived.", e);
            }

            return returnPayroll;
        }

        /// <summary>
        /// To call UpdatePayroll method with session id
        /// </summary>
        /// <param name="payRoll">payRoll componant</param>
        /// <returns></returns>
        [HttpPost]
        public bool UpdatePayroll(List<PayrollConfig> payRoll)
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[ApiControllers.Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }
            return UpdatePayroll(sessionid, payRoll);
        }

        /// <summary>
        /// To update the payRoll details while editing
        /// </summary>
        /// <param name="s">session id</param>
        /// <param name="payRoll">payRoll componant</param>
        /// <returns></returns>
        [HttpPost]
        public bool UpdatePayroll(string s, List<PayrollConfig> payRoll)
        {
            bool result = false;
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                foreach (PayrollConfig obj in payRoll)
                {
                    obj.DateFrom = new DateTime(obj.Year, obj.DateFrom.Month, obj.DateFrom.Day);
                    obj.DateTo = new DateTime(obj.Year, obj.DateTo.Month, obj.DateTo.Day);

                    result = servicelibrary.UpdatePayroll(User, obj);
                }

            }
            catch (Exception e)
            {
                string message = "PayRoll could not be updated. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Error while edit payRoll", e, payRoll[0].CreatedBy.ToString());
            }

            return result;
        }

        [HttpGet]
        public bool AddPayroll(List<PayrollConfig> payRoll)
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }

            return AddPayroll(sessionid, payRoll);
        }
        /// <summary>
        /// To add the payRoll details
        /// </summary>
        /// <param name="s">session id</param>
        /// <param name="payRoll">payRoll componant</param>
        /// <returns></returns>
        [HttpPost]
        public bool AddPayroll(string s, List<PayrollConfig> payRoll)
        {
            bool result = false;
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                foreach (PayrollConfig obj in payRoll)
                {
                    obj.DateFrom= new DateTime(obj.Year, obj.DateFrom.Month, obj.DateFrom.Day);
                    obj.DateTo = new DateTime(obj.Year, obj.DateTo.Month, obj.DateTo.Day);
                    result = servicelibrary.AddPayroll(User, obj);
                }

            }
            catch (Exception e)
            {
                string message = "PayRoll could not be insert. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Error while add payRoll", e, payRoll[0].CreatedBy.ToString());
            }

            return result;
        }

        [HttpPost]
        public PayrollConfig GetAccountingMonth([FromBody]DateTime currentDate)
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }

            return GetAccountingMonth(sessionid, currentDate);
        }

        public PayrollConfig GetAccountingMonth(string s,DateTime currentDate )
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                return servicelibrary.GetAccountingMonth(User, currentDate);

            }
            catch (Exception e)
            {
                string message = "Get All Payroll Config could not be retreived. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Get All PayrollConfig could not be retreived.", e);
            }
            return null;
        }

        [HttpPost]
        public bool CheckPayrollConfigYear([FromBody]int Year)
        {
            string sessionid = null;
            try
            {
                sessionid = Request.Properties[Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionid == null)
            {
                sessionid = System.Web.HttpContext.Current.Session.SessionID;
            }

            return CheckPayrollConfigYear(sessionid, Year);
        }

        public bool CheckPayrollConfigYear(string s, int Year)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                return servicelibrary.CheckPayrollConfigYear(User, Year);

            }
            catch (Exception e)
            {
                string message = "Get All Payroll Config could not be retreived. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Get All PayrollConfig could not be retreived.", e);
            }
            return false;
        }
    }
}
