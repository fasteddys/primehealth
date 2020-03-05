using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
using CallCenterNotesApp.DLL;

namespace CallCenterNotesApp.Portal
{
    public partial class CcEmailRequestShowPendingAssignDocReq : System.Web.UI.Page
    {
        CallcentereMailRequest mdb = new CallcentereMailRequest();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
            //Bind();
        }
        public void Bind()
        {
       
            string user = Request.Cookies["UserName"].Value;
            string type = Request.Cookies["UserType"].Value;

            if (type == "CallCenterDoctor")
            {
                var data = mdb.GetAllReqByCallcenterDoctor(user, "PendingAssignDocReq");
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
            else if (type == "CallCenterManager")
            {
                var data = mdb.GetAllReqBYCallcenterManager("PendingAssignDocReq");
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
            else if (type == "CallCenterUser")
            {
                var data = mdb.GetAllReqByCallcenterUser(user, "PendingAssignDocReq");
                lstNewReq.DataSource = data;
                lstNewReq.DataBind();
            }
            //ClaimApprovalDBDataContext db = new ClaimApprovalDBDataContext();
            //var x = from p in db.CallCenterEmailApprovals
            //        where p.ReqStatues == "PendingAssignDocReq"
            //        orderby p.ID descending
            //        select p;
            //lstNewReq.DataSource = x.ToList();
            //lstNewReq.DataBind();


        }
    }
}