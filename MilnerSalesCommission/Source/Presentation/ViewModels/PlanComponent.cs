using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
   public class PlanComponent
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PlanComponent() { }
       /// <summary>
       /// The Unique ID of this Plan
       /// </summary>
        public int PlanID { get; set; }
        /// <summary>
        /// The PlanName of this Plan
        /// </summary>
        public string PlanName { get; set; }
        /// <summary>
        /// The BasisType of this Plan
        /// </summary>
        public int BasisType { get; set; }
        /// <summary>
        /// The BMQuotaBonus of this Plan
        /// </summary>
        public bool BMQuotaBonus { get; set; }
        /// <summary>
        /// The SMEligible of this Plan
        /// </summary>
        public bool SMEligible { get; set; }
        /// <summary>
        /// The TenureBonus of this Plan
        /// </summary>
        public bool TenureBonus { get; set; }
        /// <summary>
        /// The CreatedOn date of this Plan
        /// </summary>
        public DateTime CreatedOn { get; set; }
        /// <summary>
        /// The CreatedBy of this Plan
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// The ModifiedOn date of this Plan
        /// </summary>
        public DateTime ModifiedOn { get; set; }
        /// <summary>
        /// The ModifiedBy of this Plan
        /// </summary>
        public int ModifiedBy { get; set; }
        /// <summary>
        /// The IsActive of this Plan 
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// The TGPcustomerInfo list of this Plan
        /// </summary>
        public List<TGPCustomerInfo> TGPcustomerlist { get; set; }
        /// <summary>
        /// The TenureBonus list of this Plan
        /// </summary>
        public List<TenureBonus> TenureBonuslist { get; set; }
        /// <summary>
        /// The Bimonthly list of this Plan
        /// </summary>
        public List<BIMonthlyBonusInfo> Bimonthlylist { get; set; }
        
    }
}
