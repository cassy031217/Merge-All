using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MemberPortal.Models;

namespace MemberPortal.Repository
{
    public class ClientAccount
    {

        PORTALEntities db = new PORTALEntities();

        //Savings Account Header
        public IEnumerable<usp_GetMemberSavingsAcct_Result3> GetClientSavingsAccts(string _acctName)
        {
            var getsavings = db.usp_GetMemberSavingsAcct(_acctName);
            return getsavings;
        }

        //Savings Account Details
        public List<usp_GetSavingAcctLedger_Result1> GetClientSavingsLedger(int _acctno, string _cifkey)
        {
            try
            {
                var query = db.usp_GetSavingAcctLedger(_acctno, _cifkey);
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Report Savings
        public List<PrintSavingsAccount_Result> GetSavingsReport(string _acctno, string _cifkey, string _datefrom, string _dateTo)
        {
            try
            {
                var query = db.PrintSavingsAccount(_acctno, _cifkey, _datefrom, _dateTo);
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Shared Capital
        public IEnumerable<usp_GetMemberSCAcct_Result> GetClientSharedCapitalAccts(string _acctName)
        {
            var getsharedCapital = db.usp_GetMemberSCAcct(_acctName);
            return getsharedCapital;
        }

        //Shared Capital Account Details
        public List<usp_GetSharedCapitalLedger_Result> GetClientSharedCapitalLedger(int _acctno, string _cifkey)
        {
            try
            {
                var query = db.usp_GetSharedCapitalLedger(_acctno, _cifkey);
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Time Deposit Capital
        public IEnumerable<usp_GetTimeDepositAcct_Result> GetClientTDAccts(string _acctName)
        {
            var getTD = db.usp_GetTimeDepositAcct(_acctName);
            return getTD;
        }

        //Shared Capital Account Details
        public List<usp_GetTimeDepositLedger_Result> GetClientTDLedger(int _acctno, string _cifkey)
        {
            try
            {
                var query = db.usp_GetTimeDepositLedger(_acctno, _cifkey);
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Loan Accounts
        public IEnumerable<usp_GetMemberAcctList_Result4> GetClientLoanAccts(string _acctno)
        {
            var getloan = db.usp_GetMemberAcctList(_acctno);
            return getloan;
        }

        //Loan Account Ledger
        public List<usp_GetMemberAcctLedger_Result2> GetClientLoanAcctLedger(string _acctno,int _cifkey)
        {
            try
            {
                var query = db.usp_GetMemberAcctLedger(_acctno,_cifkey);
                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Get Creation Date as Activation Code via verification code

        public DateTime GetCreationDate(Guid activationLink)
        {
            try
            {
                var getCreationDate = (from p in db.CIFOnlineUsers where p.ActivationCode == activationLink select p).FirstOrDefault();
                return Convert.ToDateTime(getCreationDate.CreationDate);
            }
            catch (Exception ex)
            { 
                throw ex;
            }

        }

        //END
    
    }
}