<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ShowAllClients.aspx.cs" Inherits="CallCenterNotesApp.Portal.ShowAllClients" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
       <link href="../assets/css/jquery.dataTables.css" rel="stylesheet" />
    <div class="row">
            <div class="col-md-12">
                <ul class="breadcrumb">
                    <li><a href="#"><i class="fa fa-home"></i> Home </a></li>
                    <li><a href="#"> Show All Clients </a></li>
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
                                All Clients
                            </header>
                            <br />
<%--                            <form class="form-horizontal tasi-form" role="form" runat="server">--%>
                                <div class="form-group">
                                    <label class="col-sm-2 col-sm-2 control-label">Medical ID </label>
                                    <div class="col-sm-3">
                                        <input type="text" class="form-control input-lg m-b-10" id="MedicalID" runat="server">
                                    </div>
                                    <div class="col-sm-6">
                                        <button type="submit" class="btn btn-primary input-lg m-b-10" id="btn_Submit" runat="server" onserverclick="ClientSearch">Find User Data</button>
                                    </div>
                                </div>
                                
                                

<%--                            </form>--%>
                            
                            <div class="panel-body table-responsive">
                                <table class="table " data-sortable id="example">
                                    <thead >
                                        <tr style="font-weight: bold; color:darkblue" >
                                            <th style='text-align:center;'>ID</th>
                                            <th style='text-align:center;'>User Name</th>
                                            <th style='text-align:center;'>Password</th>
                                            <th style='text-align:center;'>MedicalID</th>
                                            <th style='text-align:center;'>Email</th>
                                            <th style='text-align:center;'>Type</th>
                                            <th style='text-align:center;'>Network</th>
                                            <th style='text-align:center;'>ClientName</th>
                                            <th style='text-align:center;'>Branchname</th>
                                        </tr>
                                    </thead>
                                    <tbody style='text-align:center;vertical-align:middle'>
                                        <asp:ListView runat="server" ID="lstNewReq">
                                                <ItemTemplate>
                                                    <tr >
                                                        <td><%#Eval("ID") %></td>
                                                        <td><strong><%#Eval("Name") %></strong></td>
                                                        <td><strong><%#Eval("Password") %></strong></td>
                                                        <td><strong><%#Eval("Medical_id") %></strong></td>
                                                        <td><strong><%#Eval("Email") %></strong></td>
                                                        <td><strong><%#Eval("gender") %></strong></td>
                                                        <td><strong><%#Eval("Type") %></strong></td>
                                                        <td><strong><%#Eval("ClientName") %></strong></td>
                                                        <td><strong><%#Eval("BranchName") %></strong></td>
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
