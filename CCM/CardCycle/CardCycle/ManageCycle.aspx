<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Site2.Master"  AutoEventWireup="true" CodeBehind="ManageCycle.aspx.cs" Inherits="CardCycle.ManageClient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lbl_num_booklet" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Red" Text="Please Enter the Number Of Bookelt " Visible="False"></asp:Label>

    <div class="alert alert-danger" runat="server" id="div1">
                        Please Check Number Of Members 
                    </div>
        <div class="row">
    <div class="col-lg-12 col-md-6">
						<div class="widget pink-1 animated fadeInDown">
							<div class="widget-content padding">
								<div class="widget-icon">
									<i class="fa fa-edit"></i>
								</div>
								<div class="text-box">
									<p class="Client"><b>Client Notes</b>
                                        <br />
                                        <p><span class="animate-number" runat="server" id="spn_close" style="color:#fff">Take Care !!!</span></p>
									</p>
									<div class="clearfix"></div>
								</div>
							</div>
						</div>
					</div>
        </div>
    <div class="widget">
            <div class="widget-content padding">
                <div align="center"><h2>Manage Request</h2> </div>
                <hr />
                  <div class="alert alert-danger" runat="server" id="div_Error">
                              Not Allowed Opration For This Account Manager
                            </div>
                  <div class="row">
						  <div class="form-group">
							<label for="input-text" class="col-sm-2 control-label"></label>
							<div class="col-sm-10" align="Right">
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cardDBConnectionString %>" SelectCommand="SELECT [name] FROM [accountTB] WHERE ([type] = @type)">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="'It'" Name="type" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <asp:DropDownList ID="DropDownList1" runat="server" Font-Bold="True" Font-Size="Medium" DataSourceID="SqlDataSource2" DataTextField="name" DataValueField="name" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:DropDownList ID="drop_quality" runat="server" Font-Bold="True" Font-Size="Medium" Visible="False" OnSelectedIndexChanged="drop_quality_SelectedIndexChanged"></asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:cardDBConnectionString %>" SelectCommand="select name from accountTB where type='QualityControl'"></asp:SqlDataSource>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:cardDBConnectionString %>" SelectCommand="select name from accountTB where type='QualityControl' or type='It'or type='Issuance'"></asp:SqlDataSource>
                                
                               

                                <a class="btn btn-success" runat="server" id="btn_transfare" onserverclick="Transfare"><i class="fa fa-arrow-circle-right"></i> Transfare ISS</a>
                                <a class="btn btn-success" runat="server" id="btn_trnsfare_print" onserverclick="Transfare_print"><i class="fa fa-arrow-circle-right"></i> Transfare print</a>
                                <a class="btn btn-success" runat="server" id="btn_transfare_Qc" onserverclick="Transfare_QC"><i class="fa fa-arrow-circle-right"></i> Transfare QC</a>
                                <a class="btn btn-success" runat="server" id="btn_asign" onserverclick="AssignToMe"><i class="fa fa-arrow-circle-right"></i> Assign To Me Iss</a>
                                <a class="btn btn-success" runat="server" id="btn_asignPrint" onserverclick="AssignToMePrint"><i class="fa fa-arrow-circle-right"></i> Assign To Me Print</a>
                                <a class="btn btn-success" runat="server" id="btn_AsignQC" onserverclick="AssignToMeQC"><i class="fa fa-arrow-circle-right"></i> Assign To Me Quality</a>
                                <a class="btn btn-danger" runat="server" id="btn_Complain" onserverclick="Complain_Action"><i class="fa fa-arrow-circle-right"></i> Complain</a>
                                <asp:DropDownList ID="StatusDropDown" runat="server"  Font-Bold="True" AppendDataBoundItems="true" Font-Size="Medium" >
                                    <asp:ListItem>Issuing </asp:ListItem>
                                    <asp:ListItem>Printing</asp:ListItem>
                                    <asp:ListItem>Quality Control</asp:ListItem>
                                </asp:DropDownList>
                                <a class="btn btn-success" runat="server" id="btn_Change_Status" onserverclick="btn_Change_StatusServerClick"><i class="fa fa-arrow-circle-right"></i> Change Status</a>

                            </div>
						  </div>
                            </div>
                <br />
                <div class="row">
						  <div class="form-group">
							
							<div class="col-sm-12" align="Center">
                             <label runat="server" id="Label1" style="font-size:38px;"></label>
							</div>
						  </div>
                            </div>
                <br />
                 
						<div class="row" id="RSubject" runat="server">
						  <div class="form-group">
							<label for="input-text" class="col-sm-2 control-label">Request Subject</label>
							<div class="col-sm-10">
                             <label runat="server" id="lbl_Sub"></label>
							</div>
						  </div>
                            </div>
                <br />
                	<div class="row">
						  <div class="form-group">
							<label for="input-text" class="col-sm-2 control-label">Request Type</label>
							<div class="col-sm-10">
                             <label runat="server" id="Label2"></label>
							</div>
						  </div>
                            </div>

                <div class="row">
                 <div class="form-group" runat="server" id="ExcepCarea">
							<label for="input-text" class="col-sm-2 control-label">Exception Comments</label>
                     <div class="col-sm-10">
							  <textarea runat="server" id="ExcepComm" rows="10" style="width:100%;font-weight:bold;font-size:small" readonly  ></textarea>
							</div>
						  </div>
                    </div>

                <br />
                <div class="row" runat="server" id="ClientNameArea">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Client Name</label>
                        <div class="col-sm-10">
                            <label runat="server" id="ClientTxt"></label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row" >
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Number Of Members</label>
                        <div class="col-sm-10">
                            <label runat="server" id="NumOfCardsByACC"></label>
                        </div>
                    </div>
                </div>
                <br />

                <div class="row" runat="server" id="AccountManagerCommentsArea">
                 <div class="form-group">
							<label for="input-text" class="col-sm-2 control-label">Account Manager Comments</label>
                     <div class="col-sm-10">
							  <textarea runat="server" id="txtrBody" rows="10" style="width:100%;font-weight:bold;font-size:small" readonly  ></textarea>
							</div>
						  </div>
                     <br />
                    </div>
               
                
                <div class="row" runat="server" id="FinalNumberOfMembersArea">
                    <div class="form-group" runat="server" id="NCardsdivIss">
                        <label for="input-text" class="col-sm-2 control-label" >Final Number Of Members</label>
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" id="IssCardsNum" rows="1" style="font-weight: bold; font-size: small" Width="125px" Height="26px" ></asp:TextBox>
                        </div>
                    </div>
                 <br />
                </div>
                 <div class="row" runat="server" id="IssuanceCommentsArea">
                 <div class="form-group">
							<label for="input-text" class="col-sm-2 control-label">Issuance Comments</label>
                     <div class="col-sm-10">
                         <asp:TextBox ID="TextBox1" runat="server"  TextMode="MultiLine" Width="100%" Rows="6"></asp:TextBox>
							</div>
						  </div>
                       <br />
                    </div>
                   
                
                <%--      <asp:FileUpload ID="FileUpload1" runat="server" class="btn btn-default" />--%>
               <div class="row" runat="server" id="AccountBackCommentsArea">
                 <div class="form-group">
							<label for="input-text" class="col-sm-2 control-label">Account Back Comments</label>
                     <div class="col-sm-10">
                         <asp:TextBox ID="accountcom" runat="server" Width="100%" TextMode="MultiLine"  Rows="6"></asp:TextBox>
							</div>
						  </div>
                    <br />
                    </div>
               
                <div class="row" runat="server" id="PricingBackCommentsArea">
                 <div class="form-group">
							<label for="input-text" class="col-sm-2 control-label">Pricing Back Comments</label>
                     <div class="col-sm-10">
                         <asp:TextBox ID="pricingcom" runat="server" Width="100%" TextMode="MultiLine"  Rows="6"></asp:TextBox>
							</div>
						  </div>
                    <br />
                    </div>
                
                <div class="row" runat="server" id="QualityControlCommentsArea">
                 <div class="form-group">
							<label for="input-text" class="col-sm-2 control-label"> Quality Control Comments </label>
                     <div class="col-sm-10">
                         <asp:TextBox ID="QCcom" runat="server" Width="100%" TextMode="MultiLine"  Rows="6"></asp:TextBox>
							</div>
						  </div>
                    <br />
                    </div>
                

                <%--<input type="button" runat="server" value="Submit Request To Issuance" class="btn btn-success" onserverclick="Add_requestToIssue" />--%>

              <div class="row" runat="server" id="ClosingCommentsArea">
                 <div class="form-group">
							<label for="input-text" class="col-sm-2 control-label">Closing Comments</label>
                     <div class="col-sm-10">
                         <asp:TextBox ID="text2" runat="server" Width="100%" TextMode="MultiLine"  Rows="6"></asp:TextBox>
							</div>
						  </div>
                   <br />
                    </div>
                   
               
                <div class="row">
                 <div class="form-group" runat="server" id="EnterCompComments">
					<label for="input-text" class="col-sm-2 control-label">Add Complain Comments</label>
                     <div class="col-sm-10">
							  <textarea runat="server" id="ComplaintextEntrance" rows="3" style="width:100%;font-weight:bold;font-size:small"  ></textarea>
							</div>
						  </div>
                    </div>
                <br />
                <div class="row">
                 <div class="form-group" runat="server" id="ComplainComments">
					<label for="input-text" class="col-sm-2 control-label">Complain Comments</label>
                     <div class="col-sm-10">
							  <textarea runat="server" id="Complaintext" rows="10" style="width:100%;font-weight:bold;font-size:small" readonly  ></textarea>
							</div>
						  </div>
                    </div>
                <br />
                <div class="row" runat="server" id="div_NumberOfBookelt" visible="false">
                    <div class="form-group" runat="server" id="Div3">
                        <label for="input-text" class="col-sm-2 control-label" > Number Of Booklets</label>
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" id="txtNumberOfBookelt" rows="1" style="font-weight: bold; font-size: small" Width="125px" Height="26px" ></asp:TextBox>
                            
                        </div>
                    </div>
                 <br />
                </div>

                <br />
                <div class="row" runat="server" id="downloadarea">
                <div class="form-group">
							<label class="col-sm-2 control-label"> <span style="font-size:28px">&nbsp;<i class="glyphicon icon-attach-circled"></i></span> </label>
							<div class="col-sm-8">
                                <%--     <input type="button" runat="server" value="Submit Request To Issuance" class="btn btn-success" onserverclick="Add_requestToIssue" />--%>                          <%--     <input type="button" runat="server" value="Submit Request To Issuance" class="btn btn-success" onserverclick="Add_requestToIssue" />--%>&nbsp;&nbsp;&nbsp;&nbsp;  
                                <input type="button" class="btn btn-primary" runat="server" id="btn_download" value="Download Issuance file" onserverclick="DownloadFile" />
                                <input type="button" class="btn btn-primary" runat="server" id="btn_Original_download" value="Download Source file" onserverclick="DownloadOriginalFile" />
                                <asp:Label ID="lbmessage" runat="server" Text="lbmessage" Visible="False"></asp:Label>

                              
							</div>
                    <div class="col-sm-2">
                        <asp:FileUpload ID="FileUpload1" runat="server" class="btn btn-default" />
                        <br />
                        <asp:RegularExpressionValidator ID="rexp" runat="server" ControlToValidate="FileUpload1"
                             ErrorMessage="Only rar files "
                                ValidationExpression="(.*\.([Rr][Aa][Rr])$)" ForeColor="Red"></asp:RegularExpressionValidator>
                        </div>
						  </div>
                    </div>
                <br />	
                    <div class="col-md-12">
											<div class="toolbar-btn-action">
                                                <a class="btn btn-success" runat="server" id="btn_iss_print" onserverclick="ApvIssueAndPrint"><i class="fa fa-archive"></i> Issued and Print</a>
												<a class="btn btn-success" runat="server" id="btn_issue" onserverclick="ApvIssue"><i class="fa fa-edit"></i> Issued</a>
												<a class="btn btn-danger" runat="server" id="btn_print" onserverclick="ApvPrint"><i class="fa fa-print"></i> Printed</a>
												<a class="btn btn-danger" runat="server" id="btn_PTechnical" onserverclick="ApvPTech"><i class="fa fa-print"></i> Pending Technical</a>
												<a class="btn btn-primary" runat="server" id="btn_QC" onserverclick="ApvQC"><i class="fa fa-book"></i>Quality Control</a>
                                                <a class="btn btn-info" runat="server" id="btn_back_to_Issuance" onserverclick="BackToIssuance"><i class="glyphicon glyphicon-saved"></i> Back To Issuance</a> 
                                                 <a class="btn btn-info" runat="server" id="btn_AMConfirm" onserverclick="ApvACconfirm"><i class="glyphicon glyphicon-saved"></i> Confrim Print</a>
                                                <a class="btn btn-info" runat="server" id="btn_UWcconfirm" onserverclick="ApUWconfrim"><i class="glyphicon glyphicon-saved"></i> Confrim Print</a>

                                                 <a class="btn btn-info" runat="server" id="btn_AMExceptionConfirm" onserverclick="AMExceptionConfirm"><i class="glyphicon glyphicon-saved"></i> Confrim AC Exception</a>
                                                 <a class="btn btn-info" runat="server" id="btn_IssExceptionConfirm" onserverclick="IssExceptionConfirm"><i class="glyphicon glyphicon-saved"></i> Confrim Admin Exception</a>


                                                  <a class="btn btn-warning" runat="server" id="btn_Mang" onserverclick="ApvColse"><i class="fa fa-close"></i> Approve Request</a>

                                                <a class="btn btn-danger" runat="server" id="btn_reject" onserverclick="ApvReject"><i class="glyphicon glyphicon-floppy-remove"></i> Reject</a>
                                                <a class="btn btn-danger" runat="server" id="btn_reject_Seen" onserverclick="ApvRejectSeen"><i class="glyphicon glyphicon-floppy-remove"></i> Seen</a>

                                                <a class="btn btn-danger" runat="server" id="btn_Close" onserverclick="Apv_Issu_Close"><i class="glyphicon glyphicon-lock"></i> Close</a>
                                                <a class="btn btn-primary" runat="server" id="btn_fw" onserverclick="btn_fw_ServerClick"><i class="fa fa-arrow-right"></i>Forward</a>

	                                            <a class="btn btn-info" runat="server" id="AddComplain"    onserverclick="ApvAddComplain"><i class="glyphicon glyphicon-saved"></i> Add Complain</a>
                                                <a class="btn btn-info" runat="server" id="rejectComplain" onserverclick="ApvrejectComplain"><i class="glyphicon glyphicon-saved"></i> reject Complain</a>
                                                <a class="btn btn-info" runat="server" id="acceptComplain" onserverclick="ApvAcceptComplain"><i class="glyphicon glyphicon-saved"></i>Close And Accept</a>

                                            </div>
										</div>						
							<div class="row">
                                <div class="col-md-2">

                                </div>
                                <div class="col-md-10">
                               <%--     <input type="button" runat="server" value="Submit Request To Issuance" class="btn btn-success" onserverclick="Add_requestToIssue" />--%>
                                </div>
							</div>
							
							
						  
            </div>

        </div>
</asp:Content>
