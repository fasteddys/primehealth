using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.Exchange;
using Microsoft.Exchange.WebServices.Data;
using WebApplication1.BLL;

namespace WebApplication1.Portal
{

    public partial class ManageRequest : System.Web.UI.Page
    {

        List<ListItem> files = new List<ListItem>();
        Requests mdb = new Requests();
        Accounts mdb2 = new Accounts();
        string sub = "";
        string body = "";
        string attch = "";
        string reply = "";
        string acManger = "";
        string UserComment = "";
        string AssignedPerson = "";
        string CAsign;
        string Status;
        string addedbytype;
        string rembpend;
        int reqesteditems=0;
        int founditemss=0;
        DateTime pendingtimestart;
        DateTime pendingtimeend;
        DateTime rembsubmittime;
        string pendingassignee;
        string pending;
        string pendingcomments;
        string assignedclaims;
        string submit;
        string userpendingclaimscomments;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string type = Request.Cookies["typeark"].Value;
                    string user = Request.Cookies["nameark"].Value;
                    btn_issue.Visible = false;
                    Downloadbtn.Visible = false;
                    btnback.Visible = false;
                    div_Error.Visible = false;
                    btn_update.Visible = false;
                    divclaims.Visible = false;
                    upload_div.Visible = false;
                    btnsolved.Visible = false;
                    btn_Mang.Visible = false;
                    btn_asign.Visible = false;
                    btn_confirm.Visible = false;
                    btnclaims.Visible = false;
                    adminassign.Visible = false;
                    btn_assign_claims_btn.Visible = false;
                    btnsubmit.Visible = false;
                    btnDataEntry.Visible = false;
                    btn_assign_dataentry.Visible = false;
                    btn_donot_Return_To_Archiving.Visible = false;
                    bttn_assign_dataentry_Finish.Visible = false;
                    int id = int.Parse(Request.QueryString["id"]);
                    Label1.InnerText = id.ToString();
                    mdb.GetDetailByid(id, ref sub, ref body, ref attch, ref reply, ref acManger, ref UserComment, ref CAsign, ref AssignedPerson, ref Status, ref addedbytype, ref rembpend, ref reqesteditems, ref founditemss, ref pendingtimestart, ref pendingtimeend, ref pendingassignee, ref pending, ref pendingcomments, ref assignedclaims, ref submit, ref rembsubmittime, ref userpendingclaimscomments);
                    lbl_Sub.InnerHtml = sub;
                    if (userpendingclaimscomments==null)
                    {
                        userpendigclaimscomment.Text ="";
                        
                    }
                    else
                    {
                        userpendigclaimscomment.Text = userpendingclaimscomments.ToString();
                    }
                    claims_assignee.InnerHtml = pendingassignee;
                    lbl_Creator.InnerHtml = acManger;
                    lbl_Assignee.InnerHtml = AssignedPerson;
                    pendingfrom.InnerHtml = pendingtimestart.ToString();
                    pendingto.InnerHtml = pendingtimeend.ToString();
                    lbl_status.InnerHtml = Status;
                    txtrBody.InnerHtml = body;
                    TextBox1.Text = UserComment;
                    TextBox2.Text = pendingcomments;
                    reqitems.Text = reqesteditems.ToString();
                    reqitems.Enabled = false;
                    pendingclaims.Visible = false;
                    var usersList = mdb2.GetAllUsers().Select(u => new ListItem
                    {
                        Text = u.UserName,
                        Value = u.id.ToString()
                    }).ToList();
                    drp1.Items.Add("----");
                    foreach (var item in usersList)
                    {
                        drp1.Items.Add(item);
                    }

                    if (type == "Admin" && Status != "Closed")
                    {
                        adminassign.Visible = true;
                    }
                    
                    if (type == "Admin" && Status == "Rejected" || type=="Admin" && Status=="Submitted")
                    {
                        adminassign.Visible = false;
                    }

                    if (type == "Admin" && Status == "Pending Claims")
                    {
                        adminassign.Visible = false;
                    }
                    if (Status=="Pending Claims" && type=="Data Entry")
                    {
                         btn_assign_claims_btn.Visible = true;
                        
                    }
                    if (Status == "Pending Claims" && assignedclaims == null)
                    {
                        btn_assign_claims_btn.Disabled = false;
                        btnsolved.Disabled = true;

                    }

