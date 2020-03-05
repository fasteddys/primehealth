using BNC.Utilities.DTO;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BNC.Utilities
{
    public class Mailing
    {
        protected string AutoDiscoverEmailServiceEmail = string.Empty;


        public bool SendMailForAction (MailingDTO MailingDTO)
        {
            if (MailingDTO.To.Where(x=>x!=null&& x!="").Count() > 0)
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

                if (MailingDTO.ActionTypeID == 1)
                {
                    string MailSubject = "Request No. " + MailingDTO.RequestID + " has been " + MailingDTO.Action + " by " + MailingDTO.ActionBy;
                    string MailBody = "";

                    if (MailingDTO.IsFinalApproved == false)
                    {

                        MailBody = "Dear " + MailingDTO.Actionto + " ," +
                       "</p><p style='margin: 0; font - size: 14px; line - height: 17px; text - align: justify'>&#160;</p><p style='margin: 0; font - size: 14px; line - height: 17px; text - align: justify'>" +
                                    " Your Request With No. " + MailingDTO.RequestID + " has been " + MailingDTO.Action + " by " + MailingDTO.ActionBy + " for " + MailingDTO.Duration + " " + MailingDTO.Unit + " of " + MailingDTO.LeaveTypeName + " Leave" +
                                         ",Starting from: " + MailingDTO.LeaveStartDate + " To " + MailingDTO.LeaveEndtDate + " and  You " + "will be back to work on " +
                                         MailingDTO.LeaveComeBackDate + ", it is now Pending for " + MailingDTO.NextActionto + " Action.";
                    }

                    else
                    {

                        MailBody = "Dear " + MailingDTO.Actionto + " ," +
                       "</p><p style='margin: 0; font - size: 14px; line - height: 17px; text - align: justify'>&#160;</p><p style='margin: 0; font - size: 14px; line - height: 17px; text - align: justify'>" +
                                    " Your Request With No. " + MailingDTO.RequestID + " has been " + MailingDTO.Action + " by " + MailingDTO.ActionBy + " for " + MailingDTO.Duration + " " + MailingDTO.Unit + " of " + MailingDTO.LeaveTypeName + " Leave" +
                                         ",Starting from: " + MailingDTO.LeaveStartDate + " To " + MailingDTO.LeaveEndtDate + " and  You " + "will be back to work on " +
                                         MailingDTO.LeaveComeBackDate + ".";
                    }
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
                    string MailSubject = "Request No. " + MailingDTO.RequestID + " has been " + MailingDTO.Action + " by " + MailingDTO.ActionBy;
                    string MailBody = "Dear " + MailingDTO.Actionto + " ," +
                        "</p><p style='margin: 0; font - size: 14px; line - height: 17px; text - align: justify'>&#160;</p><p style='margin: 0; font - size: 14px; line - height: 17px; text - align: justify'>" +
                       " Your Request With No. " + MailingDTO.RequestID + " has been " + MailingDTO.Action + " by " + MailingDTO.ActionBy + " for " + MailingDTO.Duration + " " + MailingDTO.Unit + " of " + MailingDTO.LeaveTypeName + " Leave" +
                                          ",Starting from: " + MailingDTO.LeaveStartDate + " To " + MailingDTO.LeaveEndtDate + " and back to work on " + MailingDTO.LeaveComeBackDate + " , Rejection Reason (" + MailingDTO.RejectionReason + ")";
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
            }
            else { return false; }
        }
        public bool SendMailOnRequestingVication(MailingDTO MailingDTO)
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

                string MailSubject = "Request No. " + MailingDTO.RequestID + " has been " + MailingDTO.Action + " by " + MailingDTO.Actionto;
                string MailBody = "Dear " + MailingDTO.Actionto + " ," +
                    "</p><p style='margin: 0; font - size: 14px; line - height: 17px; text - align: justify'>&#160;</p><p style='margin: 0; font - size: 14px; line - height: 17px; text - align: justify'>" +
                                 "Please note That a Request With No." + MailingDTO.RequestID + " has been submitted successfully for " + MailingDTO.Duration + " " + MailingDTO.Unit + " Of " + MailingDTO.LeaveTypeName + " Leave " +
                                      ",starting from: " + MailingDTO.LeaveStartDate + " to " + MailingDTO.LeaveEndtDate + " and back to work on " + MailingDTO.LeaveComeBackDate + " ,it is now waiting for " + MailingDTO.NextActionto + " action ";


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
