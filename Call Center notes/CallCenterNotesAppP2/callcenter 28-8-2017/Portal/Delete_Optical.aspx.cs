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
    public partial class Delete_Optical : System.Web.UI.Page
    {
        PhNetworkEntities db = new PhNetworkEntities();
        OpticalClass mdb = new OpticalClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.DeleteOpticalData(id);
            Response.Redirect("~/Portal/ShowAllOpticals.aspx");
        }
    }
}