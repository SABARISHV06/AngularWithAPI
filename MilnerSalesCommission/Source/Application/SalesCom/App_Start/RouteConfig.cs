// Copyright 2015, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//
using System.Web.Mvc;
using System.Web.Routing;

namespace SalesCommission
{
    /// <summary>
    /// To handle any routing configurations.
    /// </summary>
    public sealed class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "Default.aspx",
                defaults: ""
            );
        }
    }
}