using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hr_System.Classes;
using Hr_System.Models;

namespace Hr_System.Controllers
{
    public class DailyReportController : Controller
    {
        HRSystemEntities db = new HRSystemEntities();
        string Name;
        string type;
        public ActionResult Create()
        {
            ViewBag.Date = DateTime.Now.Date.ToString("dd/MM/yyyy");
            return View();
        }
        #region publicfunction
        public ActionResult ApprovedByManager()
        {
            Name = (string)Session["UserName"];
            return View("AllReports", db.DailyReports.Where(p => p.UserName == Name).Where(p => p.RequestStatus == "ReviewedByManager").OrderByDescending(p=>p.ID));
        }
        #endregion
        #region normaluserfunctions
        public ActionResult AllMyReports()
        {
            Name = (string)Session["UserName"];
            return View("AllReports", db.DailyReports.Where(p => p.UserName == Name).OrderByDescending(p=>p.ID));
        }
        public ActionResult PendingTeamLeaderApproval()
        {
            Name = (string)Session["UserName"];
            return View("AllReports", db.DailyReports.Where(p => p.UserName == Name).Where(p => p.RequestStatus == "PendingTeamLeaderReview" || p.RequestStatus == "PendingSeniorReview").OrderByDescending(p => p.ID));

        }
        public ActionResult PendingSupervisorsApproval()
        {
            Name = (string)Session["UserName"];
            return View("AllReports", db.DailyReports.Where(p => p.UserName == Name).Where(p => p.RequestStatus == "ReviewedByTeamLeader" || p.RequestStatus == "PendingSupervisorReview" || p.RequestStatus == "PendingSupervisorBackupReview" || p.RequestStatus== "PendingDuputyManagerReview").OrderByDescending(p => p.ID));

        }
        public ActionResult RejectedByTeamLeader()
        {
            Name = (string)Session["UserName"];
            return View("AllReports", db.DailyReports.Where(p => p.UserName == Name).Where(p => p.RequestStatus == "RejectedByTeamLeader").OrderByDescending(p => p.ID));

        }
        public ActionResult RejectedBySupervisor()
        {
            Name = (string)Session["UserName"];
            return View("AllReports", db.DailyReports.Where(p => p.UserName == Name).Where(p => p.RequestStatus == "RejectedBySupervisor").OrderByDescending(p => p.ID));

        }
        public ActionResult PendingManagerApproval()
        {
            Name = (string)Session["UserName"];
            return View("AllReports", db.DailyReports.Where(p => p.UserName == Name).Where(p => p.RequestStatus == "PendingManagerReview" || p.RequestStatus== "PendingDuputyManagerReview" || p.RequestStatus== "ReviewedBySupervisor").OrderByDescending(p => p.ID));

        }
        #endregion
        #region TeamLeader & Supervisor
       public ActionResult PendingMyApproval()
       {
           Name = (string)Session["UserName"];
           return View("AllReports", db.DailyReports.Where(p => p.MyTeamLeader == Name).Where(p => p.RequestStatus == "PendingTeamLeaderReview"||p.RequestStatus== "PendingSeniorReview").OrderByDescending(p => p.ID));
       }
        public ActionResult PendingMyApprovalAsTeamLeaderForSupervisor()
        {
            Name = (string)Session["UserName"];
            return View("AllReports", db.DailyReports.Where(p => p.MySupervisor == Name).Where(p => p.RequestStatus == "PendingTeamLeaderReview" || p.RequestStatus == "PendingSeniorReview").OrderByDescending(p => p.ID));
        }
        public ActionResult PendingMyApprovalAsAsupervisor()
        {
            Name = (string)Session["UserName"];
            return View("AllReports", db.DailyReports.Where(p => p.MySupervisor == Name||p.MyManagerBackup==Name).Where(p => p.RequestStatus == "ReviewedByTeamLeader" || p.RequestStatus == "SubmittedByTeamLeaderForSupervisor" || p.RequestStatus== "SubmittedByTeamLeaderForSupervisorBackup" || p.RequestStatus== "PendingSupervisorReview"||p.RequestStatus== "PendingSupervisorBackupReview"||p.RequestStatus== "PendingDuputyManagerReview").OrderByDescending(p => p.ID));
        }
        public ActionResult PendingSupervisorApprovalByTeamLeader()
        {
            Name = (string)Session["UserName"];
            return View("AllReports", db.DailyReports.Where(p => p.UserName == Name).Where(p => p.RequestStatus == "SubmittedByTeamLeaderForSupervisor"||p.RequestStatus== "SubmittedByTeamLeaderForSupervisorBackup").OrderByDescending(p => p.ID));
        }
        public ActionResult PendingManagerApprovalByTeamLeader()
       {
           Name = (string)Session["UserName"];
           return View("AllReports", db.DailyReports.Where(p => p.UserName == Name).Where(p => p.RequestStatus == "SubmittedByTeamLeader").OrderByDescending(p => p.ID));
       }
        public ActionResult PendingManagerApprovalBySupervisor()
        {
            Name = (string)Session["UserName"];
            return View("AllReports", db.DailyReports.Where(p => p.UserName == Name).Where(p => p.RequestStatus == "PendingManagerReview"||p.RequestStatus== "PendingDuputyManagerReview").OrderByDescending(p => p.ID));
        }
        #endregion
        #region manager
        public ActionResult PendingMyApprovalasManager()
       {
           Name = (string)Session["UserName"];
           return View("AllReports", db.DailyReports.Where(p => p.MyManager == Name).Where(p => p.RequestStatus == "PendingManagerReview" || p.RequestStatus == "PendingDuputyManagerReview"||p.RequestStatus== "ReviewedBySupervisor").OrderByDescending(p => p.ID));
       }
       public ActionResult PendingTeamLeadersApproval()
       {
           Name = (string)Session["UserName"];
           return View("AllReports", db.DailyReports.Where(p => p.MyManager == Name).Where(p => p.RequestStatus == "PendingTeamLeaderReview"||p.RequestStatus== "PendingSeniorReview").OrderByDescending(p => p.ID));
       }
        public ActionResult PendingSupervisorsApprovals()
        {
            Name = (string)Session["UserName"];
            return View("AllReports", db.DailyReports.Where(p => p.MyManager == Name).Where(p => p.RequestStatus == "ReviewedByTeamLeader" || p.RequestStatus == "SubmittedByTeamLeaderForSupervisor" || p.RequestStatus == "SubmittedByTeamLeaderForSupervisorBackup" || p.RequestStatus == "PendingSupervisorReview" || p.RequestStatus == "PendingSupervisorBackupReview" || p.RequestStatus == "PendingDuputyManagerReview").OrderByDescending(p => p.ID));
        }
        public ActionResult AlreadySeenasManager()
       {
           Name = (string)Session["UserName"];
           return View("AllReports", db.DailyReports.Where(p => p.MyManager == Name).Where(p => p.RequestStatus == "ReviewedByManager").OrderByDescending(p => p.ID));
       }
       public ActionResult AllRejectedReports()
       {
           Name = (string)Session["UserName"];
           return View("AllReports", db.DailyReports.Where(p => p.MyManager == Name).Where(p => p.RequestStatus == "RejectedByTeamLeader"||p.RequestStatus== "RejectedBySupervisor").OrderByDescending(p => p.ID));
       }
       #endregion
        public ActionResult ManageRequest(int? id)
        {
            DailyReport request = db.DailyReports.Find(id);
            ViewBag.RequestOwner = request.EmpType;
            ViewBag.RequestStatus = request.RequestStatus;
            return View("ManageRequest", request);
        }
        public JsonResult AddRequestByNormalUser(DailyReport request)
        {
            Name = (string)(Session["UserName"]);
            type = (string)(Session["UserType"]);
            string msg = "";
            bool valid = checkifinsertedDailyReport(Name);
            VacationController ctr = new VacationController();
            bool alreadyholiday = ctr.checkifitsalreadyholidayForExuse(DateTime.Now.Date, DateTime.Now.Date, Name);
            bool alreadyofficialholiday = ctr.checkifofficialdayorWeekend(DateTime.Now.Date, DateTime.Now.Date);
            if (valid == true&&alreadyholiday==false&&alreadyofficialholiday==false)
            {
                var departmentName = (from item in db.accountTBs where item.EmpName == Name select item.DepartmentName).SingleOrDefault();
                var SubdepartmentName = (from item in db.accountTBs where item.EmpName == Name select item.SubDepartmentName).SingleOrDefault();
                var Supervisor = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepTeamLeader).SingleOrDefault();
                var SupervisorBackup = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepBackupSupervisor).SingleOrDefault();
                var DuputyManager = (from p in db.DepartmentTBs where p.DeptName == departmentName select p.DeptBackupManager).SingleOrDefault();
                var teamleader = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepBackupTeamLeader).SingleOrDefault();
                var manager = (from p in db.DepartmentTBs where p.DeptName == departmentName select p.DeptManager).SingleOrDefault();
                if (teamleader == null)
                 {
                    if (Supervisor==null)
                    {
                            DailyReport DR = new DailyReport();
                            DR.DailyWork = request.DailyWork;
                            DR.MyManager = manager;
                            DR.RequestStatus = "PendingManagerReview";
                            DR.EmpType = type;
                            DR.UserName = Name;
                            DR.Managment = departmentName;
                            DR.Department = SubdepartmentName;
                            DR.Day = DateTime.Now.Date;
                            db.DailyReports.Add(DR);
                            db.SaveChanges();
                            msg = "Report Added Successfully";
                   
                    }
                    else
                    {
                        if (SupervisorBackup==null)
                        {
                            DailyReport DR = new DailyReport();
                            DR.DailyWork = request.DailyWork;
                            DR.MyManager = manager;
                            DR.RequestStatus = "PendingSupervisorReview";
                            DR.EmpType = type;
                            DR.UserName = Name;
                            DR.MySupervisor = Supervisor;
                            DR.Managment = departmentName;
                            DR.Department = SubdepartmentName;
                            DR.Day = DateTime.Now.Date;
                            db.DailyReports.Add(DR);
                            db.SaveChanges();
                            msg = "Report Added Successfully";
                        }
                        //else
                        //{
                        //    DailyReport DR = new DailyReport();
                        //    DR.DailyWork = request.DailyWork;
                        //    DR.MyManager = manager;
                        //    DR.RequestStatus = "PendingSupervisorBackupReview";
                        //    DR.EmpType = type;
                        //    DR.UserName = Name;
                        //    DR.MySupervisor = SupervisorBackup;
                        //    DR.Managment = departmentName;
                        //    DR.Department = SubdepartmentName;
                        //    DR.Day = DateTime.Now.Date;
                        //    db.DailyReports.Add(DR);
                        //    db.SaveChanges();
                        //    msg = "Report Added Successfully";
                        //}
                    }
                }
                else
                {

                    DailyReport DR = new DailyReport();
                    var DepartmentSenior = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepSenior).SingleOrDefault();
                    DR.DailyWork = request.DailyWork;
                    if (DepartmentSenior==null)
                    {
                        DR.RequestStatus = "PendingTeamLeaderReview";
                        DR.MyTeamLeader = teamleader;
                    }
                    //else
                    //{
                    //    DR.RequestStatus = "PendingSeniorReview";
                    //    DR.MyTeamLeader = DepartmentSenior;
                    
