using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemberPortal.Repository;
using MemberPortal.Models;


namespace MemberPortal.Interface
{
    public interface ILoanCalc
    {
        List<usp_CreateAmortization_Result1> ComputeLoanCalc(decimal amount, int terms);
        void Dispose();

    }
}
