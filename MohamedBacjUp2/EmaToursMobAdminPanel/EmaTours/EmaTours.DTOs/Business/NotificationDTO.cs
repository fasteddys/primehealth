using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.DTOs.Business
{
   public class NotificationDTO
    {
        public int NotificationID { get; set; }
        public Nullable<int> NotificationDirectionFK { get; set; }
        public Nullable<int> SenderFK { get; set; }
        public Nullable<int> RecipientFK { get; set; }
        public Nullable<int> NotificationMethodFK { get; set; }
        public Nullable<int> RequestTypeFK { get; set; }
        public Nullable<int> RequestFK { get; set; }
        public Nullable<bool> IsSent { get; set; }
        public Nullable<System.DateTime> SentTime { get; set; }
        public Nullable<bool> IsReceived { get; set; }
        public Nullable<System.DateTime> ReceivedTime { get; set; }
        public Nullable<bool> IsViewed { get; set; }
        public Nullable<int> ViewedPersonFK { get; set; }
        public Nullable<System.DateTime> ViewedTime { get; set; }
        public string DeviceID { get; set; }
        public string NotificationHeader { get; set; }
        public string NotificationBody { get; set; }
        public string NotificationLink { get; set; }
        public Nullable<int> LanguageFK { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public int? VisitID { get; set; }

        public Nullable<System.DateTime> CreationDate { get; set; }
    }
}
