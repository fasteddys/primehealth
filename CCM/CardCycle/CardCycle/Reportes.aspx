<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Site2.Master"AutoEventWireup="true"CodeBehind="Reportes.aspx.cs" Inherits="CardCycle.Reportes" %>
 <asp:Content ID= "Content1" ContentPlaceHolderID =  "ContentPlaceHolder1" runat="server">
<!DOCTYPE html>

<html>
<head>
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
    <div>
        <div>
            <asp:CheckBoxList ID="list_of_check_boxs" runat="server" OnLoad="list_of_check_boxs_Load">
                
                <asp:ListItem>  Addition </asp:ListItem>
                <asp:ListItem>Renewal</asp:ListItem>
                <asp:ListItem>Cancellation</asp:ListItem>
                <asp:ListItem>Transfer</asp:ListItem>
                <asp:ListItem>Lost Card</asp:ListItem>
                <asp:ListItem>Modification</asp:ListItem>
                <asp:ListItem>Missing Photo</asp:ListItem>
                <asp:ListItem>reprint card</asp:ListItem>
                <asp:ListItem>Missing Photo</asp:ListItem>
            </asp:CheckBoxList>
        </div>
        <br />
        <asp:Label ID="Label6" runat="server" ForeColor="Red" Font-Size="Large" Text="From"></asp:Label>
        &nbsp;
        <asp:TextBox ID="TextBox1"  runat="server" placeholder="M/d/YYYY"></asp:TextBox>
        <asp:Label ID="Label7" runat="server" ForeColor="Red" Font-Size="Large" Text="To"></asp:Label>

        <asp:TextBox ID="TextBox2" placeholder="M/d/YYYY"  runat="server"></asp:TextBox>
        
     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2" ErrorMessage="date is required " ForeColor="#FF3300"></asp:RequiredFieldValidator>

        &nbsp;&nbsp;&nbsp;

        <asp:Button ID="submit" OnClick="submit_Click" CssClass="btn btn-primary" runat="server" Text="search" />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="date is required " ForeColor="#FF3300"></asp:RequiredFieldValidator>
    </div>
    <%#Eval("id")%>
    <div align="center" id="PrintDiv">
        <h1  align="center">Daily Report</h1>
    <table id="datatables-1" align="center" class="table table-striped table-bordered" cellspacing="0" width="100%">

							
									 
									     <tbody>
									            <tr>
									                <td><strong>no</strong></td>
									                <td><strong>Client Name</strong></td>
									                <td><strong>No.of members</strong></td> 
                                                    <td><strong>request type</strong></td>    
                                                     <td><strong>received request date</strong></td>  
                                                    <td><strong>close Date</strong></td> 
                                                    <td><strong>case</strong></td> 
                                                          
									            </tr>
                                                <asp:ListView runat="server" ID="lstNewReq">
                                                <ItemTemplate>
                                                <tr>
												<td><%#Eval("id")%></td>
                                                <td><%#Eval("ClientName")%></td>
                                                <td><%#Eval("IssCardsNum") %></td>
                                                <td><%#Eval("rType") %></td> 
                                                <td><%#Eval("rdate") %></td>
                                                 <td><%#Eval("isApproved") %></td>
                                                    <td><%#Eval("States") %></td> 
                                               
                                                                                                                                                
											    </tr>
                                            </ItemTemplate>
                                            </asp:ListView>	
                                             <tr style="border:solid 1px #000000">
									                <td ><strong> Total No.of members</strong></td>
									                <td colspan="3" align="left" ><b >
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="Label1" runat="server" style="font-size:20px;font-weight:bold;color:red" Text=""></asp:Label></b></td>
                                              
									                
									            </tr>
                                
									    </table>
        <%#Eval("ClientName")%>

            <asp:Label ID="Label3" Visible="false" runat="server" Text="Label">report prepared by</asp:Label>

             <asp:Label ID="Label2" Visible="false" runat="server" style="font-size:20px;font-weight:bold;color:black" Text="hiiii"></asp:Label></b></td>
        <br />  
         
        <asp:Label ID="Label4" Visible="false" runat="server" Text="DATE :"></asp:Label>
        <asp:Label ID="Label5" runat="server" Visible="False"></asp:Label>
    </div>
    
        <div class="row">
                 <div class="col-md-12" align="right">
                     <a class="btn btn-danger" onclick="printDiv('PrintDiv')" style="border-radius:0px 0px; " id="A1" ><i class="fa fa-print fa-2x"></i></a>
                        <a class="btn btn-success" runat="server" id="ExportExcel" onserverclick="export_to_exel_Click"><i class="fa fa-file-excel-o"></i>Export Excel</a>

                 </div>
             </div>
    
    <script src="assets/js/init.js"></script>

<%#Eval("IssCardsNum") %>
</body>
</html>
<%--     <button id="btnExport" onclick="fnExcelReport();"> EXPORT </button>--%>
<%--     <iframe id="txtArea1" style="display:none"></iframe>--%>

<%--    <asp:Button ID="export_to_exel" runat="server"  class="btn btn-success fa-file-excel-o" Text="<i class='fa fa-print fa-2x'></i>" OnClick="export_to_exel_Click" />--%>
     <script src="assets/libs/jquery/jquery-1.11.1.min.js" type="text/javascript"></script>
<script>
    //function fnExcelReport() {
    //    debugger;
    //    var tab_text = "<table border='2px'><tr bgcolor='#87AFC6'>";
    //    var textRange; var j = 0;
    //    tab = document.getElementById('datatables-1'); // id of table

    //    for (j = 0 ; j < tab.rows.length ; j++) {
    //        tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
    //        //tab_text=tab_text+"</tr>";
    //    }

    //    tab_text = tab_text + "</table>";
    //    tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
    //    tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
    //    tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

    //    var ua = window.navigator.userAgent;
    //    var msie = ua.indexOf("MSIE ");

    //    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
    //    {
    //        txtArea1.document.open("txt/html", "replace");
    //        txtArea1.document.write(tab_text);
    //        txtArea1.document.close();
    //        txtArea1.focus();
    //        sa = txtArea1.document.execCommand("SaveAs", true, "Say Thanks to Sumit.xls");
    //    }
    //    else                 //other browser not tested on IE 11
    //        sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));

    //    return (sa);
    //}
    function openfileDialog() {
      
        $("#FileUpload1").OnClientClick();
   }
</script>
    </asp:Content>



