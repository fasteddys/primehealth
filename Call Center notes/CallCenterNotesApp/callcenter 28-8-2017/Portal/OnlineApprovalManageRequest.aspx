<%@ Page ValidateRequest="false" Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="OnlineApprovalManageRequest.aspx.cs" Inherits="CallCenterNotesApp.Portal.OnlineApprovalManageRequest" %>



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
                <div class="panel" style="padding: 25px">
<%--                    <form class="form-horizontal tasi-form" role="form" runat="server" enctype="multipart/form-data">--%>
                        <header class="panel-heading">
                            <div class="row">
                                <div class="col-sm-3">
                                    <h4>Request Data</h4>
                                </div>

                            </div>
                        </header>
                        <div class="row">
                            <div class="col-sm-6" align="Center" style="margin-top: 10px; margin-bottom: 10px">
                                <label id="TicketID" runat="server" class="pull-right" style="font-size: 38px; padding-bottom: 15px; color: goldenrod">3290</label>
                            </div>
                         
                        </div>


                        <br />
                        <br />
                        <div class="panel-body">

                            <div style="top: 30px">
                            </div>


                            <div class="form-group" runat="server">
                                <label class="col-sm-2 col-sm-2 control-label">User Medical ID  </label>
                                <div class="col-sm-4">
                                    <input type="text" class="form-control input-lg m-b-10" id="UserMedicalID" runat="server" readonly>
                                </div>

                                <%--Update----------------------------------------------------------------------------%>
                                <div class="form-group" id="PatientNameDiv" runat="server">
                                    <br />
                                    <label class="col-sm-2 col-sm-2 control-label">Member Name  </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="Member" runat="server" disabled>
                                    </div>
                                    <label class="col-sm-2 col-sm-2 control-label">Request Creation Time </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="RequestCreationTimeID" runat="server" disabled>
                                    </div>
                                </div>

                                <div class="form-group" id="CompanyNameDiv" runat="server">
                                    <label class="col-sm-2 col-sm-2 control-label">Provider Name   </label>


                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="ProviderName" runat="server" disabled>
                                    </div>
                                    <label class="col-sm-2 col-sm-2 control-label">Company Name </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="CompanyNameID" runat="server" disabled>
                                    </div>
                                </div>

                                <div class="form-group" id="Div5" runat="server">
                                    <label class="col-sm-2 col-sm-2 control-label">IVR Number  </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="IVRNumber" runat="server" disabled>
                                    </div>
                                    <label class="col-sm-2 col-sm-2 control-label">Approval Category</label>
                                    <div class="col-sm-4">


                                        <input type="text" class="form-control input-lg m-b-10" id="Text1" runat="server" disabled>
                                    </div>
                                </div>


                                <div class="form-group" id="RequestCreationTimeDiv" runat="server">
                                    <label class="col-sm-2 col-sm-2 control-label">Request Status </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="RequestStatusID" runat="server" disabled>
                                    </div>

                                    <label class="col-sm-2 col-sm-2 control-label">Doctor Name </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="DoctorAssignee" runat="server" disabled>
                                    </div>

                                </div>
                                <div class="form-group" id="Div6" runat="server">

                                    <label class="col-sm-2 col-sm-2 control-label">MobileNumber </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="MobileNumber" runat="server" disabled>
                                    </div>
                                    <label class="col-sm-2 col-sm-2 control-label">Close  Time </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="CloseTime" runat="server" disabled>
                                    </div>
                                </div>
                                   <div class="form-group" id="Div1" runat="server">

                                    <label class="col-sm-2 col-sm-2 control-label">Note </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="Note" runat="server" disabled>
                                    </div>
                                    <label class="col-sm-2 col-sm-2 control-label">Diagnose </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="Diagnose" runat="server" disabled>
                                    </div>
                                </div> 
                                
                          <div class="form-group" id="Div3" runat="server">      
                                    <label class="col-sm-2 col-sm-2 control-label">CoInsurance Percentage </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="CoInsurancePercentage" runat="server" disabled>
                                    </div>

                                              </div>


                                        <div class="form-group" id="Div2" runat="server">
                                              <label class="col-sm-2 col-sm-2 control-label"> </label>
                                           
                                    <div class="col-sm-4">
