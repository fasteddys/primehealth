using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Microsoft.Exchange.WebServices.Data;
using CallCenterNotesApp.DLL;
using CallCenterNotesApp.CustomExceptions;
using static CallCenterNotesApp.Enums.Enums;
using System.Net;

namespace CallCenterNotesApp.BLL
{
    public class Mailing
    {
        protected string GeneralSignature =string.Empty;
        protected string GeneralAutoDiscoverEmailServiceEmail;
        protected string GeneralDefaultSendingMail = "";
        protected string SepratorLine = "<div dir='rtl' style='color: #000000; font-family: Calibri,Arial,Helvetica,sans-serif; font-size: 12pt; background-color: #ffffff;'><hr style='width: 98%; display: inline-block;' tabindex='-1' />";
        protected string AutoDiscoverEmailServiceEmail = string.Empty;
        protected string DefaultSendingMail = string.Empty;
        protected string DefaultExchangeWebServicesUri = string.Empty;
        protected string MailDomain = string.Empty;
        protected string MailUserName = string.Empty;
        protected string MailPassw0rd = string.Empty;
        protected string Signature = string.Empty;
        protected string GeneralMailDomain = string.Empty;
        protected string GeneralMailUserName = string.Empty;
        protected string GeneralMailPassw0rd = string.Empty;
        protected string SPCallCenterSignature = string.Empty;
        protected string SPAutoDiscoverEmailServiceEmail = string.Empty;
        protected string SPDefaultSendingMail = string.Empty;
        protected string SPMailDomain = string.Empty;
        protected string SPMailUserName = string.Empty;
        protected string SPMailPassw0rd = string.Empty;
        protected string SaveTicketAttachmentPath = string.Empty;
        protected string GeneralEmailExchangeWebSerivceUri = string.Empty;
        protected string SPEmailExchangeWebSerivceUri = string.Empty;


