using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;

namespace CallCenterNotesApp.Portal
{
    public partial class ShowAllUsers : System.Web.UI.Page
    {
        ClientNotesClass mdb = new ClientNotesClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("ShowAllUsers")).Attributes["class"] = "active";
            if (!IsPostBack)
            {
                Bind();
                //mdb.UpdateUserData(2, "Primr@123");
                //Response.Redirect("Login.aspx");
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

            var data = mdb.GetAllUsers();
            lstNewReq.DataSource = data;
            lstNewReq.DataBind();

        }

        //protected void UpdateUserData_ServerClick(object sender, EventArgs e)
        //{
        //    //int UID = int.Parse(Request.QueryString["id"]);

        //        mdb.UpdateUserData(2, "Primr@123");
        //        Response.Redirect("Login.aspx");

        //}

    }
}