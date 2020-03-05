<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="ManageRequest.aspx.cs" Inherits="WS.Portal.ManageRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron" data-pages="parallax">
        <div class="container-fluid container-fixed-lg sm-p-l-20 sm-p-r-20">
            <div class="inner">
                
                <ul class="breadcrumb">
                    <li>
                        <p><a href="Dashboard.aspx">DashBoard</a></p>
                    </li>
                   
                    <li><a href="#" onclick="JavaScript:window.history.back(1);return false;">Tickets</a></li>
                    <li><a href="#" class="active">Ticket Details</a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="container-fluid container-fixed-lg">
        <div class="widget form-group-default">
            <div class="widget-content padding">
                <div align="center">
                    <h2>Ticket Details</h2>
                </div>
                <br />
                <div class="row">
                    <div class="form-group">
                        <div class="col-sm-12" align="Center">
                            <label runat="server" id="Label1" style="font-size: 38px;"></label>
                        </div>
                    </div>
                </div>
                <br />
                <br />
                 <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Request Creator</label>
                        <div class="col-sm-10">
                            <label runat="server" id="lbl_Creator"></label>
                        </div>
                    </div>
                </div>
                <br />
                 <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Client Name</label>
                        <div class="col-sm-10">
                            <label runat="server" id="lbl_Client"></label>
                        </div>
                    </div>
                </div>
                <br />
                 <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Email</label>
                        <div class="col-sm-10">
                            <label runat="server" id="lbl_Email"></label>
                        </div>
                    </div>
                </div>
                <br />
                 <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Received On</label>
                        <div class="col-sm-10">
                            <label runat="server" id="lbl_Received"></label>
                        </div>
                    </div>
                </div>
                <br />
                  <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Responsibility Of</label>
                        <div class="col-sm-10">
                            <label runat="server" id="lbl_Assignee"></label>
                        </div>
                    </div>
                </div>
                 <br />
                  <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Response Time</label>
                        <div class="col-sm-10">
                            <label runat="server" id="lbl_Respond"></label>
                        </div>
                    </div>
                </div>
                 <br />
                 <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Status</label>
                        <div class="col-sm-10">
                            <label runat="server" id="lbl_status"></label>
                        </div>
                    </div>
                </div>
                <br />
                   <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Department</label>
                        <div class="col-sm-10">
                            <label runat="server" id="Lbl_department"></label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Ticket Subject</label>
                        <div class="col-sm-10">
                            <label runat="server" id="lbl_Sub"></label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">User Complaint</label>
                        <div class="col-sm-10">
                            <textarea runat="server" id="txtrBody" rows="10" style="width: 100%; font-weight: bold; font-size: small" ></textarea>
                        </div>
                    </div>
                </div>

                <br />
                <div class="row" id="headreply" runat="server">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Head Of Department/Responsible Person Comments</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtrcomplaintreply" runat="server" TextMode="MultiLine" Width="100%" Rows="6"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
