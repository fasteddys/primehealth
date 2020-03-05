﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AMS.Models;
using Microsoft.Exchange.WebServices.Data;
using static AMS.Enums.Enums;

namespace AMS.BLL
{
    public class EmailApprovalRequestBLL
    {
        public bool GetNewEmails(ExchangeService service, string EmailToGetMails, string SaveAttachmentPath, int EmailsToGetCount)
        {
            try
            {
                List<EmailMessage> UnReadMessages = new List<EmailMessage>();
                var IsNewEmails = EmailingService.GetSpecificSharedMailMessages(EmailToGetMails, service, EmailsToGetCount, out UnReadMessages);
                if (IsNewEmails)
                {
                    HandleNewEmails(UnReadMessages, EmailToGetMails, SaveAttachmentPath, service);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "EmailApprovalRequestBLL", "GetNewEmails", "GetNewEmails_BLL");
                return false;
            }
        }
        public void HandleNewEmails(List<EmailMessage> UnReadEmails, string ReceivingEmail, string AttachmentsPath, ExchangeService service)
        {
            try
            {
                using (PhNetworkEntities DbContext = new PhNetworkEntities())
                {
                    foreach (var item in UnReadEmails)
                    {
                        string EmailID = item.Id.ToString();
                        if (DbContext.EmailApprovalRequests.Any(o => o.AutoGeneratedEmailID.Equals(EmailID)) == false)
                        {
                            EmailApprovalRequest Request = new EmailApprovalRequest();
                            if (ReceivingEmail.ToLower() == DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "FaxCallCenterEmail").FirstOrDefault().ConfigurationValue.ToString().ToLower())
                            {
                                Request = new EmailApprovalRequest()
                                {
                                    CreationDate = item.DateTimeReceived,
                                    CreatedBy = "AutoGenerated",
                                    CreatedByNotes = item.Body.ToString() == string.Empty ? "." : item.Body.ToString(),
                                    RequstStatusID = (int)RequestStatus.NewAutoGenerated,
                                    MailSubject = item.Subject == null ? "." : item.Subject,
                                    IsAutoGenereated = true,
                                    AutoGeneratedEmailID = item.Id.ToString(),
                                    IsFaxMail = true,
                                    MailSource = ReceivingEmail,
                                    InsertionDate=DateTime.Now
                                };
                                DbContext.EmailApprovalRequests.Add(Request);
                            }
                            else
                            {
                                Request = new EmailApprovalRequest()
                                {
                                    CreationDate = item.DateTimeReceived,
                                    CreatedBy = "AutoGenerated",
                                    CreatedByNotes = item.Body.ToString() == string.Empty ? "." : item.Body.ToString(),
                                    RequstStatusID = (int)RequestStatus.NewAutoGenerated,
                                    MailSubject = item.Subject == null ? "." : item.Subject,
                                    IsAutoGenereated = true,
                                    AutoGeneratedEmailID = item.Id.ToString(),
                                    IsFaxMail = false,
                                    MailSource = ReceivingEmail,
                                    InsertionDate = DateTime.Now

                                };
                                DbContext.EmailApprovalRequests.Add(Request);
                            }
                            DbContext.SaveChanges();
                            bool AddMailingResult = SaveMailingDetails(item.From.Address.ToString(), item.ToRecipients.Select(p => p.Address).ToList(), item.CcRecipients.Select(p => p.Address).ToList(), Request.ID);
                            bool AddAttachmentsResult = SaveAttachments(Request.ID, AttachmentsPath, service, Request.AutoGeneratedEmailID);
                        }
                        else
                        {
                            // Ignore Duplicate Email
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "EmailApprovalRequestBLL", "HandleNewEmails", "HandleNewEmails_BLL");
            }
        }
        public bool SaveMailingDetails(string FromMail, List<string> ToRecipients, List<string> CcRecipients, int RequestNumber)
        {
            try
            {
                using (PhNetworkEntities dbContext = new PhNetworkEntities())
                {
                    //Add Client Email

                    EmailRequestMailingDetail FromMailingDetails = new EmailRequestMailingDetail()
                    {
                        Email = FromMail.ToString(),
                        IsTO = true,
                        IsCC = false,
                        IsBCC = false,
                        IsDeleted = false,
                        TicketNumber = RequestNumber
                    };

                    dbContext.EmailRequestMailingDetails.Add(FromMailingDetails);

                    //Add To Emails

                    if (ToRecipients.Any())
                    {
                        foreach (var item in ToRecipients)
                        {
                            var CallCenterGeneralMail = dbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralCallCenterEmail").FirstOrDefault().ConfigurationValue;
                            var SpCallCenterGeneralMail = dbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPCallCenterEmail").FirstOrDefault().ConfigurationValue;

                            if (item.ToLower() == CallCenterGeneralMail || item.ToLower() == SpCallCenterGeneralMail)
                            {
                                //Ignore
                            }
                            else
                            {
                                EmailRequestMailingDetail ToMailingDetails = new EmailRequestMailingDetail()
                                {
                                    Email = item.ToString(),
                                    IsTO = true,
                                    IsCC = false,
                                    IsBCC = false,
                                    IsDeleted = false,
                                    TicketNumber = RequestNumber
                                };
                                dbContext.EmailRequestMailingDetails.Add(ToMailingDetails);
                            }
                        }

                    }

                    //Add Cc List

                    if (CcRecipients.Any())
                    {
                        foreach (var item in CcRecipients)
                        {
                            EmailRequestMailingDetail CcMailingDetails = new EmailRequestMailingDetail()
                            {
                                Email = item.ToString(),
                                IsTO = false,
                                IsCC = true,
                                IsBCC = false,
                                IsDeleted = false,
                                TicketNumber = RequestNumber
                            };
                            dbContext.EmailRequestMailingDetails.Add(CcMailingDetails);
                        }

                    }

                    int Result = dbContext.SaveChanges();
                    if (Result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "EmailApprovalRequestBLL", "SaveMailingDetails", "SaveMailingDetails_BLL");
                return false;
            }
        }
        public bool SaveAttachments(int RequestID, string SavingPath, ExchangeService service, ItemId itemId)
        {
            try
            {
                using (PhNetworkEntities DBContext = new PhNetworkEntities())
                {
                    EmailMessage Message = EmailMessage.Bind(service, itemId, new PropertySet(ItemSchema.Attachments));
                    string FilePath = SavingPath + "EmailRequestAttach\\" + RequestID + "\\" + "TicketAttach\\";
                    string BackupPath = DBContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "AttachmentsBackupPath").FirstOrDefault().ConfigurationValue;
                    string BackupFullPath = BackupPath + "EmailRequestAttach\\" + RequestID + "\\" + "TicketAttach\\";
                    Message.Load();
                    if (Message.HasAttachments)
                    {
                        int InlineCounter = 0;

                        foreach (Attachment attachment in Message.Attachments)
                        {
                            if (attachment is FileAttachment)
                            {
                                if (attachment.IsInline)
                                {
                                    string sHTMLCOntent = Message.Body.Text;
                                    FileAttachment[] attachments = new FileAttachment[100];

                                    string sType = attachment.ContentType.ToLower();
                                    if (sType.Contains("image"))
                                    {
                                        attachments[InlineCounter] = (FileAttachment)attachment;
                                        string sID = attachments[InlineCounter].ContentId;
                                        sType = sType.Replace("image/", "");
                                        string sFilename = sID + "." + sType;
                                        if (sFilename != string.Empty)
                                        {
                                            if (!Directory.Exists(FilePath))
                                            {
                                                DirectoryInfo di = Directory.CreateDirectory(FilePath);
                                            }
                                            if (DBContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "TakeBackup").FirstOrDefault().ConfigurationValue == "1")
                                            {
                                                if (!Directory.Exists(BackupFullPath))
                                                {
                                                    DirectoryInfo BackupDi = Directory.CreateDirectory(BackupFullPath);
                                                }
                                            }
                                        }
                                        attachments[InlineCounter].Load(FilePath + sFilename);
                                        if (DBContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "TakeBackup").FirstOrDefault().ConfigurationValue == "1")
                                        {
                                            attachments[InlineCounter].Load(BackupFullPath + sFilename);
                                        }
                                        string oldString = "cid:" + sID;
                                        sHTMLCOntent = sHTMLCOntent.Replace(oldString, FilePath + sFilename);
                                        if (InlineCounter < Message.Attachments.Count)
                                        {
                                            InlineCounter++;
                                        }
                                        EmailRequestAttachmentsDetail EmailRequestAttachDetails = new EmailRequestAttachmentsDetail();
                                        EmailRequestAttachDetails.Name = sFilename;
                                        EmailRequestAttachDetails.Path = "/EmailRequestAttach/" + RequestID + "/" + "TicketAttach" + "/" + sFilename;
                                        EmailRequestAttachDetails.IsTicketAttachment = true;
                                        EmailRequestAttachDetails.IsDoctorAttachment = false;
                                        EmailRequestAttachDetails.IsAuditAttachment = false;
                                        EmailRequestAttachDetails.IsOtherAttachment = false;
                                        EmailRequestAttachDetails.IsTransferToAuditAttach = false;
                                        EmailRequestAttachDetails.IsReopeningAttachment = false;
                                        EmailRequestAttachDetails.IsFaxAttachment = false;
                                        EmailRequestAttachDetails.IsDeleted = false;
                                        //EmailRequestAttachDetails
                                        EmailRequestAttachDetails.TicketNumber = RequestID;
                                        DBContext.EmailRequestAttachmentsDetails.Add(EmailRequestAttachDetails);
                                    }
                                }

                                else
                                {
                                    FileAttachment fileAttachment = attachment as FileAttachment;

                                    if (fileAttachment.Name != string.Empty)
                                    {
                                        if (!Directory.Exists(FilePath))
                                        {
                                            DirectoryInfo di = Directory.CreateDirectory(FilePath);
                                        }
                                        if (DBContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "TakeBackup").FirstOrDefault().ConfigurationValue == "1")
                                        {
                                            if (!Directory.Exists(BackupFullPath))
                                            {
                                                DirectoryInfo BackupDi = Directory.CreateDirectory(BackupFullPath);
                                            }
                                        }
                                    }
                                    fileAttachment.Load(FilePath + fileAttachment.Name);
                                    if (DBContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "TakeBackup").FirstOrDefault().ConfigurationValue == "1")
                                    {
                                        fileAttachment.Load(BackupFullPath + fileAttachment.Name);
                                    }
                                    EmailRequestAttachmentsDetail EmailRequestAttachDetails = new EmailRequestAttachmentsDetail();
                                    EmailRequestAttachDetails.Name = fileAttachment.Name;
                                    EmailRequestAttachDetails.Path = "/EmailRequestAttach/" + RequestID + "/" + "TicketAttach" + "/" + fileAttachment.Name;
                                    EmailRequestAttachDetails.IsTicketAttachment = true;
                                    EmailRequestAttachDetails.IsDoctorAttachment = false;
                                    EmailRequestAttachDetails.IsAuditAttachment = false;
                                    EmailRequestAttachDetails.IsOtherAttachment = false;
                                    EmailRequestAttachDetails.IsTransferToAuditAttach = false;
                                    EmailRequestAttachDetails.IsReopeningAttachment = false;
                                    EmailRequestAttachDetails.IsFaxAttachment = false;
                                    EmailRequestAttachDetails.IsDeleted = false;
                                    EmailRequestAttachDetails.TicketNumber = RequestID;
                                    DBContext.EmailRequestAttachmentsDetails.Add(EmailRequestAttachDetails);
                                }
                            }
                            else // Attachment is an item attachment.
                            {
                                ItemAttachment itemAttachment = attachment as ItemAttachment;
                                itemAttachment.Load(ItemSchema.MimeContent);
                                string fileName = FilePath + Regex.Replace(itemAttachment.Item.Subject, @"\s+", "") + ".eml";
                                string backupPath = BackupFullPath + Regex.Replace(itemAttachment.Item.Subject, @"\s+", "") + ".eml";
                                if (itemAttachment.Item.Subject != string.Empty)
                                {
                                    if (!Directory.Exists(FilePath))
                                    {
                                        DirectoryInfo di = Directory.CreateDirectory(FilePath);
                                    }
                                    if (DBContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "TakeBackup").FirstOrDefault().ConfigurationValue == "1")
                                    {
                                        if (!Directory.Exists(BackupFullPath))
                                        {
                                            DirectoryInfo BackupDi = Directory.CreateDirectory(BackupFullPath);
                                        }
                                    }
                                }
                                try
                                {
                                    File.WriteAllBytes(fileName, itemAttachment.Item.MimeContent.Content);
                                }
                                catch (Exception ex)
                                {
                                }
                                if (DBContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "TakeBackup").FirstOrDefault().ConfigurationValue == "1")
                                {
                                    try
                                    {
                                        File.WriteAllBytes(backupPath, itemAttachment.Item.MimeContent.Content);
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                }
                                EmailRequestAttachmentsDetail EmailRequestAttachDetails = new EmailRequestAttachmentsDetail();
                                EmailRequestAttachDetails.Name = Regex.Replace(itemAttachment.Item.Subject, @"\s+", "") + ".eml";
                                EmailRequestAttachDetails.Path = "/EmailRequestAttach/" + RequestID + "/" + "TicketAttach" + "/" + Regex.Replace(itemAttachment.Item.Subject, @"\s+", "") + ".eml";
                                EmailRequestAttachDetails.IsTicketAttachment = true;
                                EmailRequestAttachDetails.IsDoctorAttachment = false;
                                EmailRequestAttachDetails.IsAuditAttachment = false;
                                EmailRequestAttachDetails.IsOtherAttachment = false;
                                EmailRequestAttachDetails.IsTransferToAuditAttach = false;
                                EmailRequestAttachDetails.IsReopeningAttachment = false;
                                EmailRequestAttachDetails.IsFaxAttachment = false;
                                EmailRequestAttachDetails.IsDeleted = false;
                                EmailRequestAttachDetails.TicketNumber = RequestID;
                                DBContext.EmailRequestAttachmentsDetails.Add(EmailRequestAttachDetails);
                            }
                        }
                        int Result = DBContext.SaveChanges();
                        if (Result > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "EmailApprovalRequestBLL", "SaveAttachments", "SaveAttachments_BLL");
                return false;
            }
        }
        public string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}