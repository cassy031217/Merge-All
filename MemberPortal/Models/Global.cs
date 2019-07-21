using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace MemberPortal.Models
{
    public class Global
    {
        public static string UserName { get; set; }
        public static string AccountName { get; set; }
        public static int ID { get; set; }
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string MiddleName { get; set; }

        public static string Gender { get; set; }

        public static string Cifkey { get; set; }
        public static DateTime? LastLoginDate { get; set; }
        public static DateTime Birthdate { get; set; }
        public static string EmailAddress { get; set; }
        public static string RegCifKey { get; set; }
        public static string UserGUID { get; set; }
        public static string AccessCode { get; set; }
        public static string Branchcode { get; set; }
        public static DateTime AcctCreation { get; set; }
        public static Guid AcctGuid { get; set; }
        public static string ReportType { get; set; }
        public static string AcctGuidLink { get; set; }
    }

    public class LoanCalculator
    {
        public int LoanID { get; set; }
        public int MonthNo { get; set; }
        public decimal Principal { get; set; }
        public decimal Interest { get; set; }
        public decimal Total { get; set; }
        public decimal Balances { get; set; }

    }

    public class LoanCalcContext : DbContext
    {
        public DbSet<LoanCalculator> Calculator { get; set; }
    }


}