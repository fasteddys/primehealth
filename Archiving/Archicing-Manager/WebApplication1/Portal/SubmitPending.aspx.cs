using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Portal
{
    public partial class SubmitPending : System.Web.UI.Page
    {
        subRequests mdb = new subRequests();

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Cookies["typeark"].Value;
            string user = Request.Cookies["nameark"].Value;
            if (type == "User" || type == "Remb")
            {
                Response.Redirect("/Login.aspx");
            }
            else
            {
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("Span24")).Attributes["class"] = "icon-thumbnail bg-success";
                if (!IsPostBack)
                {
                    Bind();
                }
            }

        }

        private void Bind()
        {
            string type = Request.Cookies["typeark"].Value;
            string user = Request.Cookies["nameark"].Value;
            if (type == "Admin" || type == "Archiving" || type == "ArcAdmin")
            {
                var data = mdb.GetAllReqPending_ForArchiving();
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
            else if ( type == "EnterpriseAdmin" )
            {
                var data = mdb.GetAllReqPending_ForEnterprisAdmin();
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
            else if (type == "TPA" || type == "TPAAdmin")
            {
                var data = mdb.GetAllReqPending_ForTPA();
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
            else if (type == "Data Entry")
            {
                var data = mdb.GetAllReqPendingByDE(user);
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