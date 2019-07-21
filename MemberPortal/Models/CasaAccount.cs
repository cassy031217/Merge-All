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
    
    public partial class CasaAccount
    {
        public string BranchCode { get; set; }
        public string GroupCode { get; set; }
        public string AccountNumber { get; set; }
        public string CIFkey { get; set; }
        public string AccountName { get; set; }
        public string TypeOfAccount { get; set; }
        public string AccountStatus { get; set; }
        public string ProductCode { get; set; }
        public Nullable<System.DateTime> DateOpen { get; set; }
        public Nullable<System.DateTime> DateClosed { get; set; }
        public string PassbookNumber { get; set; }
        public Nullable<decimal> AccountBalance { get; set; }
        public Nullable<decimal> BeginningBalance { get; set; }
        public Nullable<decimal> HoldBalance { get; set; }
        public Nullable<System.DateTime> MaturityDate { get; set; }
        public Nullable<decimal> Principal { get; set; }
        public Nullable<int> Terms { get; set; }
        public Nullable<double> InterestRate { get; set; }
    
        public virtual CasaProduct CasaProduct { get; set; }
    }
}