using DAL.CRUD;
using DAL.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;
using TickitingSystem.Models;

namespace TickitingSystem.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        static int? ticketid;
        static int? detailsid;
        Ticket_TypeDAL ticketTypeDAL = new Ticket_TypeDAL();
        User_DAL userDAL = new User_DAL();
        TicketDAL ticketDAL = new TicketDAL();
        TicketDetail ticketdetailsDAL = new TicketDetail();
        AttachmentDAL attachmentDAL = new AttachmentDAL();
        StatusDAL statusDAL = new StatusDAL();

        // GET: Ticket
        public ActionResult OpenNewTicket()
        {
            var tTypes = ticketTypeDAL.GetAllTicket_Types();
            ViewBag.types = tTypes;
            return View();
        }
        // action used to add new ticket
        [Authorize]
        public JsonResult AddTicketJson(DAL.DataBase.Ticket ticket)
        {

            var user = userDAL.GetUserById(int.Parse(@Request.Cookies["UserIDCookie"].Value));
            ticket.User_ID = user.User_ID;
            ticket.StartDate = DateTime.Now;
            ticket.Status_ID = 1;
            ticketDAL.ADDTicket(ticket);
            ticketid = ticket.ID;
            string message = "success";
            ViewBag.message = "success";
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        // action used to add ticket detialt to the new ticket
        [Authorize]
        public JsonResult AddTicketDetailsJson(DAL.DataBase.Ticket_Details details)
        {
            details.Ticket_ID = ticketid;
            details.Date = DateTime.Now;
            var user = userDAL.GetUserById(int.Parse(@Request.Cookies["UserIDCookie"].Value));
            details.User_ID = user.User_ID;
            ticketdetailsDAL.ADDTicket_Detail(details);
            detailsid = details.TicketDetails_ID;
            string message = "success";
            ViewBag.message = "success";
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        // action used when add reply to ticket by user or by IT Dep
        [Authorize]
        public JsonResult AddTicketDetailsWithIDJson(DAL.DataBase.Ticket_Details details)
        {
            ticketid = details.Ticket_ID;
            details.Date = DateTime.Now;
            var user = userDAL.GetUserById(int.Parse(@Request.Cookies["UserIDCookie"].Value));
            details.User_ID = user.User_ID;
            ticketdetailsDAL.ADDTicket_Detail(details);
            detailsid = details.TicketDetails_ID;
            var ticket = ticketDAL.GetByTicketID(ticketid);

            if (user.Dept_ID == 1)
            {
                ticket.Status_ID = 5;
            }
            else
            {
                ticket.Status_ID = 2;
            }
            ticketDAL.EditTicket(ticket);
            string message = "success";
            ViewBag.message = "success";
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        //action used to upload attach for new ticket or a reply on ticket 
        [Authorize]
        public JsonResult AddAttachJson()
        {
            int? t = ticketid;
            int? d = detailsid;
            string strMappath = Server.MapPath( "/Attachment/" + t + "/" + d + "/")  ;
            //string strMappath = "/Attachment/" + t + "/" + d + "/";

            if (!Directory.Exists(strMappath))
            {
                DirectoryInfo di = Directory.CreateDirectory(strMappath);
            }



            for (int i = 0; i < Request.Files.Count; i++)
            {
                DAL.DataBase.Attachment a = new Attachment();
                var file = Request.Files[i];
                var fileName = Path.GetFileName(file.FileName);
                string[] FileArray = fileName.Split('.');
                string extension = FileArray[1];
                //string fname = t.ToString() + "_" + d.ToString() + "_" + i + "." + extension;
                var path1 = strMappath + fileName;
                file.SaveAs(path1);
                a.Ticket_Details_ID = d;
                a.Ticket_ID = t;
                a.Path = @"\Attachment\" + t + @"\" + d + @"\" + fileName;
                a.FileName = fileName;
                attachmentDAL.ADDAttachment(a);
            }
            try
            {
                TakeBackup();
            }
            catch (Exception ex){ }
            string message = "success";
            ViewBag.message = "success";
            var user = userDAL.GetUserById(int.Parse(@Request.Cookies["UserIDCookie"].Value));
            var ticket = ticketDAL.GetByTicketID(ticketid);
            if (ticket.Status_ID == 1)
            {
                Mailing.Send_Mail_NewTicket(user.User_ID, ticket.Ticket_type_ID, ticket.Subject, t);
            }
            if (ticket.Status_ID != 1)
            {
                Mailing.Send_Mail_ReplyTicket(user.User_ID, ticket.Ticket_type_ID, ticket.Subject, t);
            }

            ticketid = null;
            detailsid = null;
            return Json(message, JsonRequestBehavior.AllowGet);


        }
        public void TakeBackup()
        {

            int? t = ticketid;
            int? d = detailsid;
            string strMappath = WebConfigurationManager.AppSettings["BackUpLocation"] + t + "/" + d + "/";

            if (!Directory.Exists(strMappath))
            {
                DirectoryInfo di = Directory.CreateDirectory(strMappath);
            }
            for (int i = 0; i < Request.Files.Count; i++)
            {
                DAL.DataBase.Attachment a = new Attachment();
                var file = Request.Files[i];
                var fileName = Path.GetFileName(file.FileName);
                string[] FileArray = fileName.Split('.');
                string extension = FileArray[1];
                //string fname = t.ToString() + "_" + d.ToString() + "_" + i + "." + extension;
                var path1 = strMappath + fileName;
                file.SaveAs(path1);
             
            }


        }

        // view all tickets by ticket type id 
        [Authorize]
        public ActionResult ViewAllTickets(int id)
        {
            ViewBag.users = userDAL.GetAllUsers();
            ViewBag.status = statusDAL.GetAllStatuss();
            ViewBag.type = ticketTypeDAL.GetAllTicket_Types();
            var user = userDAL.GetUserById(int.Parse(@Request.Cookies["UserIDCookie"].Value));
            if (user.Dept_ID == 1)
            {
                var tickets = ticketDAL.GetAllTicketsByTypeID(id).OrderByDescending(x=>x.ID);
                return View(tickets);
            }
            else
            {
                var tickets = ticketDAL.GetAllTicketsByUserID(user.User_ID, id).OrderByDescending(x => x.ID);
                return View(tickets);
            }

        }
        //view all ticket by type id and status id 
        [Authorize]
        public ActionResult ViewTicketsbyStatus(int typeID, int StatusID)
        {
            ViewBag.users = userDAL.GetAllUsers();
            ViewBag.status = statusDAL.GetAllStatuss();
            ViewBag.type = ticketTypeDAL.GetAllTicket_Types();
            var user = userDAL.GetUserById(int.Parse(@Request.Cookies["UserIDCookie"].Value));
            if (user.Dept_ID == 1)
            {
                var tickets = ticketDAL.GetAllTicketsByTypeIDAndStatus(typeID, StatusID).OrderByDescending(x => x.ID);
                return View("ViewAllTickets", tickets);
            }
            else
            {
                var tickets = ticketDAL.GetAllTicketsByTypeIDAndStatusandUserID(user.User_ID, typeID, StatusID).OrderByDescending(x => x.ID);
                return View("ViewAllTickets", tickets);
            }
        }
        //view to IT user his assigned tickets
        [Authorize]
        public ActionResult ViewTicketsbyAssignPerson(int typeID)
        {
            ViewBag.users = userDAL.GetAllUsers();
            ViewBag.status = statusDAL.GetAllStatuss();
            ViewBag.type = ticketTypeDAL.GetAllTicket_Types();
            var user = userDAL.GetUserById(int.Parse(@Request.Cookies["UserIDCookie"].Value));

            var tickets = ticketDAL.GetAllTicketsByTypeIDAndAssignP(typeID, user.User_ID);
            return View("ViewAllTickets", tickets);
        }
        // view ticket to add reply or open ticket or to assign or to close 
        [Authorize]
        public ActionResult ManageTicket(int id)
        {
            ViewBag.users = userDAL.GetAllUsers();
            ViewBag.status = statusDAL.GetAllStatuss();
            ViewBag.type = ticketTypeDAL.GetAllTicket_Types();
            ViewBag.tID = id;


            ManageTicket manamge = new ManageTicket();
            User user = new DAL.DataBase.User();
            user= userDAL.GetUserById(int.Parse(@Request.Cookies["UserIDCookie"].Value));

            if (user.Dept_ID == 1)

            {
                manamge.ticket = ticketDAL.GetByTicketID(id);
                manamge.ticket_Details = ticketdetailsDAL.GetAllTicket_DetailsByTicketID(id);
                manamge.attachments = attachmentDAL.GetAttachmentsByTicketID(id);
                if (manamge.ticket.Assign_Person_ID == user.User_ID)
                { ticketdetailsDAL.updatelastdetails(id, int.Parse(@Request.Cookies["UserIDCookie"].Value));
                }
                return View(manamge);

            }
            else
            {
                manamge.ticket = ticketDAL.GetByTicketID(id);

                if (user.User_ID == manamge.ticket.User_ID)
                {
                    manamge.ticket_Details = ticketdetailsDAL.GetAllTicket_DetailsByTicketID(id);
                    manamge.attachments = attachmentDAL.GetAttachmentsByTicketID(id);
                    ticketdetailsDAL.updatelastdetails(id, int.Parse(@Request.Cookies["UserIDCookie"].Value));
                    return View(manamge);
                }
                else
                { return     RedirectToAction ("Index", "Error");}
            }
        }
        public ActionResult _ManageTicket(int id)
        {
            ViewBag.users = userDAL.GetAllUsers();
            ViewBag.status = statusDAL.GetAllStatuss();
            ViewBag.type = ticketTypeDAL.GetAllTicket_Types();
            ViewBag.tID = id;

            ManageTicket manamge = new ManageTicket();
            manamge.ticket = ticketDAL.GetByTicketID(id);
            manamge.ticket_Details = ticketdetailsDAL.GetAllTicket_DetailsByTicketID(id);
            manamge.attachments = attachmentDAL.GetAttachmentsByTicketID(id);
            return PartialView(manamge);
        }
        public JsonResult GetLastInsertedDetails(int UserID, int TicktID)
        {
            ticket_attachment ticket_attachment = new ticket_attachment();
            ticket_attachment.ticket = ticketdetailsDAL.GetBycustomizedExpTicketDetails(x => (x.Ticket_ID == TicktID && x.User_ID == UserID)).Last();
            ticket_attachment.attachment = attachmentDAL.GetAttachmentsByTicketDetailsID(ticket_attachment.ticket.TicketDetails_ID);
            return Json(ticket_attachment, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EditTicketStatus(DAL.DataBase.Ticket updatedticket)
        {
            string message = "";
            var gettiket = ticketDAL.GetByTicketID(updatedticket.ID);
            //assign
            if (updatedticket.Status_ID == 2)
            {
                gettiket.Assign_Person_ID = updatedticket.Assign_Person_ID;
                gettiket.Status_ID = updatedticket.Status_ID;
                ticketDAL.EditTicket(gettiket);
                ticketdetailsDAL.AddAssignEvent(updatedticket.ID, updatedticket.Assign_Person_ID);

                message = "success";
                ViewBag.message = "success";
            }
            //close
            if (updatedticket.Status_ID == 6)
            {
                gettiket.EndDate = DateTime.Now;
                gettiket.Status_ID = updatedticket.Status_ID;
                ticketDAL.EditTicket(gettiket);
                ticketdetailsDAL.AddCloseEvent(updatedticket.ID, int.Parse(@Request.Cookies["UserIDCookie"].Value));
                Mailing.Send_Mail_CloseTicket(gettiket.User_ID, gettiket.Ticket_type_ID, gettiket.Subject, gettiket.ID);
                message = "success";
                ViewBag.message = "success";
            }
            //reopen
            if (updatedticket.Status_ID == 7)
            {

                gettiket.Status_ID = updatedticket.Status_ID;
                ticketDAL.EditTicket(gettiket);
                ticketdetailsDAL.AddReOpenEvent(updatedticket.ID, int.Parse(@Request.Cookies["UserIDCookie"].Value));
                Mailing.Send_Mail_ReOpenTicket(gettiket.User_ID, gettiket.Ticket_type_ID, gettiket.Subject, gettiket.ID);
                message = "success";
                ViewBag.message = "success";
            }
            // open data quest ticket
            if (updatedticket.Status_ID == 3)
            {
                gettiket.Ticket_ID = updatedticket.Ticket_ID;
                gettiket.Status_ID = updatedticket.Status_ID;
                ticketDAL.EditTicket(gettiket);
                ticketdetailsDAL.AddOpenDQEvent(updatedticket.ID, updatedticket.Ticket_ID, int.Parse(@Request.Cookies["UserIDCookie"].Value));
                Mailing.Send_Mail_OpenOutScourseTicket(gettiket.User_ID, gettiket.Ticket_type_ID, gettiket.Subject, gettiket.ID, gettiket.Ticket_ID);
                message = "success";
                ViewBag.message = "success";
            }
            // oped itf ticket
            if (updatedticket.Status_ID == 4)
            {
                gettiket.Ticket_ID = updatedticket.Ticket_ID;
                gettiket.Status_ID = updatedticket.Status_ID;
                ticketDAL.EditTicket(gettiket);
                ticketdetailsDAL.AddOpenITFEvent(updatedticket.ID, updatedticket.Ticket_ID, int.Parse(@Request.Cookies["UserIDCookie"].Value));
                Mailing.Send_Mail_OpenOutScourseTicket(gettiket.User_ID, gettiket.Ticket_type_ID, gettiket.Subject, gettiket.ID, gettiket.Ticket_ID);
                message = "success";
                ViewBag.message = "success";
            }
            return Json(gettiket, message, JsonRequestBehavior.AllowGet);
        }
        public FileResult FileDownload(string path, string name)
        {
            //path = WebConfigurationManager.AppSettings["Location"]+ path ;

            if (!string.IsNullOrEmpty(path))
            {

             path =  Server.MapPath(path);
                //var vFullFileName = HostingEnvironment.MapPath(path);

                var file = new FileInfo( path);
                if (file.Exists)
                {
                    return File( path, "content-disposition", name);
                }
            }

            //file is empty, so return null
            return null;
        }

    }
}