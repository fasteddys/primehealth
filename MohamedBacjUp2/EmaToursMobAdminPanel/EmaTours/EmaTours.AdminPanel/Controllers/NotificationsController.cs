using EmaTours.AdminPanel.Helper;
using EmaTours.DTOs.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static EmaTours.BLL.StaticData.StaticEnums;

namespace EmaTours.AdminPanel.Controllers
{
    public class NotificationsController : BaseController
    {
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index()
        {
            return View(UnitOfWork.EMAUsersBLL.GetAll());
        }
        [HttpGet]
        public JsonResult GetNewRequests()
        {
            List<NotificationToAdminDTO> notification = new List<NotificationToAdminDTO>();

            foreach (var item in UnitOfWork.ClientTripRequestsBLL.Find(x=>x.StatusFK==(int) RequestStatus.New))
            {
                if (item != null)
                {

                    notification.Add(new NotificationToAdminDTO
                    { RequestID = item.ClientTripRequestID,
                        CreationDate = item.CreationDate.ToString(),
                        ClientName = UnitOfWork.ClientsBLL.Find(x => x.ClientID == item.ClientFK).FirstOrDefault()?.ClientName
                        ,
                        Type= "Trip"

                    });
                }
            }
            foreach (var item in UnitOfWork.ClientPickUpRequestsBLL.Find(x => x.StatusFK == (int)RequestStatus.New))
            {
                if (item != null)
                {

                    notification.Add(new NotificationToAdminDTO
                    {
                        RequestID = item.ClientPickUpRequestID,
                        CreationDate = item.CreationDate.ToString(),
                        ClientName = UnitOfWork.ClientsBLL.Find(x => x.ClientID == item.ClientFK).FirstOrDefault().ClientName
                        ,
                        Type="PickUp"

                    });
                }
            }




            return Json(notification.Select(x => new NotificationToAdminDTO
            {
                RequestID = x.RequestID,
                CreationDate= x.CreationDate
                ,  ClientName= x.ClientName,
                 Type= x.Type
            }), JsonRequestBehavior.AllowGet);
        }


    }
}