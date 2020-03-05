<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="TPAExportExcelReport.aspx.cs" Inherits="WebApplication1.Portal.TPAExportExcelReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="../assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css"/>
<link href="../assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css"/>
<link href="../assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen"/>


    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:GridView ID="GridView2" runat="server"></asp:GridView>
     <div class="jumbotron" data-pages="parallax">
        <div class="container-fluid container-fixed-lg sm-p-l-20 sm-p-r-20">
            <div class="inner">
       <a href="#"  runat="server" onserverclick="Refresh"></a>
                <ul class="breadcrumb">
                    <li>
                        <p>DashBoard</p>
                    </li>
                    <li><a href="#" class="active">TPA Search Results</a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="container-fluid container-fixed-lg bg-white">
        <div class="panel panel-transparent">
            <div class="panel-heading">
                <div align="center" ><h2 style="font-size: x-large; font-weight: bold">TPA Search Reports</h2> </div>
            </div>
            <div class="table-responsive" style="overflow:auto">
            <div class="panel-body">                                
                
                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label" style="font-size:medium; font-weight: bold">Select Date </label>
                        <div class="col-sm-6">
                            <div class="row">
                                <div class="col-sm-5">
                                        <asp:TextBox ID="txtSearch" placeholder="Search Providers..." CssClass="form-control text-box text-darkblue-3" runat="server" Width="309px"></asp:TextBox>
                                    </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />


                <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" ShowGridLines="True" Width="492px">
                    <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px"></DayHeaderStyle>

                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC"></NextPrevStyle>

                    <OtherMonthDayStyle ForeColor="#CC9966"></OtherMonthDayStyle>

                    <SelectedDayStyle BackColor="Blue" Font-Bold="True"></SelectedDayStyle>

                    <SelectorStyle BackColor="#FFCC66"></SelectorStyle>

                    <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC"></TitleStyle>

                    <TodayDayStyle BackColor="#FFCC66" ForeColor="White"></TodayDayStyle>
                </asp:Calendar>

                <br />
            <div class="col-sm-10" align="Center">
                <a class="btn btn-success" runat="server" onserverclick="btn_Search_ServerClick" id="btn_search" ><i class="fa fa-check"></i> Get Provider Details</a>
<%--                <a class="btn btn-success" runat="server" onserverclick="btn_SearchQc_ServerClick" id="btn_search_Qc" ><i class="fa fa-check"></i> Get Back_QC Details</a>--%>
<%--                <a class="btn btn-success" runat="server" onserverclick="btn_SearchTPA_ServerClick" id="btn_search_Tpa" ><i class="fa fa-check"></i> Get Back_TPA Details</a>--%>
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
                                    <td>
                                        <div class="btn-group btn-group-xs">
                                             <a href="ManageRequest.aspx?id=<%#Eval("id")%>" title="Edit" class="btn btn-default"><i class="fa fa-edit"></i></a>
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
