<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ProviderDashboard.aspx.cs" Inherits="CallCenterNotesApp.Portal.ProviderDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
           <link href="../assets/css/jquery.dataTables.css" rel="stylesheet" />

    <div class="row">
                <div class="col-md-12">
                    <!--breadcrumbs start -->
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="fa fa-home"></i> Home</a></li>
                        <li><a href="#">Provider Dahsboard</a></li>
                    </ul>
                    <!--breadcrumbs end -->
                </div>
            </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <section class="content">

                    <div class="row" style="margin-bottom:5px;">


                        <div class="col-md-4">
                            <div class="sm-st clearfix">
                                <span class="sm-st-icon st-red" ><i class="fa fa-compass"></i></span>
                                <div class="sm-st-info">
                                    <span id="TottalDoctors" runat="server">0</span>
                                    Total Doctors
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="sm-st clearfix">
                                <span class="sm-st-icon st-violet"><i class="fa fa-file-text-o"></i></span>
                                <div class="sm-st-info">
                                    <span id="TottalHospitals" runat="server">0</span>
                                    Total Hospitals
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="sm-st clearfix">
                                <span class="sm-st-icon st-blue"><i class="fa fa-bullhorn"></i></span>
                                <div class="sm-st-info">
                                    <span id="TottalPharmacies" runat="server">0</span>
                                    Total Pharmacies
                                </div>
                            </div>
                        </div>
                                            </div>
                            <div class="row" style="margin-bottom:5px;">

                        <div class="col-md-4">
                            <div class="sm-st clearfix">
                                <span class="sm-st-icon st-blue"><i class="fa fa-dedent"></i></span>
                                <div class="sm-st-info">
                                    <span id="TottalLabs" runat="server">0</span>
                                    Total Labs / Scan
                                </div>
                            </div>
                        </div>

                        <div class="col-md-4">
                            <div class="sm-st clearfix">
                                <span class="sm-st-icon st-green"><i class="fa fa-dedent"></i></span>
                                <div class="sm-st-info">
                                    <span id="TottalOpticals" runat="server">0</span>
                                    Total Optical
                                </div>
                            </div>
                        </div>

                                <div class="col-md-4">
                            <div class="sm-st clearfix">
                                <span class="sm-st-icon st-violet"><i class="fa fa-ban"></i></span>
                                <div class="sm-st-info">
                                    <span id="TottalProviders" runat="server">0</span>
                                    Total PH Providers
                                </div>
                            </div>
                        </div>
                    </div>
        
                    <div class="row">
                        <div class="col-md-12">
                            <div class="widget">
                                <div class="widget-content padding" align="center">
                                    
                                    <img src="../assets/img/medical_4-582x249.png" style="" />
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
      
       <script src="../assets/js/jquery.min.js"></script>
    <script src="../assets/js/jquery.js"></script>
    <script src="../assets/js/jquery.dataTables.js"></script>
    <script>
        $(document).ready(function () {
            $('#example').DataTable();
        });
    </script>
    
</asp:Content>

