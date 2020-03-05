<%@ Page ValidateRequest="false" Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="CcEmailRequestManageRequest.aspx.cs" Inherits="CallCenterNotesApp.Portal.CcEmailRequestManageRequest" %>



<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <link href="../assets/css/bootstrap-select.min.css" rel="stylesheet" />
    <div class="row">
        <div class="col-md-12"> 
            <ul class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Manage Callcenter Email Request</a></li>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
      <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
        Error , Please Verify Inputs
    </div>
    <div class="alert alert-success alert-dismissable" runat="server" id="div_Success">
        Success
    </div>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <section class="panel" >
<%--                    <form class="form-horizontal tasi-form" role="form" runat="server" enctype="multipart/form-data">--%>
                    <header class="panel-heading">
                        <div class ="row">
                            <div class="col-sm-3">
                        <h4>Request Details</h4>
                                </div>
                                      
                   </div>
                    </header>
                        <div class="row">
                         <div class="col-sm-6" align="Center" style="margin-top:10px; margin-bottom:10px">
                                <label id="TicketID" runat="server" class="pull-right" style="font-size: 38px; padding-bottom: 15px ;color:goldenrod">3290</label>
                            </div>
                          
                            
                            <%--------------------------------Transfare To Bool-------------------------------------------%>                      

                            <div class="col-sm-6 ">
                          <div class="form-group"  >
                        <div runat="server" id="ViewTransfer" style="margin-top: 10px"  >
                            <label class="col-sm-4 col-sm-4 control-label" >Transfare Request</label>
                            <textarea ID="TransferComment" placeholder="Transfare Reason..." runat="server" CssClass="input-sm"></textarea> 
                            <button type="submit" class="btn btn-danger " runat="server" validationgroup="TransferNotes" OnServerClick="transfer_ServerClick" id="TransfareBool" style="margin-left: 15px">Transfare To Pool</button>
                                                       
                            </div>
                           <div>
                              <button id="Release" runat="server" style="float:left; margin-left:290px; margin-top:30px;" onserverclick="Release_ServerClick" class="btn btn-danger" type="submit">Release</button>
                              </div>
                            </div>
                                </div>
                            <div class="pull-right" style="margin-right:290px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                            ErrorMessage="Reason Is Required" ForeColor="Red"
                                            ControlToValidate="TransferComment"  ValidationGroup="TransferNotes">                                                  
                                        </asp:RequiredFieldValidator>
                                </div>


                            <div class="col-sm-6 " align="Center">
                                <div class="form-group"  >
                                    <div runat="server" id="ExtractReport" style="margin-top: 10px"  >
                                        <button type="submit" class="btn btn-primary " runat="server" validationgroup="TransferNotes" OnServerClick="ExtractReportFun" id="ExtractReportBt" style="margin-left: 15px">Extract Request Report</button>
                                    </div>
                              </div>
                            </div>


         

                            <div class="col-sm-6 ">
                          <div class="form-group"  >
                        <div runat="server" id="AsssignComments" style="margin-top: 10px"  >
                            <label class="col-sm-4 col-sm-4 control-label" >Transfer Authentication To Manager</label>
                            <textarea ID="AssginComment" placeholder="Reauthentication Reason..." runat="server" CssClass="input-sm"></textarea> 
                            <button type="submit" class="btn btn-danger " 
                                runat="server" validationgroup="AssginNote" 
                                OnServerClick="AssignRequestToManager" id="AssginToMeMGR" style="margin-left: 15px">Assgin To Me</button>
                         
                            </div>
                            </div>
                                </div>


                            <div class="pull-right" style="margin-right:290px">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                            ErrorMessage="Reauthentication Is Required" ForeColor="Red"
                                            ControlToValidate="AssginComment"  ValidationGroup="AssginNote">
                                                   
                                        </asp:RequiredFieldValidator>
                                </div>




              </div>   
                      
                        <br />
                        <br />
                    <div class="panel-body" style="margin-left:10px;padding-right:40px" >
                        
                            <div style="top: 30px">
                                </div>
                                <div class="form-group" runat="server">
                                    <label class="col-sm-2 col-sm-2 control-label">Request Subject</label>
                                    <div class="col-sm-10">
                                        <input type="text" class="form-control input-lg m-b-9" id="TicketSubjectID" runat="server" readonly >
                                    </div>
                                   
                                </div>
                        <br />                        <br />


                                <div class="form-group" runat="server">
                                    <label class="col-sm-2 col-sm-2 control-label">User Medical ID  </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" ID="UserMedicalID" runat="server" readonly>
                                  
                                         <asp:LinkButton ID="EditButton" runat="server" EnableTheming="True" onclick="EditUserMedicalId"
                                                        >Edit</asp:LinkButton>

                                      
                                         <asp:LinkButton ID="SaveButton" runat="server" EnableTheming="True" onclick="SaveUserMedicalId" 
                                                        >Save </asp:LinkButton>

                                         <asp:LinkButton ID="CancelButton" runat="server" EnableTheming="True" onclick="CancelUserMedicalId" 
                                                         >| Cancel</asp:LinkButton>
                                        
                                    </div>
                                        
                                    <label class="col-sm-2 col-sm-2 control-label">Request Type</label>
                                    <div class="col-sm-4">
                                            <asp:DropDownList runat="server" ID="TicketTypeIDID"  Width="100%" Height="45px">
                                                <asp:ListItem Value="1">General</asp:ListItem>
                                                <asp:ListItem Value="2">Special</asp:ListItem>
                                            </asp:DropDownList>
                                        <br />
                                        <asp:LinkButton ID="EditTicketTypeBTN" runat="server" EnableTheming="True"  onclick="EditTicketType"
                                                    >Edit</asp:LinkButton>
                                    <asp:LinkButton ID="SaveTicketTypeBTN" runat="server" EnableTheming="True" onclick="SaveTicketType" 
                                                    >save </asp:LinkButton>
                                    <asp:LinkButton ID="CancelTicketTypeBTN" runat="server" EnableTheming="True" onclick="CancelTicketType" 
                                                    >| cancel</asp:LinkButton>
                                        <br />
                                        <br />
                                    </div>                                                                                                                                     
                                          
                                    <%-- <label class="col-sm-2 col-sm-2 control-label">Request Type </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="TicketTypeIDID" runat="server" disabled>
                                    </div>

                                </div>--%>


                                <%--Update----------------------------------------------------------------------------%>
                                <div class="form-group" id="PatientNameDiv" runat="server">
                                    
                                    <label class="col-sm-2 col-sm-2 control-label">Member Name  </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="PatientNameID" runat="server" disabled>
                                    </div>
                                      <label class="col-sm-2 col-sm-2 control-label">Request Creation Time </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="RequestCreationTimeID" runat="server" disabled>
                                    </div>
                                </div>

                                <div class="form-group" id="CompanyNameDiv" runat="server">
                                    <label class="col-sm-2 col-sm-2 control-label">Request Creator  </label>
                                    

                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="RequestCreatorID" runat="server" disabled>
                                    </div>
                                    <label class="col-sm-2 col-sm-2 control-label">Company Name </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="CompanyNameID" runat="server" disabled>
                                    </div>
                                </div>

                                  <div class="form-group" id="Div5" runat="server">
                                    <label class="col-sm-2 col-sm-2 control-label">Priority  </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="PriorityID" runat="server" disabled>
                                    </div>
                                      
                                                                       
                                                                         
                                      <%--------------------------------------------------------Buttons-------------------------------%>
                                      
                                    <label class="col-sm-2 col-sm-2 control-label">Approval Category</label>
                                    <div class="col-sm-4">
                                       

                                            <asp:DropDownList runat="server" ID="ApprovalCategory"  Width="100%" Height="45px">
                                                <asp:ListItem Value="1">Inpatient</asp:ListItem>
                                                <asp:ListItem Value="2">Outpatient</asp:ListItem>
                                                <asp:ListItem Value="3">Medication</asp:ListItem>
                                            </asp:DropDownList>
                                                   
                                        <asp:LinkButton ID="UpdateLinkButton" runat="server" EnableTheming="True" onclick="EditApprovalCategory"
                                                     >Edit</asp:LinkButton>

                                      <asp:LinkButton ID="SaveLinkButton" runat="server" EnableTheming="True" onclick="SaveApprovalCategory" 
                                                      >save </asp:LinkButton>
                                    
                                      <asp:LinkButton ID="CancelLinkButton" runat="server" EnableTheming="True" onclick="CancelApprovalCategory" 
                                                      >| cancel</asp:LinkButton>      
                                        <br />

                                        </div>
                                </div>


                                <div class="form-group" id="RequestCreationTimeDiv" runat="server">
                                    <label class="col-sm-2 col-sm-2 control-label">Request Status </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="RequestStatusID" runat="server" disabled>
                                    </div>
                                    
                                     <label class="col-sm-2 col-sm-2 control-label">Opreator name </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="OpreatorName" runat="server" disabled>
                                    </div>


                                </div>
                                      <div class="form-group" id="Div6" runat="server">
                                   
                                    <label class="col-sm-2 col-sm-2 control-label">Request Source </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="MailSource" runat="server" disabled>
                                    </div>
                                           <label class="col-sm-2 col-sm-2 control-label">Close  Time </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="CloseTime" runat="server" disabled>
                                    </div>
                                </div>
                                <div class="form-group" id="FaxNumberdiv" runat="server">
                                   
                                 <label class="col-sm-2 col-sm-2 control-label">Fax Number</label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" ID="FaxNumber" runat="server" readonly>
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Enter valid Phone number" ControlToValidate="FaxNumber" ForeColor="Red" ValidationExpression="^[1-9]\d*|0\d+" ></asp:RegularExpressionValidator> 

                                         <asp:LinkButton ID="EditFaxNumberbtn" runat="server" EnableTheming="True" onclick="EditFaxNumber"
                                                        >Edit</asp:LinkButton>

                                      
                                         <asp:LinkButton ID="SaveFaxNumberbtn" runat="server" EnableTheming="True" onclick="SaveFaxNumber" 
                                                        >Save </asp:LinkButton>

                                         <asp:LinkButton ID="CancelFaxNumberbtn" runat="server" EnableTheming="True" onclick="CancelFaxNumber" 
                                                         >| Cancel</asp:LinkButton>
                                        
                                    </div>
                                </div>
                                       
                             
                                 





                            
                                <div class="form-group" id="TicketAttachmentTable" style="margin-left:65px;padding-right:40px" runat="server">
                                       <label>Request Attachments</label>

                                    <asp:ListView runat="server" ID="TicketAttachment">
                                        <ItemTemplate>
                                            &nbsp &nbsp

                                             <asp:LinkButton ID="LinkButton2" OnClick="LinkButton2_Click" Path='<%#Eval("Path")%>' FileName='<%#Eval("Name") %>' runat="server"> <%#Eval("Name") %></asp:LinkButton>
                                            <%-- <div class="btn-group btn-group-xs">
													          <a data-toggle="tooltip" href="CcEmailRequestManageRequest.aspx?id=<%#Eval("ID")%>" title="Edit" class="btn btn-default"><i class="fa fa-edit"></i></a>
													        </div>--%>
                                                    </nsfare>
                                        </ItemTemplate>

                                    </asp:ListView>
                                </div>
                            </div>
                        <br />
                            <div style="margin-top: 5px; margin-bottom: 5px; height: 2px; width: 100%; border-top: 1px solid gray;"></div>
                            <div class="form-group  col-md-12" id="UserNotesDiv" runat="server">
                                    <label  class=" col-sm-2 control-label">Request Body</label>
                                    <div class="col-sm-10">
                                        <FTB:FreeTextBox ID="UserNotes" ReadOnly="true" EnableHtmlMode="false" runat="server" Height="320px" Width="100%" BackColor="237, 237, 237" ButtonSet="OfficeMac" EditorBorderColorDark="238, 238, 238" EditorBorderColorLight="238, 238, 238" GutterBackColor="238, 238, 238" GutterBorderColorDark="238, 238, 238" GutterBorderColorLight="238, 238, 238">
                                        </FTB:FreeTextBox>

                                    </div>
                                </div>
                           <%-- <div style="bottom: 40px; padding-top: 10px">
                            </div>--%>
                            <div class="row">
                                <div class="col-md-6" style="border-right:solid; border-right-color:gainsboro">
                                    <header class="panel-heading" style="margin-bottom: 15px">
                                        <h4>Doctor Notes</h4>
                                    </header>
                                    <div class="form-group" id="AssignedDoctorNameDiv" runat="server">
                                        <label class="col-sm-4 col-sm-4 control-label">Assigned Doctor Name</label>
                                        <div class="col-sm-6">
                                            <input type="text" class="form-control input-lg m-b-10" id="AssignedDoctorNameID" runat="server" disabled>
                                        </div>
                                    </div>
                                    <div class="form-group" id="CommentToTransFare" runat="server">
                                        <label class="col-sm-4 col-sm-4 control-label">Transfare To Audit Notes</label>
                                        <div class="col-sm-6">
                              <textarea runat="server" class="form-control input-lg m-b-10" id="TransfareRequestToAuditNote" cols="20" rows="2"></textarea>


                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ErrorMessage="Notes Is Required" ForeColor="Red"
                                            ControlToValidate="TransfareRequestToAuditNote" Display="Dynamic" ValidationGroup="AuditNotesvalid">
                                                   
                                        </asp:RequiredFieldValidator>

                                        <br />
                         <asp:FileUpload ID="FileUpload3" AllowMultiple="true" runat="server" />
                

                                    </div>

                                    <div class="col-sm-4">
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:Button ID="TransferToAuditbutton" OnClick="TransferToAudit" CssClass="btn btn-danger" ValidationGroup="AuditNotesvalid" runat="server" Text="Transfer To Audit" />
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <div class="row">
                                    <div class="form-group" id="Div3" runat="server">
                                        <label class="col-sm-4 col-sm-4 control-label">Technical Approve Notes By Doctor</label>
                                        <div class="col-sm-6">
                                            <textarea runat="server" name="txtImagename1" ValidationGroup="TecnecalApproveDoctor"
                                                 class="form-control input-lg m-b-10" id="TechnicalApproveCommentByDoctor"
                                                 cols="20" rows="2"></textarea>
                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="TecnecalApproveDoctor"
                                            ErrorMessage="Notes Is Required" ForeColor="Red"
                                            ControlToValidate="TechnicalApproveCommentByDoctor" Display="Dynamic">
                                                   
                                        </asp:RequiredFieldValidator>
               
                                        <asp:FileUpload ID="UploadTechnicalApproveDoctorAttachment" AllowMultiple="true" runat="server" accept=".PDF" />
                                        <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="EndTecnecalApprove" 
                                            ControlToValidate="UploadTechnicalApproveDoctorAttachment"  ForeColor="Red" ID="RequiredFieldValidatorDoctorEndTecnecalApprove" runat="server"
                                            ErrorMessage="Attachment(s) Is Required"></asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="RegExValFileUploadFileType" runat="server"
                        ControlToValidate="UploadTechnicalApproveDoctorAttachment"
                        ErrorMessage="Only .pdf Files are allowed" Font-Bold="True" ValidationGroup="EndTecnecalApprove"
                        Font-Size="Medium" ForeColor="Red"
                        ValidationExpression="(.*?)\.(pdf)$"></asp:RegularExpressionValidator>
                                    </div>
                                        </div>
                                    <div class="row">
                                        <div class="form-group" id="DoctorAttachTable" runat="server">
