using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;

namespace CallCenterNotesApp.Portal
{
    public partial class ShowAllClients : System.Web.UI.Page
    {
        ClaimsApprovalClass mdb = new ClaimsApprovalClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("ShowClientData")).Attributes["class"] = "active";
            if (!IsPostBack)
            {
                // Bind();
            }
        }
        protected void ClientSearch(object sender, EventArgs e)
        {
            try
            {
                if (MedicalID.Value != "")
                {
                    Bind();

                    //div_Success.Visible = true;
                    //div_Error.Visible = false;
                }
                else
                {
                    //div_Error.Visible = true;
                    //div_Success.Visible = false;

                }
            }
            catch (Exception)
            {
                //div_Error.Visible = true;
                //div_Success.Visible = false;

            }
        }

        public void Bind()
        {
            string user = Request.Cookies["UserName"].Value;
            string type = Request.Cookies["UserType"].Value;

            var data = mdb.GetAllUers(MedicalID.Value);
            lstNewReq.DataSource = data;
            lstNewReq.DataBind();

        }
    }
}