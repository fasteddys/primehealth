using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;


namespace CallCenterNotesApp.Portal
{
    public partial class ShowAllFeedback : System.Web.UI.Page
    {
        ClaimsApprovalClass mdb = new ClaimsApprovalClass();
        protected void Page_Load(object sender, EventArgs e)
        {

            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("ShowAllFeedback")).Attributes["class"] = "active";
            if (IsPostBack)
            {
                Bind();
            }
        }
        public void Bind()
        {
            string user = Request.Cookies["UserName"].Value;
            string type = Request.Cookies["UserType"].Value;

            var data = mdb.GetAllFeedback(FeedbackTypeDropdownListnew.SelectedValue);
            lstNewReq.DataSource = data;
            lstNewReq.DataBind();

        }
        public void delete()
        {


            var data = mdb.GetAllFeedback(FeedbackTypeDropdownListnew.SelectedValue);
            lstNewReq.DataSource = data;
            lstNewReq.DataBind();

        }
        protected void RefreshPageTimerFN(object sender, EventArgs e)
        {
            Bind();
        }
    }
}