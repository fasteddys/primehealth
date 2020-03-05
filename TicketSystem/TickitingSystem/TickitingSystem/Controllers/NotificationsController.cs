using DAL.CRUD;
using DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TickitingSystem.Models;

namespace TickitingSystem.Controllers
{
    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class NotificationsController : Controller
    {

        // GET: Notifications
        [Authorize]

        public JsonResult Push(int ID)
        {
            User_DAL userDAL = new User_DAL();
            TicketDAL ticketDAL = new TicketDAL();
            TicketDetail ticketdetailsDAL = new TicketDetail();
            User theloginuser = userDAL.GetUserById(ID);



            //        List<int?> list_of_IDS = new List<int?>();
            //list_of_IDS     = ticketdetailsDAL.GetAllTicket_Detail()
            //            .GroupBy(p => p.Ticket_ID).Select(g => g.First()).Where(x => x.Seen_By_IT == null)
            //            .OrderByDescending(x=>x.TicketDetails_ID).Select(x=>x.Ticket_ID)
            //            .ToList();


            List<Ticket> ticket = ticketDAL.GetBycustomizedExpTicket(x => x.Status_ID == 1);

            //  foreach (int? value in list_of_IDS)
            //// {
            //     Ticket tic = ticketDAL.GetByTicketID(value);
            //     ticket.Add(tic);


            // }
            //   List<User> listofuser = new List<DAL.DataBase.User>();
            List<notificationobject> notification = new List<notificationobject>();

            foreach (var item in ticket)
            {
                if (item != null)
                {
                    notificationobject notificat = new notificationobject();
                    int i = Convert.ToInt16(item.User_ID);
                    User user = userDAL.GetUserById(i);
                    notificat.Ticketnumber = item.ID.ToString();
                    notificat.TicketDate = item.StartDate.ToString();

                    notificat.username = user.User_Name.ToString();

                    notification.Add(notificat);
                }
            }

            return Json(notification.Select(x => new notificationobject
            {
                username = x.username,
                Ticketnumber = x.Ticketnumber
         ,
                TicketDate = x.TicketDate
                // Assigment of child fields
            }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Getmessages(int ID)
        {
            User_DAL userDAL = new User_DAL();
            TicketDAL ticketDAL = new TicketDAL();
            TicketDetail ticketdetailsDAL = new TicketDetail();
            User theloginuser = userDAL.GetUserById(ID);
            List<notificationobject> listmessage = new List<notificationobject>();
            List<Ticket_Details> ticketdetalslist = new List<Ticket_Details>();
            if (theloginuser.Dept_ID == 1)
            {
                TicketingDBEntities Db = new TicketingDBEntities();


                var ticketdetalslisttoIT =



                 ( //x.Seen_By_User == null&&x.User_ID!= theloginuser.User_ID
                 from post in Db.Ticket_Details
                 join meta in Db.Tickets on post.Ticket_ID equals meta.ID
                 where
                 post.User_ID != theloginuser.User_ID &&
                 post.Seen_By_IT == null &&
                 meta.Assign_Person_ID == theloginuser.User_ID
                 select new { TicketDate = post.Date, User_ID = post.User_ID, Ticket_ID = post.Ticket_ID }).ToList();


                foreach (var item in ticketdetalslisttoIT)
                {
                    notificationobject message = new notificationobject();
                    message.TicketDate = item.TicketDate.ToString();
                    message.Ticketnumber = item.Ticket_ID.ToString();
                    message.username = userDAL.GetUserById(item.User_ID).User_Name;
                    listmessage.Add(message);


                }
            }
            else
            {
                TicketingDBEntities Db = new TicketingDBEntities();

                var detalslist =

                  ( //x.Seen_By_User == null&&x.User_ID!= theloginuser.User_ID
                  from post in Db.Ticket_Details
                  join meta in Db.Tickets on post.Ticket_ID equals meta.ID
                  where post.User_ID != theloginuser.User_ID && post.Seen_By_User == null && meta.User_ID == theloginuser.User_ID
                  select new { TicketDate = post.Date, User_ID = post.User_ID, Ticket_ID = post.Ticket_ID }).ToList();





                foreach (var item in detalslist)
                {
                    notificationobject message = new notificationobject();
                    message.TicketDate = item.TicketDate.ToString();
                    message.Ticketnumber = item.Ticket_ID.ToString();
                    message.username = userDAL.GetUserById(item.User_ID).User_Name;
                    listmessage.Add(message);


                }
            }
            return Json(listmessage.Select(x => new notificationobject
            {
                username = x.username,
                Ticketnumber = x.Ticketnumber,
                TicketDate = x.TicketDate
            }), JsonRequestBehavior.AllowGet);
        }
    }
}