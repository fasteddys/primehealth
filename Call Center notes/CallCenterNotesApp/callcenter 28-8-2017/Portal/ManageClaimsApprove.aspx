<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ManageClaimsApprove.aspx.cs" Inherits="CallCenterNotesApp.Portal.ManageClaimsApprove" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
        <div class="row">
            <div class="col-md-12">
                <ul class="breadcrumb">
                    <li><a href="#"><i class="fa fa-home"></i>  Home</a></li>
                    <li><a href="#"> Manage Claims Approval </a></li>
                </ul>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
            <div class="alert alert-success" runat="server" id="div_Success_update">
                    Your Comments Added Successfuly
                </div>
                <div class="alert alert-success" runat="server" id="div_Error_update">
                    Pleaze Add  Your Comments 
                </div>
                <div class="alert alert-success" runat="server" id="div_Error_Download">
                    Your Files Not Found Please Contact IT Admin
                </div>
            <section class="content">
                <div class="row">
                    <div class="col-md-12">
                        <section class="panel">
                            <header class="panel-heading">View Your Data </header>
                            <div class="panel-body">
<%--                                <form class="form-horizontal tasi-form" role="form" runat="server">--%>

                                    <div class="form-group" runat="server" id="TicketNumDiv"> 
                                        <div class="col-sm-12" align="Center">
                                            <label runat="server" id="TicketNum" style="font-size: 38px;"></label>
                                        </div>
                                    </div>

                                    <div class="form-group" id="UsernameDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label"> Username</label>
                                          <div class="col-lg-10" >
                                              <p id="Username" runat="server" class="form-control-static"></p>
                                          </div>
                                      </div>
                                    
                                    <div class="form-group" id="CreationTimeDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label"> Creation Time</label>
                                          <div class="col-lg-10" >
                                              <p id="CreationTime" runat="server" class="form-control-static"></p>
                                          </div>
                                      </div>

                                    <div class="form-group" id="MedicalIdDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label"> Medical ID</label>
                                          <div class="col-lg-10" >
                                              <p id="MedicalID" runat="server" class="form-control-static"></p>
                                          </div>
                                      </div>

                                    <div class="form-group" id="ReqtypeDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label"> Request type</label>
                                          <div class="col-lg-10" >
                                              <p id="Reqtype" runat="server" class="form-control-static"></p>
                                          </div>
                                      </div>

                                    <div class="form-group" id="ReqTitleDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label"> Request Title</label>
                                          <div class="col-lg-10" >
                                              <p id="ReqTitle" runat="server" class="form-control-static"></p>
                                          </div>
                                      </div>

                                    <div class="form-group" id="RequestSubjectDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label"> Request Subject</label>
                                          <div class="col-lg-10" >
                                              <p id="RequestSubject" runat="server" class="form-control-static"></p>
                                          </div>
                                      </div>
                                    
                                    <div class="form-group" id="UserImageDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label"> User Image</label>
                                          <div class="col-lg-10" >
                                            <button type="submit" class="btn btn-danger" id="UserImage" onserverclick="DownloadClientImage" runat="server">Download Image 1</button>
                                            <button type="submit" class="btn btn-danger" id="UserImage2" onserverclick="DownloadClientImage2" runat="server">Download Image 2</button>
                                            <button type="submit" class="btn btn-danger" id="UserImage3" onserverclick="DownloadClientImage3" runat="server">Download Image 3</button>
                                            <button type="submit" class="btn btn-danger" id="UserImage4" onserverclick="DownloadClientImage4" runat="server">Download Image 4</button>
                                            <button type="submit" class="btn btn-danger" id="UserImage5" onserverclick="DownloadClientImage5" runat="server">Download Image 5</button>
                                          </div>
                                      </div>

                                    <div class="form-group" id="CallCenterCommentDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label">CallCenter Comment</label>
                                          <div class="col-lg-10">
                                            <textarea runat="server" id="CallCenterComment" rows="4" style="width: 100%; font-weight: bold; font-size: large" ></textarea>

                                          </div>
                                      </div>

                                    <div class="form-group" id="approveimageDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label">Upload image</label>
                                          <div class="col-lg-10">
                                                <asp:FileUpload ID="FileUpload1" runat="server" class="btn btn-default" />
                                                <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Pleaze Choose A File" Visible="False"></asp:Label>
                                                <br />
                                                <asp:RegularExpressionValidator ID="rexp" runat="server" ControlToValidate="FileUpload1" ValidationGroup="SendApprovalOrRejection"
                                                    ErrorMessage="Only .pdf files "
                                                    ValidationExpression="(.*\.([Pp][Dd][Ff])$)" ForeColor="Red"></asp:RegularExpressionValidator>
                                          </div>
                                      </div>

                                    <div class="form-group" id="CallCentgerImageDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label"> CallCenter Image</label>
                                          <div class="col-lg-10" >
                                            <button type="submit" class="btn btn-primary" id="CallCentgerImage" onserverclick="DownloadCallCenterImage" runat="server">Download CallCenter Image</button>
                                          </div>
                                      </div>

                                    <div class="form-group" id="CallCenterCommentAddedByDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label"> Added By</label>
                                          <div class="col-lg-10" >
                                              <p id="CallCenterCommentAddedBy" runat="server" class="form-control-static"></p>
                                          </div>
                                      </div>

                                    <div class="form-group" id="CallCenterCommentAddedTimeDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label"> Addition Time</label>
                                          <div class="col-lg-10" >
                                              <p id="CallCenterCommentAddedTime" runat="server" class="form-control-static"></p>
                                          </div>
                                      </div>

                                    <div class="col-md-12" style="text-align: center;">
                                        <div class="toolbar-btn-action">
                                            <button type="submit" class="btn btn-primary"  ValidationGroup="SendApprovalOrRejection" id="btn_Approve" onserverclick="btn_Approve_ServerClick" runat="server">Approve Request</button>
                                            <button type="submit" class="btn btn-success"  ValidationGroup="SendApprovalOrRejection" id="btn_Reject"  onserverclick="btn_Reject_ServerClick" runat="server">Reject Request</button>
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
