<%@ Page Title="" Language="C#" MasterPageFile="~/Master Pages/Site2.Master" AutoEventWireup="true" CodeBehind="AMAccount.aspx.cs" Inherits="CardCycle.AMAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="row">
        <div class="col-md-12">
                    <div class="widget">
            <div class="widget-content padding">
                <div align="Center" ><h2>My Account</h2> </div>
                <hr />
                                 <div class="alert alert-success" runat="server" id="div_success">
                                User  Successfully Opration
                            </div>
                            <div class="alert alert-danger" runat="server" id="div_Error">
                               Erorr Opration
                            </div>

                <div class="row">
						  <div class="form-group">
							<label for="input-text" class="col-sm-2 control-label">User Name</label>
							<div class="col-sm-10">
                               <input type="text" class="form-control" runat="server" id="txt_name" placeholder="" readonly>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="this filed required" ControlToValidate="txt_name" ForeColor="Red"></asp:RequiredFieldValidator>--%>
							</div>
						  </div>
                            </div>
                <br />
                                <div class="row">
						  <div class="form-group">
							<label for="input-text" class="col-sm-2 control-label">User Password</label>
							<div class="col-sm-10">
                            <%--   <input type="text" >--%>
                                <asp:TextBox class="form-control" runat="server" ID="txt_pass" ></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="this filed required" ControlToValidate="txt_pass" ForeColor="Red"></asp:RequiredFieldValidator>--%>
							</div>
						  </div>
                            </div>
                 <br />	
                    <div class="col-md-12">
											<div class="toolbar-btn-action">
											<%--	<a class="btn btn-success" runat="server" id="btn_Add" onserverclick="Adduser"><i class="glyphicon glyphicon-floppy-save"></i> Add</a>--%>
												<a class="btn btn-warning" runat="server" id="btn_update" onserverclick="updateuser"><i class="glyphicon glyphicon-wrench"></i> Update Password</a>
												<%--<a class="btn btn-danger" runat="server" id="btn_delete" onserverclick="DeleteUser"><i class="glyphicon glyphicon-remove-circle"></i>Delete</a>--%>
                                                 
											</div>
										</div>	
                <br />
                <br />	
                	
        
                </div>
                        </div>

        </div>
    </div>
</asp:Content>
