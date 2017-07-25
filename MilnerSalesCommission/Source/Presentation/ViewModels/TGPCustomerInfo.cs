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
using Microsoft.SqlServer.Server;
using System.Data;

namespace ViewModels
{
    public class TGPCustomerInfo
    {
        /// <summary>
        /// The Default Constructor
        /// </summary>
        public TGPCustomerInfo() { }
        /// <summary>
        /// The Unique ID of this TGPCustomerInfo
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The PlanID of this TGPCustomerInfo
        /// </summary>
        public int PlanID { get; set; }
        /// <summary>
        /// The SalesType of this TGPCustomerInfo
        /// </summary>
        public int SalesType { get; set; }
        /// <summary>
        /// The SalesType of this TGPCustomerInfo
        /// </summary>
        public string SalesTypeName { get; set; }
        /// <summary>
        /// The Percentage of this TGPCustomerInfo
        /// </summary>
        public Decimal Percentage { get; set; }
        /// <summary>
        /// The CustormerType of this TGPCustomerInfo
        /// </summary>
        public int CustomerType { get; set; }

    }
    /// <summary>
    /// The Collection of this TGPCustomerInfo
    /// </summary>
    public class TGPCustomerInfoCollection : List<TGPCustomerInfo>, IEnumerable<SqlDataRecord>
    {
        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
        {
            var TGPSqlrow = new SqlDataRecord(
                  new SqlMetaData("ID", SqlDbType.Int),
                  new SqlMetaData("PlanID", SqlDbType.Int),
                  new SqlMetaData("SalesType", SqlDbType.Int),
                  new SqlMetaData("Percentage", SqlDbType.Decimal,20,3),
                  new SqlMetaData("CustomerType", SqlDbType.Int)
                 );

            foreach (TGPCustomerInfo TGPinfo in this)
            {
                TGPSqlrow.SetInt32(0, TGPinfo.ID);
                TGPSqlrow.SetInt32(1, TGPinfo.PlanID);
                TGPSqlrow.SetInt32(2, TGPinfo.SalesType);
                TGPSqlrow.SetDecimal(3, TGPinfo.Percentage);
                TGPSqlrow.SetInt32(4, TGPinfo.CustomerType);

                yield return TGPSqlrow;

            }
        }
    }
}
