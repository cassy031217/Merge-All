using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MemberPortal.Repository;
using System.Net;
using MemberPortal.Models;

namespace MemberPortal.Controllers
{
    public class HistoryController : Controller
    {
        ClientAccount context = new ClientAccount();

        public ActionResult SavingsAcctHist(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var getSavingsAccount = context.GetClientSavingsAccts(Global.AccountName).FirstOrDefault();
                ViewBag.AcctNo = getSavingsAccount.AcctNo;
                ViewBag.ProdDescription = getSavingsAccount.ProdName;
                ViewBag.Balance = getSavingsAccount.activeAcctBalance;
                ViewBag.Cifkey = getSavingsAccount.CIFkey;
                string cifkey = getSavingsAccount.CIFkey;

                var getSavingAcctLedger = context.GetClientSavingsLedger(id, cifkey);
                ViewBag.totalDebit = getSavingAcctLedger.Sum(i => i.Debit);
                ViewBag.totalCredit = getSavingAcctLedger.Sum(i => i.Credit);
                ViewBag.countDebit = getSavingAcctLedger.Count(i => i.Debit != 0);
                ViewBag.countCredit = getSavingAcctLedger.Count(i => i.Credit != 0);
                return View(getSavingAcctLedger);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult ShareCapitalAcctHist(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var getSharedCapitalAccount = context.GetClientSharedCapitalAccts(Global.AccountName).FirstOrDefault();
                ViewBag.AcctNo = getSharedCapitalAccount.AcctNo;
                ViewBag.ProdDescription = getSharedCapitalAccount.ProdName;
                ViewBag.Balance = getSharedCapitalAccount.activeAcctBalance;
                string cifkey = getSharedCapitalAccount.CIFkey;

                var getSharedCapitalLedger = context.GetClientSharedCapitalLedger(id, cifkey);
                ViewBag.totalDebit = getSharedCapitalLedger.Sum(i => i.Debit);
                ViewBag.totalCredit = getSharedCapitalLedger.Sum(i => i.Credit);
                ViewBag.countDebit = getSharedCapitalLedger.Count(i => i.Debit != 0);
                ViewBag.countCredit = getSharedCapitalLedger.Count(i => i.Credit != 0);
                return View(getSharedCapitalLedger);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult TimeDepositAcctHist(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var getTDAccount = context.GetClientTDAccts(Global.AccountName).FirstOrDefault();
                ViewBag.AcctNo = getTDAccount.AcctNo;
                ViewBag.ProdDescription = getTDAccount.ProdName;
                ViewBag.Balance = getTDAccount.activeAcctBalance;
                string cifkey = getTDAccount.CIFkey;

                var getTDLedger = context.GetClientTDLedger(id, cifkey);
                ViewBag.totalDebit = getTDLedger.Sum(i => i.Debit);
                ViewBag.totalCredit = getTDLedger.Sum(i => i.Credit);
                ViewBag.countDebit = getTDLedger.Count(i => i.Debit != 0);
                ViewBag.countCredit = getTDLedger.Count(i => i.Credit != 0);
                return View(getTDLedger);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult LoanAcctHist(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var getLoanAccount = context.GetClientLoanAccts(Global.AccountName).FirstOrDefault();
                ViewBag.AcctNo = getLoanAccount.AccountNumber;
                ViewBag.ProdDescription = getLoanAccount.ProductName;
                ViewBag.Balance = getLoanAccount.PrincipalBalance;
                int cifkey =Convert.ToInt32(getLoanAccount.IDX);

                var getLoanAccountLedger = context.GetClientLoanAcctLedger(getLoanAccount.AccountNumber,cifkey);
                ViewBag.totalPrincipalPaid = getLoanAccountLedger.Sum(i => i.Principal);
                ViewBag.totalInterestPaid = getLoanAccountLedger.Sum(i => i.Interest);
                ViewBag.totalPenalty = getLoanAccountLedger.Sum(i => i.Penalty);
                ViewBag.countPrincipalPaid = getLoanAccountLedger.Count(i => i.Principal != 0);
                ViewBag.countInterestPaid = getLoanAccountLedger.Count(i => i.Interest != 0);
                ViewBag.countPenalty = getLoanAccountLedger.Count(i => i.Penalty != 0);
                return View(getLoanAccountLedger);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
    }
}