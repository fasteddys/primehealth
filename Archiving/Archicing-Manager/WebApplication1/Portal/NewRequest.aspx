<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="WebApplication1.Users.Portal" %>
<%@ MasterType VirtualPath="~/MasterPages/Main.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron" data-pages="parallax">
        <div class="container-fluid container-fixed-lg sm-p-l-20 sm-p-r-20">
            <div class="inner">

                <ul class="breadcrumb">
                    <li>
                        <p>DashBoard</p>
                    </li>
                    <li><a href="#" class="active">New Request</a>
                    </li>
                </ul>
                <div class="alert alert-success alert-dismissable" runat="server" id="div1">
                 <h5>  Hint: You Can Forward Requests If Rejected Due To Wrong Number Of Requested Items/Subject..Review Details then Click On Update Request.</h5> 
                </div>
                <div class="alert alert-success alert-dismissable" runat="server" id="div_success">
                  <strong>Request Added Successfully</strong> 
                </div>
                <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
                  <strong> Error Operation...(Please Note That (Request Header) AND (Number Of Requested Claims/Batches/Invoices) are Mandatory). </strong> 
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
                        <h5>Request Details
                        </h5>
                        <form class="" role="form">
                            <div class="form-group form-group-default required ">
                                <label>Header</label>
                                <input type="text" runat="server" id="input_text" class="form-control" required="required">
                            </div>
                            <div class="row">
                                <div class="container-fluid container-fixed-lg">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <div class="panel-title">
                                                Subject
                                            </div>
                                        </div>
                                        <div class="panel-body no-scroll ">
                                            <div class="wysiwyg5-wrapper b-a b-grey">
                                                <textarea id="txtrBody" runat="server" class="wysiwyg demo-form-wysiwyg" placeholder="Enter text ..."></textarea>
                                            </div>
                                            <br>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Number Of Requested Claims/Batches/Invoices</label>
                                    <input type="text" runat="server" id="NumberOfClaims" required="required">
                                </div>
                                <br />
                                </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Attached File</label>
                                    <div class="col-sm-10">
                                        <asp:FileUpload ID="FileUpload1" runat="server" class="btn btn-success btn-rounded"  />
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
    <script type="text/javascript">
        $(function () {
            $('#<%=FileUpload1.ClientID %>').change(
            function () {
                var fileExtension = ['xlsx', 'xls', 'csv', 'rar', 'zip', 'pdf'];
                if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {

                    $('#<%=SubmitBtn.ClientID %>').attr("disabled", true);
                    $('#<%= myLabel.ClientID %>').html("Only excel , PDF and compressed files are allowed.");
                }
                else {
                    $('#<%=SubmitBtn.ClientID %>').attr("disabled", false);
                    $('#<%= myLabel.ClientID %>').html("");
                }
            })
        })
</script>
</asp:Content>
