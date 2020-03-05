using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Portal
{
    public partial class AddAccount : System.Web.UI.Page
    {
        Accounts mdb = new Accounts();
        protected void Page_Load(object sender, EventArgs e)
        {
            div_Error.Visible = false;
            div_success.Visible = false;
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("MainAdminLi")).Attributes["class"] = "icon-thumbnail bg-success";
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("UsersAdminLi")).Attributes["class"] = "open";
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("spanarrow")).Attributes["class"] = "arrow open";
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("AdduserLi")).Attributes["class"] = "icon-thumbnail bg-success";
        }

        protected void SubmitBtn_ServerClick(object sender, EventArgs e)
        {
            if (BatchNumTxt.Value == "" || ClaimsNUmtxt.Value == "")
            {
                div_Error.Visible = true;
            }
            else
            {
                bool add = mdb.adduser(BatchNumTxt.Value, ClaimsNUmtxt.Value, TypeDLL.SelectedValue);
                if (add)
                {
                    clear();
                    div_success.Visible = true;
                }
                else
                {
                    clear();
                    div_Error.Visible = true;
                }
            }
        }
        public void clear()
        {
            BatchNumTxt.Value = "";
            ClaimsNUmtxt.Value = "";
        }
    }
}