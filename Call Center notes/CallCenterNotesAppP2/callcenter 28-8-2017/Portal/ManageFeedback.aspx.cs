using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Web.UI;
using CallCenterNotesApp.BLL;
using CallCenterNotesApp.DLL;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;

using Microsoft.Exchange.WebServices.Data;
using System.Web.Mail;

namespace CallCenterNotesApp.Portal
{
    public partial class ManageFeedback : System.Web.UI.Page
    {
        PhNetworkEntities db = new PhNetworkEntities();
        ClaimsApprovalClass mdb = new ClaimsApprovalClass();

        protected void Page_Load(object sender, EventArgs e)
        {

            string Usernamestr = "";
            string MedicalIDstr = "";
            string FBReqTitlestr = "";
            string FBSubjectstr = "";
            string FBCreationDatestr = "";
            string PrimeHealthCommentHeaderstr = "";
            string PrimeHealthCommentstr = "";
            string StatusStr = "";
            string RepliedPersonstr = "";
            string RepliedDatestr = "";

            if (!IsPostBack)
            {
                div_Success_update.Visible = false;
                div_Error_update.Visible = false;
                int id = int.Parse(Request.QueryString["id"]);
                FeedbackNum.InnerText = id.ToString();
                string type = Request.Cookies["UserType"].Value.Trim();
                string UserName = Request.Cookies["UserName"].Value.Trim();

                mdb.GetFeedbackRequestDetailsByid(id, ref Usernamestr, ref MedicalIDstr, ref FBReqTitlestr, ref FBSubjectstr, ref PrimeHealthCommentstr, ref StatusStr, ref RepliedPersonstr, ref RepliedDatestr, ref PrimeHealthCommentHeaderstr, ref FBCreationDatestr);

                Username.InnerText = Usernamestr;
                MedicalID.InnerText = MedicalIDstr;
                FBReqTitle.InnerText = FBReqTitlestr;
                FBSubject.InnerText = FBSubjectstr;
                FBCreationTime.InnerText = FBCreationDatestr;
                PrimeHealthComment.InnerText = PrimeHealthCommentstr;
                if(PrimeHealthCommentHeaderstr==null)
                {
                    PrimeHealthCommentHeaderstr = "re-" + FBSubjectstr;
                }

                PrimeHealthCommentHeader.Value = PrimeHealthCommentHeaderstr;
                RepliedPerson.InnerText = RepliedPersonstr;
                RepliedDate.InnerText = RepliedDatestr;


                btn_Reply.Visible = false;
                RepliedPersonDiv.Visible = false;
                RepliedDateDiv.Visible = false;
                /////////////////////////////////////////////////////////

                if (type == "DirectorUser")
                {
                    if(StatusStr != "Yes")
                    {
                        btn_Reply.Visible = true;
                    }
                    else
                    {
                        RepliedPersonDiv.Visible = true;
                        RepliedDateDiv.Visible = true;

                    }
                }
                
            }


        }
        protected void UpdateFeedback_ServerClick(object sender, EventArgs e)
        {
            string Usernamestr = "";
            string MedicalIDstr = "";
            string FBReqTitlestr = "";
            string FBSubjectstr = "";
            string FBCreationDatestr = "";
            string PrimeHealthCommentHeaderstr = "";
            string PrimeHealthCommentstr = "";
            string StatusStr = "";
            string RepliedPersonstr = "";
            string RepliedDatestr = "";
            int id = int.Parse(Request.QueryString["id"]);
            mdb.GetFeedbackRequestDetailsByid(id, ref Usernamestr, ref MedicalIDstr, ref FBReqTitlestr, ref FBSubjectstr, ref PrimeHealthCommentstr, ref StatusStr, ref RepliedPersonstr, ref RepliedDatestr, ref PrimeHealthCommentHeaderstr, ref FBCreationDatestr);


            string UserName = Request.Cookies["UserName"].Value.Trim();

            ////////////////////////////////////////////////////////////////////////////////////
            string ClientUsername = "", ClientEmail = "", ClientType = "";
            mdb.GetUserDataByMedicalID(MedicalIDstr, ref ClientUsername, ref ClientEmail, ref ClientType);
            try
            {
                if (ClientEmail != null)
                {
                    ExchangeService service = new ExchangeService();
                    service.AutodiscoverUrl("noreply@prime-health.org");
                    service.UseDefaultCredentials = false;
                    service.Credentials = new WebCredentials("noreply", "NoP@ssw0rd", "primehealth.local");
                    EmailMessage message = new EmailMessage(service);
                    message.Subject = "You have recieved reply on your Feedback ";
                    message.Body = " Dear " + ClientUsername + ",\r\n" + " Your Feedback has been viewed and you have recieved reply \r\n " + " ' thank you for contact us '";
                    message.From = "noreply@Prime-Health.org";
                    message.ToRecipients.Add(ClientEmail);

                     
                    //Directory.CreateDirectory(Server.MapPath("~/AttachedCallCenter/" + id + "/"));
                    //FileUpload1.SaveAs(Server.MapPath("~/AttachedCallCenter/" + id + "/") + FileUpload1.FileName);

                    //message.Attachments.AddFileAttachment(Server.MapPath("~/AttachedCallCenter/" + id + "/") + FileUpload1.FileName);
                    // message.BccRecipients.Add("Mostafa.Mahmoud@Prime-Health.org");

                     message.BccRecipients.Add("eugenie.ezzat@prime-health.org");
                    message.Save();
                    message.SendAndSaveCopy();
                }
                if (PrimeHealthCommentHeader.Value != "" && PrimeHealthComment.Value != "")
                {
                    mdb.UpdateFeedback(id, PrimeHealthCommentHeader.Value, PrimeHealthComment.Value, UserName);
                    div_Success_update.Visible = true;
                    btn_Reply.Visible = false;
                    Response.Redirect("~/Portal/ShowAllFeedback.aspx");
                }
                else
                {
                    div_Error_update.Visible = true;

                }
            }
            catch (Exception ex)
            {

            }
 
            ////////////////////////////////////////////////////////////////////////////////////

           
        }
        protected void SeenFeedback_ServerClick(object sender, EventArgs e)
        {
            string UserName = Request.Cookies["UserName"].Value.Trim();
            string Usernamestr = "";
            string MedicalIDstr = "";
            string FBReqTitlestr = "";
            string FBSubjectstr = "";
            string FBCreationDatestr = "";
            string PrimeHealthCommentHeaderstr = "";
            string PrimeHealthCommentstr = "";
            string StatusStr = "";
            string RepliedPersonstr = "";
            string RepliedDatestr = "";
            int id = int.Parse(Request.QueryString["id"]);
            mdb.GetFeedbackRequestDetailsByid(id, ref Usernamestr, ref MedicalIDstr, ref FBReqTitlestr, ref FBSubjectstr, ref PrimeHealthCommentstr, ref StatusStr, ref RepliedPersonstr, ref RepliedDatestr, ref PrimeHealthCommentHeaderstr, ref FBCreationDatestr);
            mdb.Update_To_Seen(id, "Re -  " + FBReqTitle.InnerText, UserName);
            Response.Redirect("~/Portal/ShowAllFeedback.aspx");


        }

    
    }
}