<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddNewLab.aspx.cs" Inherits="CallCenterNotesApp.Portal.AddNewLab" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <!--breadcrumbs start -->
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="fa fa-home"></i> Home</a></li>
                        <li><a href="#">Add New Lab / Scan</a></li>
                    </ul>
                    <!--breadcrumbs end -->
                </div>
            </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
                    <section class="content">
                        <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
                                Please Insert the Correct Full Data of Lab / Scan
                            </div>
                        <div class="alert alert-success alert-dismissable" runat="server" id="div_Success">
                                Your Lab / Scan data Inserted Correctly
                            </div>
                        <div class="row">
                            <div class="col-md-12">
                                <section class="panel">
                                    <header class="panel-heading"> <a href="ShowAllLabs.aspx"><i class="fa fa-home"></i>  Show All Labs / Scan </a> </header>
                                    <div class="panel-body">
<%--                                        <form class="form-horizontal tasi-form" role="form" runat="server">--%>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Lab / Scan Name : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="LabNameID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Lab / Scan Address : </label>
                                                <div class="col-sm-2">
                                                    <input type="text" class="form-control input-lg m-b-10" id="LabAddressCodeID" runat="server">
                                                </div>
                                                <div class="col-sm-6">
                                                    <input type="text" class="form-control input-lg m-b-10" id="LabAddressID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group" >
                                                <label class="col-sm-2 col-sm-2 control-label">Lab / Scan SubLocation :</label>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="LabSubLocationID" runat="server" class="form-control input-lg m-b-10" AutoPostBack="True"> </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Lab / Scan Hotline : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="LabPhone1ID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Lab / Scan Landline : </label>
                                                <div class="col-sm-2">
                                                    <input type="text" class="form-control input-lg m-b-10" id="SubLocationCode" runat="server">
                                                </div>
                                                <div class="col-sm-6">
                                                    <input type="text" class="form-control input-lg m-b-10" id="LabPhone2ID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Lab / Scan Mobile : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="LabPhone3ID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Other : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="LabPhone4ID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Lab / Scan Notes : </label>
                                                <div class="col-sm-8" >
                                                    <textarea runat="server" id="LabNotesID" rows="3" style="width: 100%; font-weight: bold; font-size: large"></textarea>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Lab / Scan Langitude : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="LabLangitudeID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Lab / Scan Latitude : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="LabLatitudeID" runat="server">
                                                </div>
                                            </div>

                                            

                                            <div class="form-group" >
                                                <label class="col-sm-2 col-sm-2 control-label">Lab / Scan Category :</label>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="LabCategoryID" runat="server" class="form-control input-lg m-b-10" >
                                                        <asp:ListItem Value="0">-- Enter Category type --</asp:ListItem>
                                                        <asp:ListItem Value="1">مراكز أشعه</asp:ListItem>
                                                        <asp:ListItem Value="2">معامل تحاليل</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <label> <input type="checkbox" value="1" id="ISDCat" runat="server"> Belong To Network ( D ) </label>
                                                </div>
                                            </div>
                                            
                                            <div class="col-md-12" style="text-align:center;">
                                                <div class="toolbar-btn-action">
                                                    <button type="submit" class="btn btn-primary" id="btn_Submit" runat="server" onserverclick="SubmitNewLab" >Submit Lab / Scan Data</button>
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
