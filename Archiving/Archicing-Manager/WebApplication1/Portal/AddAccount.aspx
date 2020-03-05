<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="AddAccount.aspx.cs" Inherits="WebApplication1.Portal.AddAccount" %>
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
                    <li><a href="#" class="active">New Member</a>
                    </li>
                </ul>
                <div class="alert alert-success alert-dismissable" runat="server" id="div_success">
                    Member Added Successfully
                </div>
                <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
                    Member Already Exsists
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid container-fixed-lg" runat="server" id="id2">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="panel-title">
                            Add New Member
                        </div>
                    </div>
                    <div class="panel-body">
                        <form class="" id="myForm" role="form">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="This Filed Is Required" ControlToValidate="BatchNumTxt" ForeColor="Red"></asp:RequiredFieldValidator>
                            <div class="form-group form-group-default required ">
                                <label>Username</label>
                                <input type="text" runat="server" id="BatchNumTxt" class="form-control required" required="required">
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="This Filed Is Required" ControlToValidate="ClaimsNUmtxt" ForeColor="Red"></asp:RequiredFieldValidator>

                            <div class="form-group form-group-default required ">
                                <label>Password</label>
                                <input type="text" runat="server" id="ClaimsNUmtxt" class="form-control required" required="required"   >
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="This Filed Is Required" ControlToValidate="TypeDLL" ForeColor="Red"></asp:RequiredFieldValidator>
                            <div class="form-group form-group-default required ">
                                <label>Type</label>
                                <asp:DropDownList runat="server" ID="TypeDLL" class="form-control required" required="required">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>Admin</asp:ListItem>
                                    <asp:ListItem>Archiving</asp:ListItem>
                                    <asp:ListItem>ArcAdmin</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div align="center" class="row">
                                <button id="SubmitBtn" runat="server" onserverclick="SubmitBtn_ServerClick" class="btn btn-primary btn-cons m-b-10" type="button">
                                    <i class="pg-form"></i><span class="bold">Submit</span>
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
    <script>
        function myFunction() {
            document.getElementById("myForm").reset();
        }
</script>
</asp:Content>
