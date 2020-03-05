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
    public class DashboardController : Controller
    {
        TicketingDBEntities db = new TicketingDBEntities();
        // GET: Dashboard
        User_DAL userDAL = new User_DAL();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]

        public JsonResult countNumberOfClosedJson(int UserID)
        {
          User user = userDAL.GetUserById(UserID);
            if (user.Dept_ID!=1)
            {
                int count = db.Tickets.Where(x => (x.Status_ID == 6&&x.User_ID==UserID)).Count();
                return Json(count, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int count = db.Tickets.Where(x => x.Status_ID == 6).Count();
                return Json(count, JsonRequestBehavior.AllowGet);
            }

        }

        [Authorize]

        public JsonResult countNumberOfDataquest(int UserID)
        {
         
                int count = db.Tickets.Where(x => x.Ticket_type_ID == 1).Count();
                return Json(count, JsonRequestBehavior.AllowGet);
            

        }
        [Authorize]

        public JsonResult countNumberOfITfuson(int UserID)
        {

            int count = db.Tickets.Where(x => x.Ticket_type_ID == 2).Count();
            return Json(count, JsonRequestBehavior.AllowGet);


        }
        [Authorize]

        public JsonResult countNumberOfNewTicket(int UserID)
        {
            User user = userDAL.GetUserById(UserID);
            if (user.Dept_ID != 1)
            {
                int count = db.Tickets.Where(x => (x.Status_ID == 1 && x.User_ID == UserID)).Count();
                return Json(count, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int count = db.Tickets.Where(x => x.Status_ID == 1).Count();
                return Json(count, JsonRequestBehavior.AllowGet);
            }

        }
        [Authorize]

        public JsonResult countNumberOfpendingForConfirmation(int UserID)
        {
            User user = userDAL.GetUserById(UserID);
            if (user.Dept_ID != 1)
            {
                int count = db.Tickets.Where(x => (x.Status_ID == 5 && x.User_ID == UserID)).Count();
                return Json(count, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int count = db.Tickets.Where(x => x.Status_ID == 5).Count();
                return Json(count, JsonRequestBehavior.AllowGet);
            }

        }
        [Authorize]

        public JsonResult countNumberOfPendingforReview(int UserID)
        {
            User user = userDAL.GetUserById(UserID);
            if (user.Dept_ID != 1)
            {
                int count = db.Tickets.Where(x => (x.Status_ID == 2 && x.User_ID == UserID)).Count();
                return Json(count, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int count = db.Tickets.Where(x => x.Status_ID == 2).Count();
                return Json(count, JsonRequestBehavior.AllowGet);
            }

        }
        [Authorize]

        public JsonResult countNumberOfPendingforReviewITfusion(int UserID)
        {
            User user = userDAL.GetUserById(UserID);
            if (user.Dept_ID != 1)
            {
                int count = db.Tickets.Where(x => (x.Status_ID == 4 && x.User_ID == UserID && x.Ticket_type_ID == 2)).Count();
                return Json(count, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int count = db.Tickets.Where(x => x.Status_ID == 4 && x.Ticket_type_ID == 2).Count();
                return Json(count, JsonRequestBehavior.AllowGet);
            }

        }

        [Authorize]

        public JsonResult countNumberOfPendingforReviewDataquest(int UserID)
        {
            User user = userDAL.GetUserById(UserID);
            if (user.Dept_ID != 1)
            {
                int count = db.Tickets.Where(x => (x.Status_ID == 3 && x.User_ID == UserID && x.Ticket_type_ID == 1)).Count();
                return Json(count, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int count = db.Tickets.Where(x => x.Status_ID == 3 && x.Ticket_type_ID == 1).Count();
                return Json(count, JsonRequestBehavior.AllowGet);
            }

        }







        [Authorize]

        public JsonResult TheBestEmplyee()
        {
            List<int?> count =
              (from l in db.Tickets.Where(x => x.Assign_Person_ID != null).Select(x => x.Assign_Person_ID)
               group l by l into gr
               orderby gr.Count() descending
               select gr.Key).ToList();
            List<Dashboard> listdash = new List<Dashboard>();
            foreach (var item in count)
            {
                string username = userDAL.GetUserById(item).User_Name;
                int allcount = db.Tickets.ToList().Where(x => x.Assign_Person_ID != null).Count();
                int countassignedticket = db.Tickets.ToList().Where(x => x.Assign_Person_ID == item).Count();

                // decimal zz = count / allcount;
                Dashboard dash = new Dashboard();
                dash.prcent = Convert.ToInt16(Math.Round(((double)(countassignedticket * 100) / allcount), 0)).ToString();
                dash.username = username;
                listdash.Add(dash);
            }
            return Json(listdash, JsonRequestBehavior.AllowGet);



        }

        //public JsonResult TheBestEmplyee()
        //{
        //    List<int?> count =
        //      (from l in db.Tickets.Where(x => x.Assign_Person_ID != null).Select(x => x.Assign_Person_ID)
        //       group l by l into gr
        //       orderby gr.Count() descending
        //       select gr.Key).ToList();
        //    List<Dashboard> listdash = new List<Dashboard>();
        //    foreach (var item in count)
        //    {
        //        string username = userDAL.GetUserById(item).User_Name;
        //        int allcount = db.Tickets.ToList().Where(x => x.Assign_Person_ID != null).Count();
        //        int countassignedticket = db.Tickets.ToList().Where(x => x.Assign_Person_ID == item).Count();

        //        // decimal zz = count / allcount;
        //        Dashboard dash = new Dashboard();
        //     var value=  ( (double)countassignedticket / allcount)*10;

        //        var precent = ((int)(value)) * 10;

        //        dash.prcent =( Convert.ToInt16(precent)).ToString(); 
        //        dash.username = username;
        //        listdash.Add(dash);
        //    }
        //    return Json(listdash, JsonRequestBehavior.AllowGet);



        //}







        [Authorize]

        public JsonResult ThemostaskEmplyee()
        {



           List< int?> count =
                (from l in db.Tickets.Where(x => x.User_ID != null).Select(x => x.User_ID)
                               group l by l into gr
                 orderby gr.Count() descending
                 select gr.Key).Take(5).ToList();

            List<string> usersname=new List<string>();
            foreach (var item in count)
            {
                string username = userDAL.GetUserById(item).User_Name;
                usersname.Add(username);
            }
          
            return Json(usersname, JsonRequestBehavior.AllowGet);



        }
    
        
    }
}