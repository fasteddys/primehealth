using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Users
{
    public partial class Closed : System.Web.UI.Page
    {
        Requests mdb = new Requests();
        protected void Page_Load(object sender, EventArgs e)
        {
            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("ClosedLi")).Attributes["class"] = "icon-thumbnail bg-success";
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
                var data = mdb.GetAllReqClosedBYACM(name);
                ItemsList.DataSource = data;
                ItemsList.DataBind();
            }
            else if (type == "DataEntry")
            {
                var data = mdb.GetAllReqClosedByArchiving(name);
                ItemsList.DataSource = data;
                ItemsList.DataBind();
            }
            else if (type == "TBAUser")
            {
                var data = mdb.GetAllReqClosedAll();
                ItemsList.DataSource = data;
                ItemsList.DataBind();
            }
            else if (type == "Admin")
            {
                var data = mdb.GetAllReqClosedAll();
                ItemsList.DataSource = data;
                ItemsList.DataBind();
            }

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Bind();
        }
    }
}