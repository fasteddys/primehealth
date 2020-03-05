<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddUser.aspx.cs" Inherits="CallCenterNotesApp.Portal.AddUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <div class="row">
                <div class="col-md-12">
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="fa fa-home"></i> Home</a></li>
                        <li><a href="#">Add New User</a></li>
                    </ul>
                </div>
            </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
                        <!-- Main content -->
                            <!--row1-->
                            <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
                                Please Insert the Correct Full Data of User Details 
                            </div>
                            <div class="alert alert-success alert-dismissable" runat="server" id="div_Success">
                                User added successfully
                            </div>
                    <section class="content">
                        <div class="row">
                            <div class="col-md-12">
                                <section class="panel">
                                    <header class="panel-heading"> Insert User Information </header>
                                    <div class="panel-body">
<%--                                        <form class="form-horizontal tasi-form" role="form" runat="server">--%>
                                            
                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">User Name</label>
                                                <div class="col-sm-3">
                                                    <input type="text" class="form-control input-lg m-b-10" id="UserName" runat="server">
                                                </div>

                                                <label class="col-sm-2 col-sm-2 control-label">User Type</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="UserTypeDropdownList" runat="server" class="form-control input-lg m-b-10">
                                                        <asp:ListItem Value="Administrator">Admin User</asp:ListItem>
                                                        <asp:ListItem Value="CallCenterUser">Call center Operator</asp:ListItem>
                                                        <asp:ListItem Value="CallCenterDoctor">Call center Doctor</asp:ListItem>
                                                        <asp:ListItem Value="CallCenterAuditDoctor">Call center Audit Doctor</asp:ListItem>
                                                        <asp:ListItem Value="CallCenterManager">Call center manager</asp:ListItem>
                                                        <asp:ListItem Value="DirectorUser">Director user</asp:ListItem>
                                                        <asp:ListItem Value="ProviderUser">Provider User</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                            </div>


                                            <div class="col-md-12" style="text-align:center;">
                                                <div class="toolbar-btn-action">
                                                    <button type="submit" class="btn btn-primary" id="btn_Submit_user_Info" runat="server" onserverclick="ApvSubmitNewClientNotes" >Submit User Info</button>
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
