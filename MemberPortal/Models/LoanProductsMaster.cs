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
    
    public partial class LoanProductsMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoanProductsMaster()
        {
            this.LoanAccounts = new HashSet<LoanAccount>();
        }
    
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ModeOfPayment { get; set; }
        public string FrequencyOfPayment { get; set; }
        public Nullable<int> NumberOfPayments { get; set; }
        public Nullable<decimal> InterestRate { get; set; }
        public string InterestModeofPayment { get; set; }
        public string InterestComputationMethod { get; set; }
        public string InterestFrequency { get; set; }
        public string InterestComputationFrequency { get; set; }
        public Nullable<int> AdvancedInterest { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoanAccount> LoanAccounts { get; set; }
    }
}
