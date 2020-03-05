using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Portal
{
    public partial class Archiving : System.Web.UI.Page
    {
        Requests mdb = new Requests();

        protected void Page_Load(object sender, EventArgs e)
        {
            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("ArchivingSPN")).Attributes["class"] = "icon-thumbnail bg-success";
            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            string name = Request.Cookies["nameark"].Value;
            string type = Request.Cookies["typeark"].Value;
            if (type == "User" || type == "Remb" || type=="Data Entry")
            {
                var data = mdb.GetAllReqSearchingByUser(name);
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
            else if (type == "Admin")//update
            {
                var data = mdb.GetAllReqSearchingByArchivingforadmin();
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
            else if ( type == "Archiving" || type == "ArcAdmin")
            {
                var data = mdb.GetAllReqSearchingByArchiving(name);
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
            else if (type == "EnterpriseAdmin")
            {
                var data = mdb.GetAllReqSearching();
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