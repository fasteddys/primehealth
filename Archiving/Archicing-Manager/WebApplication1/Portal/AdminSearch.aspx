<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="AdminSearch.aspx.cs" Inherits="WebApplication1.Portal.AdminSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<link href="../assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css"/>
<link href="../assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css"/>
<link href="../assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen"/>
<link href="../assets/css/jquery.dataTables.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
       <div class="widget">
            <div class="widget-content padding">
                <div align="center">
                    <hr />
                    	<div class="table-responsive" style="overflow:auto">
				     		<table id="example" data-sortable class="table table-detailed ">
		                        <thead>
                        <tr>
                            <th>ID</th>
                            <th>Subject</th>
                            <th>Creator</th>
                            <th>Received</th>
                            <th>Status</th>
                            <th>Assigned To</th>
                            <th>Accepted At</th>
                            <th>Finished At</th>
                            <th>Closed At</th>
                            <th>Requested Items</th>
                            <th>Found Items</th>
                            <td data-sortable="false">Actions</td>
                        </tr>
                    </thead>
                   <tbody>
                      <asp:ListView runat="server" ID="lstNewReq">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("id")%></td>
                                    <td><strong><%#Eval("rSubject").ToString().Substring(0,Math.Min(50,Eval("rSubject").ToString().Length))%></strong></td>
                                    <td><b><%#Eval("rFrom") %></b></td>
                                    <td><%#Eval("rDate") %></td>
                                    <td><span class="label label-info" style="background: <%#Eval("rStatusColor")%>"><%#Eval("rStatus") %> </span></td>
                                    <td><%#Eval("AssigenPerson") %></td>
                                     <td><%#Eval("AssignedTime") %></td>
                                     <td><%#Eval("FinishedArchivingDate") %></td>
                                     <td><%#Eval("ApprovedDate") %></td>
                                     <td style="text-align:center"><%#Eval("NumberOfReqClaims") %></td>
                                     <td style="text-align:center"><%#Eval("NumberOfFoundClaims") %></td>
                                    <td>
                                        <a href="ManageRequest.aspx?id=<%#Eval("id")%>" title="Edit" class="btn btn-default"><i class="fa fa-edit"></i></a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </tbody>
                </table>
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
        <script src="js/jquery.js"></script>
    <script src="js/jquery.dataTables.js"></script>
    <script>
        $(document).ready(function () {
            $('#example').DataTable();
        });
    </script>
</asp:Content>