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
    public class BIMonthlyBonusInfo
    {
        /// <summary>
        /// The Default Constructor
        /// </summary>
        public BIMonthlyBonusInfo() { }
        /// <summary>
        /// The Unique ID of this BIMonthlyBonusInfo
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The PlanID of this BIMonthlyBonusInfo
        /// </summary>
        public int PlanID { get; set; }
        /// <summary>
        /// The Months of this BIMonthlyBonusInfo
        /// </summary>
        public string Months { get; set; }
        /// <summary>
        /// The EntryPointA of this BIMonthlyBonusInfo
        /// </summary>
        public int EntryPointA { get; set; }
        /// <summary>
        /// The EntryPointB of this BIMonthlyBonusInfo
        /// </summary>
        public int EntryPointB { get; set; }
        /// <summary>
        /// The Percentage of this BIMonthlyBonusInfo
        /// </summary>
        public Decimal Percentage { get; set; }
        /// <summary>
        /// The Tier of this BIMonthlyBonusInfo
        /// </summary>
        public string Tier { get; set; }

    }
    /// <summary>
    /// The Collection of this BIMonthlyBonusInfo
    /// </summary>
    public class BiMonthlyBonusCollection : List<BIMonthlyBonusInfo>, IEnumerable<SqlDataRecord>
    {
        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
        {
            var BiMonthlySQLrow = new SqlDataRecord(
                  new SqlMetaData("ID", SqlDbType.Int),
                  new SqlMetaData("PlanID", SqlDbType.Int),
                  new SqlMetaData("Months", SqlDbType.NVarChar, 10),
                  new SqlMetaData("EntryPointA", SqlDbType.Int),
                  new SqlMetaData("EntryPointB", SqlDbType.Int),
                  new SqlMetaData("Percentage", SqlDbType.Decimal,20,3),
                  new SqlMetaData("Tier", SqlDbType.NVarChar, 50)
                  );

            foreach (BIMonthlyBonusInfo BiMonthly in this)
            {
                BiMonthlySQLrow.SetInt32(0, BiMonthly.ID);
                BiMonthlySQLrow.SetInt32(1, BiMonthly.PlanID);
                BiMonthlySQLrow.SetString(2, BiMonthly.Months != null ? BiMonthly.Months : "");
                BiMonthlySQLrow.SetInt32(3, BiMonthly.EntryPointA);
                BiMonthlySQLrow.SetInt32(4, BiMonthly.EntryPointB);
                BiMonthlySQLrow.SetDecimal(5, BiMonthly.Percentage);
                BiMonthlySQLrow.SetString(6, BiMonthly.Tier != null ? BiMonthly.Tier : "");

                yield return BiMonthlySQLrow;

            }
        }
    }
}