                    if (assignedclaims=="true")
                    {
                        btn_assign_claims_btn.Disabled = true;
                        btnsolved.Disabled = false;
                    }

                    if (pending=="true")
                    {
                        pendingclaims.Visible = true;
                    }

                    if (Status == "Pending Confirmation" && user == mdb.getRequestOwner(id) && type=="User")
                    {
                        btnclaims.Visible = true;
                    }

                    if (Status=="Pending Claims" && type=="Data Entry")
                    {
                        btnsolved.Visible = true;
                    }

                    if (pendingtimestart==DateTime.MinValue)
                    {
                        pendingfrom.InnerHtml = "";
                    }

                    if (pendingtimeend==DateTime.MinValue)
                    {
                        pendingto.InnerHtml = "";
                    }

                    if (type=="EnterpriseAdmin")
                    {
                       btn_issue.Visible = false;
                       Downloadbtn.Visible = false;
                       div_Error.Visible = false;
                       btn_update.Visible = false;
                       upload_div.Visible = false;
                      btn_Mang.Visible = false;
                       btn_asign.Visible = false;
                       btn_confirm.Visible = false;
                       btn_reject.Visible = false;
                       btn_update.Visible = false;
                    }

                    if (Status == "Searching" && user == AssignedPerson)
                    {
                        upload_div.Visible = true;
                    }

                    if (Status == "Pending Confirmation"  && user == AssignedPerson)
                    {
                        btnback.Visible = true;
                    }

                    if (Status == "Rejected" && user == AssignedPerson)
                    {
                        btnback.Visible = true;
                    }

                    if (Status == "Pending Confirmation" && addedbytype == "Remb")
                    {
                        if (type=="EnterpriseAdmin")
                        {
                             btnback.Visible = false;
                        }

                        else if (type=="Admin" || type=="ArcAdmin")
                        {
                            btnback.Visible = true;
                        }

                        else
                        {
                            btnback.Visible = false;                            
                        }
                    }

                    if (Status == "Pending Confirmation" && addedbytype == "Data Entry")
                    {
                        if (type == "EnterpriseAdmin")
                        {
                            btnback.Visible = false;
                        }

                        else if (type == "Admin" || type == "ArcAdmin")
                        {
                            btnback.Visible = true;
                        }

                        else
                        {
                            btnback.Visible = false;
                        }
                    }

                    if (Status == "Pending Confirmation" || Status == "Rejected" || Status == "Closed")
                    {
                        if (reply == "" || reply==null)
                        {
                            Downloadbtn.Visible = false;
                        }

                        else
                        {
                            Downloadbtn.Visible = true;
                        }
                    }
                    if (user == AssignedPerson)
                    {
                        TextBox1.ReadOnly = false;
                        founditems.Enabled = true;
                    }
                    else
                    {
                        TextBox1.Enabled = false;
                        founditems.Enabled = false;
                    }

                    if (Status == "Rejected" && user == mdb.getRequestOwner(id))
                    {
                        btn_update.Visible = true;
                    }
                    else
                    {
                        btn_update.Visible = false;
                    }
                    if (founditemss == 0)
                    {
                        founditems.Text = "";
                    }

                    else
                    {
                        founditems.Text = founditemss.ToString();
                    }

                    try
                    {
                        string[] filePaths = Directory.GetFiles(Server.MapPath("~/Attached/" + id));
                        foreach (string filePath in filePaths)
                        {
                            files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                        }

                        downloadlist.DataSource = files;
                        downloadlist.DataBind();
                    }
                    catch (Exception ex)
                    {         
                    }

                    if (Status == "Rejected" && mdb.getRequestOwner(id) == user)
                    {
                        TextBox1.ReadOnly = false;
                        lbl_Sub.Disabled = false;
                        txtrBody.Disabled = false;
                        TextBox1.Enabled = true;
                        reqitems.Enabled = true;
                    }

                    if (Status == "Pending Confirmation" && mdb.getRequestOwner(id) == user)
                    {
                        TextBox1.ReadOnly = false;
                        lbl_Sub.Disabled = false;
                        txtrBody.Disabled = false;
                        TextBox1.Enabled = true;
                    }

