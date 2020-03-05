using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;


namespace WebApplication1.Portal
{
    public partial class RemPending : System.Web.UI.Page
    {

        Requests mdb = new Requests();

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Cookies["typeark"].Value;

            if (type == "User" || type == "Remb")
            {
                Response.Redirect("/Login.aspx");
            }

            else
            {
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("rembpending")).Attributes["class"] = "icon-thumbnail bg-success";
                if (!IsPostBack)
                {
                    Bind();
                }
            }
        }
        public void Bind()
        {

            string type = Request.Cookies["typeark"].Value;
            if (type == "Admin" || type == "ArcAdmin" || type == "EnterpriseAdmin")
            {
                var data = mdb.GetAllReqRembPConfirm();
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Bind();
        }
    }
}