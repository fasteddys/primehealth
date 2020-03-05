<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="MyAccount.aspx.cs" Inherits="WebApplication1.Portal.MyAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <div class="jumbotron" data-pages="parallax">
        <div class="container-fluid container-fixed-lg sm-p-l-20 sm-p-r-20">
            <div class="inner">

                <ul class="breadcrumb">
                    <li>
                        <p>DashBoard</p>
                    </li>
                    <li><a href="#" class="active">My Account</a>
                    </li>
                </ul>
                <div class="alert alert-success alert-dismissable" runat="server" id="div_success">
                    User Updated Successfully
                </div>
                <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
                    Error Operation
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid container-fixed-lg">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="panel-title">
                            Account Setting
                        </div>
                    </div>
                    <div class="panel-body">
                        <form class="" id="myForm" role="form">
                            <div class="form-group form-group-default required ">
                                <label>Username</label>
                                <input type="text" runat="server" id="BatchNumTxt" class="form-control required" required="required">
                            </div>
                            <div class="form-group form-group-default required ">
                                <label>Password</label>
                                <input type="password" runat="server" id="ClaimsNUmtxt" class="form-control required" required="required">
                            </div>
                            <div align="center" class="row">
                                <button id="SubmitBtn" runat="server" onserverclick="SubmitBtn_ServerClick" class="btn btn-primary btn-cons m-b-10" type="button">
                                    <i class="pg-form"></i><span class="bold">Update Password</span>
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
