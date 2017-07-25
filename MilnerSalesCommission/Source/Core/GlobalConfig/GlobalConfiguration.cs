// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//


using System;
using System.Xml.Serialization;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Net;
using System.Linq;
using System.Net.Sockets;
using System.Configuration;
using System.Data.SqlClient;

namespace GlobalConfig
{
    /// <summary>
    /// Global configuration settings to store configuration keys and values
    /// </summary>
    public class GlobalConfiguration
    {
        #region Private Properties

        /// <summary>
        /// When true, settings have changed and require saving.
        /// </summary>
        private bool m_GlobalSettingsChanged = false;

        #endregion

        #region Public Properties

        /// <summary>
        /// The optional temp directory location.
        /// </summary>
        private string m_connectionStringManager = string.Empty; //ConfigurationManager.ConnectionStrings["MYSQLConnectionString"].ConnectionString;

        /// <summary>
        /// Get or set the optional temp directory location.
        /// </summary>
        public string ConnectionString
        {
            get {

                return GetConnectionString();
            }
            
        }
        private string GetConnectionString()
        {
            SqlConnectionStringBuilder myBuilder = new SqlConnectionStringBuilder();
            myBuilder.UserID = string.IsNullOrEmpty(m_UserID) ? "" : m_UserID; 
            myBuilder.Password = string.IsNullOrEmpty(m_Password) ? "" : m_Password;
            myBuilder.IntegratedSecurity = true;
            myBuilder.InitialCatalog = m_InitialCatalog;
            myBuilder.DataSource = m_DataSource;
            myBuilder.ConnectTimeout = m_ConnectionTimeout;
            return myBuilder.ConnectionString.ToString();
            //return "Data Source=nnnnnn;Initial Catalog=sc;Integrated Security=True";
        }
        /// <summary>
        /// The name of the mailbox or user name on the SMTP server.
        /// </summary>
        private string m_UserID = Properties.Settings.Default.UserID;

        /// <summary>
        /// The name of the mailbox or user name on the SMTP server.
        /// </summary>
        public string DBUserID
        {
            get { return m_UserID; }
            set
            {
                if (value != m_UserID)
                {
                    m_UserID = value;
                    m_GlobalSettingsChanged = true;
                }
            }
        }


        /// <summary>
        /// The password, in encrypted form, of the mailbox or user name on the SMTP server.
        /// </summary>
        private string m_Password = Properties.Settings.Default.Password;

        /// <summary>
        /// The password, in encrypted form, of the mailbox or user name on the SMTP server.
        /// </summary>
        public string Password
        {
            get { return m_Password; }
            set
            {
                if (value != m_Password)
                {
                    m_Password = value;
                    m_GlobalSettingsChanged = true;
                }
            }
        }

        /// <summary>
        /// The name of the Database for the application.
        /// </summary>
        private string m_InitialCatalog = Properties.Settings.Default.InitialCatalog;

        /// <summary>
        /// The name of the database for the application.
        /// </summary>
        public string InitialCatalog
        {
            get { return m_InitialCatalog; }
            set
            {
                if (value != m_InitialCatalog)
                {
                    m_InitialCatalog = value;
                    m_GlobalSettingsChanged = true;
                }
            }
        }

        /// <summary>
        /// The name of the datasource of the SQL server.
        /// </summary>
        private string m_DataSource = Properties.Settings.Default.DataSource;

        /// <summary>
        /// The name of the datasource of the SQL server.
        /// </summary>
        public string DataSource
        {
            get { return m_DataSource; }
            set
            {
                if (value != m_DataSource)
                {
                    m_DataSource = value;
                    m_GlobalSettingsChanged = true;
                }
            }
        }

        /// <summary>
        /// The name of the mailbox or user name on the SMTP server.
        /// </summary>
        private string m_EmailHost = Properties.Settings.Default.EmailHost;

        /// <summary>
        /// The name of the mailbox or user name on the SMTP server.
        /// </summary>
        public string EmailHost
        {
            get { return m_EmailHost; }
            set
            {
                if (value != m_EmailHost)
                {
                    m_EmailHost = value;
                    m_GlobalSettingsChanged = true;
                }
            }
        }

