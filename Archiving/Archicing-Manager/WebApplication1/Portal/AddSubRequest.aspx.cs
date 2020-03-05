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
    public partial class AddSubRequest : System.Web.UI.Page
    {
        subRequests mdb = new subRequests();
        Accounts mdd = new Accounts();
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Cookies["typeark"].Value;
            if (type == "Data Entry")
            {
                div_Error.Visible = false;
                div_success.Visible = false;
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("Span11")).Attributes["class"] = "icon-thumbnail bg-success";

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
                if (NumberOfBatches.Value != "" && input_text.Value != "" && DropDownList1.SelectedValue != "---" && DropDownList2.SelectedValue != "---")
                {
                    if (FileUpload1.HasFile)
                    {

                        subRequests md = new BLL.subRequests();
                        string Fuser = Request.Cookies["nameark"].Value;
                        string added = Request.Cookies["typeark"].Value;
                        int MID = md.GetMaxID();
                        int pathID = MID + 1;
                        Directory.CreateDirectory(Server.MapPath("~/SubAttached/" + pathID + "/"));
                        FileUpload1.SaveAs(Server.MapPath("~/SubAttached/" + pathID + "/") + FileUpload1.FileName);
                        CreateBackup();
                        string src = "~/SubAttached/" + pathID + "/" + FileUpload1.FileName;
                        // Session["category"]= DropDownList1.SelectedValue.ToString();
                        int lastID = 0;
                        mdb.addrequestToArchiving(input_text.Value, txtrBody.Value, Fuser, NumberOfBatches.Value, out lastID, DropDownList1.SelectedValue, DropDownList2.SelectedValue, txtyear.Value, src);
                         SendMail(lastID, Fuser.ToString(), input_text.Value.ToString(), DropDownList1.SelectedValue);
                        div_success.Visible = true;
                        clean();

                    }
                    else
                    {
                        div_Error.Visible = true;
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
            input_text.Value = "";
            NumberOfBatches.Value = "";
        }
        public void CreateBackup()
        {
            subRequests md = new BLL.subRequests();
            int MID = md.GetMaxID();
            int pathID = MID + 1;
            Directory.CreateDirectory(@"\\10.1.1.15\Backups\Archiving\SubAttached\" + pathID + "/");
            FileUpload1.SaveAs(@"\\10.1.1.15\Backups\Archiving\SubAttached\" + pathID + "/" + FileUpload1.FileName);
        }
        public void SendMail(int id, string username, string subject, string subtype)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("ArcReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("ArcReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                string usermail = mdd.getUserEmail(mdb.getRequestOwner(id));
                message.Subject = "New Submit Ticket Received";
                message.From = "ArcReply@Prime-Health.org";
                if(subtype=="InPatient")
                {
                   message.Body = " Dear Archiving Team you have received New Submit [Ticket # " + id + "] from " + username + "  Header : " + subject + " ";
                  message.ToRecipients.Add("Archiving@Prime-Health.org");
                }

                if (subtype=="OutPatient")
                {
                    message.Body = " Dear TPA Team you have received New Submit [Ticket # " + id + "] from " + username + "  Header : " + subject + " ";
                   message.ToRecipients.Add("TPA@Prime-Health.org");
                   message.ToRecipients.Add("TPAQuality@Prime-Health.org");
                }
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