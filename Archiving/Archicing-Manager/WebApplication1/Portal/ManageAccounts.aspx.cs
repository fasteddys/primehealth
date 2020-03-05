using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Portal
{
    public partial class ManageAccounts : System.Web.UI.Page
    {
        Accounts ac = new Accounts();
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Cookies["typeark"].Value;
            if (type == "Admin")
            {
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("MainAdminLi")).Attributes["class"] = "icon-thumbnail bg-success";
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("UsersAdminLi")).Attributes["class"] = "open";
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("spanarrow")).Attributes["class"] = "arrow open";
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("MngUsersLi")).Attributes["class"] = "icon-thumbnail bg-success";
                if (!IsPostBack)
                {
                    Bind();
                }

            }

            else
            {
                Response.Redirect("/Login.aspx");
            }
        }
        public void Bind()
        {
            var data = ac.GetData();
            ItemsList.DataSource = data;
            ItemsList.DataBind();
        }

        protected void ItemsList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            Label lblTemp = ItemsList.Items[e.ItemIndex].FindControl("EmpIDLabel") as Label;
            int id = int.Parse(lblTemp.Text);
            ac.DeleteUser(id);
            Bind();
        }

        protected void ItemsList_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            ItemsList.EditIndex = e.NewEditIndex;
            Bind();
        }

        protected void ItemsList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            Label lblTemp = ItemsList.Items[e.ItemIndex].FindControl("EmpIDLabel1") as Label;
            int id = int.Parse(lblTemp.Text);
            TextBox txtname = ItemsList.Items[e.ItemIndex].FindControl("EmpNameTextBox") as TextBox;
            string name = txtname.Text;
            TextBox txtpass = ItemsList.Items[e.ItemIndex].FindControl("EmpPassTextBox") as TextBox;
            string pass = txtpass.Text;
            DropDownList ddlTemp = ItemsList.Items[e.ItemIndex].FindControl("DropDownList2") as DropDownList;
            string type = ddlTemp.SelectedValue;
            ac.UpdateUser(id, name, pass, type);
            ItemsList.EditIndex = -1;
            Bind();
        }

        protected void ItemsList_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            ItemsList.EditIndex = -1;
            Bind();
        }
    }
}