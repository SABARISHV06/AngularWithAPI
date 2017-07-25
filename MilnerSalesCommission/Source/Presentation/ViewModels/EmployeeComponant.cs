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
    public class EmployeeComponant
    {
        /// <summary>
        /// Default Constructors
        /// </summary>
        public EmployeeComponant() { }
        /// <summary>
        /// The unique ID of this Employee.
        /// </summary>
        public int UID { get; set; }
        /// <summary>
        /// The employee ID of this Employee.
        /// </summary>
        public string EmployeeID { get; set; }
        /// <summary>
        /// The AccountName of this Employee.
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// The First Name of this Employee.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The Last Name of this Employee.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The Middle Name of this Employee.
        /// </summary>
        public string MiddleName { get; set; }
        /// <summary>
        /// The Role ID of this Employee.
        /// </summary>
        public int RoleID { get; set; }
        /// <summary>
        /// The Role ID of this Employee.
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// The Date of hire of this Employee.
        /// </summary>
        public DateTime DateofHire { get; set; }
        /// <summary>
        /// The Date of Position of this Employee.
        /// </summary>
        public DateTime DateInPosition { get; set; }
        /// <summary>
        /// The Primary Branch ID of this Employee.
        /// </summary>
        public string PrimaryBranch { get; set; }
        /// <summary>
        /// The Primary Branch Name of this Employee.
        /// </summary>
        public string PrimaryBranchName { get; set; }
        /// <summary>
        /// The Secondary Branch ID of this Employee.
        /// </summary>
        public string[] SecondaryBranch { get; set; }
        /// <summary>
        /// The Secondary Branch Name of this Employee.
        /// </summary>
        public string SecondaryBranchName { get; set; }
        /// <summary>
        /// The Pay Plan ID of this Employee.
        /// </summary>
        public string PayPlanID { get; set; }
        /// <summary>
        /// The Pay Plan Name of this Employee.
        /// </summary>
        public string PayPlanName { get; set; }
        /// <summary>
        /// The BP Salary of this Employee.
        /// </summary>
        public bool BPSalary { get; set; }
        /// <summary>
        /// The BP Draw of this Employee.
        /// </summary>
        public bool BPDraw { get; set; }
        /// <summary>
        /// The Month Amount of this Employee.
        /// </summary>
        public decimal MonthAmount { get; set; }
        /// <summary>
        /// The Type of Draw of this Employee.
        /// </summary>
        public int TypeofDraw { get; set; }
        /// <summary>
        /// The DR Percentage of this Employee.
        /// </summary>
        public Decimal DRPercentage { get; set; }
        /// <summary>
        /// The DD Period of this Employee.
        /// </summary>
        public DateTime DDPeriod { get; set; }
        /// <summary>
        /// The DD Amount of this Employee.
        /// </summary>
        public decimal DDAmount { get; set; }
        /// <summary>
        /// The Report Manager ID of this Employee.
        /// </summary>
        public int ReportMgr { get; set; }
        /// <summary>
        /// The Report Manager Name of this Employee.
        /// </summary>
        public string ReportMgrName { get; set; }
        /// <summary>
        /// The Approve Manager ID of this Employee.
        /// </summary>
        public int ApproveMgr { get; set; }
        /// <summary>
        /// The Approve Manager Name of this Employee.
        /// </summary>
        public string ApproveMgrName { get; set; }
        /// <summary>
        /// The E-mail of this Employee.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// The Draw Term of this Employee.
        /// </summary>
        public int DrawTerm { get; set; }
        /// <summary>
        /// The Created On of this Employee Record.
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// The Created By of this Employee Record.
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// The Modified On of this Employee Record.
        /// </summary>
        public DateTime ModifiedOn { get; set; }
        /// <summary>
        /// The Modified By of this Employee Record.
        /// </summary>
        public int ModifiedBy { get; set; }
        /// <summary>
        /// The Is Active of this Employee.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// The LastLogin of this Employee Record.
        /// </summary>
        public DateTime LastLogin { get; set; }
        /// <summary>
        /// The UserLog entry ID of this Employee Record.
        /// </summary>
        public int UserLogID { get; set; }
    }
}
