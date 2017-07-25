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
using ServiceLibrary;

namespace ApiControllers.Controllers
{
    /// <summary>
    /// To handle new plan componant controllers related actions and behaiours
    /// </summary>
    public sealed class NewPlanComponentController : ApiBaseController
    {
        /// <summary>
        /// To call GetPlans method with session ID.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<PlanComponent> GetPlans()
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
            return GetPlans(sessionid);
        }

        /// <summary>
        /// To Get All Plans
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpGet]
        public List<PlanComponent> GetPlans(string s)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                List<PlanComponent> plans = servicelibrary.GetAllPlans(User);

                //Utility.AuditLogHelper.InfoLogMessage("Retrieved all Plan details");

                return plans;
            }
            catch (Exception e)
            {
                string message = "Get Plans could not be retrieved. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Get Plans could not be retrieved.", e);
            }

            return null;
        }

        /// <summary>
        /// To call GetPlanbyID method with session ID.
        /// </summary>
        /// <param name="Plan"></param>
        /// <returns></returns>

        [HttpPost]
        public PlanComponent GetPlanbyID( PlanComponent Plan)
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
            return GetPlanbyID(sessionid, Plan);
        }

        /// <summary>
        /// To Get All Plans by ID
        /// </summary>
        /// <param name="s"></param>
        /// <param name="plan"></param>
        /// <returns></returns>
        [HttpPost]
        public PlanComponent GetPlanbyID(string s, PlanComponent plan)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                PlanComponent plandetail = servicelibrary.GetPlanbyID(User, plan);

                //Utility.AuditLogHelper.InfoLogMessage("Retrieved Plan details by ID");

                return plandetail;
            }
            catch (Exception e)
            {
                string message = "Get Plan by ID could not be retrieved. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Get Plan by ID could not be retrieved.", e);
            }

            return null;
        }
        /// <summary>
        /// To call AddPlan method with session ID.
        /// </summary>
        /// <param name="Plan"></param>
        /// <returns></returns>
        [HttpPost]
        public int AddPlan(PlanComponent Plan)
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
            return AddPlan(sessionid, Plan);//collection.add(object)
        }

       /// <summary>
       /// To Create New plan details
       /// </summary>
       /// <param name="s"></param>
       /// <param name="Plan"></param>
       /// <returns></returns>
        [HttpPost]
        public int AddPlan(string s, PlanComponent Plan)
        {
            try
            {
                TGPCustomerInfoCollection TGPcollection = new TGPCustomerInfoCollection();
                TenureBonusCollection Tenurecollection = new TenureBonusCollection();
                BiMonthlyBonusCollection Bimonthlycollection = new BiMonthlyBonusCollection();

                foreach(TGPCustomerInfo TGPinfo in Plan.TGPcustomerlist)
                {
                    TGPcollection.Add(TGPinfo);
                }
                foreach(BIMonthlyBonusInfo Bimonthly in Plan.Bimonthlylist)
                {
                    Bimonthlycollection.Add(Bimonthly);
                }
                foreach(TenureBonus Tenure in Plan.TenureBonuslist)
                {
                    Tenurecollection.Add(Tenure);
                }
                
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                return servicelibrary.AddPlan(User, Plan,TGPcollection, Tenurecollection, Bimonthlycollection);
             }
            catch (Exception e)
            {
                string message = "Plan details could not be Inserted. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Error while Pay plan creation", e, Plan.CreatedBy.ToString());
            }

            return 0;
        }

        /// <summary>
        /// To call EditPlan method with session ID.
        /// </summary>
        /// <param name="Plan"></param>
        /// <returns></returns>
        [HttpPost]
        public bool EditPlan(PlanComponent Plan)
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
           return EditPlan(sessionid, Plan);
        }

        /// <summary>
        /// To Edit Plan details
        /// </summary>
        /// <param name="s"></param>
        /// <param name="Plan"></param>
        /// <returns></returns>
        [HttpPost]
        public bool EditPlan(string s, PlanComponent Plan)
        {
            try
            {
                TGPCustomerInfoCollection TGPcollection = new TGPCustomerInfoCollection();
                TenureBonusCollection Tenurecollection = new TenureBonusCollection();
                BiMonthlyBonusCollection Bimonthlycollection = new BiMonthlyBonusCollection();

                foreach (TGPCustomerInfo TGPinfo in Plan.TGPcustomerlist)
                {
                    TGPcollection.Add(TGPinfo);
                }
                foreach (BIMonthlyBonusInfo Bimonthly in Plan.Bimonthlylist)
                {
                    Bimonthlycollection.Add(Bimonthly);
                }
                foreach (TenureBonus Tenure in Plan.TenureBonuslist)
                {
                    Tenurecollection.Add(Tenure);
                }
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                return servicelibrary.EditPlan(User, Plan, TGPcollection, Tenurecollection, Bimonthlycollection);
            }
            catch (Exception e)
            {
                string message = "Plan details could not be Updated. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Error while Edit Pay plan", e, Plan.ModifiedBy.ToString());
            }

            return false;
        }
        /// <summary>
        /// To call DeletePlan method with session ID.
        /// </summary>
        /// <param name="Plan"></param>
        /// <returns></returns>
        [HttpPost]
        public bool DeletePlan(PlanComponent Plan)
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
           return DeletePlan(sessionid, Plan);
        }

    /// <summary>
    /// To Delete Plan details by PlanID
    /// </summary>
    /// <param name="s"></param>
    /// <param name="Plan"></param>
    /// <returns></returns>
        [HttpPost]
        public bool DeletePlan(string s, PlanComponent Plan)
        {
            
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                return servicelibrary.DeletePlan(User, Plan);
            }
            catch (Exception e)
            {
                string message = "Plan details could not be Deleted. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Error while activate/deactivate Pay plan.", e, Plan.ModifiedBy.ToString());
            }

            return false;
        }

        /// <summary>
        /// To call GetAllTGP method with session ID.
        /// </summary>
        /// <param name="Plan"></param>
        /// <returns></returns>

        [HttpPost]
        public List<TGPCustomerInfo> GetAllTGP(PlanComponent Plan)
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
            return GetAllTGP(sessionid, Plan);
        }

        /// <summary>
        /// To Get All Target Gross Profit by PlanID
        /// </summary>
        /// <param name="s"></param>
        /// <param name="plan"></param>
        /// <returns></returns>
        [HttpPost]
        public List<TGPCustomerInfo> GetAllTGP(string s, PlanComponent plan)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                List<TGPCustomerInfo> TGPs = servicelibrary.GetAllTGP(User, plan);

                return TGPs;
            }
            catch (Exception e)
            {
                string message = "Get Target Gross Profit by PlanID could not be executed. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Get Target Gross Profit by PlanID could not be retrieved.", e);
            }

            return null;
        }
        /// <summary>
        /// To call GetAllBimonthly method with session ID.
        /// </summary>
        /// <param name="Plan"></param>
        /// <returns></returns>

        [HttpPost]
        public List<BIMonthlyBonusInfo> GetAllBimonthly(PlanComponent Plan)
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
            return GetAllBimonthly(sessionid, Plan);
        }

        /// <summary>
        /// To Get All BiMonthlyBonus Info by PlanID
        /// </summary>
        /// <param name="s"></param>
        /// <param name="plan"></param>
        /// <returns></returns>
        [HttpPost]
        public List<BIMonthlyBonusInfo> GetAllBimonthly(string s, PlanComponent plan)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                List<BIMonthlyBonusInfo> Bimonthlyinfo = servicelibrary.GetAllBimonthlyBonus(User, plan);

                return Bimonthlyinfo;
            }
            catch (Exception e)
            {
                string message = "Get Bimonthly Bonus Info by PlanID could not be executed. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Get Bimonthly Bonus Info by PlanID could not be retrieved.", e);
            }

            return null;
        }
        /// <summary>
        /// To Get All Tenure Bonus by PlanID
        /// </summary>
        /// <param name="Plan"></param>
        /// <returns></returns>

        [HttpPost]
        public List<TenureBonus> GetAllTenure(PlanComponent Plan)
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
            return GetAllTenure(sessionid, Plan);
        }

        /// <summary>
        /// To Get All Tenure Bonus by PlanID
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpPost]
        public List<TenureBonus> GetAllTenure(string s, PlanComponent plan)
        {
            try
            {
                Servicelibrary servicelibrary = ServicelibraryCache.GetServicelibrary(s, User, HttpContext.Current.Session.Timeout);

                List<TenureBonus> Tenure = servicelibrary.GetAllTenureBonus(User, plan);

                return Tenure;
            }
            catch (Exception e)
            {
                string message = "Get Tenure Bonus by PlanID could not be executed. " + e.Message;
                OnHttpResponseMessage(message);
                Utility.AuditLogHelper.ErrorLog("Get Tenure Bonus by PlanID could not be retrieved.", e);
            }

            return null;
        }
    }
}
