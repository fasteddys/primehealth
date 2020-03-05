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
    public partial class AddAccountAdmin : System.Web.UI.Page
    {
        Accounts mdb = new Accounts();
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Cookies["typeark"].Value;
            string user = Request.Cookies["nameark"].Value;
            if (type == "EnterpriseAdmin")
            {
                div_Error.Visible = false;
                div_success.Visible = false;
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("MainAdminLi")).Attributes["class"] = "icon-thumbnail bg-success";
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("UsersAdminLi")).Attributes["class"] = "open";
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("spanarrow")).Attributes["class"] = "arrow open";
                ((System.Web.UI.HtmlControls.HtmlGenericControl)Page.Master.FindControl("Span8")).Attributes["class"] = "icon-thumbnail bg-success";

            }

            else
            {
                Response.Redirect("/Login.aspx");
            }
        }
        protected void SubmitBtn_ServerClick(object sender, EventArgs e)
        {
            if (BatchNumTxt.Value == "" || ClaimsNUmtxt.Value == "")
            {
                div_Error.Visible = true;
            }
            else
            {
                bool add = mdb.adduser(BatchNumTxt.Value, ClaimsNUmtxt.Value, TypeDLL.SelectedValue,Emailtxt.Value);
                if (add)
                {
                    SendMail_Welcome_To_Archiving(BatchNumTxt.Value, ClaimsNUmtxt.Value, Emailtxt.Value);
                    clear();
                    div_success.Visible = true;
                }
                else
                {
                    clear();
                    div_Error.Visible = true;
                }
            }
        }
        public void clear()
        {
            BatchNumTxt.Value = "";
            ClaimsNUmtxt.Value = "";
            TypeDLL.SelectedValue = "";
            Emailtxt.Value = "";
        }
        public void SendMail_Welcome_To_Archiving(string username,string password,string mail)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("ArcReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("ArcReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Welcome To Archiving System";
                message.Body = " Dear '" + username + "' Welcome To Archiving System, Kindly click on this URL : http://10.1.1.28  to access archiving System , Your Username is (Please Copy & paste it into system ) : '" + username + "',Your Password is  (Please Copy&paste it into system)-(You can change it later) : '" +password+ "'.";
                message.From = "ArcReply@Prime-Health.org";
                message.ToRecipients.Add(Emailtxt.Value);
                message.Save();
                message.SendAndSaveCopy();
            }

            catch (Exception ex)
            {

            }
        }

    }
}