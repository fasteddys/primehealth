using CardCycle.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;// for backgroundworker class
using System.Net;
using System.Net.Mail;
using System.Threading;
using Microsoft.Exchange.WebServices.Auth;
using Microsoft.Exchange.WebServices.Data;

namespace CardCycle
{
    public partial class WebForm5 : System.Web.UI.Page
    {
       
        Request mdb = new Request();
        protected void Page_Load(object sender, EventArgs e)
        {
            

            try
            {

            div_success.Visible = false;
            div_Error.Visible = false;
            div1.Visible = false;
            ExcepC.Visible = false;
            //input_text.Value = "Updated";
            if (!IsPostBack)
            {
                int mid = mdb.GetMaxID();
                Label1.InnerHtml = (mid+1).ToString();
                string id= Request.QueryString["id"];
                string sub = Request.QueryString["lbl"];
                string crcr = Request.QueryString["Cardsss"];

                string body="";
                try
                {
                     body= Request.Cookies["txtBody"].Value.ToString();
                }
                catch (NullReferenceException)
                { }
                string type = Request.QueryString["lbl2"];
                //string ClientName = Request.QueryString["lbl3"];

                if (id != null && sub != null && body != null)
                {
                    
                    input_text.Value ="FW"+ " "+id ;
                    string B=body.Replace("%0d%0a", "\n");
                    txtrBody.InnerHtml = B;
                    DropDownList1.Text = type;
                    //DropDownList2.SelectedItem = ClientName;
                    CardsNum.Text = crcr;
                }
            }
            }
                catch(Exception)
            {
                Response.Redirect("login.aspx");  
            }

            if (DropDownList1.Text == "Exceptional-Addition" || DropDownList1.Text == "Exceptional-Transfer" || DropDownList1.Text == "Exception-Cancellation" || DropDownList1.Text == "Exception-Modification")
                ExcepC.Visible = true;
        }
        protected void Add_requestToIssue(object sender, EventArgs e)
        {
           try
            {
                if ((DropDownList1.Text != "----Select----" && DropDownList1.Text != "Exception" && DropDownList2.SelectedItem.ToString() != "----Select----" && CardsNum.Text != null )
                      || (DropDownList1.Text == "Exceptional-Addition" || DropDownList1.Text == "Exceptional-Transfer" || DropDownList1.Text == "Exception-Cancellation" || DropDownList1.Text == "Exception-Modification" && DropDownList2.SelectedItem.ToString() != "----Select----" && CardsNum.Text != null && ExcepComments.Value != ""))
                {                                               // in case not exceptions
                    if (FileUpload1.HasFile)
                    {
                        //Get latest id for save
                        Request md = new BLL.Request();
                        int MID = md.GetMaxID();
                        //imgNews.Visible = true;
                        int pathID = MID + 1;
                        //////////////////////////////////
                        // Directory.CreateDirectory("~/Attached/"+pathID+"/");
                        Directory.CreateDirectory(Server.MapPath("~/Attached/" + pathID + "/"));
                        FileUpload1.SaveAs(Server.MapPath("~/Attached/" + pathID + "/") + FileUpload1.FileName);

                        try
                        {
                            Directory.CreateDirectory("E:\\Attached\\" + pathID + "\\");
                            FileUpload1.SaveAs("E:\\Attached\\" + pathID + "\\" + FileUpload1.FileName);

                            Directory.CreateDirectory(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Attached\" + pathID + "/");
                         FileUpload1.SaveAs(@"\\10.1.1.15\Backups\Developers Backup\4-CCM\Attached\" + pathID + "/" + FileUpload1.FileName);



                    }
                        catch
                    {
                        mdb.SendEmailBackupFail();

                    }

                    string src = "";
                        string AccSrc = "~/Attached/" + pathID + "/" + FileUpload1.FileName;
                        string Fuser = Request.Cookies["name"].Value;

                        if (DropDownList1.Text == "Exceptional-Addition"|| DropDownList1.Text == "Exception-Modification" || DropDownList1.Text == "Exceptional-Transfer" || DropDownList1.Text == "Exception-Cancellation")
                        {
                            try
                            {
                                mdb.SendEmailEpecialRequest(); 
                                mdb.addrequestToException(input_text.Value, txtrBody.Value, src, Fuser, DropDownList1.Text, DropDownList2.SelectedItem.ToString(), CardsNum.Text, ExcepComments.Value , AccSrc);
                            }
                            catch 
                            {
                                div1.Visible = true; 
                            }
                           
                        }
                        else
                        {
                            mdb.addrequestToIssuance(input_text.Value, txtrBody.Value, src, Fuser, DropDownList1.Text, DropDownList2.SelectedItem.ToString(), CardsNum.Text, AccSrc );
                        }
                        div_success.Visible = true;
                        clean();
                    }
                    else
                    {
                        string ath = Request.QueryString["accattach"];
                        
                        if (ath != null)
                        {
                            string src = "";
                            string accountsrc = ath;

                            string Fuser = Request.Cookies["name"].Value;
                            if (DropDownList1.Text == "Exceptional-Addition" || DropDownList1.Text == "Exceptional-Transfer" || DropDownList1.Text == "Exception-Cancellation" || DropDownList1.Text == "Exception-Modification")
                            {
                                try
                                {
                                    mdb.SendEmailEpecialRequest();
                                    mdb.addrequestToException(input_text.Value, txtrBody.Value, src, Fuser, DropDownList1.Text, DropDownList2.SelectedItem.ToString(), CardsNum.Text, ExcepComments.Value, accountsrc);
                                }
                                catch 
                                { 
                                    div1.Visible = true; 
                                }
                            }
                            else
                            {
                                mdb.addrequestToIssuance(input_text.Value, txtrBody.Value, src, Fuser, DropDownList1.Text, DropDownList2.SelectedItem.ToString(), CardsNum.Text, accountsrc);
                            } 
                            div_success.Visible = true;
                            clean();
                        }
                        else
                        {
                            string src = "";
                            string accountsrc = "";
                            string Fuser = Request.Cookies["name"].Value;
                            if (DropDownList1.Text == "Exceptional-Addition" || DropDownList1.Text == "Exceptional-Transfer" || DropDownList1.Text == "Exception-Cancellation" || DropDownList1.Text == "Exception-Modification")
                            {
                                try
                                {
                                    mdb.SendEmailEpecialRequest();
                                    mdb.addrequestToException(input_text.Value, txtrBody.Value, src, Fuser, DropDownList1.Text, DropDownList2.SelectedItem.ToString(), CardsNum.Text, ExcepComments.Value, accountsrc);
                                }
                                catch
                                { 
                                    div1.Visible = true; 
                                }
                            }
                            else
                            {
                                mdb.addrequestToIssuance(input_text.Value, txtrBody.Value, src, Fuser, DropDownList1.Text, DropDownList2.SelectedItem.ToString(), CardsNum.Text, accountsrc );
                            } 
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
            catch (Exception Ex)
            {
                div_Error.Visible = true;
            }
        }
        public void clean()
        {
            txtrBody.InnerHtml = "";
            FileUpload1.Attributes.Clear();
            input_text.Value = "";
        }

        //public void SendEmailEpecialRequest()
        //{
        //    ExchangeService service = new ExchangeService();
        //    service.AutodiscoverUrl("CCM-Reply@prime-health.org");
        //    service.UseDefaultCredentials = false;
        //    service.Credentials = new WebCredentials("CCM-Reply", "NoP@ssw0rd", "primehealth.local");
        //    EmailMessage message = new EmailMessage(service);
        //    message.Subject = "Special Request";
        //    message.Body = " Be careful there is a new request has been submitted now ";
        //    message.From = "CCM-Reply@Prime-Health.org";
        //    //message.ToRecipients.Add("Mostafa.Mahmoud@Prime-Health.org");
        //    message.ToRecipients.Add("Mohamed.AbdElsattar@Prime-Health.org");
        //    message.ToRecipients.Add("david.maher@prime-health.org");
        //    message.CcRecipients.Add("Ayman.Shaalan@Prime-Health.org");
        //    //message.BccRecipients.Add("Mohamed.Soliman@Prime-Health.org");
        //    message.Save();
        //    message.SendAndSaveCopy();
        //}

        //public void SendEmailBackupFail()
        //{
        //    ExchangeService service = new ExchangeService();
        //    service.AutodiscoverUrl("CCM-Reply@prime-health.org");
        //    service.UseDefaultCredentials = false;
        //    service.Credentials = new WebCredentials("CCM-Reply", "NoP@ssw0rd", "primehealth.local");
        //    EmailMessage message = new EmailMessage(service);
        //    message.Subject = "Backup Failure";
        //    message.Body = " Be careful there is a problem in folder backup ";
        //    message.From = "CCM-Reply@Prime-Health.org";
        //    //message.ToRecipients.Add("Mostafa.Mahmoud@Prime-Health.org");
        //    message.ToRecipients.Add("Mostafa.Mahmoud@Prime-Health.org");
        //    message.CcRecipients.Add("Mohamed.AbdElsattar@prime-health.org");
        //    message.CcRecipients.Add("Mohamed.Maher@Prime-Health.org");
        //    //message.BccRecipients.Add("Mohamed.Soliman@Prime-Health.org");
        //    message.Save();
        //    message.SendAndSaveCopy();
        //}


    }
}