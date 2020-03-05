using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;

namespace CallCenterNotesApp.Portal
{
    public partial class ShowAllLabs : System.Web.UI.Page
    {
        LaboratoriesClass mdb = new LaboratoriesClass();
        protected void Page_Load(object sender, EventArgs e)
        {

            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("ShowAllLabs")).Attributes["class"] = "active";
            if (!IsPostBack)
            {
                Bind();
            }
        }
        public void Bind()
        {
            string user = Request.Cookies["UserName"].Value;
            string type = Request.Cookies["UserType"].Value;

            var data = mdb.GetAllLabs();
            lstNewReq.DataSource = data;
            lstNewReq.DataBind();

        }
    }
}