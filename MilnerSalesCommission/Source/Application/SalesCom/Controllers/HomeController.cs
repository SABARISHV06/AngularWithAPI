// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Comsquared Systems, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Comsquared Systems, Inc., without the express
// written consent of an officer of the corporation.


using System.Web.Mvc;

namespace SalesCommission.Controllers
{
    public sealed class HomeController : Controller
    {
        /// <summary>
        /// Report unhandled exceptions.
        /// </summary>
        /// <param name="filterContext">The exception details.</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            Utility.UtilityLog.EventLogException("SalesCommission", "A problem occurred in the REST API home controller.  The technical details follow.", filterContext.Exception);
        }

        /// <summary>
        /// // GET: /Home/
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
