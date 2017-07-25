// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//

using ApiControllers.Common;
using System;
using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Utility;


namespace SalesCommission
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Define Web API 
        /// </summary>
        private const string m_WebApiPrefix = "api";
        /// <summary>
        /// Define Web Api Execution Path
        /// </summary>
        private static string m_WebApiExecutionPath = String.Format("~/{0}", m_WebApiPrefix);

        /// <summary>
        /// To get audit log to file flag
        /// </summary>
        private static readonly bool m_LogsToFileEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["LogsToFileEnabled"]);

        protected void Application_Start()
        {         

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            if (m_LogsToFileEnabled)
            {
                log4net.Config.XmlConfigurator.Configure();
            }
            IntializeJsonFormetters();
        }

        /// <summary>
        /// To check web api request
        /// </summary>
        /// <returns></returns>
        private static bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith(m_WebApiExecutionPath);
        }
        /// <summary>
        /// Initialize the Json Formetters
        /// </summary>
        private static void IntializeJsonFormetters()
        {
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings();
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

        }
        /// <summary>
        /// Handle the start of a new session.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        protected void Session_Start(Object sender, EventArgs e)
        {
            Session["a"] = 1; // This is a dummy session variable required to trigger creation of cookies.
        }

        /// <summary>
        /// A session is ending.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        protected void Session_End(object sender, EventArgs e)
        {
            try
            {
                ServiceClientProxy.ShutDown(Session.SessionID);
            }
            catch
            {
            }
        }
        /// <summary>
        /// This application is ending.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        protected void Application_End(object sender, EventArgs e)
        {
            ServiceClientProxy.End();
        }

        /// <summary>
        /// Report unhandled exceptions.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            UtilityLog.EventLogException("Sales Commission", "A problem occurred with WEB API and throw the following exception.", ex);
        }
    }
}