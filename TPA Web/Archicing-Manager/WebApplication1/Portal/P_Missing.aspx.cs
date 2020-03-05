using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Portal
{
    public partial class P_Missing : System.Web.UI.Page
    {
        Requests mdb = new Requests();
        protected void Page_Load(object sender, EventArgs e)
        {
            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("P_MLI")).Attributes["class"] = "icon-thumbnail bg-success";
            if (!IsPostBack)
            {
                Bind();
            }
        }
        public void Bind()
        {
            string name = Request.Cookies["nameark"].Value;
            string type = Request.Cookies["typeark"].Value;

            if (type == "User")
            {
                var data = mdb.GetAllReqP_MissingByUser(name);
                ItemsList.DataSource = data;
                ItemsList.DataBind();
            }
            else if ( type == "DataEntry")
            {
                var data = mdb.GetAllReqP_MissingByArchiving(name);
                ItemsList.DataSource = data;
                ItemsList.DataBind();
            }

            else if (type == "Admin")
            {
                var data = mdb.GetAllReqP_MissingByAdmin();
                ItemsList.DataSource = data;
                ItemsList.DataBind();
            }
        }
    }
}