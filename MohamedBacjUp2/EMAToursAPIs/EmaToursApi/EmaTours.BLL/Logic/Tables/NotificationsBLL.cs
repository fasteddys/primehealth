using EmaTours.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmaTours.Entities;
using EmaTours.DTOs.Business;
using static EmaTours.BLL.StaticData.StaticEnums;

namespace EmaTours.BLL.Logic.Tables
{
    public class NotificationsBLL : Repository<Notification>
    {
        DateTime creationDate;

        public NotificationsBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
        public List<NotificationDTO> GetClientNotifications(UserNotificationDTO UserNotification)
        {
            List<Notification> ListOfNotifications = Find(x => x.RecipientFK == UserNotification.UserID
               //&& x.NotificationDirectionFK == (int)NotificationDirection.FromAgentToClient
               //&& x.NotificationMethodFK == (int)NotificationMethod.InApplication&&x.IsActive==true
               
               ).ToList();
            List<NotificationDTO> ReternedNotifications = new List<NotificationDTO>();
            foreach(var item in ListOfNotifications)
            {
                ReternedNotifications.Add(new NotificationDTO
                {   NotificationBody= item.NotificationBody,
                    NotificationHeader= item.NotificationHeader,
                    SentTime= item.SentTime,
                    RequestFK= item.RequestFK
                });
            }

            return ReternedNotifications;

        }





    }

}
