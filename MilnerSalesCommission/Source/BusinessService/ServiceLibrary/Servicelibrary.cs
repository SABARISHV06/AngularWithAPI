// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//

using System;
using System.Security.Principal;
using System.Runtime.Caching;
using Utility;
using GlobalConfig;
using Security;

namespace ServiceLibrary
{
    /// <summary>
    /// Rest Service API access Library
    /// </summary>
    public partial class Servicelibrary : IDisposable
    {
        #region Public Properties
        /// <summary>
        /// This object has been disposed.
        /// </summary>
        private bool m_Disposed = false;
        /// <summary>
        /// Objects private to this session.
        /// </summary>
        private ManageCache m_CacheObjects = new ManageCache();
        /// <summary>
        /// Assign global configuration settings
        /// </summary>
        private GlobalConfiguration m_GlobalConfig = new GlobalConfiguration();
        /// <summary>
        /// Public access to Caching. Objects may expire unexpectedly from caches.  Use this store only for
        /// performance caching.
        /// </summary>
        private System.Runtime.Caching.MemoryCache MySession { get; set; }
        /// <summary>
        /// Delegate used to define the session keep alive of a handler for reports the access.
        /// </summary>
        /// <param name="sessionID"></param>
        public delegate void TKeepAliveHandler(string sessionID);
        /// <summary>
        /// The handler called when an accessed.
        /// </summary>
        public TKeepAliveHandler KeepAliveHandler { get; set; }
        /// <summary>
        /// Set default Cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public static void SetInDefaultCache(string key, object obj)
        {
            MemoryCache.Default[key] = obj;
        }
        /// <summary>
        /// To get default Cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetFromDefaultCache(string key)
        {
            return MemoryCache.Default[key];
        }
        /// <summary>
        /// Init Session ID
        /// </summary>
        private string m_SessionID = "";
        /// <summary>
        /// To get Session ID
        /// </summary>
        public string SessionID
        {
            get
            {
                return m_SessionID;
            }
        }
        /// <summary>
        /// Init authentication key
        /// </summary>
        private string m_AuthenticateKey = "";
        /// <summary>
        /// To get Authentication Key
        /// </summary>
        public string AuthenticateKey
        {
            get
            {
                return m_AuthenticateKey;
            }
        }

        /// <summary>
        /// Access Global Configurations
        /// </summary>
        private GlobalConfiguration GlobalConfig
        {
            get
            {
                m_GlobalConfig.LoadGlobalSettings();
                return m_GlobalConfig;
            }
        }

        private UserIdentity m_MyUserIdentity = null;

        /// <summary>
        /// To get User Identity
        /// </summary>
        public UserIdentity UserIdentity
        {
            get
            {
                return m_MyUserIdentity;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Use the specified user for authorization.
        /// </summary>
        /// <param name="sessionid">The current session's key.</param>
        /// <param name="user">The name of the user requesting access to  content.</param>
        public Servicelibrary(string sessionid, IPrincipal user)
        {
            m_MyUserIdentity = new UserIdentity(user);
            Initialize(sessionid, m_MyUserIdentity.Name);

        }
        /// <summary>
        /// Use the specified user for authorization.
        /// </summary>
        /// <param name="sessionid">The current session's key.</param>
        /// <param name="user">The name of the user requesting access to  content.</param>
        public Servicelibrary(string sessionid)
        {
            m_MyUserIdentity = new UserIdentity();
            Initialize(sessionid, m_MyUserIdentity.Name);
        }

        /// <summary>
        /// To initialize rest API library services
        /// </summary>
        /// <param name="sessionid"></param>
        private void Initialize(string sessionid, string user)
        {
            m_SessionID = sessionid;
            MySession = new MemoryCache(sessionid, null);
            MemoryCache.Default["MySession" + sessionid] = MySession;

            try
            {
                m_GlobalConfig = new GlobalConfiguration();
                m_GlobalConfig.LoadGlobalSettings();
            }
            catch (Exception ex)
            {
                UtilityLog.EventLogException("SalesCommission", "Failed during the Initialize.", ex);
            }
        }
        /// <summary>
        /// To Store bytes in session
        /// </summary>
        /// <param name="name"></param>
        /// <param name="bytes"></param>
        public void SetBytesInSession(string name, byte[] bytes)
        {
            MySession[name] = bytes;
        }
        /// <summary>
        /// To get bytes in session
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public byte[] GetBytesFromSession(string name)
        {
            return (byte[])MySession[name];
        }
        /// <summary>
        /// Set Session Keep alive
        /// </summary>
        public void KeepAlive()
        {
            if (KeepAliveHandler != null && !string.IsNullOrEmpty(SessionID))
            {
                KeepAliveHandler(SessionID);
            }
        }

        #endregion

        #region IDisposable Pattern

        /// <summary>
        /// Implement IDisposable. Only call this on application exit.
        /// Do not make this method virtual. 
        /// A derived class should not be able to override this method. 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method. 
            // Therefore, you should call GC.SupressFinalize to 
            // take this object off the finalization queue 
            // and prevent finalization code for this object 
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implement idisposable. 
        /// </summary>
        /// <param name="disposing">If disposing equals true, the method has been called directly 
        /// or indirectly by a user's code. Managed and unmanaged resources 
        /// can be disposed. 
        /// If disposing equals false, the method has been called by the 
        /// runtime from inside the finalizer and you should not reference 
        /// other objects. Only unmanaged resources can be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called. 
            if (!this.m_Disposed)
            {
                // If disposing equals true, dispose all managed 
                // and unmanaged resources. 
                if (disposing)
                {
                    // Dispose managed resources here.

                    MySession.Dispose();


                    m_CacheObjects.Dispose();
                }

                // Note disposing has been done.
                m_Disposed = true;
            }
        }

        #endregion

    }

}