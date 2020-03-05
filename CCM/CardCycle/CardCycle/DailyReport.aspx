<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Site2.Master" AutoEventWireup="true" CodeBehind="DailyReport.aspx.cs" Inherits="CardCycle.DailyReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="js/jquery-ui.js"></script>
    <div class="row">
        <div class="col-md-12">
        <div class="widget">
            <div class="widget-content pading">
                <div align="center"><h2>Reportes</h2> </div>
                <hr />
                            
                <!-- item ---->
                <div class="row">
						  <div class="form-group">
							<label for="input-text" class="col-sm-2 control-label">Enter Date <br /> (dd/mm/yyyy)</label> FROM<div class="col-sm-10">
                               <input type="text" class="form-control  datepicker" runat="server" id="txtDate" placeholder=""><asp:RequiredFieldValidator  ValidationGroup="first" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate" ErrorMessage="please Enter a Valid Date" ForeColor="Red" OnDataBinding="btn_reject_ServerClick1"></asp:RequiredFieldValidator>
&nbsp;<div class="col-sm-10"> TO <asp:TextBox class="form-control datepicker" ID="TextBox1" runat="server"></asp:TextBox>
                                      <asp:RequiredFieldValidator ValidationGroup="first" ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox1" ErrorMessage="please Enter a Valid Date" ForeColor="Red" OnDataBinding="btn_reject_ServerClick1"></asp:RequiredFieldValidator>
                                  </div> </div>
						  </div>
                            </div>
                 <!--End item ---->
                <br />

                <!-- item ---->
                 <div class="row">
                    <div class="form-group">
                    <label for="input-text" class="col-sm-2 control-label">Select Status</label>
							<div class="col-sm-10">
                               <asp:DropDownList ID="DropDownList_stats" runat="server" Font-Bold="True" Font-Size="Small" Height="14px" Width="100%" style="height:30px">
                                </asp:DropDownList> 
							</div>
						  </div>
                </div>
                <!--- End item---->
                <br />
                                <!-- item ---->
                <div class="row">
						  <div class="form-group">
							<label for="input-text" class="col-sm-2 control-label">Select User</label>
							<div class="col-sm-10">
                               <asp:DropDownList ID="DropDownList2" runat="server" Font-Bold="True" Font-Size="Small" Height="14px" Width="100%" style="height:30px">
                                </asp:DropDownList> 
							</div>
						  </div>
                            </div>
                 <!--End item ---->
           <br />
                <!-- Button -->
                <div class="row">
                                <div class="col-md-2">

                                </div>
                                <div class="col-md-12" align="Center">
                                    <asp:LinkButton ID="LinkButton1" ValidationGroup="first" runat="server" OnClick="btn_reject_ServerClick1" CssClass="btn btn-danger button_report"><i class="glyphicon glyphicon-file"></i> Show&nbsp; Report</asp:LinkButton>
                                    <%--<a class="btn btn-danger" runat="server" style="border-radius:0px 0px;width:300px; " id="btn_reject" onserverclick="btn_reject_ServerClick1"><i class="glyphicon glyphicon-file"></i> Show&nbsp; Report</a>--%>
                                </div>
							</div>
                <style>

                    .button_report {border-radius:0px 0px;width:300px;
                    }
                </style>
                <!-- End Button -->
                <br />
                  
                <div class="row">
                    <div class="col-md-12" align="Center">
                        <%--<table data-sortable class="table">
										<thead>
											<tr>
												<th>No</th>
                                                
												<th>Subject</th>
                                                <th>Type</th>											
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
                                                <td><%#Eval("rType") %></td>                                              
                                                <td><span class="label label-info" style="background:<%#Eval("color")%>" > <%#Eval("States") %> </span></td>
												
												<td>
													<div class="btn-group btn-group-xs">
													  <a data-toggle="tooltip" href="ManageCycle.aspx?id=<%#Eval("id")%>&state=<%#Eval("States") %>" title="View" class="btn btn-default"><i class="fa fa-times"></i></a>
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
									</table>--%>
                        <div id="PrintDiv">
                        <table id="datatables-1" class="table table-striped table-bordered" cellspacing="0" width="100%">

							
									 
									     <tbody>
									            <tr>
									                <td><strong>no</strong></td>
									                <td>Subject</td>
									                <td>Clien tName</td>
									                <td><strong>Type</strong></td>
                                                    <td>States </td>
                                                    <td>
                                                        Time
                                                    </td> 
									            </tr>
                                                <asp:ListView runat="server" ID="lstNewReq">
                                                <ItemTemplate>
                                                <tr>
												<td><%#Eval("id")%></td>
                                                <td><strong><%#Eval("rSubject")%></strong></td>
                                                <td><strong><%#Eval("ClientName") %></strong></td>
                                                <td><%#Eval("rType") %></td>                                              
                                                <td><span class="label label-info" style="background:<%#Eval("color")%>" > <%#Eval("States") %> </span></td>
												
												<td>
													<div class="btn-group btn-group-xs">
													  <a data-toggle="tooltip" href="TimeDetails.aspx?id=<%#Eval("id")%>&type=<%#Eval("rType") %>" title="View" class="btn btn-default"><i class="fa fa-clock-o"></i></a>
													</div>
												</td>
											</tr>
                                            </ItemTemplate>
                                            </asp:ListView>	
                                             <tr>
									                <td><strong>Total</strong></td>
									                <td colspan="4" aliggn="Right"><b >
                                                        <asp:Label ID="Label1" runat="server" style="font-size:20px;font-weight:bold;color:red" Text=""></asp:Label></b></td>
                                              
									                
									            </tr>
									    </table>
                            </div>
                    </div>
                </div>
                <br>

                 <hr />
                <div class="col-md-12" align="right">    <a class="btn btn-danger" onclick="printDiv('PrintDiv')" style="border-radius:0px 0px; " id="A1" ><i class="fa fa-print fa-2x"></i></a> </div>
                  <br />    
                <!-- item ---->
                <div class="row">
						  <div class="form-group">
							<label for="input-text" class="col-sm-2 control-label">Enter Date <br /> (dd/mm/yyyy)</label> FROM<div class="col-sm-10">
                               <input type="text" class="form-control datepicker" runat="server" id="Text1" placeholder="">
                                <asp:RequiredFieldValidator ValidationGroup="second" ID="RequiredFieldValidator3" runat="server" ControlToValidate="Text1" ErrorMessage="please Enter a Valid Date" ForeColor="Red" OnDataBinding="btn_reject_ServerClick1"></asp:RequiredFieldValidator>
