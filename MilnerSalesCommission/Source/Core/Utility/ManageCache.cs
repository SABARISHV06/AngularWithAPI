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
using System.Timers;

using System.Reflection;

namespace Utility
{
    /// <summary>
    /// A collection of objects with a sliding expiration max lifetime.  Removed items are disposed automatically unless
    /// a custom handler is provided to manage removed objects.
    /// </summary>
    public class ManageCache : IDisposable
    {
        #region Public Properties

        /// <summary>
        /// Delegate used to define the signature of a handler for removed items.
        /// </summary>
        public delegate void TRemovedHandler(object obj);

        /// <summary>
        /// The handler called when an item is removed from the cache.
        /// </summary>
        public TRemovedHandler RemovedHandler { get; set; }

        #endregion

        #region Private Properties

        /// <summary>
        /// This object has been disposed.
        /// </summary>
        private bool m_Disposed = false;

        /// <summary>
        /// Mutex guarantee that only a single thread accesses the cache at the same time.
        /// </summary>
        private object m_Mutex = new object();

        /// <summary>
        /// The threads that cull expired objects.
        /// </summary>
        private static ExpireMgr s_ExpireMgr = new ExpireMgr();

        /// <summary>
        /// The collection of cached objects.
        /// </summary>
        private Dictionary<string, Item> m_CachedObjects = new Dictionary<string, Item>();
        
        #endregion

        #region Constructors