        /// <summary>
        /// The name of the port on the SMTP server.
        /// </summary>
        private int m_EmailPort = Properties.Settings.Default.EmailPort;

        /// <summary>
        /// The name of the port on the SMTP server.
        /// </summary>
        public int EmailPort
        {
            get { return m_EmailPort; }
            set
            {
                if (value != m_EmailPort)
                {
                    m_EmailPort = value;
                    m_GlobalSettingsChanged = true;
                }
            }
        }

        /// <summary>
        /// The name of the port on the SMTP server.
        /// </summary>
        private string m_EmailUserName = Properties.Settings.Default.EmailUserName;

        /// <summary>
        /// The name of the port on the SMTP server.
        /// </summary>
        public string EmailUserName
        {
            get { return m_EmailUserName; }
            set
            {
                if (value != m_EmailUserName)
                {
                    m_EmailUserName = value;
                    m_GlobalSettingsChanged = true;
                }
            }
        }

        /// <summary>
        /// The name of the port on the SMTP server.
        /// </summary>
        private string m_EmailPassword = Properties.Settings.Default.EmailPassword;

        /// <summary>
        /// The name of the port on the SMTP server.
        /// </summary>
        public string EmailPassword
        {
            get { return m_EmailPassword; }
            set
            {
                if (value != m_EmailPassword)
                {
                    m_EmailPassword = value;
                    m_GlobalSettingsChanged = true;
                }
            }
        }
        /// <summary>
        /// The name of the timeout on the SMTP server.
        /// </summary>
        private int m_EmailTimeout = Properties.Settings.Default.EmailTimeout;

        /// <summary>
        /// The name of the timeout on the SMTP server.
        /// </summary>
        public int EmailTimeout
        {
            get { return m_EmailTimeout; }
            set
            {
                if (value != m_EmailTimeout)
                {
                    m_EmailTimeout = value;
                    m_GlobalSettingsChanged = true;
                }
            }
        }

        /// <summary>
        /// The name of the timeout on the SMTP server.
        /// </summary>
        private bool m_EmailUseDefaultCredentials = Properties.Settings.Default.EmailDefaultCredentials;

        /// <summary>
        /// The name of the timeout on the SMTP server.
        /// </summary>
        public bool EmailUseDefaultCredentials
        {
            get { return m_EmailUseDefaultCredentials; }
            set
            {
                if (value != m_EmailUseDefaultCredentials)
                {
                    m_EmailUseDefaultCredentials = value;
                    m_GlobalSettingsChanged = true;
                }
            }
        }

        /// <summary>
        /// The name of the timeout on the SMTP server.
        /// </summary>
        private bool m_EmailSSL = Properties.Settings.Default.EmailSSL;

        /// <summary>
        /// The name of the timeout on the SMTP server.
        /// </summary>
        public bool EmailSSL
        {
            get { return m_EmailSSL; }
            set
            {
                if (value != m_EmailSSL)
                {
                    m_EmailSSL = value;
                    m_GlobalSettingsChanged = true;
                }
            }
        }

        /// <summary>
        /// The name of the timeout on the SMTP server.
        /// </summary>
        private string m_EmailSender = Properties.Settings.Default.EmailFrom;

        /// <summary>
        /// The name of the timeout on the SMTP server.
        /// </summary>
        public string EmailSender
        {
            get { return m_EmailSender; }
            set
            {
                if (value != m_EmailSender)
                {
                    m_EmailSender = value;
                    m_GlobalSettingsChanged = true;
                }
            }
        }

        /// <summary>
        /// The name of the timeout on the SQL Command.
        /// </summary>
        private int m_CommandTimeout = Properties.Settings.Default.DefaultSQLCommandTimeout;

        /// <summary>
        /// The name of the timeout on the SQL Command.
        /// </summary>
        public int CommandTimeout
        {
            get { return m_CommandTimeout; }
            set
            {
                if (value != m_CommandTimeout)
                {
                    m_CommandTimeout = value;
                    m_GlobalSettingsChanged = true;
                }
            }
        }

        /// <summary>
        /// The name of the timeout on the SQL connection.
        /// </summary>
        private int m_ConnectionTimeout = Properties.Settings.Default.ConnectionTimeout;

