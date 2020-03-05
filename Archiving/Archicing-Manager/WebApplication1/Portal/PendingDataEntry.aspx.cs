using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;


namespace WebApplication1.Portal
{
    public partial class PendingDataEntry : System.Web.UI.Page
    {
        Requests mdb = new Requests();

        protected void Page_Load(object sender, EventArgs e)
        {
            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("PendingDataEntrySpan")).Attributes["class"] = "icon-thumbnail bg-success";
            if (!IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            string name = Request.Cookies["nameark"].Value;
            string type = Request.Cookies["typeark"].Value;
            if (type == "Remb")
            {
                var data = mdb.GetAllReqPendingDataEntryByUser(name);
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
            else if (type == "Admin" || type == "Archiving" || type == "ArcAdmin" || type == "EnterpriseAdmin" || type == "Data Entry")
            {
                var data = mdb.GetAllReqPendingDataEntry();
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