using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MemberPortal.Repository;
using System.Net;
using MemberPortal.Models;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;

namespace MemberPortal.Controllers
{
    public class TransactionController : Controller
    {
        ClientAccount context = new ClientAccount();
        public string _datefrom { get; set; }
        public string _dateTo { get; set; }



        //Home
        public ActionResult Index()
        {
            ViewBag.AcctName = Global.AccountName;
            return View(context);
        }


        [HttpPost]
        public ActionResult ExportSavingsAccount(SelectDateRange dateRange , string _acctno, string _cifkey, string _acctName)
        {

            switch (dateRange.DateRangeValue)
            {
                case "01/01/2019 To 03/31/2019":
                    _datefrom = "01/01/2019";
                    _dateTo = "03/31/2019";
                    break;
                case "04/01/2019 To 06/30/2019":
                    _datefrom = "04/01/2019";
                    _dateTo = "06/30/2019";
                    break;
                case "07/01/2019 To 09/30/2019":
                    _datefrom = "07/01/2019";
                    _dateTo = "09/30/2019";
                    break;
                case "10/01/2019 To 12/31/2019":
                    _datefrom = "10/01/2019";
                    _dateTo = "12/31/2019";
                    break;
                default:
                    _datefrom = "01/01/2019";
                    _dateTo = "12/31/2019";
                    break;
            }

            List<PrintSavingsAccount_Result> allCustomer = new List<PrintSavingsAccount_Result>();
            allCustomer = context.GetSavingsReport("00101000954", "001009", "01/01/2019", "03/30/2019");
            if (allCustomer.Count() == 0)
            {
                
                //Alert Sana pag wala data
                return RedirectToAction("SavingsAcct", "SOA", new { accountName = _acctName, acctno = _acctno, cifkey = _cifkey });
            }
            ReportDocument rd = new ReportDocument();
            rd.Load(Server.MapPath("~/Reports/printsavingsaccount.rpt"));
            rd.SetDataSource(allCustomer);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, true, "crReport");
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "CustomerList.pdf");

        }
    }
  }
