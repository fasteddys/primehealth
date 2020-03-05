<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="InPatientmanger.aspx.cs" Inherits="WebApplication1.Portal.InPatientmanger" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
 <link href="../assets/plugins/jquery-datatable/media/css/jquery.dataTables.css" rel="stylesheet" type="text/css"/>
<link href="../assets/plugins/jquery-datatable/extensions/FixedColumns/css/dataTables.fixedColumns.min.css" rel="stylesheet" type="text/css"/>
<link href="../assets/plugins/datatables-responsive/css/datatables.responsive.css" rel="stylesheet" type="text/css" media="screen"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="jumbotron" data-pages="parallax">
        <div class="container-fluid container-fixed-lg sm-p-l-20 sm-p-r-20">
            <div class="inner">
                <ul class="breadcrumb">
                    <li>
                        <p><a href="AllRequests.aspx">DashBoard</a></p>
                    </li>
                    <li>
                        InPatient<a href="InPatientmanger.aspx">Admin</a>
                    </li>
                    <li>
                        <a href="#" class="active">Manage</a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
   <br />
        <asp:Panel runat="server" DefaultButton="btnSSN">

                  <div id="Div2" style="position:absolute" runat="server" >
                              <strong>  Choose Search Parameters :-</strong> 
                            </div> 
   <br />
   <br />
    <div class="container-fluid container-fixed-lg bg-white">
     <asp:DropDownList ID="DropDownList1" runat="server" >
     <asp:ListItem>Batch Number</asp:ListItem>
     <asp:ListItem>Provider Name</asp:ListItem>
     <asp:ListItem>Pre_AuthID</asp:ListItem>
     <asp:ListItem>Set ID</asp:ListItem>
     <asp:ListItem>Box Number</asp:ListItem>
     <asp:ListItem>Added By</asp:ListItem>
    </asp:DropDownList>
      <br /> 
      <br />
           <div>
          <input id="txtsearch" runat="server" type="text" placeholder="Enter Search Parameters..."  >
      </div>
        <br />
        <br />

                 <asp:Button runat="server" ID="btnSSN" OnClick="btnSearch_Click" Text="Submit" />

            </div>

        </asp:Panel>
    <br />
    <div class="row" >
        <div class="col-md-12" runat="server" id="normalsearch">
       <div class="widget">
            <div class="widget-content padding">
                <div align="center">
                    <hr />
                    	<div class="table-responsive" style="overflow:auto" >
				     		<table id="example" data-sortable  class="table table-detailed;">
		                        <thead>
                         <tr>
                            <td>Batch Number</td>
                            <td>Box Number</td>
                            <td>Pre_AuthID</td>
                            <td>SetID</td>
                            <td>Provider Name</td>
                            <td>Added By</td>
                            <td>AddTime</td>
                        </tr>
                    </thead>
                   <tbody>
                      <asp:ListView runat="server" ID="lstNewReq">
                            <ItemTemplate>
                                <tr>
                                               <td><strong><%#Eval("BatchID")%></strong></td>
                                               <td><strong><%#Eval("BoxID")%></strong></td>
                                               <td><strong><%#Eval("Pre_AuthID")%></strong></td>
                                               <td><strong><%#Eval("SetID")%></strong></td>
                                               <td><strong><%#Eval("Provider_Name")%></strong></td>
                                               <td><strong><%#Eval("AddedBy")%></strong></td>
                                               <td><strong><%#Eval("AddTime")%></strong></td>
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
    <div class="row">
        <div class="col-md-12" runat="server" id="adminsearch">
       <div class="widget">
            <div class="widget-content padding">
                <div align="center">
                    <hr />
                    	<div class="table-responsive" style="overflow:auto">
				     		<table id="example" data-sortable class="table table-detailed ">
		                        <thead>
                      <tr align="center">
                            <td>ID</td>
                            <td>Box Number</td>
                            <td>Batch Number</td>
                            <td>Pre_AuthID</td>
                            <td>SetID</td>
                            <td>Provider Name</td>
                            <td>Added By</td>
                            <td>Added Time</td>
                            <td data-sortable="false">Actions</td>
                        </tr>
                    </thead>
                   <tbody>
                            <asp:ListView runat="server" ID="ItemsList" OnItemCanceling="ItemsList_ItemCanceling" OnItemDeleting="ItemsList_ItemDeleting" OnItemEditing="ItemsList_ItemEditing" OnItemUpdating="ItemsList_ItemUpdating">
                            <ItemTemplate>
                                <tr align="center">
                                    <td>
                                    <asp:Label ID="IDTxt" runat="server" Text='<%#Eval("ID")%>' /></td>
                                     <td><strong><%#Eval("BoxID")%></strong></td>
                                               <td><strong><%#Eval("BatchID")%></strong></td>
                                               <td><strong><%#Eval("Pre_AuthID")%></strong></td>
                                               <td><strong><%#Eval("SetID")%></strong></td>
                                               <td><strong><%#Eval("Provider_Name")%></strong></td>
                                               <td><strong><%#Eval("AddedBy")%></strong></td>
                                               <td><strong><%#Eval("AddTime")%></strong></td>
                                    <td >
                                        <div class="btn-group btn-group-xs">
                                            <asp:LinkButton ID="EditBtn" runat="server" CommandName="Edit"><span class="fa fa-edit"></span></asp:LinkButton>
                                            <span><a>|  </a></span>
                                            <asp:LinkButton ID="DeleteBtn" runat="server" CommandName="Delete"><span class="fa red fa-minus-circle"></span></asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <tr align="center">
                                    <td>
                                        <asp:Label ID="EditIDTxt" runat="server" Text='<%#Eval("ID")%>' />
                                    </td>
                                     <td>
                                        <asp:TextBox ID="boxtxt" runat="server" Text=' <%#Eval("BoxID")%>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="BatchNumtxt" runat="server" Text=' <%#Eval("BatchID")%>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="pre_ID" runat="server" Text=' <%#Eval("Pre_AuthID")%>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="SetIDtxt" runat="server" Text=' <%#Eval("SetID")%>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="providerName" runat="server" Text=' <%#Eval("Provider_Name")%>' />
                                    </td>
                                    <td><b><%#Eval("AddedBy") %></b></td>
                                    <td><%#Eval("AddTime") %></td>
                                                                        <td>
                                        <div class="btn-group btn-group-xs">
                                            <asp:LinkButton ID="UpdateBtn" runat="server" CommandName="Update"><span class="fa fa-check-square-o"></span></asp:LinkButton>
                                            <span><a>|  </a></span>
                                            <asp:LinkButton ID="CancelBtn" runat="server" CommandName="Cancel"><span class="fa red fa-remove"></span></asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                            </EditItemTemplate>
                        </asp:ListView>
                    </tbody>
                </table>
            </div>
                </div>
        </div>
    </div>
        </div>
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