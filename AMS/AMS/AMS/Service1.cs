using AMS.BLL;
using AMS.Models;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Timers;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace AMS
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer(); // name space(using System.Timers;)  
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            using (PhNetworkEntities Db=new PhNetworkEntities())
            {
                double ElapsedTime = int.Parse((from r in Db.EmailApprovalsConfigurations where r.ConfigurationKey == "AMSTimeElapsedTimeInms" select r.ConfigurationValue).SingleOrDefault());
                WriteToFile("Service is started at " + DateTime.Now);

                timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);

                timer.Interval = ElapsedTime; //number in milisecinds  

                timer.Enabled = true;

            }
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteToFile("AMS_2 started at " + DateTime.Now);
            WriteToFile("Getting General Mail started at " + DateTime.Now);
            GetGeneralCallCenterEmails();
            WriteToFile("Finish Getting General Mail at " + DateTime.Now);
            WriteToFile("Getting SP Mail started at " + DateTime.Now);
            GetSPCallCenterEmails();
            WriteToFile("Finish Getting SP Mail at " + DateTime.Now);
            WriteToFile("Getting Fax Mail started at " + DateTime.Now);
            GetFaxEmails();
            WriteToFile("Finish Getting Fax Mail at " + DateTime.Now);
            WriteToFile("AMS_2 successed at" + DateTime.Now);
        }
        protected override void OnStop()
        {
            using (PhNetworkEntities db = new PhNetworkEntities())
            {
                var Tomail = (from t in db.EmailApprovalsConfigurations where t.ConfigurationKey == "AMSStopedWorkingMail" && t.ConfigurationValue != null select t.ConfigurationValue).ToList();
                ExchangeService service = new ExchangeService();
                service.Url = new Uri(db.EmailApprovalsConfigurations.Where(x => x.ConfigurationKey == "LocalExchangeWebServicesUri").FirstOrDefault().ConfigurationValue);
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("noreply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "AMS has stopped working";
                message.Body = "AMS has stopped working , Please Check";
                message.From = "noreply@prime-health.org";
                foreach (var item in Tomail)
                {
                    message.ToRecipients.Add(item);
                }

                message.SendAndSaveCopy();

            }
        }
        public void GetGeneralCallCenterEmails()
        {
            try
            {
                using (PhNetworkEntities DbContext = new PhNetworkEntities())
                {
                    ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2016);
                    EmailApprovalRequestBLL EmailApprovalRequest = new EmailApprovalRequestBLL();
                    string GeneralUserName = DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralEmailUserName").FirstOrDefault().ConfigurationValue;
                    string GeneralPassword = DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralEmailPassword").FirstOrDefault().ConfigurationValue;
                    string GeneralDomain = DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralEmailDomain").FirstOrDefault().ConfigurationValue;
                    int EmailsToGetCount = Convert.ToInt32(DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralEmailCount").FirstOrDefault().ConfigurationValue);
                    string GeneralEmail = DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralCallCenterEmail").FirstOrDefault().ConfigurationValue;
                    string RequestAttachPath = DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SaveTicketAttachmentPath").FirstOrDefault().ConfigurationValue;
                    service.Credentials = new WebCredentials(GeneralUserName, GeneralPassword, GeneralDomain);
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                    service.Url = new Uri(DbContext.EmailApprovalsConfigurations.Where(x => x.ConfigurationKey == "GeneralExchangeWebServicesUri").FirstOrDefault().ConfigurationValue);
                    var IsGeneralEmailWorkingOnline= DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "GeneralEmailWorkingOnline").FirstOrDefault().ConfigurationValue;
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
                    EmailApprovalRequest.GetNewEmails(service, GeneralEmail, RequestAttachPath, EmailsToGetCount);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "Service1.cs", "GetGeneralCallCenterEmails", "Front Service");
            }
        }
        public void GetSPCallCenterEmails()
        {
            try
            {
                using (PhNetworkEntities DbContext = new PhNetworkEntities())
                {
                    ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2016);
                    EmailApprovalRequestBLL EmailApprovalRequest = new EmailApprovalRequestBLL();
                    string SPUserName = DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPEmailUserName").FirstOrDefault().ConfigurationValue;
                    string SPPassword = DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPEmailPassword").FirstOrDefault().ConfigurationValue;
                    string SPDomain = DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPEmailDomain").FirstOrDefault().ConfigurationValue;
                    int EmailsToGetCount = Convert.ToInt32(DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPEmailCount").FirstOrDefault().ConfigurationValue);
                    string SPEmail = DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPCallCenterEmail").FirstOrDefault().ConfigurationValue;
                    string RequestAttachPath = DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SaveTicketAttachmentPath").FirstOrDefault().ConfigurationValue;
                    service.Credentials = new WebCredentials(SPUserName, SPPassword, SPDomain);
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                    service.Url = new Uri(DbContext.EmailApprovalsConfigurations.Where(x=>x.ConfigurationKey== "SPExchangeWebServicesUri").FirstOrDefault().ConfigurationValue);
                    var IsSPEmailWorkingOnline = DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SPEmailWorkingOnline").FirstOrDefault().ConfigurationValue;
                    service.UseDefaultCredentials = false;
                    if (IsSPEmailWorkingOnline == "1")
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
                    EmailApprovalRequest.GetNewEmails(service, SPEmail, RequestAttachPath, EmailsToGetCount);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "Service1.cs", "GetSPCallCenterEmails", "Front Service");
            }
        }
        public void GetFaxEmails()
        {
            try
            {
                using (PhNetworkEntities DbContext = new PhNetworkEntities())
                {
                    ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2016);
                    EmailApprovalRequestBLL EmailApprovalRequest = new EmailApprovalRequestBLL();
                    string FaxUserName = DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "FaxEmailUserName").FirstOrDefault().ConfigurationValue;
                    string FaxPassword = DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "FaxEmailPassword").FirstOrDefault().ConfigurationValue;
                    string FaxDomain = DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "FaxEmailDomain").FirstOrDefault().ConfigurationValue;
                    //string FaxAutodiscoverUrl = DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "FaxEmailAutoDiscoverUrl").FirstOrDefault().ConfigurationValue;
                    int FaxEmailsToGetCount = Convert.ToInt32(DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "FaxEmailCount").FirstOrDefault().ConfigurationValue);
                    string FaxEmail = DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "FaxCallCenterEmail").FirstOrDefault().ConfigurationValue;
                    string RequestAttachPath = DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "SaveTicketAttachmentPath").FirstOrDefault().ConfigurationValue;
                    service.Credentials = new WebCredentials(FaxUserName, FaxPassword, FaxDomain);
                    ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                    service.Url = new Uri(DbContext.EmailApprovalsConfigurations.Where(x => x.ConfigurationKey == "FaxExchangeWebServicesUri").FirstOrDefault().ConfigurationValue);
                    service.UseDefaultCredentials = false;
                    var IsFaxEmailWorkingOnline = DbContext.EmailApprovalsConfigurations.Where(p => p.ConfigurationKey == "FaxEmailWorkingOnline").FirstOrDefault().ConfigurationValue;
                    if (IsFaxEmailWorkingOnline == "1")
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
                    EmailApprovalRequest.GetNewEmails(service, FaxEmail, RequestAttachPath, FaxEmailsToGetCount);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "Service1.cs", "GetFaxEmails", "Front Service");
            }

        }
        public void WriteToFile(string Message)
        {

            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";

            if (!Directory.Exists(path))
            {

                Directory.CreateDirectory(path);

            }

            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

            if (!File.Exists(filepath))
            {

                // Create a file to write to.   

                using (StreamWriter sw = File.CreateText(filepath))
                {

                    sw.WriteLine(Message);

                }

            }
            else
            {

                using (StreamWriter sw = File.AppendText(filepath))
                {

                    sw.WriteLine(Message);

                }

            }

        }
        public void WriteExceptionToFile(string Message)
        {

            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Exception";

            if (!Directory.Exists(path))
            {

                Directory.CreateDirectory(path);

            }

            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Exception\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

            if (!File.Exists(filepath))
            {

                // Create a file to write to.   

                using (StreamWriter sw = File.CreateText(filepath))
                {

                    sw.WriteLine(Message);

                }

            }
            else
            {

                using (StreamWriter sw = File.AppendText(filepath))
                {

                    sw.WriteLine(Message);

                }

            }

        }
    }
}