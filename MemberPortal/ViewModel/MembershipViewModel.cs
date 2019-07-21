using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MemberPortal.Models;
using System.Web.Mvc;

namespace MemberPortal.ViewModel
{
    public class MembershipViewModel
    {
        public Membership Membership { get; set; }

        //public int Age
        //{
        //    get
        //    {
        //        var age = DateTime.Today.Year - Membership.BirthDate.Year;
        //        return age;
        //    }
        //}
    }

    public enum Gender
    {
        Male,
        Female
    }

    public enum CivilStatus
    {
        Single,
        Married,
        Divorced
    }
}