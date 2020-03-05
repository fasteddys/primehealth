<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ShowAllFeedback.aspx.cs" Inherits="CallCenterNotesApp.Portal.ShowAllFeedback" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
       <link href="../assets/css/jquery.dataTables.css" rel="stylesheet" />
    <div class="row">
            <div class="col-md-12">
                <ul class="breadcrumb">
                    <li><a href="#"><i class="fa fa-home"></i> Home </a></li>
                    <li><a href="#"> All Feedback </a></li>
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
                                All Feedback
                            </header>
                            <br />
<%--                            <form class="form-horizontal tasi-form" role="form" runat="server">--%>
                                <div class="form-group" style="font-weight: bold; font-size: large; color:darkcyan">
                                    <label class="col-sm-2 col-sm-2 control-label">Seen</label>
                                    <div class="col-sm-3">
                                        
                                        <asp:DropDownList ID="FeedbackTypeDropdownListnew" runat="server" class="form-control m-b-10" AutoPostBack="True">
                                            <asp:ListItem Value="Not">Select Feedback Type Please</asp:ListItem>
                                            <asp:ListItem Value="Yes">Seen</asp:ListItem>
                                            <asp:ListItem Value="No">NotSeen</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <asp:ScriptManager ID="ScriptManager1" runat="server" />
                                <asp:Timer ID="Timer1" OnTick="RefreshPageTimerFN" runat="server" Interval="20000" />
<%--                            </form>--%>
                            
                            <div class="panel-body table-responsive">
                                <table class="table " data-sortable id="example">
                                    <thead >
                                        <tr style="font-weight: bold; color:darkblue" >
                                            <th style='text-align:center;'>ID</th>
                                            <th style='text-align:center;'>Title</th>
                                            <th style='text-align:center;'>User Name</th>
                                            <th style='text-align:center;'>Medical ID</th>
                                            <th style='text-align:center;'>Date</th>
                                            <th style='text-align:center;'>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody style='text-align:center;vertical-align:middle'>
                                        <asp:ListView runat="server" ID="lstNewReq">
                                                <ItemTemplate>
                                                    <tr >
                                                        <td><%#Eval("id") %></td>
                                                        <td><strong><%#Eval("Title") %></strong></td>
                                                        <td><strong><%#Eval("UserName") %></strong></td>
                                                        <td><b><%#Eval("MedicalID") %></b></td>
                                                        <td><b><%#Eval("CreationTime") %></b></td>
                                                        <td>
													        <div class="btn-group btn-group-xs">
													          <a data-toggle="tooltip" href="ManageFeedback.aspx?id=<%#Eval("id")%>" title="Edit" class="btn btn-default"><i class="fa fa-edit"></i></a>
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
