using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;
using CallCenterNotesApp.DLL;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;

namespace CallCenterNotesApp.Portal
{
    public partial class ManageLocation_sData : System.Web.UI.Page
    {
        PhNetworkEntities db = new PhNetworkEntities();
        LocationsAndCategories mdb = new LocationsAndCategories();
        protected void Page_Load(object sender, EventArgs e)
        {

            string LocationNamestr = "";

            if (!IsPostBack)
            {
                div_Success_update.Visible = false;
                int id = int.Parse(Request.QueryString["id"]);
                string type = Request.Cookies["UserType"].Value.Trim();
                UpdateLocationData.Visible = false;
                DeleteLocationData.Visible = false;
                //int id = 3;
                // IDLable.InnerText = id.ToString();

                mdb.GetLocationDetailByid(id, ref LocationNamestr);

                LocationNameID.Value = LocationNamestr;

                if (type == "ProviderUser")
                {
                    UpdateLocationData.Visible = true;
                    DeleteLocationData.Visible = true;
                }
                /////////////////////////////////////////////////////////

            }


        }
        protected void DeleteLocationData_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.DeleteLocationData(id);
            Response.Redirect("~/Portal/AddLocations.aspx");
        }
        protected void UpdateLocationData_ServerClick(object sender, EventArgs e)
        {

            int id = int.Parse(Request.QueryString["id"]);
            mdb.UpdateLocationData(id, LocationNameID.Value);

            div_Success_update.Visible = true;
        }
    }
}