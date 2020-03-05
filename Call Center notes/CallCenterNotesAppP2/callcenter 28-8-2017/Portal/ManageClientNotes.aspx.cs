using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;
using CallCenterNotesApp.DLL;


namespace CallCenterNotesApp.Portal
{
    public partial class ManageClientNotes : System.Web.UI.Page
    {
        PhNetworkEntities db = new PhNetworkEntities();
        ClientNotesClass mdb = new ClientNotesClass();
        protected void Page_Load(object sender, EventArgs e)
        {

            string Creatorstr = "";
            string CreationTimestr = "";
            string ClientNamestr = "";
            string Notesstr = "";

            if (!IsPostBack)
            {
                div_Success_update.Visible = false;
                int id = int.Parse(Request.QueryString["id"]);
               // IDLable.InnerText = id.ToString();
                string type = Request.Cookies["UserType"].Value.Trim();
                string Uname = Request.Cookies["UserName"].Value.Trim();

                mdb.GetDetailByid(id, ref CreationTimestr, ref Creatorstr, ref ClientNamestr, ref Notesstr);

                ClientNotesCreator.InnerText = Creatorstr;
                CreationTime.InnerText = CreationTimestr;
                ClientName.Value = ClientNamestr;
                Notes.InnerText = Notesstr;

                btn_DeleteClientNotes.Visible = false;
                btn_UpdateClientNotes.Visible = false;
                /////////////////////////////////////////////////////////
                if (type == "CallCenterManager")
                {
                    btn_DeleteClientNotes.Visible = true;
                    btn_UpdateClientNotes.Visible = true;
                }
            }


        }
        protected void DeleteClientNotes_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.DeleteClient(id);
            Response.Redirect("~/Portal/ViewNotes.aspx");
        }
        protected void UpdateClientNotes_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.UpdateClientNotes(id, ClientName.Value, Notes.Value);
            div_Success_update.Visible = true;
        }
    }
}