        public ManageCache()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Add or update an item in the cache.
        /// </summary>
        /// <param name="key">The key for the new item.</param>
        /// <param name="userObj">The value for the key.</param>
        /// <param name="timeout">The maximum lifetime of this object, in minutes.  0 means forever.</param>
        public void Add(string key, object userObj, int timeout = 0)
        {
            object oldUserObj = null;

            lock (m_Mutex)
            {
                if (m_CachedObjects.ContainsKey(key))
                {
                    oldUserObj = m_CachedObjects[key].Value;
                    m_CachedObjects[key].Value = userObj;
                }
                else
                {
                    Item item = new Item(userObj, timeout);
                    m_CachedObjects.Add(key, item);
                    if (m_CachedObjects.Count == 1)
                    {
                        s_ExpireMgr.Register(ExpireItems);
                    }
                }
            }

            try
            {
                if (oldUserObj == null)
                {
                    return;
                }

                // Manage removal or disposal of a replaced object.
                if (RemovedHandler != null)
                {
                    RemovedHandler(oldUserObj);
                }
                else
                {
                    if (oldUserObj.GetType().GetInterfaces().Contains(typeof(IDisposable)))
                    {
                        try
                        {
                            MethodInfo m = oldUserObj.GetType().GetMethod("Dispose", BindingFlags.Public | BindingFlags.Instance, null, new System.Type[] { }, null);
                            m.Invoke(oldUserObj, null);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Get an item from the cache.
        /// </summary>
        /// <param name="key">The key to the item.</param>
        /// <param name="userObj">The user object for the key, or null if the object doesn't exist.</param>
        public void Get(string key, out object userObj)
        {
            userObj = null;

            lock (m_Mutex)
            {
                if (m_CachedObjects.ContainsKey(key))
                {
                    userObj = m_CachedObjects[key].Value;
                }
            }
        }

        /// <summary>
        /// Indicate that the item is still active.
        /// </summary>
        /// <param name="key">The key to the item.</param>
        public void Strobe(string key)
        {
            lock (m_Mutex)
            {
                if (m_CachedObjects.ContainsKey(key))
                {
                    m_CachedObjects[key].Strobe();
                }
            }
        }

        /// <summary>
        /// Get the max lifetime of an item from the cache.
        /// </summary>
        /// <param name="key">The key to the item.</param>
        public int GetTimeout(string key)
        {
            lock (m_Mutex)
            {
                if (m_CachedObjects.ContainsKey(key))
                {
                    return m_CachedObjects[key].Timeout;
                }
            }

            return 0;
        }

        /// <summary>
        /// Remove an item from the cache.  If the object is IDisposable then it is disposed unless a custom remove handler has been installed.
        /// </summary>
        /// <param name="key">The key to the item.</param>
        public void Remove(string key)
        {
            object userObj = null;

            lock (m_Mutex)
            {
                userObj = RemoveAndGet(key);
            }

            try
            {
                if (userObj != null)
                {
                    if (RemovedHandler != null)
                    {
                        RemovedHandler(userObj);
                    }
                    else
                    {
                        if (userObj.GetType().GetInterfaces().Contains(typeof(IDisposable)))
                        {
                            try
                            {
                                MethodInfo m = userObj.GetType().GetMethod("Dispose", BindingFlags.Public | BindingFlags.Instance, null, new System.Type[] { }, null);
                                m.Invoke(userObj, null);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Remove an item from the cache and return the user-defined object.
        /// </summary>
        /// <param name="key">The user-supplied key for the item.</param>
        /// <returns>null if the item doesn't exist, or the user's object.</returns>
        private object RemoveAndGet(string key)
        {
            object userObj = null;
            if (m_CachedObjects.ContainsKey(key))
            {
                userObj = m_CachedObjects[key].Value;
                m_CachedObjects.Remove(key);

                if (m_CachedObjects.Count == 0)
                {
                    s_ExpireMgr.Unregister(ExpireItems);
                }
            }

            return userObj;
        }

        /// <summary>
        /// The callback that expires stale items for this store.
        /// </summary>
        private bool ExpireItems()
        {
            bool empty = false;

            try
            {
                List<object> expiredItems = new List<object>();

                lock (m_Mutex)
                {
                    foreach (string key in m_CachedObjects.Keys.ToList())
                    {
                        Item item = m_CachedObjects[key];
                        if (item.IsExpired())
                        {
                            try
                            {
                                if (item.Value != null)
                                {
                                    expiredItems.Add(item.Value);
                                }

                                m_CachedObjects.Remove(key);
                            }
                            catch
                            {
                            }
                        }
                    }

                    empty = (m_CachedObjects.Count == 0);
                }

                try
                {
                    if (RemovedHandler != null)
                    {
                        foreach (object userObj in expiredItems)
                        {
                            RemovedHandler(userObj);
                        }
                    }
                    else
                    {
                        foreach (object userObj in expiredItems)
                        {
                            if (userObj.GetType().GetInterfaces().Contains(typeof(IDisposable)))
                            {
                                try
                                {
                                    MethodInfo m = userObj.GetType().GetMethod("Dispose", BindingFlags.Public | BindingFlags.Instance, null, new System.Type[]{}, null);
                                    m.Invoke(userObj, null);
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
                catch
                {
                }

                expiredItems.Clear();
            }
            catch
            {
            }

            return empty;
        }

        #endregion

        /// <summary>
        /// An element stored in the cache.
        /// </summary>
        private class Item
        {
            /// <summary>
            /// The most recent access time.
            /// </summary>
            private DateTime m_LastAccess = DateTime.Now;

            /// <summary>
            /// The number of minutes the object may remain unused.
            /// </summary>
            private TimeSpan? m_MaxLife = null;

            /// <summary>
            /// The user-defined object they stored in the cache.
            /// </summary>
            private object m_Object = null;

            /// <summary>
            /// Get the sliding lifetime of an object, as minutes.
            /// </summary>
            public int Timeout
            {
                get
                {
                    if (m_MaxLife == null)
                    {
                        return 0;
                    }

                    return (int)(((TimeSpan)m_MaxLife).TotalMinutes);
                }
            }

            /// <summary>
            /// Get or set the user-defined object they stored in the cache.
            /// </summary>
            public object Value
            {
                get
                {
                    m_LastAccess = DateTime.Now;
                    return m_Object;
                }

                set
                {
                    m_LastAccess = DateTime.Now;
                    m_Object = value;
                }
            }

            /// <summary>
            /// Indicate that the item was just used by restarting the timer.
            /// </summary>
            public void Strobe()
            {
                m_LastAccess = DateTime.Now;
            }

            /// <summary>
            /// Wrap a user-defined cache object.
            /// </summary>
            /// <param name="obj">The user's object.</param>
            /// <param name="timeout">The maximum lifetime of this object, in minutes.  0 means forever.</param>
            public Item(object obj, int timeout)
            {
                Value = obj;
                if (timeout > 0)
                {
                    m_MaxLife = TimeSpan.FromMinutes(timeout);
                }
            }

            /// <summary>
            /// When true, the object has not been accessed within the timeout period.
            /// </summary>
            /// <returns>false if the object has been used more recently than its max lifetime.</returns>
            public bool IsExpired()
            {
                if (m_MaxLife != null)
                {
                    DateTime t = m_LastAccess.Add((TimeSpan)m_MaxLife);
                    return (t < DateTime.Now);
                }

                return false;
            }
        }

        /// <summary>
        /// A threaded activity for invoking SessionStore callbacks which expire stale items.
        /// </summary>
        private class ExpireMgr : IDisposable
        {
            #region Public Properties

            /// <summary>
            /// Delegate used to define the signature of a handler for expiring items.  Handler must return true
            /// if the collection being expired becomes empty.
            /// </summary>
            public delegate bool TExpireHandler();

            #endregion

            #region Private Properties

            /// <summary>
            /// This object has been disposed.
            /// </summary>
            private bool m_Disposed = false;

            /// <summary>
            /// The list of store handlers called to check for outdated items.
            /// </summary>
            private List<TExpireHandler> m_ExpireHandlers = new List<TExpireHandler>();

            /// <summary>
            /// Protect concurrent access to the expiration callback list.
            /// </summary>
            private object m_HandlerListMutex = new object();
            
            /// <summary>
            /// Thread controlling the polling rate of the expire activity.
            /// </summary>
            private System.Timers.Timer m_ExpireTimer = new System.Timers.Timer();

            #endregion

            #region Public Methods

            public ExpireMgr()
            {
                m_ExpireTimer.Enabled = false;
                m_ExpireTimer.AutoReset = false;
                m_ExpireTimer.Interval = 60000;
                m_ExpireTimer.Elapsed += new ElapsedEventHandler(OnElapsed_ExpireTimer);
            }

            /// <summary>
            /// Add a new MTISessionStore to the object expiration activity.
            /// </summary>
            /// <param name="h">The store on which expires will be performed.</param>
            public void Register(TExpireHandler h)
            {
                lock (m_HandlerListMutex)
                {
                    m_ExpireHandlers.Add(h);

                    // If this is the first store then start expiration activity.
                    if (m_ExpireHandlers.Count == 1)
                    {
                        m_ExpireTimer.Enabled = true;
                    }
                }
            }

            /// <summary>
            /// Remove an MTISessionStore to the object expiration activity.
            /// </summary>
            /// <param name="h">The store on which expires were performed.</param>
            public void Unregister(TExpireHandler h)
            {
                lock (m_HandlerListMutex)
                {
                    if (m_ExpireHandlers.Remove(h))
                    {
                        // If this is the first store then start expiration activity.
                        if (m_ExpireHandlers.Count == 0)
                        {
                            m_ExpireTimer.Enabled = false;
                        }
                    }
                }
            }

            /// <summary>
            /// Method called by the expiration thread to call the list of expiration callbacks.  It is timer driven.
            /// </summary>
            /// <param name="sender">Not used.</param>
            /// <param name="e">Not used.</param>
            private void OnElapsed_ExpireTimer(object sender, ElapsedEventArgs e)
            {
                lock (m_HandlerListMutex)
                {
                    foreach (TExpireHandler h in m_ExpireHandlers.ToList())
                    {
                        if (h())
                        {
                            m_ExpireHandlers.Remove(h);
                        }
                    }

                    if (m_ExpireHandlers.Count > 0)
                    {
                        m_ExpireTimer.Enabled = true;
                    }
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
                        try
                        {
                            m_ExpireTimer.Dispose();
                        }
                        catch
                        {
                        }
                    }

                    // Call the appropriate methods to clean up 
                    // unmanaged resources here.  If disposing is false, only the following code is executed.

                    // Note disposing has been done.
                    m_Disposed = true;

                }
            }

            #endregion
        }

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
                    try
                    {
                        s_ExpireMgr.Unregister(ExpireItems);
                    }
                    catch
                    {
                    }

                    if (RemovedHandler != null)
                    {
                        foreach (Item item in m_CachedObjects.Values)
                        {
                            if (item.Value != null)
                            {
                                try
                                {
                                    RemovedHandler(item.Value);
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (object userObj in m_CachedObjects.Values)
                        {
                            if (userObj.GetType().GetInterfaces().Contains(typeof(IDisposable)))
                            {
                                try
                                {
                                    MethodInfo m = userObj.GetType().GetMethod("Dispose", BindingFlags.Public | BindingFlags.Instance, null, new System.Type[] { }, null);
                                    m.Invoke(userObj, null);
                                }
                                catch
                                {
                                }
                            }
                        }
                    }

                    m_CachedObjects.Clear();
                }

                // Call the appropriate methods to clean up 
                // unmanaged resources here.  If disposing is false, only the following code is executed.

                // Note disposing has been done.
                m_Disposed = true;

            }
        }

        #endregion
    }
}
