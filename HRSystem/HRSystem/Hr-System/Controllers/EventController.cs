using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Hr_System.Models;
using System;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.WebServices.Data;
using Newtonsoft.Json;

namespace Hr_System.Controllers
{
    public class EventController : Controller
    {
        //
        // GET: /Events/
        HRSystemEntities db = new HRSystemEntities();
        public ActionResult AddEvent()
        {
            string Activeuser = (string)(Session["UserName"]);
            string UserType = (string)(Session["UserType"]);
            List<accountTB> ListOfEmployees = new List<accountTB>();

            var ListOfMySupervisedSubDepartments = from p in db.SubDeps
                                                   where p.DepTeamLeader == Activeuser || p.DepBackupSupervisor == Activeuser
                                                   select p.SubDepartmentName;
            foreach (var item in ListOfMySupervisedSubDepartments)
            {
                var EmployeesInSubDep = from p in db.accountTBs
                                        where p.SubDepartmentName == item
                                        select p;
                foreach (var Employees in EmployeesInSubDep)
                {
                    ListOfEmployees.Add(Employees);
                }
            }
            ViewBag.EmployeesForSupervisor = ListOfEmployees;



            var Receiving = (from u in db.accountTBs
                             where u.SubDepartmentName == "Archiving - Receiving"
                             select u).ToList();
            var Ticketing = (from u in db.accountTBs
                             where u.SubDepartmentName == "Archiving - Ticketing"
                             select u).ToList();
            var Operations = (from u in db.accountTBs
                              where u.SubDepartmentName == "IT - Operations"
                              select u).ToList();
            var Developers = (from u in db.accountTBs
                              where u.SubDepartmentName == "IT - Developers"
                              select u).ToList();
            var Issuance = (from u in db.accountTBs
                            where u.SubDepartmentName == "Issuance"
                            select u).ToList();
            var Quality = (from u in db.accountTBs
                           where u.SubDepartmentName == "Quality Control"
                           select u).ToList();
            var TPA = (from u in db.accountTBs
                       where u.SubDepartmentName == "TPA"
                       select u).ToList();
            var TPAQuality = (from u in db.accountTBs
                              where u.SubDepartmentName == "TPA Quality"
                              select u).ToList();
            var TPAPreparation = (from u in db.accountTBs
                                  where u.SubDepartmentName == "TPA Preparation"
                                  select u).ToList();
            var Supervisors = (from u in db.accountTBs
                               where u.SubDepartmentName == "IT Development - Supervisors"
                               select u).ToList();

            ViewBag.Receiving = Receiving;
            ViewBag.Ticketing = Ticketing;
            ViewBag.Operations = Operations;
            ViewBag.Developers = Developers;
            ViewBag.Issuance = Issuance;
            ViewBag.Quality = Quality;
            ViewBag.TPA = TPA;
            ViewBag.TPAQuality = TPAQuality;
            ViewBag.TPAPreparation = TPAPreparation;
            ViewBag.Supervisors = Supervisors;
            return View();
        }
        public JsonResult createEvent(string EventName,string Description ,string EventDay,string EventFrom, string EventTo , List<int> Employees)
        {
            string Activeuser = (string)(Session["UserName"]);
            Event events = new Event();
            EmpEvent empevents = new EmpEvent();
            events.EventName = EventName;
            events.EventDescription = Description;
            events.EventDay = Convert.ToDateTime(EventDay);
            //events.EventFrom = Convert.ToDateTime(EventFrom);
            //events.EventTo = Convert.ToDateTime(EventTo);

            events.EventFrom = EventFrom;
            events.EventTo = EventTo;
            //EventFrom = DateTime.Now.ToString("hh:mm:ss");
            //events.EventFrom = DateTime.Parse(EventFrom);
            //EventTo = DateTime.Now.ToString("hh:mm:ss");
            //events.EventTo = Convert.ToDateTime(EventTo);

            //events.EventFrom = DateTime.ParseExact(EventFrom, "yyyy-MM-dd hh:mm:ss", null);
            //events.EventTo = DateTime.ParseExact(EventTo, "yyyy-MM-dd hh:mm:ss", null);


            //DateTime FromTime = DateTime.Parse(EventFrom);
            //events.EventFrom = default(DateTime).Add(FromTime.TimeOfDay);

            //DateTime ToTime = DateTime.Parse(EventTo);
            //events.EventTo = default(DateTime).Add(ToTime.TimeOfDay);
           
            
            //events.EventFrom = (DateTime.Now.ToString("hh:mm:ss"))timefrom;
            //events.EventTo = DateTime.Parse(EventTo);
            events.CreatedBy = Activeuser;
            events.CreatedAt = DateTime.Now;
            db.Events.Add(events);
            db.SaveChanges();
            foreach (var item in Employees)
            {
                empevents.EmpID = item;
                empevents.EventID = events.EventID;
                empevents.Response = "Pending";
                db.EmpEvents.Add(empevents);
                db.SaveChanges();
                var User = (from u in db.accountTBs
                              where u.ID == item
                              select u).SingleOrDefault();
                SendEventMail(User.EmpName, User.Email, Activeuser, EventDay, EventFrom, EventTo);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetEvents()
        {
            string Activeuser = (string)(Session["UserName"]);
            string UserType = (string)(Session["UserType"]);
            var UserId = (from u in db.accountTBs
                          where u.EmpName == Activeuser
                          select u.ID).SingleOrDefault();
            var employees = (from s in db.accountTBs
                             where s.EmpName != "Admin" && s.SubDepartmentName != "Deleted" && s.EmpType!="Manager"
                             select s.EmpName).ToList();
            ViewBag.employees = employees;
            if (UserType == "Manager" || UserType== "Deputy Manager")
            {
                var events = (from e in db.Events                             
                              select e).ToList();
                return View(events);
            }
            else if(UserType == "Supervisor")
            {
                var events = (from e in db.Events
                              where e.EmpEvents.Any(i => i.EmpID == UserId) || e.CreatedBy == Activeuser
                              select e).ToList();
                return View(events);
            }
            else
            {
                var events = (from e in db.Events
                              where e.EmpEvents.Any(i => i.EmpID == UserId) || e.CreatedBy== Activeuser
                              select e).ToList();
                return View(events);
            }          
        }
        public ActionResult EventDetails(int ID)
        {
            string Activeuser = (string)(Session["UserName"]);
            Event events = db.Events.Find(ID);
            var UserId = (from u in db.accountTBs
                          where u.EmpName == Activeuser
                          select u.ID).SingleOrDefault();
            var UserResponse = (from u in db.EmpEvents
                                where u.EventID == ID && u.EmpID == UserId
                                select u.Response).SingleOrDefault();
            var UsersInEvent = (from u in db.EmpEvents
                                where u.EventID == ID
                                select u).ToList();
            if (events.EmpEvents.Any(i=>i.EmpID==UserId))
            {
                ViewBag.UserInEvent = "True";
            }
            else
            {
                ViewBag.UserInEvent = "False";
            }
            ViewBag.UsersInEvent = UsersInEvent;
            ViewBag.Response = UserResponse;
            return View(events);
        }
        [HttpPost]
        public JsonResult ManageEvent(string RejectComment , int EmpEventID)
        {
            string Activeuser = (string)(Session["UserName"]);
            var User = (from u in db.accountTBs
                          where u.EmpName == Activeuser
                          select u).SingleOrDefault();
            var EmpEvent = (from e in db.EmpEvents
                            where e.EmpID == User.ID && e.EventID == EmpEventID
                            select e).SingleOrDefault();
            var Event = (from e in db.Events
                         where e.EventID == EmpEventID
                         select e).SingleOrDefault();
            var CreatorMail = (from u in db.accountTBs
                               where u.EmpName == Event.CreatedBy
                               select u.Email).SingleOrDefault();
            if (RejectComment != null)
            {

                EmpEvent.RejectComment = RejectComment;
                EmpEvent.Response = "Reject";
                EmpEvent.ResponseTime = DateTime.Now;
            }
            else 
            {
                EmpEvent.RejectComment = RejectComment;
                EmpEvent.Response = "Accept";
                EmpEvent.ResponseTime = DateTime.Now;
            }
            
            db.SaveChanges();
            SendResponseMail(User.EmpName, User.Email, Event.CreatedBy, Event.EventDay, Event.EventFrom, Event.EventTo, EmpEvent.Response , CreatorMail);
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetEventsOFEmployee(string EmpName , DateTime From , DateTime To)
        {
            //DateTime? EventFrom = DateTime.Parse(From);
            //DateTime? EventTo = DateTime.Parse(To);
            var UserId = (from u in db.accountTBs
                          where u.EmpName == EmpName
                          select u.ID).SingleOrDefault();
            var PendingEvents = (from e in db.Events
                          where e.EmpEvents.Any(i => i.EmpID == UserId) && e.EmpEvents.Any(i=>i.Response=="Pending") && e.CreatedAt>=From && e.CreatedAt<=To
                          select e).ToList();
            var AcceptEvents = (from e in db.Events
                                 where e.EmpEvents.Any(i => i.EmpID == UserId) && e.EmpEvents.Any(i => i.Response == "Accept") && e.CreatedAt >= From && e.CreatedAt <= To
                                select e).ToList();
            var RejectEvents = (from e in db.Events
                                 where e.EmpEvents.Any(i => i.EmpID == UserId) && e.EmpEvents.Any(i => i.Response == "Reject") && e.CreatedAt >= From && e.CreatedAt <= To
                                select e).ToList();
            var employees = (from s in db.accountTBs
                             where s.EmpName != "Admin" && s.SubDepartmentName != "Deleted" && s.EmpType != "Manager"
                             select s.EmpName).ToList();
            ViewBag.employees = employees;
            ViewBag.Pending = PendingEvents;
            ViewBag.PendingCount = PendingEvents.Count;
            ViewBag.Accepted = AcceptEvents;
            ViewBag.AcceptedCount = AcceptEvents.Count;
            ViewBag.Rejected = RejectEvents;
            ViewBag.RejectedCount = RejectEvents.Count;
            return View();
        }
        public void SendEventMail(string user,string usermail , string creator ,string eventday ,string From , string To)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "New Event Created";
                message.Body = "Dear  " + user + " , " + creator + " has just Created an Event that Startes on "+ eventday +" From "+From+" To "+ To +" Waiting for your response";
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(usermail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
        }
        public void SendResponseMail(string user, string usermail, string creator, DateTime? eventday, string From, string To,string response , string creatormail)
        {
            try
            {
                string date = String.Format("{0:d}", eventday);
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Event Response";
                message.Body = "Dear  " + creator + " , " + user + " has just "+ response +" Event that startes on " + date + " From " + From + " To " + To ;
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(creatormail);
                message.CcRecipients.Add(usermail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
        }

    }
}
