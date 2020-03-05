using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TasksMonitoringSystem;
using TasksMonitoringSystem.DTOs;
using TasksMonitoringSystem.SharedClass;

namespace TasksMonitoringSystem.Controllers
{
    [MyAuthorizeAttribute]
    public class UserDailsTasksController : Controller
    {
        private TaskMSEntities db = new TaskMSEntities();
        int userId = 1;
        int GCID=0;
        // GET: UserDailsTasks
        public ActionResult Index(int CID = 0)
        {
            Response.Cookies["CID"].Value = new JavaScriptSerializer().Serialize(CID);
            var todayDataTime = DateTime.Now;
            var todayData = todayDataTime.Date;
            IQueryable<UserDailsTask> userDailsTasks = null;
            ViewBag.CompanyFK = new SelectList(shared.GetAllCompanies(), "CompanyID", "CompanyName", CID);
            if (CID == 0)
                userDailsTasks = db.UserDailsTasks.Where(udt=>udt.DateOfTask== todayData).OrderBy(udt => udt.StatusFK).ThenByDescending(udt => udt.Task.PriorityFK).Include(u => u.Task).Include(u => u.TasksStatusDIM).Include(u => u.User).Include(u=>u.Task.CompanyDIM);
            else
                userDailsTasks = db.UserDailsTasks.Where(udt => udt.DateOfTask == todayData && udt.Task.CompanyFK == CID).OrderBy(udt => udt.StatusFK).ThenByDescending(udt => udt.Task.PriorityFK).Include(u => u.Task).Include(u => u.TasksStatusDIM).Include(u => u.User).Include(u => u.Task.CompanyDIM);
            return View(userDailsTasks.ToList());
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //-----------------------------------------------------------------------------------------------
        public ActionResult PendingDailyTasks(int CID = 0)
        {
            var dateTime = DateTime.Now;
            var dateNow = dateTime.Date;
            IQueryable<UserDailsTask> PendingDailsTasks = null;

            ViewBag.CompanyFK = new SelectList(shared.GetAllCompanies(), "CompanyID", "CompanyName", CID);
            if(CID == 0)
                PendingDailsTasks = db.UserDailsTasks.Where(udt => udt.DateOfTask == dateNow && udt.StatusFK==(int)StaticData.StaticEnums.TaskStatus.Pending).OrderBy(udt => udt.StatusFK).ThenByDescending(udt => udt.Task.PriorityFK).Include(u => u.Task).Include(u => u.TasksStatusDIM).Include(u => u.User).Include(u => u.Task.CompanyDIM);
            else
                PendingDailsTasks = db.UserDailsTasks.Where(udt => udt.DateOfTask == dateNow && udt.StatusFK == (int)StaticData.StaticEnums.TaskStatus.Pending && udt.Task.CompanyFK == CID).OrderBy(udt => udt.StatusFK).ThenByDescending(udt => udt.Task.PriorityFK).Include(u => u.Task).Include(u => u.TasksStatusDIM).Include(u => u.User).Include(u => u.Task.CompanyDIM);
            return View(PendingDailsTasks.ToList());
        }
        public ActionResult AssignDailyTasks(int CID = 0)
        {
            var dateTime = DateTime.Now;
            var dateNow = dateTime.Date;
            IQueryable<UserDailsTask> AssignDailsTasks = null;

            ViewBag.CompanyFK = new SelectList(shared.GetAllCompanies(), "CompanyID", "CompanyName", CID);
            if (CID == 0)
                AssignDailsTasks = db.UserDailsTasks.Where(udt => udt.DateOfTask == dateNow && udt.StatusFK == (int)StaticData.StaticEnums.TaskStatus.Assign).Include(u => u.Task).OrderBy(udt => udt.StatusFK).ThenByDescending(udt => udt.Task.PriorityFK).Include(u => u.TasksStatusDIM).Include(u => u.User).Include(u => u.Task.CompanyDIM);
            else
                AssignDailsTasks = db.UserDailsTasks.Where(udt => udt.DateOfTask == dateNow && udt.StatusFK == (int)StaticData.StaticEnums.TaskStatus.Assign && udt.Task.CompanyFK == CID).OrderBy(udt => udt.StatusFK).ThenByDescending(udt => udt.Task.PriorityFK).Include(u => u.Task).Include(u => u.TasksStatusDIM).Include(u => u.User).Include(u => u.Task.CompanyDIM);
            return View(AssignDailsTasks.ToList());
        }
        public ActionResult DoneDailyTasks(int CID = 0)
        {
            var dateTime = DateTime.Now;
            var dateNow = dateTime.Date;
            IQueryable<UserDailsTask> DoneDailsTasks = null;

            ViewBag.CompanyFK = new SelectList(shared.GetAllCompanies(), "CompanyID", "CompanyName", CID);
            if(CID == 0)
                DoneDailsTasks = db.UserDailsTasks.Where(udt => udt.DateOfTask == dateNow && udt.StatusFK == (int)StaticData.StaticEnums.TaskStatus.Completed).OrderBy(udt => udt.StatusFK).ThenByDescending(udt => udt.Task.PriorityFK).Include(u => u.Task).Include(u => u.TasksStatusDIM).Include(u => u.User).Include(u => u.Task.CompanyDIM);
            else
                DoneDailsTasks = db.UserDailsTasks.Where(udt => udt.DateOfTask == dateNow && udt.StatusFK == (int)StaticData.StaticEnums.TaskStatus.Completed && udt.Task.CompanyFK == CID).Include(u => u.Task).OrderBy(udt => udt.StatusFK).ThenByDescending(udt => udt.Task.PriorityFK).Include(u => u.TasksStatusDIM).Include(u => u.User).Include(u => u.Task.CompanyDIM);
            return View(DoneDailsTasks.ToList());
        }
        //-----------------------------------------------------------------------------------------------
        //users functionality
        public ActionResult AssignToUser(int id)
        {
            if (ModelState.IsValid)
            {
                UserDailsTask userDailsTask = db.UserDailsTasks.Find(id);
                if(userDailsTask!=null)
                {
                    userDailsTask.AssignTime = DateTime.Now;
                    userDailsTask.AssignedByFK = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(Request.Cookies["UserData"].Value).UserID;
                    userDailsTask.StatusFK =(int) StaticData.StaticEnums.TaskStatus.Assign;

                    db.Entry(userDailsTask).State = EntityState.Modified;
                    db.SaveChanges();

                }

            }
            return RedirectToAction("Index",  new{ CID= Request.Cookies["CID"].Value});
        }
        public JsonResult TaskFinished(int id,string Comment)
        {
            bool Success = false;
            if (ModelState.IsValid)
            {
                UserDailsTask userDailsTask = db.UserDailsTasks.Find(id);
                if (userDailsTask != null)
                {
                    userDailsTask.CompletedOn = DateTime.Now;
                    userDailsTask.StatusFK = (int)StaticData.StaticEnums.TaskStatus.Completed;
                    userDailsTask.ClosingComment = Comment;

                    db.Entry(userDailsTask).State = EntityState.Modified;
                    db.SaveChanges();
                    Success = true;

                }

            }
          return  Json(Success, JsonRequestBehavior.AllowGet);
            //  var x = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(Request.Cookies["UserData"].Value).UserID;


            //return RedirectToAction("myTasks", "Users",new { userId = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDTO>(Request.Cookies["UserData"].Value).UserID });



        }
        //-------------------------------------------
        public ActionResult findTaks()
        {
         
            return View();
        }
        [HttpPost]
        public JsonResult findTasksSearch(DailyTaskSearchInputDTO dailyTaskSearchInputDTO)
        {
            List<DailyTaskSearchOutputDTO> dailyTaskSearchList = new List<DailyTaskSearchOutputDTO>();
            DailyTaskSearchOutputDTO tempDailyTaskSearchOutputDTO;
            if (dailyTaskSearchInputDTO.From != null&& dailyTaskSearchInputDTO.To != null)
            {
                var from = DateTime.ParseExact(dailyTaskSearchInputDTO.From, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var to = DateTime.ParseExact(dailyTaskSearchInputDTO.To, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    
                int count = db.UserDailsTasks.Where(udt => udt.DateOfTask >= from && udt.DateOfTask <= to).ToList().Count;
            
                foreach (var dailyTask in db.UserDailsTasks.Where(udt => udt.DateOfTask >= from && udt.DateOfTask <= to))
                {
                    var task = db.Tasks.Where(t => t.TaskID == dailyTask.TaskFK).FirstOrDefault();
                    tempDailyTaskSearchOutputDTO = new DailyTaskSearchOutputDTO();
                    tempDailyTaskSearchOutputDTO.UserDailyTasksID = dailyTask.UserDailyTasksID;
                    tempDailyTaskSearchOutputDTO.CompanyName = task.CompanyDIM.CompanyName;
                    tempDailyTaskSearchOutputDTO.TaskName = task.TaskName;
                    tempDailyTaskSearchOutputDTO.TaskStatusName = dailyTask.TasksStatusDIM.TaskStatusName;
                    tempDailyTaskSearchOutputDTO.TaskPriority = task.PriorityTypeDIM.PriorityTypeName;
                    if(dailyTask.User!=null)
                    {
                        tempDailyTaskSearchOutputDTO.UserName = dailyTask.User.UserName;

                    }
                    tempDailyTaskSearchOutputDTO.DateOfTask = dailyTask.DateOfTask;
                    tempDailyTaskSearchOutputDTO.AssignTime = dailyTask.AssignTime;
                    tempDailyTaskSearchOutputDTO.CompletedOn = dailyTask.CompletedOn;
                    tempDailyTaskSearchOutputDTO.ClosingComment = dailyTask.ClosingComment;
                    dailyTaskSearchList.Add(tempDailyTaskSearchOutputDTO);
                }

            }

            return Json( dailyTaskSearchList);

        }
        [HttpGet]
        public JsonResult NumberOfPendingTasks()
        {
            var todayDataTime = DateTime.Now;
            var todayData = todayDataTime.Date;
            var userDailsTasks = db.UserDailsTasks.Where(udt => udt.DateOfTask == todayData && udt.StatusFK == (int)StaticData.StaticEnums.TaskStatus.Pending).ToList();

            int count = userDailsTasks.Count;
            return Json(count, JsonRequestBehavior.AllowGet);
        }
        //-----------------------------------------------------------------------
        [HttpGet]
        public JsonResult NumberOfDoneTasks()
        {
            var todayDataTime = DateTime.Now;
            var todayData = todayDataTime.Date;
            var userDailsTasks = db.UserDailsTasks.Where(udt => udt.DateOfTask == todayData && udt.StatusFK == (int)StaticData.StaticEnums.TaskStatus.Completed).ToList();

            int count = userDailsTasks.Count;
            return Json(count, JsonRequestBehavior.AllowGet);
        }
        //-----------------------------------------------------------------------
        [HttpGet]
        public JsonResult NumberOfAssignTasks()
        {
            var todayDataTime = DateTime.Now;
            var todayData = todayDataTime.Date;
            var userDailsTasks = db.UserDailsTasks.Where(udt => udt.DateOfTask == todayData && udt.StatusFK == (int)StaticData.StaticEnums.TaskStatus.Assign).ToList();
            int count = userDailsTasks.Count;
            return Json(count, JsonRequestBehavior.AllowGet);
        }
        //-----------------------------------------------------------------------
        public JsonResult NumberOfAllTasks()
        {
            var todayDataTime = DateTime.Now;
            var todayData = todayDataTime.Date;
            var userDailsTasks = db.UserDailsTasks.Where(udt => udt.DateOfTask == todayData).ToList();
            int count = userDailsTasks.Count;
            return Json(count, JsonRequestBehavior.AllowGet);
        }
        //-----------------------------------------------------------------------
    }
}
