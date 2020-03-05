<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ManageFeedback.aspx.cs" Inherits="CallCenterNotesApp.Portal.ManageFeedback" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
        <div class="row">
            <div class="col-md-12">
                <ul class="breadcrumb">
                    <li><a href="#"><i class="fa fa-home"></i>  Home</a></li>
                    <li><a href="#"> Manage Feedback </a></li>
                </ul>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
            <div class="alert alert-success" runat="server" id="div_Success_update">
                    Your Comments Added Successfuly
                </div>
    <div class="alert alert-danger" runat="server" id="div_Error_update">
                    Your Comments Not Added Successfuly
                </div>
            <section class="content">
                <div class="row">
                    <div class="col-md-12">
                        <section class="panel">
                            <header class="panel-heading">View Feedback Data </header>
                            <div class="panel-body">
<%--                                <form class="form-horizontal tasi-form" role="form" runat="server">--%>

                                    <div class="form-group" runat="server" id="TicketNumDiv"> 
                                        <div class="col-sm-12" align="Center">
                                            <label runat="server" id="FeedbackNum" style="font-size: 38px;"></label>
                                        </div>
                                    </div>

                                    <div class="form-group" id="UsernameDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label"> Username</label>
                                          <div class="col-lg-10" >
                                              <p id="Username" runat="server" class="form-control-static"></p>
                                          </div>
                                      </div>
                                    
                                    <div class="form-group" id="MedicalIdDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label"> Medical ID</label>
                                          <div class="col-lg-10" >
                                              <p id="MedicalID" runat="server" class="form-control-static"></p>
                                          </div>
                                      </div>

                                    <div class="form-group" id="FBTitleDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label"> Feedback Title</label>
                                          <div class="col-lg-10" >
                                              <p id="FBReqTitle" runat="server" class="form-control-static"></p>
                                          </div>
                                      </div>

                                    <div class="form-group" id="FBSubjectDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label"> Feedback Subject</label>
                                          <div class="col-lg-10" >
                                              <p id="FBSubject" runat="server" class="form-control-static"></p>
                                          </div>
                                      </div>

                                    <div class="form-group" id="FBCreationTimeDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label"> Creation Time</label>
                                          <div class="col-lg-10" >
                                              <p id="FBCreationTime" runat="server" class="form-control-static"></p>
                                          </div>
                                      </div>

                                    <div class="form-group" id="PrimeHealthCommentHeaderDiv" runat="server" style="font-weight: bold; font-size: large">
                                                <label class="col-sm-2 col-sm-2 control-label">Comment Title</label>
                                                <div class="col-sm-3">
                                                    <input type="text" class="form-control input-lg m-b-10" id="PrimeHealthCommentHeader" runat="server" style="width: 100%; font-weight: bold; font-size: large">
                                                </div>
                                            </div>

                                    <div class="form-group" id="PrimeHealthCommentDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label">PrimeHealth Comment</label>
                                          <div class="col-lg-10">
                                            <textarea runat="server" id="PrimeHealthComment" rows="4" style="width: 100%; font-weight: bold; font-size: large"></textarea>
                                          </div>
                                      </div>

                                    <div class="form-group" id="RepliedPersonDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label"> Replied Person</label>
                                          <div class="col-lg-10" >
                                              <p id="RepliedPerson" runat="server" class="form-control-static"></p>
                                          </div>
                                      </div>

                                    <div class="form-group" id="RepliedDateDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label"> Replied Date</label>
                                          <div class="col-lg-10" >
                                              <p id="RepliedDate" runat="server" class="form-control-static"></p>
                                          </div>
                                      </div>

                                    <div class="col-md-12" style="text-align: center;">
                                        <div class="toolbar-btn-action">
                                            <button type="submit" class="btn btn-primary" id="btn_Reply" onserverclick="UpdateFeedback_ServerClick" runat="server">Submit Reply</button>
                                             &nbsp;&nbsp;      <button type="submit" class="btn btn-danger" id="seen" onserverclick="SeenFeedback_ServerClick" runat="server" style="width: 103px">Seen</button>
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
