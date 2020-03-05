<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="AddCompanyNote.aspx.cs" Inherits="CallCenterNotesApp.Portal.AddCompanyNote" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <!--breadcrumbs start -->
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="fa fa-home"></i> Home</a></li>
                        <li><a href="#">Add Client Notes</a></li>
                    </ul>
                    <!--breadcrumbs end -->
                </div>
            </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
                        <!-- Main content -->
                            <!--row1-->
                            <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
                                Please Insert the Correct Full Data of Client Notes 
                            </div>
                            <div class="alert alert-success alert-dismissable" runat="server" id="div_Success">
                                Your Notes Inserted Correctly
                            </div>
                    <section class="content">
                        <div class="row">
                            <div class="col-md-12">
                                <section class="panel">
                                    <header class="panel-heading"> Select Your Data </header>
                                    <div class="panel-body">
<%--                                        <form class="form-horizontal tasi-form" role="form" runat="server">--%>
                                            
                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Action Date</label>
                                                <div class="col-sm-3">
                                                    <input type="text" class="form-control input-lg m-b-10" id="RequestDate" runat="server" disabled>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Client Name</label>
                                                <div class="col-sm-3">
                                                    <input type="text" class="form-control input-lg m-b-10" id="ClientName" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group" >
                                                <label class="col-sm-2 col-sm-2 control-label">Client Type</label>
                                                <div class="col-sm-3">

                                                    <asp:DropDownList ID="ClientTypeDropdownListnew" runat="server" class="form-control m-b-10" AutoPostBack="True" >
                                                        <asp:ListItem Value="NoData">Select Notes Type</asp:ListItem>
                                                        <asp:ListItem Value="Client">Client Notes</asp:ListItem>
                                                        <asp:ListItem Value="Provider">Provider Notes</asp:ListItem>
                                                        <asp:ListItem Value="Instructions">Instructions Notes</asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Notes</label>
                                                <div class="col-sm-10" >
                                                    <textarea runat="server" id="Notes" rows="8" style="width: 100%; font-weight: bold; font-size: large"></textarea>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Attachments</label>
                                                <div class="col-sm-3">
                                                    <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" class="btn btn-default" />
                                                </div>
                                            </div>

                                            <div class="col-md-12" style="text-align:center;">
                                                <div class="toolbar-btn-action">
                                                    <button type="submit" class="btn btn-primary" id="btn_Submit" runat="server" onserverclick="ApvSubmitNewClientNotes" >Submit Client Notes</button>
                                                </div>
                                            </div>
<%--                                        </form>--%>
                                    </div>
                                </section>
                            </div>
                        </div>
                    </section>
            <!-- /.content -->
        </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
</asp:Content>
