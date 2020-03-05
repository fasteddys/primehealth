using System;
using Microsoft.Exchange.WebServices.Data;

namespace DispatchingMVCVer1.Models
{
    public class Mailing
    {
        public void SendMail_NewRequestCreated(string CreatorName, string CreatorEmail, string AccountEmail, string AccountDep, int ReqID)
        {
            ExchangeService service = new ExchangeService();
            service.AutodiscoverUrl("InhouseDept@Prime-Health.org");
            service.AutodiscoverUrl("DispatchingReply@prime-health.org");
            
            
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials("DispatchingReply", "NoP@ssw0rd", "primehealth.local");
            EmailMessage message = new EmailMessage(service);
            message.Subject = "New Request";
            message.Body = "Dear  " + AccountDep + "Team" + "  , " + "\t\r" + CreatorName + " has just Send you New Request  : " + "ID Is : " + ReqID + "\t\r" + "" + "   ";
            message.From = "InhouseDept@Prime-Health.org";
            message.From = "DispatchingReply@prime-health.org";
            
            message.ToRecipients.Add(AccountEmail);
            message.CcRecipients.Add(CreatorEmail);
            //message.CcRecipients.Add("Ahmed.Shaker@Prime-Health.org");
            message.CcRecipients.Add("Hazem.Youssef @Prime-Health.org");
            message.Save();
            message.SendAndSaveCopy();
        }
        public void SendMail_RequestApprovedByAccount(string QualityEmail, string CreatorEmail, string AccountApprovedEmail, string AccountApprovedName, int ReqID)
        {
            ExchangeService service = new ExchangeService();
            service.AutodiscoverUrl("InhouseDept@Prime-Health.org");
            service.AutodiscoverUrl("DispatchingReply@prime-health.org");
            
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials("DispatchingReply", "NoP@ssw0rd", "primehealth.local");
            EmailMessage message = new EmailMessage(service);
            message.Subject = "New Request";
            message.Body = AccountApprovedName + " has just Approved on  New Request  : " + "ID Is : " + ReqID + "\t\r" + "" + "   ";
            message.From = "DispatchingReply@prime-health.org";
            //message.ToRecipients.Add("Ahmed.Shaker@Prime-Health.org");
            //message.ToRecipients.Add("Ahmed.Mostafa@Prime-Health.org");
            message.ToRecipients.Add("Hazem.Youssef @Prime-Health.org");
            message.ToRecipients.Add("sayed.Hamada@Prime-Health.org");
            message.ToRecipients.Add("Ahmed.Ezzat@Prime-Health.org");
            message.CcRecipients.Add(CreatorEmail);
            message.CcRecipients.Add(AccountApprovedEmail);
            message.Save();
            message.SendAndSaveCopy();
        }
        public void RequestPreparedByStock(string CreatorName, string CreatorEmail, string PreparedPerson, string PreparedPersonEmail, string AccountApprovedEmail, int ReqID)
        {
            ExchangeService service = new ExchangeService();
            service.AutodiscoverUrl("DispatchingReply@prime-health.org");
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials("DispatchingReply", "NoP@ssw0rd", "primehealth.local");
            EmailMessage message = new EmailMessage(service);
            message.Subject = "Request Prepared";
            message.Body = "Dear " + CreatorName + "  , " + "\t\r" + PreparedPerson + " has just Prepared Request  : " + "ID Is : " + ReqID + "\t\r" + "" + "   ";
            message.From = "DispatchingReply@prime-health.org";
            message.ToRecipients.Add(CreatorEmail);
            message.CcRecipients.Add(PreparedPersonEmail);
            message.CcRecipients.Add(AccountApprovedEmail);
            if (PreparedPersonEmail != "Hazem.Youssef @Prime-Health.org")
            {
                message.CcRecipients.Add("Hazem.Youssef @Prime-Health.org");
            }
            message.Save();
            message.SendAndSaveCopy();
        }
        public void RequestClosed(string CreatorName, string CreatorEmail, string AccountApprovedEmail, string PreparedPersonEmail, int ReqID)
        {
            ExchangeService service = new ExchangeService();
            service.AutodiscoverUrl("DispatchingReply@prime-health.org");
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials("DispatchingReply", "NoP@ssw0rd", "primehealth.local");
            EmailMessage message = new EmailMessage(service);
            message.Subject = "Request Closed";
            message.Body = CreatorName + " " + "Has Just Closed Request :" + "ID IS :" + ReqID + "\t\r" + "" + "   ";
            message.From = "DispatchingReply@prime-health.org";
            message.ToRecipients.Add(AccountApprovedEmail);
            message.CcRecipients.Add(CreatorEmail);
            message.CcRecipients.Add(PreparedPersonEmail);
            if (PreparedPersonEmail != "Hazem.Youssef @Prime-Health.org")
            {
                message.CcRecipients.Add("Hazem.Youssef @Prime-Health.org");
            }
            message.Save();
            message.SendAndSaveCopy();
        }
        public void SendMail_RequestRejected(string CreatorName, string CreatorEmail, string RejectPerson, string RejectPersonEmail, int ReqID)
        {
            ExchangeService service = new ExchangeService();
            service.AutodiscoverUrl("DispatchingReply@prime-health.org");
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials("DispatchingReply", "NoP@ssw0rd", "primehealth.local");
            EmailMessage message = new EmailMessage(service);
            message.Subject = "Request Rejected";
            message.Body = "Dear  " + CreatorName + "  , " + "\t\r" + RejectPerson + " has just Reject Your New Request  : " + "ID Is : " + ReqID + "\t\r" + "" + "   ";
            message.From = "DispatchingReply@prime-health.org";
            message.ToRecipients.Add(CreatorEmail);
            message.CcRecipients.Add(RejectPersonEmail);
            message.Save();
            message.SendAndSaveCopy();
        }
        public void SendMail_NewBackToStockRequest(string CreatorName, string CreatorEmail, string QualityEmail, int ReqID)
        {
            ExchangeService service = new ExchangeService();
            service.AutodiscoverUrl("DispatchingReply@prime-health.org");
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials("DispatchingReply", "NoP@ssw0rd", "primehealth.local");
            EmailMessage message = new EmailMessage(service);
            message.Subject = "New Back To Stock Request";
            message.Body = CreatorName + " has just Send you New Back To Stock Request  : " + "ID Is : " + ReqID + "\t\r" + "" + "   ";
            message.From = "DispatchingReply@prime-health.org";
            //message.ToRecipients.Add("Ahmed.Shaker@Prime-Health.org");
            //message.ToRecipients.Add("Ahmed.Mostafa@Prime-Health.org");
            message.ToRecipients.Add("Hazem.Youssef @Prime-Health.org");
            message.ToRecipients.Add("sayed.Hamada@Prime-Health.org");
            message.ToRecipients.Add("Ahmed.Ezzat@Prime-Health.org");
            message.CcRecipients.Add(CreatorEmail);
            message.Save();
            message.SendAndSaveCopy();
        }
        public void SendMail_RequestReturnedClosed(string CreatorName, string CreatorEmail, string ClosedPerson, string ClosedPersonEmail, int ReqID)
        {
            ExchangeService service = new ExchangeService();
            service.AutodiscoverUrl("DispatchingReply@prime-health.org");
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials("DispatchingReply", "NoP@ssw0rd", "primehealth.local");
            EmailMessage message = new EmailMessage(service);
            message.Subject = "Request Closed";
            message.Body = "Dear  " + CreatorName + " Team" + "  , " + "\t\r" + ClosedPerson + " has just Closed Back To Stock Request  : " + "ID Is : " + ReqID + "\t\r" + "" + "   ";
            message.From = "DispatchingReply@prime-health.org";
            message.ToRecipients.Add(CreatorEmail);
            message.CcRecipients.Add(ClosedPersonEmail);
            if (ClosedPersonEmail != "Hazem.Youssef @Prime-Health.org")
            {
                message.CcRecipients.Add("Hazem.Youssef @Prime-Health.org");
            }
            message.Save();
            message.SendAndSaveCopy();
        }
        public void SendMail_RequestTransfered(string TranseferedTo, string TranseferedToEmail, string TranseferFrom, string TranseferFromEmail, int ReqID)
        {
            ExchangeService service = new ExchangeService();
            service.AutodiscoverUrl("DispatchingReply@prime-health.org");
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials("DispatchingReply", "NoP@ssw0rd", "primehealth.local");
            EmailMessage message = new EmailMessage(service);
            message.Subject = "Request Transefered";
            message.Body = "Dear" + TranseferedTo + " , " + TranseferFrom + "Has Just Transefered The Request: " + "ID Is : " + ReqID + "To You " + "\t\r" + "" + "   ";
            message.From = "DispatchingReply@prime-health.org";
            message.ToRecipients.Add(TranseferedToEmail);
            message.CcRecipients.Add(TranseferFromEmail);
            if (TranseferedToEmail != "Hazem.Youssef @Prime-Health.org" || TranseferFromEmail != "Hazem.Youssef @Prime-Health.org")
            {
                message.CcRecipients.Add("Hazem.Youssef @Prime-Health.org");
            }
            message.Save();
            message.SendAndSaveCopy();
        }
        public void SendMail_StockNotification(string Itemname)
        {
            ExchangeService service = new ExchangeService();
            service.AutodiscoverUrl("DispatchingReply@prime-health.org");
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials("DispatchingReply", "NoP@ssw0rd", "primehealth.local");
            EmailMessage message = new EmailMessage(service);
            message.Subject = "Stock Notification";
            message.Body = "Dear All" + "  , " + "\t\r" + Itemname + " Bocklets number available in stock just reached 500  ";
            message.From = "DispatchingReply@prime-health.org";

            //message.ToRecipients.Add("Ahmed.Shaker@Prime-Health.org");
            //message.ToRecipients.Add("Ahmed.Mostafa@Prime-Health.org");
            message.ToRecipients.Add("Hazem.Youssef @Prime-Health.org");           
            message.ToRecipients.Add("sayed.Hamada@Prime-Health.org");         
            message.ToRecipients.Add("Ahmed.Ezzat@Prime-Health.org");
            message.Save();
            message.SendAndSaveCopy();
        }
    }
}