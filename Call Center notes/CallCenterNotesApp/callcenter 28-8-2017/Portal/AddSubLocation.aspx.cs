using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;


namespace CallCenterNotesApp.Portal
{
    public partial class AddSubLocation : System.Web.UI.Page
    {
        LocationsAndCategories mdb = new LocationsAndCategories();
        protected void Page_Load(object sender, EventArgs e)
        {

            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("AddNewSubLocation")).Attributes["class"] = "active";
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
                div_Error.Visible = false;
                div_Success.Visible = false;
                Bind();
                if (LocationID.SelectedValue == "-- Enter Location Name --") { TableDiv.Visible = true; }
                else { TableDiv.Visible = false; }

            }
        }
        protected void SubmitNewSubLocation(object sender, EventArgs e)
        {
            try
            {

                if (SubLocationNameID.Value != "" && LocationID.SelectedIndex.ToString() != "-- Enter Location Name --")
                {
                    mdb.addSubLocationDeatails(SubLocationNameID.Value, LocationID.SelectedValue.ToString());
                    div_Success.Visible = true;
                    div_Error.Visible = false;
                    Response.Redirect("~/Portal/AddSubLocation.aspx");
                    clean();
                }
                else
                {
                    div_Error.Visible = true;
                    div_Success.Visible = false;

                }
            }
            catch (Exception)
            {
                div_Error.Visible = true;
                div_Success.Visible = false;

            }
        }
        public void Bind()
        {
            string user = Request.Cookies["UserName"].Value;
            string type = Request.Cookies["UserType"].Value;

            var data = mdb.GetAllSubLocations();
            lstNewReq.DataSource =data;
            lstNewReq.DataBind();

        }
        public void clean()
        {
            SubLocationNameID.Value = "";


        }
    }
}