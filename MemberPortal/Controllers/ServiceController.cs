using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MemberPortal.Controllers
{
    public class ServiceController : Controller
    {
        // *** LOAN *** //
        public ActionResult LoanProduct()
        {
            return View();
        }
        
        public ActionResult BusinessLoan()
        {
            return View();
        }
        public ActionResult SalaryLoan()
        {
            return View();
        }
        // *** LOAN *** //


        //  *** Savings *** //
        public ActionResult SavingsProduct()
        {
            return View();
        }

        public ActionResult RegularSaving()
        {
            return View();
        }
        public ActionResult KiddieSaving()
        {
            return View();
        }

        public ActionResult ShareCapitalSaving()
        {
            return View();
        }

        public ActionResult TDSaving()
        {
            return View();
        }
        //  *** Savings *** //


        //DAMAYAN
        public ActionResult Damayan()
        {
            return View();
        }

    }
}