                    //}
                    DR.UserName = Name;
                    DR.MyManager = manager;
                    DR.MySupervisor = Supervisor;
                    DR.Day = DateTime.Now.Date;
                    DR.EmpType = type;
                    DR.Managment = departmentName;
                    DR.Department = SubdepartmentName;
                    db.DailyReports.Add(DR);
                    db.SaveChanges();
                    msg = "Report Added Successfully";
                }
            }
            else if (valid==false)
            {
                  msg = "You Already Have Delivered Your Daily Report";
            }
            else if (alreadyholiday == true)
            {
                msg = "You Can Not Submit Report when you are on Vacation";
            }
            else if (alreadyofficialholiday == true)
            {
                msg = "You Can Not Submit Report on WeekEnd/Official Holiday";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddRequestByTeamLeader(DailyReport request)
        {
             Name = (string)(Session["UserName"]);
             type = (string)(Session["UserType"]);
            string msg = "";
            var departmentName = (from item in db.accountTBs where item.EmpName == Name select item.DepartmentName).SingleOrDefault();
            var SubdepartmentName = (from item in db.accountTBs where item.EmpName == Name select item.SubDepartmentName).SingleOrDefault();
            var manager = (from p in db.DepartmentTBs where p.DeptName == departmentName select p.DeptManager).SingleOrDefault();
            var Supervisor = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepTeamLeader).SingleOrDefault();
            var SupervisorBackup = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepBackupSupervisor).SingleOrDefault();
            var DuputyManager = (from p in db.DepartmentTBs where p.DeptName == departmentName select p.DeptBackupManager).SingleOrDefault();
            bool valid = checkifinsertedDailyReport(Name);
            VacationController ctr = new VacationController();
            bool alreadyholiday = ctr.checkifitsalreadyholidayForExuse(DateTime.Now.Date, DateTime.Now.Date, Name);
            bool alreadyofficialholiday = ctr.checkifofficialdayorWeekend(DateTime.Now.Date, DateTime.Now.Date);
            if (valid == true&&alreadyofficialholiday==false&&alreadyholiday==false)
            {
                DailyReport DR = new DailyReport();
                DR.DailyWork = request.DailyWork;
                DR.Day = DateTime.Now.Date;
                if (SupervisorBackup==null)
                {
                    DR.RequestStatus = "SubmittedByTeamLeaderForSupervisor";
                    DR.MySupervisor = Supervisor;
                }
                //else
                //{
                //    DR.RequestStatus = "SubmittedByTeamLeaderForSupervisorBackup";
                //    DR.MySupervisor = SupervisorBackup;
                //}
                DR.MyManager = manager;
                DR.UserName = Name;
                DR.EmpType = type;
                DR.Managment = departmentName;
                DR.Department = SubdepartmentName;
                db.DailyReports.Add(DR);
                db.SaveChanges();
                msg = "Report Added Successfully";
            }
            else if (valid==false)
            {
                msg = "You Already Delivered Your Daily Report ";
            }
            else if (alreadyholiday == true)
            {
                msg = "You Can Not Submit Report when you are on Vacation";
            }
            else if (alreadyofficialholiday == true)
            {
                msg = "You Can Not Submit Report on WeekEnd/Official Holiday";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddRequestBySupervisorOrDuputyManager(DailyReport request)
        {
            Name = (string)(Session["UserName"]);
            type = (string)(Session["UserType"]);
            string msg = "";
            var departmentName = (from item in db.accountTBs where item.EmpName == Name select item.DepartmentName).SingleOrDefault();
            var SubdepartmentName = (from item in db.accountTBs where item.EmpName == Name select item.SubDepartmentName).SingleOrDefault();
            var manager = (from p in db.DepartmentTBs where p.DeptName == departmentName select p.DeptManager).SingleOrDefault();
            var DuputyManager = (from p in db.DepartmentTBs where p.DeptName == departmentName select p.DeptBackupManager).SingleOrDefault();
            bool valid = checkifinsertedDailyReport(Name);
            VacationController ctr = new VacationController();
            bool alreadyholiday = ctr.checkifitsalreadyholidayForExuse(DateTime.Now.Date, DateTime.Now.Date, Name);
            bool alreadyofficialholiday = ctr.checkifofficialdayorWeekend(DateTime.Now.Date, DateTime.Now.Date);
            if (valid == true && alreadyofficialholiday == false && alreadyholiday == false)
            {
                DailyReport DR = new DailyReport();
                DR.DailyWork = request.DailyWork;
                DR.Day = DateTime.Now.Date;
                DR.RequestStatus = "PendingManagerReview";
                DR.MyManager = manager;
                DR.UserName = Name;
                DR.EmpType = type;
                DR.Managment = departmentName;
                DR.Department = SubdepartmentName;
                db.DailyReports.Add(DR);
                db.SaveChanges();
                msg = "Report Added Successfully";
            }
            else if (valid == false)
            {
                msg = "You Already Delivered Your Daily Report ";
            }
            else if (alreadyholiday == true)
            {
                msg = "You Can Not Submit Report when you are on Vacation";
            }
            else if (alreadyofficialholiday == true)
            {
                msg = "You Can Not Submit Report on WeekEnd/Official Holiday";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ApproveByTeamLeader(DailyReport request,int ID)
        {
            var msg = "";
            DailyReport req = db.DailyReports.Find(ID);
            req.TeamLeaderApproved = "true";
            req.TeamLeaderApprovalTime = DateTime.Now;
            req.RequestStatus = "ReviewedByTeamLeader";
            db.SaveChanges();
            msg = "Team Leader Approved Report";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ApproveBySupervisor(DailyReport request, int ID)
        {
            var msg = "";
            DailyReport req = db.DailyReports.Find(ID);
            req.SupervisorApproved = "true";
            req.SuppervisorApprovedOn = DateTime.Now;
            req.RequestStatus = "ReviewedBySupervisor";
            db.SaveChanges();
            msg = "Supervisor Approved Report";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RejectByTeamLeader(DailyReport request, int ID)
        {
            var msg = "";
            DailyReport req = db.DailyReports.Find(ID);
            req.TeamLeaderApproved = "false";
            req.TeamLeaderApprovalTime = DateTime.Now;
            req.RequestStatus = "RejectedByTeamLeader";
            db.SaveChanges();
            msg = "Team Leader Rejected Report";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RejectBySupervisor(DailyReport request, int ID)
        {
            var msg = "";
            DailyReport req = db.DailyReports.Find(ID);
            req.SupervisorApproved = "false";
            req.SuppervisorApprovedOn = DateTime.Now;
            req.RequestStatus = "RejectedBySupervisor";
            db.SaveChanges();
            msg = "Supervisor Rejected Report";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SeenByManager(DailyReport request, int ID)
        {
            var msg = "";
            string usertype = (string)Session["UserType"];
            DailyReport req = db.DailyReports.Find(ID);
            if (usertype=="Manager")
            {
                req.ManagerApproved = "true";
                req.ManagerApprovalTime = DateTime.Now;
                req.RequestStatus = "ReviewedByManager";
                msg = "Manager Reviewed This Day Report";

            }
            else if (usertype== "Deputy Manager")
            {
                req.MyManagerBackupApproval = "true";
                req.MyManagerBackupApprovedOn = DateTime.Now;
                req.RequestStatus = "ReviewedByManager";
                msg = "Deputy Manager Reviewed This Day Report";
            }    
            db.SaveChanges();
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public bool checkifinsertedDailyReport(string username)
        {
            Name = (string)(Session["UserName"]);
            bool validreport = true;
            var DailyReports = db.DailyReports.ToList();
            for (int i = 0; i < DailyReports.Count; i++)
            {
                if (DailyReports[i].UserName==Name)
                {
                    if (DailyReports[i].Day == DateTime.Now.Date)
                    {
                        if (DailyReports[i].RequestStatus == "RejectedByTeamLeader"|| DailyReports[i].RequestStatus == "RejectedBySupervisor")
                        {
                            validreport = true;
                        }
                        else
                        {
                            validreport = false;

                        }
                    }
                }
            }
            return validreport;
        }
    }
}