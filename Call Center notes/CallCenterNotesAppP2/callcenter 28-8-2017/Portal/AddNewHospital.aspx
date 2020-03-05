<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddNewHospital.aspx.cs" Inherits="CallCenterNotesApp.Portal.AddNewHospital" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <!--breadcrumbs start -->
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="fa fa-home"></i> Home</a></li>
                        <li><a href="#">Add New Hospital</a></li>
                    </ul>
                    <!--breadcrumbs end -->
                </div>
            </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
                    <section class="content">
                        <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
                                Please Insert the Correct Full Data of Hospital 
                            </div>
                        <div class="alert alert-success alert-dismissable" runat="server" id="div_Success">
                                Your Hospital's data Inserted Correctly
                            </div>
                        <div class="row">
                            <div class="col-md-12">
                                <section class="panel">
                                    <header class="panel-heading"> <a href="ShowAllHospitals.aspx"><i class="fa fa-home"></i>  Show All Hospitals </a> </header>
                                    <div class="panel-body">
<%--                                        <form class="form-horizontal tasi-form" role="form" runat="server">--%>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Hospital Name : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="HospitalNameID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Hospital Address : </label>
                                                <div class="col-sm-2">
                                                    <input type="text" class="form-control input-lg m-b-10" id="HospitalAddressCodeID" runat="server">
                                                </div>
                                                <div class="col-sm-6">
                                                    <input type="text" class="form-control input-lg m-b-10" id="HospitalAddressID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group" >
                                                <label class="col-sm-2 col-sm-2 control-label">Hospital SubLocation :</label>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="HospitalSubLocationID" runat="server" class="form-control input-lg m-b-10" AutoPostBack="True"> </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Hospital Hotline : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="HospitalPhone1ID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Hospital Landline : </label>
                                                <div class="col-sm-2">
                                                    <input type="text" class="form-control input-lg m-b-10" id="SubLocationCode" runat="server">
                                                </div>
                                                <div class="col-sm-6">
                                                    <input type="text" class="form-control input-lg m-b-10" id="HospitalPhone2ID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Hospital Mobile : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="HospitalPhone3ID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Other : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="HospitalPhone4ID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Hospital Notes : </label>
                                                <div class="col-sm-8" >
                                                    <textarea runat="server" id="HospitalNotesID" rows="3" style="width: 100%; font-weight: bold; font-size: large"></textarea>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Hospital Langitude : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="HospitalLangitudeID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Hospital Latitude : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="HospitalLatitudeID" runat="server">
                                                </div>
                                            </div>

                                            

                                            <div class="form-group" >
                                                <label class="col-sm-2 col-sm-2 control-label">Hospital Network :</label>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="HospitalNetworkID" runat="server" class="form-control m-b-10" >
                                                        <asp:ListItem Value="0">-- Enter Network type --</asp:ListItem>
                                                        <asp:ListItem Value="1">Q</asp:ListItem>
                                                        <asp:ListItem Value="2">L</asp:ListItem>
                                                        <asp:ListItem Value="3">M</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <label> <input type="checkbox" value="1" id="ISDCat" runat="server"> Belong To Network ( D ) </label>
                                                </div>
                                            </div>

                                            <div class="form-group" >
                                                <label class="col-sm-2 col-sm-2 control-label">Hospital Category :</label>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="HospitalCategoryID" runat="server" class="form-control m-b-10" >
                                                        <asp:ListItem Value="0">-- Enter Category type --</asp:ListItem>
                                                        <asp:ListItem Value="1">مستشفيات</asp:ListItem>
                                                        <asp:ListItem Value="2">مراكز متخصصة</asp:ListItem>
                                                        <asp:ListItem Value="3">بصريات</asp:ListItem>
                                                        <asp:ListItem Value="4">عيادات متخصصة</asp:ListItem>
                                                        <asp:ListItem Value="5">عيادات خارجيه و مركز جراحات أطفال</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            
                                            <div class="col-md-12" style="text-align:center;">
                                                <div class="toolbar-btn-action">
                                                    <button type="submit" class="btn btn-primary" id="btn_Submit" runat="server" onserverclick="SubmitNewHospital" >Submit Hospital's Data</button>
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
