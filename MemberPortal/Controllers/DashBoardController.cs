using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MemberPortal.Models;

namespace MemberPortal.Controllers
{
    public class DashBoardController : Controller
    {
       
        public ActionResult Index()
        {
            ViewBag.AcctName = Global.AccountName;
            ViewBag.LastLogin = DateTime.Now;     //Global.LastLoginDate.ToString("U");
            return View();
        }

        public ActionResult Support()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }

    }
}