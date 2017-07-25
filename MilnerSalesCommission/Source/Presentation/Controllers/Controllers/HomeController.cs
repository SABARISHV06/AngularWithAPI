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

namespace ApiControllers.Controllers
{
    /// <summary>
    /// To handle home page related actions and behaiours
    /// </summary>
    public sealed class HomeController : ApiBaseController
    {
        
        /// <summary>
        /// Returns the timeout for an activity that is a percentage of the session timeout. 
        /// </summary>
        /// <returns></returns>
        public int GetSessionTimeout()
        {
            string sessionID = null;

            try
            {
                sessionID = Request.Properties[Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionID == null)
            {
                sessionID = System.Web.HttpContext.Current.Session.SessionID;
            }

            return GetSessionTimeout(sessionID);
        }

        /// <summary>
        /// Returns the timeout for an activity that is a percentage of the session timeout. 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int GetSessionTimeout(string s)
        {
            try
            {
                return Convert.ToInt16(Math.Ceiling(System.Web.HttpContext.Current.Session.Timeout * .75));
            }
            catch (Exception ex)
            {
                string message = "Failed to Get Session Timeout." + ex.Message;
                OnHttpResponseMessage(message);
            }

            return 0;
        }

        /// <summary>
        /// Perform keep-alive on this session. 
        /// </summary>
        [HttpGet]
        public void KeepAlive()
        {
            string sessionID = null;

            try
            {
                sessionID = Request.Properties[Handlers.ApiSessionHandler.SessionIdToken] as string;
            }
            catch
            {
            }

            if (sessionID == null)
            {
                sessionID = System.Web.HttpContext.Current.Session.SessionID;
            }

            KeepAlive(sessionID);
        }

        /// <summary>
        /// Perform keep-alive on a session.
        /// </summary>
        /// <param name="s">The ID of the current session</param>
        [HttpGet]
        public void KeepAlive(string s)
        {
            try
            {
               Servicelibrary servicelib = ServiceLibrary.ServicelibraryCache.GetServicelibrary(s, User, System.Web.HttpContext.Current.Session.Timeout);
               servicelib.KeepAlive();
            }
            catch (Exception ex)
            {
                string message = "Failed to keep session alive." + ex.Message;
                OnHttpResponseMessage(message);
            }
        }
        /// <summary>
        /// Log Out from application
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public void LogOut()
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

            LogOut(sessionid);
        }

        /// <summary>
        /// Log out
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public void LogOut(string s)
        {
            try
            {
                ServicelibraryCache.ShutDown(s);
                HttpContext.Current.Session.Abandon();
            }
            catch (Exception e)
            {
                string message = "Failed to LogOut. " + e.Message;
                OnHttpResponseMessage(message);
            }
        }
    }
}
