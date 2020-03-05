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
    public partial class Delete_Doctors : System.Web.UI.Page
    {
        PhNetworkEntities db = new PhNetworkEntities();
        DoctorClass mdb = new DoctorClass();
        protected void Page_Load(object sender, EventArgs e)
        {

            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("Delete_Doctors")).Attributes["class"] = "active";
            int id = int.Parse(Request.QueryString["id"]);
            mdb.DeleteDoctorData(id);
            Response.Redirect("~/Portal/ShowAllDoctors.aspx");
        }
    }
}