// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//


using System.Text;
using System.Security.Principal;
using Utility;

namespace ServiceLibrary
{
    /// <summary>
    /// Store service library into Memory cache
    /// </summary>
    public sealed class ServicelibraryCache
    {
        #region Public Properties and Methods
        /// <summary>
        /// The collection of Cache objects.
        /// </summary>
        private static ManageCache m_CacheStore = new ManageCache();

        /// <summary>
        /// The root of the key for retrieving a Cache's encrypted user name.
        /// </summary>
        private const string USER_ID_TOKEN = "user-token_ref";

        /// <summary>
        /// Combine Cache ID and a constant string to make a key for the cache.
        /// </summary>
        /// <param name="Cache">The Cache index to the cache.</param>
        /// <param name="key">An identifier for a thing needed from the cache.</param>
        /// <returns>A key to an element in the cache.</returns>
        private static string MakeToken(string Cache, string key)
        {
            StringBuilder sb = new StringBuilder(Cache);
            sb.Append(key);
            return sb.ToString();
        }

        /// <summary>
        /// Store user tokens in static dictionary for later retrieval if memory cache resets
        /// </summary>
        /// <param name="sessid">The ID of the Cache to which the credentials belong.</param>
        /// <param name="user_token">The credentials for the user.</param>
        public static void StoreUserToken(string sessid, string user_token)
        {
            m_CacheStore.Add(MakeToken(sessid, USER_ID_TOKEN), user_token, 0);
            }

        /// <summary>
        /// Retrieve user token stored for this Cache
        /// </summary>
        /// <param name="sessid">The ID of the Cache to which the credentials belong.</param>
        /// <returns>The credentials for the user, or null if they have expired from the cache.</returns>
        public static string GetUserToken(string sessid)
        {
            string usr_token = string.Empty;
            object obj;
            m_CacheStore.Get(MakeToken(sessid, USER_ID_TOKEN), out obj);

            if (obj != null)
            {
                    return (string)obj;
            }

            return string.Empty;
        }

        /// <summary>
        /// Get a GetRSAPIlibrary based on sessionid.  If Cache is not
        /// available, create new one.
        /// </summary>
        /// <param name="sessionid">The key to the current Cache.</param>
        /// <param name="user">The identity of the user requesting access to .</param>
        /// <param name="timeout">The Timeout property specifies the time-out period assigned to the Cache object for the application, in minutes. If the user does not use the Cache within the time-out period, the Cache ends.</param>
        /// <returns>The Cache object.</returns>
        public static Servicelibrary GetServicelibrary(string sessionid, IPrincipal user, int timeout = 0)
        {
            object obj;
            m_CacheStore.Get(sessionid, out obj);

            Servicelibrary rsAPIlibrary = obj as Servicelibrary;
            if (rsAPIlibrary != null)
            {
                return rsAPIlibrary;
            }

            if (m_CacheStore.RemovedHandler == null)
            {
                m_CacheStore.RemovedHandler = DisposeSessionsHandler;
            }

            rsAPIlibrary = new Servicelibrary(sessionid, user);
            m_CacheStore.Add(sessionid, rsAPIlibrary, timeout);

            return rsAPIlibrary;
        }

        /// <summary>
        /// Get a GetRSAPIlibrary instance. 
        /// </summary>
        /// <param name="sessionid">The key to the current Cache.</param>
        /// <param name="timeout">The Timeout property specifies the time-out period assigned to the Cache object for the application, in minutes. If the user does not use the Cache within the time-out period, the Cache ends.</param>
        /// <returns>The Cache object.</returns>
        public static Servicelibrary GetServicelibrary(string sessionid, int timeout = 0)
        {
            object obj;
            m_CacheStore.Get(sessionid, out obj);

            Servicelibrary rsAPIlibrary = obj as Servicelibrary;
            if (rsAPIlibrary != null)
            {
                return rsAPIlibrary;
            }

            if (m_CacheStore.RemovedHandler == null)
            {
                m_CacheStore.RemovedHandler = DisposeSessionsHandler;
            }

            rsAPIlibrary = new Servicelibrary(sessionid);
            m_CacheStore.Add(sessionid, rsAPIlibrary, timeout);

            return rsAPIlibrary;
        }

        /// <summary>
        /// Called when cache expires Cache objects, including the sessions themselves.
        /// </summary>
        /// <param name="obj">An object removed from the Cache store.</param>
        static void DisposeSessionsHandler(object obj)
        {
            try
            {
                Servicelibrary rsAPIlibrary = obj as Servicelibrary;
                if (rsAPIlibrary != null)
                {
                    // Only sessions should get this far.
                    rsAPIlibrary.Dispose();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Shutdown a single Cache.
        /// </summary>
        /// <param name="sessid"></param>
        public static void ShutDown(string sessid)
        {
            try
            {
                // Disposal is performed in a callback.
                m_CacheStore.Remove(sessid);
                m_CacheStore.Remove(sessid + USER_ID_TOKEN);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Prepare for application close by ending static services.
        /// </summary>
        public static void End()
        {
            if (m_CacheStore != null)
            {
                m_CacheStore.Dispose();
                m_CacheStore = null;
            }
        }
        #endregion
    }
}

