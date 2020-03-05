<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="ManageRequest.aspx.cs" Inherits="WebApplication1.Portal.ManageRequest" %>
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
                    <li><a href="#" class="active">Manage Request</a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="container-fluid container-fixed-lg">
        <div class="widget form-group-default">
            <div class="widget-content padding">
                <div align="center">
                    <h2>Manage Request</h2>
                </div>
                <hr />


                <div class="alert alert-success alert-dismissable" runat="server" id="div_success">
                    Uploaded sucsess
                </div>
                
                <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
                    Please Insert the Correct Excel and check (FoundClaims,DublicatedClaims , InpatientClaims) 
                </div>
                <div class="alert alert-success alert-dismissable" runat="server" id="div_QualityBackComments2">
                    Please Insert Your Comment
                </div>

                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label"></label>
                        <div class="col-sm-10" align="Right">
                            <a class="btn btn-success" runat="server" onserverclick="btn_asign_ServerClick" id="btn_asign"><i class="fa fa-check-circle-o"></i> Accept</a>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="form-group">
                        <div class="col-sm-12" align="Center">
                            <label runat="server" id="Label1" style="font-size: 38px;"></label>
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Client Type</label>
                        <div class="col-sm-10">
                            <label runat="server" id="ClientType"></label>
                        </div>
                    </div>
                </div>
                <br />

                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Client Name</label>
                        <div class="col-sm-10">
                            <label runat="server" id="lbl_Sub"></label>
                        </div>
                    </div>
                </div>

                <br />
                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Provider Name</label>
                        <div class="col-sm-10">
                            <label runat="server" id="txtrBody" ></label>
                        </div>
                    </div>
                </div>

                <br />
                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">DataEntry Name</label>
                        <div class="col-sm-10">
                            <label runat="server" id="txtrAssPer" ></label>
                        </div>
                    </div>
                </div>
                 <br />
                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Total Claims</label>
                        <div class="col-sm-10">
                            <label runat="server" id="NClaimslb" ></label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row" >
                    <div class="form-group" runat="server" id="TMoneydiv">
                        <label for="input-text" class="col-sm-2 control-label" >Tottal Money</label>
                        <div class="col-sm-10">

                            <asp:TextBox  runat="server" id="Moneycounter" rows="1" style="font-weight: bold; font-size: small" Width="176px"  ></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                   <div class="row" >
                    <div class="form-group" runat="server" id="NClaimdiv">
                        <label for="input-text" class="col-sm-2 control-label" >Number Of Found Claims</label>
                        <div class="col-sm-10">

                            <asp:TextBox runat="server" id="foundcounter" rows="1" style="font-weight: bold; font-size: small" Width="176px" ></asp:TextBox>
                        </div>
                    </div>
                </div> 
                <br />
                <div class="row" >
                    <div class="form-group" runat="server" id="NDubClaimdiv">
                        <label for="input-text" class="col-sm-2 control-label" >Number Of Dublicated Claims</label>
                        <div class="col-sm-10">

                            <asp:TextBox  runat="server" id="Dublicatedcounter" rows="1" style="font-weight: bold; font-size: small" Width="176px"  ></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row" >
                    <div class="form-group" runat="server" id="NInpClaimdiv">
                        <label for="input-text" class="col-sm-2 control-label" >Number Of InPatient Claims</label>
                        <div class="col-sm-10">

                            <asp:TextBox runat="server" id="InPcounter" rows="1" style="font-weight: bold; font-size: small" Width="176px" ></asp:TextBox>
                        </div>
                    </div>
                </div>
              <br />
                <div class="row" >
                    <div class="form-group" runat="server" id="NWrongClaimdiv">
                        <label for="input-text" class="col-sm-2 control-label" >Number Of Wrong Claims</label>
                        <div class="col-sm-10">

                            <asp:TextBox runat="server" id="Wrongcounter" rows="1" style="font-weight: bold; font-size: small" Width="176px" ></asp:TextBox>
                        </div>
                    </div>
                </div>
              <br />
                <div class="row" >
                    <div class="form-group" runat="server" id="NRejClaimdiv">
                        <label for="input-text" class="col-sm-2 control-label" >Number Of Rejected Claims</label>
                        <div class="col-sm-10">

                            <asp:TextBox runat="server" id="Rejectedtecounter" rows="1" style="font-weight: bold; font-size: small" Width="176px" ></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row" >
                    <div class="form-group" runat="server" id="NRecClaimdiv">
                        <label for="input-text" class="col-sm-2 control-label" >Number Of Received Claims</label>
                        <div class="col-sm-10">

                            <asp:TextBox runat="server" id="Receivedcounter" rows="1" style="font-weight: bold; font-size: small" Width="176px" ></asp:TextBox>
                        </div>
                    </div>
                </div>

              <br />
                <div class="row" runat="server" id="EnterComDiv" >
                 <div class="form-group" >
							<label for="input-text" class="col-sm-2 control-label">Enter Your Comments</label>
                     <div class="col-sm-10">
                         <asp:TextBox ID="Enter_Your_Comments" runat="server" Width="100%" TextMode="MultiLine"  Rows="3"></asp:TextBox>
							</div>
						  </div>
                    </div>
              <br />
                <div class="row" runat="server" id="DisplayComDiv">
                 <div class="form-group">
							<label for="input-text" class="col-sm-2 control-label">Display All Comments</label>
                     <div class="col-sm-10">
                         <asp:TextBox ID="Display_All_Comments" runat="server" Width="100%" TextMode="MultiLine"  Rows="6"></asp:TextBox>
							</div>
						  </div>
                    </div>
                <br />
                      <div class="row">
                    <div class="form-group">
                        
                </div>
                <br />

