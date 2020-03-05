using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Portal
{
    public partial class MyAccount : System.Web.UI.Page
    {
        Accounts ac = new Accounts();
        protected void Page_Load(object sender, EventArgs e)
        {
            div_Error.Visible = false;
            div_success.Visible = false;
            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("MyAccountLi")).Attributes["class"] = "icon-thumbnail bg-success";
            if (!IsPostBack)
            {
                string name = Request.Cookies["nameark"].Value.Trim();
                BatchNumTxt.Value = name;
                ClaimsNUmtxt.Value = ac.GetPassword(name);
                BatchNumTxt.Disabled = true;
            }
        }

        protected void SubmitBtn_ServerClick(object sender, EventArgs e)
        {
            if (BatchNumTxt.Value == "" || ClaimsNUmtxt.Value == "")
            {
                div_Error.Visible = true;
            }
            else
            {
                string user = BatchNumTxt.Value.ToString();
                string pass = ClaimsNUmtxt.Value.ToString();
                ac.UpdatePassword(user, pass);
                div_success.Visible = true;
            }
        }
    }
}