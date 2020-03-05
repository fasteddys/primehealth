<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="sssssssssssssss.aspx.cs" Inherits="CallCenterNotesApp.Portal.sssssssssssssss" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="server">
<%--    <form runat="server">--%>
            <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" />
            <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="UploadMultipleFiles" accept="image/gif, image/jpeg" />
            <hr />
            <asp:Label ID="lblSuccess" runat="server" ForeColor="Green" />
<%--        </form>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderScripts" runat="server">
</asp:Content>
