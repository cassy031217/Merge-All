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
    
    public partial class usp_GetMemberAcctList_Result2
    {
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> LoanAmount { get; set; }
        public Nullable<decimal> PrincipalBalance { get; set; }
        public int id { get; set; }
        public string AccountStatus { get; set; }
        public string CIFKey { get; set; }
    }
}