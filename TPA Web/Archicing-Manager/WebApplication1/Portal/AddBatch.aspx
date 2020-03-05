<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="AddBatch.aspx.cs" Inherits="WebApplication1.Portal.BatchManager" %>
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
                    <li><a href="#" class="active">New Request</a>
                    </li>
                </ul>
                <div class="alert alert-success alert-dismissable" runat="server" id="div_success">
                    Batch Added Successfully
                </div>
                <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
                    Erorr Opration
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
                            Add New Batch
                        </div>
                    </div>
                    <div class="panel-body">
                        <form class="" id="myForm" role="form" onSubmit="return validateComplete(document.myform)">
                            <div class="form-group form-group-default required ">
                                <label>Batch Num</label>
                                <input type="text" runat="server" id="BatchNumTxt" class="form-control required" required="required">
                            </div>
                            <div class="form-group form-group-default required ">
                                <label>Box Num</label>
                                <input type="text" runat="server" id="BoxNumTxt" class="form-control required" required="required">
                            </div>
                            <div align="center" class="row">
                                <button id="SubmitBtn" runat="server" onserverclick="AddBatchBtn_ServerClick" class="btn btn-primary btn-cons m-b-10" type="button">
                                    <i class="pg-form"></i><span class="bold">Submit</span>
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="container-fluid container-fixed-lg">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <div class="panel-title">
                                All Batches
                            </div>
                            <div class="pull-right">
                                <div class="col-xs-12">
                                    <input type="text" id="search-table" class="form-control pull-right" placeholder="Search">
                                </div>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                        <div class="panel-body">
                            <table class="table table-hover demo-table-search" id="tableWithSearch">
                                <thead>
                                    <tr align="center">
                                        <td>ID</td>
                                        <td>Batch</td>
                                        <td>Box</td>
                                        <td>Claims Number</td>
                                        <td>Added employee </td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:ListView runat="server" ID="ItemsList">
                                        <ItemTemplate>
                                            <tr align="center">
                                                <td><asp:Label ID="IDTxt" runat="server" Text='<%#Eval("id")%>' /></td>
                                                <td><strong><%#Eval("batch")%></strong></td>
                                                <td><b><%#Eval("box") %></b></td>
                                                <td><b><%#Eval("NumOFClaims") %></b></td>

                                                
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
