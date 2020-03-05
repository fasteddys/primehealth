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
    public partial class ManageSubLocation_sData : System.Web.UI.Page
    {
        PhNetworkEntities db = new PhNetworkEntities();
        LocationsAndCategories mdb = new LocationsAndCategories();
        protected void Page_Load(object sender, EventArgs e)
        {

            string SubLocationNamestr = "";
            string LocationIDstr = "" ;

            if (!IsPostBack)
            {
                #region fill locations DropdownList
                var SubLocationList = mdb.GetAllLocations().Select(u => new ListItem
                {
                    Text = u.LocationName,
                    Value = u.LocID.ToString()
                }).ToList();
                LocationID.Items.Add("-- Enter location Name --");

                foreach (var item in SubLocationList)
                {
                    LocationID.Items.Add(item);
                }
                #endregion

                div_Success_update.Visible = false;
                int id = int.Parse(Request.QueryString["id"]);
                string type = Request.Cookies["UserType"].Value.Trim();
                UpdateSubLocationData.Visible = false;
                DeleteSubLocationData.Visible = false;

                mdb.GetSubLocationDetailByid(id, ref SubLocationNamestr, ref LocationIDstr);

                SubLocationNameID.Value = SubLocationNamestr;
                LocationID.SelectedValue = LocationIDstr.ToString();

                if (type == "ProviderUser")
                {
                    UpdateSubLocationData.Visible = true;
                    DeleteSubLocationData.Visible = true;
                }
                /////////////////////////////////////////////////////////

            }


        }
        protected void DeleteSubLocationData_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.DeleteSubLocationData(id);
            Response.Redirect("~/Portal/AddSubLocation.aspx");
        }
        protected void UpdateSubLocationData_ServerClick(object sender, EventArgs e)
        {

            int id = int.Parse(Request.QueryString["id"]);
            mdb.UpdateSubLocationData(id, SubLocationNameID.Value, LocationID.SelectedValue);

            div_Success_update.Visible = true;
        }
    }
}