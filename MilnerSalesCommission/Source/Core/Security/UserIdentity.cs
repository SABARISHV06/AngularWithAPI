// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//


using System;
using System.Configuration;
using System.Security.Principal;
using System.Text;

namespace Security
{
    /// <summary>
    /// To maintain the user identity.
    /// </summary>
    public sealed class UserIdentity : IDisposable
    {
        #region Private Properties
        /// <summary>
        /// Set Windows Identity
        /// </summary>
        private WindowsIdentity m_MyUserIdentity = null;
        /// <summary>
        /// Set Windows Identity Pricipal
        /// </summary>
        private WindowsPrincipal m_MyUserPrincipal = null;
        /// <summary>
        ///  Set  Pricipal
        /// </summary>
        private IPrincipal m_MyUserHandle = null;
        /// <summary>
        /// The name of the user.
        /// </summary>
        private string m_Name = string.Empty;
        /// <summary>
        /// When true object has been disposed.
        /// </summary>
        private bool m_Disposed = false;
        /// <summary>
        /// When true the identity lifetime is controlled by this instance.
        /// </summary>
        private bool m_InstanceManaged;

        #endregion

        /// <summary>
        /// Get the handle to the user information.
        /// </summary>
        private IPrincipal Principal
        {
            get { return m_MyUserHandle; }
        }

        /// <summary>
        /// Gets the value that indicates whether the user is authenticated.
        /// </summary>
        private bool IsAuthenticated { get; set; }

        /// <summary>
        /// Create a managed identity.
        /// </summary>
        public UserIdentity()
        {
            m_InstanceManaged = true;
            try
            {
                m_MyUserIdentity = WindowsIdentity.GetCurrent();
                m_Name = m_MyUserIdentity.Name;
                m_MyUserPrincipal = new WindowsPrincipal((WindowsIdentity)m_MyUserIdentity);
                m_MyUserHandle = (IPrincipal)m_MyUserPrincipal;
                IsAuthenticated = m_MyUserHandle.Identity.IsAuthenticated;
            }
            catch
            {
            }
        }

        /// <summary>
        /// Wrap an unmanaged identity.
        /// </summary>
        /// <param name="user">The user's identity.</param>
        public UserIdentity(IPrincipal user)
        {
            m_InstanceManaged = false;

            if (String.IsNullOrEmpty(user.Identity.Name))
            {
                m_MyUserIdentity = WindowsIdentity.GetCurrent();
                m_Name = m_MyUserIdentity.Name;
                m_MyUserPrincipal = new WindowsPrincipal((WindowsIdentity)m_MyUserIdentity);
                m_MyUserHandle = (IPrincipal)m_MyUserPrincipal;
                IsAuthenticated = m_MyUserHandle.Identity.IsAuthenticated;
            }
            else
            {
                m_MyUserHandle = user;
                m_Name = user.Identity.Name;
                IsAuthenticated = m_MyUserHandle.Identity.IsAuthenticated;
            }
        }

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        public string Name
        {
            get { return m_Name; }
        }

        #region Dispose Pattern

        /// <summary>
        /// Dispose of resources.
        /// </summary>
        /// <param name="disposing">When true dispose of unmanaged resources.</param>
        internal void Dispose(bool disposing)
        {
            if (!m_Disposed)
            {
                if (disposing)
                {
                    if (m_InstanceManaged)
                    {
                        if (m_MyUserIdentity != null)
                        {
                            m_MyUserIdentity.Dispose();
                            m_MyUserIdentity = null;
                        }
                       
                    }
                    m_MyUserHandle = null;
                    m_MyUserPrincipal = null;
                }

                m_Disposed = true;
            }
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~UserIdentity()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