<%--                                            <br />
                                            <br />
                                            <br />
                                            <br />--%>
                                            <label style="" class="col-sm-4 col-sm-4 control-label"">Doctor Attachments</label>
                                            <div class="col-sm-6">
                                            <asp:ListView runat="server" ID="DoctorAttachment">
                                                <ItemTemplate>
                                                    &nbsp &nbsp
                                                <asp:LinkButton ID="LinkButton2" OnClick="LinkButton2_Click" Path='<%#Eval("Path")%>' FileName='<%#Eval("Name") %>' runat="server"> <%#Eval("Name") %></asp:LinkButton>
                                                    </tr>
                                                </ItemTemplate>

                                            </asp:ListView>
                                           

                                                </div>


                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="form-group" id="Div2" runat="server">
                                            <div class="col-sm-7">

                                                   <label style="" class="col-sm-7 col-sm-7 control-label"">Doctor Technical Approve</label>

                                                <asp:ListView runat="server" ID="DoctorTechnicalApprove">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton2" OnClick="LinkButton2_Click" Path='<%#Eval("Path")%>' FileName='<%#Eval("Name") %>' runat="server"> <%#Eval("Name") %></asp:LinkButton>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>

                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <header class="panel-heading" style="margin-bottom: 15px">
                                        <h4>Audit Notes</h4>
                                    </header>
                                    <div class="form-group" id="AuditApprovalNameDiv" runat="server">
                                        <label class="col-sm-3 col-sm-3 control-label">Audit Name </label>
                                        <div class="col-sm-6">
                                            <input type="text" class="form-control input-lg m-b-10" id="AuditApprovalNameID" runat="server" disabled>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
               


                                     <div class="form-group" id="Div4" runat="server">
                                        <label class="col-sm-3 col-sm-3 control-label">Technical Approve Notes By Audit</label>
                                      
                                            <div class="col-sm-6 ">
                                            <textarea runat="server" class="form-control input-lg m-b-10" id="TechnicalApproveCommentByAudit" cols="20" rows="2"></textarea>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                ErrorMessage="Notes Is Required" ForeColor="Red"
                                                ControlToValidate="TechnicalApproveCommentByAudit" Display="Dynamic" ValidationGroup="TecnecalApproveDoctor">
                                            </asp:RequiredFieldValidator>
                                                <asp:FileUpload ID="UploadTechnicalApproveAuditAttachment" AllowMultiple="true" runat="server" accept=".pdf" />
                                            <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="EndTecnecalApprove" 
                                            ControlToValidate="UploadTechnicalApproveAuditAttachment"  ForeColor="Red" ID="RequiredFieldValidatorAuditEndTecnecalApprove" runat="server"
                                            ErrorMessage="Attachment(s) Is Required"></asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                        ControlToValidate="UploadTechnicalApproveAuditAttachment"
                        ErrorMessage="Only .pdf Files are allowed" Font-Bold="True" ValidationGroup="EndTecnecalApprove"
                        Font-Size="Medium" ForeColor="Red"
                        ValidationExpression="(.*?)\.(pdf)$"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>


                                    <div class="form-group" id="Div1" runat="server">

                                        <label class="col-sm-3 col-sm-3 control-label">Audit Attachments</label>
                                        <div class="form-group col-sm-6>" id="AuditAttachmentTable" runat="server">
                                            <asp:ListView runat="server" ID="AuditAttachment">
                                                <ItemTemplate>

                                                    <asp:LinkButton ID="LinkButton2" OnClick="LinkButton2_Click" Path='<%#Eval("Path")%>' FileName='<%#Eval("Name") %>' runat="server"> <%#Eval("Name") %></asp:LinkButton>

                                                    </tr>
                                                </ItemTemplate>

                                            </asp:ListView>
                                        
                                        </div>
                                    </div>
                                    <div class="form-group" id="Div7" runat="server">
                                        <label class="col-sm-4 col-sm-4 control-label">Audit Technical Approve</label>
                                              <asp:ListView runat="server" ID="AuditTechnicalApprove">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton2" OnClick="LinkButton2_Click" Path='<%#Eval("Path")%>' FileName='<%#Eval("Name") %>' runat="server"> <%#Eval("Name") %></asp:LinkButton>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                    </div>

                                </div>





                            </div>
                                    <br />

                            <div class="container" id="technicalapprovediv">
                                <div class="row">
                                    <div class="col-md-12">
                                       <%-- <div class="col-sm-3 " style="margin-left: 30%">
                                            <textarea runat="server" class="form-control input-lg m-b-10" id="TechnicalApproveComment" cols="20" rows="2" placeholder="Reson For Technical Approve"></textarea>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                ErrorMessage="Notes Is Required" ForeColor="Red"
                                                ControlToValidate="TechnicalApproveComment" Display="Dynamic" ValidationGroup="valid">
                                            </asp:RequiredFieldValidator>
                                        </div>--%>
                                        <div class="col-sm-7" style="margin-left:38%">

                                            <asp:Button ID="Buttonticnecalapproval" CssClass="btn btn-primary"
                                                runat="server" OnClick="TechnicalApprove" Text="Technical Approve" ValidationGroup="TecnecalApproveDoctor" />
                                         <br />
                                            <button type="submit" class="btn btn-primary" runat="server" ValidationGroup="EndTecnecalApprove" onserverclick="EndTechnicalApprove" id="EndTechnicalApproveBtn">End Technical Approve</button>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <br />
                        <div>
                                <div style="margin-top: 5px; margin-bottom: 5px; height: 2px; width: 100%; border-top: 1px solid gray;"></div>
                         <p style="font-size:larger;">  Technical Approvel To Department</p> 
                            <textarea runat="server" name="txtImagename1" 
                                                 class="form-control input-lg m-b-10" id="TechnicalApprovelToDepartmentComment"
                                                 cols="20" rows="2"></textarea>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                                ErrorMessage="Notes Is Required" ForeColor="Red"
                                                ControlToValidate="TechnicalApprovelToDepartmentComment" Display="Dynamic" ValidationGroup="TechnicalApprovelToDepartment">
                                            </asp:RequiredFieldValidator>
                            <br />

                               <asp:ListBox ID="DepartmentList" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                <asp:Button ID="TechnicalApproveFromDepartmentsBtn" CssClass="btn btn-danger" ValidationGroup="TechnicalApprovelToDepartment"
                                                runat="server" OnClick="TechnicalApproveByDepartment" Text="Technical Approve By Departments" />
                                                
                                      <button type="button" class="btn btn-info btn-sm"
                                    data-toggle="modal" data-target="#trackTechnicalApprovel">
                                   Track Of Technical Approvel</button>



                            <div>   
                                <div class="form-group" id="Div8" runat="server">
                                        <label class="col-sm-3 col-sm-3 control-label">Provider Attachments</label>
                                        <div class="form-group col-sm-6>" id="Div9" runat="server">
                                            <asp:ListView runat="server" ID="ProviderAttachments">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton2" OnClick="LinkButton2_Click" Path='<%#Eval("Path")%>' FileName='<%#Eval("Name") %>' runat="server"> <%#Eval("Name") %></asp:LinkButton>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                                              
                                <div class="form-group" id="Div10" runat="server">
                                        <label class="col-sm-3 col-sm-3 control-label">Account Managers Attachments</label>
                                        <div class="form-group col-sm-6>" id="Div12" runat="server">
                                            <asp:ListView runat="server" ID="AccountManagersAttachments">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton2" OnClick="LinkButton2_Click" Path='<%#Eval("Path")%>' FileName='<%#Eval("Name") %>' runat="server"> <%#Eval("Name") %></asp:LinkButton>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                            </div>


                              
                        </div>


                            <div runat="server" id="ApproveOrRejectDiv">
                                <div style="margin-top: 5px; margin-bottom: 5px; height: 2px; width: 100%; border-top: 1px solid gray;"></div>
                                <header class="panel-heading" style="margin-bottom: 15px">
                                    <h4>Approve / Reject Request</h4>
                                </header>
                                <div class="col-md-12">
                                    <div class="form-group" id="Div11" runat="server">
                                        <label class=" col-sm-3 control-label">Approval Or Rejection Notes</label>
                                        <div class="col-sm-9">
                                            <FTB:FreeTextBox ID="ApproveorrejectNotes" runat="server" Height="280px" Width="100%" BackColor="237, 237, 237" ButtonSet="OfficeMac" EditorBorderColorDark="237, 237, 237" EditorBorderColorLight="237, 237, 237" GutterBackColor="237, 237, 237" GutterBorderColorDark="237, 237, 237" GutterBorderColorLight="237, 237, 237">
                                            </FTB:FreeTextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
    <div class="col-xs-12">
      <div class="box">
        <!-- /.box-header -->
        <div class="box-body">
          
       <%--     <div class="form-group row">
              <label for="" class="col-sm-3 form-control-label">Search in Address Book</label>
              <div class="col-sm-7">
                <select class="form-control selectpicker" id="select-country" data-live-search="true">
                <option data-tokens="china">China</option>
  <option data-tokens="malayasia">Malayasia</option>
  <option data-tokens="singapore">Singapore</option>
                </select>

              </div>
            </div>--%>
      
        </div>
        <!-- /.box-body -->
      </div>
                                <div class="row" id="ViewEmailSection" runat="server">
                                    <div class="col-md-8" style="border-right:solid; border-right-color:gainsboro ">
                                        <div class="row">
                                            <label class="col-sm-8 col-sm-8 control-label">Add/Update Email Receipts</label>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-4">
                                                <asp:ListView ID="AddToList" runat="server" ShowFooter="true" 
                                                    AutoGenerateColumns="false" OnSelectedIndexChanged="receiveEmail_SelectedIndexChanged" OnSelectedIndexChanging="receiveEmail_SelectedIndexChanging">
                                                    <ItemTemplate>
                                                        <div  style="margin-bottom:5px">
                                                            <asp:Button ID="RowNumber" CommandName="select" order='<%#Eval("ID") %>' Text='<%#Eval("ID") %>' runat="server" CssClass="btn btn-danger" />
                                                            <asp:TextBox  ValidationGroup="SubmitValidation" ID="TextBox1" Text='<%#Eval("Email") %>' runat="server"></asp:TextBox>
                                                            <%--                        <asp:TextBox ID="TextBox2" Text='<%#Eval("Column2") %>' runat="server"></asp:TextBox>--%>
                                         <asp:RequiredFieldValidator validationgroup="SubmitToValidation"
                                          ID="RequiredFieldValidator2" runat="server"
                                        ErrorMessage="Required" ForeColor="Red"
                                        ControlToValidate="TextBox1" Display="Dynamic"> 
                                    </asp:RequiredFieldValidator>
                                                 <asp:RegularExpressionValidator  validationgroup="SubmitToValidation"
                                                     ID="RegularExpressionValidator4" runat="server" ForeColor="Red"
                                                     ControlToValidate="TextBox1" ErrorMessage="enter Valid Mail" 
                                                     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>       
                        
                                                        
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                                <br />
                                                <asp:Button ID="AddTo" runat="server" Text="Add To Receipt" OnClick="ADDreceiveofEmail" CssClass="btn btn-default" />
                                            </div>
                                            <div class="col-md-4">
                                                <asp:ListView ID="AddCcList" runat="server" ShowFooter="true" AutoGenerateColumns="false" OnSelectedIndexChanging="InCC_SelectedIndexChanging">
                                                    <ItemTemplate>
                                                        <div  style="margin-bottom:5px">
                                                            <asp:Button ID="RowNumber" CommandName="select" order='<%#Eval("ID") %>' Text='<%#Eval("ID") %>' runat="server" CssClass="btn btn-danger" />
                                                            <asp:TextBox ID="TextBox1" Text='<%#Eval("Email") %>' runat="server"></asp:TextBox>
                                                            <%--                        <asp:TextBox ID="TextBox2" Text='<%#Eval("Column2") %>' runat="server"></asp:TextBox>--%>
                                                          <asp:RegularExpressionValidator validationgroup="SubmitToValidation"
                                                     ID="RegularExpressionValidator4" runat="server" ForeColor="Red"
                                                     ControlToValidate="TextBox1" ErrorMessage="enter Valid Mail" 
                                                     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator> 
                                                        
                                                        
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                                <br />

                                                <asp:Button ID="AddCc" runat="server" Text="Add Cc Receipt" OnClick="ADDInCCofEmailclick" CssClass="btn btn-default" />
                                            </div>
                                            <div class="col-md-4" >

                                                <asp:ListView ID="AddBccList" runat="server" ShowFooter="true" AutoGenerateColumns="false" OnPagePropertiesChanging="InBCC_PagePropertiesChanging" OnSelectedIndexChanging="InBCC_SelectedIndexChanging">
                                                    <ItemTemplate>
                                                        <div style="margin-bottom:5px">
                                                            <asp:Button ID="RowNumber" CommandName="select" order='<%#Eval("ID") %>' Text='<%#Eval("ID") %>' runat="server" CssClass="btn btn-danger" />
                                                            <asp:TextBox ID="TextBox1" Text='<%#Eval("Email") %>' runat="server"></asp:TextBox>
                                                            <%--                        <asp:TextBox ID="TextBox2" Text='<%#Eval("Column2") %>' runat="server"></asp:TextBox>--%>
                                                         <asp:RegularExpressionValidator  validationgroup="SubmitToValidation"
                                                     ID="RegularExpressionValidator4" runat="server" ForeColor="Red"
                                                     ControlToValidate="TextBox1" ErrorMessage="enter Valid Mail" 
                                                     ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator> 
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:ListView>
                                                <br />
                                                <asp:Button ID="AddBcc" runat="server" Text="Add Bcc Receipt" OnClick="ADDInBCCofEmailclick" CssClass="btn btn-default" />
                                            </div>
                                        </div>

                                    </div>
                           
                                                                    <div class="col-md-3" style="margin-left:10px" >
                                        <div class="row">
                                            <label class="col-sm-5 col-sm-5 control-label">Upload File(s) </label>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="form-group" id="ManagerApprovalTimeDiv" runat="server">
                                                <div class="col-sm-2">
                                                    <asp:FileUpload ID="FileUpload2" AllowMultiple="true" runat="server" />
                      <asp:RequiredFieldValidator Display="Dynamic" ValidationGroup="SubmitToValidation" ControlToValidate="FileUpload2"  ForeColor="Red" ID="RequiredFieldValidator5" runat="server" ErrorMessage="Attachment(s) Is Required"></asp:RequiredFieldValidator>


                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorFileUpload2" runat="server"
                        ControlToValidate="FileUpload2"
                        ErrorMessage="Only .pdf Files are allowed" Font-Bold="True" ValidationGroup="EndTecnecalApprove"
                        Font-Size="Medium" ForeColor="Red"
                        ValidationExpression=".*"></asp:RegularExpressionValidator>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                             <div class="col-md-12" style="text-align: center;">
                <div class="toolbar-btn-action" style="margin-top:50px">

                    <button type="submit" class="btn btn-success" runat="server" validationgroup="SubmitToValidation" onserverclick="Approve" id="ApproveBtn">Approve</button>
                    <button type="submit" class="btn btn-danger" runat="server" validationgroup="SubmitToValidation" onserverclick="Reject" id="RejectBtn">Reject</button>
                    <button type="submit" class="btn btn-primary" runat="server" validationgroup="Assign" onserverclick="Assign" id="AssignBtn">Assign</button>
                    <button type="submit" class="btn btn-warning" runat="server" onserverclick="Close" id="CloseBtn">Close</button>
                    <button id="IgnoreBtn" runat="server" onserverclick="IgnoreBtn_ServerClick" class="btn btn-warning" type="submit">Ignore</button>

                    <br />
                     <asp:TextBox class="form-control input-lg m-b-10" ID="ReopenComment" placeholder="Type Reopen Reason" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ForeColor="Red" runat="server" ID="ReopenValidation" ErrorMessage="Enter Reason For Reopen"  ControlToValidate="ReopenComment"
                            ValidationGroup="ReopenGroup"></asp:RequiredFieldValidator>

                        <br />
                         <asp:Button ID="ReopenBtn" CssClass="btn btn-default" runat="server" OnClick="ReopenTicket" ForeColor="Red" ValidationGroup="ReopenGroup" Text="Reopen" />                      
                    
                    <br />                                                          
                </div>
            </div>
                    </div>
            </div>
                        </div>
                             <div id="trackTechnicalApprovel" class="modal fade" tabindex="-1">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-info">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h6 class="modal-title">Technical Approval Track</h6>
                </div>
                <div class="modal-body">

                    <div>
                          
                        <table class="table table-bordered" id="DrugTable">
                            <thead>
                                <tr>
                                    <th>Request ID</th>
                                    <th>Department Note</th>
                                    <th>Start Technical Approve Time</th>
                                    <th>End Technical Approve Time</th>
                                    <th>Assigned Person</th>
                                    <th>Department Name</th>

                                    <th>Done</th>

                                </tr>
                            </thead>
                            <tbody id="TableBody1">

                                <asp:ListView ID="TrackOfTechnicalApproval" runat="server">

                                    <ItemTemplate>

                                        <tr>
                                       <td><%#Eval("RequestID")%></td>
                                       <td><%#Eval("Note")%></td>
                                       <td><%#Eval("StartTechnicalApprovalTime")%></td>
                                       <td><%#Eval("EndTechnicalApprovalTime")%></td>
                                       <td><%#Eval("Assignee")%></td>
                                       <td><%#Eval("DepartmentName")%></td>

<td> <asp:CheckBox ID="CheckBox1" Checked='<%#Eval("IsDone")%>' Enabled="false"  runat="server" /></td>

                                            

                                        </tr>

                                    </ItemTemplate>

                                </asp:ListView>

                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>

    </div>
<%--            </form>--%>
        </div>
    </section>
    </div>
        </div>
    </section>

   




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">

    
<script src="../assets/js/bootstrap-select.min.js"></script>


    <script>
        $("#submit").click(function () {
            $("#UserMedicalID").attr('disabled', false);
        });
    </script> 
       <script type="text/javascript">
        $(function () {
            $('[id*=DepartmentList]').multiselect({
                includeSelectAllOption: true
            });
         
        });
    </script>
  <style>
      .modal-dialog{
  width:1000px;
}
  </style>
</asp:Content>
