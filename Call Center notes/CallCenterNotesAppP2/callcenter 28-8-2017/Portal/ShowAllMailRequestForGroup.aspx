<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPages/MasterPage.Master" AutoEventWireup="true" CodeBehind="ShowAllMailRequestForGroup.aspx.cs" Inherits="CallCenterNotesApp.Portal.ShowAllMailRequestForGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <link href="../assets/css/jquery.dataTables.css" rel="stylesheet" />
    <div class="row">
        <div class="col-md-12">
            <ul class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home </a></li>
            </ul>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <section class="content">
<%--        <form runat="server">--%>
            <div class="row">
                <div class="col-xs-12">
                    <div class="panel">
                        <header class="panel-heading">
                            All Email Requests
                        </header>
                        <div class="row" style="margin: 5px">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" Text="Medical ID"></asp:Label>
                                    <asp:TextBox ID="MedicalIDtxt" CssClass="form-control input-sm " runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label12" runat="server" Text="Mail Subject"></asp:Label>
                                    <asp:TextBox ID="MailSubject" runat="server" CssClass="form-control input-sm "></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="Label9" runat="server" Text="Email Content"></asp:Label>
                                    <asp:TextBox ID="EmailContent" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="recipientlable" runat="server" Text="Receipts (To/Cc/Bcc)"></asp:Label>
                                    <asp:TextBox ID="recipient" runat="server" CssClass="form-control input-sm "></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Attachment" runat="server" Text="Attachment Name"></asp:Label>
                                    <asp:TextBox ID="AttachmentName" runat="server" CssClass="form-control input-sm "></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label13" runat="server" Text="Mail Source"></asp:Label>
                                    <asp:DropDownList ID="MailSource" runat="server" DataValueField="ID" CssClass="form-control input-sm ">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                           
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label8" runat="server" Text="Status"></asp:Label>
                                    <asp:DropDownList ID="Status" runat="server" DataValueField="ID" CssClass="form-control input-sm ">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label3" runat="server" Text="Operator Name"></asp:Label>
                                    <asp:TextBox ID="OperatorName" runat="server" CssClass="form-control input-sm "></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" Text="Doctor Name"></asp:Label>
                                    <asp:TextBox ID="DoctorName" runat="server" CssClass="form-control input-sm "></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-2 ">
                                <div class="form-group">
                                    <asp:Label ID="Label4" runat="server" Text="Audit Name"></asp:Label>
                                    <asp:TextBox ID="AuditName" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label7" runat="server" Text="Provider Name"></asp:Label>
                                    <asp:TextBox ID="ProviderName" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="lable9" runat="server" Text="TicketType"></asp:Label>
                                    <asp:DropDownList ID="Type" runat="server" DataTextField="Name" DataValueField="ID" CssClass="form-control input-sm">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>Special</asp:ListItem>
                                        <asp:ListItem>Normal</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label10" runat="server" Text="Approval Category"></asp:Label>
                                    <asp:DropDownList ID="ApprovalCategory" runat="server" DataValueField="ID" CssClass="form-control input-sm ">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label11" runat="server" Text="Priority"></asp:Label>
                                    <asp:DropDownList ID="Priority" runat="server" DataValueField="ID" CssClass="form-control input-sm ">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                             <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label5" runat="server" Text="Creation From"></asp:Label>
                                    <input type="date" runat="server" id="CreationDateFrom" class="form-control input-sm " />
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label6" runat="server" Text="Creation To"></asp:Label>
                                    <input type="date" runat="server" id="CrationEndDateTo" class="form-control input-sm " /><br />
                                </div>
                            </div>
                        </div>
                        <div style="margin-left: 40%; margin-top: 25px; margin-bottom: 25px">
                            <asp:Button ID="Search" runat="server" OnClick="Search_Click" Text="Search With Filters" CssClass="btn btn-primary" />

                        </div>

                        <div class="panel-body table-responsive">
                            <table class="table " id="example">
                                <thead>
                                    <tr style="font-weight: bold; color: darkblue">
                                        <th style='text-align: center;'>ID</th>
                                        <th style='text-align: center;'>Medical ID</th>
                                        <th style='text-align: center;'>Request Subject</th>
                                        <th style='text-align: center;'>Operator Name</th>
                                        <th style='text-align: center;'>Creation Date</th>
                                        <%--                                        <th style='text-align: center;'>Close Time</th>--%>

                                        <th style='text-align: center;'>Assigned Doctor</th>
                                        <th style='text-align: center;'>Assigned Audit</th>
                                        <th style='text-align: center;'>Request Type</th>
                                        <th style='text-align: center;'>Request Status</th>
                                        <th style='text-align: center;'>Category</th>
                                        <th style='text-align: center;'>Actions</th>
                                    </tr>
                                </thead>
                                <tbody style='text-align: center; vertical-align: middle'>
                                    <asp:ListView runat="server" ID="lstNewReq">
                                        <ItemTemplate>
                                            <tr style='background-color: <%#Eval("ColorHex")%>'>
                                                <td><%#Eval("ID") %></td>
                                                <td><strong><%#Eval("MedicalID") %></strong></td>
                                                <td><strong><%#Eval("MailSubject") %></strong></td>
                                                <td><strong><%#Eval("OperatorAssignee") %></strong></td>
                                                <td><strong><%#Eval("CreationDate") %></strong></td>
                                                <%--                                                <td><strong><%#Eval("ClosedTime") %></strong></td>--%>

                                                <td><strong><%#Eval("DoctorAssignee") %></strong></td>
                                                <td><strong><%#Eval("AuditAssignee") %></strong></td>
                                                <td><strong><%#Eval("RequestTypeName") %></strong></td>
                                                <td><strong><%#Eval("RequestStatusName") %></strong></td>

                                                <td><strong><%#Eval("CategoryName") %></strong></td>

                                                <td>
                                                    <div class="btn-group btn-group-xs">
                                                        <a data-toggle="tooltip" href="CcEmailRequestManageRequest.aspx?id=<%#Eval("ID")%>" title="Edit" class="btn btn-default"><i class="fa fa-edit"></i></a>
                                                    </div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </tbody>
                            </table>
                            <br />
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
            </div>
            <asp:Panel ID="Panel2" runat="server">
                <%-- <asp:Button ID="Next" runat="server" Text="Next" OnClick="Next_Click" /> 
                            <asp:Button ID="Befor" runat="server" Text="Before" OnClick="Befor_Click" />--%>
            </asp:Panel>
<%--        </form>--%>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
    <script src="../assets/js/jquery.min.js"></script>
    <script src="../assets/js/jquery.js"></script>
    <script src="../assets/js/jquery.dataTables.js"></script>
    <script>
        $(document).ready(function () {
            $('#example').DataTable({
                "order": [[0, "desc"]]
            });
        })
    </script>

</asp:Content>
