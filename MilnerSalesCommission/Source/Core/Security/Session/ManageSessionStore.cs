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
using System.Text;
using System.Threading.Tasks;

namespace Security.Session
{
    /// <summary>
    /// All session access will be performed within a single class and 
    /// All session objects will be exposed via getters and setters and/or properties.
    /// All session state entries will be specified from constants
    /// </summary>
    public sealed class ManageSessionStore
    {
        #region Private variables

        private string _loginName = string.Empty;
        private string _password = string.Empty;
        private Dictionary<string, object> _store = new Dictionary<string, object>();
        private Dictionary<Guid, object> _temporaryStore = new Dictionary<Guid, object>();

        #endregion

        #region Session Constants

        private const string CONSTMFSESSIONSTORENAME = "MFSessionStore";
        private const string CONSTSECURITYQUESTION = "SecurityQuestion";
        private const string CONSTLQCNETPRINCIPAL = "LQCNetPrincipal";
        private const string CONSTTMPSESSIONSTORENAME = "TMPSessionStore";

        public const string CONSTAUTHKEY = "LQR";
        public const string CONSTLASTACCESSEDPAGE = "LastAccessedPage";
        public const string CONSTUSERNAME = "TemP";
        public const string CONSTUSERPASSWORD = "PSWD";

        #endregion

        #region Public functions

        public object this[string index]
        {
            get
            {
                try
                {
                    return this._store[index];
                }
                catch (Exception ex)
                {
                    throw (new Exception(ex.Message));
                }
            }
            set
            {
                try
                {
                    this._store[index] = value;
                }
                catch (Exception ex)
                {
                    throw (new Exception(ex.Message));
                }
            }
        }

        public Dictionary<string, object> Store
        {
            get
            {
                return _store;
            }
        }

        public Guid ArchiveTemporaryItem(object item)
        {
            Guid id = Guid.NewGuid();
            _temporaryStore[id] = item;
            return id;
        }

        public object GetTemporaryItem(Guid id)
        {
            object returnItem = null;
            if (_temporaryStore.Keys.Contains(id))
            {
                returnItem = _temporaryStore[id];
            }
            return returnItem;
        }

        public object RestoreTemporaryItem(Guid id)
        {
            object returnItem = null;
            if (_temporaryStore.Keys.Contains(id))
            {
                returnItem = _temporaryStore[id];
                _temporaryStore.Remove(id);
            }
            return returnItem;
        }

        public void FreeTemporaryItem(Guid id)
        {
            if (_temporaryStore.Keys.Contains(id))
            {
                _temporaryStore.Remove(id);
            }
        }

        public static ManageSessionStore GetSessionStore()
        {
            return (ManageSessionStore)System.Web.HttpContext.Current.Session[CONSTMFSESSIONSTORENAME];
        }

        public static void FreeSessionStore()
        {
            System.Web.HttpContext.Current.Session[CONSTMFSESSIONSTORENAME] = null;
            System.Web.HttpContext.Current.Session[CONSTTMPSESSIONSTORENAME] = null;
            System.Web.HttpContext.Current.Session.Abandon();
        }

        public static void ArchiveSessionStore()
        {
            System.Web.HttpContext.Current.Session[CONSTTMPSESSIONSTORENAME] = (ManageSessionStore)System.Web.HttpContext.Current.Session[CONSTMFSESSIONSTORENAME];
            System.Web.HttpContext.Current.Session[CONSTMFSESSIONSTORENAME] = null;
        }

        public static ManageSessionStore RestoreSessionStore()
        {
            System.Web.HttpContext.Current.Session[CONSTMFSESSIONSTORENAME] = (ManageSessionStore)System.Web.HttpContext.Current.Session[CONSTTMPSESSIONSTORENAME];
            System.Web.HttpContext.Current.Session[CONSTTMPSESSIONSTORENAME] = null;
            return (ManageSessionStore)System.Web.HttpContext.Current.Session[CONSTMFSESSIONSTORENAME];
        }

        public void SetBytesInSession(string name, byte[] bytes)
        {
            System.Web.HttpContext.Current.Session[name] = bytes;
        }

        public byte[] GetBytesFromSession(string name)
        {
            return (byte[])System.Web.HttpContext.Current.Session[name];
        }

        public static object GetSessionValue(string sessionConstName)
        {
            try
            {
                return System.Web.HttpContext.Current.Session[sessionConstName];
            }
            catch
            {
                return null;
            }
        }

        public static void SetSessionValue(string sessionConstName, object constValue)
        {
            try
            {
                System.Web.HttpContext.Current.Session[sessionConstName] = constValue;
            }
            catch
            {
            }
        }

        #endregion
    }
}