                    if (type == "User")
                    {
                        btn_Mang.Visible = true;
                        btn_Mang.Disabled = true;
                        btn_asign.Visible = false;
                        btn_issue.Visible = false;
                        btn_reject.Visible = false;

                        if (Status == "Pending Confirmation")
                        {
                            if (mdb.getRequestOwner(id) == user)
                            {
                                btn_Mang.Disabled = false;

                            }
                        }
                        else
                        {
                            btn_Mang.Disabled = true;
                        }
                    }

                    if (Status == "Pending Confirmation" || Status == "Rejected")
                    {
                        founditems.Enabled = false;
                    }
                    if (Status=="PendingDataEntry")
                    {
                        if (assignedclaims == "true")
                        {
                            btn_assign_dataentry.Disabled = true;
                        }
                        else
                        {
                            btn_assign_dataentry.Disabled = false;
                        }
                    }
                    if (type=="Remb")
                    {
                        if (Status == "Pending Confirmation" && mdb.getRequestOwner(id) == user)
                         {
                             if (rembpend=="true")
                             {
                                  btnDataEntry.Visible = true;
                             }
                         }
                    }
                    if (Status=="PendingDataEntry")
                    {
                        if (type=="Data Entry")
                        {
                            btn_assign_dataentry.Visible = true;
                        }
                    }

                    if (type == "Data Entry")
                    {
                        if (Status == "PendingDataEntry" && pendingassignee == user)
                        {
                           bttn_assign_dataentry_Finish.Visible = true; 
                        }
                    }
               
                    if (type == "Remb")
                    {
                        btn_reject.Visible = false;
                        if (Status == "Pending Confirmation")
                        {
                            btn_Mang.Visible = false;
                            if (mdb.getRequestOwner(id) == user)
                            {
                                btn_confirm.Visible = true;

                            }
                            if (mdb.getRequestOwner(id) == user)
                            {
                                if (rembpend == "true")
                                {
                                    btn_confirm.Disabled = true;
                                    btnsubmit.Visible = true;

                                }
                            }

                            if (mdb.getRequestOwner(id) == user)
                            {
                                if (submit == "true")
                                {
                                    btn_confirm.Visible = false;
                                    btnsubmit.Disabled = true;
                                }
                            }
                        }
                    }

