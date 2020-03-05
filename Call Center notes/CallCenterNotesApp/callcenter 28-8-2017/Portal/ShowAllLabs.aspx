﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ShowAllLabs.aspx.cs" Inherits="CallCenterNotesApp.Portal.ShowAllLabs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
       <link href="../assets/css/jquery.dataTables.css" rel="stylesheet" />
    <div class="row">
            <div class="col-md-12">
                <ul class="breadcrumb">
                    <li><a href="#"><i class="fa fa-home"></i> Home </a></li>
                    <li><a href="#"> Show All Labs / Scan </a></li>
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
                                All Labs / Scan
                            </header>
                            <br />
<%--                            <form class="form-horizontal tasi-form" role="form" runat="server">--%>
                                <%--<div class="form-group" style="font-weight: bold; font-size: large; color:darkcyan">
                                    <label class="col-sm-2 col-sm-2 control-label">Seen</label>
                                    <div class="col-sm-3">
                                        
                                        <asp:DropDownList ID="FeedbackTypeDropdownListnew" runat="server" class="form-control m-b-10" AutoPostBack="True">
                                            <asp:ListItem Value="Not">Select Feedback Type Please</asp:ListItem>
                                            <asp:ListItem Value="Yes">Seen</asp:ListItem>
                                            <asp:ListItem Value="No">NotSeen</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>
                                <%--<asp:ScriptManager ID="ScriptManager1" runat="server" />
                                <asp:Timer ID="Timer1" OnTick="RefreshPageTimerFN" runat="server" Interval="20000" />--%>
<%--                            </form>--%>
                            
                            <div class="panel-body table-responsive">
                                <table class="table " data-sortable id="example">
                                    <thead >
                                        <tr style="font-weight: bold; color:darkblue" >
                                            <th style='text-align:center;'>ID</th>
                                            <th style='text-align:center;'>Lab / Scan Name</th>
                                            <th style='text-align:center;'>Lab / Scan Location</th>
                                            <th style='text-align:center;'>Lab / Scan Notes</th>
                                            <th style='text-align:center;'>Lab / Scan Category</th>
                                            <th style='text-align:center;'>Edit</th>
                                             <th style='text-align:center;'>Delete</th>
                                        </tr>
                                    </thead>
                                    <tbody style='text-align:center;vertical-align:middle'>
                                        <asp:ListView runat="server" ID="lstNewReq">
                                                <ItemTemplate>
                                                    <tr >
                                                        <td><%#Eval("ID") %></td>
                                                        <td><strong><%#Eval("LabName") %></strong></td>
                                                        <td><strong><%#Eval("LabSublocationName") %></strong></td>
                                                        <td><strong><%#Eval("LabNotes") %></strong></td>
                                                        <td><strong><%#Eval("Category") %></strong></td>
                                                        <td>
													        <div class="btn-group btn-group-xs">
													          <a data-toggle="tooltip" href="ManageLab'sData.aspx?id=<%#Eval("ID")%>" title="Edit" class="btn btn-default"><i class="fa fa-edit"></i></a>
													        </div>
												        </td>

                                                        <td>
													        <div class="btn-group btn-group-xs">
													          <a data-toggle="tooltip" href="DeleteLab.aspx?id=<%#Eval("ID")%>" title="Delete" class="btn btn-default" Onclick="if (!confirm('Are you sure you want delete?')) return false;" ><i class="fa fa-edit"></i></a>
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
