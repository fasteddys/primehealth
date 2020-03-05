using Microsoft.Exchange.WebServices.Data;
using System;
using System.Linq;
using System.Web.Http;

namespace WebService.Controllers
{
    public class TestController : ApiController
    {
        //
        // GET: /Test/
        [HttpGet]
        public void test()

        {
            ExchangeService service = new ExchangeService();
            service.AutodiscoverUrl("noreply@prime-health.org");

            FolderId SharedMailbox = new FolderId(WellKnownFolderName.Inbox, "Support-SYS@MEDGULF.COM.EG");
            ItemView itemView = new ItemView(1000);
            var x = service.FindItems(SharedMailbox, itemView);

            foreach (var item in x)
            {
                var e = item.Subject;
                GetAttachmentsFromEmail(service, item.Id);
            }

        }
        public static void GetAttachmentsFromEmail(ExchangeService service, ItemId itemId)
        {
            // Bind to an existing message item and retrieve the attachments collection.
            // This method results in an GetItem call to EWS.
            EmailMessage message = EmailMessage.Bind(service, itemId, new PropertySet(ItemSchema.Attachments));
            // Iterate through the attachments collection and load each attachment.
            foreach (Attachment attachment in message.Attachments)
            {
                if (attachment is FileAttachment)
                {
                    FileAttachment fileAttachment = attachment as FileAttachment;
                    // Load the attachment into a file.
                    // This call results in a GetAttachment call to EWS.
                    fileAttachment.Load("C:\\temp\\" + fileAttachment.Name);

                    Console.WriteLine("File attachment name: " + fileAttachment.Name);
                }
                else // Attachment is an item attachment.
                {
                    ItemAttachment itemAttachment = attachment as ItemAttachment;
                    // Load attachment into memory and write out the subject.
                    // This does not save the file like it does with a file attachment.
                    // This call results in a GetAttachment call to EWS.
                    itemAttachment.Load();
                    Console.WriteLine("Item attachment name: " + itemAttachment.Name);
                }
            }
        }
        [HttpGet]
        public void TestGetBody()
        {
            ExchangeService service = new ExchangeService();
            service.AutodiscoverUrl("noreply@prime-health.org");

            var items = service.FindItems(new FolderId(WellKnownFolderName.Inbox, new Mailbox("Support-SYS@MEDGULF.COM.EG")),new ItemView(15));

        }

    }
}