<%--                                        <input type="text" class="form-control input-lg m-b-10" id="Text2" runat="server" disabled>--%>
                                        <label class="container">Medical 
                                      <input type="checkbox" checked="checked" disabled runat="server" id="MedicalApprovalCheck">
                                      <span class="checkmark"></span>
                                      </label>                                                                         
                                       <label class="container">Financial 
                                      <input type="checkbox" checked="checked" disabled runat="server" id="FinancialApprovalCheck">
                                      <span class="checkmark"></span>
                                      </label>

                                    </div>
                                 
                                </div>




                                <br />

                                <label>Request Attachments</label>
                                <div class="form-group" id="TicketAttachmentTable" runat="server">
                                    <asp:ListView runat="server" ID="TicketAttachment">
                                        <ItemTemplate>
                                            &nbsp &nbsp

                                <asp:LinkButton ID="LinkButton2" OnClick="LinkButton2_Click" Path='<%#Eval("AttachmentPath")%>' FileName='<%#Eval("AttachmentName") %>' runat="server"> 
    <%#Eval("AttachmentName") %></asp:LinkButton>
                                            </nsfare>
                                        </ItemTemplate>

                                    </asp:ListView>
                                </div>

                                
                            </div>
                          
                        </div>

                                  <div class="form-group" id="Div4" runat="server">   
                                      <button type="button" class="btn btn-info btn-sm" data-toggle="modal" data-target="#Invoice">
                                    Drug List</button>
                                    </div>
                        <div style="margin-top: 5px; margin-bottom: 5px; height: 2px; width: 100%; border-top: 1px solid gray;"></div>

                        <div style="bottom: 40px; padding-top: 10px">
                        </div>
                </div>
                <br />
            </div>
            <div class="panel">
            <header class="panel-heading">
                <div class="row">
                    <div class="col-sm-3">
                        <h4>Request Details</h4>
                    </div>

                </div>
            </header>

            <div class="row " style="margin:20px">
                <asp:ListView ID="RequestDetails" runat="server">
                    <ItemTemplate>
                        <div class="panel-group" style="margin-bottom:15px">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" href="#<%#Eval("RequestDetailsID")%>"> 
                                           <span class='<%#Eval("RequestDetailsTypeName")%>'></span> <span><b><%#Eval("UserName")%></b></span> <span> <%#Eval("DetailsDate")%>  </span>
                                        <i class="fa fa-plus position-right pull-right text-slate"></i></a>
                                    </h4>
                                </div>
                                <div id="<%#Eval("RequestDetailsID")%>" class="panel-collapse collapse">
                                    <div class="panel-body">
                                       
                                <div runat="server"  class="appandnote"><%#Eval("Notes")%></div>
                                          <asp:HiddenField runat="server" ID="hiddin" Value='<%#Eval("RequestDetailsID")%>' />
                                    </div>
                                  <div id="Note" runat="server"></div>

                                </div>
                            </div>
                        </div>
                       
                    </ItemTemplate>

                </asp:ListView>

            </div>
            <div class="col-sm-9">
                <FTB:FreeTextBox ID="ApproveorrejectNotes" runat="server" Height="280px" Width="100%" BackColor="237, 237, 237" ButtonSet="OfficeMac" EditorBorderColorDark="237, 237, 237" EditorBorderColorLight="237, 237, 237" GutterBackColor="237, 237, 237" GutterBorderColorDark="237, 237, 237" GutterBorderColorLight="237, 237, 237">
                </FTB:FreeTextBox>
            </div>
            <asp:FileUpload ID="FileUpload1" runat="server" />
                  <br />
                               <div class="col-sm-2">
               <span>IVR Number</span>  <asp:CheckBox ID="IVRCheckBox" runat="server"  AutoPostBack="true" OnCheckedChanged="IVRCheckBox_CheckedChanged" />
                <asp:TextBox ID="IVRtxt" Visible="false" runat="server"></asp:TextBox>
                    </div>
               <br />
                <asp:RegularExpressionValidator ID="IVRValidation" ForeColor="Red"
    ControlToValidate="IVRtxt" runat="server"
    ErrorMessage="Only Numbers allowed"
    ValidationExpression="\d+">
