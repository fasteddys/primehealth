<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ViewNotes.aspx.cs" Inherits="CallCenterNotesApp.Portal.ViewNotes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
       <link href="../assets/css/jquery.dataTables.css" rel="stylesheet" />
    <div class="row">
            <div class="col-md-12">
                <ul class="breadcrumb">
                    <li><a href="#"><i class="fa fa-home"></i> Home </a></li>
                    <li><a href="#"> All Client Notes </a></li>
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
                                All Client Notes

                            </header>
                            <br />
<%--                            <form class="form-horizontal tasi-form" role="form" runat="server">--%>
                                <div class="form-group" style="font-weight: bold; font-size: large; color:darkcyan">
                                    <label class="col-sm-2 col-sm-2 control-label">Client Type</label>
                                    <div class="col-sm-3">
                                        
                                        <asp:DropDownList ID="ClientTypeDropdownListnew" runat="server" class="form-control m-b-10" AutoPostBack="True" >
                                            <asp:ListItem Value="NoData">Select Notes Type</asp:ListItem>
                                            <asp:ListItem Value="Client">Client Notes</asp:ListItem>
                                            <asp:ListItem Value="Provider">Provider Notes</asp:ListItem>
                                            <asp:ListItem Value="Instructions">Instructions Notes</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <asp:ScriptManager ID="ScriptManager1" runat="server" />
                                <asp:Timer ID="Timer1" OnTick="RefreshPageTimerFN" runat="server" Interval="20000" />
<%--                            </form>--%>
                            <div class="panel-body table-responsive">
                                
                                <table class="table " data-sortable id="example" >
                                    <thead >
                                        <tr  >
                                            <th style='text-align:center;'>ID</th>
                                            <th style='text-align:center;'>Creator</th>
                                            <th style='text-align:center;'>Creation Date</th>
                                            <th style='text-align:center;'>Client Name</th>
                                            <th style='text-align:center;'>Client Type</th>
                                            <th style='text-align:center;'>action</th>
                                        </tr>
                                    </thead>
                                    <tbody style='text-align:center;vertical-align:middle'>
                                        <asp:ListView runat="server" ID="lstNewReq">
                                                <ItemTemplate>
                                                    <tr >
                                                        <td><%#Eval("ClientID") %></td>
                                                        <td><strong><%#Eval("Creator") %></strong></td>
                                                        <td><strong><%#Eval("CreationTime") %></strong></td>
                                                        <td><b><%#Eval("ClientName") %></b></td>
                                                        <td><b><%#Eval("ClientType") %></b></td>
                                                        <td>
													        <div class="btn-group btn-group-xs">
													          <a data-toggle="tooltip" href="ManageClientNotes.aspx?id=<%#Eval("ClientID")%>" title="Edit" class="btn btn-default"><i class="fa fa-edit"></i></a>
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
