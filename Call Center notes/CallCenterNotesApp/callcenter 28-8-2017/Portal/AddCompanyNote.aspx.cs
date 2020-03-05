using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;

namespace CallCenterNotesApp.Portal
{
    public partial class AddCompanyNote : System.Web.UI.Page
    {
        ClientNotesClass mdb = new ClientNotesClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("AddCompanyNote")).Attributes["class"] = "active";
            div_Error.Visible = false;
            RequestDate.Value = DateTime.Now.ToString();
            div_Success.Visible = false;
        }
        protected void ApvSubmitNewClientNotes(object sender, EventArgs e)
        {
            try
            {
                string AddClientNotesName = Request.Cookies["UserName"].Value;
                string OpenReqType = Request.Cookies["UserType"].Value;
                if (ClientName.Value != "" && Notes.Value != "")
                {
                    mdb.addClientNotes(RequestDate.Value, ClientName.Value,ClientTypeDropdownListnew.SelectedValue, Notes.Value, AddClientNotesName, FileUpload1.PostedFile, FileUpload1);
                    div_Success.Visible = true;
                    clean();

                }
                else
                {
                    div_Error.Visible = true;
                }
            }
            catch (Exception)
            {
                div_Error.Visible = true;

            }
        }
        public void clean()
        {
            ClientName.Value = "";
            Notes.Value = "";
            
        }
    }
}