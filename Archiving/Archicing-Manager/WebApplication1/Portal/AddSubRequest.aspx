<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AddSubRequest.aspx.cs" Inherits="WebApplication1.Portal.AddSubRequest" %>
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
                    <li><a href="#" class="active">New Submission Request</a>
                    </li>
                </ul>
                <div class="alert alert-success alert-dismissable" runat="server" id="div_success">
                  <strong>Submission Request Added Successfully</strong> 
                </div>
                <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
                  <strong> Error Operation...(Please Note That (Request Header) AND (Number Of Submitted Batches) are Mandatory). </strong>
                </div>
                 <div class="alert alert-success alert-dismissable" runat="server" id="div1">
                   <h5>Please Make sure of Adding Submitted Batches Numbers in Request Subject .</h5> 
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
                            Add New Submissions Request
                        </div>
                    </div>
                    <div class="panel-body">
                        <h5>Submissions Request Details
                        </h5>
                        <form class="" role="form">
                            <div class="form-group form-group-default required ">
                                <label>Header</label>
                                <input type="text" runat="server" id="input_text" class="form-control" required="required">
                            </div>
                            <div class="row">
                                <div class="container-fluid container-fixed-lg">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <div class="panel-title">
                                                Subject
                                            </div>
                                        </div>
                                        <div class="panel-body no-scroll ">
                                            <div class="wysiwyg5-wrapper b-a b-grey">
                                                <textarea id="txtrBody" runat="server" class="wysiwyg demo-form-wysiwyg" placeholder="Enter text ..."></textarea>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Number Of Batches</label>
                                    <input type="text" runat="server" id="NumberOfBatches" required="required">
                                </div>
                                <br />
                                </div>
                            <br />
                            <div class="row">
                                <div class="form-group col-md-2">
                                <label class="col-sm-5 control-label">Choose Type Of Batches:</label>
                                </div>
                                <div class="form-group col-md-3">
                                <asp:DropDownList ID="DropDownList1" runat="server">
                                 <asp:ListItem>---</asp:ListItem>
                               <asp:ListItem>OutPatient</asp:ListItem>
                               <asp:ListItem>InPatient</asp:ListItem>
                               </asp:DropDownList> 
                                </div>
                            </div>
                             <br />
                              <div class="row">
                                <div class="form-group col-md-2">
                                <label class="col-sm-5 control-label">Month:</label>
                                </div>
                                <div class="form-group col-md-3">
                                <asp:DropDownList ID="DropDownList2" runat="server">
                               <asp:ListItem>---</asp:ListItem>
                               <asp:ListItem>01</asp:ListItem>
                               <asp:ListItem>02</asp:ListItem>
                               <asp:ListItem>03</asp:ListItem>
                               <asp:ListItem>04</asp:ListItem>
                               <asp:ListItem>05</asp:ListItem>
                               <asp:ListItem>06</asp:ListItem>
                               <asp:ListItem>07</asp:ListItem>
                               <asp:ListItem>08</asp:ListItem>
                               <asp:ListItem>09</asp:ListItem>
                               <asp:ListItem>10</asp:ListItem>
                               <asp:ListItem>11</asp:ListItem>
                               <asp:ListItem>12</asp:ListItem>
                               </asp:DropDownList> 
                               </div>
                            </div>
                             <br />
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Year:</label>
                                    <input type="text" runat="server" id="txtyear" required="required">
                                </div>
                                </div>
                            <br />
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Attached File</label>
                                    <div class="col-sm-10">
                                        <asp:FileUpload ID="FileUpload1" runat="server" class="btn btn-success btn-rounded"  />
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row" align="center">
                                <div class="form-group">
                                    <asp:Label runat="server" ID="myLabel" Font-Size="Large" CssClass="text-center text-danger" />
                                </div>
                            </div>
                            <div align="center" class="row">
                                <button id="SubmitBtn" runat="server" onserverclick="SubmitBtn_ServerClick" class="btn btn-primary btn-cons m-b-10" type="button">
                                    <i class="pg-form"></i><span class="bold">Submit</span>
                                </button>
                            </div>
                            <div id="ErrorMsg">

                            </div>
                        </form>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
