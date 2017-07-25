// Copyright 2016-2017, Milner Technologies, Inc.
//
// This document contains data and information proprietary to
// Milner Technologies, Inc.  This data shall not be disclosed,
// disseminated, reproduced or otherwise used outside of the
// facilities of Milner Technologies, Inc., without the express
// written consent of an officer of the corporation.
//

using DBContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utility
{
    public enum TErrorMessageType
    {
        /// <summary>
        /// Error Type
        /// </summary>
        ERROR = 0,
        /// <summary>
        /// Set Warning Type
        /// </summary>
        WARNING = 1,
        /// <summary>
        /// Set Information
        /// </summary>
        INFORMATION = 2,
        /// <summary>
        /// Debugging information
        /// </summary>
        DIAGNOSTIC = 3
    }

    /// <summary>
    /// Write Exception Log Information into Audit Trial
    /// </summary>
    public sealed class ExceptionLog: ApplicationException
    {
        /// <summary>
        /// To Write Error Logs
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <param name="exception"></param>
        /// <param name="sessionID"></param>
        /// <param name="sessionUser"></param>
        public ExceptionLog(string message, TErrorMessageType type, Exception exception, string sessionID = "", string sessionUser = "") : base(message)
        {
            WriteEntry(exception, message, type, sessionID, sessionUser);
        }
        /// <summary>
        /// To Write Error Logs into Database
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="type"></param>
        /// <param name="sessionID"></param>
        /// <param name="sessionUser"></param>
        public void WriteEntry(Exception exception, string message, TErrorMessageType type, string sessionID="", string sessionUser = "")
        {
            UtilityLog.SetAuditTrial(type, DateTime.Now, message, exception.StackTrace, sessionUser, sessionID);
        }

    }
}
