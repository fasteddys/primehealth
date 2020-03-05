<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="ClientsReports.aspx.cs" Inherits="WebApplication1.Portal.ClientsReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
      <style type="text/css">
        .auto-style2 {
            text-align: center;
            color: #FF0000;
        }
        .auto-style3 {
            font-size: small;
        }
        .auto-style4 {
            text-align: center;
            color: #FF0000;
            font-size: small;
        }
        .auto-style5 {
            text-align: left;
            color: #0078ff;
            font-size:large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="widget" align="Center">
        <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error1">
                    Sorry no data belongs to this date
                </div>
            <div class="widget-content padding">
                <div align="center"><h2>Client Monthly Report</h2> </div>
                <hr />
            <div class="col-md-12" > 
                <asp:Label ID="Label4" runat="server" Text="Client" Font-Bold="true"></asp:Label>
                <asp:DropDownList ID="ClientName" runat="server" CssClass="fa-align-center" Font-Bold="True" Font-Size="Medium" DataSourceID="ClientSearch" DataTextField="ClientName" DataValueField="ClientName"></asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="ClientSearch" ConnectionString='<%$ ConnectionStrings:ArchivingConnectionString1 %>' SelectCommand="SELECT DISTINCT [ClientName] FROM [Provider]"></asp:SqlDataSource>
                
                <asp:Label ID="Label2" runat="server" Text="Month" Font-Bold="true"></asp:Label>
                <asp:DropDownList ID="MonthNum" runat="server" CssClass="fa-align-center" Font-Bold="True" Width="150px" Font-Size="Medium" DataSourceID="MonthNumber" DataTextField="PMonth" DataValueField="PMonth"></asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="MonthNumber" ConnectionString='<%$ ConnectionStrings:ArchivingConnectionString1 %>' SelectCommand="SELECT DISTINCT [PMonth] FROM [Provider]"></asp:SqlDataSource>
                
                <asp:Label ID="Label3" runat="server" Text="Year" Font-Bold="true"></asp:Label>
                <asp:DropDownList ID="YearNum" runat="server" CssClass="fa-align-center" Font-Bold="True" Width="150px" Font-Size="Medium" DataSourceID="YearNumber" DataTextField="PYear" DataValueField="PYear"></asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="YearNumber" ConnectionString='<%$ ConnectionStrings:ArchivingConnectionString1 %>' SelectCommand="SELECT DISTINCT [PYear] FROM [Provider] ORDER BY [PYear]"></asp:SqlDataSource>
              <br />
              <br />
            <div class="col-sm-10" align="Center">
                <a class="btn btn-success" runat="server" onserverclick="btn_Search_QC_ServerClick" id="btn_QC_Report" ><i class="fa fa-check"></i> Get QC Report</a>
                <a class="btn btn-success" runat="server" onserverclick="btn_Search_TBA_ServerClick" id="btn_TBA_Report" ><i class="fa fa-check"></i> Get TBA Report</a>
            </div>
                <br />
						<div class="widget">
							<div class="widget-header">
								<h2><strong>
                                    </strong></h2>
								<div class="additional-btn" >
									<a href="#" class="hidden reload"><i class="icon-ccw-1"></i></a>
									<a href="#" class="widget-toggle"><i class="icon-down-open-2"></i></a>
									<a href="#" class="widget-close"><i class="icon-cancel-3"></i></a>
								</div>
							</div>
							
                            <div class="widget-content">
								<div class="table-responsive">
									<form class='form-horizontal' role="grid"  >
									<table id="datatables-1" class="table table-striped table-bordered" cellspacing="0" width="100%">
									        <tbody>
                                                 <tr>
									                <td colspan="6" class="auto-style2"><strong><span class="auto-style2" > Client Details <asp:Label ID="apvissu" runat="server"></asp:Label></span> </strong></td>    
									            </tr>
                                                 <tr>
									                <td class="auto-style5"><strong>Total Claims</strong></td>
									                <td>
                                                        <asp:Label ID="lbel1" runat="server" Text="Label"></asp:Label></td>
									                <td class="auto-style5"> <strong>Found Claims</strong></td>
                                                    <td>
                                                        <asp:Label ID="lbel2" runat="server" Text="Label"></asp:Label></td>
									            </tr>
                                                 <tr>
									                <td class="auto-style5"><strong>Missing Claims</strong></td>
									                <td>
                                                        <asp:Label ID="lbel3" runat="server" Text="Label"></asp:Label></td>
									                <td class="auto-style5"><strong>Dublicated Claims</strong></td>
                                                    <td>
                                                        <asp:Label ID="lbel4" runat="server" Text="Label"></asp:Label></td>
									            </tr>
                                                 <tr>
                                                <td class="auto-style5"><strong>InPatient Claims</strong></td>
									                <td>
                                                        <asp:Label ID="lbel5" runat="server" Text="Label"></asp:Label></td>
									                <td class="auto-style5"><strong>Wrong Claims</strong></td>
                                                    <td>
                                                        <asp:Label ID="lbel6" runat="server" Text="Label"></asp:Label></td>
                                                </tr>  
									        </tbody>
									    </table>
									</form>
								</div>
							</div>
						</div>
                
					</div>
                </div>

            </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
