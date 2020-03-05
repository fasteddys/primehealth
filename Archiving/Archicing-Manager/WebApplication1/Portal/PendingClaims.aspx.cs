using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;


namespace WebApplication1.Portal
{
    public partial class PendingClaims : System.Web.UI.Page
    {
        Requests mdb = new Requests();
        protected void Page_Load(object sender, EventArgs e)
        {
            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("Span12")).Attributes["class"] = "icon-thumbnail bg-success";
            if (!IsPostBack)
            {
                Bind();
            }
        }
        private void Bind()
        {
            string name = Request.Cookies["nameark"].Value;
            string type = Request.Cookies["typeark"].Value;
            if (type == "Data Entry" || type == "EnterpriseAdmin" || type=="Archiving" || type=="Admin" || type=="ArcAdmin")
            {
                var data = mdb.GetAllPendingClaims();
                lstNewReq.DataSource = data;
                lstNewReq.DataBind(); 
            }

            else if (type=="User")
            {
                var data = mdb.GetAllPendingClaimsForUser(name);
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