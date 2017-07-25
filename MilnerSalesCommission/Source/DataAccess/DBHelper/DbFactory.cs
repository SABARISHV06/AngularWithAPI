//Copyright 2016-2017, Milner Technologies, Inc.
//
//This document contains data and information proprietary to
//Milner Technologies, Inc.  This data shall not be disclosed,
//disseminated, reproduced or otherwise used outside of the
//facilities of Milner Technologies, Inc., without the express
//written consent of an officer of the corporation.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security;
using System.Security.Permissions;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using Microsoft.Win32;
using GlobalConfig;

namespace DBContext
{
    /// <summary>
    /// Declared enum database type for SQL Client
    /// </summary>
    public enum TDbType : int 
    {
        /// <summary>
        /// The backend database is Microsoft SQL.
        /// </summary>
        SQLClient = 0,
    }

    /// <summary>
    /// DB Factory class
    /// </summary>
    internal class DbFactory
    {
        #region Static Private
        
        #endregion

        #region Constructor

        /// <summary>
        /// A class instance for creating entities.
        /// </summary>
        private static DbProviderFactory Factory { get; set; }

        /// <summary>
        /// Declare the variable of database type
        /// </summary>
        private static TDbType s_DatabaseType = TDbType.SQLClient;

        /// <summary>
        /// Set the database type based on the DbproviderFactory Instance
        /// </summary>
        public static TDbType DatabaseType 
        { 
            get
            {
                return s_DatabaseType;
            }
            set
            {    
                 s_DatabaseType = value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        static DbFactory()
        {

            string datatype = "System.Data.SQLClient";
            s_DatabaseType = TDbType.SQLClient; // The default.

            try
            {
                Factory = DbProviderFactories.GetFactory(datatype);
            }
            catch
            {
                DataRow drType = GetFactoryType(datatype);
                Factory = DbProviderFactories.GetFactory(drType);
            }

            
        }

        /// <summary>
        /// DbProviderFactories.GetFactoryClasses Method
        /// </summary>
        /// <param name="invariantName"></param>
        /// <returns></returns>
        private static DataRow GetFactoryType(string invariantName)
        {
            DataTable dt = DbProviderFactories.GetFactoryClasses();
            // Use this for loop to see what row holds System.Data.SqlClient
            // "DbProviderFactories.GetFactoryClasses Method"
            // http://msdn2.microsoft.com/en-us/library/system.data.common.dbproviderfactories.getfactoryclasses(VS.80).aspx
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["InvariantName"].Equals(invariantName))
                {
                    return dt.Rows[i];
                }
            }
            return null;
        }

        #endregion

        //#region Connection String Settings

        ///// <summary>
        ///// Returns the db connection string.
        ///// </summary>
        ///// <returns>Connection string for the database.</returns>
        //public static string GetConnectionString()
        //{
        //    try
        //    {
        //        GlobalConfiguration conig = new GlobalConfig.GlobalConfiguration();
        //        conig.LoadGlobalSettings();
                
        //        SqlConnectionStringBuilder myBuilder = new SqlConnectionStringBuilder();
        //        //myBuilder.UserID = conig.DBUserID;
        //        //myBuilder.Password =  conig.Password;
        //        myBuilder.IntegratedSecurity = true;
        //        myBuilder.InitialCatalog = conig.InitialCatalog;
        //        myBuilder.DataSource = conig.DataSource;
        //        myBuilder.ConnectTimeout = conig.ConnectionTimeout;
        //        return myBuilder.ConnectionString.ToString();
        //        //return "Data Source=ssss;Initial Catalog=ssss;Integrated Security=True";
        //    }
        //    catch (Exception exp)
        //    {
        //        throw new Exception("GetConnectionString method returned a failure." , exp);
        //    }

        //}

        //#endregion

        #region Connection Factories

        /// <summary>
        /// Initializes a new instance of the class and sets the ConnectionString. 
        /// </summary>
        /// <param name="connectionString">The string used to connect to a database.</param>
        public static DbConnection NewConnection(string connectionString)
        {
            DbConnection conn = Factory.CreateConnection();
            conn.ConnectionString = connectionString;
            return conn;
        }