</asp:RegularExpressionValidator>
                <div class="col-md-12" style="text-align: center;">
                <div class="toolbar-btn-action" style="margin-top: 50px">

                    <button type="submit" class="btn btn-success" runat="server" validationgroup="SubmitToValidation" onserverclick="Approve" id="ApproveBtn">Approve</button>
                    <button type="submit" class="btn btn-danger" runat="server" validationgroup="SubmitToValidation" onserverclick="Reject" id="RejectBtn">Reject</button>
                    <button type="submit" class="btn btn-primary" runat="server" validationgroup="Assign" onserverclick="Assign" id="AssignBtn">Assign</button>
                    <button type="submit" class="btn btn-info" runat="server" onserverclick="Reply" id="Replybtn">Reply</button>
                    <button type="submit" class="btn btn-default" runat="server" onserverclick="EndOnhod" id="EndOnhodBtn">EndOnhold</button>

                    <br />
                    <asp:TextBox class="form-control input-lg m-b-10" ID="ReasonComment" placeholder="Type Reason" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ForeColor="Red" runat="server" ID="TerminatValidation" ErrorMessage="Enter Reason For Termenation"
                        ControlToValidate="ReasonComment" ValidationGroup="TermenateGroup"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ForeColor="Red" runat="server" ID="OnholdValidator" ErrorMessage="Enter Reason For Onhold"
                        ControlToValidate="ReasonComment" ValidationGroup="OnholdGroup"></asp:RequiredFieldValidator>

                    <br />
                    <button type="submit" class="btn btn-warning" runat="server" onserverclick="Termenate" id="TermenateBtn" validationgroup="TermenateGroup">Termenate</button>
                    <button type="submit" class="btn btn-default" runat="server" onserverclick="Onhold" validationgroup="OnholdGroup" id="OnholdBtn">Onhold</button>

                    <br />
                </div>

            </div>
                </div>
               <div id="Invoice" class="modal fade" tabindex="-1">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-info">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h6 class="modal-title">Demanded Drugs List</h6>
                </div>
                <div class="modal-body">

                    <div>
                        <table class="table table-bordered" id="DrugTable">
                            <thead>
                                <tr>
                                    <th>Drug Name</th>
                                    <th>UnitQuantity</th>
                                    <th>UnitPrice</th>
                                    <th>DemandedQuantity</th>
                                    <th>TotalPrice</th>
                                    <th>Listed/Not</th>
                                </tr>
                            </thead>
                            <tbody id="TableBody1">

                                <asp:ListView ID="DrugList" runat="server">

                                    <ItemTemplate>

                                        <tr>
                                            <td><%#Eval("DrugName")%></td>
                                            <td><%#Eval("UnitQuantity")%></td>
                                            <td><%#Eval("UnitPrice")%></td>
                                            <td><%#Eval("DemandedQuantity")%></td>
                                            <td><%#Eval("TotalPrice")%></td>
                                            <td> <asp:CheckBox ID="CheckBox1" Checked='<%#Eval("IsDrugList")%>' Enabled="false"  runat="server" /></td>

                                        </tr>

                                    </ItemTemplate>

                                </asp:ListView>

                            </tbody>
                        </table>
                    </div>
                    <div><label class="col-sm-5 col-sm-5 control-label">Total Approval Price </label>
                        <div><input type="text" class=" input-lg m-b-5" id="TotalApprovalPrice" runat="server" disabled></div>
                        <label class="col-sm-5 col-sm-5 control-label">Total Member pays </label>
                        <div><input type="text" class="input-lg m-b-5" id="TotalMemberpays" runat="server" disabled></div>
                       <label class="col-sm-5 col-sm-5 control-label">Total Price </label>
                        <div><input type="text" class="input-lg m-b-5" id="TotalPayments" runat="server" disabled></div>


                        </div>



                </div>
                <div class="modal-footer">
                    <button  type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>

    </div>


<%--            </form>--%>
        </div>


        </div>

    </section>
<style>
/* The container */
.container {
    display: block;
    position: relative;
    padding-left: 35px;
    margin-bottom: 12px;
    cursor: pointer;
    font-size: 22px;
    -webkit-user-select: none;
    -moz-user-select: none;
    -ms-user-select: none;
    user-select: none;
}

/* Hide the browser's default checkbox */
.container input {
    position: absolute;
    opacity: 0;
    cursor: pointer;
    height: 0;
    width: 0;
}

/* Create a custom checkbox */
.checkmark {
    position: absolute;
    top: 0;
    left: 0;
    height: 25px;
    width: 25px;
    background-color: #eee;
}

/* On mouse-over, add a grey background color */
.container:hover input ~ .checkmark {
    background-color: #ccc;
}

/* When the checkbox is checked, add a blue background */
.container input:checked ~ .checkmark {
    background-color: #2196F3;
}

/* Create the checkmark/indicator (hidden when not checked) */
.checkmark:after {
    content: "";
    position: absolute;
    display: none;
}

