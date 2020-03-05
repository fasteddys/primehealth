using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.Exchange;
using Microsoft.Exchange.WebServices;
using Microsoft.Exchange.WebServices.Data;
using WebApplication1.BLL;

namespace WebApplication1.Users
{
    public partial class Portal : System.Web.UI.Page
    {
        Requests mdb = new Requests();
        Accounts mdd = new Accounts();

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Cookies["typeark"].Value;
            if (type == "User" || type == "Remb" || type=="Data Entry")
            {
                div_Error.Visible = false;
                div_success.Visible = false;
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("AddReqLi")).Attributes["class"] = "icon-thumbnail bg-success";

            }

            else
            {
                Response.Redirect("/Login.aspx");
            }

        }

        protected void SubmitBtn_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (input_text.Value != "" && NumberOfClaims.Value != "")
                {
                    if (FileUpload1.HasFile)
                    {

                        Requests md = new BLL.Requests();

                        int MID = md.GetMaxID();

                        int pathID = MID + 1;
                        Directory.CreateDirectory(Server.MapPath("~/Attached/" + pathID + "/"));
                        FileUpload1.SaveAs(Server.MapPath("~/Attached/" + pathID + "/") + FileUpload1.FileName);
                        CreateBackup();
                        string src = "~/Attached/" + pathID + "/" + FileUpload1.FileName;
                        string Fuser = Request.Cookies["nameark"].Value;
                        string added = Request.Cookies["typeark"].Value;
                        int lastID=0;
                        mdb.addrequestToArchiving(input_text.Value, txtrBody.Value, src, Fuser, added, NumberOfClaims.Value,out lastID);
                        SendMail(lastID,Fuser.ToString(),input_text.Value.ToString());
                        div_success.Visible = true;
                        clean();
                    }
                    else
                    {
                        string ath = Request.QueryString["attach"];
                        if (ath != null)
                        {
                            string src = ath;
                            string Fuser = Request.Cookies["nameark"].Value;
                            string added = Request.Cookies["typeark"].Value;
                            int lastID = 0;                            
                            mdb.addrequestToArchiving(input_text.Value, txtrBody.Value, src, Fuser, added, NumberOfClaims.Value,out lastID);
                            SendMail(lastID,Fuser.ToString(),input_text.Value.ToString());
                            div_success.Visible = true;
                            clean();
                        }
                        else
                        {
                            string src = "";
                            string Fuser = Request.Cookies["nameark"].Value;
                            string added = Request.Cookies["typeark"].Value;
                            int lastID = 0;
                            mdb.addrequestToArchiving(input_text.Value, txtrBody.Value, src, Fuser, added, NumberOfClaims.Value,out lastID);
                            SendMail(lastID,Fuser.ToString(),input_text.Value.ToString());
                            div_success.Visible = true;
                            clean();
                        }
                    }
                }
                else
                {
                    div_Error.Visible = true;
                }
            }
            catch (Exception ex)
            {
                div_Error.Visible = true;
            }
        }
        public void clean()
        {
            txtrBody.InnerHtml = "";
            FileUpload1.Attributes.Clear();
            input_text.Value = "";
            NumberOfClaims.Value = "";
        }

        public void CreateBackup()
        {
            Requests md = new BLL.Requests();
            int MID = md.GetMaxID();
            int pathID = MID + 1;
            Directory.CreateDirectory(@"\\10.1.1.15\Backups\Archiving\Attached\" + pathID + "/");
            FileUpload1.SaveAs(@"\\10.1.1.15\Backups\Archiving\Attached\" + pathID + "/" + FileUpload1.FileName);
        }

        public void SendMail(int id ,string username , string subject)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("ArcReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("ArcReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                string usermail=mdd.getUserEmail(mdb.getRequestOwner(id));
                message.Subject ="New Ticket Received";
                message.Body = " Dear Archiving Team [Ticket # " + id + "] has just been received from "+username+"  Header : "+subject+" ";
                message.From = "ArcReply@Prime-Health.org";
                message.ToRecipients.Add("Archiving@Prime-Health.org");
                message.CcRecipients.Add(usermail);
                message.Save();
                message.SendAndSaveCopy();
            }

            catch (Exception ex)
            {

            }
        }


    }
}