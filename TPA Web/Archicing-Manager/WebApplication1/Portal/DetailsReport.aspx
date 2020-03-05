<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="DetailsReport.aspx.cs" Inherits="WebApplication1.Portal.DetailsReport" %>
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
                <asp:Label ID="Label4" runat="server" Text="Client Name" Font-Bold="true"></asp:Label>
                <asp:DropDownList ID="ClientName" runat="server" CssClass="fa-align-center" Font-Bold="True" Font-Size="Medium" DataSourceID="ClientSearch" DataTextField="ClientName" DataValueField="ClientName"></asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="ClientSearch" ConnectionString='<%$ ConnectionStrings:ArchivingConnectionString1 %>' SelectCommand="SELECT DISTINCT [ClientName] FROM [Provider]"></asp:SqlDataSource>
                
                <asp:Label ID="Label2" runat="server" Text="Month" Font-Bold="true"></asp:Label>
                <asp:DropDownList ID="MonthNum" runat="server" CssClass="fa-align-center" Font-Bold="True" Width="150px" Font-Size="Medium" DataSourceID="MonthNumber" DataTextField="PMonth" DataValueField="PMonth"></asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="MonthNumber" ConnectionString='<%$ ConnectionStrings:ArchivingConnectionString1 %>' SelectCommand="SELECT DISTINCT [PMonth] FROM [Provider]"></asp:SqlDataSource>
                
                <asp:Label ID="Label3" runat="server" Text="Year" Font-Bold="true"></asp:Label>
                <asp:DropDownList ID="YearNum" runat="server" CssClass="fa-align-center" Font-Bold="True" Width="150px" Font-Size="Medium" DataSourceID="YearNumber" DataTextField="PYear" DataValueField="PYear"></asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="YearNumber" ConnectionString='<%$ ConnectionStrings:ArchivingConnectionString1 %>' SelectCommand="SELECT DISTINCT [PYear] FROM [Provider] ORDER BY [PYear]"></asp:SqlDataSource>
              <br />
              <br />
            <div class="col-sm-10" align="Center">
                                
                <form runat="server" id="s">
                <asp:GridView runat="server" ID="GridView1"> </asp:GridView>
                     </div>
                <\form>


                <a class="btn btn-success" runat="server" onserverclick="btn_ClientReport_QC_ServerClick" id="btn_ClientReport_QC" ><i class="fa fa-check"></i> Get ClientReport QC</a>
                <a class="btn btn-success" runat="server" onserverclick="btn_ClientReport_TPA_ServerClick" id="btn_ClientReport_TBA" ><i class="fa fa-check"></i> Get ClientReport TBA</a>
                <a class="btn btn-success" runat="server" onserverclick="btn_ClientReport_Report_ServerClick" id="btn_ClientReport_Report" ><i class="fa fa-check"></i> Generate Detailed Report </a>
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
                            <td>Total Claims</td>
                            <td>found Claims</td>
                            <td>missing Claims</td>
                            <td>Dublicated Claims</td>
                            <td>InPatient Claims</td>
                            <td>Wrong Claims</td>
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
                                    <td><%#Eval("TottalClaims") %></td>
                                    <td><%#Eval("TottalFoundClaims") %></td>
                                    <td><%#Eval("TottalMissClaims") %></td>
                                    <td><%#Eval("DublicatedClaims") %></td>
                                    <td><%#Eval("InPatientClaims") %></td>
                                    <td><%#Eval("WrongClaims") %></td>
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
