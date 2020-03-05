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
    public partial class AddLocations : System.Web.UI.Page
    {
        LocationsAndCategories mdb = new LocationsAndCategories();
        protected void Page_Load(object sender, EventArgs e)
        {

            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("AddNewLocation")).Attributes["class"] = "active";
            if (!IsPostBack)
            {

                div_Error.Visible = false;
                div_Success.Visible = false;
                Bind();

            }
        }
        protected void SubmitNewLocation(object sender, EventArgs e)
        {
            try
            {

                if (LocationNameID.Value != "")
                {
                    mdb.addLocationDeatails(LocationNameID.Value);
                    div_Success.Visible = true;
                    div_Error.Visible = false;
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

            var data = mdb.GetAllLocations();
            lstNewReq.DataSource = data;
            lstNewReq.DataBind();

        }
        public void clean()
        {
            LocationNameID.Value = "";


        }
    }
}