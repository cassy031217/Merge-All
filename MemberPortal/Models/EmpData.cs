using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MemberPortal.Models
{
    public class EmpData
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Sex { get; set; }
        public string AccountName { get; set; }
        public string Cifkey { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime Birthdate { get; set; }
        public string EmailAddress { get; set; }
        public string AccessCode { get; set; }
        public string Branchcode { get; set; }
        public string Username { get; set; }
        public DateTime AcctCreation { get; set; }
        public Guid GUID { get; set; }
    }
}