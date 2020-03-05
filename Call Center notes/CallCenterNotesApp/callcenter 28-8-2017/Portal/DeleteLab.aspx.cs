using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;
using CallCenterNotesApp.DLL;

namespace CallCenterNotesApp.Portal
{
    public partial class DeleteLab : System.Web.UI.Page
    {
        PhNetworkEntities db = new PhNetworkEntities();
        LaboratoriesClass mdb = new LaboratoriesClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.DeleteLabData(id);
            Response.Redirect("~/Portal/ShowAllLabs.aspx");
        }
    }
}