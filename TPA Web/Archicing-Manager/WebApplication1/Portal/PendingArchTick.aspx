<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" enableEventValidation="true" CodeBehind="PendingArchTick.aspx.cs" Inherits="WebApplication1.Portal.PendingArchTick" %>
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
                    <li><a href="#" class="active">Pending Arch </a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="container-fluid container-fixed-lg bg-white">
        <div class="panel panel-transparent">
            <div class="panel-heading">
                <div class="pull-right">
                    <div class="col-xs-12">
                        <input type="text" id="search-table" class="form-control pull-right" placeholder="Search">
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="table-responsive" style="overflow:auto">
            <div class="panel-body">
                <table class="table table-hover demo-table-search" id="tableWithSearch" >
                    <thead>
                        <tr>
                            <td>ID</td>
                            <td>Client Name</td>
                            <td>Creator</td>
                            <td>Creation Time</td>
                            <td>Pending</td>
                            <td>Pending Time</td>
                            <td>Month</td>
                            <td>Year</td>
                            <td>Type</td>
                            <td>Tottal Providers</td>
                            <td>Tottal Excel</td>
                            <td>Tottal Claims</td>                            
                            <td>Status</td>

                            <td data-sortable="false">Actions</td>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:ListView runat="server" ID="ItemsList">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("ArchTickID")%></td>
                                    <td><b><%#Eval("ClientName") %></b></td>
                                    <td><strong><%#Eval("Creator").ToString().Substring(0,Math.Min(50,Eval("Creator").ToString().Length))%></strong></td>                                    
                                    <td><b><%#Eval("CreationTime") %></b></td>
                                    <td><b><%#Eval("Pending") %></b></td>
                                    <td><b><%#Eval("PendingDate") %></b></td>
                                    <td><b><%#Eval("Month") %></b></td>
                                    <td><%#Eval("Year") %></td>
                                    <td><%#Eval("ServiceType") %></td>
                                    <td><%#Eval("TottalProviders") %></td>
                                    <td><%#Eval("TottalExcel") %></td>
                                    <td><%#Eval("TottalClaims") %></td>                                    
                                    <td><span class="label label-info" style="background: <%#Eval("StatusColor")%>"><%#Eval("Status") %> </span></td>
                                    
                                    <td>
                                        <div class="btn-group btn-group-xs">
                                            <a href="ManageArchReq.aspx?id=<%#Eval("ArchTickID")%>" title="Edit" class="btn btn-default"><i class="fa fa-edit"></i></a>
                                        </div>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
         <script src="../assets/plugins/jquery-datatable/media/js/jquery.dataTables.min.js" type="text/javascript"></script>
<script src="../assets/plugins/jquery-datatable/extensions/TableTools/js/dataTables.tableTools.min.js" type="text/javascript"></script>
<script src="../assets/plugins/jquery-datatable/extensions/Bootstrap/jquery-datatable-bootstrap.js" type="text/javascript"></script>
<script type="text/javascript" src="../assets/plugins/datatables-responsive/js/datatables.responsive.js"></script>
<script type="text/javascript" src="../assets/plugins/datatables-responsive/js/lodash.min.js"></script>
    <script src="../assets/js/datatables.js" type="text/javascript"></script>
</asp:Content>
