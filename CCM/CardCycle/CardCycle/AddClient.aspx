<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Site2.Master" AutoEventWireup="true" CodeBehind="AddClient.aspx.cs" Inherits="CardCycle.AddClient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="widget">
                <div class="widget-content padding">
                    <div align="center">
                        <h2>Add New Client</h2>
                    </div>
                    <hr />
                    <div class="alert alert-success" runat="server" id="div_success">
                        Client Added Successfully
                    </div>
                    <div class="alert alert-danger" runat="server" id="div_Error">
                        Client Already Exsists
                    </div>
                    <div class="col-sm-12" align="Center">
                        <label runat="server" id="Label1" style="font-size: 38px;"></label>
                    </div>
                    <br />
                    <div class="row">
                        <div class="form-group">
                            <label for="input-text" class="col-sm-2 control-label">Client Name</label>
                            <div class="col-sm-10">
                                <input type="text" class="form-control" runat="server" id="input_text" placeholder="">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="this filed required" ControlToValidate="input_text" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="form-group">
                            <label for="input-text" class="col-sm-2 control-label">Client Notes</label>
                            <div class="col-sm-10">
                                <textarea runat="server" class="summernote-small form-control" id="txtrBody" rows="10" style="width: 100%; font-weight: bold; font-size: small"></textarea>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="this filed required" ControlToValidate="txtrBody" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="form-group">
                            <label for="input-text" class="col-sm-2 control-label">ClientStatus</label>
                            <div class="col-sm-10">
                                <label>
                                    <input type="checkbox" value="1" id="ActivateClients" runat="server">
                                    Active - Not Active </label>
                            </div>
                        </div>
                    </div>

                    <Br/>
                    <div class="row">
                        <div class="form-group">
                            <label for="input-text" class="col-sm-2 control-label">Type</label>
                            <div class="col-sm-6">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="DropDownType" CssClass="form-control dropdown" Font-Bold="True" Font-Size="Small" AppendDataBoundItems="true" runat="server">
                                            <asp:ListItem>Normal</asp:ListItem>
                                            <asp:ListItem>TPA</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-6">
                            <input type="button" id="AddClientBtn" runat="server" value="Add Client" class="btn btn-success" onserverclick="AddClient_ServerClick" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
   	
    	<!-- Page Specific JS Libraries -->
	<script src="assets/libs/bootstrap-select/bootstrap-select.min.js"></script>
	<script src="assets/libs/bootstrap-inputmask/inputmask.js"></script>
	<script src="assets/libs/summernote/summernote.js"></script>
	<script src="assets/js/pages/forms.js"></script>
</asp:Content>
