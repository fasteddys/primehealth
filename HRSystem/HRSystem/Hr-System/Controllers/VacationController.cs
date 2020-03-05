using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hr_System.Models;
using Microsoft.Reporting.WebForms;
using System.IO;
using Microsoft.Exchange;
using Microsoft.Exchange.WebServices;
using Microsoft.Exchange.WebServices.Data;
using System.Data.Objects;
namespace Hr_System.Controllers
{
    public class VacationController : Controller
    {
        private HRSystemEntities db = new HRSystemEntities();
        string Name;
        string type;
        #region PublicFunctions
        public ActionResult GetMyVacation()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.UserName == Name).OrderByDescending(p => p.ID));

        }
        public ActionResult GetMyApprovedManagerVacation()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.UserName == Name).Where(p => p.ReqStatus == "ApprovedByManager").OrderByDescending(p => p.ID));

        }
        public ActionResult GetMyRejectedManagerVacation()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.UserName == Name).Where(p => p.ReqStatus == "RejectedByManager").OrderByDescending(p => p.ID));

        }
        public ActionResult GetMySupervisorRejectedVacation()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.UserName == Name).Where(p => p.ReqStatus == "RejectedBySupervisor").OrderByDescending(p => p.ID));

        }
        #endregion
        #region Normal User Functions
        public ActionResult GetMyPendingTeamLeaderVacation()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.UserName == Name).Where(p => p.ReqStatus == "PendingTeamLeaderApproval" || p.ReqStatus == "PendingSeniorApproval").OrderByDescending(p => p.ID));

        }
        public ActionResult GetMyApprovedTeamLeaderVacation()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.UserName == Name).Where(p => p.ReqStatus == "PendingSupervisorApproval" || p.ReqStatus == "PendingSupervisorBackupApproval" || p.ReqStatus == "RevokedByUser").OrderByDescending(p => p.ID));

        }
        public ActionResult GetMyPendingManagerVacation()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.UserName == Name).Where(p => p.ReqStatus == "PendingManagerApproval" || p.ReqStatus == "PendingDuputyManagerApproval" || p.ReqStatus == "PendingDuputyManagerApprovalOnRevoke" || p.ReqStatus == "PendingManagerApprovalOnRevoke").OrderByDescending(p => p.ID));

        }
        public ActionResult GetMyApprovedSupervisorVacation()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.UserName == Name).Where(p => p.ReqStatus == "PendingManagerApproval").OrderByDescending(p => p.ID));

        }
        public ActionResult GetMyRejectedTeamLeaderVacation()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.UserName == Name).Where(p => p.ReqStatus == "RejectedByTeamLeader").OrderByDescending(p => p.ID));

        }
        public ActionResult GetApprovedRevokedVacations()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.UserName == Name).Where(p => p.ReqStatus == "ApprovedOnRevokeBySupervisor" || p.ReqStatus == "ApprovedOnRevokeByManager" || p.ReqStatus == "ApprovedOnRevokeByTeamLeader").OrderByDescending(p => p.ID));

        }
        public ActionResult GetRejectRevokedVacations()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.UserName == Name).Where(p => p.ReqStatus == "RevokeRejectedBySupervisor" || p.ReqStatus == "RevokeRejectedByManager" || p.ReqStatus == "RevokeRejectedByTeamLeader").OrderByDescending(p => p.ID));

        }
        #endregion
        #region TeamLeader&Supervisor Functions
        public ActionResult GetPendingMyApprovalVacations()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.MyTeamLeader == Name || p.MyBackupTeamLeader == Name).Where(p => p.ReqStatus == "PendingTeamLeaderApproval" || p.ReqStatus == "PendingSeniorApproval").OrderByDescending(p => p.ID));
        }
        public ActionResult GetPendingMyApprovalAsAsupervisorVacations()
        {
            Name = (string)Session["UserName"];
            string UserType = (string)Session["UserType"];

            if (UserType == "Supervisor")
            {
                //new
                string VRole;
                VRole = (from T in db.accountTBs
                         where T.EmpName == Name
                         select T.SupervisorRole).First();
                ViewBag.VAutRole = VRole;
                if (UserType == "Supervisor" && VRole == "yes")
                {
                    return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.MySupervisor == Name
                    || p.MyBackupSupervisor == Name).Where(p => p.ReqStatus == "PendingSupervisorBackupApproval"
                        || p.ReqStatus == "RevokedByUser" || p.ReqStatus == "RevokedByTeamLeader").OrderByDescending(p => p.ID));
                }
                //end
                else
                {
                    return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.MySupervisor == Name
                        || p.MyBackupSupervisor == Name).Where(p => p.ReqStatus == "PendingSupervisorApproval" ||
                            p.ReqStatus == "PendingSupervisorBackupApproval" || p.ReqStatus == "RevokedByUser" ||
                            p.ReqStatus == "RevokedByTeamLeader").OrderByDescending(p => p.ID));
                }
            }
            else
            {
                List<Request> RequestsForDuputyManager = new List<Request>();
                var requests = db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.MySupervisor == Name ||
                    p.MyBackupManager == Name).Where(p => p.ReqStatus == "PendingSupervisorApproval" ||
                        p.ReqStatus == "PendingSupervisorBackupApproval" || p.ReqStatus == "RevokedByUser" ||
                        p.ReqStatus == "RevokedByTeamLeader" || p.ReqStatus == "PendingDuputyManagerApproval" ||
                        p.ReqStatus == "PendingDuputyManagerApprovalOnRevoke").ToList();
                foreach (var req in requests)
                {
                    if ((req.MySupervisor == Name) && (req.ReqStatus == "PendingSupervisorApproval" ||
                        req.ReqStatus == "PendingSupervisorBackupApproval" || req.ReqStatus == "RevokedByUser"
                        || req.ReqStatus == "RevokedByTeamLeader"))
                    {
                        RequestsForDuputyManager.Add(req);
                    }
                    if ((req.MyBackupManager == Name) && (req.ReqStatus == "PendingDuputyManagerApproval"
                        || req.ReqStatus == "PendingDuputyManagerApprovalOnRevoke"))
                    {
                        RequestsForDuputyManager.Add(req);
                    }
                }
                return View("AllRequests", RequestsForDuputyManager.OrderByDescending(p => p.ID));
            }
        }
        public ActionResult GetPendingMyTeamLeadersAsAsupervisorVacations()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.MySupervisor == Name || p.MyBackupSupervisor == Name).Where(p => p.ReqStatus == "PendingTeamLeaderApproval" || p.ReqStatus == "PendingSeniorApproval").OrderByDescending(p => p.ID));
        }
        public ActionResult GetPendingManagerApproval()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.UserName == Name).Where(p => p.ReqStatus == "PendingManagerApproval" || p.ReqStatus == "PendingDuputyManagerApproval" || p.ReqStatus == "RevokedBySupervisor" || p.ReqStatus == "PendingDuputyManagerApprovalOnRevoke").OrderByDescending(p => p.ID));

        }
        public ActionResult GetPendingSupervisorApproval()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.UserName == Name).Where(p => p.ReqStatus == "PendingSupervisorApproval" || p.ReqStatus == "PendingSupervisorBackupApproval" || p.ReqStatus == "RevokedByTeamLeader").OrderByDescending(p => p.ID));

        }
        public ActionResult GetSupervisorApprovedOnRevoke()
        {
            Name = (string)Session["UserName"];

            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.UserName == Name).Where(p => p.ReqStatus == "ApprovedOnRevokeBySupervisor").OrderByDescending(p => p.ID));
        }
        public ActionResult GetManagerApprovedOnRevoke()
        {
            Name = (string)Session["UserName"];

            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.UserName == Name).Where(p => p.ReqStatus == "ApprovedOnRevokeByManager").OrderByDescending(p => p.ID));
        }
        public ActionResult GetManagerRejectedOnRevoke()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.UserName == Name).Where(p => p.ReqStatus == "RevokeRejectedByManager").OrderByDescending(p => p.ID));
        }
        public ActionResult GetSupervisorRejectedOnRevoke()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.UserName == Name).Where(p => p.ReqStatus == "RevokeRejectedBySupervisor").OrderByDescending(p => p.ID));
        }
        #endregion
        #region Manager Function
        public ActionResult GetPendingMyApprovalAsManagerVacations()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.MyManager == Name).Where(p => p.ReqStatus == "PendingManagerApproval" || p.ReqStatus == "PendingDuputyManagerApproval" || p.ReqStatus == "RevokedBySupervisor" || p.ReqStatus == "PendingDuputyManagerApprovalOnRevoke" || p.ReqStatus == "PendingManagerApprovalOnRevoke").OrderByDescending(p => p.ID));
        }
        public ActionResult GetMyApprovedManagerVacationByManager()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.MyManager == Name).Where(p => p.ReqStatus == "ApprovedByManager").OrderByDescending(p => p.ID));
        }

        //new
        public ActionResult GetMyApprovedSupervisorVacationBySupervisor()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.MySupervisor == Name).Where(p => p.ReqStatus == "ApprovedByManager" || p.ReqStatus == "PendingManagerApproval" || p.ReqStatus == "PendingDuputyManagerApproval").OrderByDescending(p => p.ID));
        }
        //end

        //new
        public ActionResult GetMyApprovedTeamLeaderVacationByTeamleader()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.MyTeamLeader == Name).Where(p => p.ReqStatus == "ApprovedByManager" || p.ReqStatus == "PendingManagerApproval" || p.ReqStatus == "PendingDuputyManagerApproval" || p.ReqStatus == "PendingSupervisorApproval" || p.ReqStatus == "PendingSupervisorBackupApproval").OrderByDescending(p => p.ID));
        }
        //end
        public ActionResult GetMyRejectedManagerVacationByManager()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.MyManager == Name).Where(p => p.ReqStatus == "RejectedByManager").OrderByDescending(p => p.ID));

        }
        public ActionResult GetTeamLeadersPendingVacation()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.MyManager == Name).Where(p => p.ReqStatus == "PendingTeamLeaderApproval" || p.ReqStatus == "PendingSeniorApproval").OrderByDescending(p => p.ID));
        }
        public ActionResult GetSupervisorsPendingVacation()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.MyManager == Name).Where(p => p.ReqStatus == "PendingSupervisorApproval" || p.ReqStatus == "RevokedByUser" || p.ReqStatus == "RevokedByTeamLeader" || p.ReqStatus == "PendingSupervisorBackupApproval").OrderByDescending(p => p.ID));
        }
        public ActionResult GetTeamLeadersRejectedVacation()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.MyManager == Name).Where(p => p.ReqStatus == "RejectedByTeamLeader").OrderByDescending(p => p.ID));
        }
        public ActionResult GetSupervisorsRejectedVacation()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.MyManager == Name).Where(p => p.ReqStatus == "RevokeRejectedBySupervisor" || p.ReqStatus == "RejectedBySupervisor").OrderByDescending(p => p.ID));
        }
        public ActionResult GetAllApprovedRevokedVacation()
        {
            Name = (string)Session["UserName"];
            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.MyManager == Name).Where(p => p.ReqStatus == "ApprovedOnRevokeByManager" || p.ReqStatus == "ApprovedOnRevokeBySupervisor").OrderByDescending(p => p.ID));
        }
        public ActionResult GetAllRejectedRevokedVacation()
        {
            Name = (string)Session["UserName"];

            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.MyManager == Name).Where(p => p.ReqStatus == "RevokeRejectedByManager" || p.ReqStatus == "RevokeRejectedBySupervisor").OrderByDescending(p => p.ID));
        }

      
        public ActionResult GetAllSuspendedManagerLeaves()
        {
            Name = (string)Session["UserName"];

            var GetManagerNameIfDelegated = db.accountTBs.Where(a => a.DelegateTo == Name && a.DelegatedAuthorities == "yes").FirstOrDefault().EmpName;
            if (string.IsNullOrEmpty(GetManagerNameIfDelegated))
            { return View(); }

            return View("AllRequests", db.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.MyManager == GetManagerNameIfDelegated).Where(p => p.ReqStatus == "PendingManagerApproval" || p.ReqStatus == "PendingDuputyManagerApproval" || p.ReqStatus == "RevokedBySupervisor" || p.ReqStatus == "PendingDuputyManagerApprovalOnRevoke" || p.ReqStatus == "PendingManagerApprovalOnRevoke").OrderByDescending(p => p.ID));
        }
        #endregion
        //all of the above actions return partial views 
        public ActionResult ManageRequest(int? id)
        {
            //used to manage requests, it displays info about ur leave and you can view and print the leave details
            Request request = db.Requests.Find(id);
            ViewBag.ID = request.ID;
            ViewBag.UserName = request.UserName;
            Session["MySupervisor"] = request.MySupervisor;
            Session["MyDuputyManager"] = request.MyBackupManager;
            Session["MyTeamLeader"] = request.MyTeamLeader;
            ViewBag.RequestType = request.ReqSubType;
            var account = db.accountTBs.Where(p => p.EmpName == request.UserName).ToList();
            ViewBag.RemainingAnnual = account.First().AnnualVac.ToString();
            ViewBag.RemainingCasual = account.First().CasualVac.ToString();
            ViewBag.HireDate = account.First().HireDate.Value.Date;
            ViewBag.Managment = request.ManagmentName;
            ViewBag.Department = request.DepartmentName;
            return View("ManageRequest", request);
        }
        public ActionResult Create()
        {
            type = (string)(Session["UserType"]);
            Name = (string)(Session["UserName"]);
            var departmentName = (from item in db.accountTBs where item.EmpName == Name select item.DepartmentName).SingleOrDefault();
            var SubdepartmentName = (from item in db.accountTBs where item.EmpName == Name select item.SubDepartmentName).SingleOrDefault();
            ViewBag.EMPDepartmentName = departmentName;
            ViewBag.EMPSubDept = SubdepartmentName;
            ViewBag.Name = Name;
            ViewBag.Type = type;
            var EmpType = type;
            var EmpName = Name;
            var Department = departmentName;
            var SubDepartment = SubdepartmentName;
            // List<accountTB> ListOfPotetionalUsers = null; //list of users you can delegate to
            List<accountTB> ListOfPotetionalUsers = new List<accountTB>();
            var HireDate = (from p in db.accountTBs where p.EmpName == Name select p.HireDate.Value).SingleOrDefault();
            var DateDiff = Convert.ToInt32((DateTime.Now.Date - HireDate).TotalDays);//variable used to know how many days
            //did you work for the company since you were hired
            var RemainingAnnualVacation = (from p in db.accountTBs where p.EmpName == Name select p.AnnualVac).SingleOrDefault();
            ViewBag.DateDiff = DateDiff;
            ViewBag.RemainingAnnualVacation = RemainingAnnualVacation;
            if (EmpType == "TeamLeader")
            {
                ListOfPotetionalUsers = (from p in db.accountTBs
                                         where p.EmpType == "Normal"
                                         where p.SubDepartmentName == SubDepartment
                                         select p).ToList();

            }
            else if (EmpType == "Supervisor" || EmpType == "Deputy Manager")
            {
                var ListOfMySupervisedSubDepartments = (from p in db.SubDeps
                                                        where p.DepTeamLeader == EmpName || p.DepBackupSupervisor == EmpName
                                                        select p).ToList();
                var TeamLeaders = (from p in ListOfMySupervisedSubDepartments select p.DepBackupTeamLeader).ToList();
                if (TeamLeaders.Count == 0 && EmpType != "Deputy Manager")
                {
                    string sub1 = ListOfMySupervisedSubDepartments[0].SubDepartmentName;
                    string sub2 = ListOfMySupervisedSubDepartments[1].SubDepartmentName;
                    TeamLeaders = (from p in db.accountTBs where p.SubDepartmentName == sub1 || p.SubDepartmentName == sub2 select p.EmpName).ToList();
                }
                List<accountTB> accountdetails = new List<accountTB>();
                List<accountTB> finalteamleaders = new List<accountTB>(); ;
                foreach (var TeamLeader in TeamLeaders)
                {
                    accountdetails = (from p in db.accountTBs where p.EmpName == TeamLeader && p.EmpName != EmpName select p).ToList();
                    foreach (var item in accountdetails)
                    {
                        finalteamleaders.Add(item);
                    }
                }
                ListOfPotetionalUsers = finalteamleaders;
            }
            else if (EmpType == "Manager")
            {
                var Department_ID = (from p in db.DepartmentTBs where p.DeptName == Department select p.ID).FirstOrDefault();
                var subdeparments_Managed_By_Manager = (from p in db.SubDeps where p.DepartmentID == Department_ID select p).Count();
                if (subdeparments_Managed_By_Manager == 0)
                {
                    ListOfPotetionalUsers = (from p in db.accountTBs
                                             where p.EmpType == "Normal"
                                             where p.DepartmentName == Department
                                             select p).ToList();
                }
                else
                {
                    var DuputyManagers = (from p in db.SubDeps where p.DepartmentID == Department_ID select p.DepTeamLeader).Distinct();

                    List<accountTB> accountdetails = new List<accountTB>();
                    List<accountTB> finalDuputyManagers = new List<accountTB>(); ;
                    foreach (var DuputyManager in DuputyManagers)
                    {
                        accountdetails = (from p in db.accountTBs where p.EmpName == DuputyManager select p).ToList();
                        foreach (var item in accountdetails)
                        {
                            finalDuputyManagers.Add(item);
                        }
                    }

                    var DepartmentDMSAccount = (from p in db.accountTBs where p.EmpType == "Deputy Manager" select p).ToList();
                    foreach (var Account in DepartmentDMSAccount)
                    {
                        finalDuputyManagers.Add(Account);
                    }
                    finalDuputyManagers = finalDuputyManagers.Distinct().ToList();
                    ListOfPotetionalUsers = finalDuputyManagers;
                }
            }
            ViewBag.DelegateTo = ListOfPotetionalUsers;
            //new
            if (ListOfPotetionalUsers.Count() == 0)
            {
                ViewBag.CheckIfDelegationListIsNull = 0;
            }

            //end
            return View();
        }
        [HttpPost]
        public JsonResult CreateNormalUserVacation(Request request, string ReqSubType, string DurationFrom, string DurationTo, string HalfDayVacationType, string DelegateTo)
        {
            string message;

            type = (string)(Session["UserType"]);
            Name = (string)(Session["UserName"]);
            List<DateTime> days = new List<DateTime>();
            DateTime backwork;
            decimal ReqDuration;
            var departmentName = (from item in db.accountTBs where item.EmpName == Name select item.DepartmentName).SingleOrDefault();
            var SubdepartmentName = (from item in db.accountTBs where item.EmpName == Name select item.SubDepartmentName).SingleOrDefault();
            var DepartmentSenior = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepSenior).SingleOrDefault();
            var teamleader = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepBackupTeamLeader).SingleOrDefault();
            var Supervisor = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepTeamLeader).SingleOrDefault();
            var SupervisorBackup = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepBackupSupervisor).SingleOrDefault();
            var DuputyManager = (from p in db.DepartmentTBs where p.DeptName == departmentName select p.DeptBackupManager).SingleOrDefault();
            var manager = (from p in db.DepartmentTBs where p.DeptName == departmentName select p.DeptManager).SingleOrDefault();
            var arabicusername = (from p in db.accountTBs where p.EmpName == Name select p.ArabicName).SingleOrDefault();
            var RemainingAnnualVacation = (from p in db.accountTBs where p.EmpName == Name select p.AnnualVac).SingleOrDefault();
            var RemainingCasualVacation = (from p in db.accountTBs where p.EmpName == Name select p.CasualVac).SingleOrDefault();
            var EMPCODE = (from p in db.accountTBs where p.EmpName == Name select p.EmpCode).SingleOrDefault();
            var HireDate = (from p in db.accountTBs where p.EmpName == Name select p.HireDate.Value).SingleOrDefault();
            var DateDiff = Convert.ToInt32((DateTime.Now.Date - HireDate).TotalDays);
            var UserMail = (from item in db.accountTBs where item.EmpName == Name select item.Email).SingleOrDefault();
            var SeniorEmail = (from item in db.accountTBs where item.EmpName == DepartmentSenior select item.Email).SingleOrDefault();
            var TeamLeaderMail = (from item in db.accountTBs where item.EmpName == teamleader select item.Email).SingleOrDefault();
            var SupervisorMail = (from item in db.accountTBs where item.EmpName == Supervisor select item.Email).SingleOrDefault();
            var BackupSupervisorMail = (from item in db.accountTBs where item.EmpName == SupervisorBackup select item.Email).SingleOrDefault();
            var DuputyManagerMail = (from item in db.accountTBs where item.EmpName == DuputyManager select item.Email).SingleOrDefault();
            var ManagerMail = (from item in db.accountTBs where item.EmpName == manager select item.Email).SingleOrDefault();
            CheckDays(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, ReqSubType, HalfDayVacationType, out days, out backwork, out ReqDuration);


            if (ReqSubType == "مرضية")
            {
                if (DateDiff >= 180)
                {
                    if (RemainingAnnualVacation > 0 && RemainingAnnualVacation >= ReqDuration / 4)
                    {
                        if (teamleader == null)
                        {
                            if (Supervisor == null)
                            {
                                bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                if (checkifitsvalidDates == true)
                                {
                                    message = "Please Check start/end vacation Date";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                                else if (checkifitsofficialholiday == true)
                                {
                                    message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }

                                else if (checkifitsalreadytakenholiday == true)
                                {
                                    message = "You Requested Start/End Date which is a Pending Holiday or already Approved Vacation";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }

                                else if (checkifitsalreadyexcuseday == true)
                                {
                                    message = "You Requested Start/End vacation Date which you requested Excuse in";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }

                                else if (backwork == DateTime.MinValue.Date)
                                {
                                    message = "Please Review Dates";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    request.ReqType = "إجازة";
                                    request.EmpType = type;
                                    request.ReqDate = DateTime.Now;
                                    if (DuputyManager == null)
                                    {
                                        request.ReqStatus = "PendingManagerApproval";
                                        request.MyBackupManager = null;
                                    }
                                    else
                                    {
                                        request.ReqStatus = "PendingDuputyManagerApproval";
                                        request.MyBackupManager = DuputyManager;
                                    }

                                    request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                    request.DurationTo = DateTime.Parse(DurationTo).Date;
                                    request.BacktoWorkDate = backwork;
                                    request.UserName = Name;
                                    request.EmployeeCode = EMPCODE;
                                    request.DepartmentName = SubdepartmentName;
                                    request.ManagmentName = departmentName;
                                    request.MyTeamLeader = teamleader;
                                    request.MyManager = manager;
                                    request.ArabicName = arabicusername;
                                    request.ReqDuration = ReqDuration;
                                    db.Requests.Add(request);
                                    db.SaveChanges();
                                    int rq_id = request.ID;
                                    decrease_vacation_count(rq_id);
                                    InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                    message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                    if (request.ReqStatus == "PendingManagerApproval")
                                    {
                                        SendMail_NormalUser_VacationRequested_For_Manager(Name, UserMail, manager, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                    }
                                    else
                                    {
                                        SendMail_NormalUser_VacationRequested_For_ManagerBackup(Name, UserMail, DuputyManager, ManagerMail, DuputyManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                    }
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                if (SupervisorBackup == null)
                                {
                                    bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                    bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                    bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                    bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                    if (checkifitsvalidDates == true)
                                    {
                                        message = "Please Check start/end vacation Date";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    else if (checkifitsofficialholiday == true)
                                    {
                                        message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }

                                    else if (checkifitsalreadytakenholiday == true)
                                    {
                                        message = "You Requested Start/End Date which is a Pending Holiday or already Approved Vacation";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }


                                    else if (checkifitsalreadyexcuseday == true)
                                    {
                                        message = "You Requested Start/End vacation Date which you requested Excuse in";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }

                                    else if (backwork == DateTime.MinValue.Date)
                                    {
                                        message = "Please Review Dates";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        request.ReqType = "إجازة";
                                        request.EmpType = type;
                                        request.ReqDate = DateTime.Now;
                                        if (DuputyManager != null)
                                        {

                                            request.MyBackupManager = DuputyManager;
                                        }
                                        request.ReqStatus = "PendingSupervisorApproval";
                                        request.MySupervisor = Supervisor;
                                        request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                        request.DurationTo = DateTime.Parse(DurationTo).Date;
                                        request.BacktoWorkDate = backwork;
                                        request.UserName = Name;
                                        request.EmployeeCode = EMPCODE;
                                        request.DepartmentName = SubdepartmentName;
                                        request.ManagmentName = departmentName;
                                        request.MyTeamLeader = teamleader;
                                        request.MyManager = manager;
                                        request.ArabicName = arabicusername;
                                        request.ReqDuration = ReqDuration;
                                        db.Requests.Add(request);
                                        db.SaveChanges();
                                        int rq_id = request.ID;
                                        decrease_vacation_count(rq_id);
                                        InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                        message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                        SendMail_NormalUser_VacationRequested_For_Supervisor(Name, UserMail, Supervisor, SupervisorMail, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                else
                                {
                                    bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                    bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                    bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                    bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                    if (checkifitsvalidDates == true)
                                    {
                                        message = "Please Check start/end vacation Date";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    else if (checkifitsofficialholiday == true)
                                    {
                                        message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }

                                    else if (checkifitsalreadytakenholiday == true)
                                    {
                                        message = "You Requested Start/End Date which is a Pending Holiday or already Approved Vacation";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }


                                    else if (checkifitsalreadyexcuseday == true)
                                    {
                                        message = "You Requested Start/End vacation Date which you requested Excuse in";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }

                                    else if (backwork == DateTime.MinValue.Date)
                                    {
                                        message = "Please Review Dates";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        request.ReqType = "إجازة";
                                        request.EmpType = type;
                                        request.ReqDate = DateTime.Now;
                                        if (DuputyManager != null)
                                        {

                                            request.MyBackupManager = DuputyManager;
                                        }
                                        request.ReqStatus = "PendingSupervisorBackupApproval";
                                        request.MySupervisor = Supervisor;
                                        request.MyBackupSupervisor = SupervisorBackup;
                                        request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                        request.DurationTo = DateTime.Parse(DurationTo).Date;
                                        request.BacktoWorkDate = backwork;
                                        request.UserName = Name;
                                        request.EmployeeCode = EMPCODE;
                                        request.DepartmentName = SubdepartmentName;
                                        request.ManagmentName = departmentName;
                                        request.MyTeamLeader = teamleader;
                                        request.MyManager = manager;
                                        request.ArabicName = arabicusername;
                                        request.ReqDuration = ReqDuration;
                                        db.Requests.Add(request);
                                        db.SaveChanges();
                                        int rq_id = request.ID;
                                        decrease_vacation_count(rq_id);
                                        InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                        message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                        SendMail_NormalUser_VacationRequested_For_SupervisorBackup(Name, UserMail, SupervisorBackup, BackupSupervisorMail, ManagerMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }

                        else
                        {
                            bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                            bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                            bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                            bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                            if (checkifitsvalidDates == true)
                            {
                                message = "Please Check start/end vacation Date";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                            if (checkifitsofficialholiday == true)
                            {
                                message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }

                            else if (checkifitsalreadytakenholiday == true)
                            {
                                message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }


                            else if (checkifitsalreadyexcuseday == true)
                            {
                                message = "You Requested Start/End vacation Date which you requested Excuse in";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }

                            else if (backwork == DateTime.MinValue.Date)
                            {
                                message = "Please Review Dates";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                request.ReqType = "إجازة";
                                request.EmpType = type;
                                request.ReqDate = DateTime.Now;
                                if (DepartmentSenior == null)
                                {
                                    request.ReqStatus = "PendingTeamLeaderApproval";
                                    request.MyTeamLeader = teamleader;
                                }
                                else
                                {
                                    request.ReqStatus = "PendingSeniorApproval";
                                    request.MyTeamLeader = teamleader;
                                    request.MyBackupTeamLeader = DepartmentSenior;
                                }
                                if (DuputyManager != null)
                                {

                                    request.MyBackupManager = DuputyManager;
                                }
                                if (SupervisorBackup != null)
                                {
                                    request.MySupervisor = Supervisor;
                                    request.MyBackupSupervisor = SupervisorBackup;
                                }
                                else
                                {
                                    request.MySupervisor = Supervisor;
                                }
                                request.UserName = Name;
                                request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                request.DurationTo = DateTime.Parse(DurationTo).Date;
                                request.BacktoWorkDate = backwork;
                                request.EmployeeCode = EMPCODE;
                                request.ManagmentName = departmentName;
                                request.MyManager = manager;
                                request.ArabicName = arabicusername;
                                request.ReqDuration = ReqDuration;
                                db.Requests.Add(request);
                                db.SaveChanges();
                                int rq_id = request.ID;
                                decrease_vacation_count(rq_id);
                                InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                if (request.ReqStatus == "PendingTeamLeaderApproval")
                                {
                                    SendMail_NormalUser_VacationRequested_For_TeamLeader(Name, UserMail, teamleader, TeamLeaderMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                }
                                else
                                {
                                    SendMail_NormalUser_VacationRequested_For_TeamLeaderBackup(Name, UserMail, DepartmentSenior, SeniorEmail, TeamLeaderMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                }
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }

                    else
                    {
                        message = "You have no remaining days !";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                }

                else
                {
                    message = "You have not exceeded 6 months !";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }




            else if (ReqSubType == "عارضه")
            {

                if (DateDiff >= 180)
                {
                    var LastVacation = (from item in db.accountTBs where item.EmpName == Name select item.LastCasualVacation).SingleOrDefault();
                    var Excceded = (from item in db.accountTBs where item.EmpName == Name select item.MaximumNumberofCasualExceed).SingleOrDefault();
                    DateTime EndcasualVacation;
                    if (LastVacation == null)
                    {
                        if (ReqDuration <= 2)
                        {
                            if (Excceded == "false")
                            {
                                if (RemainingCasualVacation > 0 && RemainingCasualVacation >= ReqDuration)
                                {
                                    if (teamleader == null)
                                    {
                                        if (Supervisor == null)
                                        {
                                            bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                            bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                            bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                            bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                            if (checkifitsvalidDates == true)
                                            {
                                                message = "Please Check start/end vacation Date";
                                                return Json(message, JsonRequestBehavior.AllowGet);
                                            }
                                            else if (checkifitsofficialholiday == true)
                                            {
                                                message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                                return Json(message, JsonRequestBehavior.AllowGet);
                                            }

                                            else if (checkifitsalreadytakenholiday == true)
                                            {
                                                message = "You Requested Start/End Date which is a Pending Holiday or already Approved Vacation";
                                                return Json(message, JsonRequestBehavior.AllowGet);
                                            }


                                            else if (checkifitsalreadyexcuseday == true)
                                            {
                                                message = "You Requested Start/End vacation Date which you requested Excuse in";
                                                return Json(message, JsonRequestBehavior.AllowGet);
                                            }

                                            else if (backwork == DateTime.MinValue.Date)
                                            {
                                                message = "Please Review Dates";
                                                return Json(message, JsonRequestBehavior.AllowGet);
                                            }
                                            else
                                            {
                                                request.ReqType = "إجازة";
                                                request.EmpType = type;
                                                request.ReqDate = DateTime.Now;
                                                if (DuputyManager == null)
                                                {
                                                    request.ReqStatus = "PendingManagerApproval";
                                                    request.MyBackupManager = null;
                                                }
                                                else
                                                {
                                                    request.ReqStatus = "PendingDuputyManagerApproval";
                                                    request.MyBackupManager = DuputyManager;
                                                }
                                                request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                                request.DurationTo = DateTime.Parse(DurationTo).Date;
                                                request.BacktoWorkDate = backwork;
                                                request.UserName = Name;
                                                request.EmployeeCode = EMPCODE;
                                                request.DepartmentName = SubdepartmentName;
                                                request.ManagmentName = departmentName;
                                                request.MyTeamLeader = teamleader;
                                                request.MyManager = manager;
                                                request.ArabicName = arabicusername;
                                                request.ReqDuration = ReqDuration;
                                                db.Requests.Add(request);
                                                db.SaveChanges();
                                                int rq_id = request.ID;
                                                decrease_vacation_count(rq_id);
                                                InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                                message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                                if (request.ReqStatus == "PendingManagerApproval")
                                                {
                                                    SendMail_NormalUser_VacationRequested_For_Manager(Name, UserMail, manager, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                                }
                                                else
                                                {
                                                    SendMail_NormalUser_VacationRequested_For_ManagerBackup(Name, UserMail, DuputyManager, ManagerMail, DuputyManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                                }
                                                return Json(message, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                        else
                                        {
                                            if (SupervisorBackup == null)
                                            {
                                                bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                                bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                                bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                                bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                                if (checkifitsvalidDates == true)
                                                {
                                                    message = "Please Check start/end vacation Date";
                                                    return Json(message, JsonRequestBehavior.AllowGet);
                                                }
                                                else if (checkifitsofficialholiday == true)
                                                {
                                                    message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                                    return Json(message, JsonRequestBehavior.AllowGet);
                                                }

                                                else if (checkifitsalreadytakenholiday == true)
                                                {
                                                    message = "You Requested Start/End Date which is a Pending Holiday or already Approved Vacation";
                                                    return Json(message, JsonRequestBehavior.AllowGet);
                                                }


                                                else if (checkifitsalreadyexcuseday == true)
                                                {
                                                    message = "You Requested Start/End vacation Date which you requested Excuse in";
                                                    return Json(message, JsonRequestBehavior.AllowGet);
                                                }

                                                else if (backwork == DateTime.MinValue.Date)
                                                {
                                                    message = "Please Review Dates";
                                                    return Json(message, JsonRequestBehavior.AllowGet);
                                                }
                                                else
                                                {
                                                    request.ReqType = "إجازة";
                                                    request.EmpType = type;
                                                    request.ReqDate = DateTime.Now;
                                                    request.ReqStatus = "PendingSupervisorApproval";
                                                    if (DuputyManager != null)
                                                    {

                                                        request.MyBackupManager = DuputyManager;
                                                    }
                                                    request.MySupervisor = Supervisor;
                                                    request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                                    request.DurationTo = DateTime.Parse(DurationTo).Date;
                                                    request.BacktoWorkDate = backwork;
                                                    request.UserName = Name;
                                                    request.EmployeeCode = EMPCODE;
                                                    request.DepartmentName = SubdepartmentName;
                                                    request.ManagmentName = departmentName;
                                                    request.MyTeamLeader = teamleader;
                                                    request.MyManager = manager;
                                                    request.ArabicName = arabicusername;
                                                    request.ReqDuration = ReqDuration;
                                                    db.Requests.Add(request);
                                                    db.SaveChanges();
                                                    int rq_id = request.ID;
                                                    decrease_vacation_count(rq_id);
                                                    InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                                    message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                                    SendMail_NormalUser_VacationRequested_For_Supervisor(Name, UserMail, Supervisor, SupervisorMail, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                                    return Json(message, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                            else
                                            {
                                                bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                                bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                                bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                                bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                                if (checkifitsvalidDates == true)
                                                {
                                                    message = "Please Check start/end vacation Date";
                                                    return Json(message, JsonRequestBehavior.AllowGet);
                                                }
                                                else if (checkifitsofficialholiday == true)
                                                {
                                                    message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                                    return Json(message, JsonRequestBehavior.AllowGet);
                                                }

                                                else if (checkifitsalreadytakenholiday == true)
                                                {
                                                    message = "You Requested Start/End Date which is a Pending Holiday or already Approved Vacation";
                                                    return Json(message, JsonRequestBehavior.AllowGet);
                                                }


                                                else if (checkifitsalreadyexcuseday == true)
                                                {
                                                    message = "You Requested Start/End vacation Date which you requested Excuse in";
                                                    return Json(message, JsonRequestBehavior.AllowGet);
                                                }

                                                else if (backwork == DateTime.MinValue.Date)
                                                {
                                                    message = "Please Review Dates";
                                                    return Json(message, JsonRequestBehavior.AllowGet);
                                                }
                                                else
                                                {
                                                    request.ReqType = "إجازة";
                                                    request.EmpType = type;
                                                    request.ReqDate = DateTime.Now;
                                                    request.ReqStatus = "PendingSupervisorBackupApproval";
                                                    if (DuputyManager != null)
                                                    {

                                                        request.MyBackupManager = DuputyManager;
                                                    }
                                                    request.MySupervisor = Supervisor;
                                                    request.MyBackupSupervisor = SupervisorBackup;
                                                    request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                                    request.DurationTo = DateTime.Parse(DurationTo).Date;
                                                    request.BacktoWorkDate = backwork;
                                                    request.UserName = Name;
                                                    request.EmployeeCode = EMPCODE;
                                                    request.DepartmentName = SubdepartmentName;
                                                    request.ManagmentName = departmentName;
                                                    request.MyTeamLeader = teamleader;
                                                    request.MyManager = manager;
                                                    request.ArabicName = arabicusername;
                                                    request.ReqDuration = ReqDuration;
                                                    db.Requests.Add(request);
                                                    db.SaveChanges();
                                                    int rq_id = request.ID;
                                                    decrease_vacation_count(rq_id);
                                                    InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                                    message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                                    SendMail_NormalUser_VacationRequested_For_SupervisorBackup(Name, UserMail, SupervisorBackup, BackupSupervisorMail, ManagerMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                                    return Json(message, JsonRequestBehavior.AllowGet);
                                                }
                                            }
                                        }
                                    }

                                    else
                                    {
                                        bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                        bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                        bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                        bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                        bool validcasual = CheckifItsValidCasualVacationRequest(DateTime.Parse(DurationFrom).Date, ReqDuration, Name, HalfDayVacationType, backwork);
                                        if (checkifitsvalidDates == true)
                                        {
                                            message = "Please Check start/end vacation Date";
                                            return Json(message, JsonRequestBehavior.AllowGet);
                                        }
                                        if (checkifitsofficialholiday == true)
                                        {
                                            message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                            return Json(message, JsonRequestBehavior.AllowGet);
                                        }

                                        else if (checkifitsalreadytakenholiday == true)
                                        {
                                            message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                                            return Json(message, JsonRequestBehavior.AllowGet);
                                        }


                                        else if (checkifitsalreadyexcuseday == true)
                                        {
                                            message = "You Requested Start/End vacation Date which you requested Excuse in";
                                            return Json(message, JsonRequestBehavior.AllowGet);
                                        }
                                        else if (validcasual == true)
                                        {
                                            message = "Please Review Casual Vacation Rules";
                                            return Json(message, JsonRequestBehavior.AllowGet);
                                        }
                                        else if (backwork == DateTime.MinValue.Date)
                                        {
                                            message = "Please Review Dates";
                                            return Json(message, JsonRequestBehavior.AllowGet);
                                        }
                                        else
                                        {
                                            request.ReqType = "إجازة";
                                            request.EmpType = type;
                                            request.ReqDate = DateTime.Now;
                                            if (DepartmentSenior == null)
                                            {
                                                request.ReqStatus = "PendingTeamLeaderApproval";
                                                request.MyTeamLeader = teamleader;
                                            }
                                            else
                                            {
                                                request.ReqStatus = "PendingSeniorApproval";
                                                request.MyTeamLeader = teamleader;
                                                request.MyBackupTeamLeader = DepartmentSenior;
                                            }
                                            if (DuputyManager != null)
                                            {

                                                request.MyBackupManager = DuputyManager;
                                            }
                                            if (SupervisorBackup != null)
                                            {
                                                request.MySupervisor = Supervisor;
                                                request.MyBackupSupervisor = SupervisorBackup;
                                            }
                                            else
                                            {
                                                request.MySupervisor = Supervisor;
                                            }
                                            request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                            request.DurationTo = DateTime.Parse(DurationTo).Date;
                                            request.BacktoWorkDate = backwork;
                                            request.UserName = Name;
                                            request.DepartmentName = SubdepartmentName;
                                            request.ManagmentName = departmentName;
                                            request.MyManager = manager;
                                            request.ArabicName = arabicusername;
                                            request.ReqDuration = ReqDuration;
                                            request.EmployeeCode = EMPCODE;
                                            db.Requests.Add(request);
                                            db.SaveChanges();

                                            int rq_id = request.ID;
                                            decrease_vacation_count(rq_id);

                                            InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                            message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                            if (request.ReqStatus == "PendingTeamLeaderApproval")
                                            {
                                                SendMail_NormalUser_VacationRequested_For_TeamLeader(Name, UserMail, teamleader, TeamLeaderMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                            }
                                            else
                                            {
                                                SendMail_NormalUser_VacationRequested_For_TeamLeaderBackup(Name, UserMail, DepartmentSenior, SeniorEmail, TeamLeaderMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                            }
                                            return Json(message, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                }

                                else
                                {
                                    message = "You Do Not Have Enough Casual Vacation";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                            }

                            else
                            {
                                message = "You Have Exceeded Casual Vacations Limit !";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            message = "Maximum Number of Casual Vacation is two days !";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        EndcasualVacation = LastVacation.Value.Date.AddDays(Convert.ToDouble(ReqDuration));
                    }









                    int DateDifference = Convert.ToInt32((EndcasualVacation.Date - LastVacation.Value.Date).TotalDays);
                    if (Excceded == "false")
                    {
                        if (DateDifference <= 2)
                        {
                            if (RemainingCasualVacation > 0 && RemainingCasualVacation >= ReqDuration)
                            {
                                if (teamleader == null)
                                {
                                    if (Supervisor == null)
                                    {
                                        bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                        bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                        bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                        bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                        if (checkifitsvalidDates == true)
                                        {
                                            message = "Please Check start/end vacation Date";
                                            return Json(message, JsonRequestBehavior.AllowGet);
                                        }
                                        else if (checkifitsofficialholiday == true)
                                        {
                                            message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                            return Json(message, JsonRequestBehavior.AllowGet);
                                        }

                                        else if (checkifitsalreadytakenholiday == true)
                                        {
                                            message = "You Requested Start/End Date which is a Pending Holiday or already Approved Vacation";
                                            return Json(message, JsonRequestBehavior.AllowGet);
                                        }


                                        else if (checkifitsalreadyexcuseday == true)
                                        {
                                            message = "You Requested Start/End vacation Date which you requested Excuse in";
                                            return Json(message, JsonRequestBehavior.AllowGet);
                                        }

                                        else if (backwork == DateTime.MinValue.Date)
                                        {
                                            message = "Please Review Dates";
                                            return Json(message, JsonRequestBehavior.AllowGet);
                                        }
                                        else
                                        {
                                            request.ReqType = "إجازة";
                                            request.EmpType = type;
                                            request.ReqDate = DateTime.Now;
                                            if (DuputyManager == null)
                                            {
                                                request.ReqStatus = "PendingManagerApproval";
                                                request.MyBackupManager = null;
                                            }
                                            else
                                            {
                                                request.ReqStatus = "PendingDuputyManagerApproval";
                                                request.MyBackupManager = DuputyManager;
                                            }
                                            request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                            request.DurationTo = DateTime.Parse(DurationTo).Date;
                                            request.BacktoWorkDate = backwork;
                                            request.UserName = Name;
                                            request.EmployeeCode = EMPCODE;
                                            request.DepartmentName = SubdepartmentName;
                                            request.ManagmentName = departmentName;
                                            request.MyTeamLeader = teamleader;
                                            request.MyManager = manager;
                                            request.ArabicName = arabicusername;
                                            request.ReqDuration = ReqDuration;
                                            db.Requests.Add(request);
                                            db.SaveChanges();
                                            int rq_id = request.ID;
                                            decrease_vacation_count(rq_id);
                                            InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                            message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                            if (request.ReqStatus == "PendingManagerApproval")
                                            {
                                                SendMail_NormalUser_VacationRequested_For_Manager(Name, UserMail, manager, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                            }
                                            else
                                            {
                                                SendMail_NormalUser_VacationRequested_For_ManagerBackup(Name, UserMail, DuputyManager, ManagerMail, DuputyManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                            }
                                            return Json(message, JsonRequestBehavior.AllowGet);
                                        }
                                    }
                                    else
                                    {
                                        if (SupervisorBackup == null)
                                        {
                                            bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                            bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                            bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                            bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                            if (checkifitsvalidDates == true)
                                            {
                                                message = "Please Check start/end vacation Date";
                                                return Json(message, JsonRequestBehavior.AllowGet);
                                            }
                                            else if (checkifitsofficialholiday == true)
                                            {
                                                message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                                return Json(message, JsonRequestBehavior.AllowGet);
                                            }

                                            else if (checkifitsalreadytakenholiday == true)
                                            {
                                                message = "You Requested Start/End Date which is a Pending Holiday or already Approved Vacation";
                                                return Json(message, JsonRequestBehavior.AllowGet);
                                            }


                                            else if (checkifitsalreadyexcuseday == true)
                                            {
                                                message = "You Requested Start/End vacation Date which you requested Excuse in";
                                                return Json(message, JsonRequestBehavior.AllowGet);
                                            }

                                            else if (backwork == DateTime.MinValue.Date)
                                            {
                                                message = "Please Review Dates";
                                                return Json(message, JsonRequestBehavior.AllowGet);
                                            }
                                            else
                                            {
                                                request.ReqType = "إجازة";
                                                request.EmpType = type;
                                                request.ReqDate = DateTime.Now;
                                                request.ReqStatus = "PendingSupervisorApproval";
                                                request.MySupervisor = Supervisor;
                                                if (DuputyManager != null)
                                                {

                                                    request.MyBackupManager = DuputyManager;
                                                }
                                                request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                                request.DurationTo = DateTime.Parse(DurationTo).Date;
                                                request.BacktoWorkDate = backwork;
                                                request.UserName = Name;
                                                request.EmployeeCode = EMPCODE;
                                                request.DepartmentName = SubdepartmentName;
                                                request.ManagmentName = departmentName;
                                                request.MyTeamLeader = teamleader;
                                                request.MyManager = manager;
                                                request.ArabicName = arabicusername;
                                                request.ReqDuration = ReqDuration;
                                                db.Requests.Add(request);
                                                db.SaveChanges();
                                                int rq_id = request.ID;
                                                decrease_vacation_count(rq_id);
                                                InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                                message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                                SendMail_NormalUser_VacationRequested_For_Supervisor(Name, UserMail, Supervisor, SupervisorMail, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                                return Json(message, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                        else
                                        {
                                            bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                            bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                            bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                            bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                            if (checkifitsvalidDates == true)
                                            {
                                                message = "Please Check start/end vacation Date";
                                                return Json(message, JsonRequestBehavior.AllowGet);
                                            }
                                            else if (checkifitsofficialholiday == true)
                                            {
                                                message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                                return Json(message, JsonRequestBehavior.AllowGet);
                                            }

                                            else if (checkifitsalreadytakenholiday == true)
                                            {
                                                message = "You Requested Start/End Date which is a Pending Holiday or already Approved Vacation";
                                                return Json(message, JsonRequestBehavior.AllowGet);
                                            }


                                            else if (checkifitsalreadyexcuseday == true)
                                            {
                                                message = "You Requested Start/End vacation Date which you requested Excuse in";
                                                return Json(message, JsonRequestBehavior.AllowGet);
                                            }

                                            else if (backwork == DateTime.MinValue.Date)
                                            {
                                                message = "Please Review Dates";
                                                return Json(message, JsonRequestBehavior.AllowGet);
                                            }
                                            else
                                            {
                                                request.ReqType = "إجازة";
                                                request.EmpType = type;
                                                request.ReqDate = DateTime.Now;
                                                request.ReqStatus = "PendingSupervisorBackupApproval";
                                                request.MySupervisor = Supervisor;
                                                request.MyBackupSupervisor = SupervisorBackup;
                                                if (DuputyManager != null)
                                                {

                                                    request.MyBackupManager = DuputyManager;
                                                }
                                                request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                                request.DurationTo = DateTime.Parse(DurationTo).Date;
                                                request.BacktoWorkDate = backwork;
                                                request.UserName = Name;
                                                request.EmployeeCode = EMPCODE;
                                                request.DepartmentName = SubdepartmentName;
                                                request.ManagmentName = departmentName;
                                                request.MyTeamLeader = teamleader;
                                                request.MyManager = manager;
                                                request.ArabicName = arabicusername;
                                                request.ReqDuration = ReqDuration;
                                                db.Requests.Add(request);
                                                db.SaveChanges();
                                                int rq_id = request.ID;
                                                decrease_vacation_count(rq_id);
                                                InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                                message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                                SendMail_NormalUser_VacationRequested_For_SupervisorBackup(Name, UserMail, SupervisorBackup, BackupSupervisorMail, ManagerMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                                return Json(message, JsonRequestBehavior.AllowGet);
                                            }
                                        }
                                    }
                                }

                                else
                                {
                                    bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                    bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                    bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                    bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                    bool validcasual = CheckifItsValidCasualVacationRequest(DateTime.Parse(DurationFrom).Date, ReqDuration, Name, HalfDayVacationType, backwork);
                                    if (checkifitsvalidDates == true)
                                    {
                                        message = "Please Check start/end vacation Date";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    if (checkifitsofficialholiday == true)
                                    {
                                        message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }

                                    else if (checkifitsalreadytakenholiday == true)
                                    {
                                        message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }


                                    else if (checkifitsalreadyexcuseday == true)
                                    {
                                        message = "You Requested Start/End vacation Date which you requested Excuse in";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    //else if (validcasual == true)
                                    //{
                                    //    message = "Please check Casual vacation Rules";
                                    //    return Json(message, JsonRequestBehavior.AllowGet);
                                    //}
                                    else if (backwork == DateTime.MinValue.Date)
                                    {
                                        message = "Please Review Dates";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        request.ReqType = "إجازة";
                                        request.EmpType = type;
                                        request.ReqDate = DateTime.Now;
                                        if (DepartmentSenior == null)
                                        {
                                            request.ReqStatus = "PendingTeamLeaderApproval";
                                            request.MyTeamLeader = teamleader;
                                        }
                                        else
                                        {
                                            request.ReqStatus = "PendingSeniorApproval";
                                            request.MyTeamLeader = teamleader;
                                            request.MyBackupTeamLeader = DepartmentSenior;
                                        }
                                        if (DuputyManager != null)
                                        {

                                            request.MyBackupManager = DuputyManager;
                                        }
                                        if (SupervisorBackup != null)
                                        {
                                            request.MySupervisor = Supervisor;
                                            request.MyBackupSupervisor = SupervisorBackup;
                                        }
                                        else
                                        {
                                            request.MySupervisor = Supervisor;
                                        }
                                        request.UserName = Name;
                                        request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                        request.DurationTo = DateTime.Parse(DurationTo).Date;
                                        request.BacktoWorkDate = backwork;
                                        request.DepartmentName = SubdepartmentName;
                                        request.ManagmentName = departmentName;
                                        request.MyManager = manager;
                                        request.ArabicName = arabicusername;
                                        request.ReqDuration = ReqDuration;
                                        request.EmployeeCode = EMPCODE;
                                        db.Requests.Add(request);
                                        db.SaveChanges();
                                        int rq_id = request.ID;
                                        decrease_vacation_count(rq_id);
                                        InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                        message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                        if (request.ReqStatus == "PendingTeamLeaderApproval")
                                        {
                                            SendMail_NormalUser_VacationRequested_For_TeamLeader(Name, UserMail, teamleader, TeamLeaderMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                        }
                                        else
                                        {
                                            SendMail_NormalUser_VacationRequested_For_TeamLeaderBackup(Name, UserMail, DepartmentSenior, SeniorEmail, TeamLeaderMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                        }
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }

                            else
                            {
                                message = "You Do Not Have Enough Casual Vacation";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            message = "Maximum Casual Vacation is Two Days ";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                    }

                    else
                    {
                        message = "You Have Exceeded Number of Casual Vacations ";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }

                }

                else
                {
                    message = "You have not exceeded 6 months !";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }

            else if (ReqSubType == "عارضه - أخري")
            {

                if (DateDiff >= 180)
                {
                    if (RemainingAnnualVacation > 0 && RemainingAnnualVacation >= ReqDuration)
                    {
                        if (teamleader == null)
                        {
                            if (Supervisor == null)
                            {
                                bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                if (checkifitsvalidDates == true)
                                {
                                    message = "Please Check start/end vacation Date";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                                else if (checkifitsofficialholiday == true)
                                {
                                    message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }

                                else if (checkifitsalreadytakenholiday == true)
                                {
                                    message = "You Requested Start/End Date which is a Pending Holiday or already Approved Vacation";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }


                                else if (checkifitsalreadyexcuseday == true)
                                {
                                    message = "You Requested Start/End vacation Date which you requested Excuse in";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }

                                else if (backwork == DateTime.MinValue.Date)
                                {
                                    message = "Please Review Dates";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    request.ReqType = "إجازة";
                                    request.EmpType = type;
                                    request.ReqDate = DateTime.Now;
                                    if (DuputyManager == null)
                                    {
                                        request.ReqStatus = "PendingManagerApproval";
                                        request.MyBackupManager = null;
                                    }
                                    else
                                    {
                                        request.ReqStatus = "PendingDuputyManagerApproval";
                                        request.MyBackupManager = DuputyManager;
                                    }
                                    request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                    request.DurationTo = DateTime.Parse(DurationTo).Date;
                                    request.BacktoWorkDate = backwork;
                                    request.UserName = Name;
                                    request.EmployeeCode = EMPCODE;
                                    request.DepartmentName = SubdepartmentName;
                                    request.ManagmentName = departmentName;
                                    request.MyTeamLeader = teamleader;
                                    request.MyManager = manager;
                                    request.ArabicName = arabicusername;
                                    request.ReqDuration = ReqDuration;
                                    db.Requests.Add(request);
                                    db.SaveChanges();
                                    int rq_id = request.ID;
                                    decrease_vacation_count(rq_id);
                                    InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                    message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                    if (request.ReqStatus == "PendingManagerApproval")
                                    {
                                        SendMail_NormalUser_VacationRequested_For_Manager(Name, UserMail, manager, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                    }
                                    else
                                    {
                                        SendMail_NormalUser_VacationRequested_For_ManagerBackup(Name, UserMail, DuputyManager, ManagerMail, DuputyManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                    }
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                if (SupervisorBackup == null)
                                {
                                    bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                    bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                    bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                    bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                    if (checkifitsvalidDates == true)
                                    {
                                        message = "Please Check start/end vacation Date";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    else if (checkifitsofficialholiday == true)
                                    {
                                        message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }

                                    else if (checkifitsalreadytakenholiday == true)
                                    {
                                        message = "You Requested Start/End Date which is a Pending Holiday or already Approved Vacation";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }


                                    else if (checkifitsalreadyexcuseday == true)
                                    {
                                        message = "You Requested Start/End vacation Date which you requested Excuse in";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }

                                    else if (backwork == DateTime.MinValue.Date)
                                    {
                                        message = "Please Review Dates";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        request.ReqType = "إجازة";
                                        request.EmpType = type;
                                        request.ReqDate = DateTime.Now;
                                        request.ReqStatus = "PendingSupervisorApproval";
                                        request.MySupervisor = Supervisor;
                                        if (DuputyManager != null)
                                        {

                                            request.MyBackupManager = DuputyManager;
                                        }
                                        request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                        request.DurationTo = DateTime.Parse(DurationTo).Date;
                                        request.BacktoWorkDate = backwork;
                                        request.UserName = Name;
                                        request.EmployeeCode = EMPCODE;
                                        request.DepartmentName = SubdepartmentName;
                                        request.ManagmentName = departmentName;
                                        request.MyTeamLeader = teamleader;
                                        request.MyManager = manager;
                                        request.ArabicName = arabicusername;
                                        request.ReqDuration = ReqDuration;
                                        db.Requests.Add(request);
                                        db.SaveChanges();
                                        int rq_id = request.ID;
                                        decrease_vacation_count(rq_id);
                                        InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                        message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                        SendMail_NormalUser_VacationRequested_For_Supervisor(Name, UserMail, Supervisor, SupervisorMail, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                else
                                {
                                    bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                    bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                    bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                    bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                    if (checkifitsvalidDates == true)
                                    {
                                        message = "Please Check start/end vacation Date";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    else if (checkifitsofficialholiday == true)
                                    {
                                        message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }

                                    else if (checkifitsalreadytakenholiday == true)
                                    {
                                        message = "You Requested Start/End Date which is a Pending Holiday or already Approved Vacation";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }


                                    else if (checkifitsalreadyexcuseday == true)
                                    {
                                        message = "You Requested Start/End vacation Date which you requested Excuse in";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }

                                    else if (backwork == DateTime.MinValue.Date)
                                    {
                                        message = "Please Review Dates";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        request.ReqType = "إجازة";
                                        request.EmpType = type;
                                        request.ReqDate = DateTime.Now;
                                        request.ReqStatus = "PendingSupervisorBackupApproval";
                                        request.MySupervisor = Supervisor;
                                        request.MyBackupSupervisor = SupervisorBackup;
                                        if (DuputyManager != null)
                                        {
                                            request.MyBackupManager = DuputyManager;
                                        }
                                        request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                        request.DurationTo = DateTime.Parse(DurationTo).Date;
                                        request.BacktoWorkDate = backwork;
                                        request.UserName = Name;
                                        request.EmployeeCode = EMPCODE;
                                        request.DepartmentName = SubdepartmentName;
                                        request.ManagmentName = departmentName;
                                        request.MyTeamLeader = teamleader;
                                        request.MyManager = manager;
                                        request.ArabicName = arabicusername;
                                        request.ReqDuration = ReqDuration;
                                        db.Requests.Add(request);
                                        db.SaveChanges();
                                        int rq_id = request.ID;
                                        decrease_vacation_count(rq_id);
                                        InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                        message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                        SendMail_NormalUser_VacationRequested_For_SupervisorBackup(Name, UserMail, SupervisorBackup, BackupSupervisorMail, ManagerMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                        else
                        {
                            bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                            bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                            bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                            bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                            bool validcasual = CheckifItsValidCasualVacationRequest(DateTime.Parse(DurationFrom).Date, ReqDuration, Name, HalfDayVacationType, backwork);
                            if (checkifitsvalidDates == true)
                            {
                                message = "Please Check start/end vacation Date";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                            if (checkifitsofficialholiday == true)
                            {
                                message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }

                            else if (checkifitsalreadytakenholiday == true)
                            {
                                message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }


                            else if (checkifitsalreadyexcuseday == true)
                            {
                                message = "You Requested Start/End vacation Date which you requested Excuse in";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                            else if (validcasual == true)
                            {
                                message = "Please Review Casual Vacation Rules";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                            else if (backwork == DateTime.MinValue.Date)
                            {
                                message = "Please Review Dates";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                            else if (ReqDuration <= RemainingCasualVacation)
                            {
                                message = "You still have enough casual vacations !";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                request.ReqType = "إجازة";
                                request.EmpType = type;
                                request.ReqDate = DateTime.Now;
                                if (DepartmentSenior == null)
                                {
                                    request.ReqStatus = "PendingTeamLeaderApproval";
                                    request.MyTeamLeader = teamleader;
                                }
                                else
                                {
                                    request.ReqStatus = "PendingSeniorApproval";
                                    request.MyTeamLeader = teamleader;
                                    request.MyBackupTeamLeader = DepartmentSenior;
                                }
                                if (DuputyManager != null)
                                {
                                    request.MyBackupManager = DuputyManager;
                                }
                                if (SupervisorBackup != null)
                                {
                                    request.MySupervisor = Supervisor;
                                    request.MyBackupSupervisor = SupervisorBackup;
                                }
                                else
                                {
                                    request.MySupervisor = Supervisor;

                                }
                                request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                request.DurationTo = DateTime.Parse(DurationTo).Date;
                                request.BacktoWorkDate = backwork;
                                request.UserName = Name;
                                request.DepartmentName = SubdepartmentName;
                                request.ManagmentName = departmentName;
                                request.MyManager = manager;
                                request.ArabicName = arabicusername;
                                request.ReqDuration = ReqDuration;
                                request.EmployeeCode = EMPCODE;
                                db.Requests.Add(request);
                                db.SaveChanges();
                                int rq_id = request.ID;
                                decrease_vacation_count(rq_id);
                                InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                if (request.ReqStatus == "PendingTeamLeaderApproval")
                                {
                                    SendMail_NormalUser_VacationRequested_For_TeamLeader(Name, UserMail, teamleader, TeamLeaderMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                }
                                else
                                {
                                    SendMail_NormalUser_VacationRequested_For_TeamLeaderBackup(Name, UserMail, DepartmentSenior, SeniorEmail, TeamLeaderMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                }
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    else
                    {
                        message = "You Have No Annual Vacations";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                }

                else
                {
                    message = "You Have not exceeded 6 months !";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }

            else if (ReqSubType == "اعتيادية")
            {

                if (DateDiff >= 180)
                {
                    if (RemainingAnnualVacation > 0 && RemainingAnnualVacation >= ReqDuration)
                    {
                        if (teamleader == null)
                        {
                            if (Supervisor == null)
                            {
                                bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                if (checkifitsvalidDates == true)
                                {
                                    message = "Please Check start/end vacation Date";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                                else if (checkifitsofficialholiday == true)
                                {
                                    message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }

                                else if (checkifitsalreadytakenholiday == true)
                                {
                                    message = "You Requested Start/End Date which is a Pending Holiday or already Approved Vacation";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }


                                else if (checkifitsalreadyexcuseday == true)
                                {
                                    message = "You Requested Start/End vacation Date which you requested Excuse in";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }

                                else if (backwork == DateTime.MinValue.Date)
                                {
                                    message = "Please Review Dates";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    request.ReqType = "إجازة";
                                    request.EmpType = type;
                                    request.ReqDate = DateTime.Now;
                                    if (DuputyManager == null)
                                    {
                                        request.ReqStatus = "PendingManagerApproval";
                                        request.MyBackupManager = null;
                                    }
                                    else
                                    {
                                        request.ReqStatus = "PendingDuputyManagerApproval";
                                        request.MyBackupManager = DuputyManager;
                                    }
                                    request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                    request.DurationTo = DateTime.Parse(DurationTo).Date;
                                    request.BacktoWorkDate = backwork;
                                    request.UserName = Name;
                                    request.EmployeeCode = EMPCODE;
                                    request.DepartmentName = SubdepartmentName;
                                    request.ManagmentName = departmentName;
                                    request.MyTeamLeader = teamleader;
                                    request.MyManager = manager;
                                    request.ArabicName = arabicusername;
                                    request.ReqDuration = ReqDuration;
                                    db.Requests.Add(request);
                                    db.SaveChanges();
                                    int rq_id = request.ID;
                                    decrease_vacation_count(rq_id);
                                    InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                    message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                    if (request.ReqStatus == "PendingManagerApproval")
                                    {
                                        SendMail_NormalUser_VacationRequested_For_Manager(Name, UserMail, manager, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                    }
                                    else
                                    {
                                        SendMail_NormalUser_VacationRequested_For_ManagerBackup(Name, UserMail, DuputyManager, ManagerMail, DuputyManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                    }
                                    //return Json(message, JsonRequestBehavior.AllowGet);
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                if (SupervisorBackup == null)
                                {
                                    bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                    bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                    bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                    bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                    if (checkifitsvalidDates == true)
                                    {
                                        message = "Please Check start/end vacation Date";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    else if (checkifitsofficialholiday == true)
                                    {
                                        message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }

                                    else if (checkifitsalreadytakenholiday == true)
                                    {
                                        message = "You Requested Start/End Date which is a Pending Holiday or already Approved Vacation";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }


                                    else if (checkifitsalreadyexcuseday == true)
                                    {
                                        message = "You Requested Start/End vacation Date which you requested Excuse in";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }

                                    else if (backwork == DateTime.MinValue.Date)
                                    {
                                        message = "Please Review Dates";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        request.ReqType = "إجازة";
                                        request.EmpType = type;
                                        request.ReqDate = DateTime.Now;
                                        request.ReqStatus = "PendingSupervisorApproval";
                                        request.MySupervisor = Supervisor;
                                        if (DuputyManager != null)
                                        {
                                            request.MyBackupManager = DuputyManager;
                                        }
                                        request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                        request.DurationTo = DateTime.Parse(DurationTo).Date;
                                        request.BacktoWorkDate = backwork;
                                        request.UserName = Name;
                                        request.EmployeeCode = EMPCODE;
                                        request.DepartmentName = SubdepartmentName;
                                        request.ManagmentName = departmentName;
                                        request.MyTeamLeader = teamleader;
                                        request.MyManager = manager;
                                        request.ArabicName = arabicusername;
                                        request.ReqDuration = ReqDuration;
                                        db.Requests.Add(request);
                                        db.SaveChanges();
                                        int rq_id = request.ID;
                                        decrease_vacation_count(rq_id);
                                        InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                        message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                        SendMail_NormalUser_VacationRequested_For_Supervisor(Name, UserMail, Supervisor, SupervisorMail, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                else
                                {
                                    bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                    bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                    bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                    bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                    if (checkifitsvalidDates == true)
                                    {
                                        message = "Please Check start/end vacation Date";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    else if (checkifitsofficialholiday == true)
                                    {
                                        message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }

                                    else if (checkifitsalreadytakenholiday == true)
                                    {
                                        message = "You Requested Start/End Date which is a Pending Holiday or already Approved Vacation";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }


                                    else if (checkifitsalreadyexcuseday == true)
                                    {
                                        message = "You Requested Start/End vacation Date which you requested Excuse in";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }

                                    else if (backwork == DateTime.MinValue.Date)
                                    {
                                        message = "Please Review Dates";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        request.ReqType = "إجازة";
                                        request.EmpType = type;
                                        request.ReqDate = DateTime.Now;
                                        request.ReqStatus = "PendingSupervisorBackupApproval";
                                        request.MySupervisor = Supervisor;
                                        request.MyBackupSupervisor = SupervisorBackup;
                                        if (DuputyManager != null)
                                        {
                                            request.MyBackupManager = DuputyManager;
                                        }
                                        request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                        request.DurationTo = DateTime.Parse(DurationTo).Date;
                                        request.BacktoWorkDate = backwork;
                                        request.UserName = Name;
                                        request.EmployeeCode = EMPCODE;
                                        request.DepartmentName = SubdepartmentName;
                                        request.ManagmentName = departmentName;
                                        request.MyTeamLeader = teamleader;
                                        request.MyManager = manager;
                                        request.ArabicName = arabicusername;
                                        request.ReqDuration = ReqDuration;
                                        db.Requests.Add(request);
                                        db.SaveChanges();
                                        int rq_id = request.ID;
                                        decrease_vacation_count(rq_id);
                                        InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                        message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                        SendMail_NormalUser_VacationRequested_For_SupervisorBackup(Name, UserMail, SupervisorBackup, BackupSupervisorMail, ManagerMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                        }
                        else
                        {
                            bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                            bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                            bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                            bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                            bool annualcheck = CheckifItsValidAnnualVacationRequest(DateTime.Parse(DurationFrom).Date);
                            if (checkifitsvalidDates == true)
                            {
                                message = "Please Check start/end vacation Date";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                            if (checkifitsofficialholiday == true)
                            {
                                message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }

                            else if (checkifitsalreadytakenholiday == true)
                            {
                                message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }


                            else if (checkifitsalreadyexcuseday == true)
                            {
                                message = "You Requested Start/End vacation Date which you requested Excuse in";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                            else if (annualcheck == true)
                            {
                                message = "Please check Annual vacation Rules";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                            else if (backwork == DateTime.MinValue.Date)
                            {
                                message = "Please Review Dates";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }

                            else
                            {
                                request.ReqType = "إجازة";
                                request.EmpType = type;
                                request.ReqDate = DateTime.Now;
                                if (DepartmentSenior == null)
                                {
                                    request.ReqStatus = "PendingTeamLeaderApproval";
                                    request.MyTeamLeader = teamleader;
                                }
                                else
                                {
                                    request.ReqStatus = "PendingSeniorApproval";
                                    request.MyTeamLeader = teamleader;
                                    request.MyBackupTeamLeader = DepartmentSenior;
                                }
                                if (DuputyManager != null)
                                {
                                    request.MyBackupManager = DuputyManager;
                                }
                                if (SupervisorBackup != null)
                                {
                                    request.MySupervisor = Supervisor;
                                    request.MyBackupSupervisor = SupervisorBackup;

                                }
                                else
                                {
                                    request.MySupervisor = Supervisor;
                                }
                                request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                request.DurationTo = DateTime.Parse(DurationTo).Date;
                                request.BacktoWorkDate = backwork;
                                request.UserName = Name;
                                request.DepartmentName = SubdepartmentName;
                                request.ManagmentName = departmentName;
                                request.MyManager = manager;
                                request.ArabicName = arabicusername;
                                request.ReqDuration = ReqDuration;
                                request.EmployeeCode = EMPCODE;
                                db.Requests.Add(request);
                                db.SaveChanges();
                                int rq_id = request.ID;
                                decrease_vacation_count(rq_id);
                                InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                if (request.ReqStatus == "PendingTeamLeaderApproval")
                                {
                                    SendMail_NormalUser_VacationRequested_For_TeamLeader(Name, UserMail, teamleader, TeamLeaderMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                }
                                else
                                {
                                    SendMail_NormalUser_VacationRequested_For_TeamLeaderBackup(Name, UserMail, DepartmentSenior, SeniorEmail, TeamLeaderMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                }
                                message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                    else
                    {
                        message = "You Have No Annual Vacations";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                }

                else
                {
                    message = "You Have not exceeded 6 months !";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }

            else
            {
                try
                {

                    if (teamleader == null)
                    {
                        if (Supervisor == null)
                        {
                            bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                            bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                            bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                            bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                            if (checkifitsvalidDates == true)
                            {
                                message = "Please Check start/end vacation Date";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                            else if (checkifitsofficialholiday == true)
                            {
                                message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }

                            else if (checkifitsalreadytakenholiday == true)
                            {
                                message = "You Requested Start/End Date which is a Pending Holiday or already Approved Vacation";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }


                            else if (checkifitsalreadyexcuseday == true)
                            {
                                message = "You Requested Start/End vacation Date which you requested Excuse in";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }

                            else if (backwork == DateTime.MinValue.Date)
                            {
                                message = "Please Review Dates";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                request.ReqType = "إجازة";
                                request.EmpType = type;
                                request.ReqDate = DateTime.Now;
                                if (DuputyManager == null)
                                {
                                    request.ReqStatus = "PendingManagerApproval";
                                    request.MyBackupManager = null;
                                }
                                else
                                {
                                    request.ReqStatus = "PendingDuputyManagerApproval";
                                    request.MyBackupManager = DuputyManager;
                                }
                                request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                request.DurationTo = DateTime.Parse(DurationTo).Date;
                                request.BacktoWorkDate = backwork;
                                request.UserName = Name;
                                request.EmployeeCode = EMPCODE;
                                request.DepartmentName = SubdepartmentName;
                                request.ManagmentName = departmentName;
                                request.MyTeamLeader = teamleader;
                                request.MyManager = manager;
                                request.ArabicName = arabicusername;
                                request.ReqDuration = ReqDuration;
                                db.Requests.Add(request);
                                db.SaveChanges();
                                InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                if (request.ReqStatus == "PendingManagerApproval")
                                {
                                    SendMail_NormalUser_VacationRequested_For_Manager(Name, UserMail, manager, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                }
                                else
                                {
                                    SendMail_NormalUser_VacationRequested_For_ManagerBackup(Name, UserMail, DuputyManager, ManagerMail, DuputyManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                }
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            if (SupervisorBackup == null)
                            {
                                bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                if (checkifitsvalidDates == true)
                                {
                                    message = "Please Check start/end vacation Date";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                                else if (checkifitsofficialholiday == true)
                                {
                                    message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }

                                else if (checkifitsalreadytakenholiday == true)
                                {
                                    message = "You Requested Start/End Date which is a Pending Holiday or already Approved Vacation";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }


                                else if (checkifitsalreadyexcuseday == true)
                                {
                                    message = "You Requested Start/End vacation Date which you requested Excuse in";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }

                                else if (backwork == DateTime.MinValue.Date)
                                {
                                    message = "Please Review Dates";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    request.ReqType = "إجازة";
                                    request.EmpType = type;
                                    request.ReqDate = DateTime.Now;
                                    request.ReqStatus = "PendingSupervisorApproval";
                                    request.MySupervisor = Supervisor;
                                    if (DuputyManager != null)
                                    {
                                        request.MyBackupManager = DuputyManager;
                                    }
                                    request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                    request.DurationTo = DateTime.Parse(DurationTo).Date;
                                    request.BacktoWorkDate = backwork;
                                    request.UserName = Name;
                                    request.EmployeeCode = EMPCODE;
                                    request.DepartmentName = SubdepartmentName;
                                    request.ManagmentName = departmentName;
                                    request.MyTeamLeader = teamleader;
                                    request.MyManager = manager;
                                    request.ArabicName = arabicusername;
                                    request.ReqDuration = ReqDuration;
                                    db.Requests.Add(request);
                                    db.SaveChanges();
                                    InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                    message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                    SendMail_NormalUser_VacationRequested_For_Supervisor(Name, UserMail, Supervisor, SupervisorMail, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                if (checkifitsvalidDates == true)
                                {
                                    message = "Please Check start/end vacation Date";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                                else if (checkifitsofficialholiday == true)
                                {
                                    message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }

                                else if (checkifitsalreadytakenholiday == true)
                                {
                                    message = "You Requested Start/End Date which is a Pending Holiday or already Approved Vacation";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }


                                else if (checkifitsalreadyexcuseday == true)
                                {
                                    message = "You Requested Start/End vacation Date which you requested Excuse in";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }

                                else if (backwork == DateTime.MinValue.Date)
                                {
                                    message = "Please Review Dates";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    request.ReqType = "إجازة";
                                    request.EmpType = type;
                                    request.ReqDate = DateTime.Now;
                                    request.ReqStatus = "PendingSupervisorBackupApproval";
                                    request.MySupervisor = Supervisor;
                                    request.MyBackupSupervisor = SupervisorBackup;
                                    if (DuputyManager != null)
                                    {
                                        request.MyBackupManager = DuputyManager;
                                    }
                                    request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                    request.DurationTo = DateTime.Parse(DurationTo).Date;
                                    request.BacktoWorkDate = backwork;
                                    request.UserName = Name;
                                    request.EmployeeCode = EMPCODE;
                                    request.DepartmentName = SubdepartmentName;
                                    request.ManagmentName = departmentName;
                                    request.MyTeamLeader = teamleader;
                                    request.MyManager = manager;
                                    request.ArabicName = arabicusername;
                                    request.ReqDuration = ReqDuration;
                                    db.Requests.Add(request);
                                    db.SaveChanges();
                                    InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                    message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                    SendMail_NormalUser_VacationRequested_For_SupervisorBackup(Name, UserMail, SupervisorBackup, BackupSupervisorMail, ManagerMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                    else
                    {
                        bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                        bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                        bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                        bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                        if (checkifitsvalidDates == true)
                        {
                            message = "Please Check start/end vacation Date";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        if (checkifitsofficialholiday == true)
                        {
                            message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                        else if (checkifitsalreadytakenholiday == true)
                        {
                            message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }


                        else if (checkifitsalreadyexcuseday == true)
                        {
                            message = "You Requested Start/End vacation Date which you requested Excuse in";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else if (backwork == DateTime.MinValue.Date)
                        {
                            message = "Please Review Dates";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            request.ReqType = "إجازة";
                            request.EmpType = type;
                            request.ReqDate = DateTime.Now;
                            request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                            request.DurationTo = DateTime.Parse(DurationTo).Date;
                            request.BacktoWorkDate = backwork;
                            if (DepartmentSenior == null)
                            {
                                request.ReqStatus = "PendingTeamLeaderApproval";
                                request.MyTeamLeader = teamleader;
                            }
                            else
                            {
                                request.ReqStatus = "PendingSeniorApproval";
                                request.MyBackupTeamLeader = DepartmentSenior;
                                request.MyTeamLeader = teamleader;
                            }
                            if (DuputyManager != null)
                            {
                                request.MyBackupManager = DuputyManager;
                            }
                            if (SupervisorBackup != null)
                            {
                                request.MySupervisor = Supervisor;
                                request.MyBackupSupervisor = SupervisorBackup;
                            }
                            else
                            {
                                request.MySupervisor = Supervisor;
                            }
                            request.UserName = Name;
                            request.DepartmentName = SubdepartmentName;
                            request.ManagmentName = departmentName;
                            request.MyManager = manager;
                            request.ArabicName = arabicusername;
                            request.ReqDuration = ReqDuration;
                            request.EmployeeCode = EMPCODE;
                            db.Requests.Add(request);
                            db.SaveChanges();
                            InsertOffdays(days, Name, ReqSubType, "PendingTeamLeaderApproval", ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                            message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                            if (request.ReqStatus == "PendingTeamLeaderApproval")
                            {
                                SendMail_NormalUser_VacationRequested_For_TeamLeader(Name, UserMail, teamleader, TeamLeaderMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                            }
                            else
                            {
                                SendMail_NormalUser_VacationRequested_For_TeamLeaderBackup(Name, UserMail, DepartmentSenior, SeniorEmail, TeamLeaderMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                            }
                            //return Json(message, JsonRequestBehavior.AllowGet);
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                    }
                }

                catch
                {
                    message = "There was problem Adding Your Request";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult CreateTeamLeaderVacation(Request request, string ReqSubType, string DurationFrom, string DurationTo, string HalfDayVacationType, string DelegateTo)
        {
            string message;
            type = (string)(Session["UserType"]);
            Name = (string)(Session["UserName"]);
            List<DateTime> days = new List<DateTime>();
            DateTime backwork;
            decimal ReqDuration;
            var departmentName = (from item in db.accountTBs where item.EmpName == Name select item.DepartmentName).SingleOrDefault();
            var SubdepartmentName = (from item in db.accountTBs where item.EmpName == Name select item.SubDepartmentName).SingleOrDefault();
            var manager = (from p in db.DepartmentTBs where p.DeptName == departmentName select p.DeptManager).SingleOrDefault();
            var Supervisor = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepTeamLeader).SingleOrDefault();
            var BackupSupervisor = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepBackupSupervisor).SingleOrDefault();
            var DuputyManager = (from p in db.DepartmentTBs where p.DeptName == departmentName select p.DeptBackupManager).SingleOrDefault();
            var arabicusername = (from p in db.accountTBs where p.EmpName == Name select p.ArabicName).SingleOrDefault();
            var RemainingAnnualVacation = (from p in db.accountTBs where p.EmpName == Name select p.AnnualVac).SingleOrDefault();
            var RemainingCasualVacation = (from p in db.accountTBs where p.EmpName == Name select p.CasualVac).SingleOrDefault();
            var SupervisorMail = (from item in db.accountTBs where item.EmpName == Supervisor select item.Email).SingleOrDefault();
            var BackupSupervisorMail = (from item in db.accountTBs where item.EmpName == BackupSupervisor select item.Email).SingleOrDefault();
            var EMPCODE = (from p in db.accountTBs where p.EmpName == Name select p.EmpCode).SingleOrDefault();
            var HireDate = (from p in db.accountTBs where p.EmpName == Name select p.HireDate.Value).SingleOrDefault();
            var DateDiff = Convert.ToInt32((DateTime.Now.Date - HireDate).TotalDays);
            var UserMail = (from item in db.accountTBs where item.EmpName == Name select item.Email).SingleOrDefault();
            var ManagerMail = (from item in db.accountTBs where item.EmpName == manager select item.Email).SingleOrDefault();
            var user = (from u in db.accountTBs where u.EmpName == Name select u).SingleOrDefault();
            CheckDays(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, ReqSubType, HalfDayVacationType, out days, out backwork, out ReqDuration);

            if (ReqSubType == "مرضية")
            {
                if (DateDiff >= 180)
                {
                    if (RemainingAnnualVacation > 0 && RemainingAnnualVacation >= ReqDuration / 4)
                    {
                        bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                        bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                        bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                        bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                        if (checkifitsvalidDates == true)
                        {
                            message = "Please Check start/end vacation Date";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        if (checkifitsofficialholiday == true)
                        {
                            message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                        else if (checkifitsalreadytakenholiday == true)
                        {
                            message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                        else if (checkifitsalreadyexcuseday == true)
                        {
                            message = "You Requested Start/End vacation Date which you requested Excuse in";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else if (backwork == DateTime.MinValue.Date)
                        {
                            message = "Please Review Dates";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            request.ReqType = "إجازة";
                            request.EmpType = type;
                            request.ReqDate = DateTime.Now;
                            if (BackupSupervisor == null)
                            {
                                request.ReqStatus = "PendingSupervisorApproval";
                                request.MySupervisor = Supervisor;

                            }
                            else
                            {
                                request.ReqStatus = "PendingSupervisorBackupApproval";
                                request.MySupervisor = Supervisor;
                                request.MyBackupSupervisor = BackupSupervisor;

                            }
                            if (DuputyManager != null)
                            {
                                request.MyBackupManager = DuputyManager;
                            }
                            request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                            request.DurationTo = DateTime.Parse(DurationTo).Date;
                            request.BacktoWorkDate = backwork;
                            request.UserName = Name;
                            request.DepartmentName = SubdepartmentName;
                            request.ManagmentName = departmentName;
                            request.MyManager = manager;
                            request.ArabicName = arabicusername;
                            request.ReqDuration = ReqDuration;
                            request.EmployeeCode = EMPCODE;
                            db.Requests.Add(request);
                            db.SaveChanges();
                            int rq_id = request.ID;
                            decrease_vacation_count(rq_id);
                            InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                            message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                            if (request.ReqStatus == "PendingSupervisorApproval")
                            {
                                SendMail_TeamLeader_VacationRequested_For_Supervisor(Name, UserMail, Supervisor, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                            }
                            else
                            {
                                SendMail_TeamLeader_VacationRequested_For_SupervisorBackup(Name, UserMail, BackupSupervisorMail, BackupSupervisorMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                            }
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                    }

                    else
                    {
                        message = "You have no remaining days !";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                }

                else
                {
                    message = "You have not exceeded 6 months !";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }



            else if (ReqSubType == "عارضه")
            {

                if (DateDiff >= 180)
                {
                    var LastVacation = (from item in db.accountTBs where item.EmpName == Name select item.LastCasualVacation).SingleOrDefault();
                    var Excceded = (from item in db.accountTBs where item.EmpName == Name select item.MaximumNumberofCasualExceed).SingleOrDefault();
                    DateTime EndcasualVacation;
                    if (LastVacation == null)
                    {
                        if (ReqDuration <= 2)
                        {

                            if (Excceded == "false")
                            {
                                if (RemainingCasualVacation > 0 && RemainingCasualVacation >= ReqDuration)
                                {
                                    bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                    bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                    bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                    bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                    bool validcasual = CheckifItsValidCasualVacationRequest(DateTime.Parse(DurationFrom).Date, ReqDuration, Name, HalfDayVacationType, backwork);
                                    if (checkifitsvalidDates == true)
                                    {
                                        message = "Please Check start/end vacation Date";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    if (checkifitsofficialholiday == true)
                                    {
                                        message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }

                                    else if (checkifitsalreadytakenholiday == true)
                                    {
                                        message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }


                                    else if (checkifitsalreadyexcuseday == true)
                                    {
                                        message = "You Requested Start/End vacation Date which you requested Excuse in";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    else if (validcasual == true)
                                    {
                                        message = "Please check Casual vacation Rules";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    else if (backwork == DateTime.MinValue.Date)
                                    {
                                        message = "Please Review Dates";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        request.ReqType = "إجازة";
                                        request.EmpType = type;
                                        request.ReqDate = DateTime.Now;
                                        if (BackupSupervisor == null)
                                        {
                                            request.ReqStatus = "PendingSupervisorApproval";
                                            request.MySupervisor = Supervisor;

                                        }
                                        else
                                        {
                                            request.ReqStatus = "PendingSupervisorBackupApproval";
                                            request.MySupervisor = Supervisor;
                                            request.MyBackupSupervisor = BackupSupervisor;
                                        }
                                        if (DuputyManager != null)
                                        {
                                            request.MyBackupManager = DuputyManager;
                                        }
                                        request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                        request.DurationTo = DateTime.Parse(DurationTo).Date;
                                        request.BacktoWorkDate = backwork;
                                        request.UserName = Name;
                                        request.DepartmentName = SubdepartmentName;
                                        request.ManagmentName = departmentName;
                                        request.MyManager = manager;
                                        request.ArabicName = arabicusername;
                                        request.ReqDuration = ReqDuration;
                                        request.EmployeeCode = EMPCODE;
                                        db.Requests.Add(request);
                                        db.SaveChanges();
                                        int rq_id = request.ID;
                                        decrease_vacation_count(rq_id);
                                        InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                        message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                        if (request.ReqStatus == "PendingSupervisorApproval")
                                        {
                                            SendMail_TeamLeader_VacationRequested_For_Supervisor(Name, UserMail, Supervisor, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                        }
                                        else
                                        {
                                            SendMail_TeamLeader_VacationRequested_For_SupervisorBackup(Name, UserMail, BackupSupervisorMail, BackupSupervisorMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                        }
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }

                                }

                                else
                                {
                                    message = "You Do Not Have Enough Casual Vacation";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                message = "You have exceeded Casual Vacations limit";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                        }

                        else
                        {
                            message = "Maximum Number of Casual Vacations is two days!";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                    }

                    else
                    {
                        EndcasualVacation = LastVacation.Value.Date.AddDays(Convert.ToDouble(ReqDuration));
                    }
                    int DateDifference = Convert.ToInt32((EndcasualVacation.Date - LastVacation.Value.Date).TotalDays);
                    if (Excceded == "false")
                    {
                        if (DateDifference <= 2)
                        {
                            if (RemainingCasualVacation > 0 && RemainingCasualVacation >= ReqDuration)
                            {
                                bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                bool validcasual = CheckifItsValidCasualVacationRequest(DateTime.Parse(DurationFrom).Date, ReqDuration, Name, HalfDayVacationType, backwork);
                                if (checkifitsvalidDates == true)
                                {
                                    message = "Please Check start/end vacation Date";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                                if (checkifitsofficialholiday == true)
                                {
                                    message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }

                                else if (checkifitsalreadytakenholiday == true)
                                {
                                    message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }


                                else if (checkifitsalreadyexcuseday == true)
                                {
                                    message = "You Requested Start/End vacation Date which you requested Excuse in";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                                else if (validcasual == true)
                                {
                                    message = "Please check Casual vacation Rules";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                                else if (backwork == DateTime.MinValue.Date)
                                {
                                    message = "Please Review Dates";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }

                                else
                                {
                                    request.ReqType = "إجازة";
                                    request.EmpType = type;
                                    request.ReqDate = DateTime.Now;
                                    if (BackupSupervisor == null)
                                    {
                                        request.ReqStatus = "PendingSupervisorApproval";
                                        request.MySupervisor = Supervisor;

                                    }
                                    else
                                    {
                                        request.ReqStatus = "PendingSupervisorBackupApproval";
                                        request.MySupervisor = Supervisor;
                                        request.MyBackupSupervisor = BackupSupervisor;
                                    }
                                    if (DuputyManager != null)
                                    {
                                        request.MyBackupManager = DuputyManager;
                                    }
                                    request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                    request.DurationTo = DateTime.Parse(DurationTo).Date;
                                    request.BacktoWorkDate = backwork;
                                    request.UserName = Name;
                                    request.DepartmentName = SubdepartmentName;
                                    request.ManagmentName = departmentName;
                                    request.MyManager = manager;
                                    request.ReqDuration = ReqDuration;
                                    request.EmployeeCode = EMPCODE;
                                    request.ArabicName = arabicusername;
                                    db.Requests.Add(request);
                                    db.SaveChanges();
                                    int rq_id = request.ID;
                                    decrease_vacation_count(rq_id);
                                    InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                    message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                    if (request.ReqStatus == "PendingSupervisorApproval")
                                    {
                                        SendMail_TeamLeader_VacationRequested_For_Supervisor(Name, UserMail, Supervisor, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                    }
                                    else
                                    {
                                        SendMail_TeamLeader_VacationRequested_For_SupervisorBackup(Name, UserMail, BackupSupervisorMail, BackupSupervisorMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                    }
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }

                            }

                            else
                            {
                                message = "You Do Not Have Enough Casual Vacation";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            message = "Maximum Casual Vacation is Two Days ";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                    }

                    else
                    {
                        message = "You Have Exceeded Number of Casual Vacations ";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }

                }

                else
                {
                    message = "You have not exceeded 6 months !";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }

            else if (ReqSubType == "عارضه - أخري")
            {

                if (DateDiff >= 180)
                {
                    if (RemainingAnnualVacation > 0 && RemainingAnnualVacation >= ReqDuration)
                    {
                        bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                        bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                        bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                        bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                        bool validcasual = CheckifItsValidCasualVacationRequest(DateTime.Parse(DurationFrom).Date, ReqDuration, Name, HalfDayVacationType, backwork);
                        if (checkifitsvalidDates == true)
                        {
                            message = "Please Check start/end vacation Date";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        if (checkifitsofficialholiday == true)
                        {
                            message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                        else if (checkifitsalreadytakenholiday == true)
                        {
                            message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }


                        else if (checkifitsalreadyexcuseday == true)
                        {
                            message = "You Requested Start/End vacation Date which you requested Excuse in";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else if (validcasual == true)
                        {
                            message = "Please check Casual vacation Rules";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else if (backwork == DateTime.MinValue.Date)
                        {
                            message = "Please Review Dates";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else if (ReqDuration <= RemainingCasualVacation)
                        {
                            message = "You still have enough casual vacations !";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                        else
                        {
                            request.ReqType = "إجازة";
                            request.EmpType = type;
                            request.ReqDate = DateTime.Now;
                            if (BackupSupervisor == null)
                            {
                                request.ReqStatus = "PendingSupervisorApproval";
                                request.MySupervisor = Supervisor;

                            }
                            else
                            {
                                request.ReqStatus = "PendingSupervisorBackupApproval";
                                request.MySupervisor = Supervisor;
                                request.MyBackupSupervisor = BackupSupervisor;
                            }
                            if (DuputyManager != null)
                            {
                                request.MyBackupManager = DuputyManager;
                            }
                            request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                            request.DurationTo = DateTime.Parse(DurationTo).Date;
                            request.BacktoWorkDate = backwork;
                            request.UserName = Name;
                            request.DepartmentName = SubdepartmentName;
                            request.ManagmentName = departmentName;
                            request.MyManager = manager;
                            request.ArabicName = arabicusername;
                            request.ReqDuration = ReqDuration;
                            request.EmployeeCode = EMPCODE;
                            db.Requests.Add(request);
                            db.SaveChanges();
                            int rq_id = request.ID;
                            decrease_vacation_count(rq_id);
                            InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                            message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                            if (request.ReqStatus == "PendingSupervisorApproval")
                            {
                                SendMail_TeamLeader_VacationRequested_For_Supervisor(Name, UserMail, Supervisor, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                            }
                            else
                            {
                                SendMail_TeamLeader_VacationRequested_For_SupervisorBackup(Name, UserMail, BackupSupervisorMail, BackupSupervisorMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                            }
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }


                    }
                    else
                    {
                        message = "You Have No Annual Vacations";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                }

                else
                {
                    message = "You Have Not exceeded 6 months !";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }

            else if (ReqSubType == "اعتيادية")
            {

                if (DateDiff >= 180)
                {
                    if (RemainingAnnualVacation > 0 && RemainingAnnualVacation >= ReqDuration)
                    {
                        bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                        bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                        bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                        bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                        bool annualcheck = CheckifItsValidAnnualVacationRequest(DateTime.Parse(DurationFrom).Date);
                        //bool delegation = checkifPassedAuthorities(Name);
                        if (checkifitsvalidDates == true)
                        {
                            message = "Please Check start/end vacation Date";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        if (checkifitsofficialholiday == true)
                        {
                            message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                        else if (checkifitsalreadytakenholiday == true)
                        {
                            message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }


                        else if (checkifitsalreadyexcuseday == true)
                        {
                            message = "You Requested Start/End vacation Date which you requested Excuse in";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else if (annualcheck == true)
                        {
                            message = "Please check Annual vacation Rules";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else if (backwork == DateTime.MinValue.Date)
                        {
                            message = "Please Review Dates";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                        //else if (delegation==true)
                        //{
                        //    message = "Please Delegate your Authorities to the Sub-Department's Senior. Go To Home -> Delegation -> Choose User -> Click 'Delegate To Selected User' ";
                        //    return Json(message, JsonRequestBehavior.AllowGet);
                        //}


                        else
                        {
                            request.ReqType = "إجازة";
                            request.EmpType = type;
                            request.ReqDate = DateTime.Now;
                            if (BackupSupervisor == null)
                            {
                                request.ReqStatus = "PendingSupervisorApproval";
                                request.MySupervisor = Supervisor;

                            }

                            else
                            {
                                request.ReqStatus = "PendingSupervisorBackupApproval";
                                request.MySupervisor = Supervisor;
                                request.MyBackupSupervisor = BackupSupervisor;
                            }
                            if (DuputyManager != null)
                            {
                                request.MyBackupManager = DuputyManager;
                            }
                            request.DurationFrom = DateTime.Parse(DurationFrom).Date;

                            if (user.DelegateTime == null)
                            {
                                user.DelegateTime = DateTime.Parse(DurationFrom).Date;
                                user.DelegateTo = DelegateTo;
                                user.DelegationTimeTo = backwork;
                            }
                            request.DurationTo = DateTime.Parse(DurationTo).Date;
                            request.BacktoWorkDate = backwork;
                            request.UserName = Name;
                            request.DepartmentName = SubdepartmentName;
                            request.ManagmentName = departmentName;
                            request.MyManager = manager;
                            request.ArabicName = arabicusername;
                            request.ReqDuration = ReqDuration;
                            request.EmployeeCode = EMPCODE;
                            db.Requests.Add(request);
                            db.SaveChanges();
                            int rq_id = request.ID;
                            decrease_vacation_count(rq_id);
                            InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                            message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                            if (request.ReqStatus == "PendingSupervisorApproval")
                            {
                                SendMail_TeamLeader_VacationRequested_For_Supervisor(Name, UserMail, Supervisor, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                            }
                            else
                            {
                                SendMail_TeamLeader_VacationRequested_For_SupervisorBackup(Name, UserMail, BackupSupervisorMail, BackupSupervisorMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                            }
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }


                    }
                    else
                    {
                        message = "You Have No Annual Vacations";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                }

                else
                {
                    message = "You Have Not exceeded 6 months !";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }

            else
            {
                try
                {
                    bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                    bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                    bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                    bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                    if (checkifitsvalidDates == true)
                    {
                        message = "Please Check start/end vacation Date";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                    if (checkifitsofficialholiday == true)
                    {
                        message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }

                    else if (checkifitsalreadytakenholiday == true)
                    {
                        message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }


                    else if (checkifitsalreadyexcuseday == true)
                    {
                        message = "You Requested Start/End vacation Date which you requested Excuse in";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                    else if (backwork == DateTime.MinValue.Date)
                    {
                        message = "Please Review Dates";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }

                    else
                    {
                        request.ReqType = "إجازة";
                        request.EmpType = type;
                        request.ReqDate = DateTime.Now;
                        if (BackupSupervisor == null)
                        {
                            request.ReqStatus = "PendingSupervisorApproval";
                            request.MySupervisor = Supervisor;

                        }
                        else
                        {
                            request.ReqStatus = "PendingSupervisorBackupApproval";
                            request.MySupervisor = Supervisor;
                            request.MyBackupSupervisor = BackupSupervisor;
                        }
                        if (DuputyManager != null)
                        {
                            request.MyBackupManager = DuputyManager;
                        }
                        request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                        request.DurationTo = DateTime.Parse(DurationTo).Date;
                        request.BacktoWorkDate = backwork;
                        request.UserName = Name;
                        request.DepartmentName = SubdepartmentName;
                        request.ManagmentName = departmentName;
                        request.MyManager = manager;
                        request.ReqDuration = ReqDuration;
                        request.EmployeeCode = EMPCODE;
                        request.ArabicName = arabicusername;
                        db.Requests.Add(request);
                        db.SaveChanges();
                        InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                        message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                        if (request.ReqStatus == "PendingSupervisorApproval")
                        {
                            SendMail_TeamLeader_VacationRequested_For_Supervisor(Name, UserMail, Supervisor, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                        }
                        else
                        {
                            SendMail_TeamLeader_VacationRequested_For_SupervisorBackup(Name, UserMail, BackupSupervisorMail, BackupSupervisorMail, SupervisorMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                        }
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }

                }

                catch
                {
                    message = "There was problem Adding Your Request";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult CreateSupervisorOrDuputyManagerVacation(Request request, string ReqSubType, string DurationFrom, string DurationTo, string HalfDayVacationType, string DelegateTo)
        {
            string message;
            type = (string)(Session["UserType"]);
            Name = (string)(Session["UserName"]);
            List<DateTime> days = new List<DateTime>();
            DateTime backwork;
            decimal ReqDuration;
            var departmentName = (from item in db.accountTBs where item.EmpName == Name select item.DepartmentName).SingleOrDefault();
            var SubdepartmentName = (from item in db.accountTBs where item.EmpName == Name select item.SubDepartmentName).SingleOrDefault();
            var manager = (from p in db.DepartmentTBs where p.DeptName == departmentName select p.DeptManager).SingleOrDefault();
            var arabicusername = (from p in db.accountTBs where p.EmpName == Name select p.ArabicName).SingleOrDefault();
            var RemainingAnnualVacation = (from p in db.accountTBs where p.EmpName == Name select p.AnnualVac).SingleOrDefault();
            var RemainingCasualVacation = (from p in db.accountTBs where p.EmpName == Name select p.CasualVac).SingleOrDefault();
            var EMPCODE = (from p in db.accountTBs where p.EmpName == Name select p.EmpCode).SingleOrDefault();
            var HireDate = (from p in db.accountTBs where p.EmpName == Name select p.HireDate.Value).SingleOrDefault();
            var DuputyManager = (from p in db.DepartmentTBs where p.DeptName == departmentName select p.DeptBackupManager).SingleOrDefault();
            var DuputyManagerMail = (from item in db.accountTBs where item.EmpName == DuputyManager select item.Email).SingleOrDefault();
            var DateDiff = Convert.ToInt32((DateTime.Now.Date - HireDate).TotalDays);
            var UserMail = (from item in db.accountTBs where item.EmpName == Name select item.Email).SingleOrDefault();
            var ManagerMail = (from item in db.accountTBs where item.EmpName == manager select item.Email).SingleOrDefault();
            var user = (from u in db.accountTBs where u.EmpName == Name select u).SingleOrDefault();
            CheckDays(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, ReqSubType, HalfDayVacationType, out days, out backwork, out ReqDuration);

            if (ReqSubType == "مرضية")
            {
                if (DateDiff >= 180)
                {
                    if (RemainingAnnualVacation > 0 && RemainingAnnualVacation >= ReqDuration / 4)
                    {
                        bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                        bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                        bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                        bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                        if (checkifitsvalidDates == true)
                        {
                            message = "Please Check start/end vacation Date";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        if (checkifitsofficialholiday == true)
                        {
                            message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                        else if (checkifitsalreadytakenholiday == true)
                        {
                            message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                        else if (checkifitsalreadyexcuseday == true)
                        {
                            message = "You Requested Start/End vacation Date which you requested Excuse in";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else if (backwork == DateTime.MinValue.Date)
                        {
                            message = "Please Review Dates";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            request.ReqType = "إجازة";
                            request.EmpType = type;
                            request.ReqDate = DateTime.Now;
                            if (DuputyManager == null)
                            {
                                request.ReqStatus = "PendingManagerApproval";
                                request.MyBackupManager = null;
                            }
                            else
                            {
                                request.ReqStatus = "PendingDuputyManagerApproval";
                                request.MyBackupManager = DuputyManager;
                            }
                            request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                            request.DurationTo = DateTime.Parse(DurationTo).Date;
                            request.BacktoWorkDate = backwork;
                            request.UserName = Name;
                            request.DepartmentName = SubdepartmentName;
                            request.ManagmentName = departmentName;
                            request.MyManager = manager;
                            request.ArabicName = arabicusername;
                            request.ReqDuration = ReqDuration;
                            request.EmployeeCode = EMPCODE;
                            db.Requests.Add(request);
                            db.SaveChanges();
                            int rq_id = request.ID;
                            decrease_vacation_count(rq_id);
                            InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                            message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                            if (request.ReqStatus == "PendingManagerApproval")
                            {
                                SendMail_Supervisor_VacationRequested_For_Manager(Name, UserMail, manager, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                            }
                            else
                            {
                                SendMail_Supervisor_VacationRequested_For_Manager_Backup(Name, UserMail, DuputyManager, DuputyManagerMail, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                            }
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                    }

                    else
                    {
                        message = "You have no remaining days !";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                }

                else
                {
                    message = "You have not exceeded 6 months !";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }



            else if (ReqSubType == "عارضه")
            {

                if (DateDiff >= 180)
                {
                    var LastVacation = (from item in db.accountTBs where item.EmpName == Name select item.LastCasualVacation).SingleOrDefault();
                    var Excceded = (from item in db.accountTBs where item.EmpName == Name select item.MaximumNumberofCasualExceed).SingleOrDefault();
                    DateTime EndcasualVacation;
                    if (LastVacation == null)
                    {
                        if (ReqDuration <= 2)
                        {

                            if (Excceded == "false")
                            {
                                if (RemainingCasualVacation > 0 && RemainingCasualVacation >= ReqDuration)
                                {
                                    bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                    bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                    bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                    bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                    bool validcasual = CheckifItsValidCasualVacationRequest(DateTime.Parse(DurationFrom).Date, ReqDuration, Name, HalfDayVacationType, backwork);
                                    if (checkifitsvalidDates == true)
                                    {
                                        message = "Please Check start/end vacation Date";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    if (checkifitsofficialholiday == true)
                                    {
                                        message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }

                                    else if (checkifitsalreadytakenholiday == true)
                                    {
                                        message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }


                                    else if (checkifitsalreadyexcuseday == true)
                                    {
                                        message = "You Requested Start/End vacation Date which you requested Excuse in";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    else if (validcasual == true)
                                    {
                                        message = "Please check Casual vacation Rules";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    else if (backwork == DateTime.MinValue.Date)
                                    {
                                        message = "Please Review Dates";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        request.ReqType = "إجازة";
                                        request.EmpType = type;
                                        request.ReqDate = DateTime.Now;
                                        request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                        request.DurationTo = DateTime.Parse(DurationTo).Date;
                                        request.BacktoWorkDate = backwork;
                                        if (DuputyManager == null)
                                        {
                                            request.ReqStatus = "PendingManagerApproval";
                                            request.MyBackupManager = null;
                                        }
                                        else
                                        {
                                            request.ReqStatus = "PendingDuputyManagerApproval";
                                            request.MyBackupManager = DuputyManager;
                                        }
                                        request.UserName = Name;
                                        request.DepartmentName = SubdepartmentName;
                                        request.ManagmentName = departmentName;
                                        request.MyManager = manager;
                                        request.ArabicName = arabicusername;
                                        request.ReqDuration = ReqDuration;
                                        request.EmployeeCode = EMPCODE;
                                        db.Requests.Add(request);
                                        db.SaveChanges();
                                        int rq_id = request.ID;
                                        decrease_vacation_count(rq_id);
                                        InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                        message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                        if (request.ReqStatus == "PendingManagerApproval")
                                        {
                                            SendMail_Supervisor_VacationRequested_For_Manager(Name, UserMail, manager, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                        }
                                        else
                                        {
                                            SendMail_Supervisor_VacationRequested_For_Manager_Backup(Name, UserMail, DuputyManager, DuputyManagerMail, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                        }
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }

                                }

                                else
                                {
                                    message = "You Do Not Have Enough Casual Vacation";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                message = "You have exceeded Casual Vacations limit";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                        }

                        else
                        {
                            message = "Maximum Number of Casual Vacations is two days!";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                    }

                    else
                    {
                        EndcasualVacation = LastVacation.Value.Date.AddDays(Convert.ToDouble(ReqDuration));
                    }
                    int DateDifference = Convert.ToInt32((EndcasualVacation.Date - LastVacation.Value.Date).TotalDays);
                    if (Excceded == "false")
                    {
                        if (DateDifference <= 2)
                        {
                            if (RemainingCasualVacation > 0 && RemainingCasualVacation >= ReqDuration)
                            {
                                bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                bool validcasual = CheckifItsValidCasualVacationRequest(DateTime.Parse(DurationFrom).Date, ReqDuration, Name, HalfDayVacationType, backwork);
                                if (checkifitsvalidDates == true)
                                {
                                    message = "Please Check start/end vacation Date";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                                if (checkifitsofficialholiday == true)
                                {
                                    message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }

                                else if (checkifitsalreadytakenholiday == true)
                                {
                                    message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }


                                else if (checkifitsalreadyexcuseday == true)
                                {
                                    message = "You Requested Start/End vacation Date which you requested Excuse in";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                                else if (validcasual == true)
                                {
                                    message = "Please check Casual vacation Rules";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                                else if (backwork == DateTime.MinValue.Date)
                                {
                                    message = "Please Review Dates";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }

                                else
                                {
                                    request.ReqType = "إجازة";
                                    request.EmpType = type;
                                    request.ReqDate = DateTime.Now;
                                    if (DuputyManager == null)
                                    {
                                        request.ReqStatus = "PendingManagerApproval";
                                        request.MyBackupManager = null;
                                    }
                                    else
                                    {
                                        request.ReqStatus = "PendingDuputyManagerApproval";
                                        request.MyBackupManager = DuputyManager;
                                    }
                                    request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                    request.DurationTo = DateTime.Parse(DurationTo).Date;
                                    request.BacktoWorkDate = backwork;
                                    request.UserName = Name;
                                    request.DepartmentName = SubdepartmentName;
                                    request.ManagmentName = departmentName;
                                    request.MyManager = manager;
                                    request.ReqDuration = ReqDuration;
                                    request.EmployeeCode = EMPCODE;
                                    request.ArabicName = arabicusername;
                                    db.Requests.Add(request);
                                    db.SaveChanges();
                                    int rq_id = request.ID;
                                    decrease_vacation_count(rq_id);
                                    InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                    message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                    if (request.ReqStatus == "PendingManagerApproval")
                                    {
                                        SendMail_Supervisor_VacationRequested_For_Manager(Name, UserMail, manager, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                    }
                                    else
                                    {
                                        SendMail_Supervisor_VacationRequested_For_Manager_Backup(Name, UserMail, DuputyManager, DuputyManagerMail, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                                    }
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }

                            }

                            else
                            {
                                message = "You Do Not Have Enough Casual Vacation";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            message = "Maximum Casual Vacation is Two Days ";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                    }

                    else
                    {
                        message = "You Have Exceeded Number of Casual Vacations ";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }

                }

                else
                {
                    message = "You have not exceeded 6 months !";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }

            else if (ReqSubType == "عارضه - أخري")
            {

                if (DateDiff >= 180)
                {
                    if (RemainingAnnualVacation > 0 && RemainingAnnualVacation >= ReqDuration)
                    {
                        bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                        bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                        bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                        bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                        bool validcasual = CheckifItsValidCasualVacationRequest(DateTime.Parse(DurationFrom).Date, ReqDuration, Name, HalfDayVacationType, backwork);
                        if (checkifitsvalidDates == true)
                        {
                            message = "Please Check start/end vacation Date";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        if (checkifitsofficialholiday == true)
                        {
                            message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                        else if (checkifitsalreadytakenholiday == true)
                        {
                            message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }


                        else if (checkifitsalreadyexcuseday == true)
                        {
                            message = "You Requested Start/End vacation Date which you requested Excuse in";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else if (validcasual == true)
                        {
                            message = "Please check Casual vacation Rules";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else if (backwork == DateTime.MinValue.Date)
                        {
                            message = "Please Review Dates";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else if (ReqDuration <= RemainingCasualVacation)
                        {
                            message = "You still have enough casual vacations !";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                        else
                        {
                            request.ReqType = "إجازة";
                            request.EmpType = type;
                            request.ReqDate = DateTime.Now;
                            if (DuputyManager == null)
                            {
                                request.ReqStatus = "PendingManagerApproval";
                                request.MyBackupManager = null;
                            }
                            else
                            {
                                request.ReqStatus = "PendingDuputyManagerApproval";
                                request.MyBackupManager = DuputyManager;
                            }
                            request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                            request.DurationTo = DateTime.Parse(DurationTo).Date;
                            request.BacktoWorkDate = backwork;
                            request.UserName = Name;
                            request.DepartmentName = SubdepartmentName;
                            request.ManagmentName = departmentName;
                            request.MyManager = manager;
                            request.ArabicName = arabicusername;
                            request.ReqDuration = ReqDuration;
                            request.EmployeeCode = EMPCODE;
                            db.Requests.Add(request);
                            db.SaveChanges();
                            int rq_id = request.ID;
                            decrease_vacation_count(rq_id);
                            InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                            message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                            if (request.ReqStatus == "PendingManagerApproval")
                            {
                                SendMail_Supervisor_VacationRequested_For_Manager(Name, UserMail, manager, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                            }
                            else
                            {
                                SendMail_Supervisor_VacationRequested_For_Manager_Backup(Name, UserMail, DuputyManager, DuputyManagerMail, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                            }
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }


                    }
                    else
                    {
                        message = "You Have No Annual Vacations";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                }

                else
                {
                    message = "You Have Not exceeded 6 months !";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }

            else if (ReqSubType == "اعتيادية")
            {

                if (DateDiff >= 180)
                {
                    if (RemainingAnnualVacation > 0 && RemainingAnnualVacation >= ReqDuration)
                    {
                        bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                        bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                        bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                        bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                        bool annualcheck = CheckifItsValidAnnualVacationRequest(DateTime.Parse(DurationFrom).Date);
                        bool delegation = checkifPassedAuthorities(Name);
                        if (checkifitsvalidDates == true)
                        {
                            message = "Please Check start/end vacation Date";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        if (checkifitsofficialholiday == true)
                        {
                            message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                        else if (checkifitsalreadytakenholiday == true)
                        {
                            message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }


                        else if (checkifitsalreadyexcuseday == true)
                        {
                            message = "You Requested Start/End vacation Date which you requested Excuse in";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else if (annualcheck == true)
                        {
                            message = "Please check Annual vacation Rules";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else if (backwork == DateTime.MinValue.Date)
                        {
                            message = "Please Review Dates";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        //else if (delegation == true)
                        //{
                        //    message = "Please Delegate your Authorities to a Team Leader. Go To Home -> Delegation -> Choose User -> Click 'Delegate To Selected User' ";
                        //    return Json(message, JsonRequestBehavior.AllowGet);
                        //}

                        else
                        {
                            request.ReqType = "إجازة";
                            request.EmpType = type;
                            request.ReqDate = DateTime.Now;
                            if (DuputyManager == null)
                            {
                                request.ReqStatus = "PendingManagerApproval";
                                request.MyBackupManager = null;
                            }
                            else
                            {
                                request.ReqStatus = "PendingDuputyManagerApproval";
                                request.MyBackupManager = DuputyManager;
                            }
                            request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                            if (user.DelegateTime == null)
                            {
                                user.DelegateTime = DateTime.Parse(DurationFrom).Date;
                                user.DelegateTo = DelegateTo;
                                user.DelegationTimeTo = backwork;
                            }
                            request.DurationTo = DateTime.Parse(DurationTo).Date;
                            request.BacktoWorkDate = backwork;
                            request.UserName = Name;
                            request.DepartmentName = SubdepartmentName;
                            request.ManagmentName = departmentName;
                            request.MyManager = manager;
                            request.ArabicName = arabicusername;
                            request.ReqDuration = ReqDuration;
                            request.EmployeeCode = EMPCODE;
                            db.Requests.Add(request);
                            db.SaveChanges();
                            int rq_id = request.ID;
                            decrease_vacation_count(rq_id);
                            InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                            message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                            if (request.ReqStatus == "PendingManagerApproval")
                            {
                                SendMail_Supervisor_VacationRequested_For_Manager(Name, UserMail, manager, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                            }
                            else
                            {
                                SendMail_Supervisor_VacationRequested_For_Manager_Backup(Name, UserMail, DuputyManager, DuputyManagerMail, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                            }
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }


                    }
                    else
                    {
                        message = "You Have No Annual Vacations";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                }

                else
                {
                    message = "You Have Not exceeded 6 months !";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }

            else
            {
                try
                {
                    bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                    bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                    bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                    bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                    if (checkifitsvalidDates == true)
                    {
                        message = "Please Check start/end vacation Date";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                    if (checkifitsofficialholiday == true)
                    {
                        message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }

                    else if (checkifitsalreadytakenholiday == true)
                    {
                        message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }


                    else if (checkifitsalreadyexcuseday == true)
                    {
                        message = "You Requested Start/End vacation Date which you requested Excuse in";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                    else if (backwork == DateTime.MinValue.Date)
                    {
                        message = "Please Review Dates";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }

                    else
                    {
                        request.ReqType = "إجازة";
                        request.EmpType = type;
                        request.ReqDate = DateTime.Now;
                        if (DuputyManager == null)
                        {
                            request.ReqStatus = "PendingManagerApproval";
                            request.MyBackupManager = null;
                        }
                        else
                        {
                            request.ReqStatus = "PendingDuputyManagerApproval";
                            request.MyBackupManager = DuputyManager;
                        }
                        request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                        request.DurationTo = DateTime.Parse(DurationTo).Date;
                        request.BacktoWorkDate = backwork;
                        request.UserName = Name;
                        request.DepartmentName = SubdepartmentName;
                        request.ManagmentName = departmentName;
                        request.MyManager = manager;
                        request.ReqDuration = ReqDuration;
                        request.EmployeeCode = EMPCODE;
                        request.ArabicName = arabicusername;
                        db.Requests.Add(request);
                        db.SaveChanges();
                        InsertOffdays(days, Name, ReqSubType, request.ReqStatus, ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                        message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                        if (request.ReqStatus == "PendingManagerApproval")
                        {
                            SendMail_Supervisor_VacationRequested_For_Manager(Name, UserMail, manager, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                        }
                        else
                        {
                            SendMail_Supervisor_VacationRequested_For_Manager_Backup(Name, UserMail, DuputyManager, DuputyManagerMail, ManagerMail, DateTime.Parse(DurationFrom), DateTime.Parse(DurationTo), ReqDuration);
                        }
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }

                }

                catch
                {
                    message = "There was problem Adding Your Request";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult CreateManagerVacation(Request request, string ReqSubType, string DurationFrom, string DurationTo, string HalfDayVacationType, string DelegateTo)
        {
            string message;
            type = (string)(Session["UserType"]);
            Name = (string)(Session["UserName"]);
            List<DateTime> days = new List<DateTime>();
            DateTime backwork;
            decimal ReqDuration;
            var departmentName = (from item in db.accountTBs where item.EmpName == Name select item.DepartmentName).SingleOrDefault();
            var SubdepartmentName = (from item in db.accountTBs where item.EmpName == Name select item.SubDepartmentName).SingleOrDefault();
            var arabicusername = (from p in db.accountTBs where p.EmpName == Name select p.ArabicName).SingleOrDefault();
            var RemainingAnnualVacation = (from p in db.accountTBs where p.EmpName == Name select p.AnnualVac).SingleOrDefault();
            var RemainingCasualVacation = (from p in db.accountTBs where p.EmpName == Name select p.CasualVac).SingleOrDefault();
            var EMPCODE = (from p in db.accountTBs where p.EmpName == Name select p.EmpCode).SingleOrDefault();
            var HireDate = (from p in db.accountTBs where p.EmpName == Name select p.HireDate.Value).SingleOrDefault();
            var DateDiff = Convert.ToInt32((DateTime.Now.Date - HireDate).TotalDays);
            var user = (from u in db.accountTBs where u.EmpName == Name select u).SingleOrDefault();
            CheckDays(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, ReqSubType, HalfDayVacationType, out days, out backwork, out ReqDuration);


            if (ReqSubType == "مرضية")
            {
                if (DateDiff >= 180)
                {
                    if (RemainingAnnualVacation > 0 && RemainingAnnualVacation >= ReqDuration / 4)
                    {
                        bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                        bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                        bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                        bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                        if (checkifitsvalidDates == true)
                        {
                            message = "Please Check start/end vacation Date";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        if (checkifitsofficialholiday == true)
                        {
                            message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                        else if (checkifitsalreadytakenholiday == true)
                        {
                            message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else if (checkifitsalreadyexcuseday == true)
                        {
                            message = "You Requested Start/End vacation Date which you requested Excuse in";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else if (backwork == DateTime.MinValue.Date)
                        {
                            message = "Please Review Dates";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                        else
                        {

                            request.ReqType = "إجازة";
                            request.EmpType = type;
                            request.ReqDate = DateTime.Now;
                            request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                            request.DurationTo = DateTime.Parse(DurationTo).Date;
                            request.BacktoWorkDate = backwork;
                            request.UserName = Name;
                            request.DepartmentName = SubdepartmentName;
                            request.ManagmentName = departmentName;
                            request.ArabicName = arabicusername;
                            request.ReqStatus = "ManagerRequest";
                            request.ReqDuration = ReqDuration;
                            request.EmployeeCode = EMPCODE;
                            db.Requests.Add(request);
                            db.SaveChanges();
                            int rq_id = request.ID;
                            decrease_vacation_count(rq_id);
                            InsertOffdays(days, Name, ReqSubType, "ManagerRequest", ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                            var AccountID = (from p in db.accountTBs where p.EmpName == request.UserName select p.ID).FirstOrDefault();
                            accountTB acc = db.accountTBs.Find(AccountID);
                            acc.AnnualVac = acc.AnnualVac - ReqDuration / 4;
                            db.SaveChanges();
                            message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                    }

                    else
                    {
                        message = "You have no remaining days !";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                }

                else
                {
                    message = "You have not exceeded 6 months !";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }



            else if (ReqSubType == "عارضه")
            {

                if (DateDiff >= 180)
                {
                    var LastVacation = (from item in db.accountTBs where item.EmpName == Name select item.LastCasualVacation).SingleOrDefault();
                    var Excceded = (from item in db.accountTBs where item.EmpName == Name select item.MaximumNumberofCasualExceed).SingleOrDefault();
                    DateTime EndcasualVacation;
                    if (LastVacation == null)
                    {
                        if (ReqDuration <= 2)
                        {
                            if (Excceded == "false")
                            {
                                if (RemainingCasualVacation > 0 && RemainingCasualVacation >= ReqDuration)
                                {
                                    bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                    bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                    bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                    bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                    bool validcasual = CheckifItsValidCasualVacationRequest(DateTime.Parse(DurationFrom).Date, ReqDuration, Name, HalfDayVacationType, backwork);
                                    if (checkifitsvalidDates == true)
                                    {
                                        message = "Please Check start/end vacation Date";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    if (checkifitsofficialholiday == true)
                                    {
                                        message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }

                                    else if (checkifitsalreadytakenholiday == true)
                                    {
                                        message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }


                                    else if (checkifitsalreadyexcuseday == true)
                                    {
                                        message = "You Requested Start/End vacation Date which you requested Excuse in";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    else if (validcasual == true)
                                    {
                                        message = "Please check Casual vacation Rules";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    else if (backwork == DateTime.MinValue.Date)
                                    {
                                        message = "Please Review Dates";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        request.ReqType = "إجازة";
                                        request.EmpType = type;
                                        request.ReqDate = DateTime.Now;
                                        request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                        request.DurationTo = DateTime.Parse(DurationTo).Date;
                                        request.BacktoWorkDate = backwork;
                                        request.UserName = Name;
                                        request.DepartmentName = SubdepartmentName;
                                        request.ManagmentName = departmentName;
                                        request.ArabicName = arabicusername;
                                        request.EmployeeCode = EMPCODE;
                                        request.ReqDuration = ReqDuration;
                                        request.ReqStatus = "ManagerRequest";
                                        db.Requests.Add(request);
                                        db.SaveChanges();
                                        int rq_id = request.ID;
                                        decrease_vacation_count(rq_id);
                                        InsertOffdays(days, Name, ReqSubType, "ManagerRequest", ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                        var AccountID = (from p in db.accountTBs where p.EmpName == request.UserName select p.ID).FirstOrDefault();
                                        accountTB acc = db.accountTBs.Find(AccountID);
                                        acc.CasualVac = acc.CasualVac - ReqDuration;
                                        acc.AnnualVac = acc.AnnualVac - ReqDuration;
                                        db.SaveChanges();
                                        message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                        return Json(message, JsonRequestBehavior.AllowGet);
                                    }
                                }

                                else
                                {
                                    message = "You Do Not Have Enough Casual Vacation";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                message = "You Have Exceeded Casual Vacation Limit";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                        }

                        else
                        {
                            message = "Maximum Number Of Casual Vacations is two days !";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                    }

                    else
                    {
                        EndcasualVacation = LastVacation.Value.Date.AddDays(Convert.ToDouble(ReqDuration));
                    }
                    int DateDifference = Convert.ToInt32((EndcasualVacation.Date - LastVacation.Value.Date).TotalDays);
                    if (Excceded == "false")
                    {
                        if (DateDifference <= 2)
                        {
                            if (RemainingCasualVacation > 0 && RemainingCasualVacation >= ReqDuration)
                            {
                                bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                                bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                                bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                                if (checkifitsvalidDates == true)
                                {
                                    message = "Please Check start/end vacation Date";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                                if (checkifitsofficialholiday == true)
                                {
                                    message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }

                                else if (checkifitsalreadytakenholiday == true)
                                {
                                    message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }


                                else if (checkifitsalreadyexcuseday == true)
                                {
                                    message = "You Requested Start/End vacation Date which you requested Excuse in";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                                else if (backwork == DateTime.MinValue.Date)
                                {
                                    message = "Please Review Dates";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                                else
                                {
                                    request.ReqType = "إجازة";
                                    request.EmpType = type;
                                    request.ReqDate = DateTime.Now;
                                    request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                                    request.DurationTo = DateTime.Parse(DurationTo).Date;
                                    request.BacktoWorkDate = backwork;
                                    request.UserName = Name;
                                    request.DepartmentName = SubdepartmentName;
                                    request.ManagmentName = departmentName;
                                    request.ArabicName = arabicusername;
                                    request.ReqDuration = ReqDuration;
                                    request.EmployeeCode = EMPCODE;
                                    request.ReqStatus = "ManagerRequest";
                                    db.Requests.Add(request);
                                    db.SaveChanges();
                                    int rq_id = request.ID;
                                    decrease_vacation_count(rq_id);
                                    InsertOffdays(days, Name, ReqSubType, "ManagerRequest", ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                                    var AccountID = (from p in db.accountTBs where p.EmpName == request.UserName select p.ID).FirstOrDefault();
                                    accountTB acc = db.accountTBs.Find(AccountID);
                                    acc.CasualVac = acc.CasualVac - ReqDuration;
                                    acc.AnnualVac = acc.AnnualVac - ReqDuration;
                                    db.SaveChanges();
                                    message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                                    return Json(message, JsonRequestBehavior.AllowGet);
                                }
                            }

                            else
                            {
                                message = "You Do Not Have Enough Casual Vacation";
                                return Json(message, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            message = "Maximum Casual Vacation is Two Days ";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                    }

                    else
                    {
                        message = "You Have Exceeded Number of Casual Vacations ";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }

                }

                else
                {
                    message = "You have not exceeded 6 months !";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }

            else if (ReqSubType == "عارضه - أخري")
            {
                if (DateDiff >= 180)
                {
                    if (RemainingAnnualVacation > 0 && RemainingAnnualVacation >= ReqDuration)
                    {
                        bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                        bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                        bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                        bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                        bool validcasual = CheckifItsValidCasualVacationRequest(DateTime.Parse(DurationFrom).Date, ReqDuration, Name, HalfDayVacationType, backwork);
                        if (checkifitsvalidDates == true)
                        {
                            message = "Please Check start/end vacation Date";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        if (checkifitsofficialholiday == true)
                        {
                            message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                        else if (checkifitsalreadytakenholiday == true)
                        {
                            message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }


                        else if (checkifitsalreadyexcuseday == true)
                        {
                            message = "You Requested Start/End vacation Date which you requested Excuse in";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else if (validcasual == true)
                        {
                            message = "Please check Casual vacation Rules";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else if (backwork == DateTime.MinValue.Date)
                        {
                            message = "Please Review Dates";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else if (ReqDuration <= RemainingCasualVacation)
                        {
                            message = "You still have enough casual vacations !";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            request.ReqType = "إجازة";
                            request.EmpType = type;
                            request.ReqDate = DateTime.Now;
                            request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                            request.DurationTo = DateTime.Parse(DurationTo).Date;
                            request.BacktoWorkDate = backwork;
                            request.UserName = Name;
                            request.DepartmentName = SubdepartmentName;
                            request.ManagmentName = departmentName;
                            request.ArabicName = arabicusername;
                            request.EmployeeCode = EMPCODE;
                            request.ReqDuration = ReqDuration;
                            request.ReqStatus = "ManagerRequest";
                            db.Requests.Add(request);
                            db.SaveChanges();
                            int rq_id = request.ID;
                            decrease_vacation_count(rq_id);
                            InsertOffdays(days, Name, ReqSubType, "ManagerRequest", ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                            var AccountID = (from p in db.accountTBs where p.EmpName == request.UserName select p.ID).FirstOrDefault();
                            accountTB acc = db.accountTBs.Find(AccountID);
                            acc.CasualVac = acc.CasualVac - ReqDuration;
                            acc.AnnualVac = acc.AnnualVac - ReqDuration;
                            db.SaveChanges();
                            message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        message = "You Have No Annual Vacations";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                }

                else
                {
                    message = "You Have Not exceeded 6 Months";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }

            }

            else if (ReqSubType == "اعتيادية")
            {
                if (DateDiff >= 180)
                {
                    if (RemainingAnnualVacation > 0 && RemainingAnnualVacation >= ReqDuration)
                    {
                        bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                        bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                        bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                        bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                        bool annualcheck = CheckifItsValidAnnualVacationRequest(DateTime.Parse(DurationFrom).Date);
                        bool delegation = checkifPassedAuthorities(Name);
                        if (checkifitsvalidDates == true)
                        {
                            message = "Please Check start/end vacation Date";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        if (checkifitsofficialholiday == true)
                        {
                            message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }

                        else if (checkifitsalreadytakenholiday == true)
                        {
                            message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }


                        else if (checkifitsalreadyexcuseday == true)
                        {
                            message = "You Requested Start/End vacation Date which you requested Excuse in";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else if (annualcheck == true)
                        {
                            message = "Please check Annual vacation Rules";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        else if (backwork == DateTime.MinValue.Date)
                        {
                            message = "Please Review Dates";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                        //else if (delegation == true)
                        //{
                        //    message = "Please Delegate your Authorities to the Department's Deputy Manager. Go To Home -> Delegation -> Choose User -> Click 'Delegate To Selected User' ";
                        //    return Json(message, JsonRequestBehavior.AllowGet);
                        //}
                        else
                        {
                            request.ReqType = "إجازة";
                            request.EmpType = type;
                            request.ReqDate = DateTime.Now;
                            request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                            if (user.DelegateTime == null)
                            {
                                user.DelegateTime = DateTime.Parse(DurationFrom).Date;
                                user.DelegateTo = DelegateTo;
                                user.DelegationTimeTo = backwork;
                            }
                            request.DurationTo = DateTime.Parse(DurationTo).Date;
                            request.BacktoWorkDate = backwork;
                            request.UserName = Name;
                            request.DepartmentName = SubdepartmentName;
                            request.ManagmentName = departmentName;
                            request.ReqStatus = "ManagerRequest";
                            request.ArabicName = arabicusername;
                            request.ReqDuration = ReqDuration;
                            request.EmployeeCode = EMPCODE;
                            db.Requests.Add(request);
                            db.SaveChanges();
                            int rq_id = request.ID;
                            decrease_vacation_count(rq_id);
                            InsertOffdays(days, Name, ReqSubType, "ManagerRequest", ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                            var AccountID = (from p in db.accountTBs where p.EmpName == request.UserName select p.ID).FirstOrDefault();
                            accountTB acc = db.accountTBs.Find(AccountID);
                            acc.AnnualVac = acc.AnnualVac - ReqDuration;
                            db.SaveChanges();
                            message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                            return Json(message, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        message = "You Have No Annual Vacations";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    message = "You Have Not exceeded 6 Months";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                try
                {
                    bool checkifitsofficialholiday = checkifofficialdayorWeekend(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                    bool checkifitsalreadytakenholiday = checkifitsalreadyholiday(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name);
                    bool checkifitsalreadyexcuseday = checkifitsalreadyExcuse(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date, Name, ReqDuration);
                    bool checkifitsvalidDates = checkifenddateandstartdateisvalid(DateTime.Parse(DurationFrom).Date, DateTime.Parse(DurationTo).Date);
                    if (checkifitsvalidDates == true)
                    {
                        message = "Please Check start/end vacation Date";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                    if (checkifitsofficialholiday == true)
                    {
                        message = "You Requested Start/End Date which is Officially Holiday or a WeekEnd";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }

                    else if (checkifitsalreadytakenholiday == true)
                    {
                        message = "You Requested Start/End Date which is a Pending Holiday or a already Approved Holiday";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }


                    else if (checkifitsalreadyexcuseday == true)
                    {
                        message = "You Requested Start/End vacation Date which you requested Excuse in";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                    else if (backwork == DateTime.MinValue.Date)
                    {
                        message = "Please Review Dates";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        request.ReqType = "إجازة";
                        request.EmpType = type;
                        request.ReqDate = DateTime.Now;
                        request.DurationFrom = DateTime.Parse(DurationFrom).Date;
                        request.DurationTo = DateTime.Parse(DurationTo).Date;
                        request.BacktoWorkDate = backwork;
                        request.UserName = Name;
                        request.DepartmentName = SubdepartmentName;
                        request.ManagmentName = departmentName;
                        request.ArabicName = arabicusername;
                        request.EmployeeCode = EMPCODE;
                        request.ReqDuration = ReqDuration;
                        request.ReqStatus = "ManagerRequest";
                        db.Requests.Add(request);
                        db.SaveChanges();
                        int rq_id = request.ID;
                        decrease_vacation_count(rq_id);
                        InsertOffdays(days, Name, ReqSubType, "ManagerRequest", ReqDuration, HalfDayVacationType, departmentName, SubdepartmentName, request.ID);
                        db.SaveChanges();
                        message = "Request Added Successfully. \n You Requested " + ReqDuration + " Day(s). \n You Should Return on " + backwork.DayOfWeek + " , " + backwork.Date.ToString("dd/MM/yyyy") + "";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                }
                catch
                {
                    message = "There was problem Adding Your Request";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult GetTypeOfUser()
        {
            type = (string)(Session["UserType"]);

            return Json(type, JsonRequestBehavior.AllowGet);
        }
        string message;
        public JsonResult ApproveByTeamLeader(int ID)
        {
            Request req = db.Requests.Find(ID);
            string requestuser = req.UserName;
            req.ReqStatus = "PendingSupervisorApproval";
            req.TeamLeadApproveDate = DateTime.Now;
            req.TeamLeaderApprove = "true";
            req.ActionTaken = "yes";
            req.ApprovedByTeamLeader = (string)(Session["UserName"]);
            var departmentName = (from item in db.accountTBs where item.EmpName == req.UserName select item.DepartmentName).SingleOrDefault();
            var SubdepartmentName = (from item in db.accountTBs where item.EmpName == requestuser select item.SubDepartmentName).SingleOrDefault();
            var teamleader = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepBackupTeamLeader).SingleOrDefault();
            var manager = (from p in db.DepartmentTBs where p.DeptName == departmentName select p.DeptManager).SingleOrDefault();
            var TeamLeaderMail = (from item in db.accountTBs where item.EmpName == teamleader select item.Email).SingleOrDefault();
            var Supervisor = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepTeamLeader).SingleOrDefault();
            var SupervisorMail = (from item in db.accountTBs where item.EmpName == Supervisor select item.Email).SingleOrDefault();
            var UserMail = (from item in db.accountTBs where item.EmpName == requestuser select item.Email).SingleOrDefault();
            var ManagerMail = (from item in db.accountTBs where item.EmpName == manager select item.Email).SingleOrDefault();
            string result = ApproveDayByDayForTeamLeader(ID);
            db.SaveChanges();
            message = "Request Approved By TeamLeader Successfully";
            SendMail_VacationApproved_ByTeamLeader(requestuser, UserMail, teamleader, TeamLeaderMail, SupervisorMail, DateTime.Parse(req.DurationFrom.ToString()));
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RejectedByTeamLeader(int ID)
        {
            Request req = db.Requests.Find(ID);
            req.ReqStatus = "RejectedByTeamLeader";
            req.TeamLeadApproveDate = DateTime.Now;
            req.TeamLeaderApprove = "false";
            req.ApprovedByTeamLeader = (string)(Session["UserName"]);
            var SubdepartmentName = (from item in db.accountTBs where item.EmpName == req.UserName select item.SubDepartmentName).SingleOrDefault();
            var teamleader = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepBackupTeamLeader).SingleOrDefault();
            var TeamLeaderMail = (from item in db.accountTBs where item.EmpName == teamleader select item.Email).SingleOrDefault();
            var Supervisor = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepTeamLeader).SingleOrDefault();
            var SupervisorMail = (from item in db.accountTBs where item.EmpName == Supervisor select item.Email).SingleOrDefault();
            var UserMail = (from item in db.accountTBs where item.EmpName == req.UserName select item.Email).SingleOrDefault();
            string result = RejectByDayForTeamLeader(ID);
            db.SaveChanges();
            increase_vacation_count(ID);
            message = "Request Rejected By TeamLeader Successfully";
            SendMail_VacationRejected_ByTeamLeader(req.UserName, UserMail, teamleader, TeamLeaderMail, SupervisorMail, DateTime.Parse(req.DurationFrom.ToString()));
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ApproveBySupervisor(int ID)
        {
            Request req = db.Requests.Find(ID);
            string requestuser = req.UserName;
            req.ActionTaken = "yes";
            req.ApprovedBySupervisor = (string)(Session["UserName"]);
            var departmentName = (from item in db.accountTBs where item.EmpName == req.UserName select item.DepartmentName).SingleOrDefault();
            var SubdepartmentName = (from item in db.accountTBs where item.EmpName == requestuser select item.SubDepartmentName).SingleOrDefault();
            var teamleader = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepBackupTeamLeader).SingleOrDefault();
            var manager = (from p in db.DepartmentTBs where p.DeptName == departmentName select p.DeptManager).SingleOrDefault();
            var TeamLeaderMail = (from item in db.accountTBs where item.EmpName == teamleader select item.Email).SingleOrDefault();
            var UserMail = (from item in db.accountTBs where item.EmpName == requestuser select item.Email).SingleOrDefault();
            var ManagerMail = (from item in db.accountTBs where item.EmpName == manager select item.Email).SingleOrDefault();
            var ManagerBackup = (from item in db.DepartmentTBs where item.DeptName == departmentName select item.DeptBackupManager).SingleOrDefault();
            var Supervisor = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepTeamLeader).SingleOrDefault();
            var SupervisorMail = (from item in db.accountTBs where item.EmpName == Supervisor select item.Email).SingleOrDefault();
            if (ManagerBackup == null)
            {
                req.ReqStatus = "ApprovedByManager";
                req.SupervisorApprovedOn = DateTime.Now;
                req.SupervisorApprove = "true";

            }
            else
            {
                req.ReqStatus = "ApprovedByManager";
                req.MyBackupManagerApproval = "true";
                req.MyBackupManagerApprovedOn = DateTime.Now;
            }
            string result = ApproveDayByDayForSupervisor(ID);//here uses this method to transfer the request to the higher authority
            //which is dupty manager or manager or backup manager
            db.SaveChanges();
            message = "Request Approved By Supervisor Successfully";
            SendMail_VacationApproved_BySupervisor(req.UserName, UserMail, Supervisor, TeamLeaderMail, ManagerMail, SupervisorMail, DateTime.Parse(req.DurationFrom.ToString()));
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RejectedBySupervisor(int ID)
        {
            Request req = db.Requests.Find(ID);
            req.ReqStatus = "RejectedByManager";
            //req.TeamLeadApproveDate = DateTime.Now;
            req.SupervisorApprovedOn = DateTime.Now;
            req.TeamLeaderApprove = "false";
            req.ActionTaken = "yes";
            req.ApprovedBySupervisor = (string)(Session["UserName"]);
            var SubdepartmentName = (from item in db.accountTBs where item.EmpName == req.UserName select item.SubDepartmentName).SingleOrDefault();
            var teamleader = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepBackupTeamLeader).SingleOrDefault();
            var TeamLeaderMail = (from item in db.accountTBs where item.EmpName == teamleader select item.Email).SingleOrDefault();
            var Supervisor = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepTeamLeader).SingleOrDefault();
            var SupervisorMail = (from item in db.accountTBs where item.EmpName == Supervisor select item.Email).SingleOrDefault();
            var UserMail = (from item in db.accountTBs where item.EmpName == req.UserName select item.Email).SingleOrDefault();
            string result = RejectDayByDayForSupervisor(ID);
            db.SaveChanges();
            increase_vacation_count(ID);
            message = "Request Rejected By Supervisor Succesfully";
            SendMail_VacationRejected_BySupervisor(req.UserName, UserMail, Supervisor, TeamLeaderMail, SupervisorMail, DateTime.Parse(req.DurationFrom.ToString()));
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ApproveByManager(int ID)
        {
            var msg = "";
            Request req = db.Requests.Find(ID);
            string ApprovePerson = (string)Session["UserName"];
            if ((string)Session["UserType"] == "Deputy Manager")
            {
                req.MyBackupManagerApprovedOn = DateTime.Now;
                req.MyBackupManagerApproval = "true";
                req.ActionTaken = "yes";
            }
            else if ((string)Session["UserType"] == "Manager")
            {
                req.ManagerApproveDate = DateTime.Now;
                req.ManagerApprove = "true";
                req.ActionTaken = "yes";
            }
            req.ReqStatus = "ApprovedByManager";
            var RequestSubType = req.ReqSubType; //نوع الاجازة
            var RequestUserName = req.UserName;
            var reqDuration = req.ReqDuration;
            string result = ApproveDayByDayForManager(ID);//excutes the request in request handler table and sets 
            //the status "ApprovedByManager"
            var AccountID = (from p in db.accountTBs where p.EmpName == RequestUserName select p.ID).FirstOrDefault();
            accountTB acc = db.accountTBs.Find(AccountID);
            var departmentName = (from item in db.accountTBs where item.EmpName == req.UserName select item.DepartmentName).SingleOrDefault();
            var manager = (from p in db.DepartmentTBs where p.DeptName == departmentName select p.DeptManager).SingleOrDefault();
            var managermail = (from item in db.accountTBs where item.EmpName == ApprovePerson select item.Email).SingleOrDefault();
            var UserMail = (from item in db.accountTBs where item.EmpName == req.UserName select item.Email).SingleOrDefault();

            //NEW
            DailyPointsReport OtherCasualVac = new DailyPointsReport();
            var ReqH = db.RequestHandlers.Where(x => x.OriginalRequestID == ID);
            string offday;
            DateTime? OffdayOtherCasual = ReqH.FirstOrDefault().Offday;
            DateTime offfday = Convert.ToDateTime(OffdayOtherCasual);
            offday = offfday.ToShortDateString();

            if (RequestSubType == "عارضه - أخري")
            {


                if ((double)req.ReqDuration == 0.50)
                {
                    acc.RedPoints += 5;
                    OtherCasualVac.GreenPoints = 0;
                    OtherCasualVac.RedPoints = 5;
                    OtherCasualVac.Comment = " أجازة عارضة أخرى يوم" + offday;
                    OtherCasualVac.EvaluationDate = DateTime.Now;
                    OtherCasualVac.UserName = req.UserName;
                    OtherCasualVac.Creator = "HR System";
                    OtherCasualVac.RatingDate = DateTime.Now.ToShortDateString();
                    db.DailyPointsReports.Add(OtherCasualVac);
                    db.SaveChanges();

                }
                if ((double)req.ReqDuration == 1.00)
                {
                    acc.RedPoints += 10;
                    db.SaveChanges();

                    OtherCasualVac.GreenPoints = 0;
                    OtherCasualVac.RedPoints = 10;
                    OtherCasualVac.Comment = " أجازة عارضة أخرى يوم " + DateTime.Now.ToShortDateString();
                    OtherCasualVac.EvaluationDate = DateTime.Now;
                    OtherCasualVac.UserName = req.UserName;
                    OtherCasualVac.Creator = "HR System";
                    OtherCasualVac.RatingDate = DateTime.Now.ToShortDateString();
                    db.DailyPointsReports.Add(OtherCasualVac);
                }
                msg = "Manager Approved Request";
                SendMail_OtherCasualVacationApproved_ByManager(RequestUserName, UserMail, ApprovePerson, managermail, DateTime.Parse(req.DurationFrom.ToString()));
            }




            //Break
            else if (RequestSubType == "اعتيادية")
            {

                msg = "Manager Approved Request";
                SendMail_VacationApproved_ByManager(RequestUserName, UserMail, ApprovePerson, managermail, DateTime.Parse(req.DurationFrom.ToString()));



            }

            //END

            else if (RequestSubType == "عارضه")
            {

                msg = "Manager Approved Request";
                SendMail_VacationApproved_ByManager(RequestUserName, UserMail, ApprovePerson, managermail, DateTime.Parse(req.DurationFrom.ToString()));



            }

            else if (RequestSubType == "مرضية")
            {

                msg = "Manager Approved Request";
                SendMail_VacationApproved_ByManager(RequestUserName, UserMail, ApprovePerson, managermail, DateTime.Parse(req.DurationFrom.ToString()));




            }

            else
            {
                msg = "Manager Approved Request";
                SendMail_VacationApproved_ByManager(RequestUserName, UserMail, ApprovePerson, managermail, DateTime.Parse(req.DurationFrom.ToString()));

            }
            db.SaveChanges();
            message = msg;
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ApproveOnRevokeBySupervisor(int ID)
        {
            var msg = "";
            Request req = db.Requests.Find(ID);
            var departmentName = (from item in db.accountTBs where item.EmpName == Name select item.DepartmentName).SingleOrDefault();
            var SubdepartmentName = (from item in db.accountTBs where item.EmpName == Name select item.SubDepartmentName).SingleOrDefault();
            req.ReqStatus = "ApprovedOnRevokeBySupervisor";
            var RequestSubType = req.ReqSubType;
            var RequestUserName = req.UserName;
            var reqDuration = req.ReqDuration;
            var Supervisor = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepTeamLeader).SingleOrDefault();
            var SupervisorMail = (from item in db.accountTBs where item.EmpName == Supervisor select item.Email).SingleOrDefault();
            var UserMail = (from item in db.accountTBs where item.EmpName == Name select item.Email).SingleOrDefault();
            string result = ApproveOnRevokeDayByDayBySupervisor(ID);
            var AccountID = (from p in db.accountTBs where p.EmpName == RequestUserName select p.ID).FirstOrDefault();
            accountTB acc = db.accountTBs.Find(AccountID);
            if (RequestSubType == "اعتيادية")
            {
                acc.AnnualVac = acc.AnnualVac + reqDuration;
                if (acc.MaximumNumberofCasualExceed == "true")
                {
                    acc.MaximumNumberofCasualExceed = "false";
                }
                SendMail_Revoke_Approved_By_Supervisor(req.UserName, UserMail, Supervisor, SupervisorMail, DateTime.Parse(req.DurationFrom.ToString()));
                msg = "Supervisor Approved Request";
            }
            db.SaveChanges();
            message = msg;
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ApproveOnRevokeByManager(int ID)
        {

            var msg = "";
            Request req = db.Requests.Find(ID);
            if ((string)Session["UserType"] == "Deputy Manager")
            {
                req.MyBackupManagerApprovedOn = DateTime.Now;
                req.MyBackupManagerApproval = "true";
                req.ActionTaken = "yes";
            }
            req.ReqStatus = "ApprovedOnRevokeByManager";
            req.ActionTaken = "yes";
            var RequestSubType = req.ReqSubType;
            var RequestUserName = req.UserName;
            var reqDuration = req.ReqDuration;
            var UserMail = (from item in db.accountTBs where item.EmpName == Name select item.Email).SingleOrDefault();
            string result = ApproveOnRevokeDayByDayByManager(ID);
            var AccountID = (from p in db.accountTBs where p.EmpName == RequestUserName select p.ID).FirstOrDefault();
            accountTB acc = db.accountTBs.Find(AccountID);
            var ManagerMail = (from item in db.accountTBs where item.EmpName == req.MyManager select item.Email).SingleOrDefault();
            if (RequestSubType == "اعتيادية")
            {
                //if (acc.AnnualVac > 0)
                //{
                acc.AnnualVac = acc.AnnualVac + reqDuration;
                if (acc.MaximumNumberofCasualExceed == "true")
                {
                    acc.MaximumNumberofCasualExceed = "false";
                }
                msg = "Manager Approved Request";
                SendMail_Revoke_Approved_By_Manager(req.UserName, UserMail, req.MyManager, ManagerMail, DateTime.Parse(req.DurationFrom.ToString()));

                //  }

                //else
                //{
                //    msg = "No Annual Vacations Remaining";
                //}
            }
            db.SaveChanges();
            message = msg;
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RejectedRevokeBySupervisor(int ID)
        {
            Request req = db.Requests.Find(ID);
            req.ReqStatus = "RevokeRejectedBySupervisor";
            string result = RejectOnRevokeDayByDayBySupervisor(ID);
            var departmentName = (from item in db.accountTBs where item.EmpName == Name select item.DepartmentName).SingleOrDefault();
            var SubdepartmentName = (from item in db.accountTBs where item.EmpName == Name select item.SubDepartmentName).SingleOrDefault();
            var Supervisor = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepTeamLeader).SingleOrDefault();
            var SupervisorMail = (from item in db.accountTBs where item.EmpName == Supervisor select item.Email).SingleOrDefault();
            var UserMail = (from item in db.accountTBs where item.EmpName == Name select item.Email).SingleOrDefault();
            db.SaveChanges();
            message = "Revoke Rejected By Supervisor Succesfully";
            SendMail_Revoke_Rejected_By_Supervisor(req.UserName, UserMail, Supervisor, SupervisorMail, DateTime.Parse(req.DurationFrom.ToString()));
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RejectedRevokeByManager(int ID)
        {
            Request req = db.Requests.Find(ID);
            req.ReqStatus = "RevokeRejectedByManager";
            string result = RejectOnRevokeDayByDayByManager(ID);
            var departmentName = (from item in db.accountTBs where item.EmpName == Name select item.DepartmentName).SingleOrDefault();
            var SubdepartmentName = (from item in db.accountTBs where item.EmpName == Name select item.SubDepartmentName).SingleOrDefault();
            var UserMail = (from item in db.accountTBs where item.EmpName == Name select item.Email).SingleOrDefault();
            var Supervisor = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepTeamLeader).SingleOrDefault();
            var SupervisorMail = (from item in db.accountTBs where item.EmpName == Supervisor select item.Email).SingleOrDefault();
            var ManagerMail = (from item in db.accountTBs where item.EmpName == req.MyManager select item.Email).SingleOrDefault();
            db.SaveChanges();
            message = "Revoke Rejected By Manager Succesfully";
            SendMail_Revoke_Rejected_By_Manager(req.UserName, UserMail, req.MyManager, ManagerMail, DateTime.Parse(req.DurationFrom.ToString()));
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RejectedByManager(int ID)
        {
            Request req = db.Requests.Find(ID);
            req.ReqStatus = "RejectedByManager";
            req.ManagerApproveDate = DateTime.Now;
            req.ManagerApprove = "false";
            req.ActionTaken = "yes";
            string result = RejectDayByDayForManager(ID);
            var departmentName = (from item in db.accountTBs where item.EmpName == req.UserName select item.DepartmentName).SingleOrDefault();
            var manager = (from p in db.DepartmentTBs where p.DeptName == departmentName select p.DeptManager).SingleOrDefault();
            var managermail = (from item in db.accountTBs where item.EmpName == manager select item.Email).SingleOrDefault();
            var UserMail = (from item in db.accountTBs where item.EmpName == req.UserName select item.Email).SingleOrDefault();
            SendMail_VacationRejected_ByManager(req.UserName, UserMail, req.MyManager, managermail, DateTime.Parse(req.DurationFrom.ToString()));
            db.SaveChanges();
            increase_vacation_count(ID);
            message = "Request Rejected By Manager Succesfully";
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RevokeByNormalUser(int ID)
        {
            Request req = db.Requests.Find(ID);
            DateTime start = req.DurationFrom.GetValueOrDefault().Date;
            var teamleader = (from p in db.SubDeps where p.SubDepartmentName == req.DepartmentName select p.DepBackupTeamLeader).SingleOrDefault();
            var departmentName = req.ManagmentName;
            var DuputyManager = (from p in db.DepartmentTBs where p.DeptName == departmentName select p.DeptBackupManager).SingleOrDefault();
            var SubdepartmentName = (from item in db.accountTBs where item.EmpName == Name select item.SubDepartmentName).SingleOrDefault();
            var Supervisor = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepTeamLeader).SingleOrDefault();
            var SupervisorMail = (from item in db.accountTBs where item.EmpName == Supervisor select item.Email).SingleOrDefault();
            var UserMail = (from item in db.accountTBs where item.EmpName == Name select item.Email).SingleOrDefault();
            if (teamleader == null)
            {
                if (DuputyManager == null)
                {
                    if (start <= DateTime.Now.Date)
                    {
                        req.ReqStatus = "PendingManagerApprovalOnRevoke";
                        string result = RevokeDayByDayByNormalUser(ID, req.ReqStatus);
                        db.SaveChanges();
                        message = "Request is pending Manager Approval On The Revoke";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        message = "You Can't Revoke this Request , Check Rules";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    if (start <= DateTime.Now.Date)
                    {
                        req.ReqStatus = "PendingDuputyManagerApprovalOnRevoke";
                        string result = RevokeDayByDayByNormalUser(ID, req.ReqStatus);
                        db.SaveChanges();
                        message = "Request is Duputy Manager Approval On The Revoke";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        message = "You Can't Revoke this Request , Check Rules";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                if (DuputyManager == null)
                {
                    if (start <= DateTime.Now.Date)
                    {
                        req.ReqStatus = "RevokedByUser";
                        string result = RevokeDayByDayByNormalUser(ID, req.ReqStatus);
                        db.SaveChanges();
                        message = "Request is pending Manager Approval On The Revoke";
                        SendMail_VacationRevoked_By_NormalUserOrTeamLeader(req.UserName, UserMail, Supervisor, SupervisorMail, DateTime.Parse(req.DurationFrom.ToString()));
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        message = "You Can't Revoke this Request , Check Rules";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    //if (start == DateTime.Now.Date)
                    if (start != null)
                    {
                        req.ReqStatus = "PendingDuputyManagerApprovalOnRevoke";
                        string result = RevokeDayByDayByNormalUser(ID, req.ReqStatus);
                        db.SaveChanges();
                        message = "Request is Duputy Manager Approval On The Revoke";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        message = "You Can't Revoke this Request , Check Rules";
                        return Json(message, JsonRequestBehavior.AllowGet);
                    }
                }
            }



        }
        public JsonResult DeleteVacation(int ID)
        {
            string message = "";
            try
            {
                Request req = db.Requests.Find(ID);
                increase_vacation_count(ID);
                db.Requests.Remove(req);
                db.SaveChanges();
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
        public JsonResult RevokeByTeamLeader(int ID)
        {
            Request req = db.Requests.Find(ID);
            DateTime start = req.DurationFrom.GetValueOrDefault().Date;
            var teamleader = (from p in db.SubDeps where p.SubDepartmentName == req.DepartmentName select p.DepBackupTeamLeader).SingleOrDefault();
            var departmentName = req.ManagmentName;
            var DuputyManager = (from p in db.DepartmentTBs where p.DeptName == departmentName select p.DeptBackupManager).SingleOrDefault();
            var SubdepartmentName = (from item in db.accountTBs where item.EmpName == Name select item.SubDepartmentName).SingleOrDefault();
            var Supervisor = (from p in db.SubDeps where p.SubDepartmentName == SubdepartmentName select p.DepTeamLeader).SingleOrDefault();
            var SupervisorMail = (from item in db.accountTBs where item.EmpName == Supervisor select item.Email).SingleOrDefault();
            var UserMail = (from item in db.accountTBs where item.EmpName == Name select item.Email).SingleOrDefault();
            if (start == DateTime.Now.Date)
            {
                req.ReqStatus = "RevokedByTeamLeader";
                string result = RevokeDayByDayByTeamLeader(ID);
                db.SaveChanges();
                SendMail_VacationRevoked_By_NormalUserOrTeamLeader(req.UserName, UserMail, Supervisor, SupervisorMail, DateTime.Parse(req.DurationFrom.ToString()));
                message = "Request is Pending Supervisor Approval On The Revoke";

                return Json(message, JsonRequestBehavior.AllowGet);
            }

            else
            {
                message = "You Can't Revoke this Request , Check Rules";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult RevokeBySupervisor(int ID)
        {
            Request req = db.Requests.Find(ID);
            DateTime start = req.DurationFrom.GetValueOrDefault().Date;
            var departmentName = req.ManagmentName;
            var DuputyManager = (from p in db.DepartmentTBs where p.DeptName == departmentName select p.DeptBackupManager).SingleOrDefault();
            var UserMail = (from item in db.accountTBs where item.EmpName == Name select item.Email).SingleOrDefault();
            var ManagerMail = (from item in db.accountTBs where item.EmpName == req.MyManager select item.Email).SingleOrDefault();
            if (start == DateTime.Now.Date)
            {
                if (DuputyManager == null)
                {
                    req.ReqStatus = "RevokedBySupervisor";
                    string result = RevokeDayByDayBySupervisor(ID, req.ReqStatus);
                    db.SaveChanges();
                    message = "Request is Pending Manager Approval On The Revoke";
                    SendMail_VacationRevoked_By_SupervisorOrDuputyManager(req.UserName, UserMail, req.MyManager, ManagerMail, DateTime.Parse(req.DurationFrom.ToString()));

                    return Json(message, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    req.ReqStatus = "PendingDuputyManagerApprovalOnRevoke";
                    string result = RevokeDayByDayBySupervisor(ID, req.ReqStatus);
                    db.SaveChanges();
                    message = "Request is Pending Duputy Manager Approval On The Revoke";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }

            else
            {
                message = "You Can't Revoke this Request , Check Rules";
                return Json(message, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Report(string type, int ID)
        {
            var OriginalRequest = db.Requests.Find(ID);
            if (OriginalRequest.ReqStatus == "ApprovedByManager" || OriginalRequest.ReqStatus == "ManagerRequest" || OriginalRequest.ReqSubType == "مرضية")
            {
                LocalReport lr = new LocalReport();
                string path = Path.Combine(Server.MapPath("~/Reports"), "Vacation.rdlc");
                if (System.IO.File.Exists(path))
                {
                    lr.ReportPath = path;
                }
                else
                {
                    return View("ManageRequest");
                }
                List<Request> cm = new List<Request>();
                cm = db.Requests.Where(a => a.ID.Equals(ID)).ToList();
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
        public void CheckDays(DateTime start, DateTime end, string Requesttype, string annualtype, out List<DateTime> offdays, out DateTime backtoworkdate, out decimal numberofdays)
        {
            try
            {
                var dateInterval = new List<DateTime>();
                var daysInWeek = new List<DayOfWeek>();
                var dayswithoutWeekEnd = new List<DateTime>();
                for (var dt = DateTime.Parse(start.ToString()).Date; dt <= DateTime.Parse(end.ToString()).Date; dt = dt.AddDays(1))
                {
                    dateInterval.Add(dt);
                    daysInWeek.Add(dt.DayOfWeek);
                }
                for (int j = 0; j < daysInWeek.Count; j++)
                {

                    if (daysInWeek[j] == DayOfWeek.Friday)
                    {
                        dateInterval.RemoveAt(j);
                        daysInWeek.RemoveAt(j);
                    }
                }


                for (int j = 0; j < daysInWeek.Count; j++)
                {
                    if (daysInWeek[j] == DayOfWeek.Saturday)
                    {
                        dateInterval.RemoveAt(j);
                        daysInWeek.RemoveAt(j);
                    }
                }


                dayswithoutWeekEnd = dateInterval;

                var Holidays = db.OffHolidays.ToList();
                for (int i = 0; i < dayswithoutWeekEnd.Count; i++)
                {
                    for (int j = 0; j < Holidays.Count; j++)
                    {
                        if (Holidays[j].OfficialDate.Value.Date == dayswithoutWeekEnd[i])
                        {
                            dayswithoutWeekEnd.RemoveAt(i);
                        }
                    }
                }

                var OffWorkDays = dayswithoutWeekEnd;
                offdays = OffWorkDays;
                DateTime backtoworkexactdate = Backtoworkdate(OffWorkDays, start, end);
                if (backtoworkexactdate.Date == DateTime.MinValue.Date)
                {
                    backtoworkdate = DateTime.MinValue.Date;
                    numberofdays = 0;
                }
                else
                {
                    backtoworkdate = backtoworkexactdate;
                    numberofdays = 0;
                }
                if (start == end)
                {
                    if (Requesttype == "اعتيادية" || Requesttype == "عارضه - أخري")
                    {
                        if (annualtype == "1 Day")
                        {
                            numberofdays = 1;

                        }
                        else if (annualtype == "1/2 Day AM")
                        {
                            numberofdays = Convert.ToDecimal(0.5);
                            backtoworkdate = start.Date;

                        }
                        else if (annualtype == "1/2 Day PM")
                        {
                            numberofdays = Convert.ToDecimal(0.5);

                        }

                    }
                    else if (Requesttype == "عارضه")
                    {
                        if (annualtype == "1 Day")
                        {
                            numberofdays = 1;

                        }
                        else if (annualtype == "1/2 Day AM")
                        {
                            numberofdays = Convert.ToDecimal(0.5);
                            backtoworkdate = start.Date;

                        }
                        else if (annualtype == "1/2 Day PM")
                        {
                            numberofdays = Convert.ToDecimal(0.5);

                        }

                    }
                    else if (Requesttype == "بالخصم")
                    {

                        if (annualtype == "1 Day")
                        {
                            numberofdays = 1;

                        }
                        else if (annualtype == "1/2 Day AM")
                        {
                            numberofdays = Convert.ToDecimal(0.5);
                            backtoworkdate = start.Date;

                        }
                        else if (annualtype == "1/2 Day PM")
                        {
                            numberofdays = Convert.ToDecimal(0.5);

                        }
                    }
                    else if (Requesttype == "مرضية")
                    {

                        if (annualtype == "1 Day")
                        {
                            numberofdays = 1;

                        }
                        else if (annualtype == "1/2 Day AM")
                        {
                            numberofdays = Convert.ToDecimal(1);
                            backtoworkdate = start.Date;

                        }
                        else if (annualtype == "1/2 Day PM")
                        {
                            numberofdays = Convert.ToDecimal(1);

                        }
                    }
                    else if (Requesttype == "أخري")
                    {

                        if (annualtype == "1 Day")
                        {
                            numberofdays = 1;

                        }
                        else if (annualtype == "1/2 Day AM")
                        {
                            numberofdays = Convert.ToDecimal(0.5);
                            backtoworkdate = start.Date;

                        }
                        else if (annualtype == "1/2 Day PM")
                        {
                            numberofdays = Convert.ToDecimal(0.5);

                        }
                    }
                }
                else
                {
                    numberofdays = offdays.Count;
                    backtoworkdate = backtoworkexactdate;

                }
            }
            catch
            {
                backtoworkdate = DateTime.MinValue.Date;
                numberofdays = 0;
                offdays = null;
            }
        }
        public DateTime Backtoworkdate(List<DateTime> offdays, DateTime start, DateTime end)
        {
            if (end < start)
            {
                return DateTime.MinValue.Date;
            }
            else
            {
                try
                {
                    DateTime endofvacation = offdays.Max();
                    var Holidays = db.OffHolidays.ToList();
                    DateTime backtoworkdate = endofvacation.AddDays(1);

                    Valid: if (backtoworkdate.DayOfWeek == DayOfWeek.Friday)
                    {
                        backtoworkdate = backtoworkdate.AddDays(2);
                    }

                    else if (backtoworkdate.DayOfWeek == DayOfWeek.Saturday)
                    {
                        backtoworkdate = backtoworkdate.AddDays(1);
                        if (backtoworkdate.DayOfWeek == DayOfWeek.Friday)
                        {
                            backtoworkdate.AddDays(2);
                            goto Valid;
                        }
                        else if (backtoworkdate.DayOfWeek == DayOfWeek.Saturday)
                        {
                            backtoworkdate.AddDays(1);
                            goto Valid;
                        }
                    }

                    for (int i = 0; i < Holidays.Count; i++)
                    {
                        if (Holidays[i].OfficialDate.Value.Date == backtoworkdate)
                        {
                            backtoworkdate = backtoworkdate.AddDays(1);
                            goto Valid;
                        }
                    }
                    return backtoworkdate.Date;
                }

                catch
                {
                    return DateTime.MinValue.Date;
                }
            }


        }
        public void InsertOffdays(List<DateTime> days, string username, string holidaytype, string reqstat, decimal duration, string daytype, string dep, string subdep, int requestID)
        {

            for (int i = 0; i < days.Count; i++)
            {

                RequestHandler RH = new RequestHandler();
                RH.RequestType = "إجازة";
                RH.RequestSubType = holidaytype;
                RH.UserName = username;
                RH.Offday = days[i];
                RH.ReqStatus = reqstat;
                RH.ReqDuration = duration;
                RH.HalfDayOrFullDay = daytype;
                RH.Department = dep;
                RH.SubDepartment = subdep;
                RH.OriginalRequestID = requestID;
                db.RequestHandlers.Add(RH);
                db.SaveChanges();
            }
        }
        public bool checkifofficialdayorWeekend(DateTime startdate, DateTime enddate)
        {
            bool isitholiday = false;
            var holiday = db.OffHolidays.ToList();


            if (startdate.DayOfWeek == DayOfWeek.Friday || startdate.DayOfWeek == DayOfWeek.Saturday)
            {
                isitholiday = true;
            }

            if (enddate.DayOfWeek == DayOfWeek.Friday || enddate.DayOfWeek == DayOfWeek.Saturday)
            {
                isitholiday = true;
            }

            for (int j = 0; j < holiday.Count; j++)
            {
                if (holiday[j].OfficialDate.Value.Date == startdate)
                {
                    isitholiday = true;
                }
            }
            for (int j = 0; j < holiday.Count; j++)
            {
                if (holiday[j].OfficialDate.Value.Date == enddate)
                {
                    isitholiday = true;
                }
            }


            return isitholiday;
        }
        public bool checkifenddateandstartdateisvalid(DateTime startdate, DateTime enddate)
        {
            bool isitholiday = false;


            if (enddate.Date >= startdate.Date)
            {
                isitholiday = false;
            }

            else
            {
                isitholiday = true;
            }



            return isitholiday;
        }
        public bool checkifitsalreadyholiday(DateTime startdate, DateTime enddate, string username)
        {
            bool alreadyholiday = false;
            var prevHolidays = db.RequestHandlers.Where(x => x.UserName == username).Where(x => x.RequestType == "إجازة").Where(x => x.ReqStatus == "ApprovedByManager" || x.ReqStatus == "PendingTeamLeaderApproval" || x.ReqStatus == "PendingDuputyManagerApproval" || x.ReqStatus == "PendingSeniorApproval" || x.ReqStatus == "PendingSupervisorApproval" || x.ReqStatus == "PendingSupervisorBackupApproval" || x.ReqStatus == "PendingSupervisorApproval" || x.ReqStatus == "PendingManagerApproval").ToList();
            for (int i = 0; i < prevHolidays.Count; i++)
            {
                if (startdate.Date == prevHolidays[i].Offday.Value.Date || enddate.Date == prevHolidays[i].Offday.Value.Date)
                {
                    alreadyholiday = true;
                }
            }

            return alreadyholiday;
        }
        public bool checkifitsalreadyholidayForExuse(DateTime startdate, DateTime enddate, string username)
        {
            bool alreadyholiday = false;
            var prevHolidays = db.RequestHandlers.Where(x => x.UserName == username).Where(x => x.RequestType == "إجازة").Where(x => x.ReqStatus == "ApprovedByManager" || x.ReqStatus == "PendingTeamLeaderApproval" || x.ReqStatus == "PendingDuputyManagerApproval" || x.ReqStatus == "PendingSeniorApproval" || x.ReqStatus == "PendingSupervisorApproval" || x.ReqStatus == "PendingSupervisorBackupApproval" || x.ReqStatus == "PendingSupervisorApproval" || x.ReqStatus == "PendingManagerApproval").ToList();
            for (int i = 0; i < prevHolidays.Count; i++)
            {
                if (startdate.Date == prevHolidays[i].Offday.Value.Date || enddate.Date == prevHolidays[i].Offday.Value.Date)
                {
                    if (prevHolidays[i].ReqDuration.Value == Convert.ToDecimal(0.50))
                    {
                        alreadyholiday = false;
                    }
                    else
                    {
                        alreadyholiday = true;
                    }
                }
            }

            return alreadyholiday;
        }
        public bool checkifitsalreadyExcuse(DateTime startdate, DateTime enddate, string username, decimal Duration)
        {
            bool alreadyholiday = false;
            var prevExcuse = db.RequestHandlers.Where(x => x.UserName == username).Where(x => x.RequestType == "Excuse").Where(x => x.ReqStatus == "PendingTeamLeaderApproval" || x.ReqStatus == "PendingManagerApproval" || x.ReqStatus == "PendingSupervisorApproval" || x.ReqStatus == "Approved" || x.ReqStatus == "PendingDuputyManagerApproval" || x.ReqStatus == "PendingSeniorApproval" || x.ReqStatus == "PendingSupervisorApproval" || x.ReqStatus == "PendingSupervisorBackupApproval").ToList();
            for (int i = 0; i < prevExcuse.Count; i++)
            {
                if (startdate.Date == prevExcuse[i].Offday.Value.Date || enddate.Date == prevExcuse[i].Offday.Value.Date)
                {

                    alreadyholiday = true;
                }
            }

            return alreadyholiday;
        }
        public bool CheckifItsValidAnnualVacationRequest(DateTime startdate)
        {
            bool valid = true;
            if (startdate.Date > DateTime.Now.Date)
            {
                valid = false;
            }

            return valid;
        }
        public bool checkifPassedAuthorities(string username)
        {
            bool checkedForAuthorities = false;
            type = (string)(Session["UserType"]);
            var account = (from p in db.accountTBs where p.EmpName == username select p).SingleOrDefault();
            if (account.DelegatedAuthorities == "yes")
            {
                checkedForAuthorities = false;
            }
            else
            {
                if (type == "TeamLeader")
                {
                    if (account.TeamLeaderRole == "yes")
                    {
                        checkedForAuthorities = false;
                    }
                    else
                    {
                        checkedForAuthorities = true;
                    }
                }
                else if (type == "Supervisor")
                {
                    if (account.SupervisorRole == "yes")
                    {
                        checkedForAuthorities = false;
                    }
                    else
                    {
                        checkedForAuthorities = true;
                    }
                }
                else if (type == "Deputy Manager")
                {
                    if (account.ManagerRole == "Yes")
                    {
                        checkedForAuthorities = false;
                    }
                    else
                    {
                        checkedForAuthorities = true;
                    }
                }
            }
            return checkedForAuthorities;
        }
        public bool CheckifItsValidCasualVacationRequest(DateTime startdate, decimal reqduration, string username, string halfdaytype, DateTime backdate)
        {
            bool Invalid = false;
            var prevHolidays = db.RequestHandlers.Where(x => x.UserName == username).Where(x => x.RequestType == "إجازة").Where(x => x.RequestSubType == "مرضية").Where(x => x.ReqStatus == "ApprovedByManager").ToList();
            if (reqduration == Convert.ToDecimal(0.5))
            {
                if (halfdaytype == "1/2 Day AM")
                {
                    if (startdate.Date == DateTime.Now.Date)
                    {

                        Invalid = false;
                    }

                    else
                    {
                        Invalid = true;

                    }

                }
                else if (halfdaytype == "1/2 Day PM")
                {
                    if (backdate.Date == DateTime.Now.Date)
                    {

                        Invalid = false;

                    }

                }

                else
                {
                    Invalid = true;

                }
            }
            else if (reqduration == 1)
            {
                if (startdate.Date.DayOfWeek == DayOfWeek.Thursday)
                {
                    DateTime nearestnormaldaywork = getnearstworkdayAfterDay(startdate.Date);

                    if (nearestnormaldaywork == DateTime.Now.Date)
                    {
                        Invalid = false;
                    }

                    else if (nearestnormaldaywork != DateTime.Now.Date)
                    {
                        if (prevHolidays.Count == 0)
                        {
                            Invalid = true;
                        }
                        else
                        {
                            for (int i = 0; i < prevHolidays.Count; i++)
                            {
                                if (nearestnormaldaywork == prevHolidays[i].Offday.Value.Date)
                                {
                                    Invalid = false;
                                }

                            }
                        }
                    }

                    else
                    {
                        Invalid = true;
                    }
                }

                else
                {
                    if (startdate.Date == DateTime.Now.Date.AddDays(-1) || startdate.Date == DateTime.Now.Date)
                    {
                        Invalid = false;
                    }
                    else if (startdate.Date != DateTime.Now.Date.AddDays(-1))
                    {
                        if (prevHolidays.Count == 0)
                        {
                            Invalid = true;
                        }
                        else
                        {
                            for (int i = 0; i < prevHolidays.Count; i++)
                            {
                                if (startdate.AddDays(1) == prevHolidays[i].Offday.Value.Date)
                                {
                                    Invalid = false;

                                }
                            }
                        }
                    }

                    else
                    {
                        Invalid = true;

                    }
                }
            }

            else if (reqduration == 2)
            {
                if (startdate.Date.DayOfWeek == DayOfWeek.Thursday)
                {
                    DateTime nearestnormaldaywork = getnearstworkdayAfterTwoDay(startdate.Date);
                    if (nearestnormaldaywork == DateTime.Now.Date)
                    {
                        Invalid = false;
                    }
                    else if (nearestnormaldaywork != DateTime.Now.Date)
                    {
                        if (prevHolidays.Count == 0)
                        {
                            Invalid = true;
                        }
                        else
                        {
                            for (int i = 0; i < prevHolidays.Count; i++)
                            {
                                if (nearestnormaldaywork == prevHolidays[i].Offday.Value.Date)
                                {
                                    Invalid = false;

                                }
                            }
                        }
                    }

                    else
                    {
                        Invalid = true;
                    }
                }
                else
                {
                    DateTime nearestnormaldaywork = getnearstworkdayAfterTwoDay(startdate.Date);

                    if (nearestnormaldaywork == DateTime.Now.Date.AddDays(-2) || nearestnormaldaywork == DateTime.Now.Date.AddDays(-1) || nearestnormaldaywork == DateTime.Now.Date)
                    {
                        Invalid = false;
                    }
                    else if (nearestnormaldaywork != DateTime.Now.Date.AddDays(-2) || nearestnormaldaywork != DateTime.Now.Date.AddDays(-1) || nearestnormaldaywork != DateTime.Now.Date)
                    {
                        if (prevHolidays.Count == 0)
                        {
                            Invalid = true;
                        }
                        else
                        {
                            for (int i = 0; i < prevHolidays.Count; i++)
                            {
                                if (nearestnormaldaywork == prevHolidays[i].Offday.Value.Date)
                                {
                                    Invalid = false;

                                }
                            }
                        }

                    }

                    else
                    {
                        Invalid = true;

                    }
                }
            }

            return Invalid;
        }
        public DateTime getnearstworkdayAfterDay(DateTime day)
        {
            var holiday = db.OffHolidays.ToList();
            DateTime datetovalidate = day;
            datetovalidate = datetovalidate.AddDays(1);

            if (datetovalidate.DayOfWeek == DayOfWeek.Friday)
            {
                datetovalidate = datetovalidate.AddDays(2);
                for (int j = 0; j < holiday.Count; j++)
                {
                    if (holiday[j].OfficialDate.Value.Date == datetovalidate.Date)
                    {
                        datetovalidate = day.AddDays(1);
                    }
                }
                return datetovalidate;
            }

            else if (datetovalidate.DayOfWeek == DayOfWeek.Saturday)
            {
                datetovalidate = datetovalidate.AddDays(1);
                for (int j = 0; j < holiday.Count; j++)
                {
                    if (holiday[j].OfficialDate.Value.Date == datetovalidate.Date)
                    {
                        datetovalidate = day.AddDays(1);
                    }
                }
                return datetovalidate;
            }


            else
            {
                for (int j = 0; j < holiday.Count; j++)
                {
                    if (holiday[j].OfficialDate.Value.Date == datetovalidate.Date)
                    {
                        datetovalidate = day.AddDays(1);
                    }
                }
                return datetovalidate;
            }

        }
        public DateTime getnearstworkdayAfterTwoDay(DateTime day)
        {
            var holiday = db.OffHolidays.ToList();
            DateTime datetovalidate = day;
            datetovalidate = datetovalidate.AddDays(2);

            if (datetovalidate.DayOfWeek == DayOfWeek.Friday)
            {
                datetovalidate = datetovalidate.AddDays(2);
                for (int j = 0; j < holiday.Count; j++)
                {
                    if (holiday[j].OfficialDate.Value.Date == datetovalidate.Date)
                    {
                        datetovalidate = day.AddDays(1);
                    }
                }
                return datetovalidate;
            }

            else if (datetovalidate.DayOfWeek == DayOfWeek.Saturday)
            {
                datetovalidate = datetovalidate.AddDays(1);
                for (int j = 0; j < holiday.Count; j++)
                {
                    if (holiday[j].OfficialDate.Value.Date == datetovalidate.Date)
                    {
                        datetovalidate = day.AddDays(1);
                    }
                }
                return datetovalidate;
            }


            else
            {
                for (int j = 0; j < holiday.Count; j++)
                {
                    if (holiday[j].OfficialDate.Value.Date == datetovalidate.Date)
                    {
                        datetovalidate = day.AddDays(1);
                    }
                }
                return datetovalidate;
            }

        }
        public string ApproveDayByDayForTeamLeader(int reqID)
        {
            var requests = (from p in db.RequestHandlers
                            where p.OriginalRequestID == reqID
                            select p
                            ).ToList();
            try
            {
                foreach (var req in requests)
                {
                    req.ReqStatus = "PendingSupervisorApproval";
                }
                db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {

                return "Error";
            }
        }
        public string RejectByDayForTeamLeader(int reqID)
        {
            var requests = (from p in db.RequestHandlers
                            where p.OriginalRequestID == reqID
                            select p
                            ).ToList();
            try
            {
                foreach (var req in requests)
                {
                    req.ReqStatus = "RejectedByTeamLeader";
                }
                db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {

                return "Error";
            }
        }
        public string ApproveDayByDayForSupervisor(int reqID)
        {
            var requests = (from p in db.RequestHandlers
                            where p.OriginalRequestID == reqID
                            select p
                            ).ToList();
            try
            {
                foreach (var req in requests)
                {
                    req.ReqStatus = "PendingManagerApproval";
                }
                db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {

                return "Error";
            }
        }
        public string RejectDayByDayForSupervisor(int reqID)
        {
            var requests = (from p in db.RequestHandlers
                            where p.OriginalRequestID == reqID
                            select p
                       ).ToList();
            try
            {
                foreach (var req in requests)
                {
                    req.ReqStatus = "RejectedBySupervisor";
                }
                db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {

                return "Error";

            }
        }
        public string ApproveDayByDayForManager(int reqID)
        {
            var requests = (from p in db.RequestHandlers
                            where p.OriginalRequestID == reqID
                            select p
                       ).ToList();
            try
            {
                foreach (var req in requests)
                {
                    req.ReqStatus = "ApprovedByManager";
                }
                db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {

                return "Error";

            }
        }
        public string RejectDayByDayForManager(int reqID)
        {

            var requests = (from p in db.RequestHandlers
                            where p.OriginalRequestID == reqID
                            select p
                      ).ToList();
            try
            {
                foreach (var req in requests)
                {
                    req.ReqStatus = "RejectedByManager";
                }
                db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {

                return "Error";
            }
        }
        public string RevokeDayByDayByNormalUser(int reqID, string reqstatus)
        {
            var requests = (from p in db.RequestHandlers
                            where p.OriginalRequestID == reqID
                            select p
          ).ToList();
            try
            {
                foreach (var req in requests)
                {
                    req.ReqStatus = reqstatus;
                }
                db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {

                return "Error";
            }

        }
        public string DeleteDayByDay(int reqID)
        {
            var requests = (from p in db.RequestHandlers
                            where p.OriginalRequestID == reqID
                            select p).ToList();
            try
            {
                foreach (var req in requests)
                {
                    db.RequestHandlers.Remove(req);
                }
                db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {

                return "Error"; ;
            }

        }
        public string RevokeDayByDayByTeamLeader(int reqID)
        {
            var requests = (from p in db.RequestHandlers
                            where p.OriginalRequestID == reqID
                            select p).ToList();
            try
            {
                foreach (var req in requests)
                {
                    req.ReqStatus = "RevokedByTeamLeader";
                }
                db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {

                return "Error";
            }

        }
        public string RevokeDayByDayBySupervisor(int reqID, string reqstatus)
        {
            var requests = (from p in db.RequestHandlers
                            where p.OriginalRequestID == reqID
                            select p).ToList();
            try
            {
                foreach (var req in requests)
                {
                    req.ReqStatus = reqstatus;
                }
                db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {

                return "Error";
            }

        }
        public string ApproveOnRevokeDayByDayBySupervisor(int reqID)
        {
            var requests = (from p in db.RequestHandlers
                            where p.OriginalRequestID == reqID
                            select p).ToList();
            try
            {
                foreach (var req in requests)
                {
                    req.ReqStatus = "ApprovedOnRevokeBySupervisor";
                }
                db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {

                return "Error";
            }

        }
        public string ApproveOnRevokeDayByDayByManager(int reqID)
        {
            var requests = (from p in db.RequestHandlers
                            where p.OriginalRequestID == reqID
                            select p).ToList();
            try
            {
                foreach (var req in requests)
                {
                    req.ReqStatus = "ApprovedOnRevokeByManager";
                }
                db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {

                return "Error";
            }

        }
        public string RejectOnRevokeDayByDayBySupervisor(int reqID)
        {
            var requests = (from p in db.RequestHandlers
                            where p.OriginalRequestID == reqID
                            select p).ToList();
            try
            {
                foreach (var req in requests)
                {
                    req.ReqStatus = "RevokeRejectedBySupervisor";
                }
                db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {

                return "Error";
            }
        }
        public string RejectOnRevokeDayByDayByManager(int reqID)
        {
            var requests = (from p in db.RequestHandlers
                            where p.OriginalRequestID == reqID
                            select p).ToList();
            try
            {
                foreach (var req in requests)
                {
                    req.ReqStatus = "RevokeRejectedByManager";
                }
                db.SaveChanges();
                return "Success";
            }
            catch (Exception ex)
            {

                return "Error";
            }
        }

        public void SendMail_NormalUser_VacationRequested_For_Manager(string user, string usermail, string manager, string managermail, DateTime startdate, DateTime enddate, decimal NumOfDays)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "New Vacation Request";
                message.Body = "Dear  " + manager + " , " + user + " has just requested a \r \t " + NumOfDays.ToString("0.00") + "\r \t day(s) vacation , Start Date : " + startdate.Date.ToString("dd/MM/yyyy") + " Ends at " + enddate.Date.ToString("dd/MM/yyyy") + "";
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
        public void SendMail_NormalUser_VacationRequested_For_ManagerBackup(string user, string usermail, string managerBackup, string managermail, string managerbackupmail, DateTime startdate, DateTime enddate, decimal NumOfDays)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "New Vacation Request";
                message.Body = "Dear  " + managerBackup + " , " + user + " has just requested a \r \t " + NumOfDays.ToString("0.00") + "\r \t day(s) vacation , Start Date : " + startdate.Date.ToString("dd/MM/yyyy") + " Ends at " + enddate.Date.ToString("dd/MM/yyyy") + "";
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
        public void SendMail_NormalUser_VacationRequested_For_Supervisor(string user, string usermail, string supervisor, string supervisormail, string managermail, DateTime startdate, DateTime enddate, decimal NumOfDays)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "New Vacation Request";
                message.Body = "Dear  " + supervisor + " , " + user + " has just requested a \r \t " + NumOfDays.ToString("0.00") + "\r \t day(s) vacation , Start Date : " + startdate.Date.ToString("dd/MM/yyyy") + " Ends at " + enddate.Date.ToString("dd/MM/yyyy") + "";
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
        public void SendMail_NormalUser_VacationRequested_For_SupervisorBackup(string user, string usermail, string supervisorbackup, string supervisorBackupmail, string managermail, string supervisormail, DateTime startdate, DateTime enddate, decimal NumOfDays)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "New Vacation Request";
                message.Body = "Dear  " + supervisorbackup + " , " + user + " has just requested a \r \t " + NumOfDays.ToString("0.00") + "\r \t day(s) vacation , Start Date : " + startdate.Date.ToString("dd/MM/yyyy") + " Ends at " + enddate.Date.ToString("dd/MM/yyyy") + "";
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
        public void SendMail_NormalUser_VacationRequested_For_TeamLeader(string user, string usermail, string teamleader, string teamleadermail, string supervisormail, DateTime startdate, DateTime enddate, decimal NumOfDays)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "New Vacation Request";
                message.Body = "Dear  " + teamleader + " , " + user + " has just requested a \r \t " + NumOfDays.ToString("0.00") + "\r \t day(s) vacation , Start Date : " + startdate.Date.ToString("dd/MM/yyyy") + " Ends at " + enddate.Date.ToString("dd/MM/yyyy") + "";
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
        public void SendMail_NormalUser_VacationRequested_For_TeamLeaderBackup(string user, string usermail, string teamleaderBackup, string teamleaderbackupmail, string teamleadermail, string supervisormail, DateTime startdate, DateTime enddate, decimal NumOfDays)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "New Vacation Request";
                message.Body = "Dear  " + teamleaderBackup + " , " + user + " has just requested a \r \t " + NumOfDays.ToString("0.00") + "\r \t day(s) vacation , Start Date : " + startdate.Date.ToString("dd/MM/yyyy") + " Ends at " + enddate.Date.ToString("dd/MM/yyyy") + "";
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
        public void SendMail_TeamLeader_VacationRequested_For_Supervisor(string user, string usermail, string supervisor, string supervisormail, DateTime startdate, DateTime enddate, decimal NumOfDays)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "New Vacation Request";
                message.Body = "Dear  " + supervisor + " , " + user + " has just requested a \r \t " + NumOfDays.ToString("0.00") + "\r \t day(s) vacation , Start Date : " + startdate.Date.ToString("dd/MM/yyyy") + " Ends at " + enddate.Date.ToString("dd/MM/yyyy") + "";
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
        public void SendMail_TeamLeader_VacationRequested_For_SupervisorBackup(string user, string usermail, string supervisorbackup, string supervisorbackupmail, string supervisormail, DateTime startdate, DateTime enddate, decimal NumOfDays)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "New Vacation Request";
                message.Body = "Dear  " + supervisorbackup + " , " + user + " has just requested a \r \t " + NumOfDays.ToString("0.00") + "\r \t day(s) vacation , Start Date : " + startdate.Date.ToString("dd/MM/yyyy") + " Ends at " + enddate.Date.ToString("dd/MM/yyyy") + "";
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
        public void SendMail_Supervisor_VacationRequested_For_Manager(string user, string usermail, string manager, string managermail, DateTime startdate, DateTime enddate, decimal NumOfDays)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "New Vacation Request";
                message.Body = "Dear  " + manager + " , " + user + " has just requested a \r \t " + NumOfDays.ToString("0.00") + "\r \t day(s) vacation , Start Date : " + startdate.Date.ToString("dd/MM/yyyy") + " Ends at " + enddate.Date.ToString("dd/MM/yyyy") + "";
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
        public void SendMail_Supervisor_VacationRequested_For_Manager_Backup(string user, string usermail, string managerbackup, string managerbackupmail, string managermail, DateTime startdate, DateTime enddate, decimal NumOfDays)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "New Vacation Request";
                message.Body = "Dear  " + managerbackup + " , " + user + " has just requested a \r \t " + NumOfDays.ToString("0.00") + "\r \t day(s) vacation , Start Date : " + startdate.Date.ToString("dd/MM/yyyy") + " Ends at " + enddate.Date.ToString("dd/MM/yyyy") + "";
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
        public void SendMail_VacationApproved_ByTeamLeader(string user, string usermail, string teamleader, string teamleadermail, string supervisormail, DateTime startdate)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Vacation Request Approved By Team Leader";
                message.Body = "Dear  " + user + " , " + teamleader + " has just Approved your vacation which Starts :" + startdate.Date.ToString("dd/MM/yyyy") + "";
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
        public void SendMail_VacationRejected_ByTeamLeader(string user, string usermail, string teamleader, string teamleadermail, string supervisormail, DateTime startdate)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Vacation Request Rejected By Team Leader";
                message.Body = "Dear  " + user + " , " + teamleader + " has just Rejected your vacation which Starts :" + startdate.Date.ToString("dd/MM/yyyy") + "";
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
        public void SendMail_VacationApproved_BySupervisor(string user, string usermail, string supervisor, string teamleadermail, string managermail, string supervisormail, DateTime startdate)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Vacation Request Approved By Supervisor";
                message.Body = "Dear  " + user + " , " + supervisor + " has just Approved your vacation which Starts :" + startdate.Date.ToString("dd/MM/yyyy") + "";
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(usermail);
                message.CcRecipients.Add(supervisormail);
                if (usermail != teamleadermail)
                {
                    message.CcRecipients.Add(teamleadermail);
                }
                message.CcRecipients.Add(managermail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
        }
        public void SendMail_VacationRejected_BySupervisor(string user, string usermail, string supervisor, string teamleadermail, string supervisormail, DateTime startdate)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Vacation Request Rejected By Supervisor";
                message.Body = "Dear  " + user + " , " + supervisor + " has just Rejected your vacation which Starts :" + startdate.Date.ToString("dd/MM/yyyy") + "";
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
        //New
        public void SendMail_OtherCasualVacationApproved_ByManager(string user, string usermail, string manager, string managermail, DateTime startdate)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Vacation Request Approved By Manager";
                message.Body = "Dear  " + user + " , " + manager + " has just Approved your vacation which Starts :" + startdate.Date.ToString("dd/MM/yyyy") + " and we added some red points due to vacation type";
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
        //END
        public void SendMail_VacationApproved_ByManager(string user, string usermail, string manager, string managermail, DateTime startdate)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Vacation Request Approved By Manager";
                message.Body = "Dear  " + user + " , " + manager + " has just Approved your vacation which Starts :" + startdate.Date.ToString("dd/MM/yyyy") + "";
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
        public void SendMail_VacationRejected_ByManager(string user, string usermail, string manager, string managermail, DateTime startdate)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Vacation Request Rejected By Manager";
                message.Body = "Dear  " + user + " , " + manager + " has just Rejected your vacation which Starts :" + startdate.Date.ToString("dd/MM/yyyy") + "";
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
        public void SendMail_VacationRevoked_By_NormalUserOrTeamLeader(string user, string usermail, string supervisor, string supervisormail, DateTime startdate)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Vacation Request Revoked";
                message.Body = "Dear  " + supervisor + " , " + user + " has just Revoked a vacation which Starts :" + startdate.Date.ToString("dd/MM/yyyy") + "";
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
        public void SendMail_VacationRevoked_By_SupervisorOrDuputyManager(string user, string usermail, string manager, string managermail, DateTime startdate)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Vacation Request Revoked";
                message.Body = "Dear  " + manager + " , " + user + " has just Revoked a vacation which Starts :" + startdate.Date.ToString("dd/MM/yyyy") + "";
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
        public void SendMail_Revoke_Approved_By_Supervisor(string user, string usermail, string Supervisor, string supervisorMail, DateTime startdate)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Vacation Request Revoke is Approved";
                message.Body = "Dear  " + user + " , " + Supervisor + " has just Approved on Revoke of vacation which Starts :" + startdate.Date.ToString("dd/MM/yyyy") + "";
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(usermail);
                message.CcRecipients.Add(supervisorMail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
        }
        public void SendMail_Revoke_Rejected_By_Supervisor(string user, string usermail, string Supervisor, string supervisorMail, DateTime startdate)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Vacation Request Revoke is Rejected";
                message.Body = "Dear  " + user + " , " + Supervisor + " has just Rejected to Revoke vacation which Starts :" + startdate.Date.ToString("dd/MM/yyyy") + "";
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(usermail);
                message.CcRecipients.Add(supervisorMail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
        }
        public void SendMail_Revoke_Approved_By_Manager(string user, string usermail, string manager, string managermail, DateTime startdate)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Vacation Request Revoke is Approved";
                message.Body = "Dear  " + user + " , " + manager + " has just Approved on Revoke of vacation which Starts :" + startdate.Date.ToString("dd/MM/yyyy") + "";
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
        public void SendMail_Revoke_Rejected_By_Manager(string user, string usermail, string manager, string managermail, DateTime startdate)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Vacation Request Revoke is Rejected";
                message.Body = "Dear  " + user + " , " + manager + " has just Rejected to Revoke vacation which Starts :" + startdate.Date.ToString("dd/MM/yyyy") + "";
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


        public JsonResult decrease_vacation_count(int ID)
        {
            var msg = "";
            Request req = db.Requests.Find(ID);
            string ApprovePerson = (string)Session["UserName"];



            var RequestSubType = req.ReqSubType; //نوع الاجازة
            var RequestUserName = req.UserName;
            var reqDuration = req.ReqDuration;


            var AccountID = (from p in db.accountTBs where p.EmpName == RequestUserName select p.ID).FirstOrDefault();
            accountTB acc = db.accountTBs.Find(AccountID);
            var departmentName = (from item in db.accountTBs where item.EmpName == req.UserName select item.DepartmentName).SingleOrDefault();




            if (RequestSubType == "عارضه - أخري")
            {
                if (acc.AnnualVac > 0 && acc.AnnualVac >= reqDuration)
                {
                    acc.AnnualVac = acc.AnnualVac - reqDuration;

                    if (acc.MaximumNumberofCasualExceed == "true")
                    {
                        acc.MaximumNumberofCasualExceed = "false";
                    }



                }

                else
                {
                    msg = "No Annual Vacations Remaining Or Not Enough Annual Vacations";
                }
            }

            //Break
            else if (RequestSubType == "اعتيادية")
            {
                if (acc.AnnualVac > 0 && acc.AnnualVac >= reqDuration)
                {
                    acc.AnnualVac = acc.AnnualVac - reqDuration;

                    if (acc.MaximumNumberofCasualExceed == "true")
                    {
                        acc.MaximumNumberofCasualExceed = "false";
                    }

                }

                else
                {
                    msg = "No Annual Vacations Remaining Or Not Enough Annual Vacations";
                }
            }

            //END

            else if (RequestSubType == "عارضه")
            {
                if (acc.CasualVac > 0 && acc.AnnualVac > 0 && acc.CasualVac >= reqDuration && acc.AnnualVac >= reqDuration)
                {
                    acc.CasualVac = acc.CasualVac - reqDuration;
                    acc.AnnualVac = acc.AnnualVac - reqDuration;
                    acc.LastCasualVacation = req.DurationFrom;
                    if ((int)reqDuration == 2)
                    {
                        acc.MaximumNumberofCasualExceed = "true";
                    }

                }
                else
                {
                    msg = "No Casual Vacations Remaining Or Not Enough Casual Vacations";
                }

            }

            else if (RequestSubType == "مرضية")
            {
                if (acc.AnnualVac > 0 && acc.AnnualVac >= reqDuration / 4)
                {
                    acc.AnnualVac = acc.AnnualVac - reqDuration / 4;

                }

                else
                {
                    msg = "No Annual Vacations Remaining Or Not Enough Annual Vacations";
                }

            }

            else
            {


            }
            db.SaveChanges();
            message = msg;
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult increase_vacation_count(int ID)
        {
            var msg = "";
            Request req = db.Requests.Find(ID);
            string ApprovePerson = (string)Session["UserName"];



            var RequestSubType = req.ReqSubType; //نوع الاجازة
            var RequestUserName = req.UserName;
            var reqDuration = req.ReqDuration;


            var AccountID = (from p in db.accountTBs where p.EmpName == RequestUserName select p.ID).FirstOrDefault();
            accountTB acc = db.accountTBs.Find(AccountID);
            var departmentName = (from item in db.accountTBs where item.EmpName == req.UserName select item.DepartmentName).SingleOrDefault();




            if (RequestSubType == "عارضه - أخري")
            {

                acc.AnnualVac = acc.AnnualVac + reqDuration;

                if (acc.MaximumNumberofCasualExceed == "false")
                {
                    acc.MaximumNumberofCasualExceed = "true";
                }




            }

            //Break
            else if (RequestSubType == "اعتيادية")
            {

                acc.AnnualVac = acc.AnnualVac + reqDuration;

                if (acc.MaximumNumberofCasualExceed == "true")
                {
                    acc.MaximumNumberofCasualExceed = "false";
                }


            }

            //END

            else if (RequestSubType == "عارضه")
            {

                acc.CasualVac = acc.CasualVac + reqDuration;
                acc.AnnualVac = acc.AnnualVac + reqDuration;
                acc.LastCasualVacation = null;
                if ((int)reqDuration == 2)
                {
                    acc.MaximumNumberofCasualExceed = "false";
                }




            }

            else if (RequestSubType == "مرضية")
            {

                acc.AnnualVac = acc.AnnualVac + reqDuration / 4;





            }

            else
            {


            }
            db.SaveChanges();
            message = msg;
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}