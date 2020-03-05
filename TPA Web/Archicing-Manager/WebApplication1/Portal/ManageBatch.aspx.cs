using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Portal
{
    public partial class ManageBatch : System.Web.UI.Page
    {
        Batches mdb = new Batches();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Bind();
            }
        }
        public void Bind()
        {
            int id = int.Parse(Request.QueryString["id"]);
           var data = mdb.GetAllClaimsByBatch(id);
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
            mdb.DeleteClaim(id);
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
            TextBox txtbatch = ItemsList.Items[e.ItemIndex].FindControl("BatchNumtxt") as TextBox;
            string batch = txtbatch.Text;
            TextBox txtclaim = ItemsList.Items[e.ItemIndex].FindControl("ClaimNumTxt") as TextBox;
            string claim = txtclaim.Text;
            mdb.UpdateClaim(id, batch, claim);
            ItemsList.EditIndex = -1;
            Bind();
        }
    }
}