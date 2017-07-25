// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//


using System.Diagnostics;
using System.Text;
using System;
using System.Reflection;
using System.Threading;
using System.Security.Principal;
using System.Data.SqlClient;
using DBContext;
using System.Data;

namespace Utility
{
    /// <summary>
    /// This class contains useful utility methods.
    /// </summary>
    public class UtilityLog
    {
       
        static public void EventLogWriteEntry(string source, string message, EventLogEntryType type)
        {
            try
            {
                try
                {
                    AssemblyInfoHelper info = new AssemblyInfoHelper();
                    message += "\n\nVersion: " + info.AssemblyVersion + "\nProcess ID: " + Process.GetCurrentProcess().Id.ToString();
                }
                catch
                {
                }

                EventLog.WriteEntry(source, message, type);
            }
            catch
            {  
            }
        }

        static public void EventLogException(string source, string message, Exception ex, EventLogEntryType type = EventLogEntryType.Error)
        {
            try
            {
                message += "\nError: " + ex.Message + "\n\nStack trace:\n" + ex.ToString();
                try
                {
                    AssemblyInfoHelper info = new AssemblyInfoHelper();
                    message += "\n\nVersion: " + info.AssemblyVersion + "\nProcess ID: " + Process.GetCurrentProcess().Id.ToString();
                }
                catch
                {
                }

                EventLog.WriteEntry(source, message, type);
            }
            catch
            {  
            }
        }

        /// <summary>
        /// Insert all kind of exception in audit trial
        /// </summary>
        /// <returns></returns>
        static public void SetAuditTrial(TErrorMessageType type, DateTime dateTime, string Message, string StackTrace="", string userID = "", string SessionUser = "", string sessionID = "")
        {
            DataTable datatable = new DataTable();
            try
            {
                //using (WindowsIdentity wi = WindowsIdentity.GetCurrent())
                //{
                //    string name = wi.Name;
                //    Message = string.Format("{0} CurrentUser={1}", Message, name);
                //}

                SqlParameter[] arParms = new SqlParameter[10];
                // @Type Input Parameter
                arParms[0] = new SqlParameter("@Type", SqlDbType.SmallInt);
                arParms[0].Value = (int)type;
                // @DateTime Input Parameter
                arParms[1] = new SqlParameter("@DateTime", SqlDbType.DateTime);
                arParms[1].Value = dateTime;
                // @Message Input Parameter
                arParms[2] = new SqlParameter("@Action", SqlDbType.NVarChar, 1500);
                arParms[2].Value = Message;
                // @User ID Input Parameter
                arParms[3] = new SqlParameter("@UserID", SqlDbType.Int);
                arParms[3].Value = String.IsNullOrEmpty(userID) ? 0 : Convert.ToInt32(userID);
                // @ExceptionID Input Parameter
                arParms[4] = new SqlParameter("@ExceptionID", SqlDbType.Int);
                arParms[4].Value = (int)type;
                // @StackTrace Input Parameter
                arParms[5] = new SqlParameter("@StackTrace", SqlDbType.NVarChar);
                arParms[5].Value = string.Empty;
                // @ThreadId Input Parameter
                arParms[6] = new SqlParameter("@ThreadId", SqlDbType.NVarChar, 128);
                arParms[6].Value = Thread.CurrentThread.ManagedThreadId.ToString();
                // @SessionUser Input Parameter
                arParms[7] = new SqlParameter("@SessionUser", SqlDbType.NVarChar, 50);
                arParms[7].Value = SessionUser;
                // @SessionID Input Parameter
                arParms[8] = new SqlParameter("@SessionID", SqlDbType.NVarChar, 50);
                arParms[8].Value = sessionID;
                //@RETURN_VALUE Return Value Parameter
                arParms[9] = new SqlParameter("@RETURN_VALUE", SqlDbType.Int, 4, ParameterDirection.ReturnValue, false, ((Byte)(0)), ((Byte)(0)), "", DataRowVersion.Current, null);
                int intResult = DbFactoryHelper.ExecuteNonQuery(DbFactoryHelper.GetDatabaseConnectionString(), CommandType.StoredProcedure, "SetAuditLogs", arParms);
                //Get Return Parameter value
                if (Convert.ToInt16(arParms[9].Value) != 0)
                {
                    throw new Exception("SetAuditTrial stored procedure returned a failure.");
                }
            }
            catch (Exception exp)
            {
                Utility.UtilityLog.EventLogWriteEntry("SalesCommission", "Method => SetAuditTrial : Failed to Set Audit Trial" + exp, System.Diagnostics.EventLogEntryType.Error);
            }
        }

        public static string escapechars(string val)
        {
            string retval = val;
            if (val != null && val.Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < val.Length; i++)
                {
                    char c = val[i];
                    if (c == '\'')
                        sb.Append("''");
                    else if (c == '\"')
                        sb.Append("''");
                    else
                        sb.Append(c);
                }
                retval = sb.ToString();
            }
            return retval;
        }



        /// <summary>   
        /// Function to Generate a 64 bit Key (cipher Key).   
        /// </summary>   
        public static string GenerateKey()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            using (CryptoRandom random = new CryptoRandom())
            {
                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }
            }

            string finalString = new String(stringChars);
            return finalString;

        }

    }

    public sealed class AssemblyInfoHelper
    {
        private Assembly m_Assembly = null;

        /// <summary>
        /// Construct an instance of this object for the calling assembly.
        /// </summary>
        public AssemblyInfoHelper()
        {
            m_Assembly = System.Reflection.Assembly.GetCallingAssembly();
        }

        /// <summary>
        /// Acquire an attribute from the calling assembly's metadata.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T CustomAttributes<T>() where T : Attribute
        {
            object[] customAttributes = m_Assembly.GetCustomAttributes(typeof(T), false);

            if ((customAttributes != null) && (customAttributes.Length > 0))
            {
                return ((T)customAttributes[0]);
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// The calling assembly's Product metadata.
        /// </summary>
        public string Product
        {
            get
            {
                return CustomAttributes<AssemblyProductAttribute>().Product;
            }
        }

        /// <summary>
        /// The calling assembly's Company metadata.
        /// </summary>
        public string Company
        {
            get
            {
                return CustomAttributes<AssemblyCompanyAttribute>().Company;
            }
        }

        /// <summary>
        /// The calling assembly's Description metadata.
        /// </summary>
        public string Description
        {
            get
            {
                return CustomAttributes<AssemblyDescriptionAttribute>().Description;
            }
        }

        /// <summary>
        /// The calling assembly's Copyright metadata.
        /// </summary>
        public string Copyright
        {
            get
            {
                return CustomAttributes<AssemblyCopyrightAttribute>().Copyright;
            }
        }

        /// <summary>
        /// The calling assembly's AssemblyVersion metadata.
        /// </summary>
        public string AssemblyVersion
        {
            get
            {
                return m_Assembly.GetName().Version.ToString();
            }
        }
    }
}
