using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Portal
{
    public partial class BatchManager1 : System.Web.UI.Page
    {
        Batches mdb = new Batches();
        protected void Page_Load(object sender, EventArgs e)
        {
            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("BatchsubmgrLi")).Attributes["class"] = "icon-thumbnail bg-success";
            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            var data = mdb.GetAllBatches();
            ItemsList.DataSource = data;
            ItemsList.DataBind();
        }

        protected void ItemsList_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            ItemsList.EditIndex = -1;
            Bind();
        }

        protected void ItemsList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            Label lblTemp = ItemsList.Items[e.ItemIndex].FindControl("IDTxt") as Label;
            int id = int.Parse(lblTemp.Text);
            mdb.DeleteBatch(id);
            Bind();
        }

        protected void ItemsList_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            ItemsList.EditIndex = e.NewEditIndex;
            Bind();
        }

        protected void ItemsList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            Label lblTemp = ItemsList.Items[e.ItemIndex].FindControl("EditIDTxt") as Label;
            int id = int.Parse(lblTemp.Text);
            TextBox txtname = ItemsList.Items[e.ItemIndex].FindControl("BatchNumtxt") as TextBox;
            string batch = txtname.Text;
            TextBox txtpass = ItemsList.Items[e.ItemIndex].FindControl("BoxNumTxt") as TextBox;
            string box = txtpass.Text;
            mdb.UpdateBatch(id,batch,box);
            ItemsList.EditIndex = -1;
            Bind();
        }
    }
}