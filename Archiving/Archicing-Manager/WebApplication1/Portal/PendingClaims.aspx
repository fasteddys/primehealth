<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="PendingClaims.aspx.cs" Inherits="WebApplication1.Portal.PendingClaims" %>
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
                    <li><a href="#" class="active">Pending Claims Requests</a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="widget">
            <div class="widget-content padding">
                <div align="center">
                    <hr />
                    	<div class="table-responsive" style="overflow:auto">
				     		<table data-sortable class="table table-detailed ">
		                        <thead>
                        <tr>
                            <th>ID</th>
                            <th>Subject</th>
                            <th>User</th>
                            <th>Received</th>
                            <th>Assigned to</th>
                            <th>Status</th>
                            <th>Claims Assignee</th>
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
                                    <td><%#Eval("PendingCliamsStart") %></td>
                                    <td><%#Eval("AssigenPerson") %></td>
                                    <td><span class="label label-info" style="background: <%#Eval("rStatusColor")%>"><%#Eval("rStatus") %> </span></td>
                                    <td><%#Eval("PendClaimsassignee") %></td>
                                    <td>
                                        <a href="ManageRequest.aspx?id=<%#Eval("id")%>" title="Edit" class="btn btn-default"><i class="fa fa-edit"></i></a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                       <tr>
                                                <td>
                                                    <asp:DataPager ID="DataPager1" runat="server" class="pagination" PagedControlID="lstNewReq" QueryStringField="id" PageSize="10">
                                                        <Fields>
                                                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                                            <asp:NumericPagerField />
                                                            <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                                        </Fields>
                                                    </asp:DataPager>
                                                     </td>
                                            </tr>
                    </tbody>
                </table>
            </div>
                </div>
        </div>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="180000" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
        <script src="../assets/plugins/jquery-datatable/media/js/jquery.dataTables.min.js" type="text/javascript"></script>
<script src="../assets/plugins/jquery-datatable/extensions/TableTools/js/dataTables.tableTools.min.js" type="text/javascript"></script>
<script src="../assets/plugins/jquery-datatable/extensions/Bootstrap/jquery-datatable-bootstrap.js" type="text/javascript"></script>
<script type="text/javascript" src="../assets/plugins/datatables-responsive/js/datatables.responsive.js"></script>
<script type="text/javascript" src="../assets/plugins/datatables-responsive/js/lodash.min.js"></script>
    <script src="../assets/js/datatables.js" type="text/javascript"></script>
</asp:Content>