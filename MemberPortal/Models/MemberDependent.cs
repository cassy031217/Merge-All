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
    
    public partial class MemberDependent
    {
        public int Id { get; set; }
        public Nullable<int> MembershipID { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string EducAttainment { get; set; }
    
        public virtual Membership Membership { get; set; }
    }
}