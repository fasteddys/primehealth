<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true"  enableEventValidation="false" CodeBehind="ArchivingPanel.aspx.cs" Inherits="WebApplication1.Portal.ArchivingPanel" %>
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
                    <li><a href="#" class="active">Archiving Panel</a>
                    </li>
                </ul>
                <div class="alert alert-success alert-dismissable" runat="server" id="div_success">
                    Request Added Successfully
                </div>
                <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error1">
                    please insert Number Of Providers
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid container-fixed-lg">
        <div class="row">
            <div class="col-md-12">

                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="panel-title">
                            Add New Client List
                        </div>
                    </div>
                    <div class="panel-body">
                        <h5 style="text-align: center;">Client Details 
                        </h5>
                        <form class="" role="form">
                            <br />
                            <%--\\\\\\\\\\\\\\\\\\\\dddddddddddd\\\\\\\\\\\\\\\\--%>
                            <div class="row">
                        <div class="form-group">
                            <label for="input-text" class="col-sm-2 control-label" > Select Client Name </label>
                            <div class="col-sm-6">                                
                                <asp:DropDownList ID="ClientDropDown" runat="server" AutoPostBack = "true" DataSourceID="SqlDataSource4" CssClass=" fa-align-center" Font-Bold="True" Font-Size="Medium" DataTextField="ClientName" DataValueField="ClientName"></asp:DropDownList>
                                <asp:SqlDataSource runat="server" ID="SqlDataSource4" ConnectionString='<%$ ConnectionStrings:ArchivingConnectionString1 %>' SelectCommand="SELECT [ClientName] FROM [Client]"></asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                         <br />
                            <%--\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\--%>

                             <%--\\\\\\\\\\\\\\\\\\rep\\\\\\\\\\\\\\\\\\\\\\\\\\\\--%>
                    <div class="row">
                        <div class="form-group">
                            <label for="input-text" class="col-sm-2 control-label">Select Month</label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="MonthDropDown" runat="server"  AutoPostBack = "true" CssClass="fa-align-center" Font-Bold="True" Font-Size="Medium">
                                    <asp:ListItem>01</asp:ListItem>
                                    <asp:ListItem>02</asp:ListItem>
                                    <asp:ListItem>03</asp:ListItem>
                                    <asp:ListItem>04</asp:ListItem>
                                    <asp:ListItem>05</asp:ListItem>
                                    <asp:ListItem>06</asp:ListItem>
                                    <asp:ListItem>07</asp:ListItem>
                                    <asp:ListItem>08</asp:ListItem>
                                    <asp:ListItem>09</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                       <br />
                     <div class="row">
                        <div class="form-group">
                            <label for="input-text" class="col-sm-2 control-label">Select Year</label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="YearDropDown" runat="server"  AutoPostBack = "true" CssClass="fa-align-center" Font-Bold="True" Font-Size="Medium">
                                    <asp:ListItem>2012</asp:ListItem>
                                    <asp:ListItem>2013</asp:ListItem>
                                    <asp:ListItem>2014</asp:ListItem>
                                    <asp:ListItem>2015</asp:ListItem>
                                    <asp:ListItem>2016</asp:ListItem>
                                    <asp:ListItem>2017</asp:ListItem>
                                    <asp:ListItem>2018</asp:ListItem>
                                    <asp:ListItem>2019</asp:ListItem>
                                    <asp:ListItem>2020</asp:ListItem>
                                    <asp:ListItem>2021</asp:ListItem>
                                    <asp:ListItem>2022</asp:ListItem>
                                    <asp:ListItem>2023</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                            <br />
                            <div class="row">
                        <div class="form-group">
                            <label for="input-text" class="col-sm-2 control-label">Select Type</label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="TypeDropDown" runat="server"  AutoPostBack = "true" CssClass="fa-align-center" Font-Bold="True" Font-Size="Medium">
                                    <asp:ListItem>InPatient</asp:ListItem>
                                    <asp:ListItem>OutPatient</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                       <br />
                            <%--\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\--%>
                            <div class="row" >
                    <div class="form-group" runat="server" id="NumProviders">
                        <label for="input-text" class="col-sm-2 control-label"  >Number Of Providers</label>
                        <div class="col-sm-10">

                            <asp:TextBox  runat="server" id="TottalProviders" rows="1" style="font-weight: bold; font-size: small" Width="176px"  ></asp:TextBox>
                        </div>
                    </div>
                </div>
            <br />
                            <div class="row" >
                    <div class="form-group" runat="server" id="NumExcel">
                        <label for="input-text" class="col-sm-2 control-label" > Number Of Excel</label>
                        <div class="col-sm-10">

                            <asp:TextBox  runat="server" id="TottalExcel" rows="1" style="font-weight: bold; font-size: small" Width="176px"  ></asp:TextBox>
                        </div>
                    </div>
                </div>
                            <br />
                <div class="row" >
                    <div class="form-group" runat="server" id="NumOfClaims">
                        <label for="input-text" class="col-sm-2 control-label" > Number Of Claims</label>
                        <div class="col-sm-10">

                            <asp:TextBox  runat="server" id="TottalClaims" rows="1" style="font-weight: bold; font-size: small" Width="176px"  ></asp:TextBox>
                        </div>
                    </div>
                </div>
                            <br />
                <div class="row">
                    <div class="form-group" runat="server" id="TottalBlock">
							<label for="input-text" class="col-sm-2 control-label">Tottal Comments</label>
                     <div class="col-sm-10">
                         <asp:TextBox ID="TottalComments" runat="server" Width="100%" TextMode="MultiLine"  Rows="10"></asp:TextBox>
							</div>
						  </div>
                    </div>
                            <br />
                            <%--\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\--%>
                 <div class="row">
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Attached File</label>
                            <div class="col-sm-10">
                                <%-- <input type="file" class="btn btn-default" title="Select file">--%>
                                <asp:FileUpload ID="FileUpload1" runat="server" class="btn btn-default" />
                                <br />
                                <asp:RegularExpressionValidator ID="rexp" runat="server" ControlToValidate="FileUpload1"
                                    ErrorMessage="Only rar files "
                                    ValidationExpression="(.*\.([Rr][Aa][Rr])$)" ForeColor="Red"></asp:RegularExpressionValidator>
                                <%--<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>--%>
                            </div>
                        </div>
                    </div>           
                                
                   <%--\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\--%>

                            <div align="center" class="row">
                                <button id="SubmitBtn" runat="server" onserverclick="SubmitBtn_ServerClick" class="btn btn-primary btn-cons m-b-10" type="button">
                                    <i class="pg-form"></i><span class="bold">Submit</span>
                                </button>
                            </div>
                            <%--///////////////////////////////////////////////////////////////////////////////--%>



                            <%--///////////////////////////////////////////////////////////////////////////////--%>
                            <div id="ErrorMsg">

                            </div>
                        </form>
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
