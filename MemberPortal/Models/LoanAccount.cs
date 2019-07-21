//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MemberPortal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LoanAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoanAccount()
        {
            this.LoanLedgers = new HashSet<LoanLedger>();
        }
    
        public string CIFKey { get; set; }
        public string BranchCode { get; set; }
        public string GroupCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string ProductCode { get; set; }
        public Nullable<decimal> LoanAmount { get; set; }
        public Nullable<decimal> PrincipalBalance { get; set; }
        public Nullable<decimal> InterestBalance { get; set; }
        public Nullable<System.DateTime> ReleaseDate { get; set; }
        public Nullable<System.DateTime> MaturityDate { get; set; }
        public string AccountStatus { get; set; }
        public string ModeOfPayment { get; set; }
        public string FrequencyOfPayment { get; set; }
        public Nullable<int> NumberOfPayments { get; set; }
        public Nullable<decimal> InterestRate { get; set; }
        public string InterestMOP { get; set; }
        public string InterestFrequency { get; set; }
        public string InterestComputationMethod { get; set; }
        public string InterestComputationFrequency { get; set; }
        public int id { get; set; }
    
        public virtual LoanProductsMaster LoanProductsMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoanLedger> LoanLedgers { get; set; }
    }
}
