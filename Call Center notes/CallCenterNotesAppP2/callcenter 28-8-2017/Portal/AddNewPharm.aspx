<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddNewPharm.aspx.cs" Inherits="CallCenterNotesApp.Portal.AddNewPharm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <!--breadcrumbs start -->
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="fa fa-home"></i> Home</a></li>
                        <li><a href="#">Add New Pharm</a></li>
                    </ul>
                    <!--breadcrumbs end -->
                </div>
            </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
                    <section class="content">
                        <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
                                Please Insert the Correct Full Data of Pharm 
                            </div>
                        <div class="alert alert-success alert-dismissable" runat="server" id="div_Success">
                                Your Pharm's data Inserted Correctly
                            </div>
                        <div class="row">
                            <div class="col-md-12">
                                <section class="panel">
                                    <header class="panel-heading"> <a href="ShowAllPharms.aspx"><i class="fa fa-home"></i>  Show All Pharmacies </a> </header>
                                    <div class="panel-body">
<%--                                        <form class="form-horizontal tasi-form" role="form" runat="server">--%>

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

                                            <div class="form-group" >
                                                <label class="col-sm-2 col-sm-2 control-label">Pharm SubLocation :</label>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="PharmSubLocationID" runat="server" class="form-control input-lg m-b-10" AutoPostBack="True"> </asp:DropDownList>
                                                    <label> <input type="checkbox" value="1" id="ISDCat" runat="server"> Belong To Network ( D ) </label>
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
                                                <div class="col-sm-2">
                                                    <input type="text" class="form-control input-lg m-b-10" id="SubLocationCode" runat="server">
                                                </div>
                                                <div class="col-sm-6">
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
                                                <label class="col-sm-2 col-sm-2 control-label">  Other : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="PharmPhone4ID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Pharm Notes : </label>
                                                <div class="col-sm-8" >
                                                    <textarea runat="server" id="PharmNotesID" rows="3" style="width: 100%; font-weight: bold; font-size: large"></textarea>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Pharm Langitude : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="PharmLangitudeID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Pharm Latitude : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="PharmLatitudeID" runat="server">
                                                </div>
                                            </div>

                                            <div class="col-md-12" style="text-align:center;">
                                                <div class="toolbar-btn-action">
                                                    <button type="submit" class="btn btn-primary" id="btn_Submit" runat="server" onserverclick="SubmitNewPharm" >Submit Pharm's Data</button>
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
