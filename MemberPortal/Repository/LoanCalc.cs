using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.Serialization;
using MemberPortal.Models;
using MemberPortal.Interface;

namespace MemberPortal.Repository
{
    public class LoanCalc: ILoanCalc, IDisposable
    {
        private PORTALEntities db;

        //constructor dependency
        public LoanCalc(PORTALEntities _context)
        {
            this.db = _context;
        }

        public List<usp_CreateAmortization_Result1> ComputeLoanCalc(decimal amount, int terms )
        {
            try
            {
                var query = db.usp_CreateAmortization(amount, terms);
                return query.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        // ****  Disposed Pattern *** //

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                };
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

        }


    }

}