using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using MemberPortal.Models;

namespace MemberPortal.ReportViewer
{
    public partial class ReportViewerTD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //PrintTDReport();
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Loans.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.Refresh();
            }
        }

        private void PrintTDReport()
        {
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Loans.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.Refresh();
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            PrintTDReport();
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Transaction/Index");
        }

    }
}