&nbsp;<div class="col-sm-10"> TO <asp:TextBox class="form-control datepicker" ID="TextBox2" runat="server"></asp:TextBox>
                                      <asp:RequiredFieldValidator ValidationGroup="second" ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox2" ErrorMessage="please Enter a Valid Date" ForeColor="Red" OnDataBinding="btn_reject_ServerClick1"></asp:RequiredFieldValidator>
                                  </div> </div>
						  </div>
                            </div>
                 <!--End item ---->
                <br />
                  <div class="row">
                    <div class="form-group">
                    <label for="input-text" class="col-sm-2 control-label">Select Team</label>
							<div class="col-sm-10">
                               <asp:DropDownList ID="DropDownList_team" runat="server" Font-Bold="True" Font-Size="Small" Height="14px" Width="100%" style="height:30px">
                                </asp:DropDownList> 
							</div>
						  </div>
                </div>
                <br />
                <!--begin button-->
                 <div class="row">
                                <div class="col-md-2">

                                </div>
                                <div class="col-md-12" align="Center">
<%--                                    <a class="btn btn-danger" runat="server" style="border-radius:0px 0px;width:300px; " id="A3" onserverclick="btn_reject_ServerClick2"><i class="glyphicon glyphicon-file"></i> Report</a>--%>
<asp:LinkButton ID="LinkButton2" ValidationGroup="second" runat="server" OnClick="btn_reject_ServerClick2" CssClass="btn btn-danger button_report"><i class="glyphicon glyphicon-file"></i> Report</asp:LinkButton>

                                    </div>
							</div>
                <!-- End Button -->
                <br />

                <!-- new table for this report-->

                  <div id="PrintDiv2" ,class="col-md-12" align="Center">
                        <table id="datatables-2" class="table table-striped table-bordered" cellspacing="0" width="100%",cellpadding ="10">

							
									 
									     <tbody>
									            <tr>
									                
									                <td>ApvQcontrol</td>
                                                    <td>QControl Tickets</td>
                                                     <td>Total num_of QCCards</td>
                                                    <td>ApvIssuance</td>
                                                    <td>Issuance Tickets</td>
									                <td>Total num_of ISSCards</td>
                                                  
									               
									            </tr>

                                                <asp:ListView runat="server" ID="ListView1">

                                                <ItemTemplate>
                                                <tr>

												
                                                  
                                                <td><strong><%#Eval("apvQControl")%></strong></td>
                                                <td><strong><%#Eval("LogQC")%></strong></td>
                                                <td><strong><%#Eval("CardsNum") %></strong></td>
                                                <td><strong><%#Eval("apvIssuance")%></strong></td>
                                                <td><strong><%#Eval("LogIssuance")%></strong></td>
                                                <td><strong><%#Eval("IssCardsNum")%></strong></td>
                                                                                      
                                               
											</tr>
                                            </ItemTemplate>
                                            </asp:ListView>	
                                            
									    </table>
                            </div>

                <!-- end of the table-->
                <br />
                <div  class="col-md-12" align="right"> <a class="btn btn-danger" onclick="printDiv('PrintDiv2')" style="border-radius:0px 0px; " id="A4" ><i class="fa fa-print fa-2x"></i></a></div> 

                 <br />
                <hr/>
                <br />

                  <!-- item ---->
                <div class="row">
						  <div class="form-group">
							<label for="input-text" class="col-sm-2 control-label">Enter Date <br /> (dd/mm/yyyy)</label> FROM<div class="col-sm-10">
                               <input type="text" class="form-control datepicker" runat="server" id="Text2" placeholder="">
                                <asp:RequiredFieldValidator  ValidationGroup="third" ID="RequiredFieldValidator5" runat="server" ControlToValidate="Text2" ErrorMessage="please Enter a Valid Date" ForeColor="Red" OnDataBinding="btn_reject_ServerClick1"></asp:RequiredFieldValidator>
