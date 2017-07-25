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

namespace ViewModels
{
    public class SaleType
    {
        /// <summary>
        /// Default Constructors
        /// </summary>
        public SaleType() { }

        /// <summary>
        /// SaleType ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// SaleType Name
        /// </summary>
        public string SaleTypeName { get; set; }
        /// <summary>
        /// The is New Customer of this SaleType.
        /// </summary>
        public bool IsNewCustomer { get; set; }
        /// <summary>
        /// The is Existing Customer of this SaleType.
        /// </summary>
        public bool IsExistingCustomer { get; set; }
        /// <summary>
        /// The is BiMonthly Bonus of this SaleType.
        /// </summary>
        public bool IsBiMonthlyBonus { get; set; }
        /// <summary>
        /// The is Tenure Bonus of this SaleType.
        /// </summary>
        public bool IsTenureBonus { get; set; }
        /// <summary>
        /// The Created On of this SaleType.
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// The Created By of this SaleType.
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// The Modified On of this SaleType.
        /// </summary>
        public DateTime ModifiedOn { get; set; }
        /// <summary>
        /// The Modified By of this SaleType.
        /// </summary>
        public int ModifiedBy { get; set; }
        /// <summary>
        /// The Is Active of this SaleType.
        /// </summary>
        public bool IsActive { get; set; }

    }

    public class CheckSaleType
    {
        /// <summary>
        /// SaleType ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Reason ID
        /// </summary>
        public int reason { get; set; }
    }
}
