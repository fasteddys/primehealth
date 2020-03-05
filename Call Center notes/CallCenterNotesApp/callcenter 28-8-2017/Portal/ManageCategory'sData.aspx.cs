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
    public partial class ManageCategory_sData : System.Web.UI.Page
    {


        PhNetworkEntities db = new PhNetworkEntities();
        LocationsAndCategories mdb = new LocationsAndCategories();
        protected void Page_Load(object sender, EventArgs e)
        {

            string CategoryNamestr = "";

            if (!IsPostBack)
            {
                div_Success_update.Visible = false;
                int id = int.Parse(Request.QueryString["id"]);
                string type = Request.Cookies["UserType"].Value.Trim();
                UpdateCategoryData.Visible = false;
                DeleteCategoryData.Visible = false;

                mdb.GetCategoryDetailByid(id, ref CategoryNamestr);

                CategoryNameID.Value = CategoryNamestr;

                if (type == "ProviderUser")
                {
                    UpdateCategoryData.Visible = true;
                    DeleteCategoryData.Visible = true;
                }
                /////////////////////////////////////////////////////////

            }


        }
        protected void DeleteCategoryData_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.DeleteCategoryData(id);
            Response.Redirect("~/Portal/AddCategory.aspx");
        }
        protected void UpdateCategoryData_ServerClick(object sender, EventArgs e)
        {

            int id = int.Parse(Request.QueryString["id"]);
            mdb.UpdateCategoryData(id, CategoryNameID.Value);

            div_Success_update.Visible = true;
        }
    }
}