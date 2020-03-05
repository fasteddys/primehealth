<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Site2.Master"  AutoEventWireup="true" CodeBehind="SearchDetails.aspx.cs" Inherits="CardCycle.SearchDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="widget">
            <div class="widget-content padding">
                <div align="center"><h2>Search Details</h2> </div>
                <hr />
                <div class="col-md-12">
						<div class="widget">
							<div class="widget-header">
								<h2><strong>
                                    <asp:Label ID="txtBsubject" runat="server" Text="Label"></asp:Label></strong></h2>
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
									            <tr >
									                <td><strong>Subject</strong></td>
									                <td> <asp:Label ID="txtSubject" runat="server" Text="Label"></asp:Label></td>
									                <td><strong>Type</strong></td>
                                                    <td> <asp:Label ID="txtType" runat="server" Text="Label"></asp:Label></td>
									            </tr>

                                                <tr>
									                <td><strong>Client Name</strong></td>
									                <td> <asp:Label ID="txtClientName" runat="server" Text="Label"></asp:Label></td>
									                <td><strong>Cards Number</strong></td>
                                                    <td> <asp:Label ID="txtCardsNumber" runat="server" Text="Label"></asp:Label></td>
									            </tr>

                                               

                                                <tr>
									                <td><strong>Account Manager</strong></td>
									                <td>
                                                        <asp:Label ID="txtAM" runat="server" Text="Label"></asp:Label></td>
									                <td><strong>Recived</strong></td>
                                                    <td>
                                                        <asp:Label ID="txtAMTime" runat="server" Text="Label"></asp:Label></td>
									            </tr>
                                                                                                									            <tr>
									                <td><strong>Issued BY</strong></td>
									                <td>
                                                        <asp:Label ID="txtISname" runat="server" Text="Label"></asp:Label></td>
									                <td><strong>Time</strong></td>
                                                    <td>
                                                        <asp:Label ID="Isstime" runat="server" Text="Label"></asp:Label></td>
									            </tr>
                                                                                                                                     									            <tr>
									                <td><strong>Printed BY </strong> </td>
									                <td>
                                                        <asp:Label ID="txtPname" runat="server" Text="Label"></asp:Label></td>
									                <td><strong>Time</strong></td>
                                                    <td>
                                                        <asp:Label ID="txtPTime" runat="server" Text="Label"></asp:Label></td>
									            </tr>
                                                                                                                                                                                     									            <tr>
									                <td><strong>Quality Control BY</strong> </td>
									                <td>
                                                        <asp:Label ID="txtQC" runat="server" Text="Label"></asp:Label></td>
									                <td><strong>Time</strong></td>
                                                    <td>
                                                        <asp:Label ID="txtQCTime" runat="server" Text="Label"></asp:Label></td>
									            </tr>
                                                                                                                                                                                                                                     									            <tr>
									                <td><strong>Approved By</strong> </td>
									                <td>
                                                        <asp:Label ID="txtapvBy" runat="server" Text="Label"></asp:Label> </td>
									                <td><strong>Approved Time</strong></td>
                                                    <td><asp:Label ID="txtISappv" runat="server" Text="Label"></asp:Label></td>
									            </tr>
									            
                                                 <tr>
									                <td><strong>Admin Confirmed By  </strong></td>
									                <td> <asp:Label ID="txtAdminConfirm" runat="server" Text="Label"></asp:Label></td>
									                <td><strong>Admin Confirm Time</strong></td>
                                                    <td> <asp:Label ID="txtAdminTime" runat="server" Text="Label"></asp:Label></td>
									            </tr>

                                                 <tr>
									                <td><strong>Account Confirmed By  </strong></td>
									                <td> <asp:Label ID="txtAccountConfirm" runat="server" Text="Label"></asp:Label></td>
									                <td><strong>Account Confirm Time</strong></td>
                                                    <td> <asp:Label ID="txtAccountTime" runat="server" Text="Label"></asp:Label></td>
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
