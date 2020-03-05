<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ShowAllApprovals.aspx.cs" Inherits="CallCenterNotesApp.Portal.ShowAllApprovals" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
       <link href="../assets/css/jquery.dataTables.css" rel="stylesheet" />
    <div class="row">
            <div class="col-md-12">
                <ul class="breadcrumb">
                    <li><a href="#"><i class="fa fa-home"></i> Home </a></li>
                    <li><a href="#"> All Approvals Requests </a></li>
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
                                All Approvals Requests
                            </header>
                            <br />
<%--                            <form class="form-horizontal tasi-form" role="form" runat="server">--%>
                                <div class="form-group" style="font-weight: bold; font-size: large; color:darkcyan">
                                    <label class="col-sm-2 col-sm-2 control-label">Request Type</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="UserTypeDropdownList" runat="server" class="form-control m-b-10" AutoPostBack="True">
                                            <asp:ListItem Value="NoData">Select Request Type</asp:ListItem>
                                            <asp:ListItem Value="Normal">Normal Request</asp:ListItem>
                                            <asp:ListItem Value="Special">Special Request</asp:ListItem>
                                        </asp:DropDownList>
                                        
                                    </div>
                                    <label class="col-sm-2 col-sm-2 control-label">Request Status</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ReqStatusDropdownList" runat="server" class="form-control m-b-10" AutoPostBack="True" >
                                            <asp:ListItem Value="NoData">Select Request Status</asp:ListItem>
                                            <asp:ListItem Value="PendingApproval">Pending Approval</asp:ListItem>
                                            <asp:ListItem Value="Approved">Approved</asp:ListItem>
                                            <asp:ListItem Value="Rejected">Reject</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <asp:ScriptManager ID="ScriptManager1" runat="server" />
                                <asp:Timer ID="Timer1" OnTick="RefreshPageTimerFN" runat="server" Interval="20000" />
<%--                            </form>--%>
                            
                            <div class="panel-body table-responsive">
                                <table class="table" id="example" >
                                    <thead >
                                        <tr style="font-weight: bold; color:darkblue" >
                                            <th style='text-align:center;'>ID</th>
                                            <th style='text-align:center;'>Creator</th>
                                            <th style='text-align:center;'>Creation Date</th>
                                            <th style='text-align:center;'>Medical ID</th>
                                            <th style='text-align:center;'>Type</th>
                                            <th style='text-align:center;'>Status</th>
                                            <th style='text-align:center;'>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody style='text-align:center;vertical-align:middle'>
                                        <asp:ListView runat="server" ID="lstNewReq">
                                                <ItemTemplate>
                                                    <tr >
                                                        <td><%#Eval("id") %></td>
                                                        <td><strong><%#Eval("userName") %></strong></td>
                                                        <td><strong><%#Eval("CreationTime") %></strong></td>
                                                        <td><b><%#Eval("medicalid") %></b></td>
                                                        <td><b><%#Eval("Reqtype") %></b></td>
                                                        <td><b><%#Eval("ReqStatus") %></b></td>
                                                        <td>
													        <div class="btn-group btn-group-xs">
													          <a data-toggle="tooltip" href="ManageClaimsApprove.aspx?id=<%#Eval("id")%>" title="Edit" class="btn btn-default"><i class="fa fa-edit"></i></a>
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
