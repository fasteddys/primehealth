<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="SowALLToManager.aspx.cs" Inherits="CallCenterNotesApp.Portal.SowALLToManager" %>

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
                            My Requests
                        </header>
                          <div class="row" style="margin-left:10px">
                              <br />
                                <div class="col-sm-2">
                                    <label>Filter by</label>
                        <asp:DropDownList ID="DropDownType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="form-control">
                            <asp:ListItem>N/A</asp:ListItem>
                            <asp:ListItem>Priority</asp:ListItem>
                            <asp:ListItem>ApprovalCategoty</asp:ListItem>
                            <asp:ListItem>Time</asp:ListItem>
                            <asp:ListItem>TicketType</asp:ListItem>
                        </asp:DropDownList>
                                    </div>
                               <div class="col-sm-2">
                                     <label>Filter Type</label>
                        <asp:DropDownList ID="DropDownSubType" runat="server" OnSelectedIndexChanged="DropDownSubType_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control">
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
                                   </div>
                              <div class="col-sm-2">
                                     <label>Ascending/Descending</label>
                        <asp:DropDownList ID="DropDownAsc" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" CssClass="form-control">
                            <asp:ListItem Value="1">⬆</asp:ListItem>
                            <asp:ListItem Value="2">↓</asp:ListItem>
                        </asp:DropDownList>
                                  </div>
                              </div>
                        <br />


                        <div class="panel-body table-responsive">
                            <table class="table " data-sortable id="example">
                                <thead>
                                    <tr style="font-weight: bold; color: darkblue">
                                        <th style='text-align: center;'>ID</th>
                                        <th style='text-align: center;'>Medical ID</th>
                                        <th style='text-align: center;'>Request Subject</th>
                                        <th style='text-align: center;'>Operator Name</th>
                                        <th style='text-align: center;'>Created By</th>
                                        <th style='text-align: center;'>Creation Date</th>
<%--                                       <th style='text-align: center;'>Close Time</th>--%>
                                        <th style='text-align: center;'>Assigned Doctor</th>
                                        <th style='text-align: center;'>Assigned Audit</th>
                                        <th style='text-align: center;'>Request Type</th>
                                        <th style='text-align: center;'>Request Status</th>
                                        <th style='text-align: center;'>Priority</th>
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
                                                <td><strong><%#Eval("CreatedBy") %></strong></td>
                                                <td><strong><%#Eval("CreationDate") %></strong></td>
<%--                                               <td><strong><%#Eval("ClosedTime") %></strong></td>--%>

                                                <td><strong><%#Eval("DoctorAssignee") %></strong></td>
                                                <td><strong><%#Eval("AuditAssignee") %></strong></td>
                                                <td><strong><%#Eval("RequestTypeName") %></strong></td>
                                                <td><strong><%#Eval("RequestStatusName") %></strong></td>
                                                <td><strong><%#Eval("Priority") %></strong></td>
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
            <asp:Panel ID="Panel1" runat="server"></asp:Panel>

            <asp:Panel ID="Panel2" runat="server">
                <%-- <asp:Button ID="Next" runat="server" Text="Next" OnClick="Next_Click" /> 
                            <asp:Button ID="Befor" runat="server" Text="Befor" OnClick="Befor_Click" />--%>
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
            //$('#example').DataTable();
        });
    </script>

</asp:Content>
