using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

namespace ApprovalsWindowsService
{
    public static class Mailing
    {
        public static void Send_Mail_Service_Stoped()
        {
            try
            {
                using (PhNetworkEntities db = new PhNetworkEntities())
                {
                    var Tomail = (from t in db.EmailApprovalsConfigurations where t.ConfigurationKey == "UpdateColorsWindowsServiceStopEmail" && t.ConfigurationValue != null select t.ConfigurationValue).ToList();
                    ExchangeService service = new ExchangeService();
                    service.Url = new Uri(db.EmailApprovalsConfigurations.Where(x => x.ConfigurationKey == "LocalExchangeWebServicesUri").FirstOrDefault().ConfigurationValue);
                    string DefaultSendingMail = "noreply@prime-health.org";
                    string MailDomain = "primehealth.local";
                    string MailUserName = "noreply";
                    string MailPassw0rd = "NoP@ssw0rd";
                    service.Credentials = new WebCredentials(MailUserName, MailPassw0rd, MailDomain);
                    service.AutodiscoverUrl(DefaultSendingMail);
                    service.UseDefaultCredentials = false;
                    EmailMessage message = new EmailMessage(service);
                    message.Subject = "Call Center windows service has been stoped Working";
                    message.Body = "Call Center windows service has been stoped Working";
                    message.From = "noreply@prime-health.org";
                    foreach (var item in Tomail)
                    {
                        message.ToRecipients.Add(item);
                    }

                    message.SendAndSaveCopy();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public static bool SendLateEmail(List<string> reciver, List<string> InCc, List<string> InBcc
            , string Subject, int RequestNumber, string Body)

        {

            //string DefaultSendingMail = ConfigurationManager.AppSettings["DefaultSendingMail"];
            //string MailDomain = ConfigurationManager.AppSettings["MailDomain"];
            //string MailUserName = ConfigurationManager.AppSettings["MailUserName"];
            //string MailPassw0rd = ConfigurationManager.AppSettings["MailPassw0rd"];

            string DefaultSendingMail = "noreply@prime-health.org";
            string MailDomain = "primehealth.local";
            string MailUserName = "noreply";
            string MailPassw0rd = "NoP@ssw0rd";

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
                if (item != null)
                {
                    message.ToRecipients.Add(item);
                }

            }
            foreach (var item in InCc)
            {
                if (item != null)
                {
                    message.CcRecipients.Add(item);
                }
            }
            foreach (var item in InBcc)
            {
                if (item != null)
                {
                    message.BccRecipients.Add(item);
                }
            }

            message.SendAndSaveCopy();

            return true;
        }


    }
}
