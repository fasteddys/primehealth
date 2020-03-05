using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
//using WS.BLL;

namespace WS.Portal
{
    public partial class ManageRequest : System.Web.UI.Page
    {
        //Complaint mdb = new Complaint();
        //Accounts mdb2 = new Accounts();
        string sub = "";
        string body = "";
        string acManger = "";
        string UserComment = "";
        string AssignedPerson = "";
        string CAsign;
        string Status;
        string clientname;
        string email;
        string department;
        DateTime assignedpersoncommenttime;
  

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //string type = Request.Cookies["typeark"].Value;
                    //string user = Request.Cookies["nameark"].Value;
                    //int id = int.Parse(Request.QueryString["id"]);
                    //Label1.InnerText = id.ToString();
                    //mdb.GetDetailByid(id,ref sub,ref body, ref acManger,ref UserComment,ref AssignedPerson,ref Status, ref assignedpersoncommenttime,ref clientname,ref email,ref department);
                    //lbl_Creator.InnerHtml = acManger;
                    //lbl_Client.InnerHtml = clientname;
                    //lbl_Email.InnerHtml = email;
                    //lbl_Assignee.InnerHtml = AssignedPerson;
                    //if (assignedpersoncommenttime==DateTime.MinValue)
                    //{
                    //    lbl_Respond.InnerHtml = "";
                    //}
                    //else
                    //{
                    //    lbl_Respond.InnerHtml = assignedpersoncommenttime.ToString();
                    //}
                    //lbl_status.InnerHtml = Status;
                    //Lbl_department.InnerHtml = department;
                    //lbl_Sub.InnerHtml = sub;
                    //txtrBody.InnerHtml = body;
                    //txtrcomplaintreply.Text = UserComment;
                }
            }
                      
            catch (Exception ex)
            {


            }
        }

    }
}