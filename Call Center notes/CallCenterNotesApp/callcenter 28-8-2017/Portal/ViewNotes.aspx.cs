using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;

namespace CallCenterNotesApp.Portal
{
    public partial class ViewNotes : System.Web.UI.Page
    {
        ClientNotesClass mdb = new ClientNotesClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("ViewNotes")).Attributes["class"] = "active";
            if (IsPostBack)
            {
                Bind();
            }
        }
        protected void Refresh(object sender, EventArgs e)
        {

            Bind();

        }
        public void Bind()
        {
            string user = Request.Cookies["UserName"].Value;
            string type = Request.Cookies["UserType"].Value;

            var data = mdb.GetAllClientNotes(ClientTypeDropdownListnew.SelectedValue);
            lstNewReq.DataSource = data;
            lstNewReq.DataBind();

        }
        protected void RefreshPageTimerFN(object sender, EventArgs e)
        {
            Bind();
        }
    }
}