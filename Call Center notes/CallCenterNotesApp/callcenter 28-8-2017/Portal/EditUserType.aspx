<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="EditUserType.aspx.cs" Inherits="CallCenterNotesApp.Portal.EditUserType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
     <link href="../assets/css/jquery.dataTables.css" rel="stylesheet" />
    <div class="row">
        <div class="col-md-12">
            <ul class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home </a></li>
                <li><a href="#">Edit user type</a></li>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
        Error , Please Verify Inputs
    </div>
    <div class="alert alert-success alert-dismissable" runat="server" id="div_Success">
        Please Verify Inputs
    </div>
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <section class="panel">
                    <header class="panel-heading">
                        <h4>User Details</h4>
                    </header>
                    <div class="panel-body">
<%--                        <form class="form-horizontal tasi-form" role="form" runat="server">--%>
                            <div style="top: 30px">
                                <div class="form-group" runat="server">
                                    <label class="col-sm-2 col-sm-2 control-label">User Name </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="UserName" runat="server" disabled>
                                    </div>
                                    <label class="col-sm-2 col-sm-2 control-label">User Type </label>
                                    <div class="col-sm-4">
                                        <input type="text" class="form-control input-lg m-b-10" id="UserType" runat="server" disabled>
                                    </div>
                                </div>
                                 <div class="form-group" runat="server">
                                    <label class="col-sm-2 col-sm-2 control-label">Change Type To</label>
                                    <div class="col-sm-4">
                                         <asp:DropDownList runat="server" ID="Dropdownlist1" Width="248px" Height="45px">
                                        <asp:ListItem Text="Select User Type " Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="Call Center Doctor" Value="CallCenterDoctor"></asp:ListItem>
                                        <asp:ListItem Text="Call Center Audit Doctor" Value="CallCenterAuditDoctor"></asp:ListItem>
                                        <asp:ListItem Text="Call Center Manager" Value="CallCenterManager"></asp:ListItem>

                                    </asp:DropDownList>
                                    </div>
                                </div>


                             
                            </div>
                    </div>
                    <br />
                <div class="container centerMarg" >
                       <asp:Button ID="btnSubmitEdit" runat="server" OnClick="SubmitEdit_Click" Text="Submit Edit" CssClass="btn btn-primary"/>
                       <asp:Button ID="btnResetPasswordUser" runat="server" OnClick="ResetPasswordUser_Click" Text="Reset Password" CssClass="btn btn-danger"/>
                       <asp:Button ID="btnDeActivateUser" runat="server" OnClick="DeActivateUser_Click" Text="DeActivate" CssClass="btn btn-info"/>
                       <asp:Button ID="btnActiveUser" runat="server" OnClick="ActiveUser_Click" Text="Activate" CssClass="btn btn-success"/>
                </div>
                    <br />
                    <br />
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
    <style>
        .centerMarg
        {
            
        width: 80%;
        margin-left: 192px;
        padding: 10px;

        }
        .centerMarg .btn
        {
            margin-right:2%;
            padding:10px 15px;
            border-radius: 20px;


        }
    </style>
</asp:Content>
