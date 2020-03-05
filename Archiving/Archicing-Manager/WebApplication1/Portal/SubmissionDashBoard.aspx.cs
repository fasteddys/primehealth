using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Portal
{
    public partial class SubmissionDashBoard : System.Web.UI.Page
    {
        subRequests mdb = new subRequests();

        protected void Page_Load(object sender, EventArgs e)
        {
            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("Span22")).Attributes["class"] = "icon-thumbnail bg-success";
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
            if (type == "Data Entry")
            {
                var data = mdb.GetAllByACC(name);
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
            else if (type == "Admin" || type == "Archiving" || type == "ArcAdmin" )
            {
                var data = mdb.GetDataArchiving();
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }

            else if (type == "TPAAdmin")
            {
                var data = mdb.GetDataTPA();
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
               else if ( type == "EnterpriseAdmin")
            {
                var data = mdb.EnterpriceAdmin();
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
            else if (type == "TPA")
            {
                var data = mdb.GetDataTPA();
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
        }
    }
}