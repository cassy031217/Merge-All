<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewerTD.aspx.cs" Inherits="MemberPortal.ReportViewer.ReportViewerTD" %>


<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Viewer</title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
      <br />
        <section id="about" class="wow fadeInUp">
            <div class="container">
                <div class="section-header">
                    <h2>View Report</h2>
                    <hr />
                </div>
                <div class="row">
                    <div class="col-md-8 mb-4">
                        <br />
                            <label class="col-md-4 control-label"><strong class="text-primary">Statement of Account</strong></label>
                            <div class="col-md-6">
                                <select class="custom-select" id="inputGroupSelect02">
                                    <option value="1">January 31, 2019 </option>
                                    <option value="2">February 28, 2019 </option>
                                    <option value="3">March 31, 2019 </option>
                                    <option value="4">April 30, 2019 </option>
                                    <option value="5">May 30, 2019 </option>
                                    <option value="6">June 30, 2019 </option>
                                    <option value="7">July 31, 2019 </option>
                                    <option value="8">August 31, 2019 </option>
                                    <option value="9">September 30, 2019 </option>
                                    <option value="10">October 31, 2019 </option>
                                    <option value="11">November 30, 2019 </option>
                                    <option value="12">December 31, 2019 </option>
                                </select>
                                <br />
                                <br />
                                <div class="input-group">
                                 <asp:Button ID="SaveButton" runat="server" Text="OK" OnClick = "SaveButton_Click" class="btn btn-success btn-sm"/>
                                 &nbsp;&nbsp;
                                 <asp:Button ID="CancelButton" runat="server" Text="View Other Account"  OnClick = "CancelButton_Click" class="btn btn-info btn-sm"/>
                                </div>
                            </div>
                            <br />
                    </div>
                    <br />
                 
                 </div>
                <div class="row">
                    <div class="alert alert-dark">
                        <div class="col-md-8 order-md-1">    
                            <div class="form-group">
          
                                 <div class="alert alert-light">
  
                                     <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="False" ZoomMode="FullPage"
                                         InternalBorderStyle="None" InternalBorderWidth="0px" ToolBarItemBorderStyle="none"
                                         Height="1000px" Width="1000px">
                                    </rsweb:ReportViewer>

                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                </div>

                                <br />
                              
                            </div>
                        </div>
                    </div>

                </div>
                <br />
             </div>
        </section>
    </form>
</body>
</html>
