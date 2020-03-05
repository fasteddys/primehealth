<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="AddBalackOfPharm.aspx.cs" Inherits="CallCenterNotesApp.Portal.AddBalackOfPharm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
<%--    <form id="formID" runat="server">--%>

        <div class="alert alert-success alert-dismissable" runat="server" id="div_Success">
                              Inserted Correctly
            </div>
         <div class="alert alert-danger alert-dismissable" runat="server" id="div_Error">
         Error
       </div>
 
            <asp:FileUpload ID="FileUpload1" runat="server" class="btn btn-default"/>
            <br />
       
         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FileUpload1" ErrorMessage="Error The File Must Be Only Excel " Font-Bold="True" ForeColor="Red" ValidationExpression=".*\.xls[xm]?"></asp:RegularExpressionValidator>
&nbsp;<br />
     <div class="col-md-12" style="text-align: center;">
         <div class="toolbar-btn-action">

             <asp:Button ID="btn_add_balack" class="btn btn-primary" runat="server" Text="Add" Width="193px" OnClick="btn_add_balack_Click"  />
             
          </div>
      </div>
<%--    </form>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
</asp:Content>