        #endregion

        #region Command Factories

        public static DbCommand NewCommand()
        {
            GlobalConfiguration s_GlobalSettings = new GlobalConfig.GlobalConfiguration();
            s_GlobalSettings.LoadGlobalSettings();
            DbCommand cmd = Factory.CreateCommand();
            cmd.CommandTimeout = s_GlobalSettings.CommandTimeout;
            return cmd;
        }

        /// <summary>
        /// Initializes a new instance of the class with the text of the query and a connection. 
        /// </summary>
        /// <param name="statement">The query text.</param>
        /// <param name="connection">The database connection.</param>
        public static DbCommand NewCommand(string statement, DbConnection connection)
        {
            GlobalConfiguration s_GlobalSettings = new GlobalConfig.GlobalConfiguration();
            s_GlobalSettings.LoadGlobalSettings();
            DbCommand cmd = Factory.CreateCommand();
            cmd.CommandText = statement;
            cmd.Connection = connection;
            cmd.CommandTimeout = s_GlobalSettings.CommandTimeout;
            return cmd;
        }

        /// <summary>
        /// Initializes a new instance of the class with the text of the query and a connection. 
        /// </summary>
        /// <param name="transaction">The transaction for the command.</param>
        public static DbCommand NewCommand(DbConnection conn, DbTransaction transaction)
        {
            GlobalConfiguration s_GlobalSettings = new GlobalConfig.GlobalConfiguration();
            s_GlobalSettings.LoadGlobalSettings();
            DbCommand cmd = conn.CreateCommand();
            cmd.Transaction = transaction;
            cmd.CommandTimeout = s_GlobalSettings.CommandTimeout;
            return cmd;
        }
        
        /// <summary>
        /// Initializes a new instance of the class with the text of the query and a connection. 
        /// </summary>
        /// <param name="statement">The query text.</param>
        /// <param name="connection">The database connection.</param>
        /// <param name="transaction">The transaction for the command.</param>
        public static DbCommand NewCommand(string statement, DbConnection connection, DbTransaction transaction)
        {
            GlobalConfiguration s_GlobalSettings = new GlobalConfig.GlobalConfiguration();
            s_GlobalSettings.LoadGlobalSettings();
            DbCommand cmd = Factory.CreateCommand();
            cmd.CommandText = statement;
            cmd.Connection = connection;
            cmd.Transaction = transaction;
            cmd.CommandTimeout = s_GlobalSettings.CommandTimeout;
            return cmd;
        }

        /// <summary>
        /// Create a new command from an existing transaction.
        /// </summary>
        /// <param name="transaction">The transaction under which this command will run.</param>
        /// 
        /// <returns>
        /// DbCommand object upon success; null if failure.
        /// </returns>
        static public DbCommand GetCommand(DbTransaction transaction)
        {
            GlobalConfiguration s_GlobalSettings = new GlobalConfig.GlobalConfiguration();
            s_GlobalSettings.LoadGlobalSettings();
            // Create a command object that contains the connection to the database
            DbCommand command = transaction.Connection.CreateCommand();

            // Store the transaction back into the command
            command.Transaction = transaction;

            //Get default timeout from config
            command.CommandTimeout = s_GlobalSettings.CommandTimeout;

            return command;
        }

        #endregion

        #region Parameter Factories

        /// <summary>
        /// Initializes a new instance of the Parameter class that uses the parameter name, the DbType, and the size.
        /// </summary>
        /// <param name="parameterName">The name of the parameter to map.</param>
        /// <param name="dbType">One of the SqlDbType values.</param>
        /// <param name="size">The length of the parameter.</param>
        public static DbParameter NewParameter(string parameterName, SqlDbType dbType, int size)
        {
            DbParameter p = Factory.CreateParameter();
            p.ParameterName = parameterName;
            p.DbType = GetDbType(dbType);
            p.Size = size;
            return p;
        }

