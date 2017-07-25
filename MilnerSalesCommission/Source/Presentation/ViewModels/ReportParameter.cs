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
    public class ReportParameter
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ReportParameter() { }
        /// <summary>
        /// The Salesperson detail of this ReportParameter
        /// </summary>
        public int SalesPerson { get; set; }
        /// <summary>
        /// The ReportMonth of this ReportParameter
        /// </summary>
        public int ReportMonth { get; set; }
        /// <summary>
        /// The ReportYear of this ReportParameter
        /// </summary>
        public int ReportYear { get; set; }
        /// <summary>
        /// The ReportPeriod of this ReportParameter
        /// </summary>
        public int PlanID { get; set; }
        /// <summary>
        /// The location ID of this ReportParameter
        /// </summary>
        public int BranchID { get; set; }
        /// <summary>
        /// The MonthsofExperience of Employee of this ReportParameter
        /// </summary>
        public int MonthsofExp { get; set; }
        /// <summary>
        /// The ReportPeriod of this ReportParameter
        /// </summary>
        public DateTime ReportPeriod { get; set; }
        /// <summary>
        /// The Amount of this ReportParameter
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// The TotalCommission of this ReportParameter
        /// </summary>
        public decimal TotalCommission { get; set; }
        /// <summary>
        /// The CommissionDue of this ReportParameter
        /// </summary>
        public decimal CommissionDue { get; set; }
        /// <summary>
        /// The DrawPaid of this ReportParameter
        /// </summary>
        public decimal DrawPaid { get; set; }
        /// <summary>
        /// The DrawRecovered of this ReportParameter
        /// </summary>
        public decimal DrawRecovered { get; set; }
        /// <summary>
        /// The DrawDificit of this ReportParameter
        /// </summary>
        public decimal DrawDificit { get; set; }
        /// <summary>
        /// The ApplicableTill Period of this ReportParameter
        /// </summary>
        public string Applicabletill { get; set; }
        /// <summary>
        /// The DrawAmount of this ReportParameter
        /// </summary>
        public decimal DrawAmount { get; set; }
        /// <summary>
        /// The DrawType of this ReportParameter
        /// </summary>
        public int DrawType { get; set; }
        /// <summary>
        /// The RecoverablePercent of this ReportParameter
        /// </summary>
        public decimal RecoverablePercent { get; set; }
        /// <summary>
        /// The Bimonthreport of this ReportParameter
        /// </summary>
        public decimal Bimonthreport { get; set; }
        /// <summary>
        /// The Tenurereport of this ReportParameter
        /// </summary>
        public decimal Tenurereport { get; set; }
        /// <summary>
        /// The TotalEarning of this ReportParameter
        /// </summary>
        public decimal TotalEarning { get; set; }
        /// <summary>
        /// The Salary of this ReportParameter
        /// </summary>
        public decimal Salary { get; set; }
        /// <summary>
        /// The EmployeeID of this ReportParameter
        /// </summary>
        public string EmployeeID { get; set; }
        /// <summary>
        /// The ReportFrom period of this ReportParameter
        /// </summary>
        public DateTime ReportFrom { get; set; }
        /// <summary>
        /// The ReportTo period of this ReportParameter
        /// </summary>
        public DateTime ReportTo { get; set; }

        /// <summary>
        /// The Paylocity Config ID of this ReportParameter
        /// </summary>
        public int PayrollConfigID { get; set; }

        /// <summary>
        /// The Final Run of this ReportParameter
        /// </summary>
        public bool IsFinalRun { get; set; }

        public int RunNo { get; set; }

    }

    public class PaylocityReports
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PaylocityReports() { }
        /// <summary>
        /// The EmployeeID of this PaylocityReports
        /// </summary>
        public string EmployeeID { get; set; }
        /// <summary>
        /// The Earnings of this PaylocityReports
        /// </summary>
        public string Earnings { get; set; }
        /// <summary>
        /// The Commission of this PaylocityReports
        /// </summary>
        public string Commission { get; set; }
        /// <summary>
        /// The Amount of this PaylocityReports
        /// </summary>
        public Decimal Amount { get; set; }
    }
    public class GeneralLedgerReports
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public GeneralLedgerReports() { }
        /// <summary>
        /// The Reporting Month of this GeneralLedgerReports
        /// </summary>
        public DateTime Month { get; set; }
        /// <summary>
        /// The BranchName of this GeneralLedgerReports
        /// </summary>
        public string BranchName { get; set; }
        /// <summary>
        /// The TotalCommissionPaid of this GeneralLedgerReports
        /// </summary>
        public Decimal TotalCommissionPaid { get; set; }

    }
}
