<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Site2.Master"  AutoEventWireup="true" CodeBehind="NewRequests.aspx.cs" Inherits="CardCycle.NewRequests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="row" >
                <div class="col-md-12" align="right">
                 <a href="#" class="btn btn-info" runat="server" onserverclick="Refresh"><i class="fa fa-refresh fa-2x" style="right:0"></i></a>
                    </div>
            </div>
            <br />
             <div class="widget">
            <div class="widget-content padding">
                <div align="center">
                    <h2>Issuing Requests</h2>
                    <hr />
                    	<div class="table-responsive" style="overflow: scroll">
				     		<table data-sortable class="table">
										<thead>
											<tr>
												<th>No</th>
										        <th>Subject</th>
                                                <th>Client Name</th>
                                                <th>Type</th>
                                                <th>No.Members</th>
												<th>Account Manager</th>
												<th>Received</th>
                                                <th>Assigned to</th>
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
                                                <td><span class="label label-warning"> <%#Eval("States") %> </span></td>
												
												<td>
													<div class="btn-group btn-group-xs">
													  <a data-toggle="tooltip" href="ManageCycle.aspx?id=<%#Eval("id")%>&state=<%#Eval("States") %>" title="Edit" class="btn btn-default"><i class="fa fa-edit"></i></a>
                                                      <a data-toggle="tooltip" href="SearchDetails.aspx?id=<%#Eval("id")%>&state=<%#Eval("States") %>" title="Search" class="btn btn-default"><i class="fa fa-search"></i></a>
													</div>
												</td>
											</tr>
                                            </ItemTemplate>
                                            </asp:ListView>	
                                                                                          <tr>
                                                <td>
                                                     <asp:DataPager ID="DataPager1" runat="server" class="pagination" PagedControlID="lstNewReq" QueryStringField="id" PageSize="15">
                                                        <Fields>
                                                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                                            <asp:NumericPagerField />
                                                            <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                                        </Fields>
                                                    </asp:DataPager>
                                                </td>
                                            </tr>
										</tbody>
									</table>
						</div>
                </div>
                </div>
                 </div>
        </div>
    </div>
     <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="20000" />
            
</asp:Content>