/* Show the checkmark when checked */
.container input:checked ~ .checkmark:after {
    display: block;
}

/* Style the checkmark/indicator */
.container .checkmark:after {
    left: 9px;
    top: 5px;
    width: 5px;
    height: 10px;
    border: solid white;
    border-width: 0 3px 3px 0;
    -webkit-transform: rotate(45deg);
    -ms-transform: rotate(45deg);
    transform: rotate(45deg);
}
</style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">

 


    <script src="../assets/js/bootstrap-select.min.js"></script>



    <script>

        $(document).ready(function () {
            var listOfAssingDetails = document.getElementsByClassName("Assign");
            for(var i=0 ; i<listOfAssingDetails.length;i++)
            {
                var item = listOfAssingDetails[i];
                item.className = item.className + ' fa fa-user'; //"icon-user-check";
                item.innerHTML = "<span>Request Assigned by</span>";

            }
            var listOfReplyByCallCenterDetails = document.getElementsByClassName("ReplyFromCallCenter");
            for (var i = 0 ; i < listOfReplyByCallCenterDetails.length; i++) {
                var item = listOfReplyByCallCenterDetails[i];
                item.className = item.className + ' fa fa-envelope'; //"icon-user-check";
                item.innerHTML = " <span>Reply by</span>";

            }
            var listOfReplyDetails = document.getElementsByClassName("ReplyFromProvider");
            for (var i = 0 ; i < listOfReplyDetails.length; i++) {
                var item = listOfReplyDetails[i];
                item.className = item.className + ' fa fa-envelope'; //"icon-user-check";
                item.innerHTML = "<span> Reply by</span>";

            }
            var listOfApprovesDetails = document.getElementsByClassName("Approve");
            for (var i = 0 ; i < listOfApprovesDetails.length; i++) {
                var item = listOfApprovesDetails[i];
                item.className = item.className + ' fa far fa-check-circle'; //"icon-user-check";
                item.innerHTML = "<span>Request Approved by</span>";

            }

            var listOfReopenDetails = document.getElementsByClassName(" Reopen");
            for (var i = 0 ; i < listOfReopenDetails.length; i++) {
                var item = listOfReopenDetails[i];
                item.className = item.className + ' fa fas fa-exclamation-triangle' //"icon-user-check";
                item.innerHTML = "<span>Request Reopened by</span>";

            }
            var listOfTerminateDetails = document.getElementsByClassName("Terminate");
            for (var i = 0 ; i < listOfTerminateDetails.length; i++) {
                var item = listOfTerminateDetails[i];
                item.className = item.className + ' fa fa-lock'; //"icon-user-check";
                item.innerHTML = "<span>Request Terminated by</span>";

            }

            var listOfCloseDetails = document.getElementsByClassName("Reject");
            for (var i = 0 ; i < listOfCloseDetails.length; i++) {
                var item = listOfCloseDetails[i];
                item.className = item.className + ' fa fa-window-close'; //"icon-user-check";
                item.innerHTML = "<span>Request Reject by</span>";

            }
            
            var listOfOnholdDetails = document.getElementsByClassName("Onhold");
            for (var i = 0 ; i < listOfOnholdDetails.length; i++) {
                var item = listOfOnholdDetails[i];
                item.className = item.className + ' fa fa-hourglass-end'; //"icon-user-check";
                item.innerHTML = "<span>Request Onhold by</span>";

            }

            var listOfEndOnholdDetails = document.getElementsByClassName("EndOnhold");
            for (var i = 0 ; i < listOfEndOnholdDetails.length; i++) {
                var item = listOfEndOnholdDetails[i];
                item.className = item.className + ' fa fa-hourglass-end'; //"icon-user-check";
                item.innerHTML = "<span> End Onhold</span>";

            }
            

        })

        $(document).ready(function () {
            appandnotes();
        })
        $("#submit").click(function () {
            $("#UserMedicalID").attr('disabled', false);
        });



        function appandnotes() {
            debugger;
            var numItems = $('.appandnote').length;
            for (var i = 0; i < numItems; i++) {
                var item1 = document.getElementsByClassName("appandnote")[i];
                if (item1 != null) {
                    let item = document.getElementsByClassName("appandnote")[i].textContent;

                    var parent = item1.parentElement;
                    // parent.removeChild(parent.childNodes[0]);
                    // parent.textContent = "";
                    $(parent).append(item);
                    item1.textContent = '';
                    item1.style.display = "none";
                }

            }

        }

    </script>

</asp:Content>
