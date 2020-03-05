<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ManageUser.aspx.cs" Inherits="CallCenterNotesApp.Portal.ManageUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <div class="row">
            <div class="col-md-12">
                <ul class="breadcrumb">
                    <li><a href="#"><i class="fa fa-home"></i>  Home</a></li>
                    <li><a href="#"> Manage User Data</a></li>
                </ul>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
            <div class="alert alert-success" runat="server" id="div_Success_update">
                    User Updated Successfully
                </div>
            <div class="alert alert-danger" runat="server" id="div_Error_update">
                Password Not Match
            </div>
            <section class="content">
                <div class="row">
                    <div class="col-md-12">
                        <section class="panel">
                            <header class="panel-heading">View Your Data </header>
                            <div class="panel-body">
<%--                                <form class="form-horizontal tasi-form" role="form" runat="server">--%>
                                    <div class="form-group" id="UserNameDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label" style="color : blue"> User Name</label>
                                          <div class="col-lg-10" >
                                              <p id="UserName" runat="server" class="form-control-static"></p>
                                          </div>
                                      </div>
                                    
                                    <div class="form-group" id="UserPassword1Div" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label" style="color : blue">Enter Password</label>
                                          <div class="col-lg-3">
                                          <input type="password" class="form-control input-lg m-b-10" id="UserPassword1" runat="server">
                                          </div>
                                      </div>
                                    
                                    <div class="form-group" id="UserPassword2Div" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label" style="color : blue">Confirm Password</label>
                                          <div class="col-lg-3">
                                          <input type="password" class="form-control input-lg m-b-10" id="UserPassword2" runat="server">
                                          </div>
                                      </div>

                                    <div class="col-md-12" style="text-align: center;">
                                        <div class="toolbar-btn-action">
                                            <button type="submit" class="btn btn-primary" id="btn_UpdateUserData" onserverclick="UpdateUserData_ServerClick" runat="server" >Update User Data</button>
                                            <button type="submit" class="btn btn-primary" id="btn_DeleteUserData" onserverclick="DeleteUserData_ServerClick" runat="server" >Delete User</button>
<%--                                        <button type="submit" class="btn btn-primary" id="Button2" runat="server" >Submit Request</button>--%>
                                        </div>
                                    </div>
<%--                                </form>--%>
                            </div>
                        </section>
                    </div>
                </div>
            </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
</asp:Content>
