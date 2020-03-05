<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ManageSubLocation'sData.aspx.cs" Inherits="CallCenterNotesApp.Portal.ManageSubLocation_sData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
        <div class="row">
            <div class="col-md-12">
                <ul class="breadcrumb">
                    <li><a href="#"><i class="fa fa-home"></i>  Home</a></li>
                    <li><a href="#"> Manage SubLocation's Data </a></li>
                </ul>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
            <div class="alert alert-success" runat="server" id="div_Success_update">
                    SubLocation Updated Successfully
                </div>
            <section class="content">
                <div class="row">
                    <div class="col-md-12">
                        <section class="panel">
                            <header class="panel-heading">View Your Data </header>
                            <div class="panel-body">
<%--                                <form class="form-horizontal tasi-form" role="form" runat="server">--%>
                                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">SubLocation Name : </label>
                                <div class="col-sm-4">
                                    <input type="text" class="form-control input-lg m-b-10" id="SubLocationNameID" runat="server">
                                </div>
                                <label class="col-sm-2 col-sm-2 control-label">Location Name : </label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="LocationID" runat="server" class="form-control m-b-10"></asp:DropDownList>
                                </div>
                            </div>
                                            
                                    <div class="col-md-12" style="text-align: center;">
                                        <div class="toolbar-btn-action">
                                            <button type="submit" class="btn btn-primary" id="DeleteSubLocationData" onserverclick="DeleteSubLocationData_ServerClick" runat="server">Delet Category's Data </button>
                                            <button type="submit" class="btn btn-primary" id="UpdateSubLocationData" onserverclick="UpdateSubLocationData_ServerClick" runat="server">Update Category's Data</button>
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
