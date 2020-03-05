using EmaTours.AdminPanel.Helper;
using EmaTours.BLL.Logic.Tables;
using EmaTours.DTOs.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static EmaTours.BLL.StaticData.StaticEnums;

namespace EmaTours.AdminPanel.Controllers
{
    
    public class PickupController : BaseController
    {

        [HttpPost]

        public ActionResult index(int id, string Password)
        {
            return View();
        }
        public ActionResult ViewAllPickup()
        {

            return View(UnitOfWork.ClientPickUpRequestsBLL.GetAllPickUpRequest());
        }
        public ActionResult GetPickUpRequest(int RequestStatusID)
        {

            return View("ViewAllPickup", UnitOfWork.ClientPickUpRequestsBLL.GetPickUpRequest(RequestStatusID));
        }
        public ActionResult GetAssignedPickUpRequest()
        {
            var x = HttpContext.Request.Cookies.Get("UserID");
            var UserID = Convert.ToInt32(HttpContext.Request.Cookies.Get("UserID").Value);

            return View("ViewAllPickup", UnitOfWork.ClientPickUpRequestsBLL.GetAssignedPickUpRequest(UserID));
        }


        //public ActionResult ManagePickupRequest(int PickupRequestID)
        //{
        //    return View(UnitOfWork.ClientPickUpRequestsBLL.GetPickupRequestByID(PickupRequestID));

        //}
        
        public ActionResult ManagePickUpRequest(int PickupRequestID)
        {
            return View(UnitOfWork.ClientPickUpRequestsBLL.ManagePickUpRequest(PickupRequestID));

        }

        public ActionResult AssignRequest(int PickUpRequestID)
        {
            ClientPickUpDTO ClientPickUp = new ClientPickUpDTO();
            ClientPickUp.PickUpRequestID = PickUpRequestID;
            ClientPickUp.UserAssigneeID = Convert.ToInt32(HttpContext.Request.Cookies.Get("UserID").Value);
            UnitOfWork.ClientPickUpRequestsBLL.UserAssignPickUpRequest(ClientPickUp);
            UnitOfWork.Submit();
            return Redirect("/PickUp/ManagePickUpRequest?PickUpRequestID=" + PickUpRequestID);

        }

        public ActionResult CloseRequest(int PickUpRequestID) {
            ClientPickUpDTO ClientPickUp = new ClientPickUpDTO();
            ClientPickUp.PickUpRequestID = PickUpRequestID;
            ClientPickUp.UserAssigneeID = Convert.ToInt32(HttpContext.Request.Cookies.Get("UserID").Value);
            UnitOfWork.ClientPickUpRequestsBLL.UserClosePickUpRequest(ClientPickUp);
            UnitOfWork.Submit();

            return Redirect("/PickUp/ManagePickUpRequest?PickUpRequestID=" + PickUpRequestID);


        }
        public ActionResult AddRequestDetails(ClientPickUpDTO ClientPickUp)
        {
            ClientPickUp.UserAssigneeID = Convert.ToInt32(HttpContext.Request.Cookies.Get("UserID").Value);
          var ClientPickUpRequet=  UnitOfWork.ClientPickUpRequestsBLL.UserSetPickUpRequestTime(ClientPickUp);
            if (UnitOfWork.Submit())
            {
                NotificationDTO notification = new NotificationDTO
                {
                    IsActive = true,
                    NotificationBody = "Please Note That Pick Up Time Will Be "+ ClientPickUp.DeterminedPickupTime,
                    NotificationDirectionFK = (int)NotificationDirection.FromAgentToClient,
                    NotificationHeader = "Pick up Time",
                    NotificationMethodFK = (int)NotificationMethod.InApplication,
                    SenderFK = Convert.ToInt32(HttpContext.Request.Cookies.Get("UserID").Value),
                    RequestFK = ClientPickUpRequet.ClientPickUpRequestID,
                    RequestTypeFK = (int)ClientRequestType.PickupRequest,
                    SentTime = DateTime.Now,
                    IsReceived = false,
                    IsSent = true,
                    RecipientFK= ClientPickUpRequet.ClientFK,         
                    
                };
                UnitOfWork.NotificationsBLL.Addnotification(notification);
                UnitOfWork.Submit();
            }

            return Redirect("/PickUp/ManagePickUpRequest?PickUpRequestID=" + ClientPickUp.PickUpRequestID);


        }




    }
}