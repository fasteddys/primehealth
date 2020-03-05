using CardCycle.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardCycle
{
    public partial class ManageClient1 : System.Web.UI.Page
    {
        Client mdb = new Client();


        protected void Page_Load(object sender, EventArgs e)
        {
            div_Success_update.Visible = false;
            div_Success_Delete.Visible = false;

            if (!IsPostBack)
            {
                string ClientNotes = "";
                string ClientName = "";
                string ClientStatus = "";
                string Type = "";

              

                int id = int.Parse(Request.QueryString["id"]);
                mdb.GetClientNotesByID(id, ref  ClientNotes, ref ClientName, ref ClientStatus, ref Type);

                if (ClientStatus == "1") { ActivateClients.Checked=true; }
                else { ActivateClients.Checked = false; }

                ClientNotesTxt.Text = ClientNotes;
                ClientNameTxt.Text = ClientName;
                DropDownType.SelectedValue = Type; 
            }
        }

        protected void btn_update_ServerClick(object sender, EventArgs e)
        {
            string ActiveValue = "";
            if (ActivateClients.Checked) { ActiveValue = "1"; }
            else { ActiveValue = "0"; }

            int id = int.Parse(Request.QueryString["id"]);
            mdb.UpdateClientNotes(id, ClientNotesTxt.Text, ClientNameTxt.Text, ActiveValue , DropDownType.SelectedValue);
            div_Success_update.Visible = true;
        }

        protected void btn_delete_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.DeleteClient(id);
            Response.Redirect("AllClients.aspx");
        }
    }
}