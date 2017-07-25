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
    public class PayrollConfig
    {
        /// <summary>
        /// Default Constructors
        /// </summary>
        public PayrollConfig(){
        }
        /// <summary>
        /// PayrollConfig ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// PayrollConfig Year
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// PayrollConfig Period
        /// </summary>
        public int Period { get; set; }
        /// <summary>
        /// PayrollConfig Month 
        /// </summary>
        public string Month { get; set; }
        /// <summary>
        /// The Date From of this PayrollConfig.
        /// </summary>
        public DateTime DateFrom { get; set; }
        /// <summary>
        /// The Date To of this PayrollConfig.
        /// </summary>
        public DateTime DateTo { get; set; }
        /// <summary>
        /// The Created On of this PayrollConfig.
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// The Created By of this PayrollConfig.
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// The Name of Created By of this PayrollConfig.
        /// </summary>
        public string CreatedByName { get; set; }
        /// <summary>
        /// The Modified On of this PayrollConfig.
        /// </summary>
        public DateTime ModifiedOn { get; set; }
        /// <summary>
        /// The Modified By of this PayrollConfig.
        /// </summary>
        public int ModifiedBy { get; set; }
        /// <summary>
        /// The Name of Modified By of this PayrollConfig.
        /// </summary>
        public string ModifiedByName { get; set; }
        /// <summary>
        /// The Is Active of this PayrollConfig.
        /// </summary>
        public bool IsActive { get; set; }

        public int ProcessByPayroll { get; set; }
    }
}
