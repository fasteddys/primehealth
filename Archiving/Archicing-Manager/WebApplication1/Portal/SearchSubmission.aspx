<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="SearchSubmission.aspx.cs" Inherits="WebApplication1.Portal.SearchSubmission" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

           <div class="jumbotron" data-pages="parallax" >
        <div class="container-fluid container-fixed-lg sm-p-l-20 sm-p-r-20" >
            <div class="inner">
                <ul class="breadcrumb">
                    <li>
                        <p>DashBoard</p>
                    </li>
                    <li><a href="#" class="active">Search Submission Claims</a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <br />
     <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
                    Error Operation...Please Review Search Parameters...
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
     <asp:ListItem>ID</asp:ListItem>
     <asp:ListItem>Subject</asp:ListItem>
     <asp:ListItem>User Comments</asp:ListItem>
     <asp:ListItem>Creator</asp:ListItem>
     <asp:ListItem>Assigned Person</asp:ListItem>
     <asp:ListItem>Archiving Comments</asp:ListItem>
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
    <div class="row">
        <div class="col-md-12">
       <div class="widget">
            <div class="widget-content padding">
                <div align="center">
                    <hr />
                    	<div class="table-responsive" style="overflow:auto">
				     		<table id="example" data-sortable class="table table-detailed ">
		                        <thead>
                        <tr>
                            <th>ID</th>
                            <th>Subject</th>
                            <th>Creator</th>
                            <th>Received</th>
                            <th>Status</th>
                            <th>Assigned To</th>
                            <th>Accepted At</th>
                            <th>Finished At</th>
                            <th>Closed At</th>
                            <th>Submitted Batches</th>
                            <th>Received Batches</th>
                            <td data-sortable="false">Actions</td>
                        </tr>
                    </thead>
                   <tbody>
                      <asp:ListView runat="server" ID="lstNewReq">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("id")%></td>
                                    <td><strong><%#Eval("rSubject").ToString().Substring(0,Math.Min(50,Eval("rSubject").ToString().Length))%></strong></td>
                                    <td><b><%#Eval("rFrom") %></b></td>
                                    <td><%#Eval("rDate") %></td>
                                    <td><span class="label label-info" style="background: <%#Eval("rStatusColor")%>"><%#Eval("rStatus") %> </span></td>
                                    <td><%#Eval("AssigenPerson") %></td>
                                     <td><%#Eval("AssignedTime") %></td>
                                     <td><%#Eval("FinishedArchivingDate") %></td>
                                     <td><%#Eval("ApprovedDate") %></td>
                                     <td style="text-align:center"><%#Eval("NumberSentOfBatches") %></td>
                                     <td style="text-align:center"><%#Eval("NumberSentOfRecBatches") %></td>
                                    <td>
                                        <a href="ManageSubRequest.aspx?id=<%#Eval("id")%>" title="Edit" class="btn btn-default"><i class="fa fa-edit"></i></a>
                                    </td>
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