                    if (type == "Data Entry")
                    {
                        btn_reject.Visible = false;
                        if (Status == "Pending Confirmation")
                        {
                            btn_Mang.Visible = false;
                            if (mdb.getRequestOwner(id) == user)
                            {
                                btn_confirm.Visible = true;

                            }
                            if (mdb.getRequestOwner(id)==user)
                            {
                                if (rembpend == "true")
                                  {
                                       btn_confirm.Disabled = true;
                                       btnsubmit.Visible = true;
                                       btn_donot_Return_To_Archiving.Visible = true;
                                 }
                            }
                            if (mdb.getRequestOwner(id)==user)
                            {
                                 if (submit == "true")
                               {
                                   btn_confirm.Visible = false;
                                  btnsubmit.Disabled = true;
                               }
                            }
                        }
                    }
                    if (type == "ArcAdmin")
                    {
                        TextBox1.ReadOnly = false;
                        btn_issue.Visible = true;
                        btn_issue.Disabled = true;
                        btn_reject.Disabled = true;
                        btn_asign.Visible = true;
                        btn_asign.Disabled = true;

                        if (CAsign == "false")
                        {
                            btn_asign.Disabled = false;
                        }
                        else
                        {
                            btn_asign.Disabled = true;
                            btn_issue.Disabled = true;
                            btn_reject.Disabled = true;
                        }
                        if ((Status == "New" || Status == "Searching") && CAsign == "true")
                        {
                            if (user == AssignedPerson)
                            {
                                btn_issue.Disabled = false;
                                btn_reject.Disabled = false;
                            }
                        }
                        else
                        {
                            btn_issue.Disabled = true;
                            btn_reject.Disabled = true;

                        }
                        if (addedbytype == "Remb" && Status == "Submitted" && type == "ArcAdmin")
                        {

                            if (submit == "true")
                            {
                                btn_Mang.Visible = true;

                            }

                        }
                        if (addedbytype == "Data Entry" && Status == "Submitted" && type == "ArcAdmin")
                        {

                            if (submit == "true")
                            {
                                btn_Mang.Visible = true;
                            }
                        }
                    }
                    else if (type == "Admin" || type == "Archiving")
                    {
                        TextBox1.ReadOnly = false;
                        btn_issue.Visible = true;
                        btn_issue.Disabled = true;
                        btn_reject.Disabled = true;
                        btn_asign.Visible = true;
                        btn_asign.Disabled = true;
                        if (CAsign == "false")
                        {
                            btn_asign.Disabled = false;
                        }
                        else
                        {
                            btn_asign.Disabled = true;
                            btn_issue.Disabled = true;
                            btn_reject.Disabled = true;
                        }
                        if ((Status == "New" || Status == "Searching") && CAsign == "true")
                        {

                            if (user == AssignedPerson)
                            {
                                btn_issue.Disabled = false;
                                btn_reject.Disabled = false;
                            }
                        }
                        else
                        {
                            btn_issue.Disabled = true;
                            btn_reject.Disabled = true;
                        }

                        if (addedbytype == "Remb" && Status == "Submitted" && type == "Admin")
                        {
                            if (submit == "true")
                            {
                                btn_Mang.Visible = true;
                            }
                        }
                        if (addedbytype == "Data Entry" && Status == "Submitted" && type == "Admin")
                        {
                            if (submit == "true")
                            {
                                btn_Mang.Visible = true;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void btn_asign_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;
            mdb.updateAssignPerson(id, user);
            SendMail_Assigned(id, user);
            Server.Transfer("ManageRequest.aspx");
        }
        protected void btn_Transfer(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string user = drp1.SelectedItem.ToString();
            mdb.updateAssignPersonforAdmin(id, user);
            Server.Transfer("ManageRequest.aspx");
        }
        protected void btn_Download(object sender, EventArgs e)
        {

            try
            {
                int id = int.Parse(Request.QueryString["id"]);
                string sub = "";
                string body = "";
                string attch = "";
                string reply = "";
                string acManger = "";
                string UserComment = "";
                string AssignedPerson = "";
                string CAsign = "";
                string Status = "";
                string addedbytype = "";
                string rembpend = "";
                int reqesteditems = 0;
                int founditemss = 0;

                mdb.GetDetailByid(id, ref sub, ref body, ref attch, ref reply, ref acManger, ref UserComment, ref CAsign, ref AssignedPerson, ref Status, ref addedbytype, ref rembpend, ref reqesteditems, ref founditemss, ref pendingtimestart, ref pendingtimeend, ref pendingassignee, ref pending, ref pendingcomments, ref assignedclaims, ref submit, ref rembsubmittime, ref userpendingclaimscomments);
                string filePath = reply;
                Response.ContentType = ContentType;
                Response.Clear();
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                Response.TransmitFile(filePath);
                Response.End();

                
            }

            catch (Exception ex)
            {
                
            }

        }
        protected void btn_Claims_ServerClick(object sender, EventArgs e)
        {
            MainDiv.Visible = false;
            divclaims.Visible = true;

        }
        protected void btn_Solved_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.EndPendingClaims(id,TextBox2.Text.ToString());
            SendMail_Solved(id);
            Server.Transfer("ManageRequest.aspx");
        }
        protected void btn_Confirm_DataEntry_User(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.ClosedByDataEntry(id);
            SendMail_ClosedByDataEntry(id);
            Server.Transfer("ManageRequest.aspx");
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    Requests md = new BLL.Requests();
                    int id = int.Parse(Request.QueryString["id"]);
                    int pathID = id;
                    Directory.CreateDirectory(Server.MapPath("~/Reply/" + pathID + "/"));
                    FileUpload1.SaveAs(Server.MapPath("~/Reply/" + pathID + "/") + FileUpload1.FileName);
                    CreateBackup();
                    string replly = "~/Reply/" + pathID + "/" + FileUpload1.FileName;
                    FileUpload1.Enabled = false;
                    Button1.Disabled = true;
                    mdb.Upload(id, replly);

                }

                else
                {

                }
            }

