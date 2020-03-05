using EmaTours.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmaTours.Entities;
using EmaTours.DTOs.Business;

namespace EmaTours.BLL.Logic.Tables
{
    public class NotificationsBLL : Repository<Notification>
    {
        DateTime creationDate;

        public NotificationsBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
    
        public void Addnotification(NotificationDTO Notification)
        {
            Add(new Entities.Notification
            {
                CreationDate = creationDate,
                DeviceID = Notification.DeviceID,
                IsActive = Notification.IsActive,
                IsReceived = Notification.IsReceived,
                IsSent = Notification.IsSent,
                IsViewed = Notification.IsViewed,
                LanguageFK = Notification.LanguageFK,
                NotificationBody = Notification.NotificationBody,
                NotificationDirectionFK = Notification.NotificationDirectionFK,
                NotificationLink = Notification.NotificationLink,
                NotificationMethodFK = Notification.NotificationMethodFK,
                ReceivedTime = Notification.ReceivedTime,
                RecipientFK = Notification.RecipientFK,
                RequestFK = Notification.RequestFK,
                RequestTypeFK = Notification.RequestTypeFK,
                  VisitFK= Notification.VisitID,
                SenderFK = Notification.SenderFK,
                SentTime = Notification.SentTime,
                ViewedPersonFK = Notification.ViewedPersonFK,
                ViewedTime = Notification.ViewedTime,
                NotificationHeader = Notification.NotificationHeader
            });


        }



    }

}
