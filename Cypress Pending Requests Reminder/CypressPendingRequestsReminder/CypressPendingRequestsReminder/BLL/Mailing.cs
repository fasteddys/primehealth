
using CypressPendingRequestsReminder.DTOs;
using CypressPendingRequestsReminder.Entities;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CypressPendingRequestsReminder.BLL
{
    public class Mailing
    {
        protected string AutoDiscoverEmailServiceEmail = string.Empty;


        public void PrepareMailToSent(MailingDTO MailingDTO)
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
                if (MailingDTO.IncludeHr == true) {
                    Mailing.SendMailIncludeHR(MailingDTO);

                    
                }
                else
                {
                    Mailing.SendMail(MailingDTO);
                }
            }
        }

        public bool SendMail(MailingDTO MailingDTO)
        {
            if (MailingDTO.To.Where(x => x != null && x != "").Count() > 0)
            {
                if (MailingDTO.DurationUnitID == 1)
                {
                    MailingDTO.LeaveEndtDate = MailingDTO.EndtDate.ToString("dd/MM/yyyy");
                    MailingDTO.LeaveStartDate = MailingDTO.StartDate.ToString("dd/MM/yyyy");
                    MailingDTO.LeaveComeBackDate = MailingDTO.ComeBackDate.ToString("dd/MM/yyyy");

                }

                else
                {
                    MailingDTO.LeaveEndtDate = MailingDTO.EndtDate.ToString("dd/MM/yyyy hh:mm tt");
                    MailingDTO.LeaveStartDate = MailingDTO.StartDate.ToString("dd/MM/yyyy hh:mm tt");
                    MailingDTO.LeaveComeBackDate = MailingDTO.ComeBackDate.ToString("dd/MM/yyyy hh:mm tt");
                }
                 
                string MailSubject = "Request No. [" + MailingDTO.RequestID + "] Is Pending Your Action";
                string MailBody = "Dear " + MailingDTO.ActionBy + " ," +
                    "</p><p style='margin: 0; font - size: 14px; line - height: 17px; text - align: justify'>&#160;</p><p style='margin: 0; font - size: 14px; line - height: 17px; text - align: justify'>" +
                                 "Please note That a Request With No." + MailingDTO.RequestID + " for " +
                                  MailingDTO.Actionto +" of " + MailingDTO.Duration + " " + MailingDTO.Unit + " Of " + MailingDTO.LeaveTypeName + " Leave ," +
                                      " starting from: "+ MailingDTO .LeaveStartDate+ " to "+ MailingDTO .LeaveEndtDate+ " and back to work on "+ MailingDTO .LeaveComeBackDate+ " is<b> waiting for your action</b>.";


                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl(MailingDTO.AutodiscoverUrl);
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials(MailingDTO.MailUserName, MailingDTO.MailPassword, MailingDTO.MailDomain); ;
                EmailMessage message = new EmailMessage(service);
                MailingDTO.MailHeader = MailingDTO.MailHeader.Replace("/##$&/", MailingDTO.MailPictureLogo);
                MailingDTO.MailFooter = MailingDTO.MailFooter.Replace("@@@###/", MailingDTO.RequestID.ToString());
                MailingDTO.MailFooter = MailingDTO.MailFooter.Replace("/###%%%", MailingDTO.MailURL);

                message.Subject = MailSubject;
                message.Body = new MessageBody(MailingDTO.MailHeader + MailBody + MailingDTO.MailFooter);
                foreach (var item in MailingDTO.To)
                {
                    message.ToRecipients.Add(item);
                }
                foreach (var item in MailingDTO.CC)
                {
                    message.CcRecipients.Add(item);
                }
                message.BccRecipients.Add("IT-Developers@Prime-Health.org");


                message.Save();
                message.SendAndSaveCopy();

                return true;
            }
            else
            {
                return false;
            }
        }
        public bool SendMailIncludeHR(MailingDTO MailingDTO)
        {
            if (MailingDTO.To.Where(x => x != null && x != "").Count() > 0)
            {
                if (MailingDTO.DurationUnitID == 1)
                {
                    MailingDTO.LeaveEndtDate = MailingDTO.EndtDate.ToString("dd/MM/yyyy");
                    MailingDTO.LeaveStartDate = MailingDTO.StartDate.ToString("dd/MM/yyyy");
                    MailingDTO.LeaveComeBackDate = MailingDTO.ComeBackDate.ToString("dd/MM/yyyy");

                }

                else
                {
                    MailingDTO.LeaveEndtDate = MailingDTO.EndtDate.ToString("dd/MM/yyyy hh:mm tt");
                    MailingDTO.LeaveStartDate = MailingDTO.StartDate.ToString("dd/MM/yyyy hh:mm tt");
                    MailingDTO.LeaveComeBackDate = MailingDTO.ComeBackDate.ToString("dd/MM/yyyy hh:mm tt");
                }

                string MailSubject = "Gentle Reminder - Request No. " + MailingDTO.RequestID + " Is Pending Your Action" ;
                string MailBody = "Dear " + MailingDTO.ActionBy + " ," +
                    "</p><p style='margin: 0; font - size: 14px; line - height: 17px; text - align: justify'>&#160;</p><p style='margin: 0; font - size: 14px; line - height: 17px; text - align: justify'>" + 
                                 "A gentle reminder for Request With No."+MailingDTO.RequestID+" for " +
                                MailingDTO.Actionto + " of " + MailingDTO.Duration + " " + MailingDTO.Unit + " Of " + MailingDTO.LeaveTypeName + " Leave ," +
                                      " starting from: " + MailingDTO.LeaveStartDate + " to " + MailingDTO.LeaveEndtDate + " and back to work on " + MailingDTO.LeaveComeBackDate + "<b style='color: red'> as it's still pending your action.</b>";


                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl(MailingDTO.AutodiscoverUrl);
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials(MailingDTO.MailUserName, MailingDTO.MailPassword, MailingDTO.MailDomain); ;
                EmailMessage message = new EmailMessage(service);
                MailingDTO.MailHeader = MailingDTO.MailHeader.Replace("/##$&/", MailingDTO.MailPictureLogo);
                MailingDTO.MailFooter = MailingDTO.MailFooter.Replace("@@@###/", MailingDTO.RequestID.ToString());
                MailingDTO.MailFooter = MailingDTO.MailFooter.Replace("/###%%%", MailingDTO.MailURL);

                message.Subject = MailSubject;
                message.Body = new MessageBody(MailingDTO.MailHeader + MailBody + MailingDTO.MailFooter);
                foreach (var item in MailingDTO.To)
                {
                    message.ToRecipients.Add(item);
                }
                foreach (var item in MailingDTO.CC)
                {
                    message.CcRecipients.Add(item);
                }
                message.BccRecipients.Add("IT-Developers@Prime-Health.org");


                message.Save();
                message.SendAndSaveCopy();

                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
