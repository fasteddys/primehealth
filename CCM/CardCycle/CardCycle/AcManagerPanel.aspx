<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Site2.Master"  AutoEventWireup="true" enableeventvalidation="false"  Async="true"
    viewstateencryptionmode="Never" CodeBehind="AcManagerPanel.aspx.cs" Inherits="CardCycle.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type = "text/javascript">
    var ddlText, ddlValue, ddl, lblMesg;
    function CacheItems() {
        ddlText = new Array();
        ddlValue = new Array();
        ddl = document.getElementById("<%=DropDownList2.ClientID %>");
        for (var i = 0; i < ddl.options.length; i++) {
            ddlText[ddlText.length] = ddl.options[i].text;
            ddlValue[ddlValue.length] = ddl.options[i].value;
        }
    }
    window.onload = CacheItems;
   
    function FilterItems(value) {
        ddl.options.length = 0;
        for (var i = 0; i < ddlText.length; i++) {
            if (ddlText[i].toLowerCase().indexOf(value) != -1) {
                AddItem(ddlText[i], ddlValue[i]);
            }
        }
        lblMesg.innerHTML = ddl.options.length + " items found.";
        if (ddl.options.length == 0) {
            AddItem("No items found.", "");
        }
    }
   
    function AddItem(text, value) {
        var opt = document.createElement("option");
        opt.text = text;
        opt.value = value;
        ddl.options.add(opt);
    }
      
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="widget">
                <div class="widget-content padding">
                    <div align="center">
                        <h2>Add Request</h2>
                    </div>
                    <hr />
                    <div class="alert alert-success" runat="server" id="div_success">
                        Request Added Successfully
                    </div>
                    <div class="alert alert-danger" runat="server" id="div_Error">
                        Please Check Request Type , Client And Number Of Members 
                    </div>
                    <div class="alert alert-danger" runat="server" id="div1">
                        error in email block 
                    </div>
                    <div class="col-sm-12" style="display:none" align="Center">
                        <label runat="server" id="Label1" style="font-size: 38px;"></label>
                    </div>
                    <br />
                    <div class="row" id="SubjectDiv" runat="server">
                        <div class="form-group">
                            <label for="input-text" class="col-sm-2 control-label">Request Subject</label>
                            <div class="col-sm-10">
                                <input type="text" class="form-control" runat="server" id="input_text" placeholder="">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="this filed required" ControlToValidate="input_text" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="form-group">
                            <label for="input-text" class="col-sm-2 control-label">Select Request</label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" CssClass="form-control dropdown" Font-Bold="True" AppendDataBoundItems="true" Font-Size="Medium" >
                                    <asp:ListItem>----Select----</asp:ListItem>
                                    <asp:ListItem>  Addition </asp:ListItem>
                                    <asp:ListItem>Discount Card</asp:ListItem>
                                    <asp:ListItem>Cancellation</asp:ListItem>
                                    <asp:ListItem>Renewal</asp:ListItem>
                                    <asp:ListItem>Transfer</asp:ListItem>
                                    <asp:ListItem>New Company</asp:ListItem>
                                    <asp:ListItem>Lost Card</asp:ListItem>
                                    <asp:ListItem>reprint card</asp:ListItem>
                                    <asp:ListItem>Missing Photo</asp:ListItem>
                                    <asp:ListItem>Modification</asp:ListItem>
                                    <asp:ListItem>Suspend</asp:ListItem>
                                    <asp:ListItem>Unsuspend</asp:ListItem>
                                    <asp:ListItem>Exceptional-Addition</asp:ListItem>
                                    <asp:ListItem>Exceptional-Transfer</asp:ListItem>
                                    <asp:ListItem>Exception-Cancellation</asp:ListItem>
                                    <asp:ListItem>Exception-Modification</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="this filed required" ControlToValidate="DropDownList1" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group" runat="server" id="ExcepC">
                            <label for="input-text" class="col-sm-2 control-label">Exception Reasons</label>
                            <div class="col-sm-10">
                                <textarea runat="server" class="summernote-small form-control"  id="ExcepComments" rows="10" style="width: 100%; font-weight: bold; font-size: small" ></textarea>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row" >
                    <div class="form-group" runat="server" id="NCardsdiv">
                        <label for="input-text" class="col-sm-2 control-label" >Number Of Members</label>
                        <div class="col-sm-10">
                            <asp:TextBox runat="server" id="CardsNum" rows="1" style="font-weight: bold; font-size: small" Width="125px" Height="26px" ></asp:TextBox>
                        </div>
                    </div>
                </div>
                    <br />
                    <div class="row">
                        <div class="form-group">
                            <label for="input-text" class="col-sm-2 control-label">Select Client</label>
                            <div class="col-sm-6">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <asp:TextBox ID="txtSearch" placeholder="Search Clients..." CssClass="form-control text-box text-darkblue-3" runat="server"
                                            onkeyup="FilterItems(this.value)"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control dropdown" Font-Bold="True" Font-Size="Medium" DataSourceID="LinqDataSource1" DataTextField="ClientName" DataValueField="id" AppendDataBoundItems="True" >
                                </asp:DropDownList>
                                <asp:LinqDataSource runat="server" EntityTypeName="" ID="LinqDataSource1" ContextTypeName="CardCycle.DAL.DataContextDataContext" Select="new (id, ClientName)" TableName="ClientTBs"></asp:LinqDataSource>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="this filed required" ControlToValidate="DropDownList2" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="form-group">
                            <label for="input-text" class="col-sm-2 control-label">Account Manager Comments</label>
                            <div class="col-sm-10">
                                <textarea runat="server" class="summernote-small form-control" id="txtrBody" rows="10" style="width: 100%; font-weight: bold; font-size: small"></textarea>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="this filed required" ControlToValidate="txtrBody" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="form-group">
                            <label class="col-sm-2 control-label">Attached File</label>
                            <div class="col-sm-10">
                                <%-- <input type="file" class="btn btn-default" title="Select file">--%>
                                <asp:FileUpload ID="FileUpload1" runat="server" class="btn btn-default" />
                                <br />
                                <asp:RegularExpressionValidator ID="rexp" runat="server" ControlToValidate="FileUpload1"
                                    ErrorMessage="Only rar files "
                                    ValidationExpression="(.*\.([Rr][Aa][Rr])$)" ForeColor="Red"></asp:RegularExpressionValidator>
                                <%--<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>--%>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-2">
                        </div>
                        <div class="col-md-10">
                            <input type="button" runat="server" value="Submit Request To Issuance" class="btn btn-success" onserverclick="Add_requestToIssue" />
                        </div>
                    </div>



                </div>

            </div>

        </div>
    </div>

    <!-- Page Specific JS Libraries -->
    <script src="assets/libs/bootstrap-select/bootstrap-select.min.js"></script>
    <script src="assets/libs/bootstrap-inputmask/inputmask.js"></script>
    <script src="assets/libs/summernote/summernote.js"></script>
    <script src="assets/js/pages/forms.js"></script>
</asp:Content>