&nbsp;<div class="col-sm-10"> TO <asp:TextBox class="form-control datepicker" ID="TextBox3" runat="server"></asp:TextBox>
                                      <asp:RequiredFieldValidator ValidationGroup="third" ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBox3" ErrorMessage="please Enter a Valid Date" ForeColor="Red" OnDataBinding="btn_reject_ServerClick1"></asp:RequiredFieldValidator>
                                  </div> </div>
						  </div>
                            </div>
                 <!--End item ---->
                <br />


                <!--begin button-->
                 <div class="row">
                                <div class="col-md-2">

                                </div>
                                <div class="col-md-12" align="Center">
<%--                                    <a class="btn btn-danger" runat="server" style="border-radius:0px 0px;width:300px; " id="A5" onserverclick="btn_reject_ServerClick3"><i class="glyphicon glyphicon-file"></i> Cards&Booklets Report</a>--%>
<asp:LinkButton ID="LinkButton3" ValidationGroup="third" runat="server" OnClick="btn_reject_ServerClick3" CssClass="btn btn-danger button_report"><i class="glyphicon glyphicon-file"></i> Cards&Booklets Report</asp:LinkButton>
 </div>
							</div>
                <!-- End Button -->
                <br />

                <!-- new table for this report-->

                  <div id="PrintDiv3" ,class="col-md-12" align="Center">
                        <table id="datatables-2" class="table table-striped table-bordered" cellspacing="0" width="100%",cellpadding ="10">

							
									 
									     <tbody>
									            <tr>
									               
                                                     <td>Total num_of QCCards</td>
                                                     <td>Total num_of ISSCards</td>
                                                    <td>Total num_of Booklets</td>
                                                    
									               
                                                  
									               
									            </tr>

                                                <asp:ListView runat="server" ID="ListView2">

                                                <ItemTemplate>
                                                <tr>

												
                                                  
                                               
                                                <td><strong><%#Eval("CardsNum") %></strong></td>
                                               
                                                
                                                <td><strong><%#Eval("IssCardsNum")%></strong></td>
                                                <td><strong><%#Eval("NumOfBooklets")%></strong></td>
                                                                                      
                                               
											</tr>
                                            </ItemTemplate>
                                            </asp:ListView>	
                                            
									    </table>
                            </div>
             <div class="row">
                 <div class="col-md-12" align="right">
                  
                     <a class="btn btn-danger" onclick="printDiv('PrintDiv3')" style="border-radius:0px 0px; " id="A6" ><i class="fa fa-print fa-2x"></i></a>
                     <a class="btn btn-success" href="DailyReport.aspx"  style="border-radius:0px 0px; " id="A2" ><i class="fa fa-file-archive-o fa-2x"></i> Report</a>
                 </div>
             </div>
                
                </div>
            </div>
            </div>
     </div>
    <script>
        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }
        //$('.datepicker').datepicker({});

        
    </script>
</asp:Content>
  

