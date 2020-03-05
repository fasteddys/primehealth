<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="BatchManager.aspx.cs" Inherits="WebApplication1.Portal.BatchManager1" %>
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
                    <li><a href="#" class="active">All Batches</a>
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
                            <td>Batch</td>
                            <td>Box</td>
                            <td>Claims Number</td>
                            <td>Added</td>
                            <td>User</td>
                            <td data-sortable="false">Actions</td>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:ListView runat="server" ID="ItemsList" onitem OnItemCanceling="ItemsList_ItemCanceling" OnItemDeleting="ItemsList_ItemDeleting" OnItemEditing="ItemsList_ItemEditing" OnItemUpdating="ItemsList_ItemUpdating">
                            <ItemTemplate>
                                <tr>
                                    <td><asp:Label ID="IDTxt" runat="server" Text='<%#Eval("id")%>' /></td>
                                    <td><strong><%#Eval("batch")%></strong></td>
                                    <td><b><%#Eval("box") %></b></td>
                                    <td><b><%#Eval("NumOFClaims") %></b></td>
                                    <td><%#Eval("AddTime") %></td>
                                    <td><%#Eval("AddedBy") %></td>
                                    <td>
                                        <div class="btn-group btn-group-xs">
                                            <asp:LinkButton ID="EditBtn" runat="server" CommandName="Edit"><span class="fa fa-edit"></span></asp:LinkButton>
                                            <span><a>|  </a></span>
                                            <asp:LinkButton ID="DeleteBtn" runat="server" CommandName="Delete"><span class="fa red fa-minus-circle"></span></asp:LinkButton>
                                            <span><a>|  </a></span>
                                            <a id="ManageBtn" href="ManageBatch.aspx?id=<%#Eval("batch")%>"><span class="fa red fa-magic"></span></a>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <td><asp:Label ID="EditIDTxt" runat="server" Text='<%#Eval("id")%>' /></td>
                                <td><asp:TextBox ID="BatchNumtxt" runat="server" Text=' <%#Eval("batch")%>' /></td>
                                <td><asp:TextBox ID="BoxNumTxt" runat="server" Text=' <%#Eval("box")%>' /></td>
                                <td><b><%#Eval("NumOFClaims") %></b></td>
                                <td><%#Eval("AddTime") %></td>
                                <td><%#Eval("AddedBy") %></td>
                                <td>
                                    <div class="btn-group btn-group-xs">
                                        <asp:LinkButton ID="UpdateBtn" runat="server" CommandName="Update"><span class="fa fa-check-circle-o"></span></asp:LinkButton>
                                        <span><a>|  </a></span>
                                        <asp:LinkButton ID="CancelBtn" runat="server" CommandName="Cancel"><span class="fa red fa-remove"></span></asp:LinkButton>
                                    </div>
                                </td>
                            </EditItemTemplate>
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