            catch
            {

            }

        }
        protected void btn_Update_ServerClick(object sender, EventArgs e)
        {

            int id = int.Parse(Request.QueryString["id"]);
            string sub = "";
            string body = "";
            string attch = "";
            string reply = "";
            string acManger = "";
            string UserComment = "";
            string AssignedPerson = "";
            string CAsign = "False";
            string Status = "New";
            string addedbytype = "";
            string rembpend = "";
            int reqesteditems = 0;
            int founditemss = 0;

            mdb.GetDetailByid(id, ref sub, ref body, ref attch, ref reply, ref acManger, ref UserComment, ref CAsign, ref AssignedPerson, ref Status, ref addedbytype, ref rembpend, ref reqesteditems, ref founditemss, ref pendingtimestart, ref pendingtimeend, ref pendingassignee, ref pending, ref pendingcomments, ref assignedclaims, ref submit, ref rembsubmittime, ref userpendingclaimscomments);

            mdb.FW_Rejected(id, sub, int.Parse(reqitems.Text),txtrBody.InnerHtml);
            SendMail_Forwarded(id);
            btn_update.Visible = false;


        }
        protected void btn_Back_ServerClick(object sender, EventArgs e)
        {

            int id = int.Parse(Request.QueryString["id"]);
            string sub = "";
            string body = "";
            string attch = "";
            string reply = "";
            string acManger = "";
            string UserComment = "";
            string AssignedPerson = "";
            string CAsign = "False";
            string Status = "New";
            string addedbytype = "";
            string rembpend = "";
            int reqesteditems = 0;
            int founditemss = 0;

            mdb.GetDetailByid(id, ref sub, ref body, ref attch, ref reply, ref acManger, ref UserComment, ref CAsign, ref AssignedPerson, ref Status, ref addedbytype, ref rembpend, ref reqesteditems, ref founditemss, ref pendingtimestart, ref pendingtimeend, ref pendingassignee, ref pending, ref pendingcomments, ref assignedclaims, ref submit, ref rembsubmittime, ref userpendingclaimscomments);

            mdb.BacktoArchiving(id);

            Server.Transfer("ManageRequest.aspx");

        }
        public void CreateBackup()
        {
            Requests md = new BLL.Requests();
            int id = int.Parse(Request.QueryString["id"]);
            int pathID = id;
            Directory.CreateDirectory(@"\\10.1.1.15\Backups\Archiving\Reply\" + pathID + "/");
            FileUpload1.SaveAs(@"\\10.1.1.15\Backups\Archiving\Reply\" + pathID + "/" + FileUpload1.FileName);
        }
        protected void btn_reject_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;
            if (founditems.Text == "")
            {
                div_Error.Visible = true;

            }

            else
            {
                mdb.UpdateRejected(id, TextBox1.Text, int.Parse(founditems.Text));
                string usermail = mdb2.getUserEmail(mdb.getRequestOwner(id));
                SendMail_rejected(id, TextBox1.Text, usermail, user);
                Server.Transfer("ManageRequest.aspx");
            }

        }
        protected void btn_asign_confirm(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;
            mdb.updateRembConfirm(id);
            Server.Transfer("ManageRequest.aspx");
        }
        protected void btn_Submit_Claims_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.UpdatePendingClaims(id, txtrBody.InnerText.ToString(), userclaimstxt.Text);
            MainDiv.Visible = true;
            SendMail_Pending_Claims(id, userclaimstxt.Text);
            Server.Transfer("ManageRequest.aspx");
        }
        protected void btn_asign_claims(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;
            mdb.updatependingclaimsassignee(id , user);
            Server.Transfer("ManageRequest.aspx");
        }
        protected void btn_asign_dataentry(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;
            mdb.updatependingDataentryassignee(id, user);
            Server.Transfer("ManageRequest.aspx");
        }
        protected void btn_asign_dataentry_Finish(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;
            mdb.updatesubmitremb(id);
            SendMail_Pending_DataEntry_finish(id);
            Server.Transfer("ManageRequest.aspx");
        }
        protected void btn_issue_ServerClick(object sender, EventArgs e)
        {

           try
           {
               int id = int.Parse(Request.QueryString["id"]);
               string user = Request.Cookies["nameark"].Value;
               if (founditems.Text == "")
               {
                   div_Error.Visible = true;


               }
               else
               {
                   mdb.UpdatePending(id, TextBox1.Text, int.Parse(founditems.Text));
                  string usermail= mdb2.getUserEmail(mdb.getRequestOwner(id));
                   SendMail_found(id, TextBox1.Text,usermail,user);
                   Server.Transfer("ManageRequest.aspx");
               }
           }

            catch
           {
               div_Error.Visible = true;
           }

        }
        protected void btn_Mang_ServerClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.UpdateClosedArchiving(id);
            SendMail_Closed(id);
            Server.Transfer("ManageRequest.aspx");
        }
        protected void btn_submit(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            mdb.updatesubmitremb(id);
            SendMail_Submitted(id);
            Server.Transfer("ManageRequest.aspx");
        }
        protected void btn_dataentry(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            string user = Request.Cookies["nameark"].Value;
            mdb.updatetopendingdataentry(id);
            SendMail_Pending_DataEntry(id);
            Server.Transfer("ManageRequest.aspx");
        }
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }
        public void SendMail_Assigned(int id, string username)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("ArcReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("ArcReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Ticket Assigned";
                message.Body = " Dear  "+ mdb.getRequestOwner(id) +" [Ticket # " + id + "] was assigned to " + username +  ".";
                message.From = "ArcReply@Prime-Health.org";
                message.ToRecipients.Add(mdb2.getUserEmail(mdb.getRequestOwner(id)));
                message.CcRecipients.Add("Archiving@Prime-Health.org");
                message.Save();
                message.SendAndSaveCopy();
            }

            catch (Exception ex)
            {

            }
        }
        public void SendMail_found(int id, string comment,string usermail,string assignee)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("ArcReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("ArcReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Ticket Found";
                if (comment=="")
                {
                    message.Body = " Dear " + mdb.getRequestOwner(id) + " [Ticket # " + id + "] has just been Found and Pending for your Confirmation.";

                }
                else
                {
                    message.Body = " Dear " + mdb.getRequestOwner(id) + " [Ticket # " + id + "] has just been Found and Pending for your Confirmation as " + assignee + " commented : " + comment + ".";

                }
                message.From = "ArcReply@Prime-Health.org";
                message.ToRecipients.Add(usermail);
                message.CcRecipients.Add("Archiving@prime-health.org");
                message.Save();
                message.SendAndSaveCopy();
            }

            catch (Exception ex)
            {

            }
        }
        public void SendMail_rejected(int id, string comment, string usermail, string assignee)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("ArcReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("ArcReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Ticket Rejected";
                if (comment=="")
                {
                    message.Body = " Dear " + mdb.getRequestOwner(id) + "[Ticket # " + id + "] has just been Rejected.";

                }

                else
                {
                    message.Body = " Dear " + mdb.getRequestOwner(id) + "[Ticket # " + id + "] has just been Rejected as " + assignee + " commented : " + comment + " .";
                }
                message.From = "ArcReply@Prime-Health.org";
                message.ToRecipients.Add(usermail);
                message.CcRecipients.Add("Archiving@prime-health.org");
                message.Save();
                message.SendAndSaveCopy();
            }

            catch (Exception ex)
            {

            }
        }
        public void SendMail_Closed(int id)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("ArcReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("ArcReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Ticket Closed";
                message.Body = " Dear Archiving Team  [Ticket # " + id + "] has just been Closed.";
                message.From = "ArcReply@Prime-Health.org";
                message.ToRecipients.Add("Archiving@Prime-Health.org");
                message.CcRecipients.Add(mdb2.getUserEmail(mdb.getRequestOwner(id)));
                message.Save();
                message.SendAndSaveCopy();
            }

            catch (Exception ex)
            {

            }
        }
        public void SendMail_Forwarded(int id)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("ArcReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("ArcReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Ticket Forwarded";
                message.Body = " Dear Archiving Team  [Ticket # " + id + "] has just been Forwarded as a new Ticket.";
                message.From = "ArcReply@Prime-Health.org";
                message.ToRecipients.Add("Archiving@Prime-Health.org");
                message.CcRecipients.Add(mdb2.getUserEmail(mdb.getRequestOwner(id)));
                message.Save();
                message.SendAndSaveCopy();
            }

            catch (Exception ex)
            {

            }
        }
        public void SendMail_Submitted(int id)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("ArcReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("ArcReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Ticket Submitted";
                message.Body = " Dear Archiving Team  [Ticket # " + id + "] has just been Submitted to you by " +mdb.getRequestOwner(id) +".";
                message.From = "ArcReply@Prime-Health.org";
                message.ToRecipients.Add("Archiving@Prime-Health.org");
                message.CcRecipients.Add(mdb2.getUserEmail(mdb.getRequestOwner(id)));
                message.Save();
                message.SendAndSaveCopy();
            }

            catch (Exception ex)
            {

            }
        }
        public void SendMail_Solved(int id)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("ArcReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("ArcReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Ticket Solved";
                message.Body = " Dear Archiving Team  [Ticket # " + id + "] has just been Solved by " + mdb.getRequestclaimsassignee(id) + ". Now ticket is in Searching process.";
                message.From = "ArcReply@Prime-Health.org";
                message.ToRecipients.Add("Archiving@Prime-Health.org");
                message.CcRecipients.Add(mdb2.getUserEmail(mdb.getRequestOwner(id)));
                message.Save();
                message.SendAndSaveCopy();
            }

            catch (Exception ex)
            {

            }
        }
        public void SendMail_ClosedByDataEntry(int id)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("ArcReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("ArcReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Ticket Closed By DataEntry";
                message.Body = " Dear Archiving Team  [Ticket # " + id + "] has just been Closed by  "+mdb.getRequestOwner(id)+" .";
                message.From = "ArcReply@Prime-Health.org";
                message.ToRecipients.Add("Archiving@Prime-Health.org");
                message.CcRecipients.Add(mdb2.getUserEmail(mdb.getRequestOwner(id)));
                message.Save();
                message.SendAndSaveCopy();
            }

            catch (Exception ex)
            {

            }
        }
        public void SendMail_Pending_Claims(int id,string userclaimscomment)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("ArcReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("ArcReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Ticket is in Pending Claims Process";
                if (userclaimscomment=="")
                {
                                    message.Body = " Dears [Ticket # " + id + "] is in Pending Claims process.";
                }
                else
                {
                    message.Body = " Dears [Ticket # " + id + "] is in Pending Claims process as " + mdb.getRequestOwner(id) + " Commented  " + userclaimscomment + ".";

                }
                message.From = "ArcReply@Prime-Health.org";
                message.ToRecipients.Add("Ayman.Mohamed@Prime-Health.org");
                message.ToRecipients.Add("Sherif.Nadim@Prime-Health.org");
                message.CcRecipients.Add(mdb2.getUserEmail(mdb.getRequestOwner(id)));
                message.CcRecipients.Add("Archiving@Prime-Health.org");
                message.Save();
                message.SendAndSaveCopy();
            }

            catch (Exception ex)
            {

            }
        }
        public void SendMail_Pending_DataEntry(int id)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("ArcReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("ArcReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Ticket is in Pending DataEntry Process";
                message.Body = " Dears [Ticket # " + id + "] is in Pending DataEntry process.";
                message.From = "ArcReply@Prime-Health.org";
                message.ToRecipients.Add("Ayman.Mohamed@Prime-Health.org");
                message.ToRecipients.Add("Sherif.Nadim@Prime-Health.org");
                message.CcRecipients.Add(mdb2.getUserEmail(mdb.getRequestOwner(id)));
                message.CcRecipients.Add("Archiving@Prime-Health.org");
                message.Save();
                message.SendAndSaveCopy();
            }

            catch (Exception ex)
            {

            }
        }
        public void SendMail_Pending_DataEntry_finish(int id)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("ArcReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("ArcReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Ticket Has Been Submitted Back";
                message.Body = " Dear Archiving Team [Ticket # " + id + "] is Submitted Back To you.";
                message.From = "ArcReply@Prime-Health.org";
                message.ToRecipients.Add("Archiving@Prime-Health.org");
                message.CcRecipients.Add(mdb2.getUserEmail(mdb.getRequestOwner(id)));
                message.CcRecipients.Add("Ayman.Mohamed@Prime-Health.org");
                message.CcRecipients.Add("Sherif.Nadim@Prime-Health.org");
                message.Save();
                message.SendAndSaveCopy();
            }

            catch (Exception ex)
            {

            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }
    }
}