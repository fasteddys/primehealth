using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Users
{
    public partial class AllRequests : System.Web.UI.Page
    {
        Requests mdb = new Requests();

        protected void Page_Load(object sender, EventArgs e)
        {
            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("HomeLi")).Attributes["class"] = "icon-thumbnail bg-success";
            try
            {
                if (!IsPostBack)
                {
                    Bind();
                }
            }
            catch
            {
                Response.Redirect("../login.aspx");
            }
        }

        public void Bind()
        {
            string name = Request.Cookies["nameark"].Value;
            string type = Request.Cookies["typeark"].Value;
            if (type == "User" || type == "Remb" || type=="Data Entry")
            {
                var data = mdb.GetAllByACC(name);
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
            else if (type == "Admin" || type == "Archiving" || type == "ArcAdmin" || type == "EnterpriseAdmin")
            {
                var data = mdb.GetData();
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
        }
    }
}