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
    public partial class AddUser : System.Web.UI.Page
    {
        ClientNotesClass mdb = new ClientNotesClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("AddUser")).Attributes["class"] = "active";
            div_Error.Visible = false;
            div_Success.Visible = false;
        }
        protected void ApvSubmitNewClientNotes(object sender, EventArgs e)
        {
            string ValidationMeassage;

            string AddUserName = Request.Cookies["UserName"].Value;
            string OpenReqType = Request.Cookies["UserType"].Value;
            if (UserName.Value != "" && UserTypeDropdownList.SelectedItem.ToString() != "")
            {
                bool isUSerAdeed = mdb.addUserData(UserName.Value, UserTypeDropdownList.SelectedValue, AddUserName, out ValidationMeassage);
                if (isUSerAdeed == true)
                {
                    div_Success.Visible = true;
                    div_Success.InnerHtml = ValidationMeassage;
                    clean();
                }
                if (isUSerAdeed == false)
                {
                    div_Error.Visible = true;
                    div_Error.InnerHtml = ValidationMeassage;
                    clean();
                }

            }
            else
            {
                div_Error.Visible = true;
            }


        }

        public void clean()
        {
            UserName.Value = "";

        }
    }
}