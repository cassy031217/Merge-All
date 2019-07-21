using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MemberPortal.Models;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace MemberPortal.ReportViewer
{
    public partial class ReportView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PrintReport();
            }
        }

        private void PrintReport()
        {
            //Global.ReportType = "Savings";
            switch (Global.ReportType)
            {
                case "Savings":

                    ReportViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/Savings.rdlc");
                    ReportViewer.LocalReport.DataSources.Clear();
                    ReportViewer.LocalReport.Refresh();

                    //LocalReport report = new LocalReport();
                    //report.ReportPath = Server.MapPath("~/Reports/Savings.rdlc");
                    //ReportDataSource rds = new ReportDataSource();

                    //Byte[] mybytes = report.Render("PDF");
                    //using (FileStream fs = File.Create(@"C:\Savings.pdf"))
                    //{
                    //    fs.Write(mybytes, 0, mybytes.Length);
                    //}



                    break;



                case "ShareCapital":
                    ReportViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/SharedCapital.rdlc");
                    ReportViewer.LocalReport.DataSources.Clear();
                    ReportViewer.LocalReport.Refresh();
                    break;
                case "TimeDeposit":
                    ReportViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/TimeDeposit.rdlc");
                    ReportViewer.LocalReport.DataSources.Clear();
                    ReportViewer.LocalReport.Refresh();
                    break;
                case "LoanApp":
                    ReportViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/LoanApps.rdlc");
                    ReportViewer.LocalReport.DataSources.Clear();
                    ReportViewer.LocalReport.Refresh();
                    break;
            }
        }


        protected void SaveButton_Click(object sender, EventArgs e)
        {
            Global.ReportType = "ShareCapital";
            switch (Global.ReportType)
            {
                case "Savings":
                    ReportViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/Savings.rdlc");
                    ReportViewer.LocalReport.DataSources.Clear();
                    ReportViewer.LocalReport.Refresh();
                    break;
                case "ShareCapital":
                    ReportViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/SharedCapital.rdlc");
                    ReportViewer.LocalReport.DataSources.Clear();
                    ReportViewer.LocalReport.Refresh();
                    break;
                case "TimeDeposit":
                    ReportViewer.LocalReport.ReportPath = Server.MapPath("~/Reports/SharedCapital.rdlc");
                    ReportViewer.LocalReport.DataSources.Clear();
                    ReportViewer.LocalReport.Refresh();
                    break;
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Transaction/Index");
        }
    }
}