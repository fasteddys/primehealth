<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Out Patient.aspx.cs" Inherits="WebApplication1.Portal.OutPaitient" %>
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
                    <li><a href="#" class="active">New Out Patient Transaction</a>
                    </li>
                </ul>
                <div class="alert alert-success alert-dismissable" runat="server" id="div_success">
                    Transaction(s) Added Successfully
                </div>
                <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
                    Error Operation
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid container-fixed-lg">
        <div class="row" id="addout" runat="server">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="panel-title">
                            Add New Out Patient Transaction</div>
                    </div>
                    <div class="panel-body">
                        <form class="" id="myForm" role="form" onSubmit="return validateComplete(document.myform)">
                            <div class="form-group form-group-default required ">
                                <label>Box Num</label>
                                <input type="text" runat="server" id="Text2" class="form-control required" required="required">
                            </div>
                             <div class="form-group form-group-default required ">
                                <label>Batch Num</label>
                                <input type="text" runat="server" id="Text1" class="form-control required" required="required">
                            </div>
                            <div class="form-group form-group-default required ">
                                <label>Claim Code</label>
                                <input type="text" runat="server" id="BatchNumTxt" class="form-control required" >
                            </div>
                            <div class="form-group form-group-default required ">
                                <label>Claim Set ID</label>
                                <input type="text" runat="server" id="BoxNumTxt" class="form-control required" >
                            </div> 
                            <div align="center" class="row">
                                <button id="SubmitBtn" runat="server" onserverclick="AddBatchBtn_ServerClick" class="btn btn-primary btn-cons m-b-10" type="button">
                                    <i class="pg-form"></i><span class="bold">Submit</span>
                                </button>
                            </div>

                             <div id="divsearch" runat="server" align="center" class="row" >
                               OR.. Import Excel Sheet.
                              <br />
                              <div id="Div2" runat="server" align="left" class="alert alert-success alert-dismissable " style="font-size:17px"  >
                              Please Choose Excel File :-
                            </div> 
                              <asp:FileUpload ID="FileUpload1"  runat="server" class="m-b-20"  />       
                                 
                                <button id="Button1" runat="server" onserverclick="btnUpload_Click" class="btn btn-primary btn-cons m-b-20" type="button" value="Import Excel File">Import Excel File</button>
                            </div>
                        
                    </div>
                </div>
            </div>
        </div>
<%--    <div class="row">
        <div class="col-md-12">
       <div class="widget">
            <div class="widget-content padding">
                <div align="center">
                    <hr />
                    	<div class="table-responsive" style="overflow:auto">
				     		<table id="example" data-sortable  class="table table-detailed;">
		                        <thead>
                        <tr>
                            <td>Box Number</td>
                            <td>Batch Number</td>
                            <td>Claim Code</td>
                            <td>SetID</td>
                            <td>Added By</td>
                            <td>Number Of Claims</td>
                        </tr>
                    </thead>
                   <tbody>
                      <asp:ListView runat="server" ID="lstNewReq">
                            <ItemTemplate>
                                <tr>
                                               <td><strong><%#Eval("BoxID")%></strong></td>
                                               <td><strong><%#Eval("BatchID")%></strong></td>
                                               <td><strong><%#Eval("claimcode")%></strong></td>
                                               <td><strong><%#Eval("setid")%></strong></td>
                                               <td><strong><%#Eval("AddedBy")%></strong></td>
                                               <td><strong><%#Eval("NumOfClaims")%></strong></td>
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
    </div>--%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
        <script src="../assets/plugins/jquery-datatable/media/js/jquery.dataTables.min.js" type="text/javascript"></script>
<script src="../assets/plugins/jquery-datatable/extensions/TableTools/js/dataTables.tableTools.min.js" type="text/javascript"></script>
<script src="../assets/plugins/jquery-datatable/extensions/Bootstrap/jquery-datatable-bootstrap.js" type="text/javascript"></script>
<script type="text/javascript" src="../assets/plugins/datatables-responsive/js/datatables.responsive.js"></script>
<script type="text/javascript" src="../assets/plugins/datatables-responsive/js/lodash.min.js"></script>
    <script src="../assets/js/datatables.js" type="text/javascript"></script>
        <script src="js/jquery.js"></script>
    <script src="js/jquery.dataTables.js"></script>
    <script>
        $(document).ready(function () {
            $('#example').DataTable();
        });
    </script>
</asp:Content>
