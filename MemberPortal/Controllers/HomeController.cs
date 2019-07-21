using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MemberPortal.Repository;
using MemberPortal.Models;
using MemberPortal.Interface;

namespace MemberPortal.Controllers
{
    public class HomeController : Controller
    {
        private ILoanCalc loan;

        //Dependdency Injection
        public HomeController()
        {
            this.loan = new LoanCalc(new PORTALEntities());
        }

        public HomeController(ILoanCalc _loanCalc)
        {
            this.loan = _loanCalc;
        }

        //Home Page
        public ActionResult Index()
        {
            return View();
        }

        //About
        public ActionResult About()
        {
            return View();
        }

        //Contact us
        public ActionResult Contact()
        {
            return View();
        }

        
        // Start Loan Calculator
        public ActionResult LoanCalc()
        {
            return View();
        }

        //Compute Lona Calculator
        public ActionResult GetLoanCalc(int termsData, decimal amountData)
        {
            try
            {
                var query = loan.ComputeLoanCalc(amountData, termsData);
                return View(query);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public PartialViewResult GetComputedLoans(int termsData, decimal amountData)
        {
            return PartialView(loan.ComputeLoanCalc(amountData, termsData).ToList());
        }


        protected override void Dispose(bool disposing)
        {
            loan.Dispose();
            base.Dispose(disposing);
        }
        // End Loan Calculator

    }
}