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
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Utility;

namespace ApiControllers.Common
{
    /// <summary>
    /// Service client Proxy to invoke ServiceLibraryProxy methods
    /// </summary>
    public sealed class ServiceClientProxy
    {
        private static ManageCache m_mangeCache = new ManageCache();
        /// <summary>
        /// Assign Proxy name
        /// </summary>
        private const string PROXY_REF_NAME = "My RSAPI Proxy";

        /// <summary>
        /// Assign mutex
        /// </summary>
        private static ManageMutex m_SessionMutex = new ManageMutex(1);

        /// <summary>
        /// Get a GetServiceLibraryProxy based on sessionid.  If Cache is not available, create new one.
        /// </summary>
        /// <returns></returns>
        public static Servicelibrary GetServiceLibraryProxy(IPrincipal user)
        {
            Servicelibrary rsProxy = null;
            m_SessionMutex.Lock();
            try
            {

                rsProxy = (Servicelibrary)System.Web.HttpContext.Current.Session[PROXY_REF_NAME];
                if (rsProxy == null)
                {
                    rsProxy = ServicelibraryCache.GetServicelibrary(System.Web.HttpContext.Current.Session.SessionID, user);
                    System.Web.HttpContext.Current.Session[PROXY_REF_NAME] = rsProxy;
                }
            }
            catch
            {
            }
            finally
            {
                m_SessionMutex.Unlock();
            }
            return rsProxy;
        }

        public static Servicelibrary GetServiceLibraryProxy(string sessionid,IPrincipal user)
        {
            Servicelibrary rsProxy = null;
            m_SessionMutex.Lock();
            try
            {
                object obj;
                m_mangeCache.Get(sessionid, out obj);
                rsProxy = (Servicelibrary)obj;
                if (rsProxy == null)
                {
                    rsProxy = ServicelibraryCache.GetServicelibrary(sessionid, user);
                    m_mangeCache.Add(sessionid, rsProxy);
                }
            }
            catch
            {
            }
            finally
            {
                m_SessionMutex.Unlock();
            }
            return rsProxy;
        }

        public static void ShutDown(string sessionId)
        {
            ServicelibraryCache.ShutDown(sessionId);
        }
        public static void End()
        {
            ServicelibraryCache.End();
        }
    }
}
