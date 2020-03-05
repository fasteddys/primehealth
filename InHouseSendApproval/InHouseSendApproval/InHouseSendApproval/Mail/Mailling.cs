using InHouseSendApproval.Model;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InHouseSendApproval.Mail
{
    public  class Mailling
    {

        private string MailUserName = InHouseSendApproval.Properties.Resources.MailUserName;
        private string MailPassword = InHouseSendApproval.Properties.Resources.MailPassword;
        private string MailDomain = InHouseSendApproval.Properties.Resources.MailDomain;
        private string MailSender = InHouseSendApproval.Properties.Resources.MailSender;
        protected string ExchangeWebServicesUri = InHouseSendApproval.Properties.Resources.ExchangeWebServicesUri;
        
        public Returned SendMail(ClientData ClientData)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                if (InHouseSendApproval.Properties.Resources.IsOnline == "1")
                {
                    // Exchange Online is out of network to you, so turn off SCP Autodiscover lookups.  
                    // If you had an on-premise serer then this likely would be set to true.
                    service.EnableScpLookup = false;
                    service.PreAuthenticate = true;
                    //End Of Office 365 Preprations.
                }
                else
                {
                    service.EnableScpLookup = true;
                }

                service.Credentials = new WebCredentials(MailUserName, MailPassword, MailDomain);
                service.HttpHeaders.Add("X-AnchorMailbox", MailSender);
                service.HttpHeaders.Add("X-PublicFolderMailbox", MailSender);
                service.Timeout = 180000;
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                service.Url = new Uri(ExchangeWebServicesUri);
                EmailMessage message = new EmailMessage(service);
                message.Subject = "TestEmailSubject Client:"+ ClientData.ClientName+" medical ID :"+ClientData.MedicalID;
                message.Body = "TestEmailbody";
                message.ToRecipients.Add(ClientData.ClientMail);
                foreach (var item in ClientData.AttachmentsUrl)
                {
                    message.Attachments.AddFileAttachment(item);
                }

                message.Save(new FolderId(WellKnownFolderName.SentItems, MailSender));
                message.SendAndSaveCopy(new FolderId(WellKnownFolderName.SentItems, MailSender));
                
                 return new Returned {
                      Message="Success",
                       Success=true
                };
            }
            catch (Exception ex)
            {
                return new Returned
                {
                    Message = "Faild",
                    Success = false,
                    FaildReason= ex.Message
                }; ;

             }
        }
    }
}
