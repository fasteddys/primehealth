<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="CcEmailRequestShowInprogressDocReq.aspx.cs" Inherits="CallCenterNotesApp.Portal.CcEmailRequestShowInprogressDocReq" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
       <link href="../assets/css/jquery.dataTables.css" rel="stylesheet" />
    <div class="row">
            <div class="col-md-12">
                <ul class="breadcrumb">
                    <li><a href="#"><i class="fa fa-home"></i> Home </a></li>
                    <li><a href="#"> Show Inprogress Doctor Request </a></li>
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
                                Show Inprogress Doctor Request
                            </header>
                            <br />
                            
                            
                            <div class="panel-body table-responsive">
                                <table class="table " data-sortable id="example">
                                    <thead >
                                        <tr style="font-weight: bold; color:darkblue" >
                                            <th style='text-align:center;'>ID</th>
                                            <th style='text-align:center;'>Patient Name</th>
                                            <th style='text-align:center;'>Medical ID</th>
                                            <th style='text-align:center;'>Company Name</th>
                                            <th style='text-align:center;'>Assigned Doctor</th>
                                            <th style='text-align:center;'>Ticket Creator</th>
                                            <th style='text-align:center;'>Ticket Creation</th>
                                            <th style='text-align:center;'>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody style='text-align:center;vertical-align:middle'>
                                        <asp:ListView runat="server" ID="lstNewReq">
                                                <ItemTemplate>
                                                    <tr >
                                                        <td><%#Eval("ID") %></td>
                                                        <td><strong><%#Eval("PatientName") %></strong></td>
                                                        <td><strong><%#Eval("UserMedicalID") %></strong></td>
                                                        <td><strong><%#Eval("CompanyName") %></strong></td>
                                                        <td><strong><%#Eval("CallcenterAssignDoctor") %></strong></td>
                                                        <td><strong><%#Eval("CallcenterTicketCreator") %></strong></td>
                                                        <td><strong><%#Eval("CallcenterlTicketCraetionTime") %></strong></td>
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
