using System;
using UpdateEmailExchangeStatus_WS.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;
using System.Net;
using System.IO;

namespace UpdateEmailExchangeStatus_WS.BLL
{
    class EmailBLL
    {
        public PhNetworkEntities Context;
        private DateTime CreationDate;
        private ExchangeService service;
        private string EmailAddress;
        public EmailBLL()
        {
            Context = new PhNetworkEntities();
            CreationDate = DateTime.Now;
            service = new ExchangeService();
            EmailAddress = "";
        }
        ~EmailBLL()
        {
            Context?.Dispose();
            GC.SuppressFinalize(this);
        }
        private void GetCallCenterEmailConfiguration()
        {
            string GeneralUserName = Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralEmailUserName").FirstOrDefault().ConfigurationValue;
            string GeneralPassword = Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralEmailPassword").FirstOrDefault().ConfigurationValue;
            string GeneralDomain = Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralEmailDomain").FirstOrDefault().ConfigurationValue;
            string GeneralAutodiscoverUrl = Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralAutoDiscoverUrl").FirstOrDefault().ConfigurationValue;
            EmailAddress = Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralCallCenterEmail").FirstOrDefault().ConfigurationValue;
            service.Credentials = new WebCredentials(GeneralUserName, GeneralPassword, GeneralDomain);
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            service.Url = new Uri(Context.EmailApprovalsConfigurations.Where(x => x.ConfigurationKey == "GeneralExchangeWebServicesUri").FirstOrDefault().ConfigurationValue);
            var IsGeneralEmailWorkingOnline = Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralEmailWorkingOnline").FirstOrDefault().ConfigurationValue;
            service.UseDefaultCredentials = false;
            if (IsGeneralEmailWorkingOnline == "1")
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
        }
        private void GetSPConfiguration()
        {
            string SPUserName = Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPEmailUserName").FirstOrDefault().ConfigurationValue;
            string SPPassword = Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPEmailPassword").FirstOrDefault().ConfigurationValue;
            string SPDomain = Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPEmailDomain").FirstOrDefault().ConfigurationValue;
            string SPAutodiscoverUrl = Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPAutoDiscoverUrl").FirstOrDefault().ConfigurationValue;
            EmailAddress = Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPCallCenterEmail").FirstOrDefault().ConfigurationValue;
            service.Credentials = new WebCredentials(SPUserName, SPPassword, SPDomain);
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            service.Url = new Uri(Context.EmailApprovalsConfigurations.Where(x => x.ConfigurationKey == "SPExchangeWebServicesUri").FirstOrDefault().ConfigurationValue);
            var IsGeneralEmailWorkingOnline = Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralEmailWorkingOnline").FirstOrDefault().ConfigurationValue;
            service.UseDefaultCredentials = false;
            if (IsGeneralEmailWorkingOnline == "1")
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
        }
        private void GetFaxConfiguration()
        {
            string FaxUserName = Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "FaxEmailUserName").FirstOrDefault().ConfigurationValue;
            string FaxPassword = Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "FaxEmailPassword").FirstOrDefault().ConfigurationValue;
            string FaxDomain = Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "FaxEmailDomain").FirstOrDefault().ConfigurationValue;
            string FaxAutodiscoverUrl = Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "FaxEmailAutoDiscoverUrl").FirstOrDefault().ConfigurationValue;
            EmailAddress = Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "FaxCallCenterEmail").FirstOrDefault().ConfigurationValue;
            service.Credentials = new WebCredentials(FaxUserName, FaxPassword, FaxDomain);
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            service.Url = new Uri(Context.EmailApprovalsConfigurations.Where(x => x.ConfigurationKey == "FaxExchangeWebServicesUri").FirstOrDefault().ConfigurationValue);
            var IsGeneralEmailWorkingOnline = Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralEmailWorkingOnline").FirstOrDefault().ConfigurationValue;
            service.UseDefaultCredentials = false;
            if (IsGeneralEmailWorkingOnline == "1")
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
        }
        public void UpdateEmail()
        {
            List<EmailApprovalRequest> emailApprovalRequests = Context.EmailApprovalRequests.Where(x => x.IsAutoGenereated == true && x.IsCompleted != true).ToList();
            string generalCallCenterEmail = Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralCallCenterEmail").FirstOrDefault().ConfigurationValue;
            string spCallCenterEmail = Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPCallCenterEmail").FirstOrDefault().ConfigurationValue;
            string faxCallCenterEmail = Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "FaxCallCenterEmail").FirstOrDefault().ConfigurationValue;
            int emailCount = 0;

            foreach (var items in emailApprovalRequests)
            {
                try
                {
                    if (items.MailSource.Equals(generalCallCenterEmail))
                    {
                        GetCallCenterEmailConfiguration();
                        emailCount = Convert.ToInt32(Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralEmailCount").FirstOrDefault().ConfigurationValue);
                    }
                    else if (items.MailSource.Equals(spCallCenterEmail))
                    {
                        GetSPConfiguration();
                        emailCount = Convert.ToInt32(Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPEmailCount").FirstOrDefault().ConfigurationValue);
                    }
                    else if (items.MailSource.Equals(faxCallCenterEmail))
                    {
                        GetFaxConfiguration();
                        emailCount = Convert.ToInt32(Context.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "FaxEmailCount").FirstOrDefault().ConfigurationValue);
                    }

                    FolderId SharedMailbox = new FolderId(WellKnownFolderName.Inbox, EmailAddress);
                    //ItemView itemView = new ItemView(int.MaxValue) { PropertySet = new PropertySet(BasePropertySet.IdOnly) };
                    //var Email = service.FindItems(SharedMailbox, itemView).Where(p => p.Id.UniqueId == items.AutoGeneratedEmailID).FirstOrDefault();
                    //EmailMessage message = EmailMessage.Bind(service, (EmailMessage.Bind(service, Email.Id)).Id, new PropertySet(BasePropertySet.FirstClassProperties, new ExtendedPropertyDefinition(0x1013, MapiPropertyType.Binary)));
                    EmailMessage message = EmailMessage.Bind(service, new ItemId(items.AutoGeneratedEmailID));

                    if (items.IsReadyToBeFollowedUp == true && items.IsReadyToBeCompleted == null)
                    {
                        items.IsReadyToBeFollowedUp = false;

                        message.IsRead = true;
                        message.Flag.FlagStatus = ItemFlagStatus.Flagged;
                        message.Flag.StartDate = DateTime.Now;
                        message.Flag.DueDate = DateTime.Now;
                        message.Update(ConflictResolutionMode.AlwaysOverwrite);
                    }
                    else if (items.IsReadyToBeFollowedUp == false && items.IsReadyToBeCompleted == true)
                    {
                        items.IsReadyToBeCompleted = false;
                        items.IsCompleted = true;

                        message.Flag.FlagStatus = ItemFlagStatus.Complete;
                        message.Flag.CompleteDate = DateTime.Now;
                        message.Update(ConflictResolutionMode.AlwaysOverwrite);
                    }
                    else if (items.IsReadyToBeFollowedUp == true && items.IsReadyToBeCompleted == true)
                    {
                        items.IsReadyToBeFollowedUp = false;
                        items.IsReadyToBeCompleted = false;
                        items.IsCompleted = true;

                        message.IsRead = true;
                        message.Flag.FlagStatus = ItemFlagStatus.Flagged;
                        message.Flag.StartDate = DateTime.Now;
                        message.Flag.DueDate = DateTime.Now;
                        message.Update(ConflictResolutionMode.AlwaysOverwrite);

                        message.Flag.FlagStatus = ItemFlagStatus.Complete;
                        message.Flag.CompleteDate = DateTime.Now;
                        message.Update(ConflictResolutionMode.AlwaysOverwrite);
                    }
                    Context.SaveChanges();
                }
                catch (Exception ex)
                {
                    //continue;
                    FileBLL.WriteToFile("Request ID " + items.ID + "Error " + ex.Message);
                    throw ex;
                }

            }
        }
        
    }
}
