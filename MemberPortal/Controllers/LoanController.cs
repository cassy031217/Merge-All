using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MemberPortal.Controllers
{
    public class LoanController : Controller
    {

        //can combine in Loan Application Controller
        public ActionResult LoanStatus()
        {
            return View();
        }
    }
}