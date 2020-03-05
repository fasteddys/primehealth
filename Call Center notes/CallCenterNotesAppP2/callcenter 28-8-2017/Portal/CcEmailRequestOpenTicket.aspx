<%@ Page ValidateRequest="false" Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="CcEmailRequestOpenTicket.aspx.cs" Inherits="CallCenterNotesApp.Portal.OpenCcEmailRequest" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Add New Email Request</a></li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <!-- Main content -->
    <!--row1-->
    <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
        Email Request Is Not Added , Please Verify Inputs
    </div>
    <div class="alert alert-success alert-dismissable" runat="server" id="div_Success">
        Your Email Request Submitted Successfully
    </div>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <section class="panel">
                    <header class="panel-heading">Request Details </header>
                    <div class="panel-body">
<%--                        <form class="form-horizontal tasi-form" role="form" runat="server">--%>

                            <div style="padding-left: 270px; padding-bottom: 20px">
                            </div>


                            <div class="form-group" style="font-weight: bold; font-size: large; color: darkcyan">
                                <label class="col-sm-2 col-sm-2 control-label">Request Type<label style="color: red">*</label></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" ID="Dropdownlist1" Width="248px" Height="45px">
                                        <asp:ListItem Text="Select Request Type " Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="General" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Special" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ValidationGroup="SubmitValidation" ErrorMessage="Request Type Is Required" ForeColor="Red" InitialValue="-1"
                                        ControlToValidate="Dropdownlist1" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <label class="col-sm-2 col-sm-2 control-label">Subject<label style="color: red">*</label></label>
                                <div class="col-sm-3">
                                    <input type="text" class="form-control input-lg m-b-10" id="MailSubject" runat="server">
                                    <asp:RequiredFieldValidator ValidationGroup="SubmitValidation" ID="SubjectValidator" runat="server" ErrorMessage="Subject Is Required"
                                        ControlToValidate="MailSubject" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                               

                            </div>

                            <div class="form-group" style="font-weight: bold; font-size: large; color: darkcyan">
                                 <label class="col-sm-2 col-sm-2 control-label">Medical ID<label style="color: red">*</label></label>
                                <div class="col-sm-3">
                                    <input type="text" class="form-control input-lg m-b-10" id="MedicalID" runat="server">
                                    <asp:RequiredFieldValidator ValidationGroup="SubmitValidation" ID="MedicalIDValidator" runat="server" ErrorMessage="Medical ID Is Required" ControlToValidate="MedicalID" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="MedicalID" ErrorMessage="Please Enter Only Numbers" ForeColor="Red" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                </div>
                                 <label class="col-sm-2 col-sm-2 control-label">Approval Category<label style="color: red">*</label></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" ID="Category" Width="248px" Height="45px">
                                        <asp:ListItem Text="Select Category " Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="Inpatient" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Outpatient" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Medication" Value="3"></asp:ListItem>

                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        ValidationGroup="SubmitValidation" ErrorMessage="Request Category Is Required" ForeColor="Red" InitialValue="-1"
                                        ControlToValidate="Category" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </div>
                               
                                

                            </div>

                            <div class="form-group" style="font-weight: bold; font-size: large; color: darkcyan">
                                 <label class="col-sm-2 col-sm-2 control-label">Priority<label style="color: red">*</label></label>
                                <div class="col-sm-3">
                                    <asp:DropDownList runat="server" ID="Priority" Width="248px" Height="45px">
                                        <asp:ListItem Text="Select Priority" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="Normal" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="high" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Emergency" Value="3"></asp:ListItem>

                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                        ValidationGroup="SubmitValidation" ErrorMessage="Request Priority Is Required" ForeColor="Red" InitialValue="-1"
                                        ControlToValidate="Priority" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <label class="col-sm-2 col-sm-2 control-label">Member Name</label>
                                <div class="col-sm-3">
                                    <input type="text" class="form-control input-lg m-b-10" id="MemberName" runat="server">
                                </div>
                                
                            </div>
                              <div class="form-group" style="font-weight: bold; font-size: large; color: darkcyan">
                               
                                   <label class="col-sm-2 col-sm-2 control-label">Provider Name</label>
                                <div class="col-sm-3">
                                    <input type="text" class="form-control input-lg m-b-10" id="ProviderName" runat="server">

                                </div>
                                  <label class="col-sm-2 col-sm-2 control-label">Client Name</label>
                                <div class="col-sm-3">
                                    <input type="text" class="form-control input-lg m-b-10" id="ClientName" runat="server">
                                </div>
                               

                            </div>
                            <div class="form-group" style="font-weight: bold; font-size: large; color: darkcyan">
                                <label class="col-sm-2 col-sm-2 control-label">Body<label style="color: red">*</label></label>
                                <div class="col-sm-8">
                                    <FTB:FreeTextBox ID="CallcenterOpenNote" EnableHtmlMode="false" runat="server" Height="220px" Width="100%" BackColor="237, 237, 237" ButtonSet="OfficeMac" EditorBorderColorDark="238, 238, 238" EditorBorderColorLight="238, 238, 238" GutterBackColor="238, 238, 238" GutterBorderColorDark="238, 238, 238" GutterBorderColorLight="238, 238, 238">
                                    </FTB:FreeTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ErrorMessage="Notes Is Required" ForeColor="Red" InitialValue="-1"
                                        ControlToValidate="CallcenterOpenNote" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </div>

                            </div>
                            <div class="row">
                                <div class="row">
                                    <h3 class="col-sm-7 col-sm-7 control-label">Add Email Receipts</h3>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:ListView ID="AddToList" runat="server" ShowFooter="true" AutoGenerateColumns="false" OnSelectedIndexChanged="receiveEmail_SelectedIndexChanged" OnSelectedIndexChanging="receiveEmail_SelectedIndexChanging">
                                            <ItemTemplate>
                                                <div style="margin-bottom: 5px">
                                                    <asp:Button ID="RowNumber" CommandName="select" order='<%#Eval("ID") %>' Text='<%#Eval("ID") %>' runat="server" CssClass="btn btn-danger" />
                                                    <asp:TextBox ValidationGroup="SubmitValidation" ID="TextBox1" Text='<%#Eval("Email") %>' runat="server"></asp:TextBox>
                                                    <%--                        <asp:TextBox ID="TextBox2" Text='<%#Eval("Column2") %>' runat="server"></asp:TextBox>--%>
                                                    <asp:RequiredFieldValidator ValidationGroup="SubmitValidation"
                                                        ID="RequiredFieldValidator2" runat="server"
                                                        ErrorMessage="Required" ForeColor="Red"
                                                        ControlToValidate="TextBox1" Display="Dynamic"> 
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ValidationGroup="SubmitValidation"
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
                                                <div style="margin-bottom: 5px">
                                                    <asp:Button ID="RowNumber" CommandName="select" order='<%#Eval("ID") %>' Text='<%#Eval("ID") %>' runat="server" CssClass="btn btn-danger" />
                                                    <asp:TextBox ID="TextBox1" Text='<%#Eval("Email") %>' runat="server"></asp:TextBox>
                                                    <%--                        <asp:TextBox ID="TextBox2" Text='<%#Eval("Column2") %>' runat="server"></asp:TextBox>--%>
                                                    <asp:RegularExpressionValidator ValidationGroup="SubmitValidation"
                                                        ID="RegularExpressionValidator4" runat="server" ForeColor="Red"
                                                        ControlToValidate="TextBox1" ErrorMessage="enter Valid Mail"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>


                                                </div>
                                            </ItemTemplate>
                                        </asp:ListView>
                                        <br />

                                        <asp:Button ID="AddCc" runat="server" Text="Add Cc Receipt" OnClick="ADDInCCofEmailclick" CssClass="btn btn-default" />
                                    </div>
                                    <div class="col-md-4">

                                        <asp:ListView ID="AddBccList" runat="server" ShowFooter="true" AutoGenerateColumns="false" OnPagePropertiesChanging="InBCC_PagePropertiesChanging" OnSelectedIndexChanging="InBCC_SelectedIndexChanging">
                                            <ItemTemplate>
                                                <div style="margin-bottom: 5px">
                                                    <asp:Button ID="RowNumber" CommandName="select" order='<%#Eval("ID") %>' Text='<%#Eval("ID") %>' runat="server" CssClass="btn btn-danger" />
                                                    <asp:TextBox ID="TextBox1" Text='<%#Eval("Email") %>' runat="server"></asp:TextBox>
                                                    <%--                        <asp:TextBox ID="TextBox2" Text='<%#Eval("Column2") %>' runat="server"></asp:TextBox>--%>
                                                    <asp:RegularExpressionValidator
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
                                <div style="margin-top: 25px; margin-bottom: 25px; margin-left: 15%; height: 2px; width: 70%; border-top: 1px solid gray;"></div>
                                <div>
                                    <div class="form-group" style="font-weight: bold; font-size: large; color: darkcyan">



                                        <label class="col-sm-2 col-sm-2 control-label">Attachments<label style="color: red">*</label></label>
                                        <div class="col-sm-3">
                                            <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" class="btn btn-default" />
                                            <asp:RequiredFieldValidator ValidationGroup="SubmitValidation" runat="server" ID="validateFile" ControlToValidate="FileUpload1" ErrorMessage="Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <br />
                            <div class="col-md-12" style="text-align: center;">
                                <div class="toolbar-btn-action">
                                    <button type="submit" validationgroup="SubmitValidation" class="btn btn-primary" id="btn_Submit" onserverclick="ApvSubmitNewCallcenterEmailApproval" runat="server">Submit Request</button>
                                </div>
                            </div>
                    </div>
<%--                </form>--%>
            </div>
    </section>
    </div>
        </div>
    </section>
    <!-- /.content -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
</asp:Content>
