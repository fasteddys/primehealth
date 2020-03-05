<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="CcEmailRequestSearchDetailsRequest.aspx.cs" Inherits="CallCenterNotesApp.Portal.CcEmailRequestSearchDetailsRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <link href="../assets/css/jquery.dataTables.css" rel="stylesheet" />
    <div class="row">
        <div class="col-md-12">
            <ul class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home </a></li>
                <li><a href="#">Show Specific Request</a></li>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <section class="content">
        <div class="row">
            <div class="col-xs-12">
                <div class="panel">
                    <header class="panel-heading">
                        All Request Details
                    </header>
                    <br />


                    <div class="panel-body table-responsive">
                        <table class="table " data-sortable id="example">
                            <thead>
                                <tr style="font-weight: bold; color: darkblue">
                                    <th style='text-align: center;'>ID</th>
                                    <th style='text-align: center;'>Medical ID</th>
                                    <th style='text-align: center;'>Request Subject</th>
                                    <th style='text-align: center;'>Created By</th>
                                    <th style='text-align: center;'>Creation Date</th>
                                    <th style='text-align: center;'>Operator Name</th>
                                    <th style='text-align: center;'>Assigned Doctor</th>
                                    <th style='text-align: center;'>Assigned Audit</th>
                                    <th style='text-align: center;'>Request Type</th>
                                    <th style='text-align: center;'>Request Status</th>
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
                                            <td><strong><%#Eval("CreatedBy") %></strong></td>
                                            <td><strong><%#Eval("CreationDate") %></strong></td>
                                            <td><strong><%#Eval("OperatorAssignee") %></strong></td>
                                            <td><strong><%#Eval("DoctorAssignee") %></strong></td>
                                            <td><strong><%#Eval("AuditAssignee") %></strong></td>
                                            <td><strong><%#Eval("RequestTypeName") %></strong></td>
                                            <td><strong><%#Eval("RequestStatusName") %></strong></td>

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
    </section>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">

    <script src="../assets/js/jquery.min.js"></script>
    <script src="../assets/js/jquery.js"></script>
    <script src="../assets/js/jquery.dataTables.js"></script>
    <script>
        $(document).ready(function () {
            $('#example').DataTable();
        });
    </script>

</asp:Content>