<%----------------------------------------------------------------------%>
                <div class="row">
                        <div class="form-group" runat="server" id="uploaddiv2">
                            <label class="col-sm-2 control-label">Attached Found Excel</label>
                            <div class="col-sm-10">
                                <%-- <input type="file" class="btn btn-default" title="Select file">--%>
                                <asp:FileUpload ID="FileUpload1" runat="server" class="btn btn-default" />
                                <br />
                                <asp:RegularExpressionValidator ID="rexp" runat="server" ControlToValidate="FileUpload1"
                                    ErrorMessage="Only .xlsx files "
                                    ValidationExpression="(.*\.([Xx][Ll][Ss][Xx])$)" ForeColor="Red"></asp:RegularExpressionValidator>
                                <%--<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>--%>
                            </div>
                        </div>
                    </div>
                     <div class="row" >
                        <div class="form-group" runat="server" id="uploaddiv1" >
                            <label class="col-sm-2 control-label">Attached Missing Excel</label>
                            <div class="col-sm-10">
                                <%-- <input type="file" class="btn btn-default" title="Select file">--%>
                                <asp:FileUpload ID="FileUpload2" runat="server" class="btn btn-default" />
                                <br />
                                <asp:RegularExpressionValidator ID="rexp2" runat="server" ControlToValidate="FileUpload2"
                                    ErrorMessage="Only .xlsx files "
                                    ValidationExpression="(.*\.([Xx][Ll][Ss][Xx])$)" ForeColor="Red"></asp:RegularExpressionValidator>
                                <%--<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>--%>
                            </div>
                        </div>
                    </div>

<%----------------------------------------------------------------------%>


                <div class="col-md-12">
                    <div class="toolbar-btn-action" align="center">
                        <a class="btn btn-success" runat="server" onserverclick="btn_issue_ServerClick" id="btn_issue"><i class="fa fa-check"></i> Move_PQC_with_found</a>
                        <a class="btn btn-success" runat="server" onserverclick="btn_issue2_ServerClick" id="btn_issue2"><i class="fa fa-check"></i> Move_PQC_with_found_missing </a>
                         <input type="button" class="btn btn-primary" runat="server" id="btn_download" value="Download Original file" onserverclick="DownloadFile" />   
                         <input type="button" class="btn btn-primary" runat="server" id="btn_download_missing" value="Download Missing file" onserverclick="DownloadMissingFile" />   
                         <input type="button" class="btn btn-primary" runat="server" id="btn_download_found" value="Download Found file" onserverclick="DownloadFoundFile" />   
                    </div>  
                    <br />            
                   <div class="toolbar-btn-action" align="center">
                         <a class="btn btn-warning" runat="server" onserverclick="btn_pending_missing_ServerClick" id="btn_pending_missing"><i class="fa fa-check-circle"></i> Pending Missing</a>&nbsp; 
                         <a class="btn btn-warning" runat="server" onserverclick="btn_BackToSearching_ServerClick" id="btn_BackToSearching"><i class="fa fa-check-circle"></i> Back To Searching</a>&nbsp;  
                         <a class="btn btn-warning" runat="server" onserverclick="btn_DeleteRequest_ServerClick" id="btn_DeleteRequest"><i class="fa fa-check-circle"></i> Delete Request</a>&nbsp;  
                        <a class="btn btn-warning" runat="server" onserverclick="btn_PendingQC_ServerClick" id="btn_Mang_PendingQC"><i class="fa fa-check-circle"></i> Confirmed To QC</a>&nbsp; 
                        <a class="btn btn-warning" runat="server" onserverclick="btn_prime_ServerClick" id="btn_Mang_prime"><i class="fa fa-check-circle"></i> Confirmed To TPA</a>&nbsp; 
                        <a class="btn btn-warning" runat="server" onserverclick="btn_Quality_Closing_ServerClick" id="btn_Closed_Quality"><i class="fa fa-check-circle"></i> Closing Quality Control</a>&nbsp; 
                        <a class="btn btn-warning" runat="server" onserverclick="btn_Mang_ServerClick" id="btn_Mang"><i class="fa fa-check-circle"></i> Approve Request</a>&nbsp;
                        <a class="btn btn-warning" runat="server" onserverclick="btn_AcceptAndClose_ServerClick" id="btn_AcceptAndClose"><i class="fa fa-check-circle"></i> Accept</a>&nbsp;
                        <a class="btn btn-warning" runat="server" onserverclick="Back_To_Quality_Control" id="btn_BackTo_Quality"><i class="fa fa-check-circle"></i> Back_To_Quality Control</a>&nbsp;
                    </div>
                </div>
            </div>
                <br />
                <div class="alert alert-success alert-dismissable" runat="server" id="div_QualityBackComments">
                    Please Insert Your Comment
                </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
