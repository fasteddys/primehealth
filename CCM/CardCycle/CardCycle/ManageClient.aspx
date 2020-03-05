<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Site2.Master" AutoEventWireup="true" CodeBehind="ManageClient.aspx.cs" Inherits="CardCycle.ManageClient1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget">
        <div class="widget-content padding">
            <div align="center">
                <h2>Manage Client</h2>
            </div>
            <div class="alert alert-success" runat="server" id="div_Success_update">
                Client Updated Successfully
            </div>
            <div class="alert alert-warning" runat="server" id="div_Success_Delete">
                Client Deleted Successfully
            </div>
            <hr />
            <br />
            <div class="row">
                <div class="form-group">
                    <label for="input-text" class="col-sm-2 control-label">ClientName</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="ClientNameTxt" runat="server" TextMode="MultiLine" Width="100%" Rows="6"></asp:TextBox>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="form-group">
                    <label for="input-text" class="col-sm-2 control-label">ClientNotes</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="ClientNotesTxt" runat="server" TextMode="MultiLine" Width="100%" Rows="6"></asp:TextBox>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="form-group">
                    <label for="input-text" class="col-sm-2 control-label">ClientStatus</label>
                    <div class="col-sm-10">
                        <label> <input type="checkbox" value="1" id="ActivateClients" runat="server"> Active - Not Active </label>                    </div>
                </div>
            </div>
             <Br/>
                    <div class="row">
                        <div class="form-group">
                            <label for="input-text" class="col-sm-2 control-label">Type</label>
                            <div class="col-sm-6">
                                <div class="row">
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="DropDownType"  CssClass="form-control dropdown"  Font-Bold="True" Font-Size="Small" AppendDataBoundItems="true" runat="server">
                                            <asp:ListItem>Normal</asp:ListItem>
                                            <asp:ListItem>TPA</asp:ListItem>
                                            
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

            <br />
            <br />
            <div class="col-md-12">
                <div class="toolbar-btn-action">
                    <a class="btn btn-primary" runat="server" id="btn_update" onserverclick="btn_update_ServerClick"><i class="fa fa-upload"></i>Update</a>
                    <a class="btn btn-danger" runat="server" id="btn_delete" onserverclick="btn_delete_ServerClick"><i class="fa fa-remove"></i>Delete</a>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-10">
                </div>
            </div>



        </div>

    </div>

    </div>
</asp:Content>