        /// <summary>
        /// The name of the timeout on the SQL connection.
        /// </summary>
        public int ConnectionTimeout
        {
            get { return m_ConnectionTimeout; }
            set
            {
                if (value != m_ConnectionTimeout)
                {
                    m_ConnectionTimeout = value;
                    m_GlobalSettingsChanged = true;
                }
            }
        }

        /// <summary>
        /// The name of the timeout on the SQL connection.
        /// </summary>
        private string m_IntegratedSecurity = Properties.Settings.Default.IntegratedSecurity;

        /// <summary>
        /// The name of the timeout on the SQL connection.
        /// </summary>
        public string IntegratedSecurity
        {
            get { return m_IntegratedSecurity; }
            set
            {
                if (value != m_IntegratedSecurity)
                {
                    m_IntegratedSecurity = value;
                    m_GlobalSettingsChanged = true;
                }
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Add a system-wide ACL to a file.  Failures are ignored.
        /// </summary>
        /// <param name="path">The name of the file</param>
        /// <param name="rights">The right to be added</param>
        /// <param name="controlType">The value of the right</param>
        private void AddGlobalFileSecurity(string path, FileSystemRights rights, AccessControlType controlType)
        {
            try
            {
                System.Security.Principal.SecurityIdentifier sid = new System.Security.Principal.SecurityIdentifier(System.Security.Principal.WellKnownSidType.WorldSid, null);
                System.Security.Principal.NTAccount acct = sid.Translate(typeof(System.Security.Principal.NTAccount)) as System.Security.Principal.NTAccount;
                string acctName = acct.ToString();

                FileSecurity security = File.GetAccessControl(path);
                security.AddAccessRule(new FileSystemAccessRule(acctName, rights, controlType));
                File.SetAccessControl(path, security);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Give the specified directory all available file update rights.
        /// </summary>
        /// <param name="path">The file to which rights are granted.</param>
        private void GrantDirRights(string path)
        {
            try
            {
                SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                NTAccount acct = sid.Translate(typeof(NTAccount)) as NTAccount;
                string acctName = acct.ToString();

                FileSystemAccessRule rule = new FileSystemAccessRule(acctName,
                    FileSystemRights.Write
                    | FileSystemRights.Read
                    | FileSystemRights.ListDirectory
                    | FileSystemRights.CreateFiles
                    | FileSystemRights.CreateDirectories
                    | FileSystemRights.DeleteSubdirectoriesAndFiles
                    | FileSystemRights.Delete
                    | FileSystemRights.ChangePermissions,
                    InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                    PropagationFlags.None,
                    AccessControlType.Allow);

                DirectorySecurity security = Directory.GetAccessControl(path);
                security.AddAccessRule(rule);

                Directory.SetAccessControl(path, security);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Give the specified file all available file update rights.
        /// </summary>
        /// <param name="path">The file to which rights are granted.</param>
        private void GrantFileRights(string path)
        {
            AddGlobalFileSecurity(path, FileSystemRights.Write, AccessControlType.Allow);
            AddGlobalFileSecurity(path, FileSystemRights.Read, AccessControlType.Allow);
            AddGlobalFileSecurity(path, FileSystemRights.Delete, AccessControlType.Allow);
            AddGlobalFileSecurity(path, FileSystemRights.ChangePermissions, AccessControlType.Allow);
        }

        private static string s_GlobalSettingsDir =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"SalesCommission\MSC");
        private static string s_GlobalSettingsPath = s_GlobalSettingsDir + @"\GlobalConfig.config";

        /// <summary>
        /// Ensure the directory that holds the global settings exists if user has sufficient rights
        /// to create it.
        /// </summary>
        private void MakeGlobalDirectory()
        {
            try
            {
                if (!Directory.Exists(s_GlobalSettingsDir))
                {
                    Directory.CreateDirectory(s_GlobalSettingsDir);

                    GrantDirRights(s_GlobalSettingsDir);
                }
            }
            catch
            {
            }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Asks whether the user has rights to update global settings.
        /// </summary>
        /// <returns>true if the user has sufficient rights to update settings.</returns>
        public bool IsSaveAllowed()
        {
            bool allowed = false;

            try
            {
                MakeGlobalDirectory();

                FileAttributes dirAttr = File.GetAttributes(s_GlobalSettingsDir);
                if ((dirAttr & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
                {
                    if (File.Exists(s_GlobalSettingsPath))
                    {
                        FileAttributes fileAttr = File.GetAttributes(s_GlobalSettingsPath);
                        allowed = ((fileAttr & FileAttributes.ReadOnly) != FileAttributes.ReadOnly);
                    }
                    else
                    {
                        allowed = true;
                    }
                }
            }
            catch
            {
            }

            return allowed;
        }

        /// <summary>
        /// Save global settings.  If settings cannot be saved the dirty flag remains unchanged.
        /// </summary>
        /// <returns>The value of the dirty flag.</returns>
        public bool SaveGlobalSettings()
        {
            if (m_GlobalSettingsChanged)
            {
                MakeGlobalDirectory();

                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(GlobalConfiguration));
                    using (StreamWriter writer = new StreamWriter(s_GlobalSettingsPath, false))
                    {
                        serializer.Serialize(writer, this);
                    }

                    m_GlobalSettingsChanged = false;
                }
                catch
                {
                }
                finally
                {
                    GrantFileRights(s_GlobalSettingsPath);
                }
            }

            return m_GlobalSettingsChanged;
        }

        /// <summary>
        /// Initialize global settings.
        /// </summary>
        /// <returns>true if settings were read from a previous session.  False means settings were not updated or are set to defaults.</returns>
        public bool LoadGlobalSettings()
        {
            MakeGlobalDirectory();

            bool fileExists = false;

            try
            {
                FileInfo fi = new FileInfo(s_GlobalSettingsPath);

                if (fi.Exists)
                {
                    using (FileStream stream = fi.OpenRead())
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(GlobalConfiguration));
                        GlobalConfiguration globalSettings = (GlobalConfiguration)serializer.Deserialize(stream);


                        m_connectionStringManager = globalSettings.ConnectionString;
                        m_UserID = globalSettings.DBUserID;
                        m_Password = globalSettings.Password;
                        m_InitialCatalog = globalSettings.InitialCatalog;
                        m_DataSource = globalSettings.DataSource;
                        m_CommandTimeout = globalSettings.CommandTimeout;
                        m_ConnectionTimeout = globalSettings.ConnectionTimeout;
                        m_EmailHost = globalSettings.EmailHost;
                        m_EmailPassword = globalSettings.EmailPassword;
                        m_EmailPort = globalSettings.EmailPort;
                        m_EmailSender = globalSettings.EmailSender;
                        m_EmailSSL = globalSettings.EmailSSL;
                        m_EmailTimeout = globalSettings.EmailTimeout;
                        m_EmailUseDefaultCredentials = globalSettings.EmailUseDefaultCredentials;
                        m_EmailUserName = globalSettings.EmailUserName;
                        m_IntegratedSecurity = globalSettings.IntegratedSecurity;
                    }

                    fileExists = true;
                }
                else
                {
                    SaveGlobalSettings();
                }
            }
            catch
            {
            }

            return fileExists;
        }

        /// <summary>
        /// Force global settings to be marked as having been modified.
        /// </summary>
        public void DirtyGlobalSettings()
        {
            m_GlobalSettingsChanged = true;
        }

        /// <summary>
        /// Convert settings to a serialized XML string.
        /// </summary>
        /// <returns>Settings in serialized form.</returns>
        public string SettingsToString()
        {
            using (StringWriter sw = new StringWriter())
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(GlobalConfiguration));
                    serializer.Serialize(sw, this);
                }
                catch (Exception ex)
                {
                    throw new Exception("Can't serialize settings.", ex);
                }

                return sw.ToString();
            }
        }

        /// <summary>
        /// Convert a string into an instance of this class.
        /// </summary>
        /// <param name="settings">Serialized string representing this class.</param>
        /// <returns>A reconstituted instance of this class.</returns>
        public static GlobalConfiguration SettingsFromString(string settings)
        {
            using (System.IO.StringReader rxml = new System.IO.StringReader(settings))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GlobalConfiguration));

                return (GlobalConfiguration)serializer.Deserialize(rxml);
            }
        }

        #endregion
    }
}
