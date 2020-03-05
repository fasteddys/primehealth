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
    public partial class DeletePharm : System.Web.UI.Page
    {
        PhNetworkEntities db = new PhNetworkEntities();
        PharmaciesClass mdb = new PharmaciesClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.DeletePharmData(id);
            Response.Redirect("~/Portal/ShowAllPharms.aspx");
        }
    }
}