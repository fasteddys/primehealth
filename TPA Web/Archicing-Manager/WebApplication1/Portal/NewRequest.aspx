 <%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="WebApplication1.Users.Portal" %>
<%@ MasterType VirtualPath="~/MasterPages/Main.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="../assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css"/>
<link href="../assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css"/>
<link href="../assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen"/>
   <script type = "text/javascript">
       var ddlText, ddlValue, ddl, lblMesg;
       function CacheItems() {
           ddlText = new Array();
           ddlValue = new Array();
           ddl = document.getElementById("<%=DropDownList1.ClientID %>");
        for (var i = 0; i < ddl.options.length; i++) {
            ddlText[ddlText.length] = ddl.options[i].text;
            ddlValue[ddlValue.length] = ddl.options[i].value;
        }
    }
    window.onload = CacheItems;

    function FilterItems(value) {
        ddl.options.length = 0;
        for (var i = 0; i < ddlText.length; i++) {
            if (ddlText[i].toLowerCase().indexOf(value) != -1) {
                AddItem(ddlText[i], ddlValue[i]);
            }
        }
        lblMesg.innerHTML = ddl.options.length + " items found.";
        if (ddl.options.length == 0) {
            AddItem("No items found.", "");
        }
    }

    function AddItem(text, value) {
        var opt = document.createElement("option");
        opt.text = text;
        opt.value = value;
        ddl.options.add(opt);
    }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron" data-pages="parallax">
        <div class="container-fluid container-fixed-lg sm-p-l-20 sm-p-r-20">
            <div class="inner">

                <ul class="breadcrumb">
                    <li>
                        <p>yDashBoard</p>
                    </li>
                    <li><a href="#" class="active">New Request</a>
                    </li>
                </ul>
                <div class="alert alert-success alert-dismissable" runat="server" id="div_success">
                    Request Added Successfully
                </div>
                <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
                    Please Insert the Header of Request 
                </div>
                <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error1">
                    Sorry this file not fount please insert correct data
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
                            Add New Request
                        </div>
                    </div>
                    <div class="panel-body">
                        <h5 style="text-align: center;">Request Details 
                        </h5>
                        <form class="" role="form">

                            <br />
                            <%--\\\\\\\\\\\\\\\\\\\\dddddddddddd\\\\\\\\\\\\\\\\--%>
                            <div class="row">
                                <div class="form-group">
                                    <label for="input-text" class="col-sm-2 control-label">Select Client Type</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ClientTypeDropDownList" runat="server" CssClass="fa-align-center" Font-Bold="True" Font-Size="Medium" AutoPostBack="True">
                                            <asp:ListItem>Inpatient</asp:ListItem>
                                            <asp:ListItem>Outpatient</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                      <br />
                            <div class="row">
                        <div class="form-group">
                            <label for="input-text" class="col-sm-2 control-label" > Select Client Name </label>
                            <div class="col-sm-6">                                
                                <asp:DropDownList ID="DropDownList5" runat="server" DataSourceID="SqlDataSource4" CssClass=" fa-align-center" Font-Bold="True" Font-Size="Medium" DataTextField="ClientName" DataValueField="ClientName" AutoPostBack="True"></asp:DropDownList>
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
                                <asp:DropDownList ID="DropDownList4" runat="server" CssClass="fa-align-center" Font-Bold="True" Font-Size="Medium" AutoPostBack="True">
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
                            <label for="input-text" class="col-sm-2 control-label" > Select Year </label>
                            <div class="col-sm-6">                                
                                <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="MonthDataSource" CssClass=" fa-align-center" Font-Bold="True" Font-Size="Medium" DataTextField="PYear" DataValueField="PYear" AutoPostBack="True"></asp:DropDownList>
                                <asp:SqlDataSource runat="server" ID="MonthDataSource" ConnectionString='<%$ ConnectionStrings:ArchivingConnectionString1 %>' SelectCommand="SELECT DISTINCT [PYear] FROM [Provider] ORDER BY [PYear]"></asp:SqlDataSource>
                                <SelectParameters>
                                        <asp:Parameter DefaultValue="Archiving" Name="Type" Type="String"></asp:Parameter>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                       <br />
                   <%--\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\--%>
                            <div class="row">
                        <div class="form-group">
                            <label for="input-text" class="col-sm-2 control-label" > Select Provider Name </label>
                            <div class="col-sm-6"> 
                                <div class="row">
                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtSearch" placeholder="Search Providers..." CssClass="form-control text-box text-darkblue-3" runat="server"
                                            onkeyup="FilterItems(this.value)" Width="309px"></asp:TextBox>
                                    </div>
                                </div>
                                <br />                               
                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" DataSourceID="SqlDataSourceProvider" CssClass=" fa-align-center" Font-Bold="True" Font-Size="Medium" DataTextField="PName" DataValueField="PName" Height="40px" Width="383px"></asp:DropDownList>
                                <asp:SqlDataSource runat="server" ID="SqlDataSourceProvider" ConnectionString='<%$ ConnectionStrings:TPASysConnectionString %>' SelectCommand="SELECT DISTINCT [PName] FROM [Provider] WHERE (([PType] = @PType) AND ([ClientName] = @ClientName) AND ([PMonth] = @PMonth) AND ([PYear] = @PYear) AND ([Assigned] IS NULL))">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ClientTypeDropDownList" PropertyName="SelectedValue" Name="PType" Type="String"></asp:ControlParameter>
                                        <asp:ControlParameter ControlID="DropDownList5" PropertyName="SelectedValue" Name="ClientName" Type="String"></asp:ControlParameter>
                                        <asp:ControlParameter ControlID="DropDownList4" PropertyName="SelectedValue" Name="PMonth" Type="String"></asp:ControlParameter>
                                        <asp:ControlParameter ControlID="DropDownList3" PropertyName="SelectedValue" Name="PYear" Type="String"></asp:ControlParameter>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                    </div>
                <br />                               
                            <%--\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\--%>

                            <%--\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\--%>
                                
                   <%--\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\--%>

                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label"> </label>
                                    <div class="col-sm-10">
                                        <asp:FileUpload ID="FileUpload1" runat="server" class="btn btn-success btn-rounded" />
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <div class="row" align="center">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="myLabel" Font-Size="Large" CssClass="text-center text-danger" />
                                </div>
                            </div>
                            <div align="center" class="row">
                                <button id="SubmitBtn" runat="server" onserverclick="SubmitBtn_ServerClick" class="btn btn-primary btn-cons m-b-10" type="button">
                                    <i class="pg-form"></i><span class="bold">Submit</span>
                                </button>
                            </div>
                            <%--///////////////////////////////////////////////////////////////////////////////--%>
           <div class="container-fluid container-fixed-lg bg-white">
        <div class="panel panel-transparent">
            <div class="table-responsive" style="overflow:auto">
            <div class="panel-body">
                <table class="table table-hover demo-table-search" id="tableWithSearch" >
                    <thead>
                        <tr>
                            <td>Provider ID</td>
                            <td>Provider Type</td>
                            <td>Client Name</td>
                            <td>Month</td>
                            <td>Year</td>
                            <td>Provider Name</td>
                            <td>Policy Number</td>
                            <td>Total Claims</td>
                            
                            <td data-sortable="false">Actions</td>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:ListView runat="server" ID="ItemsList" OnItemDeleting="ItemsList_ProviderDeleting" DataSourceID="Group">
                            <ItemTemplate>
                                <tr>

                                    <td>
                                        <asp:Label ID="IDTxt" runat="server" Text='<%#Eval("PId")%>' /></td>
                                    <td><%#Eval("PType") %></td>
                                    <td><%#Eval("ClientName") %></td>
                                    <td><b><%#Eval("PMonth") %></b></td>
                                    <td><%#Eval("PYear") %></td>
                                    <td><b><%#Eval("PName") %></b></td>
                                    <td><b><%#Eval("PolicyNumber") %></b></td>
                                    <td><%#Eval("TottalClaims") %></td>

                                    <td>
                                        <asp:LinkButton ID="DeleteBtn" runat="server" CommandName="Delete"><span class="fa red fa-minus-circle"></span></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>

                        <asp:SqlDataSource runat="server" ID="Group" ConnectionString='<%$ ConnectionStrings:TPASysConnectionString %>' SelectCommand="SELECT [PId], [PType], [ClientName], [PMonth], [PYear], [PName], [TottalClaims], [PolicyNumber] FROM [Provider] WHERE (([PType] = @PType) AND ([ClientName] = @ClientName) AND ([PName] = @PName) AND ([PMonth] = @PMonth) AND ([PYear] = @PYear) AND ([Assigned] IS NULL))">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ClientTypeDropDownList" PropertyName="SelectedValue" Name="PType" Type="String"></asp:ControlParameter>
                                <asp:ControlParameter ControlID="DropDownList5" PropertyName="SelectedValue" Name="ClientName" Type="String"></asp:ControlParameter>
                                <asp:ControlParameter ControlID="DropDownList1" PropertyName="SelectedValue" Name="PName" Type="String"></asp:ControlParameter>
                                <asp:ControlParameter ControlID="DropDownList4" PropertyName="SelectedValue" Name="PMonth" Type="String"></asp:ControlParameter>
                                <asp:ControlParameter ControlID="DropDownList3" PropertyName="SelectedValue" Name="PYear" Type="String"></asp:ControlParameter>
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource runat="server" ID="Repeat"></asp:SqlDataSource>
                        <asp:SqlDataSource runat="server" ID="Repeatation" ConnectionString='<%$ ConnectionStrings:ArchivingConnectionString1 %>' SelectCommand="SELECT [PId], [PType], [ClientName], [PMonth], [PYear], [PName], [TottalClaims], [PolicyNumber] FROM [Provider] WHERE ([PName] = @PName)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownList1" PropertyName="SelectedValue" Name="PName" Type="String"></asp:ControlParameter>
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </tbody>
                </table>
            </div>
                </div>
        </div>
    </div>



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
<script type="text/javascript">
    $(function () {
        $('#<%=FileUpload1.ClientID %>').change(
            function () {
                var fileExtension = ['xlsx', 'xls', 'csv', 'rar', 'zip'];
                if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {

                    $('#<%=SubmitBtn.ClientID %>').attr("disabled", true);
                    $('#<%= myLabel.ClientID %>').html("Only excel and compressed files are allowed.");
                }
                else {
                    $('#<%=SubmitBtn.ClientID %>').attr("disabled", false);
                    $('#<%= myLabel.ClientID %>').html("");
                } 
            })
    })  
</script>
</asp:Content>
