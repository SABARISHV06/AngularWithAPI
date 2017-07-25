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
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;
using ViewModels;
using System.Data;
using System.IO;

namespace ServiceLibrary
{
    /// <summary>
    /// To handle new commission componant controllers related actions and behaiours
    /// </summary>
    public sealed class NewCommissionController : ApiBaseController
    {
        /// <summary>
        /// To call GetAllCommissionsbyUID method with session ID
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        [HttpPost]
        public List<CommissionComponent> GetAllCommissionsbyUID(EmployeeComponant emp)
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
            return GetAllCommissionsbyUID(sessionid,emp);
        }

        /// <summary>
        /// To get all commission details by UID.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="emp"></param>
        /// <returns></returns>
        [HttpPost]
        public List<CommissionComponent> GetAllCommissionsbyUID(string s, EmployeeComponant emp)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                List<CommissionComponent> e = servicelibrary.GetAllCommissionsbyUID(User,emp);

                //Utility.AuditLogHelper.InfoLogMessage("Retrieved all Commissions details");

                return e;
            }
            catch (Exception e)
            {
                string message = "Get Commissions could not be retrieved. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Get Commissions could not be retrieved.", e);
            }

            return null;
        }

        /// <summary>
        /// To call GetCommissionbyID method with session ID
        /// </summary>
        /// <returns>All Commission details</returns>
        [HttpPost]
        public List<CommissionComponent> GetCommissionbyID([FromBody]int CommissionID)
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
            return GetCommissionbyID(sessionid, CommissionID);
        }

        /// <summary>
        /// To get Commission based on Commission ID.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="CommissionID"></param>
        /// <returns></returns>
        [HttpPost]
        public List<CommissionComponent> GetCommissionbyID(string s,int CommissionID)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                List<CommissionComponent> commissionlist= servicelibrary.GetCommissionbyID(User, CommissionID);
                foreach (CommissionComponent e in commissionlist)
                {
                    if (e.Comments != "")
                    {
                        StringReader theReader = new StringReader(e.Comments.ToString());

                        DataSet ds = new DataSet();

                        ds.ReadXml(theReader);

                        e.CommentList = new List<CommissionComponent>();

                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            CommissionComponent commission = new CommissionComponent();
                            commission.Comments = dr["Comments"].ToString();
                            commission.CommentedBy = dr["CommentedBy"].ToString();
                            commission.CreatedOn = Convert.ToDateTime(dr["Date"]);
                            e.CommentList.Add(commission);
                        }
                    }
                }

                //Utility.AuditLogHelper.InfoLogMessage("Retrieved Commission by ID details");

                return commissionlist;
            }
            catch (Exception e)
            {
                string message = "Get Commission by ID could not be retrieved. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Get Commission by ID could not be retrieved.", e);
            }

            return null;
        }

        /// <summary>
        /// To call CreateCommission method with session id
        /// </summary>
        /// <param name="Commission"></param>
        /// <returns></returns>
        [HttpPost]
        public int CreateCommission(CommissionComponent commission)
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
            return CreateCommission(sessionid, commission);
        }

        /// <summary>
        /// To add the commission details
        /// </summary>
        /// <param name="s">session id</param>
        /// <param name="commission">commission component</param>
        /// <returns></returns>
        [HttpPost]
        public int CreateCommission(string s, CommissionComponent commission)
        {
            try
            {                
               Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                return servicelibrary.CreateCommissionComponant(User, commission);

            }
            catch (Exception e)
            {
                string message = "Commission could not be Inserted. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Error while Commission creation", e, commission.CreatedBy.ToString());
            }

            return -1;
        }

        /// <summary>
        /// To call EditCommission method with session id
        /// </summary>
        /// <param name="Commission"></param>
        /// <returns></returns>
        [HttpPost]
        public bool EditCommission(CommissionComponent commission)
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
            return EditCommission(sessionid, commission);
        }

        /// <summary>
        /// To update the commission details.
        /// </summary>
        /// <param name="s">session id</param>
        /// <param name="commission">commission component</param>
        /// <returns></returns>
        [HttpPost]
        public bool EditCommission(string s, CommissionComponent commission)
        {
            try
            {
               
                 DataTable dt = new DataTable { TableName = "CommissionComments" };
                 dt.Columns.Add("Comments");
                 dt.Columns.Add("CommentedBy");
                 dt.Columns.Add("Date");
                
                if (commission.CommentList == null) //To check for existing comments and initiaze the list variable
                {
                    commission.CommentList = new List<CommissionComponent>();
                }
                if (commission.Comments != "")
                {
                    commission.CommentList.Add(commission);
                }
                for (int i = 0; commission.CommentList.Count > i; i++)
                {
                    dt = CreateDatatable(commission.CommentList[i], dt);
                }
                commission.Comments = dt.Rows.Count>0 ? ConvertDatatableToXML(dt): "";
               
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                return servicelibrary.EditCommissionComponant(User, commission);

            }
            catch (Exception e)
            {
                string message = "Commission could not be Updated. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Error while Edit Commission slip", e, commission.ModifiedBy.ToString());
            }

            return false;
        }
        /// <summary>
        /// To call EditTipLeadSlip method with session ID.
        /// </summary>
        /// <param name="commission"></param>
        /// <returns></returns>
        [HttpPost]
        public bool EditTipLeadSlip(CommissionComponent commission)
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
            return EditTipLeadSlip(sessionid, commission);
        }

        /// <summary>
        /// To update the tip lead slip details
        /// </summary>
        /// <param name="s">session id</param>
        /// <param name="commission">commission component</param>
        /// <returns></returns>
        [HttpPost]
        public bool EditTipLeadSlip(string s, CommissionComponent commission)
        {
            try
            {

                DataTable dt = new DataTable { TableName = "CommissionComments" };
                dt.Columns.Add("Comments");
                dt.Columns.Add("CommentedBy");
                dt.Columns.Add("Date");

                if (commission.CommentList == null) //To check for existing comments and initiaze the list variable
                {
                    commission.CommentList = new List<CommissionComponent>();
                }
                if (commission.Comments != "")
                {
                    commission.CommentList.Add(commission);
                }
                for (int i = 0; commission.CommentList.Count > i; i++)
                {
                    dt = CreateDatatable(commission.CommentList[i], dt);
                }
                commission.Comments = dt.Rows.Count > 0 ? ConvertDatatableToXML(dt) : "";

                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                return servicelibrary.EditTipLeadSlip(User, commission);

            }
            catch (Exception e)
            {
                string message = "Commission could not be Updated. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Error while Edit TipLead slip", e, commission.ModifiedBy.ToString());
            }

            return false;
        }

        /// <summary>
        /// To call DeleteCommission method with session id
        /// </summary>
        /// <param name="Commission"></param>
        /// <returns></returns>
        [HttpPost]
        public bool DeleteCommission(CommissionComponent commission)
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
            return DeleteCommission(sessionid, commission);
        }

        /// <summary>
        /// To delete the commission details
        /// </summary>
        /// <param name="s">session id</param>
        /// <param name="commission">commission component</param>
        /// <returns></returns>
        [HttpPost]
        public bool DeleteCommission(string s, CommissionComponent commission)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                return servicelibrary.DeleteCommissionComponant(User, commission);

            }
            catch (Exception e)
            {
                string message = "Commission could not be Deleted. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Error while Edit Commission Slip", e, commission.ModifiedBy.ToString());
            }

            return false;
        }
        /// <summary>
        /// To call GetDeactiveGMandPlanID method with session id
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public int[] GetDeactiveGMandPlanID(EmployeeComponant employee)
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
            return GetDeactiveGMandPlanID(sessionid, employee);
        }

        /// <summary>
        /// To get deactivated GM and Pay PlanID for Employee
        /// </summary>
        /// <param name="s"></param>
        /// <param name="Reportparameter"></param>
        /// <returns></returns>
        [HttpPost]
        public int[] GetDeactiveGMandPlanID(string s, EmployeeComponant employee)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                return servicelibrary.GetDeactiveGMandPlanID(User, employee);
            }
            catch (Exception e)
            {
                string message = "Get deactivated GM and Pay PlanID could not be retrieved. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Get deactivated GM and Pay PlanID could not be retrieved", e);
            }

            return null;
        }

        /// <summary>
        /// To create Datetable for Commission Comments.
        /// </summary>
        /// <param name="comments"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable CreateDatatable(CommissionComponent comments, DataTable dt)
        {
            DataRow dr = dt.NewRow();
            dr["Comments"] = comments.Comments;
            dr["CommentedBy"] = comments.CommentedBy;
            dr["Date"] = comments.CreatedOn;
            dt.Rows.Add(dr);
            return dt;
        }

        /// <summary>
        /// To convert datatable to xml
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string ConvertDatatableToXML(DataTable dt)
        {
            MemoryStream str = new MemoryStream();
            dt.WriteXml(str, true);
            str.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(str);
            string xmlstr;
            xmlstr = sr.ReadToEnd();
            return (xmlstr);
        }
    }
}
