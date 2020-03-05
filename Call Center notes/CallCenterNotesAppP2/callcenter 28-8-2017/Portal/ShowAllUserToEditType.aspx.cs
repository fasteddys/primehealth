using CallCenterNotesApp.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CallCenterNotesApp.Portal
{
    public partial class ShowAllUserToEditType : System.Web.UI.Page
    {
        Helpers helpers = new Helpers();
        
        protected void Page_Load(object sender, EventArgs e)
        {
           ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("LiChangeUserTypeForManager")).Attributes["class"] = "active";
            Bind();
        }
        public void Bind()
        {
            string UserName = Request.Cookies["UserName"].Value;
            var data = helpers.GetAllUsers(UserName);
            lstNewReq.DataSource = data;
            lstNewReq.DataBind();
        }
    }
}