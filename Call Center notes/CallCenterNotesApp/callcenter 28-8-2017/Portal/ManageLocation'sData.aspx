<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ManageLocation'sData.aspx.cs" Inherits="CallCenterNotesApp.Portal.ManageLocation_sData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
        <div class="row">
            <div class="col-md-12">
                <ul class="breadcrumb">
                    <li><a href="#"><i class="fa fa-home"></i>  Home</a></li>
                    <li><a href="#"> Manage Location's Data </a></li>
                </ul>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
            <div class="alert alert-success" runat="server" id="div_Success_update">
                    Location Updated Successfully
                </div>
            <section class="content">
                <div class="row">
                    <div class="col-md-12">
                        <section class="panel">
                            <header class="panel-heading">View Your Data </header>
                            <div class="panel-body">
<%--                                <form class="form-horizontal tasi-form" role="form" runat="server">--%>
                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Location Name : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="LocationNameID" runat="server">
                                                </div>
                                            </div>
                                            
                                    <div class="col-md-12" style="text-align: center;">
                                        <div class="toolbar-btn-action">
                                            <button type="submit" class="btn btn-primary" id="DeleteLocationData" onserverclick="DeleteLocationData_ServerClick" runat="server">Delet Location's Data </button>
                                            <button type="submit" class="btn btn-primary" id="UpdateLocationData" onserverclick="UpdateLocationData_ServerClick" runat="server">Update Location's Data</button>
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