        /// <summary>
        /// Initializes a new instance of the Parameter class that uses the parameter name and the DbType.
        /// </summary>
        /// <param name="parameterName">The name of the parameter to map.</param>
        /// <param name="dbType">One of the SqlDbType values.</param>
        public static DbParameter NewParameter(string parameterName, SqlDbType dbType)
        {
            DbParameter p = Factory.CreateParameter();
            p.ParameterName = parameterName;
            p.DbType = GetDbType(dbType);
            return p;
        }

        public static DbParameter NewParameter(string parameterName, object value)
        {
            DbParameter p = Factory.CreateParameter();
            p.ParameterName = parameterName;
            p.Value = value;
            return p;
        }
        /// <summary>
        /// Initializes a new instance of the SqlParameter class that uses the parameter name, the type of the parameter,
        /// the size of the parameter, a ParameterDirection, the precision of the parameter, the scale of the parameter,
        /// the source column, a DataRowVersion to use, and the value of the parameter.
        /// </summary>
        /// <param name="parameterName">The name of the parameter to map.</param>
        /// <param name="dbType">One of the SqlDbType values.</param>
        /// <param name="size">The length of the parameter.</param>
        /// <param name="direction">One of the ParameterDirection values.</param>
        /// <param name="isNullable">true if the value of the field can be null; otherwise false.</param>
        /// <param name="precision">The total number of digits to the left and right of the decimal point to which Value is resolved.</param>
        /// <param name="scale">The total number of decimal places to which Value is resolved.</param>
        /// <param name="sourceColumn">The name of the source column (SourceColumn) if this SqlParameter is used in a call to Update.</param>
        /// <param name="sourceVersion">One of the DataRowVersion values. </param>
        /// <param name="value">An Object that is the value of the Parameter.</param>
        public static DbParameter NewParameter(string parameterName, SqlDbType dbType, int size, ParameterDirection direction, bool isNullable,
	        byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, Object value)
        {
            DbParameter p = Factory.CreateParameter();
            p.ParameterName = parameterName;
            p.DbType = GetDbType(dbType);
            p.Size = size;
            p.SourceColumnNullMapping = isNullable;
            p.Direction = direction;
            p.Precision = precision;
            p.Scale = scale;
            p.SourceColumn = sourceColumn;
            p.SourceVersion = sourceVersion;
            p.Value = value;
            return p;
        }

        /// <summary>
        /// Initializes a new instance of the SqlParameter class that uses the parameter name, the type of the parameter,
        /// the length of the parameter the direction, the precision, the scale, the name of the source column, one of the
        /// DataRowVersion values, a Boolean for source column mapping, the value of the SqlParameter, the name of the
        /// database where the schema collection for this XML instance is located, the owning relational schema where
        /// the schema collection for this XML instance is located, and the name of the schema collection for this parameter.
        /// </summary>
        /// <param name="parameterName">The name of the parameter to map.</param>
        /// <param name="dbType">One of the SqlDbType values.</param>
        /// <param name="size">The length of the parameter.</param>
        /// <param name="direction">One of the ParameterDirection values.</param>
        /// <param name="precision">The total number of digits to the left and right of the decimal point to which Value is resolved.</param>
        /// <param name="scale">The total number of decimal places to which Value is resolved.</param>
        /// <param name="sourceColumn">The name of the source column (SourceColumn) if this SqlParameter is used in a call to Update.</param>
        /// <param name="sourceVersion">One of the DataRowVersion values. </param>
        /// <param name="sourceColumnNullMapping">true if the source column is nullable; false if it is not.</param>
        /// <param name="value">An Object that is the value of the Parameter.</param>
        /// <param name="xmlSchemaCollectionDatabase">The name of the database where the schema collection for this XML instance is located.</param>
        /// <param name="xmlSchemaCollectionOwningSchema">The owning relational schema where the schema collection for this XML instance is located.</param>
        /// <param name="xmlSchemaCollectionName">The name of the schema collection for this parameter.</param>
        public static DbParameter NewParameter(string parameterName, SqlDbType dbType, int size, ParameterDirection direction, byte precision,
	        byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, Object value,
	        string xmlSchemaCollectionDatabase, string xmlSchemaCollectionOwningSchema, string xmlSchemaCollectionName)
        {
            DbParameter p = Factory.CreateParameter();
            p.ParameterName = parameterName;
            p.DbType = GetDbType(dbType);
            p.Size = size;
            p.Direction = direction;
            p.Precision = precision;
            p.Scale = scale;
            p.SourceColumn = sourceColumn;
            p.SourceVersion = sourceVersion;
            p.Value = value;
            return p;
        }

