<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="WS.Portal.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="../assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css"/>
<link href="../assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css"/>
<link href="../assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="jumbotron" data-pages="parallax">
        <div class="container-fluid container-fixed-lg sm-p-l-20 sm-p-r-20">
            <div class="inner">
                <ul class="breadcrumb">
                    <li>
                        <p>DashBoard</p>
                    </li>
                    <li><a href="#" class="active">My Tickets</a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
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
                           <td>Ticket No.</td>
                            <td>Subject</td>
                            <td>User</td>
                            <td>Client</td>
                            <td>Complaint About</td>
                            <td>Receiving Time</td>
                            <td>Status</td>
                        </tr>
                    </thead>
                   <tbody>
                      <asp:ListView runat="server" ID="lstNewReq">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("id")%></td>
                                    <td><a href="ManageRequest.aspx?id=<%#Eval("id")%>" ><strong><%#Eval("rSubject").ToString().Substring(0,Math.Min(50,Eval("rSubject").ToString().Length))%></strong></a></td>
                                    <td><b><%#Eval("rFrom") %></b></td>
                                    <td><b><%#Eval("ClientName") %></b></td>
                                    <td><b><%#Eval("Department") %></b></td>
                                    <td><%#Eval("rDate") %></td>
                                    <td><span class="label label-info" style="background: <%#Eval("rStatusColor")%>"><%#Eval("rStatus") %> </span></td>
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
     <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="60000" />
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