using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.WebServices.Data;

namespace AMS.BLL
{
    public class EmailingService
    {
        public static bool GetSpecificSharedMailMessages(string EmailAddress, ExchangeService service, int EmailsToGetCount, out List<EmailMessage> NewMessages)
        {
            try
            {
                ItemView itemView = new ItemView(EmailsToGetCount);
                itemView.PropertySet = PropertySet.IdOnly;
                FindItemsResults<Item> findResults;
                findResults = service.FindItems(new FolderId(WellKnownFolderName.Inbox, EmailAddress), itemView);
                List<EmailMessage> FinalEmailsList = new List<EmailMessage>();
                foreach (var item in findResults.Items)
                {
                    FinalEmailsList.Add((EmailMessage)item);
                }
                PropertySet properties = (BasePropertySet.FirstClassProperties);
                service.LoadPropertiesForItems(FinalEmailsList, properties);             
                if (FinalEmailsList.Count > 0)
                {
                    NewMessages = FinalEmailsList;
                    return true;
                }
                else
                {
                    NewMessages = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, "EmailingService", "GetSpecificSharedMailMessages_"+EmailAddress+"", "GetSpecificSharedMailMessages_EmailingService");
                NewMessages = null;
                return false;
            }
        }

    }
}