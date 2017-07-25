// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//

using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{

    /// <summary>
    /// Logging messages in audit trial based on the log types.
    /// </summary>
    public static class AuditLogHelper
    {
        /// <summary>
        /// To get Common Logger
        /// </summary>
        private static readonly ILog m_Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// To get audit log to table flag
        /// </summary>
        private static readonly bool m_LogsToTableEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["LogsToTableEnabled"]);

        /// <summary>
        /// To get audit log to file flag
        /// </summary>
        private static readonly bool m_LogsToFileEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["LogsToFileEnabled"]);

               
        /// <summary>
        /// To write any debug message with exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void Debug(object message, Exception ex)
        {
            try
            {
                if (m_Logger.IsDebugEnabled) {

                    m_Logger.Debug(message, ex);
                }
                if (m_LogsToTableEnabled)
                {
                    UtilityLog.SetAuditTrial(TErrorMessageType.DIAGNOSTIC, DateTime.Now, message.ToString(), ex.StackTrace);
                }
                
            }
            catch 
            {
               
            }
        }
        /// <summary>
        /// To write any debug message
        /// </summary>
        /// <param name="message"></param>
        public static void DebugMessage(object message)
        {
            try
            {
                if (m_Logger.IsDebugEnabled) {
                    m_Logger.Debug(message);
                }
                if (m_LogsToTableEnabled)
                {
                    UtilityLog.SetAuditTrial(TErrorMessageType.DIAGNOSTIC, DateTime.Now, message.ToString(), null);
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// To write any Error message with exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void ErrorLog(object message, Exception ex, string userID = "")
        {
            
            try
            {
                if (m_Logger.IsErrorEnabled) { m_Logger.Error(message, ex);
                }
                if (m_LogsToTableEnabled)
                {
                    UtilityLog.SetAuditTrial(TErrorMessageType.ERROR, DateTime.Now, message.ToString(), ex.StackTrace, userID);
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// To write any Error message
        /// </summary>
        /// <param name="message"></param>
        public static void ErrorLogMessage(object message)
        {
            try
            {
                if (m_Logger.IsErrorEnabled) { m_Logger.Error(message);
                }
                if (m_LogsToTableEnabled)
                {
                    UtilityLog.SetAuditTrial(TErrorMessageType.ERROR, DateTime.Now, message.ToString(), null);
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// To write any info message with exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void InfoLog(object message, Exception ex)
        {
            
            try
            {
                if (m_Logger.IsInfoEnabled) { m_Logger.Info(message, ex);
                }
                if (m_LogsToTableEnabled)
                {
                    UtilityLog.SetAuditTrial(TErrorMessageType.INFORMATION, DateTime.Now, message.ToString(), ex.StackTrace);
                }
            }
            catch
            {

            }
        }
        /// <summary>
        ///  To write any info message
        /// </summary>
        /// <param name="message"></param>
        public static void InfoLogMessage(object message, string userID="")
        {
            try
            {
                if (m_Logger.IsInfoEnabled) { m_Logger.Info(message);
                }
                if (m_LogsToTableEnabled)
                {
                    UtilityLog.SetAuditTrial(TErrorMessageType.INFORMATION, DateTime.Now, message.ToString(), null, userID);
                }
            }
            catch
            {

            }
        }
        /// <summary>
        ///  To write any Warning Log message with exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void WarningLog(object message, Exception ex)
        {
            try
            {
                if (m_Logger.IsWarnEnabled) { m_Logger.Warn(message, ex);
                }
                if (m_LogsToTableEnabled)
                {
                    UtilityLog.SetAuditTrial(TErrorMessageType.WARNING, DateTime.Now, message.ToString(), null);
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// To write any Warning Log message
        /// </summary>
        /// <param name="message"></param>
        public static void WarningMessage(object message)
        {
            try
            {
                if (m_Logger.IsWarnEnabled) { m_Logger.Warn(message);
                }
                if (m_LogsToTableEnabled)
                {
                    UtilityLog.SetAuditTrial(TErrorMessageType.WARNING, DateTime.Now, message.ToString(), null);
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// To write any Fatal Log message with exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void FatalLog(object message, Exception ex)
        {
            try
            {
                if (m_Logger.IsFatalEnabled) { m_Logger.Fatal(message, ex); }
                if (m_LogsToTableEnabled)
                {
                    UtilityLog.SetAuditTrial(TErrorMessageType.ERROR, DateTime.Now, message.ToString(), null);
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// To write any Fatal Message
        /// </summary>
        /// <param name="message"></param>
        public static void FatalMessage(object message)
        {
            try
            {
                if (m_Logger.IsFatalEnabled) { m_Logger.Fatal(message); }
                if (m_LogsToTableEnabled)
                {
                    UtilityLog.SetAuditTrial(TErrorMessageType.ERROR, DateTime.Now, message.ToString(), null);
                }
            }
            catch
            {

            }
        }
    }

}
