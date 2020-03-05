using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Portal
{
    public partial class Unassigned : System.Web.UI.Page
    {
        Requests mdb = new Requests();

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Cookies["typeark"].Value;

            if (type=="User" || type=="Remb" || type=="Data Entry")
            {
                 ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("Span10")).Attributes["class"] = "icon-thumbnail bg-success";
                   if (!IsPostBack)
                   {
                         Bind();
                   }
            }

            else
            {
                Response.Redirect("/Login.aspx");
            }
           
        }

        private void Bind()
        {
            string name = Request.Cookies["nameark"].Value;
            string type = Request.Cookies["typeark"].Value;
            if (type == "User" || type == "Remb" || type == "Data Entry")//update
            {
                var data = mdb.GetAllReqUnassigned(name);
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