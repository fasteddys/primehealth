<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Site2.Master"  AutoEventWireup="true" CodeBehind="SearchForm.aspx.cs" Inherits="CardCycle.SearchForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="css/jquery.dataTables.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="row" >
                
            </div>
            <br />
             <div class="widget">
            <div class="widget-content padding">
                <div align="center">
                    <h2>Search Results</h2>
                    <hr />
                    	<div class="table-responsive">
				     		<table id="example" data-sortable class="table">
										<thead>
											<tr>
												<th>No</th>
                                                <th>Subject</th>
                                                <th>Client Name</th>
                                                <th>Type</th>
                                                <th>No.</th>
												<th>Account Manager</th>
												<th>Received</th>
												<th>By</th>
												<th>Status</th>
												<th data-sortable="false">Option</th>
											</tr>
										</thead>
										
										<tbody>
                                            <asp:ListView runat="server" ID="lstNewReq">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%#Eval("id")%></td>
                                                        <td><strong><%#Eval("rSubject")%></strong></td>
                                                        <td><strong><%#Eval("ClientName")%></strong></td>
                                                        <td><%#Eval("rType") %></td>
                                                        <td><%#Eval("CardsNum") %></td>
                                                        <td><b><%#Eval("rFrom") %></b></td>
                                                        <td><%#Eval("rdate") %></td>
                                                        <td><%#Eval("AssignPerson") %></td>
                                                        <td><span class="label label-info" style="background: <%#Eval("color")%>"><%#Eval("States") %> </span></td>

                                                        <td>
                                                            <div class="btn-group btn-group-xs">
                                                                <a data-toggle="tooltip" href="SearchDetails.aspx?id=<%#Eval("id")%>&state=<%#Eval("States") %>" title="Search" class="btn btn-default"><i class="fa fa-search"></i></a>
                                                                <div class="btn-group btn-group-xs">
                                                                    <a data-toggle="tooltip" href="ManageCycle.aspx?id=<%#Eval("id")%>&state=<%#Eval("States") %>" title="Edit" class="btn btn-default"><i class="fa fa-edit"></i></a>


                                                                </div>
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
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script src="js/jquery.js"></script>
    <script src="js/jquery.dataTables.js"></script>
    <script>
        $(document).ready(function () {
            $('#example').DataTable();
        });
    </script>
</asp:Content>
