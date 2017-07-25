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
   public  class TenureBonus
    {
        /// <summary>
        /// The Default Constructor
        /// </summary>
        public TenureBonus() { }
        /// <summary>
        /// The Unique ID of this TenureBonus
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The PlanID of this TenureBonus
        /// </summary>
        public int PlanID { get; set; }
        /// <summary>
        /// The Months of this TenureBonus
        /// </summary>
        public string Months { get; set; }
        /// <summary>
        /// The EntryPointA of this TenureBonus
        /// </summary>
        public int EntryPointA { get; set; }
        /// <summary>
        /// The EntryPointB of this TenureBonus
        /// </summary>
        public int EntryPointB { get; set; }
        /// <summary>
        /// The Percentage of this TenureBonus
        /// </summary>
        public Decimal Percentage { get; set; }
        /// <summary>
        /// The Tier of this TenureBonus
        /// </summary>
        public string Tier { get; set; }
    }
    /// <summary>
    /// The Collection of this TenureBonus
    /// </summary>
    public class TenureBonusCollection : List<TenureBonus>, IEnumerable<SqlDataRecord>
    {
        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
        {
            var TenureSQLrow = new SqlDataRecord(
                  new SqlMetaData("ID", SqlDbType.Int),
                  new SqlMetaData("PlanID", SqlDbType.Int),
                  new SqlMetaData("Months", SqlDbType.NVarChar, 10),
                  new SqlMetaData("EntryPointA", SqlDbType.Int),
                  new SqlMetaData("EntryPointB", SqlDbType.Int),
                  new SqlMetaData("Percentage", SqlDbType.Decimal,20,3),
                  new SqlMetaData("Tier", SqlDbType.NVarChar, 50)
                  );

            foreach (TenureBonus Tenure in this)
            {
                TenureSQLrow.SetInt32(0, Tenure.ID);
                TenureSQLrow.SetInt32(1, Tenure.PlanID);
                TenureSQLrow.SetString(2, Tenure.Months != null ? Tenure.Months : string.Empty);
                TenureSQLrow.SetInt32(3, Tenure.EntryPointA);
                TenureSQLrow.SetInt32(4, Tenure.EntryPointB);
                TenureSQLrow.SetDecimal(5, Tenure.Percentage);
                TenureSQLrow.SetString(6, Tenure.Tier != null ? Tenure.Tier : string.Empty);
                
                yield return TenureSQLrow;

            }
        }
    }
}