        #endregion

        #region DataAdapter Factories
        
        /// <summary>
        /// Initializes a new instance of the DbDataAdapter class with a SelectCommand and a Connection object.
        /// </summary>
        /// <param name="commandText">A String that is a SQL SELECT statement or stored procedure to be used by the SelectCommand property of the DataAdapter. </param>
        /// <param name="connection">A Connection that represents the connection. </param>
        public static DbDataAdapter NewDataAdapter(DbCommand cmd)
        {
            DbDataAdapter a = Factory.CreateDataAdapter();
            a.SelectCommand = cmd;
            return a;
        }

        /// <summary>
        /// Initializes a new instance of the DbDataAdapter class with a SelectCommand and a Connection object.
        /// </summary>
        /// <param name="commandText">A String that is a SQL SELECT statement or stored procedure to be used by the SelectCommand property of the DataAdapter. </param>
        /// <param name="connection">A Connection that represents the connection. </param>
        public static DbDataAdapter NewDataAdapter(string commandText, DbConnection connection)
        {
            DbDataAdapter a = Factory.CreateDataAdapter();
            a.SelectCommand = NewCommand(commandText, connection);
            return a;
        }

        #endregion
        
        #region CommandBuilder Factories

        public static DbCommandBuilder NewCommandBuilder(DbDataAdapter adapter)
        {
            DbCommandBuilder cmd = Factory.CreateCommandBuilder();
            cmd.DataAdapter = adapter;
            return cmd;
        }

       
        #endregion

        #region Private methods

        /// <summary>
        /// Map SqlDbType to a corresponding DbType.
        /// </summary>
        /// <param name="t">The type to map.</param>
        /// <returns>Equivalent DbType</returns>
        private static DbType GetDbType(SqlDbType t)
        {
            SqlParameter parm = new SqlParameter();
            try
            {
                parm.SqlDbType = t;
            }
            catch (Exception ex)
            {
                throw new Exception("Type " + t + " can't be mapped to a DbType.", ex);
            }

            return parm.DbType;
        }

        /// <summary>
        /// Map DbType to a corresponding SqlDbType.
        /// </summary>
        /// <param name="t">The type to map.</param>
        /// <returns>Equivalent SqlDbType</returns>
        private static SqlDbType GetSqlDbType(DbType t)
        {
            SqlParameter parm = new SqlParameter();
            try
            {
                parm.DbType = t;
            }
            catch (Exception ex)
            {
                throw new Exception("Type " + t + " can't be mapped to a SqlDbType.", ex);
            }

            return parm.SqlDbType;
        }

        #endregion

        #region Permission
        /// <summary>
        /// Set permission based on the state
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static CodeAccessPermission CreatePermission(PermissionState state)
        {
            CodeAccessPermission permissions = Factory.CreatePermission(state);
            return permissions;
        }

        #endregion

        #region Logging

        /// <summary>
        /// Safely record an error exception to the event log.
        /// </summary>
        /// <param name="source">Where the event originated.</param>
        /// <param name="message">The body of the message.</param>
        /// <param name="type">The exception.</param>
        static public void EventLogException(string source, string message, Exception ex)
        {
            try
            {
                message += "\nError: " + ex.Message + "\n\nStack trace:\n" + ex.ToString();
                EventLog.WriteEntry(source, message, EventLogEntryType.Error);
            }
            catch
            {  // If the event log is full then it will throw an exception. Empty catch = "Whoops, can't log."
            }
        }

        #endregion
    }

}
