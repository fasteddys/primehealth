<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="ManageSubRequest.aspx.cs" Inherits="WebApplication1.Portal.ManageSubRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    

    <script src="../Scripts/jquery-3.0.0.min.js"></script>
    <script language="javascript" type="text/javascript" >
       
        function PrintElem(elem) {
            Popup($(elem).html());
        }

        function Popup(data) {
            var mywindow = window.open('', 'my div', 'height=400,width=600');
            mywindow.document.write('<html><head><title>my div</title>');
            /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');
            mywindow.document.write('</head><body >');
            mywindow.document.write(data);
            
            mywindow.document.write('</body></html>');

            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10

            mywindow.print();
            mywindow.close();

            return true;
        }


   

        </script>


</asp:Content>

    
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron" data-pages="parallax">
        <div class="container-fluid container-fixed-lg sm-p-l-20 sm-p-r-20">
            <div class="inner">
                
                <ul class="breadcrumb">
                    <li>
                        <p><a href="AllRequests.aspx">DashBoard</a></p>
                    </li>
                   
                    <li><a href="#" onclick="JavaScript:window.history.back(1);return false;">Submission Requests</a></li>
                    <li><a href="#" class="active">Manage Submission Request</a>
                    </li>
                </ul>
                 <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
                    Error Operation..Please Insert Number Of Batches && Claims Received...
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid container-fixed-lg">
        <div class="widget form-group-default">
            <div class="widget-content padding">
                <div align="center">
                    <h2>Manage Submissions Request</h2>
                </div>
                <hr />
                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label"></label>
                        <div class="col-sm-10" align="Right">
                            <a class="btn btn-success" runat="server" onserverclick="btn_asign_ServerClick" id="btn_asign"><i class="fa fa-check-circle-o"></i> Assign Request To Me</a>
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
                        <label for="input-text" class="col-sm-2 control-label">Batches Type</label>
                        <div class="col-sm-10">
                            <label runat="server" id="lbl_Batches_Type"></label>
                        </div>
                    </div>
                </div>
                <br />
                 <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Month</label>
                        <div class="col-sm-10">
                            <label runat="server" id="lbl_Month"></label>
                        </div>
                    </div>
                </div>
                <br />
                  <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Year</label>
                        <div class="col-sm-10">
                            <label runat="server" id="lbl_Year"></label>
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
               <div >
 

  &nbsp;<asp:Button ID="Button1" runat="server" class="btn btn-success" Text="Download User Comment"  OnClick="Button1_Click" />
</div>
                <br />

               
                <div class="row" id="TPA" runat="server">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">TPA Comments</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Width="100%" Rows="6"></asp:TextBox>
                        </div>
                    </div>
                    </div>
                     <br />
                     <div class="row" id="Archive" runat="server">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Archiving Comments</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Width="100%" Rows="6"></asp:TextBox>
                        </div>
                    </div>

                </div>
                </div>
                <br />
                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Number Of Sent Batches</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="reqitems" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <br />
                 <br />
                <div class="row">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Number Of Received Batches</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="RecBatches" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                </div>
                <br />
                 <br />
                <div class="row" runat="server" id="divclaims">
                    <div class="form-group">
                        <label for="input-text" class="col-sm-2 control-label">Number Of Received Claims</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="RecClaims" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <br />
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
               
             
        </div>
                 <div class="col-md-12">
                    <div class="toolbar-btn-action" align="center">
                       
                        <a class="btn btn-success" runat="server" onserverclick="btn_rec_ServerClick" id="btn_rec"><i class="fa fa-check-circle-o"></i>Received</a>
                        <a class="btn btn-danger" runat="server" onserverclick="btn_Reject_ServerClick" id="btn_Reject"><i class="fa fa-lock"></i> Reject</a>
                        <a class="btn btn-success" runat="server" onserverclick="btn_confirmed_ServerClick" id="btn_issue"><i class="fa fa-check"></i> Confirmed</a>
                         <%--<a class="btn btn-success" runat="server" onserverclick="btn_SendTPA_ServerClick" id="A33"><i class="fa fa-check"></i> send to TPA</a>--%>
                        <a class="btn btn-warning" runat="server" onserverclick="btn_Mang_ServerClick" id="btn_Mang"><i class="fa fa-check-circle"></i> Approve Request</a>&nbsp;
                    </div>
                </div>
    </div>

   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
   

</asp:Content>

