<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Site2.Master"  AutoEventWireup="true" CodeBehind="ITCotrolPanel.aspx.cs" Inherits="CardCycle.ITCotrolPanel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <!-- Start info box -->
				<div class="row top-summary">
					<div class="col-lg-3 col-md-6">
						<div class="widget green-1 animated fadeInDown">
							<div class="widget-content padding">
								<div class="widget-icon">
									<i class="icon-paper-plane"></i>
								</div>
								<div class="text-box">
									<p class="maindata">New <b>Requests</b>
                                        <br />
                                        <p><span class="animate-number" runat="server" id="spn_New">254</span></p>
									</p>
									<div class="clearfix"></div>
								</div>
							</div>
							<div class="widget-footer">
								<div class="row">
									<div class="col-sm-12">
										<i class="fa fa-caret-up rel-change"></i> <b></b> 
									</div>
								</div>
								<div class="clearfix"></div>
							</div>
						</div>
					</div>

					<div class="col-lg-3 col-md-6">
						<div class="widget darkblue-2 animated fadeInDown">
							<div class="widget-content padding">
								<div class="widget-icon">
									<i class="icon-bag"></i>
								</div>
								<div class="text-box">
									<p class="maindata">TOTAL <b>Requests</b>
                                        <br />
                                        <p><span class="animate-number" runat="server" id="spn_total">254</span></p>
									</p>
									

									<div class="clearfix"></div>
								</div>
							</div>
							<div class="widget-footer">
								<div class="row">
									<div class="col-sm-12">
										<i class="fa fa-caret-down rel-change"></i> <b></b> 
									</div>
								</div>
								<div class="clearfix"></div>
							</div>
						</div>
					</div>

					<div class="col-lg-3 col-md-6">
						<div class="widget pink-1 animated fadeInDown">
							<div class="widget-content padding">
								<div class="widget-icon">
									<i class="fa fa-lock"></i>
								</div>
								<div class="text-box">
									<p class="maindata">Closed <b>Requestes</b>
                                        <br />
                                        <p><span class="animate-number" runat="server" id="spn_close" style="color:#fff">254</span></p>
									</p>
									
									<div class="clearfix"></div>
								</div>
							</div>
							<div class="widget-footer">
								<div class="row">
									<div class="col-sm-12">
										<i class="fa fa-caret-down rel-change"></i> <b></b> 
									</div>
								</div>
								<div class="clearfix"></div>
							</div>
						</div>
					</div>

					<div class="col-lg-3 col-md-6">
						<div class="widget lightblue-1 animated fadeInDown">
							<div class="widget-content padding">
								<div class="widget-icon">
									<i class="fa fa-wrench"></i>
								</div>
								<div class="text-box">
									<p class="maindata">Pending <b>Requestes</b>
                                        <br />
                                        <p><span class="animate-number" runat="server" id="spn_pend">254</span></p>
									</p>
									
									<div class="clearfix"></div>
								</div>
							</div>
							<div class="widget-footer">
								<div class="row">
									<div class="col-sm-12">
										<i class="fa fa-caret-up rel-change"></i> <b></b> 
									</div>
								</div>
								<div class="clearfix"></div>
							</div>
						</div>
					</div>

				</div>
				<!-- End of info box -->
    <div class="row">
        <div class="col-md-12">
        <div class="widget">
            <div class="widget-content padding" align="center">
                <div align="center"><h4>Card Cycle Manager </h4> </div>
                <hr />
                <img src="image/logo-color.png" style="" />
                </div>
            </div>
            </div>
        </div>
</asp:Content>
