using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.BLL;

namespace WebApplication1.Users
{
    public partial class PendingConfirmation : System.Web.UI.Page
    {
        Requests mdb = new Requests();

        protected void Page_Load(object sender, EventArgs e)
        {
            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("PendingLi")).Attributes["class"] = "icon-thumbnail bg-success";
            if (!IsPostBack)
            {
                Bind();
            }
        }
        public void Bind()
        {
            string name = Request.Cookies["nameark"].Value;
            string type = Request.Cookies["typeark"].Value;

            if (type == "User")
            {
                var data = mdb.GetAllReqPConfirmBYUser(name);
                ItemsList.DataSource = data;
                ItemsList.DataBind();  
            }
            else if (type == "TBAUser")
            {
                var data = mdb.GetAllReqPConfirmBYTba();
                ItemsList.DataSource = data;
                ItemsList.DataBind();  
            }
                

            else if (type == "DataEntry")
            {
                var data = mdb.GetAllReqPConfirmByArchiving(name);
                ItemsList.DataSource = data;
                ItemsList.DataBind();
            }
            else if (type == "Admin")
            {
                var data = mdb.GetAllReqPConfirmByAdmin();
                ItemsList.DataSource = data;
                ItemsList.DataBind();
            }
        }
    }
}