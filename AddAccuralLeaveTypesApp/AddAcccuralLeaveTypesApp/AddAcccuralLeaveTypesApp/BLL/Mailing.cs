using AddAcccuralLeaveTypesApp.DTO;
using AddAcccuralLeaveTypesApp.Model;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddAcccuralLeaveTypesApp.BLL
{
    class Mailing
    {
        protected string AutoDiscoverEmailServiceEmail = string.Empty;

        public void PrepareMailToSent(MailingDTO MailingDTO, List<string> filePaths, string Type)
        {
            using (HRMSEntities HRMS = new HRMSEntities())
            {
                string MailingDomain = HRMS.Configurations.Where(x => x.ConfigurationKey == "MailingDomain" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
                string MailingUserName = HRMS.Configurations.Where(x => x.ConfigurationKey == "MailingUserName" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
                string MailingPassword = HRMS.Configurations.Where(x => x.ConfigurationKey == "MailingPassword" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
                string MailHeader = HRMS.Configurations.Where(x => x.ConfigurationKey == "MailHeader" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
                string MailFooter = HRMS.Configurations.Where(x => x.ConfigurationKey == "MailFooter" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
                string MailPictureLogo = HRMS.Configurations.Where(x => x.ConfigurationKey == "MailPictureLogo" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
                string MailURL = HRMS.Configurations.Where(x => x.ConfigurationKey == "MailURL" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
                string AutodiscoverUrl = HRMS.Configurations.Where(x => x.ConfigurationKey == "AutodiscoverUrl" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;

                Mailing Mailing = new Mailing();
                MailingDTO.MailUserName = MailingUserName;
                MailingDTO.MailDomain = MailingDomain;
                MailingDTO.MailPassword = MailingPassword;
                MailingDTO.MailHeader = MailHeader;
                MailingDTO.MailFooter = MailFooter;
                MailingDTO.MailPictureLogo = MailPictureLogo;
                MailingDTO.MailURL = MailURL;
                MailingDTO.AutodiscoverUrl = AutodiscoverUrl;

                SendMail(MailingDTO, filePaths, Type);

            }
        }

        public bool SendMail(MailingDTO MailingDTO, List<string> filePaths, string Type)
        {
            if (MailingDTO.To.Where(x => x != null && x != "").Count() > 0)
            {
                string MailBody1 = "", MailBody2 = "";
                string MailSubject = MailingDTO.Subject;

                MailBody1 = "Dear HR Team," +
                    "</p><p style='margin: 0; font - size: 14px; line - height: 17px; text - align: justify'>&#160;</p><p style='margin: 0; font - size: 14px; line - height: 17px; text - align: justify'>";

                if (Type.Equals("Monthly"))
                {
                    MailBody2 = "Kindly, Find attached of New Monthly Entitlements and System Approved & Rejected Requests for employees. <br/>";
                }
                else if (Type.Equals("Accural"))
                {
                    MailBody2 = "Kindly, Find attached of Accural Entitlements for new employees.";
                }

                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl(MailingDTO.AutodiscoverUrl);
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials(MailingDTO.MailUserName, MailingDTO.MailPassword, MailingDTO.MailDomain); ;
                service.TraceEnabled = true;
                EmailMessage message = new EmailMessage(service);
                MailingDTO.MailHeader = MailingDTO.MailHeader.Replace("/##$&/", MailingDTO.MailPictureLogo);
                MailingDTO.MailFooter = MailingDTO.MailFooter.Replace("@@@###/", MailingDTO.RequestID.ToString());
                MailingDTO.MailFooter = MailingDTO.MailFooter.Replace("/###%%%", MailingDTO.MailURL);

                foreach (var items in filePaths)
                {
                    message.Attachments.AddFileAttachment(items);
                }

                message.Subject = MailSubject;
                
                message.Body = new MessageBody(MailingDTO.MailHeader + MailBody1 + MailBody2 + MailingDTO.MailFooter);
                foreach (var item in MailingDTO.To)
                {
                    message.ToRecipients.Add(item);
                }
                foreach (var item in MailingDTO.CC)
                {
                    message.CcRecipients.Add(item);
                }
                message.BccRecipients.Add("IT-Developers@Prime-Health.org");

                try
                {
                    message.Save();
                    message.SendAndSaveCopy();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
