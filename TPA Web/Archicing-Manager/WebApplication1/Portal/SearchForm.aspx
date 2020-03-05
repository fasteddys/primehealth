<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="SearchForm.aspx.cs" Inherits="WebApplication1.Portal.SearchForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
     <link href="../assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css"/>
<link href="../assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css"/>
<link href="../assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron" data-pages="parallax">
        <div class="container-fluid container-fixed-lg sm-p-l-20 sm-p-r-20">
            <div class="inner">
       <a href="#"  runat="server" onserverclick="Refresh"></a>
                <ul class="breadcrumb">
                    <li>
                        <p>DashBoard</p>
                    </li>
                    <li><a href="#" class="active">Search Results</a>
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

                <asp:Label ID="Label5" runat="server" Text="Type" Font-Bold="true"></asp:Label>
                <asp:DropDownList ID="ClientType" runat="server" CssClass="fa-align-center" Font-Bold="True" Font-Size="Medium" DataSourceID="TypeKind" DataTextField="PType" DataValueField="PType"></asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="TypeKind" ConnectionString='<%$ ConnectionStrings:TPASysConnectionString %>' SelectCommand="SELECT DISTINCT [PType] FROM [Provider]"></asp:SqlDataSource>
                
                <asp:Label ID="Label4" runat="server" Text="Client" Font-Bold="true"></asp:Label>
                <asp:DropDownList ID="ClientName" runat="server" CssClass="fa-align-center" Font-Bold="True" Font-Size="Medium" DataSourceID="ClientSearch" DataTextField="ClientName" DataValueField="ClientName"></asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="ClientSearch" ConnectionString='<%$ ConnectionStrings:TPASysConnectionString %>' SelectCommand="SELECT DISTINCT [ClientName] FROM [Provider] ORDER BY [ClientName] DESC"></asp:SqlDataSource>
                
                <asp:Label ID="Label1" runat="server" Text="Provider" Font-Bold="true"></asp:Label>
                <asp:TextBox ID="PName" runat="server"></asp:TextBox>
                
                <asp:Label ID="Label2" runat="server" Text="Month" Font-Bold="true"></asp:Label>
                <asp:DropDownList ID="MonthNum" runat="server" CssClass="fa-align-center" Font-Bold="True" Width="150px" Font-Size="Medium" DataSourceID="MonthDataSource" DataTextField="PMonth" DataValueField="PMonth"></asp:DropDownList>

                <asp:SqlDataSource runat="server" ID="MonthDataSource" ConnectionString='<%$ ConnectionStrings:TPASysConnectionString %>' SelectCommand="SELECT DISTINCT [PMonth] FROM [Provider] ORDER BY [PMonth]"></asp:SqlDataSource>
                <asp:Label ID="Label3" runat="server" Text="Year" Font-Bold="true"></asp:Label>
                <asp:DropDownList ID="YearNum" runat="server" CssClass="fa-align-center" Font-Bold="True" Width="150px" Font-Size="Medium" DataSourceID="YearNumber" DataTextField="PYear" DataValueField="PYear"></asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="YearNumber" ConnectionString='<%$ ConnectionStrings:ArchivingConnectionString1 %>' SelectCommand="SELECT DISTINCT [PYear] FROM [Provider] ORDER BY [PYear]"></asp:SqlDataSource>
              <br />
              <br />
            <div class="col-sm-10" align="Center">
                <a class="btn btn-success" runat="server" onserverclick="btn_Search_ServerClick" id="btn_search" ><i class="fa fa-check"></i> Get Provider Details</a>
            </div>
                <br />
                <br />
                <table class="table table-hover demo-table-search" id="tableWithSearch" >
                    <thead>
                        <tr>
                           <td>ID</td>
                            <td>Client Name</td>
                            <td>Provider Date</td>
                            <td>Provider Name</td>
                            <td>Policy Number</td>
                            <td>Status</td>
                            <td>Total Money</td>
                            <td>Total Claims</td>
                            <td>found Claims</td>
                            <td>missing Claims</td>
                            <td>Dublicated Claims</td>
                            <td>InPatient Claims</td>
                            <td>Wrong Claims</td>
                            <td>Rejected Claims</td>
                            <td>Received Claims</td>
                            <td data-sortable="false">Actions</td>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:ListView runat="server" ID="ItemsList">
                            <ItemTemplate>
                                <tr>
                                   <td><%#Eval("id")%></td>
                                    <td><b><%#Eval("ClientName") %></b></td>
                                    <td><b><%#Eval("folderpath") %></b></td>
                                    <td><strong><%#Eval("rBody").ToString().Substring(0,Math.Min(50,Eval("rBody").ToString().Length))%></strong></td>                                    
                                    <td><b><%#Eval("PolicyNum") %></b></td>
                                    <td><span class="label label-info" style="background: <%#Eval("rStatusColor")%>"><%#Eval("rStatus") %> </span></td>
                                    <td><%#Eval("TottalMoney") %></td>
                                    <td><%#Eval("TottalClaims") %></td>
                                    <td><%#Eval("TottalFoundClaims") %></td>
                                    <td><%#Eval("TottalMissClaims") %></td>
                                    <td><%#Eval("DublicatedClaims") %></td>
                                    <td><%#Eval("InPatientClaims") %></td>
                                    <td><%#Eval("WrongClaims") %></td>
                                    <td><%#Eval("RejectedClaims") %></td>
                                    <td><%#Eval("ReceiveClaims") %></td>
                                    <td>
                                        <div class="btn-group btn-group-xs">
                                            <a href="ManageRequest.aspx?id=<%#Eval("id")%>" title="Edit" class="btn btn-default"><i class="fa fa-edit"></i></a>
                                            <a href="DetailedReportSearch.aspx?id=<%#Eval("id")%>" title="Search" class="btn btn-default "><i class="fa fa-search"></i></a>
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
