using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;

namespace CallCenterNotesApp.Portal
{
    public partial class ShowAllOpticals : System.Web.UI.Page
    {
        OpticalClass mdb = new OpticalClass();
        protected void Page_Load(object sender, EventArgs e)
        {

            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("ShowAllOpticals")).Attributes["class"] = "active";
            if (!IsPostBack)
            {
                Bind();
            }
        }
        public void Bind()
        {
            string user = Request.Cookies["UserName"].Value;
            string type = Request.Cookies["UserType"].Value;

            var data = mdb.GetAllOpticalShops();
            lstNewReq.DataSource = data;
            lstNewReq.DataBind();

        }

    }
}