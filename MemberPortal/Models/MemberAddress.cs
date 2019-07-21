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
    
    public partial class MemberAddress
    {
        public int Id { get; set; }
        public int MembershipID { get; set; }
        public string ProvCode { get; set; }
        public string CityMunCode { get; set; }
        public string BrgyCode { get; set; }
        public string Zipcode { get; set; }
        public string BuildingName { get; set; }
        public string Street { get; set; }
        public Nullable<bool> IsOwned { get; set; }
        public Nullable<bool> IsRent { get; set; }
        public Nullable<decimal> NumberofRentYear { get; set; }
        public Nullable<bool> IsLivingwParent { get; set; }
        public Nullable<bool> IsOther { get; set; }
        public string Other { get; set; }
        public string AddressType { get; set; }
    
        public virtual Barangay Barangay { get; set; }
        public virtual CityMunicipality CityMunicipality { get; set; }
        public virtual Membership Membership { get; set; }
        public virtual Province Province { get; set; }
    }
}