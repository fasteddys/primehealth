<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="SearchClaimNum.aspx.cs" Inherits="WebApplication1.Portal.SearchClaimNum" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

           <div class="jumbotron" data-pages="parallax">
        <div class="container-fluid container-fixed-lg sm-p-l-20 sm-p-r-20">
            <div class="inner">
                <ul class="breadcrumb">
                    <li>
                        <p>DashBoard</p>
                    </li>
                    <li><a href="#" class="active">Search Number Of Claims In Batch</a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
      <div id="Div2" runat="server" align="left" class="alert alert-success alert-dismissable " style="font-size:17px"  >
                              Please Choose Destination Claims :-
                            </div> 
    <br />
    <div class="container-fluid container-fixed-lg bg-white">
     <asp:DropDownList ID="DropDownList1" runat="server" >
     <asp:ListItem>OutPatient</asp:ListItem>
     <asp:ListItem>InPatient</asp:ListItem>
    </asp:DropDownList>
      <br /> 
     <br />
    <div id="Div1" runat="server" align="left"  class="alert alert-success alert-dismissable " style="font-size:17px">
                              Please Choose Parameter :

    </div> 
        <br />
    <asp:DropDownList ID="DropDownList2" runat="server" >
     <asp:ListItem>Claim Code OR Pre_AuthID</asp:ListItem>
     <asp:ListItem>Set ID</asp:ListItem>
    </asp:DropDownList>
        <br />
        <br />
        <div id="Div3" runat="server" align="left" class="alert alert-success alert-dismissable " style="font-size:17px"  >
                              Please Enter Batch Number :-
                            </div> 
        <div>
          <input id="txtsearch" runat="server" type="text" placeholder="Enter Batch Number..." >
      </div>
        <br />
        <br />
        <div>
            
            <button id="Button1" runat="server" onserverclick="btnUpload_Click" class="btn btn-primary btn-cons m-b-20" type="button" value="Submit Search">Submit</button>

        </div>


        <div runat="server" id="divsuccess">

           <span style="font-size:15px" class="alert alert-success alert-dismissable" >There Is/Are  <span runat="server" id="sp1"></span><label id="lbl1" style="font-size:15px" class="alert alert-success alert-dismissable"></label>Record(s) In That Batch</span>

        </div>

       <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error" style="font-size:15px">
                    Error Operation...
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
</asp:Content>