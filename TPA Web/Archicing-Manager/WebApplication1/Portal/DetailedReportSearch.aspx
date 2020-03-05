<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="DetailedReportSearch.aspx.cs" Inherits="WebApplication1.Portal.DetailedReportSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="../assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css"/>
<link href="../assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css"/>
<link href="../assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
									                <td><strong>Provider Name</strong></td>
									                <td> <asp:Label ID="ProvName" runat="server" Text="Label"></asp:Label></td>
									                <td><strong>Client Name</strong></td>
                                                    <td> <asp:Label ID="Clname" runat="server" Text="Label"></asp:Label></td>
									            </tr>

                                               <tr>
                                                    <td><strong>Tottal Claims</strong> </td>
									                <td><asp:Label ID="txtTotClaims" runat="server" Text="Label"></asp:Label></td>
									                <td><strong>Tottal Found Claims</strong></td>
                                                    <td><asp:Label ID="txtFoundClaims" runat="server" Text="Label"></asp:Label></td>
									            </tr>

                                                <tr>                                                                                                                                                 
									                <td><strong>Tottal Missing Claims</strong> </td>
									                <td><asp:Label ID="txtMissClaims" runat="server" Text="Label"></asp:Label> </td>
									                <td><strong>Tottal InPatient Claims</strong></td>
                                                    <td><asp:Label ID="txtInpatientlaims" runat="server" Text="Label"></asp:Label></td>
									            </tr>

                                                <tr>
									                <td><strong>Tottal Wrong Claims  </strong></td>
									                <td> <asp:Label ID="txtWrongClaims" runat="server" Text="Label"></asp:Label></td>
									                <td><strong>Tottal Dublicated Claims</strong></td>
                                                    <td> <asp:Label ID="txtDublClaims" runat="server" Text="Label"></asp:Label></td>
									            </tr>

                                                <tr>
									                <td><strong>Creator</strong></td>
									                <td> <asp:Label ID="txtCereator" runat="server" Text="Label"></asp:Label></td>
									                <td><strong>Creation Date</strong></td>
                                                    <td> <asp:Label ID="txtCerDate" runat="server" Text="Label"></asp:Label></td>
									            </tr>                                            
									            
                                                <tr>
									                <td><strong>DataEntry Name</strong></td>
									                <td><asp:Label ID="txtDEntryName" runat="server" Text="Label"></asp:Label></td>
									                <td><strong>Accept Date</strong></td>
                                                    <td><asp:Label ID="txtDEntryAcceptDate" runat="server" Text="Label"></asp:Label></td>
									            </tr>
                                                   
                                                <tr>                                             									           
									                <td><strong>Quality By</strong></td>
									                <td><asp:Label ID="txtQCname" runat="server" Text="Label"></asp:Label></td>
									                <td><strong>Quality Date</strong></td>
                                                    <td><asp:Label ID="txtQualityDate" runat="server" Text="Label"></asp:Label></td>
									            </tr>
                                                  <tr>
									                <td><strong>Work TPA By </strong> </td>
									                <td><asp:Label ID="txtTBAName" runat="server" Text="Label"></asp:Label></td>
									                <td><strong>Closed Date</strong></td>
                                                    <td><asp:Label ID="txtTbaDate" runat="server" Text="Label"></asp:Label></td>
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
            <script src="../assets/plugins/jquery-datatable/media/js/jquery.dataTables.min.js" type="text/javascript"></script>
<script src="../assets/plugins/jquery-datatable/extensions/TableTools/js/dataTables.tableTools.min.js" type="text/javascript"></script>
<script src="../assets/plugins/jquery-datatable/extensions/Bootstrap/jquery-datatable-bootstrap.js" type="text/javascript"></script>
<script type="text/javascript" src="../assets/plugins/datatables-responsive/js/datatables.responsive.js"></script>
<script type="text/javascript" src="../assets/plugins/datatables-responsive/js/lodash.min.js"></script>
    <script src="../assets/js/datatables.js" type="text/javascript"></script>
</asp:Content>