        public Mailing()
        {
            using (PhNetworkEntities db=new PhNetworkEntities())
            {
                //Genarl call ceneter intilaizar
                GeneralSignature = db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralCallCenterMailSignature").FirstOrDefault().ConfigurationValue.ToString();
                GeneralAutoDiscoverEmailServiceEmail = db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralAutoDiscoverUrl").FirstOrDefault().ConfigurationValue.ToString();
                GeneralDefaultSendingMail = db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralCallCenterEmail").FirstOrDefault().ConfigurationValue.ToString();
                GeneralMailDomain = db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralEmailDomain").FirstOrDefault().ConfigurationValue.ToString();
                GeneralMailUserName = db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralEmailUserName").FirstOrDefault().ConfigurationValue.ToString();
                GeneralMailPassw0rd = db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralEmailPassword").FirstOrDefault().ConfigurationValue.ToString();
                GeneralEmailExchangeWebSerivceUri = db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralExchangeWebServicesUri").FirstOrDefault().ConfigurationValue.ToString();

                //SP call ceneter intilaizar
                SPCallCenterSignature = db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPCallCenterMailSignature").FirstOrDefault().ConfigurationValue.ToString();
                SPAutoDiscoverEmailServiceEmail = db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPAutoDiscoverUrl").FirstOrDefault().ConfigurationValue.ToString();
                SPDefaultSendingMail = db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPCallCenterEmail").FirstOrDefault().ConfigurationValue.ToString();
                SPMailDomain = db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPEmailDomain").FirstOrDefault().ConfigurationValue.ToString();
                SPMailUserName = db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPEmailUserName").FirstOrDefault().ConfigurationValue.ToString();
                SPMailPassw0rd = db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPEmailPassword").FirstOrDefault().ConfigurationValue.ToString();
                SPEmailExchangeWebSerivceUri = db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPExchangeWebServicesUri").FirstOrDefault().ConfigurationValue.ToString();


                // Other initilaizers
                SaveTicketAttachmentPath = db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SaveTicketAttachmentPath").FirstOrDefault().ConfigurationValue.ToString();

            }


        }
        public bool SendApprovalEmail(int RequestID, string Note)
        {

            try
            {
                using (PhNetworkEntities db=new PhNetworkEntities())
                {
                    var ToRecipients = (from m in db.EmailRequestMailingDetails where m.TicketNumber == RequestID && m.IsTO == true && m.IsDeleted == false select m.Email).ToList();
                    var CcRecipients = (from m in db.EmailRequestMailingDetails where m.TicketNumber == RequestID && m.IsCC == true && m.IsDeleted == false select m.Email).ToList();
                    var BccRecipients = (from m in db.EmailRequestMailingDetails where m.TicketNumber == RequestID && m.IsBCC == true && m.IsDeleted == false select m.Email).ToList();
                    BccRecipients.Add("medgulf.developers@gmail.com");

                    EmailApprovalRequest RequestToSendApprovalEmail = db.EmailApprovalRequests.Where(x => x.ID == RequestID).FirstOrDefault();

                    if (RequestToSendApprovalEmail.TicketTypeID == (int)TicketTypes.General)
                    {
                        AutoDiscoverEmailServiceEmail = GeneralAutoDiscoverEmailServiceEmail;
                        DefaultSendingMail = GeneralDefaultSendingMail;
                        MailDomain = GeneralMailDomain;
                        MailUserName = GeneralMailUserName;
                        MailPassw0rd = GeneralMailPassw0rd;
                        Signature = GeneralSignature;
                        DefaultExchangeWebServicesUri = GeneralEmailExchangeWebSerivceUri;


                    }
                    if (RequestToSendApprovalEmail.TicketTypeID == (int)TicketTypes.Special)
                    {
                        AutoDiscoverEmailServiceEmail = SPAutoDiscoverEmailServiceEmail;
                        DefaultSendingMail = SPDefaultSendingMail;
                        MailDomain = SPMailDomain;
                        MailUserName = SPMailUserName;
                        MailPassw0rd = SPMailPassw0rd;
                        Signature = SPCallCenterSignature;
                        DefaultExchangeWebServicesUri = SPEmailExchangeWebSerivceUri;
                    }

                    List<string> AttachmentsUrl = new List<string>();


                    var RequestAttachments = (from a in db.EmailRequestAttachmentsDetails where a.TicketNumber == RequestID select a).ToList();


                    ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2016);
                    if (IsDefaultEmailWorkingOnline(DefaultSendingMail)=="1")
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
                    service.UseDefaultCredentials = false;
                    service.Credentials = new WebCredentials(MailUserName, MailPassw0rd, MailDomain);
                    service.HttpHeaders.Add("X-AnchorMailbox", DefaultSendingMail);
                    service.HttpHeaders.Add("X-PublicFolderMailbox", DefaultSendingMail);
                    service.Timeout = 180000;
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                    service.Url = new Uri(DefaultExchangeWebServicesUri);
                    EmailMessage message = new EmailMessage(service);

                    message.Subject = "RE: " + RequestToSendApprovalEmail.MailSubject.ToString() + "";
                    if (RequestToSendApprovalEmail.RequstStatusID == (int)RequestStatus.ReOpenedByDoctor)
                    {
                        message.Body = Note + "<br/><br/><br/>" + Signature + SepratorLine + RequestToSendApprovalEmail.CreatedByNotes;
                        foreach (var item in RequestAttachments)
                        {
                            if (item.IsDoctorAttachment == true && item.IsReopeningAttachment == true)
                            {
                                string StaticPath = SaveTicketAttachmentPath + item.Path;
                               AttachmentsUrl.Add(StaticPath);
                            }
                        }
                    }
                    else if (RequestToSendApprovalEmail.RequstStatusID == (int)RequestStatus.AssignedByDoctor|| RequestToSendApprovalEmail.RequstStatusID == (int)RequestStatus.EndTechnicalApproveByDoctor)
                    {
                        message.Body = Note + "<br/><br/><br/>" + Signature + SepratorLine + RequestToSendApprovalEmail.CreatedByNotes;
                        foreach (var item in RequestAttachments)
                        {
                            if (item.IsDoctorAttachment == true)
                            {
                                string StaticPath = SaveTicketAttachmentPath + item.Path;
                                AttachmentsUrl.Add(StaticPath);
                            }
                        }
                    }


                    if (RequestToSendApprovalEmail.RequstStatusID == (int)RequestStatus.ReOpenedByAudit)
                    {
                        message.Body = Note + "<br/><br/><br/>" + Signature + SepratorLine + RequestToSendApprovalEmail.CreatedByNotes;
                        foreach (var item in RequestAttachments)
                        {
                            if (item.IsAuditAttachment == true && item.IsReopeningAttachment == true)
                            {
                                string StaticPath = SaveTicketAttachmentPath + item.Path;
                                AttachmentsUrl.Add(StaticPath);
                            }
                        }
                    }

                    else if (RequestToSendApprovalEmail.RequstStatusID == (int)RequestStatus.AssignedByAudit || RequestToSendApprovalEmail.RequstStatusID == (int)RequestStatus.EndTechnicalApproveByAudit)
                    {
                        message.Body = Note + "<br/><br/><br/>" + Signature + SepratorLine + RequestToSendApprovalEmail.CreatedByNotes;
                        foreach (var item in RequestAttachments)
                        {
                            if (item.IsAuditAttachment == true)
                            {
                                string StaticPath = SaveTicketAttachmentPath + item.Path;
                                AttachmentsUrl.Add(StaticPath);
                            }
                        }
                    }
                    message.From = DefaultSendingMail;

                    foreach (var item in ToRecipients)
                    {
                        message.ToRecipients.Add(item);
                    }
                    foreach (var item in CcRecipients)
                    {
                        message.CcRecipients.Add(item);
                    }
                    foreach (var item in BccRecipients)
                    {
                        message.BccRecipients.Add(item);
                    }
                    foreach (var item in AttachmentsUrl)
                    {
                        message.Attachments.AddFileAttachment(item);
                    }

                    message.Save(new FolderId(WellKnownFolderName.SentItems, DefaultSendingMail));
                    message.SendAndSaveCopy(new FolderId(WellKnownFolderName.SentItems, DefaultSendingMail));

                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "Mailing.cs", "SendApprovalEmail", "CallCenterSystemApp");
                return false;
            }
        }
        public bool SendRejectionEmail(int RequestID, string Note)
        {
            try
            {
                using (PhNetworkEntities db=new PhNetworkEntities())
                {
                    var ToRecipients = (from m in db.EmailRequestMailingDetails where m.TicketNumber == RequestID && m.IsTO == true && m.IsDeleted == false select m.Email).ToList();
                    var CcRecipients = (from m in db.EmailRequestMailingDetails where m.TicketNumber == RequestID && m.IsCC == true && m.IsDeleted == false select m.Email).ToList();
                    var BccRecipients = (from m in db.EmailRequestMailingDetails where m.TicketNumber == RequestID && m.IsBCC == true && m.IsDeleted == false select m.Email).ToList();
                    BccRecipients.Add("medgulf.developers@gmail.com");
                    EmailApprovalRequest RequestToSendApprovalEmail = db.EmailApprovalRequests.Where(x => x.ID == RequestID).FirstOrDefault();

                    if (RequestToSendApprovalEmail.TicketTypeID == (int)TicketTypes.General)
                    {
                        AutoDiscoverEmailServiceEmail = GeneralAutoDiscoverEmailServiceEmail;
                        DefaultSendingMail = GeneralDefaultSendingMail;
                        MailDomain = GeneralMailDomain;
                        MailUserName = GeneralMailUserName;
                        MailPassw0rd = GeneralMailPassw0rd;
                        Signature = GeneralSignature;
                        DefaultExchangeWebServicesUri = GeneralEmailExchangeWebSerivceUri;

                    }
                    if (RequestToSendApprovalEmail.TicketTypeID == (int)TicketTypes.Special)
                    {
                        AutoDiscoverEmailServiceEmail = SPAutoDiscoverEmailServiceEmail;
                        DefaultSendingMail = SPDefaultSendingMail;
                        MailDomain = SPMailDomain;
                        MailUserName = SPMailUserName;
                        MailPassw0rd = SPMailPassw0rd;
                        Signature = SPCallCenterSignature;
                        DefaultExchangeWebServicesUri = SPEmailExchangeWebSerivceUri;

                    }

                    List<string> AttachmentsUrl = new List<string>();


                    var RequestAttachments = (from a in db.EmailRequestAttachmentsDetails where a.TicketNumber == RequestID select a).ToList();

                    ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2016);
                    if (IsDefaultEmailWorkingOnline(DefaultSendingMail) == "1")
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
                    service.UseDefaultCredentials = false;
                    service.Credentials = new WebCredentials(MailUserName, MailPassw0rd, MailDomain);
                    service.HttpHeaders.Add("X-AnchorMailbox", DefaultSendingMail);
                    service.HttpHeaders.Add("X-PublicFolderMailbox", DefaultSendingMail);
                    service.Timeout = 180000;
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                    service.Url = new Uri(DefaultExchangeWebServicesUri);
                    EmailMessage message = new EmailMessage(service);

                    message.Subject = "RE: " + RequestToSendApprovalEmail.MailSubject.ToString() + "";
                    if (RequestToSendApprovalEmail.RequstStatusID == (int)RequestStatus.ReOpenedByDoctor)
                    {
                        message.Body = Note + "<br/><br/><br/>" + Signature + SepratorLine + RequestToSendApprovalEmail.CreatedByNotes;
                        foreach (var item in RequestAttachments)
                        {
                            if (item.IsDoctorAttachment == true && item.IsReopeningAttachment == true)
                            {
                                string StaticPath = SaveTicketAttachmentPath + item.Path;
                                AttachmentsUrl.Add(StaticPath);
                            }
                        }
                    }
                    else if (RequestToSendApprovalEmail.RequstStatusID == (int)RequestStatus.AssignedByDoctor || RequestToSendApprovalEmail.RequstStatusID == (int)RequestStatus.EndTechnicalApproveByDoctor)
                    {
                        message.Body = Note + "<br/><br/><br/>" + Signature + SepratorLine + RequestToSendApprovalEmail.CreatedByNotes;
                        foreach (var item in RequestAttachments)
                        {
                            if (item.IsDoctorAttachment == true)
                            {
                                string StaticPath = SaveTicketAttachmentPath + item.Path;
                                AttachmentsUrl.Add(StaticPath);
                            }
                        }
                    }


                    if (RequestToSendApprovalEmail.RequstStatusID == (int)RequestStatus.ReOpenedByAudit)
                    {
                        message.Body = Note + "<br/><br/><br/>" + Signature + SepratorLine + RequestToSendApprovalEmail.CreatedByNotes;
                        foreach (var item in RequestAttachments)
                        {
                            if (item.IsAuditAttachment == true && item.IsReopeningAttachment == true)
                            {
                                string StaticPath = SaveTicketAttachmentPath + item.Path;
                                AttachmentsUrl.Add(StaticPath);
                            }
                        }
                    }

                    else if (RequestToSendApprovalEmail.RequstStatusID == (int)RequestStatus.AssignedByAudit || RequestToSendApprovalEmail.RequstStatusID == (int)RequestStatus.EndTechnicalApproveByAudit)
                    {
                        message.Body = Note + "<br/><br/><br/>" + Signature + SepratorLine + RequestToSendApprovalEmail.CreatedByNotes;
                        foreach (var item in RequestAttachments)
                        {
                            if (item.IsAuditAttachment == true)
                            {
                                string StaticPath = SaveTicketAttachmentPath + item.Path;
                                AttachmentsUrl.Add(StaticPath);
                            }
                        }
                    }




                    message.From = DefaultSendingMail;

                    foreach (var item in ToRecipients)
                    {
                        message.ToRecipients.Add(item);
                    }
                    foreach (var item in CcRecipients)
                    {
                        message.CcRecipients.Add(item);
                    }
                    foreach (var item in BccRecipients)
                    {
                        message.BccRecipients.Add(item);
                    }
                    foreach (var item in AttachmentsUrl)
                    {
                        message.Attachments.AddFileAttachment(item);
                    }

                    message.Save(new FolderId(WellKnownFolderName.SentItems, DefaultSendingMail));
                    message.SendAndSaveCopy(new FolderId(WellKnownFolderName.SentItems, DefaultSendingMail));

                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "Mailing.cs", "SendApprovalEmail", "CallCenterSystemApp");
                return false;
            }
        }
        public bool SendIquiryReplyMail(int RequestID, string InquiryReplyNote, int MemberType)
        {
            try
            {
                using (PhNetworkEntities db=new PhNetworkEntities())
                {
                    EmailApprovalRequest RequestToSendIquiryReplyMail = new EmailApprovalRequest();
                    RequestToSendIquiryReplyMail = (from r in db.EmailApprovalRequests where r.ID == RequestID select r).SingleOrDefault();
                    var ToRecipients = (from m in db.EmailRequestMailingDetails where m.TicketNumber == RequestID && m.IsTO == true && m.IsDeleted == false select m.Email).ToList();
                    var CcRecipients = (from m in db.EmailRequestMailingDetails where m.TicketNumber == RequestID && m.IsCC == true && m.IsDeleted == false select m.Email).ToList();
                    var BccRecipients = (from m in db.EmailRequestMailingDetails where m.TicketNumber == RequestID && m.IsBCC == true && m.IsDeleted == false select m.Email).ToList();
                    BccRecipients.Add("medgulf.developers@gmail.com");

                    if (MemberType == 1)
                    {
                        AutoDiscoverEmailServiceEmail = GeneralAutoDiscoverEmailServiceEmail;
                        DefaultSendingMail = GeneralDefaultSendingMail;
                        MailDomain = GeneralMailDomain;
                        MailUserName = GeneralMailUserName;
                        MailPassw0rd = GeneralMailPassw0rd;
                        Signature = GeneralSignature;
                        DefaultExchangeWebServicesUri = GeneralEmailExchangeWebSerivceUri;

                    }
                    if (MemberType == 2)
                    {
                        AutoDiscoverEmailServiceEmail = SPAutoDiscoverEmailServiceEmail;
                        DefaultSendingMail = SPDefaultSendingMail;
                        MailDomain = SPMailDomain;
                        MailUserName = SPMailUserName;
                        MailPassw0rd = SPMailPassw0rd;
                        Signature = SPCallCenterSignature;
                        DefaultExchangeWebServicesUri = SPEmailExchangeWebSerivceUri;

                    }

                    List<string> AttachmentsUrl = new List<string>();
                    var RequestAttachments = (from a in db.EmailRequestAttachmentsDetails where a.TicketNumber == RequestID select a).ToList();


                    ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2016);
                    if (IsDefaultEmailWorkingOnline(DefaultSendingMail) == "1")
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
                    service.UseDefaultCredentials = false;
                    service.Credentials = new WebCredentials(MailUserName, MailPassw0rd, MailDomain);
                    service.HttpHeaders.Add("X-AnchorMailbox", DefaultSendingMail);
                    service.HttpHeaders.Add("X-PublicFolderMailbox", DefaultSendingMail);
                    service.Timeout = 180000;
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                    service.Url = new Uri(DefaultExchangeWebServicesUri);
                    EmailMessage message = new EmailMessage(service);

                    message.Subject = "RE: " + RequestToSendIquiryReplyMail.MailSubject.ToString() + "";
                    message.Body = InquiryReplyNote + "<br/><br/><br/>" + Signature + SepratorLine + RequestToSendIquiryReplyMail.CreatedByNotes;
                    foreach (var item in RequestAttachments)
                    {
                        if (item.IsOtherAttachment == true)
                        {
                            string StaticPath = SaveTicketAttachmentPath + item.Path;
                            AttachmentsUrl.Add(StaticPath);
                        }
                    }
                    message.From = DefaultSendingMail;

                    foreach (var item in ToRecipients)
                    {
                        message.ToRecipients.Add(item);
                    }
                    foreach (var item in CcRecipients)
                    {
                        message.CcRecipients.Add(item);
                    }
                    foreach (var item in BccRecipients)
                    {
                        message.BccRecipients.Add(item);
                    }
                    foreach (var item in AttachmentsUrl)
                    {
                        message.Attachments.AddFileAttachment(item);
                    }

                    message.Save(new FolderId(WellKnownFolderName.SentItems, DefaultSendingMail));
                    message.SendAndSaveCopy(new FolderId(WellKnownFolderName.SentItems, DefaultSendingMail));

                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "Mailing.cs", "SendIquiryReplyMail", "CallCenterSystemApp");
                return false;
            }
        }

        public string IsDefaultEmailWorkingOnline(string DefaultEmail)
        {
            try
            {
                using (PhNetworkEntities db=new PhNetworkEntities())
                {
                    if (DefaultEmail== db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralCallCenterEmail").FirstOrDefault().ConfigurationValue.ToString())
                    {
                        if (db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralEmailWorkingOnline").FirstOrDefault().ConfigurationValue.ToString()=="1")
                        {
                            return "1";
                        }
                        else
                        {
                            return "0";
                        }
                    }
                  else  if (DefaultEmail == db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPCallCenterEmail").FirstOrDefault().ConfigurationValue.ToString())
                    {
                        if (db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPEmailWorkingOnline").FirstOrDefault().ConfigurationValue.ToString() == "1")
                        {
                            return "1";
                        }
                        else
                        {
                            return "0";
                        }
                    }
                    else if (DefaultEmail == db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "FaxCallCenterEmail").FirstOrDefault().ConfigurationValue.ToString())
                    {
                        if (db.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "FaxEmailWorkingOnline").FirstOrDefault().ConfigurationValue.ToString() == "1")
                        {
                            return "1";
                        }
                        else
                        {
                            return "0";
                        }
                    }
                    else
                    {
                        return "0";
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}