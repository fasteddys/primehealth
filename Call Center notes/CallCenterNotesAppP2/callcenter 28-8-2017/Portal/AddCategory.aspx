<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="AddCategory.aspx.cs" Inherits="CallCenterNotesApp.Portal.AddCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <link href="../assets/css/jquery.dataTables.css" rel="stylesheet" />
    <div class="row">
                <div class="col-md-12">
                    <ul class="breadcrumb">
                        <li><a href="#"><i class="fa fa-home"></i> Home</a></li>
                        <li><a href="#">Add New Category</a></li>
                    </ul>
                </div>
            </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
                    <section class="content">
                        <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
                                Please Insert the Correct Full Data of Category 
                            </div>
                        <div class="alert alert-success alert-dismissable" runat="server" id="div_Success">
                                Your Category's data Inserted Correctly
                            </div>
                        <div class="row">
                            <div class="col-md-12">
                                <section class="panel">
                                    <div class="panel-body">
<%--                                        <form class="form-horizontal tasi-form" role="form" runat="server">--%>

                                            <div class="form-group" >
                                                <label class="col-sm-2 col-sm-2 control-label"> Category Name : </label>
                                                <div class="col-sm-4">
                                                    <input type="text" class="form-control input-lg m-b-10" id="CategoryNameID" runat="server">
                                                </div>
                                            </div>
                                             
                                            <div class="col-md-12" style="text-align:center;">
                                                <div class="toolbar-btn-action">
                                                    <button type="submit" class="btn btn-primary" id="btn_Submit" runat="server" onserverclick="SubmitNewCategory" >Submit Category Data</button>
                                                </div>
                                            </div>
<%--                                        </form>--%>
                                        <br />
                                        <div class="panel-body table-responsive">
                                <table class="table " id="example">
                                    <thead >
                                        <tr style="font-weight: bold; color:darkblue" >
                                            <th style='text-align:center;'>ID</th>
                                            <th style='text-align:center;'>Category Name</th>
                                            <th style='text-align:center;'>Actions</th>
                                        </tr>
                                    </thead>
                                    <tbody style='text-align:center;vertical-align:middle'>
                                        <asp:ListView runat="server" ID="lstNewReq">
                                                <ItemTemplate>
                                                    <tr >
                                                        <td><%#Eval("Categories_id") %></td>
                                                        <td><strong><%#Eval("Categories_name") %></strong></td>
                                                        <td>
													        <div class="btn-group btn-group-xs">
													          <a data-toggle="tooltip" href="ManageCategory'sData.aspx?id=<%#Eval("Categories_id")%>" title="Edit" class="btn btn-default"><i class="fa fa-edit"></i></a>
													        </div>
												        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                    </tbody>
                                </table>
                                <br />
                            </div>
                                    </div>
                                </section>
                            </div>
                        </div>
                    </section>
            <!-- /.content -->
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
