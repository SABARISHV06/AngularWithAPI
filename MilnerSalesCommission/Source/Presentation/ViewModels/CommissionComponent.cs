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
    public class CommissionComponent
    {
        /// <summary>
        /// Default Constructors
        /// </summary>
        public CommissionComponent() { }
        /// <summary>
        /// The unique ID of this Commission.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The SalesPerson Name of this Commission.
        /// </summary>
        public string SalesPerson { get; set; }
        /// <summary>
        /// The Date of Sale of this Commission.
        /// </summary>
        public DateTime DateofSale { get; set; }
        /// <summary>
        /// The EntryDate of this Commission.
        /// </summary>
        public DateTime EntryDate { get; set; }
        /// <summary>
        /// The InvoiceNumber of this Commission.
        /// </summary>
        public string InvoiceNumber { get; set; }
        /// <summary>
        /// The Customer Number of this Commission.
        /// </summary>
        public string CustomerNumber { get; set; }
        /// <summary>
        /// The Customer Name of this Commission.
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// The Comment Sold of this Commission.
        /// </summary>
        public string CommentSold { get; set; }
        /// <summary>
        /// The Customer Type of this Commission.
        /// </summary>
        public int CustomerType { get; set; }
        /// <summary>
        /// The Split Sale Person of this Commission.
        /// </summary>
        public string SplitSalePerson { get; set; }
        /// <summary>
        /// The Split Sale Person ID of this Commission.
        /// </summary>
        public string SplitSalePersonID { get; set; }
        /// <summary>
        /// The Amount of Sale of this Commission.
        /// </summary>
        public Decimal AmountofSale { get; set; }
        /// <summary>
        /// The Cost of Goods of this Commission.
        /// </summary>
        public Decimal CostofGoods { get; set; }
        /// <summary>
        /// The Dollar Volume of this Commission.
        /// </summary>
        public Decimal DollarVolume { get; set; }
        /// <summary>
        /// The Base Commission of this Commission.
        /// </summary>
        public Decimal BaseCommission { get; set; }
        /// <summary>
        /// The Lease Commission of this Commission.
        /// </summary>
        public Decimal LeaseCommission { get; set; }
        /// <summary>
        /// The Service Commission of this Commission.
        /// </summary>
        public Decimal ServiceCommission { get; set; }
        /// <summary>
        /// The Travel Commission of this Commission.
        /// </summary>
        public Decimal TravelCommission { get; set; }
        /// <summary>
        /// The Cash Commission of this Commission.
        /// </summary>
        public Decimal CashCommission { get; set; }
        /// <summary>
        /// The Special Commission of this Commission.
        /// </summary>
        public Decimal SpecialCommission { get; set; }
        /// <summary>
        /// The MainCommissionId of this Commission.
        /// </summary>
        public int MainCommissionID { get; set; }
        /// <summary>
        /// The TipLeadID of this Commission.
        /// </summary>
        public int TipLeadID { get; set; }
        /// <summary>
        /// The TipLeadEmpID of this Commission.
        /// </summary>
        public string TipLeadEmpID { get; set; }
        /// <summary>
        /// The TipLeadName of this Commission.
        /// </summary>
        public string TipLeadName { get; set; }
        /// <summary>
        /// The TipLeadAmount of this Commission.
        /// </summary>
        public Decimal TipLeadAmount { get; set; }
        /// <summary>
        /// The PositiveAdjustments of this Commission.
        /// </summary>
        public Decimal PositiveAdjustments { get; set; }
        /// <summary>
        /// The NegativeAdjustments of this Commission.
        /// </summary>
        public Decimal NegativeAdjustments { get; set; }
        /// <summary>
        /// The CompanyContribution of this Commission.
        /// </summary>
        public Decimal CompanyContribution { get; set; }
        /// <summary>
        /// The SlipType of this Commission.
        /// </summary>
        public int SlipType { get; set; }
        /// <summary>
        /// The TotalCEarned of this Commission.
        /// </summary>
        public Decimal TotalCEarned { get; set; }
        /// <summary>
        /// The TradIn of this Commission.
        /// </summary>
        public Decimal TradeIn { get; set; }
        /// <summary>
        /// The Status of this Commission.
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// The Account Period of this Commission Record.
        /// </summary>
        public String AccountPeriod { get; set; }
        /// <summary>
        /// The Account Period ID of this Commission Record.
        /// </summary>
        public int AccountPeriodID { get; set; }
        /// <summary>
        /// The Proces by Payroll of this Commission Record.
        /// </summary>
        public bool ProcesByPayroll { get; set; }
        /// <summary>
        /// The Branch ID of this Commission.
        /// </summary>
        public int BranchID { get; set; }
        /// <summary>
        /// The Sale Type of this Commission.
        /// </summary>
        public int SaleType { get; set; }
        /// <summary>
        /// The Product Line of this Commission.
        /// </summary>
        public int ProductLine { get; set; }
        /// <summary>
        /// The Comments of this Commission.
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// The List of Comments of this Commission.
        /// </summary>
        public List<CommissionComponent> CommentList { get; set; }
        /// <summary>
        /// The Comments added by name of this Commission.
        /// </summary>
        public string CommentedBy { get; set; }
        /// <summary>
        /// The Created On of this Commission Record.
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// The Created By of this Commission Record.
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// The Modified On of this Commission Record.
        /// </summary>
        public DateTime ModifiedOn { get; set; }
        /// <summary>
        /// The Modified By of this Commission Record.
        /// </summary>
        public int ModifiedBy { get; set; }
        /// <summary>
        /// The Is Active of this Commission.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// The IS Mail Notification of this Commission
        /// </summary>
        public bool IsNotified { get; set; }
        /// <summary>
        /// The Branch Name of this Commission.
        /// </summary>
        public string BranchName { get; set; }
        /// <summary>
        /// The Plan ID of this Commission Record Created by.
        /// </summary>
        public bool IsPlanMapped { get; set; }
        /// <summary>
        /// The UID of this Commission Record Created by.
        /// </summary>
        public bool IsGMActive { get; set; }
        /// <summary>
        /// The List of TipLeadslip of this Commission.
        /// </summary>
        public List<TipLeadSlip> TipLeadSliplist { get; set; }
    }
    public class TipLeadSlip
    {
        /// <summary>
        /// Default Constructors
        /// </summary>
        public TipLeadSlip() { }
        /// <summary>
        /// The unique ID of this TipLeadSlip.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The MainCommissionID of this TipLeadSlip
        /// </summary>
        public int MainCommissionID { get; set; }
        /// <summary>
        /// The TipLeadID of this TipLeadSlip.
        /// </summary>
        public int TipLeadID { get; set; }
        /// <summary>
        /// The TipLeadEmpID of this TipLeadSlip.
        /// </summary>
        public string TipLeadEmpID { get; set; }
        /// <summary>
        /// The TipLeadName of this TipLeadSlip.
        /// </summary>
        public string TipLeadName { get; set; }
        /// <summary>
        /// The TipLeadAmount of this TipLeadSlip.
        /// </summary>
        public Decimal TipLeadAmount { get; set; }
        /// <summary>
        /// The PositiveAdjustments of this TipLeadSlip.
        /// </summary>
        public Decimal PositiveAdjustments { get; set; }
        /// <summary>
        /// The NegativeAdjustments of this TipLeadSlip.
        /// </summary>
        public Decimal NegativeAdjustments { get; set; }
        /// <summary>
        /// The CompanyContribution of this TipLeadSlip.
        /// </summary>
        public Decimal CompanyContribution { get; set; }
        /// <summary>
        /// The SlipType of this TipLeadSlip.
        /// </summary>
        public int SlipType { get; set; }
        /// <summary>
        /// The TotalCEarned of this TipLeadSlip.
        /// </summary>
        public Decimal TotalCEarned { get; set; }
        /// <summary>
        /// The Status of this TipLeadSlip.
        /// </summary>
        public int Status { get; set; }
    }
    /// <summary>
    /// The Collection of this TipLeadSlip.
    /// </summary>
    public class TipLeadSlipCollection : List<TipLeadSlip>, IEnumerable<SqlDataRecord>
    {
        IEnumerator<SqlDataRecord> IEnumerable<SqlDataRecord>.GetEnumerator()
        {
            var TipLeadSlipRow = new SqlDataRecord(
                  new SqlMetaData("ID", SqlDbType.Int),
                  new SqlMetaData("MainCommissionID", SqlDbType.Int),
                  new SqlMetaData("TipLeadID", SqlDbType.Int),
                  new SqlMetaData("TipLeadEmpID", SqlDbType.NVarChar, 50),
                  new SqlMetaData("TipLeadName", SqlDbType.NVarChar, 50),
                  new SqlMetaData("TipLeadAmount", SqlDbType.Decimal, 20, 2),
                  new SqlMetaData("PositiveAdjustments", SqlDbType.Decimal, 20, 2),
                  new SqlMetaData("NegativeAdjustments", SqlDbType.Decimal, 20, 2),
                  new SqlMetaData("CompanyContribution", SqlDbType.Decimal, 20, 2),
                  new SqlMetaData("SlipType", SqlDbType.Int),
                  new SqlMetaData("TotalCEarned", SqlDbType.Decimal, 20, 2),
                  new SqlMetaData("Status",SqlDbType.Int)
                      );

            foreach (TipLeadSlip Tipleadslip in this)
            {
                TipLeadSlipRow.SetInt32(0, Tipleadslip.ID);
                TipLeadSlipRow.SetInt32(1, Tipleadslip.MainCommissionID);
                TipLeadSlipRow.SetInt32(2, Tipleadslip.TipLeadID);
                TipLeadSlipRow.SetString(3, Tipleadslip.TipLeadEmpID!=null? Tipleadslip.TipLeadEmpID:string.Empty);
                TipLeadSlipRow.SetString(4, Tipleadslip.TipLeadName!=null? Tipleadslip.TipLeadName:string.Empty);
                TipLeadSlipRow.SetDecimal(5, Tipleadslip.TipLeadAmount);
                TipLeadSlipRow.SetDecimal(6, Tipleadslip.PositiveAdjustments);
                TipLeadSlipRow.SetDecimal(7, Tipleadslip.NegativeAdjustments);
                TipLeadSlipRow.SetDecimal(8, Tipleadslip.CompanyContribution);
                TipLeadSlipRow.SetInt32(9, Tipleadslip.SlipType);
                TipLeadSlipRow.SetDecimal(10, Tipleadslip.TotalCEarned);
                TipLeadSlipRow.SetInt32(11, Tipleadslip.Status);

                yield return TipLeadSlipRow;

            }
        }
    }
}
