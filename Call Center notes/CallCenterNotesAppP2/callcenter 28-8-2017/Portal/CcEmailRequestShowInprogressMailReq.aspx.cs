using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;


namespace CallCenterNotesApp.Portal
{
    public partial class CcEmailRequestShowAllInprogressMailReq : System.Web.UI.Page
    {
        CallcentereMailRequest mdb = new CallcentereMailRequest();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }
        public void Bind()
        {
            string user = Request.Cookies["UserName"].Value;
            string type = Request.Cookies["UserType"].Value;
            if (type == "CallCenterDoctor")
            {
                var data = mdb.GetAllReqByCallcenterDoctor(user, "InprogressMailReq");
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
            else if (type == "CallCenterManager")
            {
                var data = mdb.GetAllReqBYCallcenterManager("InprogressMailReq");
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
            else if (type == "CallCenterUser")
            {
                var data = mdb.GetAllReqByCallcenterUser(user, "InprogressMailReq");
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }

        }
    }
}