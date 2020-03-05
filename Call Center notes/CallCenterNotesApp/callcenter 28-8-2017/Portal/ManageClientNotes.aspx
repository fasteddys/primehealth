<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ManageClientNotes.aspx.cs" Inherits="CallCenterNotesApp.Portal.ManageClientNotes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
        <div class="row">
            <div class="col-md-12">
                <ul class="breadcrumb">
                    <li><a href="#"><i class="fa fa-home"></i>  Home</a></li>
                    <li><a href="#"> Manage Client Notes</a></li>
                </ul>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
            <div class="alert alert-success" runat="server" id="div_Success_update">
                    Client Updated Successfully
                </div>
            <section class="content">
                <div class="row">
                    <div class="col-md-12">
                        <section class="panel">
                            <header class="panel-heading">View Your Data </header>
                            <div class="panel-body">
<%--                                <form class="form-horizontal tasi-form" role="form" runat="server">--%>
                                    <%--<div class="form-group" runat="server" id="TicketNumDiv"> 
                                        <div class="col-sm-12" align="Center">
                                            <label runat="server" id="IDLable" style="font-size: 38px;"></label>
                                        </div>
                                    </div>--%>
                                    <div class="form-group" id="ClientNotesCreatorDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label" style="color : blue"> Creator</label>
                                          <div class="col-lg-10" >
                                              <p id="ClientNotesCreator" runat="server" class="form-control-static"></p>
                                          </div>
                                      </div>
                                    
                                    <div class="form-group" id="CreationTimeDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label" style="color : blue">Creation Time</label>
                                          <div class="col-lg-10">
                                              <p id="CreationTime" runat="server" class="form-control-static" ></p>
                                          </div>
                                      </div>
                                    
                                    <div class="form-group" id="ClientNameDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label" style="color : blue">Client Name</label>
                                          <div class="col-lg-3">
<%--                                      <p id="ClientName" runat="server" class="form-control-static"></p>--%>
                                          <input type="text" class="form-control input-lg m-b-10" id="ClientName" runat="server">

                                          </div>
                                      </div>
                                   
                                    <div class="form-group" id="NotesDiv" runat="server" style="font-weight: bold; font-size: large">
                                          <label class="col-lg-2 col-sm-2 control-label" style="color : blue">Notes</label>
                                          <div class="col-lg-10">
                                            <textarea runat="server" id="Notes" rows="8" style="width: 100%; font-weight: bold; font-size: large"></textarea>

                                          </div>
                                      </div>
                                    
                                    <div class="form-group" id="TicketAttachmentTable" runat="server" style="font-weight: bold; font-size: large">
                                        <label class="col-lg-2 col-sm-2 control-label" style="color : blue">Attachments</label>
                                        
                                        <div class="col-lg-10">
                                            <asp:ListView runat="server" ID="ListViewNormal">
                                                
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton" OnClick="LinkButton2_Click" Path='<%#Eval("NoteAttachmentsPath")%>' FileName='<%#Eval("NoteAttachmentsName") %>' runat="server"> <%#Eval("NoteAttachmentsName") %></asp:LinkButton>
                                                    </nsfare>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                        <div class="col-lg-2">
                                            </div>
                                        <div class="col-lg-10">
                                            <asp:ListView runat="server" ID="ListViewManager">
                                            <ItemTemplate>
                                                &nbsp &nbsp
                                                <button id="DeleteButton" runat="server" onserverclick="DeleteAttachment_Click" style="all: unset;" AttachmentID='<%#Eval("NoteAttachmentsID")%>' isDeleted="False">
                                                  <span class="btn btn-danger fa fa-times"></span>
                                                  
                                              </button>
                                                 <asp:LinkButton ID="LinkButton2" OnClick="LinkButton2_Click" Path='<%#Eval("NoteAttachmentsPath")%>' FileName='<%#Eval("NoteAttachmentsName") %>' runat="server"> <%#Eval("NoteAttachmentsName") %></asp:LinkButton>
                                                <%-- <div class="btn-group btn-group-xs">
													              <a data-toggle="tooltip" href="CcEmailRequestManageRequest.aspx?id=<%#Eval("ID")%>" title="Edit" class="btn btn-default"><i class="fa fa-edit"></i></a>
													            </div>--%>
                                                        </nsfare>
                                            </ItemTemplate>

                                            </asp:ListView>
                                        </div>
                                        
                                    </div>
                                    <div class="form-group">
                                                <label class="col-lg-2 col-sm-2 control-label"></label>
                                                <div class="col-lg-10">
                                                    <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" class="btn btn-default" />
                                                </div>
                                            </div>

                                    <div class="col-md-12" style="text-align: center;">
                                        <div class="toolbar-btn-action">
                                            <button type="submit" class="btn btn-primary" id="btn_DeleteClientNotes" onserverclick="DeleteClientNotes_ServerClick" runat="server">Delete Client Notes</button>
                                            <button type="submit" class="btn btn-primary" id="btn_UpdateClientNotes" onserverclick="UpdateClientNotes_ServerClick" runat="server" >Update Client Notes</button>
<%--                                        <button type="submit" class="btn btn-primary" id="Button2" runat="server" >Submit Request</button>--%>
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
