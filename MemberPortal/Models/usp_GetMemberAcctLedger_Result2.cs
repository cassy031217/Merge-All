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
    
    public partial class usp_GetMemberAcctLedger_Result2
    {
        public string CIFKey { get; set; }
        public string UserAccountNumber { get; set; }
        public string AccountName { get; set; }
        public string ProductCode { get; set; }
        public Nullable<int> LoanAccountId { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public string Description { get; set; }
        public string ReferenceNumber { get; set; }
        public Nullable<decimal> Principal { get; set; }
        public Nullable<decimal> Interest { get; set; }
        public Nullable<decimal> Penalty { get; set; }
        public Nullable<decimal> PrincipalEnding { get; set; }
    }
}
