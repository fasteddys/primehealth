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
    public partial class DeleteHospital : System.Web.UI.Page
    {
        PhNetworkEntities db = new PhNetworkEntities();
        HospitalClass mdb = new HospitalClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.DeleteHospitalData(id);
            Response.Redirect("~/Portal/showAllHospitals.aspx");
        }
    }
}