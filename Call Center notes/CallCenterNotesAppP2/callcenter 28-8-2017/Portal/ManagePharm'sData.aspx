<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ManagePharm'sData.aspx.cs" Inherits="CallCenterNotesApp.Portal.ManagePharm_sData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
        <div class="row">
            <div class="col-md-12">
                <ul class="breadcrumb">
                    <li><a href="#"><i class="fa fa-home"></i>  Home</a></li>
                    <li><a href="#"> Manage Pharm's Data </a></li>
                </ul>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
            <div class="alert alert-success" runat="server" id="div_Success_update">
                    Pharm Updated Successfully
                </div>
            <section class="content">
                <div class="row">
                    <div class="col-md-12">
                        <section class="panel">
                            <header class="panel-heading">View Your Data </header>
                            <div class="panel-body">
<%--                                <form class="form-horizontal tasi-form" role="form" runat="server">--%>
                                    <%--<div class="form-group" runat="server" id="TicketNumDiv"> 
                                        <div class="col-sm-12" align="Center">
                                            <label runat="server" id="IDLable" style="font-size: 38px;"></label>
                                        </div>
                                    </div>--%>
                                    
                                    
                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Pharm Name : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="PharmNameID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Pharm Address : </label>
                                                <div class="col-sm-2">
                                                    <input type="text" class="form-control input-lg m-b-10" id="PharmAddressCodeID" runat="server">
                                                </div>
                                                <div class="col-sm-6">
                                                    <input type="text" class="form-control input-lg m-b-10" id="PharmAddressID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Pharm SubLocation : </label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="PharmSubLocationID" runat="server" class="form-control m-b-10"></asp:DropDownList>
                                                    <label>
                                                        <input type="checkbox" value="1" id="ISDCat" runat="server">
                                                        Belong To Network ( D ) </label>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Pharm Hotline : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="PharmPhone1ID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Pharm Landline : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="PharmPhone2ID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Pharm Mobile : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="PharmPhone3ID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Other : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="PharmPhone4ID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Pharm Notes : </label>
                                                <div class="col-sm-8" >
                                                    <textarea runat="server" id="PharmNotesID" rows="3" class="form-control input-lg m-b-10"></textarea>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Pharm Langitude : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="PharmLangitudeID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label">Pharm Latitude : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="PharmLatitudeID" runat="server">
                                                </div>
                                            </div>

                                            
                                    <div class="col-md-12" style="text-align: center;">
                                        <div class="toolbar-btn-action">
<%--                                            <button type="submit" class="btn btn-primary" id="DeletePharmData" onserverclick="DeletePharmData_ServerClick" runat="server">Delet Pharm's Data </button>--%>
                                            <button type="submit" class="btn btn-primary" id="UpdatePharmData" onserverclick="UpdatePharmData_ServerClick" runat="server">Update Pharm bh's Data</button>
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
