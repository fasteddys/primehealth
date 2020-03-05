using Hr_System.Classes;
using Hr_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Exchange.WebServices.Data;

namespace Hr_System.Controllers
{
    public class ManagmentController : Controller
    {
        DateTime ReqDate = DateTime.Now;
        string type;
        string Name;
        string Role;
        string Department;
        string SubDepartment;
        string ArabicName;
        HRSystemEntities hr = new HRSystemEntities();
        Helpers obj = new Helpers();
        public ActionResult getAllExcusesRequestsForManagment()
        {
            Name = (string)(Session["UserName"]);
            type = (string)(Session["UserType"]);
            //NEW
            Role = (from T in hr.accountTBs
                    where T.EmpName == Name
                    select T.SupervisorRole).First();
            ViewBag.AutRole = Role;
            //END
            var data = obj.getTeamLeaderPendinExcusesRequests(Name);
            if (type == "TeamLeader")
            {
                data = obj.getTeamLeaderPendinExcusesRequests(Name);

            }
            if (type == "Supervisor" && Role != "yes")
            {
                data = obj.getSupervisorExcusesRequests(Name);

            }
            //NEW
            if (type == "Supervisor" && Role == "yes")
            {
                data = obj.GetBackupSupervisorBackupRequests(Name);
            }
            //END
            else if (type == "Deputy Manager")
            {
                data = obj.getDuputyManagerExcusesRequests(Name);

            }
            else if (type == "Manager")
            {
                data = obj.getManagerPendinExcusesRequests(Name);

            }
            return View(data);
        }
        [HttpGet]
        public ActionResult GetAllExcusesPendingTeamLeadersForManager()
        {
            Name = (string)Session["UserName"];
            var departmentName = (from item in hr.accountTBs where item.EmpName == Name select item.DepartmentName).SingleOrDefault();
            var manager = (from p in hr.DepartmentTBs where p.DeptName == departmentName select p.DeptManager).SingleOrDefault();
            var data = obj.getTeamLeaderPendinExcusesRequestsForManager(manager);
            return View(data);

        }
        public ActionResult GetAllExcusesPendingSupervisorsForManager()
        {
            Name = (string)Session["UserName"];
            var departmentName = (from item in hr.accountTBs where item.EmpName == Name select item.DepartmentName).SingleOrDefault();
            var manager = (from p in hr.DepartmentTBs where p.DeptName == departmentName select p.DeptManager).SingleOrDefault();
            var data = obj.getSupervisorsPendinExcusesRequestsForManager(manager);


            return View(data);

        }
        public ActionResult GetAllExcusesPendingTeamLeadersForSupervisor()
        {
            Name = (string)Session["UserName"];
            var data = obj.getTeamLeaderPendinExcusesRequestsForSupervisor(Name);
            return View(data);

        }
        public ActionResult GetAllRejectionsByTeamLeadersForManager()
        {
            Name = (string)Session["UserName"];
            var departmentName = (from item in hr.accountTBs where item.EmpName == Name select item.DepartmentName).SingleOrDefault();
            var manager = (from p in hr.DepartmentTBs where p.DeptName == departmentName select p.DeptManager).SingleOrDefault();
            var data = obj.getRejectedExcusesByTeamLeadersForManager(manager);
            return View(data);

        }
        public ActionResult GetAllRejectionsBySupervisorsForManager()
        {
            Name = (string)Session["UserName"];
            var departmentName = (from item in hr.accountTBs where item.EmpName == Name select item.DepartmentName).SingleOrDefault();
            var manager = (from p in hr.DepartmentTBs where p.DeptName == departmentName select p.DeptManager).SingleOrDefault();
            var data = obj.getRejectedExcusesBySupervisorsForManager(manager);
            return View(data);

        }
        public ActionResult requestDetails(int ID)
        {

            Request req = hr.Requests.Find(ID);
            ViewBag.ID = req.ID;
            ViewBag.Manager = req.MyManager;
            ViewBag.TeamLeader = req.MyTeamLeader;
            ViewBag.Supervisor = req.MySupervisor;
            ViewBag.DuputyManager = req.MyBackupManager;
            ViewBag.UserName = req.UserName;
            ViewBag.Managment = req.ManagmentName;
            ViewBag.Department = req.DepartmentName;
            //ViewBag.RemainingExcuses = obj.getRemainingExcusesForUser(req.UserName);
            ViewBag.RemainingExcuseHours = obj.GetRemainingExcuseHours(req.UserName);

            return View(req);

        }







