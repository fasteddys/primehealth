<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPages/MasterPage.Master" AutoEventWireup="true" CodeBehind="ShowOnlineApprovalsRequestByStatus.aspx.cs" Inherits="CallCenterNotesApp.Portal.ShowOnlineApprovalsRequestByStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
    <link href="../assets/css/jquery.dataTables.css" rel="stylesheet" />
    <div class="row">
        <div class="col-md-12">
            <ul class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home </a></li>
            </ul>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
    <section class="content">
<%--        <form runat="server">--%>
            <div class="row">
                <div class="col-xs-12">
                    <div class="panel">
                        <header class="panel-heading">
                            All Email Requests
                        </header>
                      <%--  <div class="row" style="margin: 5px">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" Text="Member Name"></asp:Label>
                                    <asp:TextBox ID="MemberNametxt" CssClass="form-control input-sm " runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label12" runat="server" Text="Client Name"></asp:Label>
                                    <asp:TextBox ID="ClientName" runat="server" CssClass="form-control input-sm "></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="Label9" runat="server" Text="Claim Number"></asp:Label>
                                    <asp:TextBox ID="ClaimNumber" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="label" runat="server" Text="Diagnose" ></asp:Label>
                                    <asp:TextBox ID="Diagnose" runat="server" CssClass="form-control input-sm "></asp:TextBox>
                                </div>
                            </div>
                          
                      
                           
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label8" runat="server" Text="Status"></asp:Label>
                                    <asp:DropDownList ID="Status" runat="server" DataValueField="ID" CssClass="form-control input-sm ">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label4" runat="server" Text="Type"></asp:Label>
                                    <asp:DropDownList ID="Type" runat="server" DataValueField="ID" CssClass="form-control input-sm ">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label3" runat="server" Text="IVR Number"></asp:Label>
                                    <asp:TextBox ID="IVRNumber" runat="server" CssClass="form-control input-sm "></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" Text="Drug Name"></asp:Label>
                                    <asp:TextBox ID="DrugName" runat="server" CssClass="form-control input-sm "></asp:TextBox>
                                </div>
                            </div>

                           
                          
                         
                        
                             <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label5" runat="server" Text="Creation From"></asp:Label>
                                    <input type="date" runat="server" id="RequestCreationTime" class="form-control input-sm " />
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label6" runat="server" Text="Creation To"></asp:Label>
                                    <input type="date" runat="server" id="CloseTime" class="form-control input-sm " /><br />
                                </div>
                            </div>
                        </div>
                        <div style="margin-left: 40%; margin-top: 25px; margin-bottom: 25px">
                            <asp:Button ID="Search" runat="server" OnClick="Search_Click" Text="Search With Filters" CssClass="btn btn-primary" />

                        </div>--%>

                        <div class="panel-body table-responsive">
                            <table class="table " id="example">
                                <thead>
                                    <tr style="font-weight: bold; color: darkblue">
                                        <th style='text-align: center;'>ID</th>
                                        <th style='text-align: center;'>Medical ID</th>
                                        <th style='text-align: center;'>IVRNumber</th>
                                        <th style='text-align: center;'>CallCenterAssignee</th>
                                        <th style='text-align: center;'>ClaimDate</th>
                                        <th style='text-align: center;'>ClientName</th>
                                        <th style='text-align: center;'>CoInsurancePercentage</th>
                                        <th style='text-align: center;'>Diagnose</th>
                                        <th style='text-align: center;'>Action</th>

                           
                                    </tr>
                                </thead>
                                <tbody style='text-align: center; vertical-align: middle'>
                                    <asp:ListView runat="server" ID="lstNewReq">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("RequestID") %></td>

                                                <td><%#Eval("MedicalID") %></td>
                                                <td><strong><%#Eval("IVRNumber") %></strong></td>
                                                <td><strong><%#Eval("CallCenterAssignee") %></strong></td>
                                                <td><strong><%#Eval("ClaimDate") %></strong></td>
                                                <td><strong><%#Eval("ClaimNumber") %></strong></td>
                                                <td><strong><%#Eval("ClientName") %></strong></td>
                                                <td><strong><%#Eval("CloseTime") %></strong></td>
<%--                                                <td><strong><%#Eval("CoInsurancePercentage") %></strong></td>--%>


                                                <td>
                                                    <div class="btn-group btn-group-xs">
                                                        <a data-toggle="tooltip" href="OnlineApprovalManageRequest.aspx?id=<%#Eval("RequestID")%>" title="Edit" class="btn btn-default"><i class="fa fa-edit"></i></a>
                                                    </div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </tbody>
                            </table>
                            <br />
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
            </div>
            <asp:Panel ID="Panel2" runat="server">
                <%-- <asp:Button ID="Next" runat="server" Text="Next" OnClick="Next_Click" /> 
                            <asp:Button ID="Befor" runat="server" Text="Before" OnClick="Befor_Click" />--%>
            </asp:Panel>
<%--        </form>--%>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
    <script src="../assets/js/jquery.min.js"></script>
    <script src="../assets/js/jquery.js"></script>
    <script src="../assets/js/jquery.dataTables.js"></script>
    <script>
        $(document).ready(function () {
            $('#example').DataTable({
                "order": [[0, "desc"]]
            });
        })
    </script>

</asp:Content>
