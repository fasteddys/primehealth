<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ManageCategory'sData.aspx.cs" Inherits="CallCenterNotesApp.Portal.ManageCategory_sData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
        <div class="row">
            <div class="col-md-12">
                <ul class="breadcrumb">
                    <li><a href="#"><i class="fa fa-home"></i>  Home</a></li>
                    <li><a href="#"> Manage Category's Data </a></li>
                </ul>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
            <div class="alert alert-success" runat="server" id="div_Success_update">
                    Category Updated Successfully
                </div>
            <section class="content">
                <div class="row">
                    <div class="col-md-12">
                        <section class="panel">
                            <header class="panel-heading">View Your Data </header>
                            <div class="panel-body">
<%--                                <form class="form-horizontal tasi-form" role="form" runat="server">--%>
                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Category Name : </label>
                                                <div class="col-sm-4">
                                                    <input type="text" class="form-control input-lg m-b-10" id="CategoryNameID" runat="server">
                                                </div>
                                            </div>
                                            
                                    <div class="col-md-12" style="text-align: center;">
                                        <div class="toolbar-btn-action">
                                            <button type="submit" class="btn btn-primary" id="DeleteCategoryData" onserverclick="DeleteCategoryData_ServerClick" runat="server">Delet Category's Data </button>
                                            <button type="submit" class="btn btn-primary" id="UpdateCategoryData" onserverclick="UpdateCategoryData_ServerClick" runat="server">Update Category's Data</button>
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