        [HttpPost]
        public ActionResult requestDetails(int ID, string ReqStatus, string TeamLeaderApprove, string SupervisorApprove, string ManagerApprove, string status)
        {
            Name = (string)Session["UserName"];
            type = (string)(Session["UserType"]);
            Department = (string)(Session["Dep"]);
            ArabicName = (string)(Session["ArabicName"]);
            string message = "";
            string result = "";
            var Emploee = "";
            var SubDep = "";
            var Dep = "";
            Request req = hr.Requests.Find(ID);
            Emploee = req.UserName;
            SubDep = req.DepartmentName;
            Dep = req.ManagmentName;
            string RequestPresonName = Emploee;
            SubDepartment = (from item in hr.accountTBs where item.EmpName == RequestPresonName select item.SubDepartmentName).SingleOrDefault();
            string ExcuseDay = req.ExcuseDay;
            string DurationFrom = req.ExcuseDurationFrom;
            string DurationTo = req.ExcuseDurationTo;
            string MyManager = obj.getDepManagerName(Department);
            string MyTeamLeader = obj.getSubDepTeamLeaderName(SubDepartment);
            string MySupervisor = obj.getSubDepSupervisor(SubDepartment);
            string MyDepSenior = obj.getSubDepSenior(SubDepartment);
            string MyDepSupervisorBackup = obj.getSubDepBackupSupervisor(SubDepartment);
            string MyDuputyManager = obj.getDepDuputyManager(Department);
            var UserMail = (from item in hr.accountTBs where item.EmpName == RequestPresonName select item.Email).SingleOrDefault();
            var SeniorEmail = (from item in hr.accountTBs where item.EmpName == MyDepSenior select item.Email).SingleOrDefault();
            var TeamLeaderMail = (from item in hr.accountTBs where item.EmpName == MyTeamLeader select item.Email).SingleOrDefault();
            var SupervisorMail = (from item in hr.accountTBs where item.EmpName == MySupervisor select item.Email).SingleOrDefault();
            var BackupSupervisorMail = (from item in hr.accountTBs where item.EmpName == MyDepSupervisorBackup select item.Email).SingleOrDefault();
            var DuputyManagerMail = (from item in hr.accountTBs where item.EmpName == MyDuputyManager select item.Email).SingleOrDefault();
            var ManagerMail = (from item in hr.accountTBs where item.EmpName == Name select item.Email).SingleOrDefault();
            req.ReqStatus = ReqStatus;
            if (ModelState.IsValid)
            {

                if (type == "TeamLeader")
                {
                    req.TeamLeadApproveDate = ReqDate;
                    req.TeamLeaderApprove = TeamLeaderApprove;
                    req.ActionTaken = "yes";
                    req.ApprovedByTeamLeader = (string)(Session["UserName"]);
                    if (ReqStatus == "PendingSupervisorApproval")
                    {

                        result = ApproveDayByDayForTeamLeader(ID);
                        message = "Successfully Saved!";
                        hr.SaveChanges();
                        SendMail_ExcuseApproved_ByTeamLeader(RequestPresonName, UserMail, MyTeamLeader, TeamLeaderMail, SupervisorMail, ExcuseDay, DurationFrom, DurationTo);
                    }
                    else if (ReqStatus == "Approved")
                    {
                        result = RejectDayByDayForTeamLeader(ID);
                        message = "Successfully Saved!";
                        hr.SaveChanges();
                        /////////////////////////////////////////////////////////////////////////
                        decimal? TOtalExcuseHoursRemaining;

                        TOtalExcuseHoursRemaining = (from Hour in hr.accountTBs
                                                     where Hour.EmpName == req.UserName
                                                     select Hour.NumberOfExcuseHours).First();

                        decimal? ExcuseSpecificDuration = (from Dur in hr.Requests
                                                           where Dur.ID == ID
                                                           select Dur.ExcuseSpecificDuration).First();

                        string excuseduration = req.ExcuseDuration;

                        if (TOtalExcuseHoursRemaining < ExcuseSpecificDuration)
                        {
                            message = "You requested an excuse with longer duration than what you have";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        if (TOtalExcuseHoursRemaining <= 0)
                        {
                            message = "There is No Remaining Excuse hours!";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                        //  else
                        //{
                        int ReqDay = Convert.ToDateTime(req.ExcuseDay).Day;
                        int ReqMonth = Convert.ToDateTime(req.ExcuseDay).Month;
                        int ApproveDay = Convert.ToDateTime(req.MyBackupManagerApprovedOn).Day;
                        int ApproveMonth = Convert.ToDateTime(req.MyBackupManagerApprovedOn).Month;

                        decimal? exc = TOtalExcuseHoursRemaining + ExcuseSpecificDuration;
                        //if ((ReqDay<21 && ApproveDay>=21)||(ApproveMonth>ReqMonth))
                        //{

                        //    exc = totalexcuses + 1;
                        //}
                        int id = (from f in hr.accountTBs
                                  where f.EmpName == req.UserName
                                  select f.ID).First();
                        accountTB acc = hr.accountTBs.Find(id);
                        acc.NumberOfExcuseHours = exc;
                        hr.SaveChanges();
                        //   message = "Successfully Saved!";
                        // }
                        ////////////////////////////////////////////////////////////////////////////
                        SendMail_ExcuseRejected_ByTeamLeader(RequestPresonName, UserMail, MyTeamLeader, TeamLeaderMail, SupervisorMail, ExcuseDay, DurationFrom, DurationTo);

                    }
                }
                else if (type == "Supervisor")
                {
                    if (ReqStatus == "Approved")
                    {
                        req.SupervisorApprovedOn = ReqDate;
                        req.SupervisorApprove = SupervisorApprove;
                        req.ApprovedBySupervisor = (string)(Session["UserName"]);
                        req.ActionTaken = "yes";
                        result = ApproveDayByDayForSupervisor(ID);
                        message = "Successfully Saved!";
                        hr.SaveChanges();
                        SendMail_ExcuseApproved_BySupervisor(RequestPresonName, UserMail, MySupervisor, DuputyManagerMail, TeamLeaderMail, ManagerMail, SupervisorMail, ExcuseDay, DurationFrom, DurationTo);

                    }
                    else if (ReqStatus == "RejectedBySupervisor")
                    {
                        req.SupervisorApprovedOn = ReqDate;
                        req.SupervisorApprove = SupervisorApprove;
                        req.ApprovedBySupervisor = (string)(Session["UserName"]);
                        req.ActionTaken = "yes";
                        result = RejectDayByDayForSupervisor(ID);
                        message = "Successfully Saved!";
                        hr.SaveChanges();
                        /////////////////////////////////////////////////////////////////////////
                        decimal? TOtalExcuseHoursRemaining;

                        TOtalExcuseHoursRemaining = (from Hour in hr.accountTBs
                                                     where Hour.EmpName == req.UserName
                                                     select Hour.NumberOfExcuseHours).First();

                        decimal? ExcuseSpecificDuration = (from Dur in hr.Requests
                                                           where Dur.ID == ID
                                                           select Dur.ExcuseSpecificDuration).First();

                        string excuseduration = req.ExcuseDuration;

                        if (TOtalExcuseHoursRemaining < ExcuseSpecificDuration)
                        {
                            message = "You requested an excuse with longer duration than what you have";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        if (TOtalExcuseHoursRemaining <= 0)
                        {
                            message = "There is No Remaining Excuse hours!";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                        //  else
                        //{
                        int ReqDay = Convert.ToDateTime(req.ExcuseDay).Day;
                        int ReqMonth = Convert.ToDateTime(req.ExcuseDay).Month;
                        int ApproveDay = Convert.ToDateTime(req.MyBackupManagerApprovedOn).Day;
                        int ApproveMonth = Convert.ToDateTime(req.MyBackupManagerApprovedOn).Month;

                        decimal? exc = TOtalExcuseHoursRemaining + ExcuseSpecificDuration;
                        //if ((ReqDay<21 && ApproveDay>=21)||(ApproveMonth>ReqMonth))
                        //{

                        //    exc = totalexcuses + 1;
                        //}
                        int id = (from f in hr.accountTBs
                                  where f.EmpName == req.UserName
                                  select f.ID).First();
                        accountTB acc = hr.accountTBs.Find(id);
                        acc.NumberOfExcuseHours = exc;
                        hr.SaveChanges();
                        //   message = "Successfully Saved!";
                        // }
                        ////////////////////////////////////////////////////////////////////////////
                        SendMail_ExcuseRejected_BySupervisor(RequestPresonName, UserMail, MySupervisor, TeamLeaderMail, SupervisorMail, ExcuseDay, DurationFrom, DurationTo);
                    }
                    else if (ReqStatus == "PendingDuputyManagerApproval")
                    {
                        req.SupervisorApprovedOn = ReqDate;
                        req.SupervisorApprove = SupervisorApprove;
                        req.ApprovedBySupervisor = (string)(Session["UserName"]);
                        req.ActionTaken = "yes";
                        result = ApproveDayByDayForSupervisor(ID);
                        message = "Successfully Saved!";
                        hr.SaveChanges();
                        SendMail_ExcuseApproved_BySupervisor(RequestPresonName, UserMail, MySupervisor, DuputyManagerMail, TeamLeaderMail, ManagerMail, SupervisorMail, ExcuseDay, DurationFrom, DurationTo);
                    }
                }
                else if (type == "Deputy Manager")
                {
                    if (ReqStatus == "PendingManagerApproval")
                    {
                        req.SupervisorApprovedOn = ReqDate;
                        req.SupervisorApprove = SupervisorApprove;
                        req.ApprovedBySupervisor = (string)(Session["UserName"]);
                        req.ActionTaken = "yes";
                        result = ApproveDayByDayForSupervisor(ID);
                        message = "Successfully Saved!";
                        hr.SaveChanges();
                        SendMail_ExcuseApproved_BySupervisor(RequestPresonName, UserMail, MySupervisor, DuputyManagerMail, TeamLeaderMail, ManagerMail, SupervisorMail, ExcuseDay, DurationFrom, DurationTo);

                    }
                    else if (ReqStatus == "RejectedBySupervisor")
                    {
                        req.SupervisorApprovedOn = ReqDate;
                        req.SupervisorApprove = SupervisorApprove;
                        req.ApprovedBySupervisor = (string)(Session["UserName"]);
                        req.ActionTaken = "yes";
                        result = RejectDayByDayForSupervisor(ID);
                        message = "Successfully Saved!";
                        hr.SaveChanges();
                        /////////////////////////////////////////////////////////////////////////
                        decimal? TOtalExcuseHoursRemaining;

                        TOtalExcuseHoursRemaining = (from Hour in hr.accountTBs
                                                     where Hour.EmpName == req.UserName
                                                     select Hour.NumberOfExcuseHours).First();

                        decimal? ExcuseSpecificDuration = (from Dur in hr.Requests
                                                           where Dur.ID == ID
                                                           select Dur.ExcuseSpecificDuration).First();

                        string excuseduration = req.ExcuseDuration;

                        if (TOtalExcuseHoursRemaining < ExcuseSpecificDuration)
                        {
                            message = "You requested an excuse with longer duration than what you have";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        if (TOtalExcuseHoursRemaining <= 0)
                        {
                            message = "There is No Remaining Excuse hours!";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                        //  else
                        //{
                        int ReqDay = Convert.ToDateTime(req.ExcuseDay).Day;
                        int ReqMonth = Convert.ToDateTime(req.ExcuseDay).Month;
                        int ApproveDay = Convert.ToDateTime(req.MyBackupManagerApprovedOn).Day;
                        int ApproveMonth = Convert.ToDateTime(req.MyBackupManagerApprovedOn).Month;

                        decimal? exc = TOtalExcuseHoursRemaining + ExcuseSpecificDuration;
                        //if ((ReqDay<21 && ApproveDay>=21)||(ApproveMonth>ReqMonth))
                        //{

                        //    exc = totalexcuses + 1;
                        //}
                        int id = (from f in hr.accountTBs
                                  where f.EmpName == req.UserName
                                  select f.ID).First();
                        accountTB acc = hr.accountTBs.Find(id);
                        acc.NumberOfExcuseHours = exc;
                        hr.SaveChanges();
                        //   message = "Successfully Saved!";
                        // }
                        ////////////////////////////////////////////////////////////////////////////
                        SendMail_ExcuseRejected_BySupervisor(RequestPresonName, UserMail, MySupervisor, TeamLeaderMail, SupervisorMail, ExcuseDay, DurationFrom, DurationTo);

                    }
                    else if (ReqStatus == "PendingDuputyManagerApproval")
                    {
                        req.SupervisorApprovedOn = ReqDate;
                        req.SupervisorApprove = SupervisorApprove;
                        req.ApprovedBySupervisor = (string)(Session["UserName"]);
                        req.ActionTaken = "yes";
                        result = ApproveDayByDayForSupervisor(ID);
                        message = "Successfully Saved!";
                        hr.SaveChanges();
                        SendMail_ExcuseApproved_BySupervisor(RequestPresonName, UserMail, MySupervisor, DuputyManagerMail, TeamLeaderMail, ManagerMail, SupervisorMail, ExcuseDay, DurationFrom, DurationTo);

                    }
                    else if (status == "Approved")
                    {
                        req.MyBackupManagerApproval = "Approved";
                        req.MyBackupManagerApprovedOn = ReqDate;
                        result = ApproveDayByDayForManager(ID);
                        hr.SaveChanges();

                    }
                    else if (ReqStatus == "Rejected")
                    {
                        req.MyBackupManagerApproval = "Rejected";
                        req.MyBackupManagerApprovedOn = ReqDate;
                        result = RejectDayByDayForManager(ID);
                        hr.SaveChanges();
                        /////////////////////////////////////////////////////////////////////////
                        decimal? TOtalExcuseHoursRemaining;

                        TOtalExcuseHoursRemaining = (from Hour in hr.accountTBs
                                                     where Hour.EmpName == req.UserName
                                                     select Hour.NumberOfExcuseHours).First();

                        decimal? ExcuseSpecificDuration = (from Dur in hr.Requests
                                                           where Dur.ID == ID
                                                           select Dur.ExcuseSpecificDuration).First();

                        string excuseduration = req.ExcuseDuration;

                        if (TOtalExcuseHoursRemaining < ExcuseSpecificDuration)
                        {
                            message = "You requested an excuse with longer duration than what you have";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        if (TOtalExcuseHoursRemaining <= 0)
                        {
                            message = "There is No Remaining Excuse hours!";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                        //  else
                        //{
                        int ReqDay = Convert.ToDateTime(req.ExcuseDay).Day;
                        int ReqMonth = Convert.ToDateTime(req.ExcuseDay).Month;
                        int ApproveDay = Convert.ToDateTime(req.MyBackupManagerApprovedOn).Day;
                        int ApproveMonth = Convert.ToDateTime(req.MyBackupManagerApprovedOn).Month;

                        decimal? exc = TOtalExcuseHoursRemaining + ExcuseSpecificDuration;
                        //if ((ReqDay<21 && ApproveDay>=21)||(ApproveMonth>ReqMonth))
                        //{

                        //    exc = totalexcuses + 1;
                        //}
                        int id = (from f in hr.accountTBs
                                  where f.EmpName == req.UserName
                                  select f.ID).First();
                        accountTB acc = hr.accountTBs.Find(id);
                        acc.NumberOfExcuseHours = exc;
                        hr.SaveChanges();
                        //   message = "Successfully Saved!";
                        // }
                        ////////////////////////////////////////////////////////////////////////////

                    }
                }
                else if (type == "Manager")
                {
                    req.ManagerApprove = ManagerApprove;
                    req.ManagerApproveDate = ReqDate;
                    req.ActionTaken = "yes";
                    if (ReqStatus == "Approved")
                    {
                        result = ApproveDayByDayForManager(ID);
                        hr.SaveChanges();
                        SendMail_ExcuseApproved_ByManager(RequestPresonName, UserMail, Name, ManagerMail, ExcuseDay, DurationFrom, DurationTo);

                    }
                    else if (ReqStatus == "Rejected")
                    {
                        result = RejectDayByDayForManager(ID);
                        hr.SaveChanges();
                        /////////////////////////////////////////////////////////////////////////
                        decimal? TOtalExcuseHoursRemaining;

                        TOtalExcuseHoursRemaining = (from Hour in hr.accountTBs
                                                     where Hour.EmpName == req.UserName
                                                     select Hour.NumberOfExcuseHours).First();

                        decimal? ExcuseSpecificDuration = (from Dur in hr.Requests
                                                           where Dur.ID == ID
                                                           select Dur.ExcuseSpecificDuration).First();

                        string excuseduration = req.ExcuseDuration;

                        if (TOtalExcuseHoursRemaining < ExcuseSpecificDuration)
                        {
                            message = "You requested an excuse with longer duration than what you have";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        if (TOtalExcuseHoursRemaining <= 0)
                        {
                            message = "There is No Remaining Excuse hours!";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                        //  else
                        //{
                        int ReqDay = Convert.ToDateTime(req.ExcuseDay).Day;
                        int ReqMonth = Convert.ToDateTime(req.ExcuseDay).Month;
                        int ApproveDay = Convert.ToDateTime(req.MyBackupManagerApprovedOn).Day;
                        int ApproveMonth = Convert.ToDateTime(req.MyBackupManagerApprovedOn).Month;

                        decimal? exc = TOtalExcuseHoursRemaining + ExcuseSpecificDuration;
                        //if ((ReqDay<21 && ApproveDay>=21)||(ApproveMonth>ReqMonth))
                        //{

                        //    exc = totalexcuses + 1;
                        //}
                        int id = (from f in hr.accountTBs
                                  where f.EmpName == req.UserName
                                  select f.ID).First();
                        accountTB acc = hr.accountTBs.Find(id);
                        acc.NumberOfExcuseHours = exc;
                        hr.SaveChanges();
                        //   message = "Successfully Saved!";
                        // }
                        ////////////////////////////////////////////////////////////////////////////
                        SendMail_ExcuseRejected_ByManager(RequestPresonName, UserMail, Name, ManagerMail, ExcuseDay, DurationFrom, DurationTo);
                    }

                }
                else
                {

                }
                if (ReqStatus == "Approved")
                {
                    ////new
                    //decimal? TOtalExcuseHoursRemaining;

                    //TOtalExcuseHoursRemaining = (from Hour in hr.accountTBs
                    //                             where Hour.EmpName == Emploee
                    //                             select Hour.NumberOfExcuseHours).First();

                    //decimal? ExcuseSpecificDuration = (from Dur in hr.Requests
                    //                                   where Dur.ID == ID
                    //                                   select Dur.ExcuseSpecificDuration).First();

                    //string excuseduration = req.ExcuseDuration;

                    //if (TOtalExcuseHoursRemaining < ExcuseSpecificDuration)
                    //{
                    //    message = "You requested an excuse with longer duration than what you have";
                    //    return Json(message, JsonRequestBehavior.AllowGet);
                    //}
                    //if (TOtalExcuseHoursRemaining <= 0)
                    //{
                    //    message = "There is No Remaining Excuse hours!";
                    //    return Json(message, JsonRequestBehavior.AllowGet);
                    //}

                    ////  else
                    ////{
                    //int ReqDay = Convert.ToDateTime(req.ExcuseDay).Day;
                    //int ReqMonth = Convert.ToDateTime(req.ExcuseDay).Month;
                    //int ApproveDay = Convert.ToDateTime(req.MyBackupManagerApprovedOn).Day;
                    //int ApproveMonth = Convert.ToDateTime(req.MyBackupManagerApprovedOn).Month;

                    //decimal? exc = TOtalExcuseHoursRemaining - ExcuseSpecificDuration;
                    ////if ((ReqDay<21 && ApproveDay>=21)||(ApproveMonth>ReqMonth))
                    ////{

                    ////    exc = totalexcuses + 1;
                    ////}
                    //int id = (from f in hr.accountTBs
                    //          where f.EmpName == Emploee
                    //          select f.ID).First();
                    //accountTB acc = hr.accountTBs.Find(id);
                    //acc.NumberOfExcuseHours = exc;
                    //hr.SaveChanges();
                    //message = "Successfully Saved!";
                    //// }


                }
                else
                {
                    hr.SaveChanges();
                    message = "Successfully Saved!";
                }
            }
            else
            {
                message = "Error has been occured please contact your Developer!";
            }
            if (Request.IsAjaxRequest())
            {
                return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            return RedirectToAction("");
        }













        public JsonResult getRequestStatus(int ID)
        {
            string data = obj.getRequeststatus(ID);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public string ApproveDayByDayForTeamLeader(int reqID)
        {
            var requests = (from p in hr.RequestHandlers
                            where p.OriginalRequestID == reqID
                            select p
                        ).ToList();
            try
            {
                foreach (var req in requests)
                {
                    req.ReqStatus = "PendingSupervisorApproval";
                }
                hr.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {

                return "Error";
            }

        }
        public string RejectDayByDayForTeamLeader(int reqID)
        {
            var requests = (from p in hr.RequestHandlers
                            where p.OriginalRequestID == reqID
                            select p
                       ).ToList();
            try
            {
                foreach (var req in requests)
                {
                    req.ReqStatus = "RejectedByTeamLeader";
                }
                hr.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {

                return "Error";
            }
        }
        public string ApproveDayByDayForSupervisor(int reqID)
        {
            var requests = (from p in hr.RequestHandlers
                            where p.OriginalRequestID == reqID
                            select p
                        ).ToList();
            try
            {
                foreach (var req in requests)
                {
                    req.ReqStatus = "PendingDuputyManagerApproval";
                }
                hr.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {

                return "Error";
            }

        }
        public string RejectDayByDayForSupervisor(int reqID)
        {
            var requests = (from p in hr.RequestHandlers
                            where p.OriginalRequestID == reqID
                            select p
                       ).ToList();
            try
            {
                foreach (var req in requests)
                {
                    req.ReqStatus = "RejectedBySupervisor";
                }
                hr.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {

                return "Error";
            }
        }
        public string ApproveDayByDayForManager(int reqID)
        {
            var requests = (from p in hr.RequestHandlers
                            where p.OriginalRequestID == reqID
                            select p
                      ).ToList();
            try
            {
                foreach (var req in requests)
                {
                    req.ReqStatus = "Approved";
                }
                hr.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {

                return "Error";
            }
        }
        public string RejectDayByDayForManager(int reqID)
        {
            var requests = (from p in hr.RequestHandlers
                            where p.OriginalRequestID == reqID
                            select p
                     ).ToList();
            try
            {
                foreach (var req in requests)
                {
                    req.ReqStatus = "Rejected";
                }
                hr.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {

                return "Error";
            }
        }

        public void SendMail_ExcuseApproved_ByTeamLeader(string user, string usermail, string teamleader, string teamleadermail, string supervisormail, string ExcuseDay, string ExcuseDurationFrom, string ExcuseDurationTo)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Excuse Request Approved By Team Leader";
                message.Body = "Dear  " + user + " , " + teamleader + " has just Approved your Excuse on :" + ExcuseDay + " From " + ExcuseDurationFrom + " To " + ExcuseDurationTo + ".";
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(usermail);
                message.CcRecipients.Add(teamleadermail);
                message.CcRecipients.Add(supervisormail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
        }
        public void SendMail_ExcuseRejected_ByTeamLeader(string user, string usermail, string teamleader, string teamleadermail, string supervisormail, string ExcuseDay, string ExcuseDurationFrom, string ExcuseDurationTo)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Excuse Request Rejected By Team Leader";
                message.Body = "Dear  " + user + " , " + teamleader + " has just Rejected your Excuse On :" + ExcuseDay + " From " + ExcuseDurationFrom + " To " + ExcuseDurationTo + ".";
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(usermail);
                message.CcRecipients.Add(teamleadermail);
                message.CcRecipients.Add(supervisormail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
        }
        public void SendMail_ExcuseApproved_BySupervisor(string user, string usermail, string supervisor, string deputymanagermail, string teamleadermail, string managermail, string supervisormail, string ExcuseDay, string ExcuseDurationFrom, string ExcuseDurationTo)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Excuse Request Approved By Supervisor";
                message.Body = "Dear  " + user + " , " + supervisor + " has just Approved your Excuse On :" + ExcuseDay + " From " + ExcuseDurationFrom + " To " + ExcuseDurationTo + ".";
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(usermail);
                message.CcRecipients.Add(supervisormail);
                message.CcRecipients.Add(teamleadermail);
                message.CcRecipients.Add(deputymanagermail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
        }
        public void SendMail_ExcuseRejected_BySupervisor(string user, string usermail, string supervisor, string teamleadermail, string supervisormail, string ExcuseDay, string ExcuseDurationFrom, string ExcuseDurationTo)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Excuse Request Rejected By Supervisor";
                message.Body = "Dear  " + user + " , " + supervisor + " has just Rejected your Excuse On :" + ExcuseDay + " From " + ExcuseDurationFrom + " To " + ExcuseDurationTo + ".";
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(usermail);
                message.CcRecipients.Add(supervisormail);
                message.CcRecipients.Add(teamleadermail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
        }
        public void SendMail_ExcuseApproved_ByManager(string user, string usermail, string manager, string managermail, string ExcuseDay, string ExcuseDurationFrom, string ExcuseDurationTo)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Excuse Request Approved By Manager";
                message.Body = "Dear  " + user + " , " + manager + " has just Approved your Excuse On :" + ExcuseDay + " From " + ExcuseDurationFrom + " To " + ExcuseDurationTo + ".";
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(usermail);
                message.CcRecipients.Add(managermail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
        }
        public void SendMail_ExcuseRejected_ByManager(string user, string usermail, string manager, string managermail, string ExcuseDay, string ExcuseDurationFrom, string ExcuseDurationTo)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Excuse Request Rejected By Manager";
                message.Body = "Dear  " + user + " , " + manager + " has just Rejected your Excuse On :" + ExcuseDay + " From " + ExcuseDurationFrom + " To " + ExcuseDurationTo + ".";
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(usermail);
                message.CcRecipients.Add(managermail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
        }
    }
}