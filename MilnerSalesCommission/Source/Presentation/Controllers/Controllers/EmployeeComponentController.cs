// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//
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

namespace ServiceLibrary
{
    /// <summary>
    /// To handle employee componant controllers related actions and behaiours
    /// </summary>
    public sealed class EmployeeComponentController : ApiBaseController
    {

        /// <summary>
        /// To call GetAllEmployeesbyRoleID method with session ID
        /// </summary>
        /// <param name="RoleID">Role ID</param>
        /// <returns>All employee details</returns>
        [HttpPost]
        public List<EmployeeComponant> GetAllEmployeesbyRoleID([FromBody]int RoleID)
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
            return GetAllEmployeesbyRoleID(sessionid, RoleID);
        }

        /// <summary>
        /// To get all employees details
        /// </summary>
        /// <param name="s"></param>
        /// <param name="RoleID">Role ID</param>
        /// <returns>All employee details</returns>
        [HttpPost]
        public List<EmployeeComponant> GetAllEmployeesbyRoleID(string s, int RoleID)
        {
            try
            {

                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                List<EmployeeComponant> e = servicelibrary.GetAllEmployeesbyRoleID(User, RoleID);

                //Utility.AuditLogHelper.InfoLogMessage("Retrieved all Employee Details");

                return e;
            }
            catch (Exception e)
            {
                string message = "Get Employees by RoleID could not be retrieved. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Error while get all employees info by RoleID", e);
            }

            return null;
        }

        /// <summary>
        /// To call GetEmployeesByID method with session ID
        /// </summary>
        /// <param name="emp">EmployeeComponant</param>
        /// <returns>Employee</returns>
        [HttpPost]
        public EmployeeComponant GetEmployeesByID(EmployeeComponant emp)
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
            return GetEmployeesByID(sessionid, emp);
        }

        /// <summary>
        /// To get employee details by its ID
        /// </summary>
        /// <param name="s">session id</param>
        /// <param name="emp">employee componant</param>
        /// <returns></returns>
        [HttpPost]
        public EmployeeComponant GetEmployeesByID(string s, EmployeeComponant emp)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);
                EmployeeComponant e = servicelibrary.GetEmployeeByID(User,emp);
                return e;
            }
            catch (Exception e)
            {
                string message = "Get Employees by ID could not be retrieved. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Error while get employee info by ID", e);
            }

            return null;
        }

        /// <summary>
        /// To call CreateEmployee method with session id
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public int CreateEmployee(EmployeeComponant employee)
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
            return CreateEmployee(sessionid, employee);
        }

        /// <summary>
        /// To add the employee details 
        /// </summary>
        /// <param name="s">session id</param>
        /// <param name="employee">employee component</param>
        /// <returns></returns>
        [HttpPost]
        public int CreateEmployee(string s, EmployeeComponant employee)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                StringBuilder strinbuilder = new StringBuilder();

                foreach (string value in employee.SecondaryBranch)
                {
                    strinbuilder.Append(value);
                    strinbuilder.Append(',');
                };

                employee.SecondaryBranchName = strinbuilder.ToString();

                return servicelibrary.CreateEmployeeComponant(User,employee);

            }
            catch (Exception e)
            {
                string message = "Employee could not be Inserted. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Error while create employee", e, employee.CreatedBy.ToString());
            }

            return -1;
        }

        /// <summary>
        /// To call EditEmployee method with session id
        /// </summary>
        /// <param name="employee">employee componant</param>
        /// <returns></returns>
        [HttpPost]
        public bool EditEmployee(EmployeeComponant employee)
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
            return EditEmployee(sessionid, employee);
        }

        /// <summary>
        /// To update the employee details while editing
        /// </summary>
        /// <param name="s">session id</param>
        /// <param name="employee">employee componant</param>
        /// <returns></returns>
        [HttpPost]
        public bool EditEmployee(string s, EmployeeComponant employee)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);
                
                StringBuilder strinbuilder = new StringBuilder();

                foreach (string value in employee.SecondaryBranch)
                {
                    strinbuilder.Append(value);
                    strinbuilder.Append(',');
                };

                employee.SecondaryBranchName = strinbuilder.ToString();

                return servicelibrary.EditEmployeeComponant(User, employee);

            }
            catch (Exception e)
            {
                string message = "Employee could not be Updated. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Error while Edit employee", e, employee.ModifiedBy.ToString());
            }

            return false;
        }

        /// <summary>
        /// To call ActiveDeactiveEmployee method with session id
        /// </summary>
        /// <param name="employee">employee componant</param>
        /// <returns></returns>
        [HttpPost]
        public bool ActiveDeactiveEmployee(EmployeeComponant employee)
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
            return ActiveDeactiveEmployee(sessionid, employee);
        }

        /// <summary>
        /// To activate & deactivate the employee 
        /// </summary>
        /// <param name="s">session id</param>
        /// <param name="employee">employee component</param>
        /// <returns></returns>
        [HttpPost]
        public bool ActiveDeactiveEmployee(string s, EmployeeComponant employee)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                return servicelibrary.ActiveDeActiveEmployeeComponant(User, employee);

            }
            catch (Exception e)
            {
                string message = "Employee could not be Activate/deactive. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Error while Activate/deactive employee", e, employee.ModifiedBy.ToString());
            }

            return false;
        }

        /// <summary>
        /// To call GetDeActivatePlanEmployees method with session ID
        /// </summary>
        /// <returns>All employee details</returns>
        [HttpGet]
        public List<EmployeeComponant> GetDeActivatePlanEmployees()
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
            return GetDeActivatePlanEmployees(sessionid);
        }

        /// <summary>
        /// To list all employee linked with deactivate payplan
        /// </summary>
        /// <param name="s"></param>
        /// <returns>All employee details</returns>
        [HttpGet]
        public List<EmployeeComponant> GetDeActivatePlanEmployees(string s)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                List<EmployeeComponant> e = servicelibrary.GetDeActivatePlanEmployees(User);

                return e;
            }
            catch (Exception e)
            {
                string message = "Get Employee with deactivated plans could not be retrieved. " + e.Message;
                OnHttpResponseMessage(message);
            }

            return null;
        }
        /// <summary>
        /// To call GetCommissionCreatedMailIDs method with session ID
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetCommissionCreatedMailIDs(CommissionComponent email)
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
            return GetCommissionCreatedMailIDs(sessionid, email);
        }

        /// <summary>
        /// To Get all Mail Ids to send Mail Notification about Commission creation
        /// </summary>
        /// <param name="s"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetCommissionCreatedMailIDs(string s, CommissionComponent email)
        {
            try
            {

                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                string MailIds = servicelibrary.GetCommissionCreatedMailIDs(User, email);

                Utility.AuditLogHelper.InfoLogMessage("Get MailIds for Commission created Notification");

                return MailIds;
            }
            catch (Exception e)
            {
                string message = "Get MailIDs could not be retrieved. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Error while get MailIds", e);
            }

            return null;
        }
    }
}
