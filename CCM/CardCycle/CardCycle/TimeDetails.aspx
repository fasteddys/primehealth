<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Site2.Master" AutoEventWireup="true" CodeBehind="TimeDetails.aspx.cs" Inherits="CardCycle.TimeDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style2 {
            text-align: center;
            color: #FF0000;
            font-size: medium;
        }
        .auto-style3 {
            font-size: small;
        }
        .auto-style4 {
            text-align: center;
            color: #FF0000;
            font-size: small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="widget">
            <div class="widget-content padding">
                <div align="center"><h2>Time Details</h2> </div>
                <br />
                <div class="col-sm-12" align="Center">
                             <label runat="server" id="Label5" style="font-size:38px;"></label>
							</div>
                <hr />
                <div class="col-md-12">
						<div class="widget">
							<div class="widget-header">
								<h2><strong>
                                    </strong></h2>
								<div class="additional-btn">
									<a href="#" class="hidden reload"><i class="icon-ccw-1"></i></a>
									<a href="#" class="widget-toggle"><i class="icon-down-open-2"></i></a>
									<a href="#" class="widget-close"><i class="icon-cancel-3"></i></a>
								</div>
							</div>
							<div class="widget-content">
							<br>					
								<div class="table-responsive">
									<form class='form-horizontal' role='form'>
									<table id="datatables-1" class="table table-striped table-bordered" cellspacing="0" width="100%">

							
									 
									        <tbody>
									            <tr>
									                <td><strong>Subject</strong></td>
									                <td> <asp:Label ID="txtSubject" runat="server" Text="Label"></asp:Label></td>
									                <td><strong>Type</strong></td>
                                                    <td>
                                                        <asp:Label ID="txtType" runat="server" Text="Label"></asp:Label></td>
									            </tr>
                                                									            <tr>
									                <td><strong>Recived</strong></td>
									                <td>
                                                        <asp:Label ID="txtAM" runat="server" Text="Label"></asp:Label></td>
									                <td><strong></strong></td>
                                                    <td>
                                                        </td>
									           
                                                 </tr>
                                                 <tr>
									                <td colspan="4" class="auto-style2"><strong><span class="auto-style3">Issued BY <asp:Label ID="apvissu" runat="server"></asp:Label></span> </strong></td>
									               
									                
									            </tr>
                                                   <tr>
									                <td><strong>Start Time</strong></td>
									                <td>
                                                        <asp:Label ID="lbel1" runat="server" Text="Label"></asp:Label></td>
									                <td><strong>End Time</strong></td>
                                                    <td>
                                                        <asp:Label ID="lbel2" runat="server" Text="Label"></asp:Label></td>
									            </tr>
                                                                                                                                     									            <tr>
									                <td><strong>Start Date</strong></td>
									                <td>
                                                        <asp:Label ID="lbel3" runat="server" Text="Label"></asp:Label></td>
									                <td><strong>End Date</strong></td>
                                                    <td>
                                                        <asp:Label ID="lbel4" runat="server" Text="Label"></asp:Label></td>
									            </tr>
                                                <tr>
									                <td colspan="4" class="auto-style4"><strong>Printed BY <asp:Label ID="apvP" runat="server"></asp:Label></strong></td>
									               
									                
									            </tr>
                                                                                                                                                                                     									            <tr>
									                <td><strong>Start Time</strong> </td>
									                <td>
                                                        <asp:Label ID="lbel5" runat="server" Text="Label"></asp:Label></td>
									                <td><strong>End Time</strong></td>
                                                    <td>
                                                        <asp:Label ID="lbel6" runat="server" Text="Label"></asp:Label></td>
									            </tr>
                                                                                                                                                                                                                                     									            <tr>
									                <td><strong>Start Date </strong> </td>
									                <td>
                                                        <asp:Label ID="lbel7" runat="server" Text="Label"></asp:Label> </td>
									                <td><strong>End Date</strong></td>
                                                    <td><asp:Label ID="lbel8" runat="server" Text="Label"></asp:Label></td>
									            </tr>
                                                <tr>
									                <td colspan="4" class="auto-style4"><strong>Quality Control BY <asp:Label ID="txtQ" runat="server"></asp:Label></strong></td>
									               
									                
									            </tr>
                                                                                                                                                                                     									            <tr>
									                <td><strong>Start Time</strong> </td>
									                <td>
                                                        <asp:Label ID="lbel9" runat="server" Text="Label"></asp:Label></td>
									                <td><strong>End Time</strong></td>
                                                    <td>
                                                        <asp:Label ID="lbel10" runat="server" Text="Label"></asp:Label></td>
									            </tr>
                                                                                                                                                                                                                                     									            <tr>
									                <td><strong>Start Date</strong> </td>
									                <td>
                                                        <asp:Label ID="lbel11" runat="server" Text="Label"></asp:Label> </td>
									                <td><strong>End Date</strong></td>
                                                    <td><asp:Label ID="lbel12" runat="server" Text="Label"></asp:Label></td>
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
