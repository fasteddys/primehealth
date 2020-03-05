using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.DirectoryServices;
using System.Linq;
using System.Web.Http;

namespace WebService.Controllers
{
    public class TestNewController : ApiController
    {
        [HttpGet]
        public void Test()

        {
            ExchangeService service = new ExchangeService();
            service.AutodiscoverUrl("Mahetab.Muhammad@Prime-Health.org");
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials("Mahetab.Muhammad", "prime@123", "primehealth.local");

            FolderId SharedMailbox = new FolderId(WellKnownFolderName.Inbox, "CallCenter@Prime-Health.org");

            ItemView itemView = new ItemView(int.MaxValue);
            var Emails = service.FindItems(SharedMailbox, itemView);
            List<EmailMessage> FinalList = new List<EmailMessage>();
            foreach (EmailMessage msg in Emails)
            {
                //Retrieve Additional data for Email
                EmailMessage message = EmailMessage.Bind(service,
                     (EmailMessage.Bind(service, msg.Id)).Id,
                      new PropertySet(BasePropertySet.FirstClassProperties,
                      new ExtendedPropertyDefinition(0x1013, MapiPropertyType.Binary)));
                FinalList.Add(message);
                //message.IsRead = true;
                //message.Flag.FlagStatus = ItemFlagStatus.Complete;
                //message.Flag.StartDate = DateTime.Now;
                //message.Flag.DueDate = DateTime.Now;
                //message.Update(ConflictResolutionMode.AlwaysOverwrite);
            }

            var x = FinalList.GroupBy(p => p.Id);


        }

        [HttpGet]
        public void test2()
        {
            ExchangeService service = new ExchangeService();

            //service.AutodiscoverUrl("noreply@prime-health.org");
            //service.UseDefaultCredentials = false;
            //service.Credentials = new WebCredentials("noreply", "NoP@ssw0rd", "primehealth.local");
            service.AutodiscoverUrl("Mahetab.Muhammad@Prime-Health.org");
            service.UseDefaultCredentials = false;
            service.Credentials = new WebCredentials("Mahetab.Muhammad", "prime@123", "primehealth.local");

            EmailMessage message = new EmailMessage(service);
            message.Subject = "Test Mail";
            message.Body = "ميل من ايميل الكول سنتر";
            message.From = "CallCenter@Prime-Health.org";
            message.ToRecipients.Add("IT-Developers@Prime-Health.org");
            // message.Attachments.AddFileAttachment("C:\\temp2\\ssss.txt");
            //message.Save();
            message.Send();

        }

        [HttpGet]
        public void test3()
        {

            var x = GetGAL("develop.admin", "P@ssw0rdis!P@ssw0rd");

            //ExchangeService service = new ExchangeService();
            //service.AutodiscoverUrl("Ticket.System@Prime-Health.org");
            //service.UseDefaultCredentials = false;
            //service.Credentials = new WebCredentials("ticket-sys", "prime@4321", "primehealth.local");
            //EmailMessage message = new EmailMessage(service);
            //message.Subject = "Call Center windows service has been stoped Working";
            //message.Body = "Call Center windows service has been stoped Working";
            //message.From = "Ticket.System@Prime-Health.org";
            //message.ToRecipients.Add("IT@Prime-Health.org");
            //message.Attachments.AddFileAttachment("C:\\temp2\\ssss.txt");
            //message.Save();
            //message.SendAndSaveCopy();

        }
        [HttpGet]
        public static Dictionary<string,string> GetGAL(string UserName, string Password)
        {
            try
            {
                Dictionary<string, string> ReturnArray=new Dictionary<string, string>();
                DirectoryEntry deDirEntry = new DirectoryEntry("LDAP://DC=Primehealth,DC=local", UserName,Password,AuthenticationTypes.Secure);
                DirectorySearcher mySearcher = new DirectorySearcher(deDirEntry);
                mySearcher.PropertiesToLoad.Add("cn");
                mySearcher.PropertiesToLoad.Add("mail");

                string sFilter = String.Format("(&(mailnickname=*)(|(objectcategory=user)(objectcategory=group)))");

                mySearcher.Filter = sFilter;
                mySearcher.Sort.Direction = System.DirectoryServices.SortDirection.Ascending;
                mySearcher.Sort.PropertyName = "cn";
                mySearcher.PageSize = 1000;

                SearchResultCollection results;
                results = mySearcher.FindAll();

                foreach (SearchResult resEnt in results)
                {
                    ResultPropertyCollection propcoll = resEnt.Properties;
                    string Name = "";
                    string Mail = "";
                    foreach (string key in propcoll.PropertyNames)
                    {
                        if (key == "cn")
                        {
                            foreach (object values in propcoll[key])
                            {
                               
                                Name = values.ToString();
                            }
                        }
                        else if (key == "mail")
                        {
                            foreach (object values in propcoll[key])
                            {
                                Mail = values.ToString();
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Mail))
                    {
                        if (ReturnArray.TryGetValue(Name, out string myValue))
                        {
                            // Deplicate
                        }
                        else
                        {
                            ReturnArray.Add(Name, Mail);
                        }
                    }
                }
                return ReturnArray;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
