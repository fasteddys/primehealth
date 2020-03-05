<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="ManageAccounts.aspx.cs" Inherits="WebApplication1.Portal.ManageAccounts" %>
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
                    <li><a href="#" class="active">All Members</a>
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
                        <tr align="center">
                            <td>ID</td>
                            <td>Name</td>
                            <td>Password</td>
                            <td>Type</td>
                            <td data-sortable="false">Actions</td>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:ListView runat="server" ID="ItemsList" OnItemCanceling="ItemsList_ItemCanceling" OnItemDeleting="ItemsList_ItemDeleting" OnItemEditing="ItemsList_ItemEditing" OnItemUpdating="ItemsList_ItemUpdating">
                            <ItemTemplate>
                                <tr align="center">
                                    <td><asp:Label ID="EmpIDLabel" runat="server" Text= '<%#Eval("id")%>'></asp:Label></td>
                                    <td><strong><%#Eval("UserName")%></strong></td>
                                    <td><b>*********</b></td>
                                    <td><%#Eval("Type") %></td>
                                    <td>
                                        <div class="btn-group btn-group-xs">
                                            <asp:LinkButton ID="Edit" runat="server" CommandName="Edit"><span class="fa fa-edit"></span></asp:LinkButton>
                                            <span><a>  |  </a></span>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Delete"><span class="fa red fa-minus-circle"></span></asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <tr align="center">
                                    <td>
                                        <asp:Label ID="EmpIDLabel1" runat="server" Text='<%# Eval("id") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="EmpNameTextBox" runat="server" Text='<%# Bind("UserName") %>' />
                                    </td>

                                    <td>
                                        <asp:TextBox ID="EmpPassTextBox" runat="server" Text='<%# Bind("Password") %>' />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList2" runat="server">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem>Admin</asp:ListItem>
                                            <asp:ListItem>Archiving</asp:ListItem>
                                            <asp:ListItem>ArcAdmin</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update"><span class="fa fa-check-circle"></span></asp:LinkButton>
                                        <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel"><span class="fa fa-remove"></span></asp:LinkButton>
                                    </td>
                                </tr>
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
