using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

namespace CallCenterWindowsService
{
    class Mailing
    {
        public static void Send_Mail_Service_Stoped()

        {   
            ExchangeService service = new ExchangeService();
            service.AutodiscoverUrl("Ticket.System@Prime-Health.org");
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials("ticket-sys", "prime@4321", "primehealth.local");
            EmailMessage message = new EmailMessage(service);
            message.Subject = "Call Center windows service has been stoped Working" ;
            message.Body = "Call Center windows service has been stoped Working";
            message.From = "Ticket.System@Prime-Health.org";
            message.ToRecipients.Add("IT@Prime-Health.org");
            message.Attachments.AddFileAttachment("C:\\EmailRequestAttach\\4\\DoctorAttach\\1424685088.pdf");
            message.Save();
            message.SendAndSaveCopy();
        }
        public bool SendLateEmail(List<string> reciver, List<string> InCc, List<string> InBcc
            , string Subject, int RequestNumber, string LateMinutes, string Body)

        {

            //string DefaultSendingMail = ConfigurationManager.AppSettings["DefaultSendingMail"];
            //string MailDomain = ConfigurationManager.AppSettings["MailDomain"];
            //string MailUserName = ConfigurationManager.AppSettings["MailUserName"];
            //string MailPassw0rd = ConfigurationManager.AppSettings["MailPassw0rd"];

            string DefaultSendingMail ="Ticket.System@Prime - Health.org";
            string MailDomain = "primehealth.local";
            string MailUserName = "ticket-sys";
            string MailPassw0rd = "prime@4321";

            ExchangeService service = new ExchangeService();
            service.AutodiscoverUrl(DefaultSendingMail);
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials(MailUserName, MailPassw0rd, MailDomain);
            EmailMessage message = new EmailMessage(service);

            message.Subject = Subject;
            message.Body = Body;
            message.From = DefaultSendingMail;
            foreach (var item in reciver)
            {
                message.ToRecipients.Add(item);
            }
            foreach (var item in InCc)
            {
                message.CcRecipients.Add(item);
            }
            foreach (var item in InBcc)
            {
                message.BccRecipients.Add(item);
            }

            message.Save();
            message.SendAndSaveCopy();



            return true;
        }


    }
}
