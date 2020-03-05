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
                 <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
                    Error Operation..Please Insert Number Of Found Items..
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid container-fixed-lg">
        <div class="widget form-group-default">
            <div class="widget-content padding">
                <div align="center">
                    <h2>Manage Request</h2>
                </div>    
                <div id="MainDiv" runat="server">
                      <hr />
                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label"></label>
                        <div class="col-sm-10" align="Right">
                            <a class="btn btn-success" runat="server" onserverclick="btn_asign_ServerClick" id="btn_asign"><i class="fa fa-check-circle-o"></i> Assign Request To Me</a>
                        </div>                       
                        <div class="col-sm-15" align="Right">
                            <a class="btn btn-success" runat="server" onserverclick="btn_asign_confirm"  id="btn_confirm"><i class="fa fa-check-circle-o"></i> Confirm</a>
                        </div>
                        <div class="col-sm-15" align="Right">
                            <a class="btn btn-success" runat="server" onserverclick="btn_Confirm_DataEntry_User"  id="btn_donot_Return_To_Archiving"><i class="fa fa-check-circle-o"></i> Close Ticket</a>
                        </div>
                         <div class="col-sm-15" align="Right">
                            <a class="btn btn-success" runat="server" onserverclick="btn_asign_claims"  id="btn_assign_claims_btn"><i class="fa fa-check-circle-o"></i> Assign Pending Claims To Me</a>
                        </div>
                         <div class="col-sm-15" align="Right">
                            <a class="btn btn-success" runat="server" onserverclick="btn_asign_dataentry"  id="btn_assign_dataentry"><i class="fa fa-check-circle-o"></i> Assign Pending DataEntry To Me</a>
                        </div>
                        <br />
                         <div class="col-sm-15" align="Right">
                            <a class="btn btn-success" runat="server" onserverclick="btn_asign_dataentry_Finish"  id="bttn_assign_dataentry_Finish"><i class="fa fa-check-circle-o"></i> Submitted Back To Archiving</a>
                        </div>
                        <br />
                         <div class="col-sm-15" align="Right">
                            <a class="btn btn-success" runat="server" onserverclick="btn_submit"  id="btnsubmit"><i class="fa fa-check-circle-o"></i> Submitted To Archiving</a>
                        </div>
                        <br />
                         <div class="col-sm-15" align="lift">
                            <a style="background-color:#0000ff;border:3px solid Tomato" class="btn btn-success" runat="server" onserverclick="btn_dataentry" id="btnDataEntry"><i class="fa fa-check-circle-o"></i> Submitted To DataEntry</a>
                        </div>
                        <div style="position:absolute;" id="adminassign" runat="server">
                                   <strong>Assign/Transfer Ticket To:</strong>
                             <asp:DropDownList runat="server" id="drp1"  > </asp:DropDownList>
                             <a class="btn btn-info" runat="server" onserverclick="btn_Transfer" id="btntransfer"><i class="fa fa-check-circle-o"></i> Assign/Transfer</a>
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
                        <label for="input-text" class="col-sm-2 control-label">Request Creator</label>
                        <div class="col-sm-10">
                            <label runat="server" id="lbl_Creator"></label>
                        </div>
                    </div>
                </div>
                <br />
                  <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Assigned to</label>
                        <div class="col-sm-10">
                            <label runat="server" id="lbl_Assignee"></label>
                        </div>
                    </div>
                </div>
                 <br />
                 <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Status</label>
                        <div class="col-sm-10">
                            <label runat="server" id="lbl_status"></label>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
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
                        <label for="input-text" class="col-sm-2 control-label">User Comments</label>
                        <div class="col-sm-10">
                            <textarea runat="server" id="txtrBody" rows="10" style="width: 100%; font-weight: bold; font-size: small" ></textarea>
                        </div>
                    </div>
                </div>

                <br />
                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Archiving Comments</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Width="100%" Rows="6"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Number Of Requested Items</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="reqitems" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Number Of Found Items</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="founditems" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                </div>
                <br />
                <div id="pendingclaims" runat="server">
                       <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">User Comments to Claims Department</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="userpendigclaimscomment" runat="server" TextMode="MultiLine" Width="100%"  Rows="6" style="width: 100%; font-weight: bold"></asp:TextBox>
                        </div>
                    </div>
                </div>
                    <br />
                     <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Claims Assignee:</label>
                        <div class="col-sm-10">
                            <label runat="server" id="claims_assignee"></label>
                        </div>
                    </div>
                </div>
                    <br />
                    <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Claims Comments</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Width="100%"  Rows="6" style="width: 100%; font-weight: bold"></asp:TextBox>
                        </div>
                    </div>
                </div>
                    <br />
                     <div class="row row-list">
                   <div class="col-xs-1"><strong>Pending Claims Duration</strong></div>
                      <div class="col-xs-4">From:  <label runat="server" id="pendingfrom"></label></div>
                    <div class="col-xs-4">To: <label runat="server" id="pendingto"></label></div>
                    </div>  
                </div>
               
                <br />
                <div class="row" runat="server" id="divdownload">
                    <div class="form-group">
                        <asp:GridView ID="downloadlist" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover" EmptyDataText="No files uploaded">
                            <Columns>
                                <asp:BoundField DataField="Text" HeaderText="File Name" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="lnkDownload_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                </div>
                <br />
                       <div id="upload_div" runat="server" style="width:400px; margin:0 auto;">
                                                      
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Attached File</label>
                                          <asp:FileUpload ID="FileUpload1" runat="server" class="btn btn-success btn-rounded" />
                      <br />
                                          <button id="Button1" runat="server" onserverclick="btnUpload_Click" class="btn btn-primary btn-cons m-b-20" type="button" value="Import Excel File">Upload Reply</button>


                  </div>
                    <br />
               

                  

            </div>
                <br />
             
        </div>
                 <div class="col-md-12">
                    <div class="toolbar-btn-action" align="center">
                        <a class="btn btn-success" runat="server" onserverclick="btn_issue_ServerClick" id="btn_issue"><i class="fa fa-check"></i> Found</a>
                        <a class="btn btn-danger" runat="server" onserverclick="btn_reject_ServerClick" id="btn_reject"><i class="fa fa-ban"></i> Reject</a>
                        <a class="btn btn-info" runat="server" onserverclick="btn_Download" id="Downloadbtn"><i class="fa fa-check-circle-o"></i> Download Reply</a>&nbsp;
                        <a class="btn btn-warning" runat="server" onserverclick="btn_Mang_ServerClick" id="btn_Mang"><i class="fa fa-check-circle"></i> Approve Request</a>&nbsp;
                        <a class="btn btn-info" runat="server" onserverclick="btn_Update_ServerClick" id="btn_update"><i class="fa fa-check-circle-o"></i> Update Request</a>&nbsp;
                        <a class="btn btn-info" runat="server" onserverclick="btn_Back_ServerClick" id="btnback"><i class="fa fa-check-circle-o"></i>Back To Archiving</a>&nbsp;
                        <a class="btn btn-info" runat="server" onserverclick="btn_Claims_ServerClick" id="btnclaims"><i class="fa fa-check-circle-o"></i>To Pending Claims</a>&nbsp;
                        <a class="btn btn-info" runat="server" onserverclick="btn_Solved_ServerClick" id="btnsolved"><i class="fa fa-check-circle-o"></i>Solved</a>&nbsp;


                    </div>
                </div>
                </div>
                   <div class="row" runat="server" id="divclaims">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">User Comments to Claims Department</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="userclaimstxt" runat="server" TextMode="MultiLine" Width="100%" Rows="6"></asp:TextBox>
                        </div>
                    </div>
                     <br />
                  <a class="btn btn-info" runat="server" onserverclick="btn_Submit_Claims_ServerClick" id="btnsubmitclaims"><i class="fa fa-check-circle-o"></i>Submit</a>
                </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>