using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallCenterNotesApp.BLL;
using CallCenterNotesApp.DLL;

namespace CallCenterNotesApp.Portal
{
    public partial class CcEmailRequestShowPendingAssignEmailReq : System.Web.UI.Page
    {
        CallcentereMailRequest mdb = new CallcentereMailRequest();

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{

            //}
            // Bind();
            PhNetworkEntities db = new PhNetworkEntities();
            var x = from p in db.EmailApprovalRequests
                    where p.RequstStatusID == 11111
                    orderby p.ID descending
                    select p;
            lstNewReq.DataSource = x.ToList();
            lstNewReq.DataBind();
        }
        //public void Bind()
        //{
        //    string user = Request.Cookies["UserName"].Value;
        //    string type = Request.Cookies["UserType"].Value;
        //    if (type == "CallCenterDoctor")
        //    {
        //        var data = mdb.GetAllReqByCallcenterDoctor(user, "PendingAssignEmailReq");
        //        lstNewReq.DataSource = data;
        //        lstNewReq.DataBind();
        //    }
        //    else if (type == "CallCenterManager")
        //    {
        //        var data = mdb.GetAllReqBYCallcenterManager("PendingAssignEmailReq");
        //        lstNewReq.DataSource = data;
        //        lstNewReq.DataBind();
        //    }
        //    else if (type == "CallCenterUser")
        //    {
        //        var data = mdb.GetAllReqByCallcenterUserWhenAssign("PendingAssignEmailReq");
        //        lstNewReq.DataSource = data;
        //        lstNewReq.DataBind();
        //    }

        //}
        //public void Bind()
        //{
        //    string user = Request.Cookies["UserName"].Value;
        //    string type = Request.Cookies["UserType"].Value;

        //    if (type == "CallCenterDoctor")
        //    {
        //        var data = mdb.GetAllReqByCallcenterDoctor(user, "PendingAssignDocReq");
        //        lstNewReq.DataSource = data;
        //        lstNewReq.DataBind();
        //    }
        //    else if (type == "CallCenterManager")
        //    {
        //        var data = mdb.GetAllReqBYCallcenterManager("PendingAssignDocReq");
        //        lstNewReq.DataSource = data;
        //        lstNewReq.DataBind();
        //    }
        //    else if (type == "CallCenterUser")
        //    {
        //        var data = mdb.GetAllReqByCallcenterUser(user, "PendingAssignDocReq");
        //        lstNewReq.DataSource = data;
        //        lstNewReq.DataBind();
        //    }

    }
    
}