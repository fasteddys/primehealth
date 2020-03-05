<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="WebApplication1.Portal.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<link href="../assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css"/>
<link href="../assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css"/>
<link href="../assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid container-fixed-lg bg-white" id="content" runat="server">
        <div class="panel panel-transparent">
            <div class="panel-heading">
            <div class="form-group">
                <div class="jumbotron" data-pages="parallax">
        <div class="container-fluid container-fixed-lg sm-p-l-20 sm-p-r-20">
            <div class="inner">
                <ul class="breadcrumb">
                    <li>
                        <p><a href="AllRequests.aspx">DashBoard</a></p>
                    </li>
                    <li>
                        <a href="Reports.aspx">Reports Form</a>
                    </li>
                </ul>
                  <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
                    Error Operation...Please Make Sure Of Intervals And/Or Users...
                </div>
            </div>
        </div>
    </div>
                                    <asp:GridView ID="GridView2" runat="server"></asp:GridView>
           <div class="col-sm-6"> 


             <strong>Report From:</strong>
               <br />
             <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
        <NextPrevStyle VerticalAlign="Bottom" />
        <OtherMonthDayStyle ForeColor="#808080" />
        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
        <SelectorStyle BackColor="#CCCCCC" />
        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
        <WeekendDayStyle BackColor="#FFFFCC" />
    </asp:Calendar>
           </div>       
           <div class="col-sm-6">
             <strong>To:</strong>
             <asp:Calendar ID="Calendar2" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
        <NextPrevStyle VerticalAlign="Bottom" />
        <OtherMonthDayStyle ForeColor="#808080" />
        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
        <SelectorStyle BackColor="#CCCCCC" />
        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
        <WeekendDayStyle BackColor="#FFFFCC" />
    </asp:Calendar>
           </div>
         <br />  
         <br />  
         <br />  
    <div style="position:absolute; left:34px">
                 <strong>Please Choose User:</strong>
         <asp:DropDownList runat="server" id="drp1"  > </asp:DropDownList>
    
                </div> 
                <br />
                <br />
                <br />
                <a class="btn btn-primary" runat="server" onserverclick="GetdailyReport" id="A2"><i class="fa fa-check-circle-o"></i> Show Daily Report</a>
               <a class="btn btn-primary" runat="server" onserverclick="ExportToday" id="A3"><i class="fa fa-check-circle-o"></i> Export Daily Report</a>

                           <div class="col-sm-6"> 
                            <a class="btn btn-primary" runat="server" onserverclick="GetReport" id="btn1"><i class="fa fa-check-circle-o"></i> Show Selected Interval Report</a>
                           </div>
                <br />
                <br />
                            <div class="col-sm-6"> 
                           <a class="btn btn-primary" runat="server" onserverclick="Export" id="A1"><i class="fa fa-check-circle-o"></i> Export Selected Interval Report</a>
                           </div>
                <br />
                <br />
                <div class="clearfix" style="position:absolute; left:38px">

                 <strong>Number Of Tickets: </strong>  <asp:Label ID="Label1" runat="server" Font-Bold="true"></asp:Label>
                    <br />
                    <br />
                 <strong>Number Of Requested Items: </strong>  <asp:Label ID="Label2" runat="server" Font-Bold="true"></asp:Label>
                    <br />
                    <br />
                 <strong>Number Of Found Items: </strong>  <asp:Label ID="Label3" runat="server" Font-Bold="true"></asp:Label>

               
                </div>
                <br />
            </div>
                            <div class="panel-heading">
                <div class="pull-right">
                    <div class="col-xs-12">
                        <input type="text" id="search-table" class="form-control pull-right" placeholder="Search">
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>

            <div class="table-responsive" style="overflow:scroll">
            <div class="panel-body">
                <table class="table table-hover demo-table-search"  id="tableWithSearch">
                    <thead>
                        <tr>
                            <td>Ticket ID</td>
                            <td>Subject</td>
                            <td>Creator</td>
                            <td>Type</td>
                            <td>Status</td>
                            <td>Received at</td>
                            <td>Assigned to</td>
                            <td>Accepted at</td>
                            <td>Finished at</td>
                            <td>Closed at</td>
                            <th>Requested Items</th>
                            <th>Found Items</th>
                            <td data-sortable="false">Actions</td>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:ListView runat="server" ID="ItemsList">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("id")%></td>
                                    <td><strong><%#Eval("rSubject").ToString().Substring(0,Math.Min(50,Eval("rSubject").ToString().Length))%></strong></td>
                                    <td><b><%#Eval("rFrom") %></b></td>
                                    <td><b><%#Eval("rAddedByType") %></b></td>
                                    <td><span class="label label-info" style="background: <%#Eval("rStatusColor")%>"><%#Eval("rStatus") %> </span></td>
                                    <td><%#Eval("rDate") %></td>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
        <script src="../assets/plugins/jquery-datatable/media/js/jquery.dataTables.min.js" type="text/javascript"></script>
<script src="../assets/plugins/jquery-datatable/extensions/TableTools/js/dataTables.tableTools.min.js" type="text/javascript"></script>
<script src="../assets/plugins/jquery-datatable/extensions/Bootstrap/jquery-datatable-bootstrap.js" type="text/javascript"></script>
<script type="text/javascript" src="../assets/plugins/datatables-responsive/js/datatables.responsive.js"></script>
<script type="text/javascript" src="../assets/plugins/datatables-responsive/js/lodash.min.js"></script>
    <script src="../assets/js/datatables.js" type="text/javascript"></script>
</asp:Content>