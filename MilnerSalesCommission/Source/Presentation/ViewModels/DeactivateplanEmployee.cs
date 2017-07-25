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
    public class DeActivatePlanEmployee
    {
        /// <summary>
        /// The Employee details mapped with Deactivated Payplan
        /// </summary>
        public EmployeeComponant emp { get; set; }
        /// <summary>
        /// The Deactivated Payplan details.
        /// </summary>
        public PlanComponent plan { get; set; }
        
    }
}
