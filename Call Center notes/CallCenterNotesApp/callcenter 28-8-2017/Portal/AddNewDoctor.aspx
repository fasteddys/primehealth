<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddNewDoctor.aspx.cs" Inherits="CallCenterNotesApp.Portal.AddNewDoctor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <!--breadcrumbs start -->
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="fa fa-home"></i> Home</a></li>
                        <li><a href="#">Add New Doctor</a></li>
                    </ul>
                    <!--breadcrumbs end -->
                </div>
            </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
                    <section class="content">
                        <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
                                Please Insert the Correct Full Data of Doctor 
                            </div>
                        <div class="alert alert-success alert-dismissable" runat="server" id="div_Success">
                                Your Doctor's data Inserted Correctly
                            </div>
                        <div class="row">
                            <div class="col-md-12">
                                <section class="panel">
                                    <header class="panel-heading"> <a href="ShowAllDoctors.aspx"><i class="fa fa-home"></i>  Show All Doctors </a>  </header>
                                    <div class="panel-body">
<%--                                        <form class="form-horizontal tasi-form" role="form" runat="server">--%>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Doctor Name : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="DoctorNameID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Doctor Address : </label>
                                                <div class="col-sm-2">
                                                    <input type="text" class="form-control input-lg m-b-10" id="DoctorAddressCodeID" runat="server">
                                                </div>
                                                <div class="col-sm-6">
                                                    <input type="text" class="form-control input-lg m-b-10" id="DoctorAddressID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group" >
                                                <label class="col-sm-2 col-sm-2 control-label">Doctor SubLocation :</label>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="DoctorSubLocationID" runat="server" class="form-control input-lg m-b-10" AutoPostBack="True"> </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Doctor Hotline : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="DoctorPhone1ID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Doctor Landline : </label>
                                               <div class="col-sm-2">
                                                    <input type="text" class="form-control input-lg m-b-10" id="SubLocationCode" runat="server">
                                                </div>
                                                 <div class="col-sm-6">
                                                    <input type="text" class="form-control input-lg m-b-10" id="DoctorPhone2ID" runat="server">
                                                </div>
                                                
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Doctor Mobile : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="DoctorPhone3ID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Other : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="DoctorPhone4ID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Doctor Title : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="DoctorTitleID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Doctor Notes : </label>
                                                <div class="col-sm-8" >
                                                    <textarea runat="server" id="DoctorNotesID" rows="3" style="width: 100%; font-weight: bold; font-size: large"></textarea>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Doctor Langitude : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="DoctorLangitudeID" runat="server">
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-2 col-sm-2 control-label"> Doctor Latitude : </label>
                                                <div class="col-sm-8">
                                                    <input type="text" class="form-control input-lg m-b-10" id="DoctorLatitudeID" runat="server">
                                                </div>
                                            </div>

                                            

                                            <div class="form-group" >
                                                <label class="col-sm-2 col-sm-2 control-label">Doctor Network :</label>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="DoctorNetworkID" runat="server" class="form-control input-lg m-b-10" >
                                                        <asp:ListItem Value="0">-- Enter Network type --</asp:ListItem>
                                                        <asp:ListItem Value="1">Q</asp:ListItem>
                                                        <asp:ListItem Value="2">L</asp:ListItem>
                                                        <asp:ListItem Value="3">M</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <label> <input type="checkbox" value="1" id="ISDCat" runat="server"> Belong To Network ( D ) </label>
                                                </div>
                                            </div>

                                            <div class="form-group" >
                                                <label class="col-sm-2 col-sm-2 control-label">Doctor Category :</label>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="DoctorCategoryID" runat="server" class="form-control input-lg m-b-10" > </asp:DropDownList>
                                                </div>
                                            </div>
                                            
                                            <div class="col-md-12" style="text-align:center;">
                                                <div class="toolbar-btn-action">
                                                    <button type="submit" class="btn btn-primary" id="btn_Submit" runat="server" onserverclick="SubmitNewDoctor" >Submit Doctor's Data</button>
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
