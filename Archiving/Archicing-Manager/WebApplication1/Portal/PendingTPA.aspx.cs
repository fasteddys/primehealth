using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Portal
{
    public partial class PendingTPA : System.Web.UI.Page
    {
        subRequests mdb = new subRequests();

        protected void Page_Load(object sender, EventArgs e)
        {
            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("Span19")).Attributes["class"] = "icon-thumbnail bg-success";
            if (!IsPostBack)
            {
                Bind();
            }
        }
        public void Bind()
        {
            string name = Request.Cookies["nameark"].Value;
            string type = Request.Cookies["typeark"].Value;
            if (type == "Admin" || type == "Archiving" || type == "ArcAdmin" || type == "EnterpriseAdmin" || type=="TPA" || type=="TPAAdmin")
            {
                var data = mdb.TPASUb();
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
            else 
            {
                Response.Redirect("/Login.aspx");
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Bind();
        }
    }

}
