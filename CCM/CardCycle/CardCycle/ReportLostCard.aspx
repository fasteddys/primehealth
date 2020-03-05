<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportLostCard.aspx.cs" Inherits="CardCycle.ReportLostCard" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="assets/libs/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
        <link href="assets/libs/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
        <link href="assets/libs/fontello/css/fontello.css" rel="stylesheet" />
        <link href="assets/libs/animate-css/animate.min.css" rel="stylesheet" />
        <link href="assets/libs/nifty-modal/css/component.css" rel="stylesheet" />
        <link href="assets/libs/magnific-popup/magnific-popup.css" rel="stylesheet" /> 
        <link href="assets/libs/ios7-switch/ios7-switch.css" rel="stylesheet" /> 
        <link href="assets/libs/pace/pace.css" rel="stylesheet" />
        <link href="assets/libs/sortable/sortable-theme-bootstrap.css" rel="stylesheet" />
    <script>
        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }
    </script>
</head>
<body>
    
    <form id="form1" runat="server">
    <div align="center" id="PrintDiv">
        <h1  align="center">Lost Card Report</h1>
    <table id="datatables-1" align="center" class="table table-striped table-bordered" cellspacing="0" width="100%">

							
									 
									     <tbody>
									            <tr>
									                <td><strong>no</strong></td>
									                <td><strong>subject</strong></td>
									                <td><strong>Type</strong></td> 
                                                    <td><strong>Date</strong></td>                    
									            </tr>
                                                <asp:ListView runat="server" ID="lstNewReq">
                                                <ItemTemplate>
                                                <tr>
												<td><%#Eval("id")%></td>
                                                <td><%#Eval("rSubject")%></td>
                                                <td><%#Eval("rType") %></td>
                                                <td><%#Eval("rdate") %></td>                                              
											</tr>
                                            </ItemTemplate>
                                            </asp:ListView>	
                                             <tr style="border:solid 1px #000000">
									                <td ><strong>Total Requests</strong></td>
									                <td colspan="4" align="left" ><b >
                                                        <asp:Label ID="Label1" runat="server" style="font-size:20px;font-weight:bold;color:red" Text=""></asp:Label></b></td>
                                              
									                
									            </tr>
                                
									    </table>
        <table align="center">
                         <tr>
                                                 <td><asp:DataPager ID="DataPager1" runat="server" class="pagination" PagedControlID="lstNewReq" QueryStringField="id" PageSize="15">
                                                        <Fields>
                                                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                                            <asp:NumericPagerField />
                                                            <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                                        </Fields>
                                                    </asp:DataPager></td>
                                             </tr>
        </table>
    </div>
        <div class="row">
                 <div class="col-md-12" align="right">
                     <a class="btn btn-danger" onclick="printDiv('PrintDiv')" style="border-radius:0px 0px; " id="A1" ><i class="fa fa-print fa-2x"></i></a>
                 </div>
             </div>
    </form>
</body>
</html>
