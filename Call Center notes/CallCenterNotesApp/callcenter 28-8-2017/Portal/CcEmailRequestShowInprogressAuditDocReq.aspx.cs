using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;

namespace CallCenterNotesApp.Portal
{
    public partial class CcEmailRequestShowInprogressAuditDocReq : System.Web.UI.Page
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
                var data = mdb.GetAllReqByCallcenterDoctor(user, "InprogressAuditDocReq");
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
            else if (type == "CallCenterManager" || type == "CallCenterAuditDoctor")
            {
                var data = mdb.GetAllReqBYCallcenterManager("InprogressAuditDocReq");
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
            else if (type == "CallCenterUser")
            {
                var data = mdb.GetAllReqByCallcenterUser(user, "InprogressAuditDocReq");
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }

        }
    }
}