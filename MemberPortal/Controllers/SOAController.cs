using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MemberPortal.Models;
using Microsoft.Reporting.WebForms;
using System.IO;
using MemberPortal.Repository;
using System.Net;

namespace MemberPortal.Controllers
{
    public class SOAController : Controller
    {
        // GET: SOA

        ClientAccount context = new ClientAccount();

        public ActionResult SavingsAcct(string accountName, string acctno, string cifkey)
        {
            Global.ReportType = "Savings";
            if (accountName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.AcctName = accountName;
            ViewBag.AcctNo = acctno;
            ViewBag.Cifkey = cifkey;
            return View();
        }

        public String GetString(String Type)
        {
            String sReturn = "Test";
            switch (Type)
            {
                case "1":
                    sReturn = "Test 1";
                    break;
                case "2":
                    sReturn = "Test 2";
                    break;
            }
            return sReturn;
        }


        public ActionResult SCAcct(string accountName, int acctno)
        {
            Global.ReportType = "ShareCapital";
            ViewBag.AcctName = accountName;
            ViewBag.AcctNo = acctno;
            return View();
        }

        public ActionResult TDAcct(string accountName, int acctno)
        {
            Global.ReportType = "TimeDeposit";
            ViewBag.AcctName = accountName;
            ViewBag.AcctNo = acctno;
            return View();
        }
        public ActionResult LoanAcct()
        {
            Global.ReportType = "LoanApp";
            return View();
        }
    }
}