<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="WebApplication1.Portal.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="../assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css"/>
<link href="../assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css"/>
<link href="../assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error1">
                    Sorry no data belongs to this date
                </div>
    <asp:GridView ID="GridView2" runat="server"></asp:GridView>
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
                <div align="center"><h2>DataEntry Reports</h2> </div>
                <div class="pull-right">
                    <div class="col-xs-12">
                        <input type="text" id="search-table" class="form-control pull-right" placeholder="Search">
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="table-responsive" style="overflow:auto">
            <div class="panel-body">                                
                
                <asp:Label ID="Label5" runat="server" Text="From Date" Font-Bold="true"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><asp:ImageButton ID="ImageButton1" runat="server" Height="23px" ImageUrl="~/assets/img/Calendar-icon.png" OnClick="ImageButton1_Click" Width="50px" />
                <asp:Label ID="Label6" runat="server" Text="To Date" Font-Bold="true"></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><asp:ImageButton ID="ImageButton2" runat="server" Height="23px" ImageUrl="~/assets/img/Calendar-icon.png" OnClick="ImageButton2_Click" Width="50px" />
                <asp:Label ID="Label7" runat="server" Text="Employee" Font-Bold="true"></asp:Label>
                <asp:DropDownList ID="Emp" runat="server" CssClass="fa-align-center" Font-Bold="True" Width="150px" Font-Size="Medium" DataSourceID="EmpDataSource" DataTextField="UserName" DataValueField="UserName"></asp:DropDownList>
                <asp:SqlDataSource runat="server" ID="EmpDataSource" ConnectionString='<%$ ConnectionStrings:ArchivingConnectionString1 %>' SelectCommand="SELECT DISTINCT [UserName] FROM [userTB] WHERE ([Type] = @Type)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="DataEntry" Name="Type" Type="String"></asp:Parameter>
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />  
                <asp:Calendar ID="Calendar1" runat="server" style =" margin-top : 0px" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
                <asp:Calendar ID="Calendar2" runat="server" style =" margin-top : 0px" OnSelectionChanged="Calendar2_SelectionChanged"></asp:Calendar>
              <br />
                <div class="form-group" runat="server" id="Number_of_tickets_DEntry">
                        <label for="input-text" class="col-sm-2 control-label" style="font-size:15PX;align-content:center; top: 0px; left: 0px; width: 263px;" >Number Of Tickets</label>
                        <label runat="server" id="lbl_Tottal_tickets" style="font-size:15PX;align-content:center; top: 0px; left: 0px; width: 263px;"></label>
                 </div>
                <div class="form-group" runat="server" id="Number_of_Claims_DEntry">    
                            <label for="input-text" class="col-sm-2 control-label" style="font-size:12PX;align-content:center; top: 0px; left: 0px; width: 263px;color:red" >Number Of Found Claims</label>
                            <label runat="server" id="lbl_count_Found_Claims" style="font-size:15PX;align-content:center; top: 0px; left: 0px; width: 263px;"></label>
                  </div>
                <div class="form-group" runat="server" id="Return_tickets_Qc">    
                            <label for="input-text" class="col-sm-2 control-label" style="font-size:12PX;align-content:center; top: 0px; left: 0px; width: 263px;color:red" >Number Of Qc Return tickets</label>
                            <label runat="server" id="lbl_ReturnQc" style="font-size:15PX;align-content:center; top: 0px; left: 0px; width: 263px;"></label>
                  </div>
                <div class="form-group" runat="server" id="Return_tickets_TPA">    
                            <label for="input-text" class="col-sm-2 control-label" style="font-size:12PX;align-content:center; top: 0px; left: 0px; width: 263px;color:red" >Number Of TPA Return tickets</label>
                            <label runat="server" id="lbl_ReturnTPA" style="font-size:15PX;align-content:center; top: 0px; left: 0px; width: 263px;"></label>
                  </div>
                <br />
            <div class="col-sm-10" align="Center">
                <a class="btn btn-success" runat="server" onserverclick="btn_Search_ServerClick" id="btn_search" ><i class="fa fa-check"></i> Get Provider Details</a>
                <a class="btn btn-success" runat="server" onserverclick="btn_SearchQc_ServerClick" id="btn_search_Qc" ><i class="fa fa-check"></i> Get Back_QC Details</a>
                <a class="btn btn-success" runat="server" onserverclick="btn_SearchTPA_ServerClick" id="btn_search_Tpa" ><i class="fa fa-check"></i> Get Back_TPA Details</a>
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
