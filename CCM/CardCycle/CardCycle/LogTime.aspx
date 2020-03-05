<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Site2.Master"  AutoEventWireup="true" CodeBehind="LogTime.aspx.cs" Inherits="CardCycle.LogTime" %>
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
                        <h2>Logtime Tabel</h2>
                        <hr />
                        <div class="table-responsive" style="overflow: scroll">
                            <table data-sortable class="table">
                                <thead>
                                    <tr>

                                        <th>id</th>
                                        <th>Subject</th>
                                        <th>Client Name</th>
                                        <th>Issued</th>
                                        <th>Confirm AM</th>
                                        <th>Confrim UW</th>
                                        <th>Printed</th>
                                        <th>Q C</th>
                                        <th>Status </th>
                                        <th>Closed Time</th>

                                    </tr>
                                </thead>

                                <tbody>
                                    <asp:ListView runat="server" ID="lstNewReq">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("id") %></td>
                                                <td><%#Eval("rSubject") %></td>
                                                <td><%#Eval("ClientName") %></td>
                                                <td><%#Eval("LogIssuance") %></td>
                                                <td><%#Eval("LogAM") %></td>
                                                <td><%#Eval("LogUW") %></td>
                                                <td><%#Eval("LogPrint") %></td>
                                                <td><%#Eval("LogQC") %></td>
                                                <td><span class="label label-danger" style="background: <%#Eval("color")%>"><%#Eval("States") %> </span></td>
                                                <td><%#Eval("isApproved") %></td>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="20000" />
    </div>
</asp:Content>
