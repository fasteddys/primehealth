<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="ExcelSearch.aspx.cs" Inherits="WebApplication1.Portal.ExcelSearch" EnableEventValidation = "false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <h2>Excel Search Form</h2>

    <div id="Div2" runat="server" align="left" class="alert alert-success alert-dismissable " style="font-size:17px"  >
                              Please Choose Excel File :-
                            </div> 
    <div id="divsearch" runat="server" align="center" class="row" >
                              <asp:FileUpload ID="FileUpload1"  runat="server" class="m-b-20"  /> 

        <div id="Div1" runat="server" align="left" class="alert alert-success alert-dismissable " style="font-size:17px"  >
                              Please Choose your Destination Claims :-
                            </div> 
                              <asp:DropDownList ID="DropDownList1" runat="server" >
                               <asp:ListItem>OutPatient</asp:ListItem>
                               <asp:ListItem>InPatient</asp:ListItem>
                               </asp:DropDownList>   
                            <br />
                            <br />
                            <br />
                                <button id="Button1" runat="server" onserverclick="btnUpload_Click" class="btn btn-primary btn-cons m-b-20" type="button" value="Upload Excel File">Search By Excel File</button>
                              

                            </div>
                <asp:GridView  ID="GridView1" runat="server"></asp:GridView>
                        <div id="ErrorDiv" runat="server" align="center" class="alert alert-danger alert-dismissable" >
                               Error Operation...
                            </div>


</asp:Content>







<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="../assets/plugins/jquery-datatable/media/js/jquery.dataTables.min.js" type="text/javascript"></script>
<script src="../assets/plugins/jquery-datatable/extensions/TableTools/js/dataTables.tableTools.min.js" type="text/javascript"></script>
<script src="../assets/plugins/jquery-datatable/extensions/Bootstrap/jquery-datatable-bootstrap.js" type="text/javascript"></script>
<script type="text/javascript" src="../assets/plugins/datatables-responsive/js/datatables.responsive.js"></script>
<script type="text/javascript" src="../assets/plugins/datatables-responsive/js/lodash.min.js"></script>
    <script src="../assets/js/datatables.js" type="text/javascript"></script>
</asp:Content>