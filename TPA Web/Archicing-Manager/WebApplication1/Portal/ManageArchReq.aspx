<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="ManageArchReq.aspx.cs" Inherits="WebApplication1.Portal.ManageArchReq" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron" data-pages="parallax">
        <div class="container-fluid container-fixed-lg sm-p-l-20 sm-p-r-20">
            <div class="inner">

                <ul class="breadcrumb">
                    <li>
                        <p><a href="AllRequests.aspx">DashBoard</a></p>
                    </li>
                    <li><a href="#" onclick="JavaScript:window.history.back(1);return false;">Requests</a></li>
                    <li><a href="#" class="active">Manage Arch Requests</a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="container-fluid container-fixed-lg">
        <div class="widget form-group-default">
            <div class="widget-content padding">
                <div align="center">
                    <h2>Manage Arch Requests</h2>
                </div>
                <hr />  
                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label"></label>
                        <div class="col-sm-10" align="Right">
                            <a class="btn btn-success" runat="server" onserverclick="btn_AcceptForReview_ServerClick" id="btn_AcceptForReview"><i class="fa fa-check-circle-o"></i> Accept</a>
                        </div>
                    </div>
                </div>
                <br />              
                <div class="row">
                    <div class="form-group">
                        <div class="col-sm-12" align="Center">
                            <label runat="server" id="IDTicket" style="font-size: 38px;"></label>
                        </div>
                    </div>
                </div>
                <br />

                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Client Name </label>
                        <div class="col-sm-10">
                            <label runat="server" id="ClientName_lbl"></label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Month </label>
                        <div class="col-sm-10">
                            <label runat="server" id="Month_lbl" ></label>
                        </div>
                    </div>
                </div>

                <br />
                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Year </label>
                        <div class="col-sm-10">
                            <label runat="server" id="Year_lbl" ></label>
                        </div>
                    </div>
                </div>
                 <br />
                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Crator Name </label>
                        <div class="col-sm-10">
                            <label runat="server" id="CreatorName_lbl" ></label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Total Providers </label>
                        <div class="col-sm-10">
                            <label runat="server" id="TottalProviders_lbl" ></label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Total Excel </label>
                        <div class="col-sm-10">
                            <label runat="server" id="TottalExcel_lbl" ></label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Total Claims </label>
                        <div class="col-sm-10">
                            <label runat="server" id="TottalClaims_lbl" ></label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row" >
                    <div class="form-group" runat="server" id="NewTottalProviders">
                        <label for="input-text" class="col-sm-2 control-label"  >Please Enter Correct Total Providers</label>
                        <div class="col-sm-10">

                            <asp:TextBox  runat="server" id="NewTottalProviders_lbl" rows="1" style="font-weight: bold; font-size: small" Width="176px"  ></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row" >
                    <div class="form-group" runat="server" id="NewTottalExcel">
                        <label for="input-text" class="col-sm-2 control-label"  >Please Enter Correct Total Excel</label>
                        <div class="col-sm-10">

                            <asp:TextBox  runat="server" id="NewTottalExcel_lbl" rows="1" style="font-weight: bold; font-size: small" Width="176px"  ></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row" >
                    <div class="form-group" runat="server" id="NewTottalClaims">
                        <label for="input-text" class="col-sm-2 control-label"  >Please Enter Correct Total Claims</label>
                        <div class="col-sm-10">

                            <asp:TextBox  runat="server" id="NewTottalClaims_lbl" rows="1" style="font-weight: bold; font-size: small" Width="176px"  ></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                 <div class="form-group" runat="server" id="EntranceComment">
							<label for="input-text" class="col-sm-2 control-label">Enter Your Comments</label>
                     <div class="col-sm-10">
                         <asp:TextBox ID="EntranceArch_Comments" runat="server" Width="100%" TextMode="MultiLine"  Rows="6"></asp:TextBox>
							</div>
						  </div>
                    </div>
                <br />
                 <div class="row">
                 <div class="form-group" runat="server" id="TottalBlock">
							<label for="input-text" class="col-sm-2 control-label">Tottal Comments</label>
                     <div class="col-sm-10">
                         <asp:TextBox ID="TottalComments" runat="server" Width="100%" TextMode="MultiLine"  Rows="10"></asp:TextBox>
							</div>
						  </div>
                    </div>
                <div class="row">
                    <div class="form-group">
                        
                </div>
                <br />


                <div class="col-md-12">
                    <div class="toolbar-btn-action" align="center">
                        <a class="btn btn-success" runat="server" onserverclick="btn_GoToPending_ServerClick" id="btn_GoToPending"><i class="fa fa-check"></i> Go To Pending </a>
                        <input type="button" class="btn btn-primary" runat="server" id="btn_download" value="Download Original file" onserverclick="DownloadFile" />   
                        <a class="btn btn-success" runat="server" onserverclick="btn_Closed_ServerClick" id="btn_Closed"><i class="fa fa-check"></i> Closed </a>
                        <a class="btn btn-success" runat="server" onserverclick="btn_BackToArch_ServerClick" id="btn_BackToArch"><i class="fa fa-check"></i> Back To Creator </a>
                        <a class="btn btn-success" runat="server" onserverclick="btn_UpdateTottalProviders_ServerClick" id="btn_UpdateTotArch"><i class="fa fa-check"></i> UpdateTottalProviders </a>
                    </div>                     
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
