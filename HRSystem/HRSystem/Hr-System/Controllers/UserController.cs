using Hr_System.Classes;
using Hr_System.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Exchange;
using Microsoft.Exchange.WebServices;
using Microsoft.Exchange.WebServices.Data;
using System.Globalization;
namespace Hr_System.Controllers
{
    public class UserController : Controller
    {

        DateTime ReqDate = DateTime.Now;
        string type;
        string Name;
        string Dep;
        string subDep;
        string arabicName;
        string status;
        HRSystemEntities hr = new HRSystemEntities();
        Helpers obj = new Helpers();
        VacationController objj = new VacationController();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult addExcuseRequest()
        {
            string userName =(string)Session["userName"];
            decimal RemainExcusehrs = obj.GetRemainingExcuseHours(userName);
            ViewBag.Remainexcuehours = RemainExcusehrs;
            return View();

        }
        public JsonResult CreateExcuseRequest(Request req, string ReqType, string ExcuseDay, string ReqSubType, string ExcuseDuration, string ExcuseDurationFrom, string ExcuseDurationTo)
        {
            type = (string)(Session["UserType"]);
            Name = (string)(Session["UserName"]);
            Dep = (string)(Session["Dep"]);
            subDep = (string)(Session["subDep"]);
            arabicName = (string)(Session["ArabicName"]);
            string MyManager = obj.getDepManagerName(Dep);
            string MyTeamLeader = obj.getSubDepTeamLeaderName(subDep);
            string MySupervisor = obj.getSubDepSupervisor(subDep);
            string MyDepSenior = obj.getSubDepSenior(subDep);
            string MyDepSupervisorBackup = obj.getSubDepBackupSupervisor(subDep);
            string MyDuputyManager = obj.getDepDuputyManager(Dep);
            var UserMail = (from item in hr.accountTBs where item.EmpName == Name select item.Email).SingleOrDefault();
            var SeniorEmail = (from item in hr.accountTBs where item.EmpName == MyDepSenior select item.Email).SingleOrDefault();
            var TeamLeaderMail = (from item in hr.accountTBs where item.EmpName == MyTeamLeader select item.Email).SingleOrDefault();
            var SupervisorMail = (from item in hr.accountTBs where item.EmpName == MySupervisor select item.Email).SingleOrDefault();
            var BackupSupervisorMail = (from item in hr.accountTBs where item.EmpName == MyDepSupervisorBackup select item.Email).SingleOrDefault();
            var DuputyManagerMail = (from item in hr.accountTBs where item.EmpName == MyDuputyManager select item.Email).SingleOrDefault();
            var ManagerMail = (from item in hr.accountTBs where item.EmpName == MyManager select item.Email).SingleOrDefault();

            string message = "";
            bool havesubdep = obj.haveSubDep(Name);
            var code = hr.accountTBs.Where(x => x.EmpName == Name).Select(x => x.EmpCode).FirstOrDefault();
            //int remainingExcuses = obj.haveExcuseCredit(Name);
            //new
            decimal RemainingExcuseHours = obj.GetRemainingExcuseHours(Name);
            //end
            bool weekendorofficialholiday = objj.checkifofficialdayorWeekend(DateTime.Parse(ExcuseDay).Date, DateTime.Parse(ExcuseDay).Date);
            bool checkifvalidexcuse = checkifalreadyvacation(DateTime.Parse(ExcuseDay).Date, DateTime.Parse(ExcuseDay).Date, Name);
            //bool checkexcusetype = checkifvalidExcusestype(DateTime.Parse(ExcuseDay).Date, Name, ReqSubType);
            bool checkapprovedexcuses = checkifalreadyExcuseday(DateTime.Parse(ExcuseDay).Date, Name);
            bool checkmanagmentapproval = checkifvalidexcuseInManagmentView(DateTime.Parse(ExcuseDay).Date, Name);
            bool validdates = checkifvaliddates(DateTime.Parse(ExcuseDay).Date);
           
            var duputymanager = (from d in hr.accountTBs
                                 where d.EmpName == req.MyBackupManager
                                 select d.EmpName).SingleOrDefault();
            var duputymanagermail = (from d in hr.accountTBs
                                     where d.EmpName == req.MyBackupManager
                                     select d.Email).SingleOrDefault();
            //new

            string toDuration = ExcuseDurationTo.Substring(0, 5);
            string fromDuration = ExcuseDurationFrom.Substring(0,5);

            DateTime beign = Convert.ToDateTime(fromDuration);
            DateTime end = Convert.ToDateTime(toDuration);

            TimeSpan exactduration = end - beign;
            decimal exactTime =Convert.ToDecimal(exactduration.TotalHours);

           
            
            
            if (ExcuseDuration == "نصف ساعة")
            {
                req.ExcuseSpecificDuration = Convert.ToDecimal(0.50);
            }
            else if (ExcuseDuration == "ساعة")
            {
                req.ExcuseSpecificDuration = Convert.ToDecimal(1.00);
            }
            else if (ExcuseDuration == "ساعة ونصف")
            {
                req.ExcuseSpecificDuration = Convert.ToDecimal(1.50);
            }
            else if (ExcuseDuration == "ساعتين")
            {
                req.ExcuseSpecificDuration = Convert.ToDecimal(2.00);
            }
            //else if (ExcuseDuration == "ساعتين ونصف")
            //{
            //    req.ExcuseSpecificDuration = Convert.ToDecimal(2.50);
            //}
            if(req.ExcuseSpecificDuration != exactTime)
            {
                message = "Excuse duration is not correct";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            decimal? TOtalExcuseHourRemaining;
            TOtalExcuseHourRemaining = (from Hours in hr.accountTBs
                                         where Hours.EmpName ==Name
                                         select Hours.NumberOfExcuseHours).First();

            decimal? ExcusSpecificDuration = req.ExcuseSpecificDuration;

            if (TOtalExcuseHourRemaining < ExcusSpecificDuration)
            {
                message = "You requested an excuse with longer duration than what you have";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            //end
            if (validdates == true)
            {
                message = "Please Check Date..No Backdated Excuses are Allowed";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            else if (weekendorofficialholiday == true)
            {
                message = "You Cannot Request Excuse for Holiday Or WeekEnd";
                return Json(message, JsonRequestBehavior.AllowGet);

            }
            else if (checkifvalidexcuse == true)
            {
                message = "You Cannot Request Excuse for already PendingApproval/Approved vacation Day";
                return Json(message, JsonRequestBehavior.AllowGet);

            }
            else if (checkmanagmentapproval == true)
            {
                message = "You Cannot Request Another Excuse Unless the first One gets Approved/Rejected";
                return Json(message, JsonRequestBehavior.AllowGet);

            }
            else if (checkapprovedexcuses == true)
            {
                message = "You Cannot Request Two Excuses at the same day";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

            else
            {
                if (RemainingExcuseHours != 0)
                {
                    if (type == "TeamLeader" )
                    {
                        if (MyDuputyManager != null)
                        {
                        }
                        if (MyDepSupervisorBackup == null)
                        {
                            status = "PendingSupervisorApproval";
                            req.MySupervisor = MySupervisor;
                            SendMail_TeamLeader_ExcuseRequested_For_Supervisor(Name, UserMail, MySupervisor, SupervisorMail, ExcuseDay, ExcuseDuration, ExcuseDurationFrom, ExcuseDurationTo);

                        }
                        else
                        {
                            status = "PendingSupervisorBackupApproval";
                            req.MySupervisor = MySupervisor;
                            req.MyBackupSupervisor = MyDepSupervisorBackup;
                            SendMail_TeamLeader_ExcuseRequested_For_SupervisorBackup(Name, UserMail, MyDepSupervisorBackup, BackupSupervisorMail, SupervisorMail, ExcuseDay,ExcuseDuration, ExcuseDurationFrom, ExcuseDurationTo);
                        }
                        //  obj.addNotifications(ReqDate.ToString(), Name, MyManager, " " + Name + " sent you Excuse Request ", Dep, subDep, "unread");

                    }
                    else if (type == "Normal")
                    {
                        if (havesubdep == false)
                        {
                            if (MySupervisor == null)
                            {
                                if (MyDuputyManager == null)
                                {
                                    status = "PendingManagerApproval";
                                    SendMail_NormalUser_ExcuseRequested_For_Manager(Name, UserMail, MyManager, ManagerMail, ExcuseDay, ExcuseDuration, ExcuseDurationFrom, ExcuseDurationTo);


                                }
                                else
                                {
                                    status = "PendingDuputyManagerApproval";
                                    SendMail_NormalUser_ExcuseRequested_For_ManagerBackup(Name , UserMail , MyDuputyManager , ManagerMail , DuputyManagerMail, ExcuseDay , ExcuseDuration , ExcuseDurationFrom , ExcuseDurationTo);
                                    
                                }
                            }
                            else
                            {
                                if (MyDuputyManager != null)
                                {
                                }
                                if (MyDepSupervisorBackup == null)
                                {
                                    status = "PendingSupervisorApproval";
                                    req.MySupervisor = MySupervisor;
                                    SendMail_NormalUser_ExcuseRequested_For_Supervisor(Name , UserMail , MySupervisor , SupervisorMail , ManagerMail , ExcuseDay , ExcuseDuration , ExcuseDurationFrom , ExcuseDurationTo);
                                }
                                else
                                {
                                    status = "PendingSupervisorBackupApproval";
                                    req.MySupervisor = MySupervisor;
                                    req.MyBackupSupervisor = MyDepSupervisorBackup;
                                    SendMail_NormalUser_ExcuseRequested_For_SupervisorBackup(Name , UserMail , MyDepSupervisorBackup , BackupSupervisorMail , ManagerMail , SupervisorMail , ExcuseDay , ExcuseDuration , ExcuseDurationFrom , ExcuseDurationTo);
                                }
                            }
                        }

                        //  obj.addNotifications(ReqDate.ToString(), Name, MyManager, " " + Name + " sent you Excuse Request ", Dep, subDep, "unread");
                        else
                        {
                            if (MyDuputyManager != null)
                            {
                            }
                            if (MyDepSenior == null)
                            {
                                if (MyTeamLeader==null)
                                {
                                    if (MySupervisor==null)
                                    {
                                        if (MyDuputyManager == null)
                                        {
                                            status = "PendingManagerApproval";
                                            SendMail_NormalUser_ExcuseRequested_For_Manager(Name, UserMail, MyManager, ManagerMail, ExcuseDay, ExcuseDuration, ExcuseDurationFrom, ExcuseDurationTo);


                                        }
                                        else
                                        {
                                            status = "PendingDuputyManagerApproval";
                                            SendMail_NormalUser_ExcuseRequested_For_ManagerBackup(Name, UserMail, MyDuputyManager, ManagerMail, DuputyManagerMail, ExcuseDay, ExcuseDuration, ExcuseDurationFrom, ExcuseDurationTo);

                                        }
                                    }
                                    else
                                    {
                                        if (MyDepSupervisorBackup == null)
                                        {
                                            status = "PendingSupervisorApproval";
                                            req.MySupervisor = MySupervisor;
                                            SendMail_NormalUser_ExcuseRequested_For_Supervisor(Name, UserMail, MySupervisor, SupervisorMail, ManagerMail, ExcuseDay, ExcuseDuration, ExcuseDurationFrom, ExcuseDurationTo);
                                        }
                                        else
                                        {
                                            status = "PendingSupervisorBackupApproval";
                                            req.MySupervisor = MySupervisor;
                                            req.MyBackupSupervisor = MyDepSupervisorBackup;
                                            SendMail_NormalUser_ExcuseRequested_For_SupervisorBackup(Name, UserMail, MyDepSupervisorBackup, BackupSupervisorMail, ManagerMail, SupervisorMail, ExcuseDay, ExcuseDuration, ExcuseDurationFrom, ExcuseDurationTo);
                                        }
                                    }

                                }
                                else
                                {
                                    status = "PendingTeamLeaderApproval";
                                    req.MyTeamLeader = obj.getSubDepTeamLeaderName(subDep);
                                    SendMail_NormalUser_ExcuseRequested_For_TeamLeader(Name, UserMail, MyTeamLeader, TeamLeaderMail, SupervisorMail, ExcuseDay, ExcuseDuration, ExcuseDurationFrom, ExcuseDurationTo);
                                    if (MyDepSupervisorBackup != null)
                                    {
                                        req.MySupervisor = MySupervisor;
                                        req.MyBackupSupervisor = MyDepSupervisorBackup;

                                    }
                                    else
                                    {
                                        req.MySupervisor = MySupervisor;
                                    }
                                }

                            }
                            else
                            {
                                status = "PendingSeniorApproval";
                                req.MyTeamLeader = MyTeamLeader;
                                req.MyBackupTeamLeader = MyDepSenior;
                                SendMail_NormalUser_ExcuseRequested_For_TeamLeaderBackup(Name, UserMail, MyDepSenior, SeniorEmail, TeamLeaderMail, SupervisorMail, ExcuseDay, ExcuseDuration, ExcuseDurationFrom, ExcuseDurationTo);
                                if (MyDepSupervisorBackup != null)
                                {
                                    req.MySupervisor = MySupervisor;
                                    req.MyBackupSupervisor = MyDepSupervisorBackup;
                                }
                                else
                                {
                                    req.MySupervisor = MySupervisor;
                                }

                            }
                            // obj.addNotifications(ReqDate.ToString(), Name, MyTeamLeader, " " + Name + " sent you Excuse Request ", Dep, subDep, "unread");

                        }
                    }
                    else if (type == "Supervisor" || type == "Deputy Manager")
                    {
                        //if (MyDuputyManager == null)
                        //{
                        //    status = "PendingManagerApproval";
                        //    SendMail_Supervisor_ExcuseRequested_For_Manager(Name , UserMail , MyManager , ManagerMail , ExcuseDay , ExcuseDuration , ExcuseDurationFrom , ExcuseDurationTo);
                        //}
                        //else
                        
                            status = "PendingDuputyManagerApproval";
                            SendMail_Supervisor_ExcuseRequested_For_Manager_Backup(Name , UserMail , duputymanager, duputymanagermail, ManagerMail , ExcuseDay , ExcuseDuration , ExcuseDurationFrom , ExcuseDurationTo);
                        
                    }
                    else if (type == "Deputy Manager")
                    {
                        status = "PendingManagerApproval";
                        SendMail_Supervisor_ExcuseRequested_For_Manager(Name, UserMail, MyManager, ManagerMail, ExcuseDay, ExcuseDuration, ExcuseDurationFrom, ExcuseDurationTo);
                    }
             
                    
                    req.ReqStatus = status;
                    req.MyManager = obj.getDepManagerName(Dep);
                    req.ReqDate = ReqDate;
                    req.DepartmentName = subDep;
                    req.ManagmentName = Dep;
                    req.ArabicName = arabicName;
                    req.UserName = Name;
                    req.EmployeeCode = code;
                    hr.Requests.Add(req);
                    hr.SaveChanges();

                    decimal? TOtalExcuseHoursRemaining;

                    TOtalExcuseHoursRemaining = (from Hour in hr.accountTBs
                                                 where Hour.EmpName == req.UserName
                                                 select Hour.NumberOfExcuseHours).First();

                    decimal? ExcuseSpecificDuration = (from Dur in hr.Requests
                                                       where Dur.ID == req.ID
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

                    decimal? exc = TOtalExcuseHoursRemaining - ExcuseSpecificDuration;
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
/////////////////////////////////////////////////////////////////////////////////////

                    InsertExcuse(ReqSubType, Name, DateTime.Parse(ExcuseDay), status, req.ID);
                    message = "Successfully Saved!";
                    return Json(message, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    message = "You Don't Have Any Execues Credit";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }
        }
        [HttpGet]
        public ActionResult getAllMyExcusesRequests()
        {
            Name = (string)(Session["UserName"]);
            var data = obj.getAllMyExcusRequests(Name);
            return View(data);
        }
        [HttpGet]
        public ActionResult getAllMyPendingExcusesRequests()
        {
            Name = (string)(Session["UserName"]);
            type = (string)(Session["UserType"]);
            bool havesubdep = obj.haveSubDep(Name);
            var data = obj.getMyExcuseRequestsByStatusByAllTypes(Name, "PendingManagerApproval", "", "", "", "", "");
            if (type == "TeamLeader")
            {
                data = obj.getMyExcuseRequestsByStatusByAllTypes(Name, "PendingSupervisorApproval", "PendingSupervisorBackupApproval", "PendingManagerApproval", "PendingDuputyManagerApproval", "", "");
            }
            else if (type == "Normal")
            {

                data = obj.getMyExcuseRequestsByStatusByAllTypes(Name, "PendingManagerApproval", "PendingDuputyManagerApproval", "PendingSupervisorApproval", "PendingSupervisorBackupApproval", "PendingTeamLeaderApproval", "PendingSeniorApproval");


            }
            else if (type == "Supervisor" || type == "Deputy Manager")
            {

                data = obj.getMyExcuseRequestsByStatusByAllTypes(Name, "PendingManagerApproval", "PendingDuputyManagerApproval", "", "", "", "");
            }

            return View(data);
        }
        [HttpGet]
        public ActionResult getAllMyRejectedExcusesRequests()
        {
            Name = (string)(Session["UserName"]);
            type = (string)(Session["UserType"]);
            bool havesubdep = obj.haveSubDep(Name);
            var data = obj.getMyExcuseRequestsByStatus(Name, "Rejected");
            if (type == "Supervisor")
            {
                data = obj.getMyExcuseRequestsByStatus(Name, "Rejected");

            }
            else if (type == "TeamLeader")
            {
                data = obj.getMyExcuseRequestsByStatusByAllTypes(Name, "RejectedBySupervisor", "Rejected", "", "", "", "");

            }
            else if (type == "Normal")
            {
                if (havesubdep == false)
                {

                    data = obj.getMyExcuseRequestsByStatus(Name, "Rejected");

                }
                else
                {
                    data = obj.getMyExcuseRequestsByStatusByAllTypes(Name, "RejectedByTeamLeader", "RejectedBySupervisor", "Rejected", "", "", "");
                }
            }
            return View(data);
        }
        [HttpGet]
        public ActionResult getAllApprovedExcusesRequests()
        {
            Name = (string)(Session["UserName"]);
            type = (string)(Session["UserType"]);

            var data = obj.getMyExcuseRequestsByStatus(Name, "Approved");

            return View(data);
        }
        [HttpGet]
        public ActionResult getMyExcuseRequests()
        {
            return View();
        }
        [HttpGet]
        public ActionResult requestDetails(int ID)
        {

            Request req = hr.Requests.Find(ID);
            ViewBag.ID = req.ID;
            ViewBag.status = req.ReqStatus;
            ViewBag.Manager = req.MyManager;
            ViewBag.TeamLeader = req.MyTeamLeader;
            ViewBag.UserName = req.UserName;
            Session["MySupervisor"] = req.MySupervisor;
            Session["MyDuputyManager"] = req.MyBackupManager;
            Session["MyTeamLeader"] = req.MyTeamLeader;
            ViewBag.Supervisor = req.MySupervisor;
            ViewBag.DuputyManager = req.MyBackupManager;
            ViewBag.Managment = req.ManagmentName;
            ViewBag.Department = req.DepartmentName;
           // ViewBag.RemainingExcuses = obj.getRemainingExcusesForUser(req.UserName);
            ViewBag.RemainingExcusHouer = obj.GetRemainingExcuseHours(req.UserName);
            return View(req);

        }
        public JsonResult DeleteExcuse(int ID)
        {
            string message = "";
            try
            {
                Request req = hr.Requests.Find(ID);

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
                hr.Requests.Remove(req);
                hr.SaveChanges();

                DeleteDayByDay(ID);
                message = "Removed Sucussfully";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                message = "Error";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

        }
        public string DeleteDayByDay(int reqID)
        {
            var requests = (from p in hr.RequestHandlers
                            where p.OriginalRequestID == reqID
                            select p).ToList();
            try
            {
                foreach (var req in requests)
                {
                    hr.RequestHandlers.Remove(req);
                }
                hr.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {

                return "Error"; ;
            }

        }
        [HttpGet]
        public ActionResult GetUsersProject()
        {
            Name = (string)(Session["UserName"]);
            type = (string)(Session["UserType"]);
            Project pro = new Project();
            ViewBag.MainProjects = (from f in hr.Projects
                                    where f.ProjManager == Name
                                    select f).ToList();
            ViewBag.BackupProjects = (from f in hr.Projects
                                      where f.BackupProject == Name
                                      select f).ToList();
            ViewBag.AllProjects = (from f in hr.ProjectMembers
                                   where f.ProjMembs == Name
                                   select f).ToList();
            return View();
        }
        public ActionResult Report(string type, int ID)
        {
            var Request = hr.Requests.Find(ID);
            if (Request.ReqStatus == "Approved")
            {
                LocalReport lr = new LocalReport();
                string path = Path.Combine(Server.MapPath("~/Reports"), "Excuse.rdlc");
                if (System.IO.File.Exists(path))
                {
                    lr.ReportPath = path;
                }
                else
                {
                    return View("requestDetails");
                }
                List<Request> cm = new List<Request>();
                cm = hr.Requests.Where(a => a.ID.Equals(ID)).ToList();
                ReportDataSource rd = new ReportDataSource("DataSet1", cm);
                lr.DataSources.Add(rd);
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;
                string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>" + type + "</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";
                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                renderedBytes = lr.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);
                return File(renderedBytes, mimeType);
            }
            else
            {
                return null;
            }

        }
        public bool checkifalreadyvacation(DateTime startdate, DateTime enddate, string username)
        {
            bool alreadyholiday = false;
            var prevHolidays = hr.RequestHandlers.Where(x => x.UserName == username).Where(x => x.RequestType == "إجازة").Where(x => x.ReqStatus == "ApprovedByManager" || x.ReqStatus == "PendingTeamLeaderApproval" || x.ReqStatus == "PendingSupervisorApproval" || x.ReqStatus == "PendingManagerApproval" || x.ReqStatus == "PendingDuputyManagerApproval" || x.ReqStatus == "PendingSeniorApproval" || x.ReqStatus == "PendingSupervisorApproval").ToList();
            for (int i = 0; i < prevHolidays.Count; i++)
            {
                if (startdate.Date == prevHolidays[i].Offday.Value.Date || enddate.Date == prevHolidays[i].Offday.Value.Date)
                {
                    alreadyholiday = true;
                }
            }

            return alreadyholiday;
        }
        public bool checkifvaliddates(DateTime startdate)
        {
            if (startdate.Date >= DateTime.Now.Date)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
     
        public bool checkifvalidexcuseInManagmentView(DateTime startdate, string username)
        {
            bool alreadychoosentype = false;

            var excusedays = hr.RequestHandlers.Where(x => x.UserName == username).Where(x => x.RequestType == "Excuse").Where(x => x.Offday == startdate.Date).Where(x => x.ReqStatus == "PendingTeamLeaderApproval" || x.ReqStatus == "PendingManagerApproval" || x.ReqStatus == "PendingDuputyManagerApproval" || x.ReqStatus == "PendingSeniorApproval" || x.ReqStatus == "PendingSupervisorApproval" || x.ReqStatus == "PendingSupervisorBackupApproval").ToList();

            if (excusedays.Count == 0)
            {
                alreadychoosentype = false;

            }

            else
            {
                alreadychoosentype = true;

            }


            return alreadychoosentype;
        }
        public bool checkifalreadyExcuseday(DateTime startdate, string username)
        {
            bool alreadyExcuse = false;
            var userApprovedExcuses = hr.RequestHandlers.Where(p => p.RequestType == "Excuse").Where(p => p.UserName == username).Where(p => p.Offday == startdate.Date).Where(p => p.ReqStatus == "Approved");
            if (userApprovedExcuses.Count() == 0)
            {
                alreadyExcuse = false;
            }
            else
            {
                alreadyExcuse = true;
            }
            return alreadyExcuse;
        }
        public void InsertExcuse(string Excusesubtype, string username, DateTime excusesday, string reqstat, int originalID)
        {
            RequestHandler RH = new RequestHandler();
            RH.RequestType = "Excuse";
            RH.RequestSubType = Excusesubtype;
            RH.UserName = username;
            RH.Offday = excusesday;
            RH.ReqStatus = reqstat;
            RH.OriginalRequestID = originalID;
            hr.RequestHandlers.Add(RH);
            hr.SaveChanges();
        }

        public void SendMail_NormalUser_ExcuseRequested_For_Manager(string username, string usermail, string manager, string managermail, string excuseday, string ExcuseDuration, string ExcuseDurationFrom, string ExcuseDurationTo)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "New Excuse Request";
                message.Body = "Dear  " + manager + " , " + username + " has just Requested Excuse on " + excuseday + " From " + ExcuseDurationFrom + " To " + ExcuseDurationTo + ".";
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(managermail);
                message.CcRecipients.Add(usermail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
        }
        public void SendMail_NormalUser_ExcuseRequested_For_ManagerBackup(string user, string usermail, string managerBackup, string managermail, string managerbackupmail, string excuseday, string ExcuseDuration, string ExcuseDurationFrom, string ExcuseDurationTo)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "New Excuse Request";
                message.Body = "Dear  " + managerBackup + " , " + user + " has just Requested Excuse on " + excuseday + " From " + ExcuseDurationFrom + " To " + ExcuseDurationTo + ".";
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(managerbackupmail);
                message.CcRecipients.Add(usermail);
                message.CcRecipients.Add(managermail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
        }
        public void SendMail_NormalUser_ExcuseRequested_For_Supervisor(string user, string usermail, string supervisor, string supervisormail, string managermail, string excuseday, string ExcuseDuration, string ExcuseDurationFrom, string ExcuseDurationTo)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "New Excuse Request";
                message.Body = "Dear  " + supervisor + " , " + user + " has just Requested Excuse on " + excuseday + " From " + ExcuseDurationFrom + " To " + ExcuseDurationTo + ".";
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(supervisormail);
                message.CcRecipients.Add(usermail);
                message.CcRecipients.Add(managermail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
        }
        public void SendMail_NormalUser_ExcuseRequested_For_SupervisorBackup(string user, string usermail, string supervisorbackup, string supervisorBackupmail, string managermail, string supervisormail, string excuseday, string ExcuseDuration, string ExcuseDurationFrom, string ExcuseDurationTo)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "New Excuse Request";
                message.Body = "Dear  " + supervisorbackup + " , " + user + " has just Requested Excuse on " + excuseday + " From " + ExcuseDurationFrom + " To " + ExcuseDurationTo + ".";
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(supervisorBackupmail);
                message.CcRecipients.Add(usermail);
                message.CcRecipients.Add(supervisormail);
                message.CcRecipients.Add(managermail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
        }
        public void SendMail_NormalUser_ExcuseRequested_For_TeamLeader(string user, string usermail, string teamleader, string teamleadermail, string supervisormail, string excuseday, string ExcuseDuration, string ExcuseDurationFrom, string ExcuseDurationTo)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "New Excuse Request";
                message.Body = "Dear  " + teamleader + " , " + user + " has just Requested Excuse on " + excuseday + " From " + ExcuseDurationFrom + " To " + ExcuseDurationTo + ".";
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(teamleadermail);
                message.CcRecipients.Add(usermail);
                message.CcRecipients.Add(supervisormail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
        }
        public void SendMail_NormalUser_ExcuseRequested_For_TeamLeaderBackup(string user, string usermail, string teamleaderBackup, string teamleaderbackupmail, string teamleadermail, string supervisormail, string excuseday, string ExcuseDuration, string ExcuseDurationFrom, string ExcuseDurationTo)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "New Excuse Request";
                message.Body = "Dear  " + teamleaderBackup + " , " + user + " has just Requested Excuse on " + excuseday + " From " + ExcuseDurationFrom + " To " + ExcuseDurationTo + ".";
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(teamleaderbackupmail);
                message.CcRecipients.Add(usermail);
                message.CcRecipients.Add(teamleadermail);
                message.CcRecipients.Add(supervisormail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
        }
        public void SendMail_TeamLeader_ExcuseRequested_For_Supervisor(string user, string usermail, string supervisor, string supervisormail, string excuseday, string ExcuseDuration, string ExcuseDurationFrom, string ExcuseDurationTo)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "New Excuse Request";
                message.Body = "Dear  " + supervisor + " , " + user + " has just Requested Excuse on " + excuseday + " From " + ExcuseDurationFrom + " To " + ExcuseDurationTo + ".";
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(supervisormail);
                message.CcRecipients.Add(usermail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
        }
        public void SendMail_TeamLeader_ExcuseRequested_For_SupervisorBackup(string user, string usermail, string supervisorbackup, string supervisorbackupmail, string supervisormail, string excuseday, string ExcuseDuration, string ExcuseDurationFrom, string ExcuseDurationTo)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "New Excuse Request";
                message.Body = "Dear  " + supervisorbackup + " , " + user + " has just Requested Excuse on " + excuseday + " From " + ExcuseDurationFrom + " To " + ExcuseDurationTo + ".";
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(supervisorbackupmail);
                message.CcRecipients.Add(usermail);
                message.CcRecipients.Add(supervisormail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
        }
        public void SendMail_Supervisor_ExcuseRequested_For_Manager(string user, string usermail, string manager, string managermail, string excuseday, string ExcuseDuration, string ExcuseDurationFrom, string ExcuseDurationTo)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "New Excuse Request";
                message.Body = "Dear  " + manager + " , " + user + " has just Requested Excuse on " + excuseday + " From " + ExcuseDurationFrom + " To " + ExcuseDurationTo + ".";
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(managermail);
                message.CcRecipients.Add(usermail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
        }
        public void SendMail_Supervisor_ExcuseRequested_For_Manager_Backup(string user, string usermail, string managerbackup, string managerbackupmail, string managermail , string excuseday, string ExcuseDuration, string ExcuseDurationFrom, string ExcuseDurationTo)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "New Excuse Request";
                message.Body = "Dear  " + managerbackup + " , " + user + " has just Requested Excuse on " + excuseday + " From " + ExcuseDurationFrom + " To " + ExcuseDurationTo + ".";
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(managerbackupmail);
                message.CcRecipients.Add(usermail);
                //message.CcRecipients.Add(managermail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
        }
    }
}