<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Site2.Master" AutoEventWireup="true" CodeBehind="AllClients.aspx.cs" Inherits="CardCycle.AllClients" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="row">
                <div class="col-md-12" align="right">
                    <a href="#" class="btn btn-info" runat="server" onserverclick="Refresh"><i class="fa fa-refresh fa-2x" style="right: 0"></i></a>
                </div>
            </div>
            <br />
            <div class="widget">
                <div class="widget-content padding">
                    <div align="center">
                        <h2>All Clients</h2>
                        <hr />

                        <asp:Label ID="Label1" runat="server" Text="Client" Font-Bold="true"></asp:Label>
                        <asp:TextBox ID="PName" runat="server"></asp:TextBox>
                        <a class="btn btn-success" runat="server" onserverclick="btn_Search_ServerClick" id="btn_search"><i class="fa fa-check"></i>Get Client Details</a>

                        <div class="table-responsive" style="overflow: auto">
                            <table data-sortable class="table">
                                <thead>
                                    <tr align="center">
                                        <th>No</th>
                                        <th>Client Name</th>
                                        <th>Client Type</th>
                                        <th data-sortable="false">Option</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:ListView runat="server" ID="ClientsList">
                                        <ItemTemplate>
                                            <tr align="center">
                                                <td><%#Eval("id")%></td>
                                                <td><strong><%#Eval("ClientName")%></strong></td>
                                                <td><strong><%#Eval("Type")%></strong></td>
                                                <td>
                                                    <div class="btn-group btn-group-xs">
                                                        <a data-toggle="tooltip" href="ManageClient.aspx?id=<%#Eval("id")%>&ClientName=<%#Eval("ClientName") %>" title="Edit" class="btn btn-default"><i class="fa fa-edit"></i></a>
                                                    </div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <tr>
                                        <td>
                                            <asp:DataPager ID="DataPager1" runat="server" class="pagination" PagedControlID="ClientsList" QueryStringField="id" PageSize="15">
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
