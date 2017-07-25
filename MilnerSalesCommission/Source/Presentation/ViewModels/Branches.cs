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
    public class Branches
    {
        /// <summary>
        /// Default Constructors
        /// </summary>
        public Branches() { }
        /// <summary>
        /// Branch ID
        /// </summary>
        public string BranchID { get; set; }
        /// <summary>
        /// Branch Name
        /// </summary>
        public string BranchName { get; set; }
        /// <summary>
        /// Branch Code
        /// </summary>
        public string BranchCode { get; set; }
        /// <summary>
        /// The Created On of this Branch Record.
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// The Created By of this Branch Record.
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// The Modified On of this Branch Record.
        /// </summary>
        public DateTime ModifiedOn { get; set; }
        /// <summary>
        /// The Modified By of this Branch Record.
        /// </summary>
        public int ModifiedBy { get; set; }
        /// <summary>
        /// The Is Active of this Branch.
        /// </summary>
        public bool IsActive { get; set; }
        public bool ticked { get; set; }
    }
}
