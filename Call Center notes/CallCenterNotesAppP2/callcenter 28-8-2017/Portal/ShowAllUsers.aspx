<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ShowAllUsers.aspx.cs" Inherits="CallCenterNotesApp.Portal.ShowAllUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
       <link href="../assets/css/jquery.dataTables.css" rel="stylesheet" />
    <div class="row">
            <div class="col-md-12">
                <ul class="breadcrumb">
                    <li><a href="#"><i class="fa fa-home"></i> Home </a></li>
                    <li><a href="#"> Show All Users </a></li>
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
                                Show All Users

                            </header>

                            <div class="panel-body table-responsive">
                                
                                <table class="table " data-sortable id="example" >
                                    <thead >
                                        <tr  >
                                            <th style='text-align:center;'>ID</th>
                                            <th style='text-align:center;'>User Name</th>
                                            <th style='text-align:center;'>Creation Date</th>
                                            <th style='text-align:center;'>Creator</th>
                                            <th style='text-align:center;'>Type</th>
                                            <th style='text-align:center;'>action</th>
                                        </tr>
                                    </thead>
                                    <tbody style='text-align:center;vertical-align:middle'>
                                        <asp:ListView runat="server" ID="lstNewReq">
                                                <ItemTemplate>
                                                    <tr >
                                                        <td><%#Eval("UserID") %></td>
                                                        <td><strong><%#Eval("UserName") %></strong></td>
                                                        <td><strong><%#Eval("AdditionDate") %></strong></td>
                                                        <td><b><%#Eval("AddedBy") %></b></td>
                                                        <td><b><%#Eval("UserType") %></b></td>
                                                        <td>
													        <div class="btn-group btn-group-xs">

<%--                                                          <button type="submit" class="btn btn-default" title="Reset" id="btn_UpdateUserData" onserverclick="UpdateUserData_ServerClick" ><i class="fa fa-edit"></i></button>--%>
													          <a data-toggle="tooltip" href="ManageUser.aspx?id=<%#Eval("UserID")%>" title="Edit" class="btn btn-default"><i class="fa fa-edit"></i></a>

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
