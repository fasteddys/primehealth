<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddSubLocation.aspx.cs" Inherits="CallCenterNotesApp.Portal.AddSubLocation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <link href="../assets/css/jquery.dataTables.css" rel="stylesheet" />
                <div class="row">
                    <div class="col-md-12">
                        <ul class="breadcrumb">
                            <li><a href="#"><i class="fa fa-home"></i>Home</a></li>
                            <li><a href="#">Add New SubLocation</a></li>
                        </ul>
                    </div>
                </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <section class="content">
        <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
            Please Insert the Correct Full Data of SubLocation 
        </div>
        <div class="alert alert-success alert-dismissable" runat="server" id="div_Success">
            Your SubLocation data Inserted Correctly
        </div>
        <div class="row">
            <div class="col-md-12">
                <section class="panel">
                    <div class="panel-body">
<%--                        <form class="form-horizontal tasi-form" role="form" runat="server">--%>

                            <div class="form-group">
                                <label class="col-sm-2 col-sm-2 control-label">SubLocation Name : </label>
                                <div class="col-sm-4">
                                    <input type="text" class="form-control input-lg m-b-10" id="SubLocationNameID" runat="server">
                                </div>
                                <label class="col-sm-2 col-sm-2 control-label">Location Name : </label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="LocationID" runat="server" class="form-control m-b-10" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-12" style="text-align: center;">
                                <div class="toolbar-btn-action">
                                    <button type="submit" class="btn btn-primary" id="btn_Submit" onserverclick="SubmitNewSubLocation" runat="server">Submit SubLocation Data</button>
                                </div>
                            </div>
<%--                        </form>--%>
                        <br />
                        <div class="panel-body table-responsive" id="TableDiv" runat="server">
                            <table class="table " id="example" >
                                <thead>
                                    <tr style="font-weight: bold; color: darkblue">
                                        <th style='text-align: center;'>ID</th>
                                        <th style='text-align: center;'>Sublocation Name</th>
                                        <th style='text-align: center;'>Location Name</th>
                                        <th style='text-align: center;'>Actions</th>
                                    </tr>
                                </thead>
                                <tbody style='text-align: center; vertical-align: middle'>
                                    <asp:ListView runat="server" ID="lstNewReq">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("SubLocID") %></td>
                                                <td><strong><%#Eval("SubLocName") %></strong></td>
                                                <td><strong><%#Eval("LocationName") %></strong></td>
                                                <td>
                                                    <div class="btn-group btn-group-xs">
                                                        <a data-toggle="tooltip" href="ManageSubLocation'sData.aspx?id=<%#Eval("SubLocID")%>" title="Edit" class="btn btn-default"><i class="fa fa-edit"></i></a>
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
