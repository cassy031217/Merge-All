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
    
    public partial class usp_GetSavingAcctLedger_Result1
    {
        public string AcctNo { get; set; }
        public string AccountName { get; set; }
        public string ProductCode { get; set; }
        public string ProdName { get; set; }
        public string TypeOfAccount { get; set; }
        public string Description { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public string ReferenceNumber { get; set; }
        public Nullable<decimal> BeginningBalance { get; set; }
        public Nullable<decimal> Debit { get; set; }
        public Nullable<decimal> Credit { get; set; }
        public Nullable<decimal> EndingBalance { get; set; }
    }
}
