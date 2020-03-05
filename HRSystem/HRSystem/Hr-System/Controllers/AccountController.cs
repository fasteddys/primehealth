using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hr_System.Models;
using System.Net;
using System.Data;
using System.IO;
using Microsoft.Exchange;
using Microsoft.Exchange.WebServices;
using Microsoft.Exchange.WebServices.Data;
using System.Timers;
using Hr_System.Classes;

namespace Hr_System.Controllers
{
    public class AccountController : Controller
    {
        //private static Timer myTimer;
        private HRSystemEntities db = new HRSystemEntities();

        public ActionResult GetAllAccounts()
        {
            var accounttbs = db.accountTBs;
            return View(accounttbs.ToList());
        }
        //NEW
        [HttpPost]
        public ActionResult Reset()
        {
            decimal DefaultExecuseHours = 6;
            var accountId = db.accountTBs;
            foreach (var person in accountId)
            {
                if (person.NumberOfExcuseHours >= 0)
                {
                    person.NumberOfExcuseHours = 6;
                }
                if (person.NumberOfExcuseHours < 0)
                {
                    person.NumberOfExcuseHours = DefaultExecuseHours + person.NumberOfExcuseHours;
                }
            }
            db.SaveChanges();
            return RedirectToAction("GetAllAccounts");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            accountTB accounttb = db.accountTBs.Find(id);
            if (accounttb == null)
            {
                return HttpNotFound();
            }
            return View(accounttb);
        }
        public ActionResult Create()
        {

            ViewBag.DepartmentName = new SelectList(db.DepartmentTBs.ToList(), "DeptName", "DeptName");
            return View();
        }

        public JsonResult CreateManager(accountTB account, string EmpName, string DepName)
        {
            string message = "";

            account.MaximumNumberofCasualExceed = "false";
            account.NumberOfRemainingPermissions = 2;
            account.OldLeavesCount = 0;
            account.NumberOfExcuseHours = 6;
            db.accountTBs.Add(account);
            var departmentID = db.DepartmentTBs.Where(item => item.DeptName == DepName).Select(p => p.ID).FirstOrDefault();
            DepartmentTB dep = db.DepartmentTBs.Find(departmentID);
            dep.DeptManager = EmpName;
            GreenPointsReport GPR = new GreenPointsReport();
            GPR.UserName = EmpName;
            db.GreenPointsReports.Add(GPR);
            RedPointsReport RPR = new RedPointsReport();
            RPR.UserName = EmpName;
            db.RedPointsReports.Add(RPR);
            db.SaveChanges();
            message = "Done";
            return Json(message, JsonRequestBehavior.AllowGet);


        }
        public JsonResult CreateDeputyManager(accountTB account, string EmpName, string DepartmentName, string SubDepartmentName)
        {
            string message = "";

            account.MaximumNumberofCasualExceed = "false";
            account.NumberOfRemainingPermissions = 2;
            account.OldLeavesCount = 0;
            account.NumberOfExcuseHours = 6;
            db.accountTBs.Add(account);
            var departmentID = db.DepartmentTBs.Where(item => item.DeptName == DepartmentName).Select(p => p.ID).FirstOrDefault();
            DepartmentTB dep = db.DepartmentTBs.Find(departmentID);
            dep.DeptBackupManager = EmpName;
            var SubdepartmentID = db.SubDeps.Where(item => item.SubDepartmentName == SubDepartmentName).Select(p => p.ID).FirstOrDefault();
            SubDep Subdep = db.SubDeps.Find(SubdepartmentID);
            Subdep.DepTeamLeader = EmpName;
            GreenPointsReport GPR = new GreenPointsReport();
            GPR.UserName = EmpName;
            db.GreenPointsReports.Add(GPR);
            RedPointsReport RPR = new RedPointsReport();
            RPR.UserName = EmpName;
            db.RedPointsReports.Add(RPR);
            db.SaveChanges();
            message = "Done";
            return Json(message, JsonRequestBehavior.AllowGet);


        }
        public JsonResult CreateDeputyManagerWithoutSub(accountTB account, string EmpName, string DepartmentName)
        {
            string message = "";

            account.MaximumNumberofCasualExceed = "false";
            account.NumberOfRemainingPermissions = 2;
            account.OldLeavesCount = 0;
            account.NumberOfExcuseHours = 6;
            db.accountTBs.Add(account);
            var departmentID = db.DepartmentTBs.Where(item => item.DeptName == DepartmentName).Select(p => p.ID).FirstOrDefault();
            DepartmentTB dep = db.DepartmentTBs.Find(departmentID);
            dep.DeptBackupManager = EmpName;
            GreenPointsReport GPR = new GreenPointsReport();
            GPR.UserName = EmpName;
            db.GreenPointsReports.Add(GPR);
            RedPointsReport RPR = new RedPointsReport();
            RPR.UserName = EmpName;
            db.RedPointsReports.Add(RPR);
            db.SaveChanges();
            message = "Done";
            return Json(message, JsonRequestBehavior.AllowGet);


        }
        public JsonResult CreateTeamLeader(accountTB account, string EmpName, string SubDepartmentName)
        {


            string message = "";
            account.MaximumNumberofCasualExceed = "false";
            account.NumberOfRemainingPermissions = 2;
            account.RedPoints = 0;
            account.GreenPoints = 0;
            account.OldLeavesCount = 0;
            account.NumberOfExcuseHours = 6;
            db.accountTBs.Add(account);
            var SubdepartmentID = db.SubDeps.Where(item => item.SubDepartmentName == SubDepartmentName).Select(p => p.ID).FirstOrDefault();
            SubDep dep = db.SubDeps.Find(SubdepartmentID);
            dep.DepBackupTeamLeader = EmpName;
            GreenPointsReport GPR = new GreenPointsReport();
            GPR.UserName = EmpName;
            db.GreenPointsReports.Add(GPR);
            RedPointsReport RPR = new RedPointsReport();
            RPR.UserName = EmpName;
            db.RedPointsReports.Add(RPR);
            db.SaveChanges();
            message = "Done";
            return Json(message, JsonRequestBehavior.AllowGet);

        }
        public JsonResult CreateSupervisor(accountTB account, string EmpName, string SubDepartmentName)
        {


            string message = "";
            account.MaximumNumberofCasualExceed = "false";
            account.NumberOfRemainingPermissions = 2;
            account.RedPoints = 0;
            account.GreenPoints = 0;
            account.OldLeavesCount = 0;
            account.NumberOfExcuseHours = 6;
            db.accountTBs.Add(account);
            var SubdepartmentID = db.SubDeps.Where(item => item.SubDepartmentName == SubDepartmentName).Select(p => p.ID).FirstOrDefault();
            SubDep dep = db.SubDeps.Find(SubdepartmentID);
            dep.DepTeamLeader = EmpName;
            GreenPointsReport GPR = new GreenPointsReport();
            GPR.UserName = EmpName;
            db.GreenPointsReports.Add(GPR);
            RedPointsReport RPR = new RedPointsReport();
            RPR.UserName = EmpName;
            db.RedPointsReports.Add(RPR);
            db.SaveChanges();
            message = "Done";
            return Json(message, JsonRequestBehavior.AllowGet);

        }
        public JsonResult CreateMultiTeamLeader(accountTB account, List<string> SubDepartmentName, string EmpName)
        {
            string message = "";
            account.MaximumNumberofCasualExceed = "false";
            account.NumberOfRemainingPermissions = 2;
            account.OldLeavesCount = 0;
            account.RedPoints = 0;
            account.GreenPoints = 0;
            account.NumberOfExcuseHours = 6;
            db.accountTBs.Add(account);
            foreach (var subdep in SubDepartmentName)
            {
                var SubdepartmentID = db.SubDeps.Where(item => item.SubDepartmentName == subdep).Select(p => p.ID).FirstOrDefault();
                SubDep dep = db.SubDeps.Find(SubdepartmentID);
                dep.DepBackupTeamLeader = EmpName;
            }
            GreenPointsReport GPR = new GreenPointsReport();
            GPR.UserName = EmpName;
            db.GreenPointsReports.Add(GPR);
            RedPointsReport RPR = new RedPointsReport();
            RPR.UserName = EmpName;
            db.RedPointsReports.Add(RPR);
            db.SaveChanges();
            message = "Done";
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CreateMultiSupervisor(accountTB account, List<string> SubDepartments, string EmpName)
        {
            string message = "";
            account.MaximumNumberofCasualExceed = "false";
            account.NumberOfRemainingPermissions = 2;
            account.OldLeavesCount = 0;
            account.RedPoints = 0;
            account.GreenPoints = 0;
            account.NumberOfExcuseHours = 6;
            db.accountTBs.Add(account);
            foreach (var subdep in SubDepartments)
            {
                var SubdepartmentID = db.SubDeps.Where(item => item.SubDepartmentName == subdep).Select(p => p.ID).FirstOrDefault();
                SubDep dep = db.SubDeps.Find(SubdepartmentID);
                dep.DepTeamLeader = EmpName;
            }
            GreenPointsReport GPR = new GreenPointsReport();
            GPR.UserName = EmpName;
            db.GreenPointsReports.Add(GPR);
            RedPointsReport RPR = new RedPointsReport();
            RPR.UserName = EmpName;
            db.RedPointsReports.Add(RPR);
            db.SaveChanges();
            message = "Done";
            return Json(message, JsonRequestBehavior.AllowGet);


        }
        public JsonResult CreateNormalUser(accountTB account, string EmpName)
        {
            string message = "";
            account.MaximumNumberofCasualExceed = "false";
            account.NumberOfRemainingPermissions = 2;
            account.OldLeavesCount = 0;
            account.RedPoints = 0;
            account.GreenPoints = 0;
            account.NumberOfExcuseHours = 6;
            db.accountTBs.Add(account);
            GreenPointsReport GPR = new GreenPointsReport();
            GPR.UserName = EmpName;
            db.GreenPointsReports.Add(GPR);
            RedPointsReport RPR = new RedPointsReport();
            RPR.UserName = EmpName;
            db.RedPointsReports.Add(RPR);
            db.SaveChanges();
            message = "Done";
            return Json(message, JsonRequestBehavior.AllowGet);


        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            accountTB accounttb = db.accountTBs.Find(id);
            if (accounttb == null)
            {
                return HttpNotFound();
            }
            return View(accounttb);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            accountTB accounttb = db.accountTBs.Find(id);
            db.accountTBs.Remove(accounttb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public JsonResult getSubDepByDepName(string name)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var depID = (from item in db.DepartmentTBs where item.DeptName == name select item.ID).SingleOrDefault();
            var subdeps = (from p in db.SubDeps where p.DepartmentID == depID select p);
            return Json(subdeps, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CalculateAnnualVacation(string EmpHireDate, string Exceptional)
        {
            try
            {
                int annaualvacations;
                if (Exceptional == "false")
                {
                    DateTime emphire = DateTime.ParseExact(EmpHireDate, "dd/MM/yyyy", null);
                    int daysInYear = DateTime.IsLeapYear(emphire.Year) ? 366 : 365;
                    int daysLeftInYear = daysInYear - emphire.DayOfYear;
                    annaualvacations = Convert.ToInt32((daysLeftInYear * 1.75) / 30);
                    return Json(annaualvacations, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    DateTime emphire = DateTime.ParseExact(EmpHireDate, "dd/MM/yyyy", null);
                    int daysInYear = DateTime.IsLeapYear(emphire.Year) ? 366 : 365;
                    int daysLeftInYear = daysInYear - emphire.DayOfYear;
                    annaualvacations = Convert.ToInt32((daysLeftInYear * 2.5) / 30);
                    return Json(annaualvacations, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public JsonResult CalculateCasualVacation(string EmpHireDate)
        {

            try
            {
                int casualvacations;

                DateTime emphire = DateTime.ParseExact(EmpHireDate, "dd/MM/yyyy", null);

                int hiremonth = emphire.Month;
                int num_of_remainingMonth = 12 - hiremonth;
                if (num_of_remainingMonth == 11)
                {
                    casualvacations = 6;
                }

                else if (num_of_remainingMonth <= 5)
                {
                    casualvacations = 0;

                }
                else
                {
                    casualvacations = Convert.ToInt32(Convert.ToDouble(0.5 * num_of_remainingMonth));
                }
                return Json(casualvacations, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public ActionResult EditProfile(int? id)
        {
            accountTB acc = db.accountTBs.Find(id);
            ViewBag.UserName = acc.EmpName;
            ViewBag.Password = acc.Password;
            return View();
        }
        public ActionResult delegateAuthority(int? id)
        {

            //AutoDelegate();
            accountTB acc = db.accountTBs.Find(id);
            var EmpType = acc.EmpType;
            var EmpName = acc.EmpName;
            var Department = acc.DepartmentName;
            var SubDepartment = acc.SubDepartmentName;
            List<accountTB> ListOfPotetionalUsers = null;
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
                                                        where p.DepTeamLeader == EmpName
                                                        select p).ToList();
                var TeamLeaders = (from p in ListOfMySupervisedSubDepartments select p.DepBackupTeamLeader);
                List<accountTB> accountdetails = new List<accountTB>();
                List<accountTB> finalteamleaders = new List<accountTB>(); ;
                foreach (var TeamLeader in TeamLeaders)
                {
                    accountdetails = (from p in db.accountTBs where p.EmpName == TeamLeader select p).ToList();
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

            return View(ListOfPotetionalUsers);
        }
        public JsonResult DelegateMyAuth(string theAuthorityTransfer)
        {
            string Name = (string)(Session["UserName"]);
            string type = (string)(Session["UserType"]);
            string Dep = (string)(Session["Dep"]);
            string subDep = (string)(Session["subDep"]);
            var UserMail = (from item in db.accountTBs where item.EmpName == Name select item.Email).SingleOrDefault();
            var Delegatedersonmail = (from item in db.accountTBs where item.EmpName == theAuthorityTransfer select item.Email).SingleOrDefault();

            bool alreadyDelegated = CheckDelegationstatus(Name);
            bool checkifanyremaingPendingVacations = checkifremainingPendings(Name, type);
            string message = "";
            if (type == "TeamLeader")
            {
                if (CheckDelegationAuthorityForSenior(Name) == true)
                {
                    message = "You Can NOT Transfer the Delegation";
                }
                else if (alreadyDelegated == true)
                {
                    message = "You already Have Delegated Someone";
                }
                else if (checkifanyremaingPendingVacations == true)
                {
                    message = "Please Finish All Your Pending Vacations and/or Excuses";
                }
                else
                {
                    var subdepartments = (from p in db.SubDeps where p.DepBackupTeamLeader == Name select p);
                    foreach (var item in subdepartments)
                    {
                        item.DepSenior = theAuthorityTransfer;
                    }
                    var useraccount = (from p in db.accountTBs where p.EmpName == theAuthorityTransfer select p).SingleOrDefault();
                    var originaluser = (from p in db.accountTBs where p.EmpName == Name select p).SingleOrDefault();
                    useraccount.EmpType = "TeamLeader";
                    originaluser.DelegatedAuthorities = "yes";
                    useraccount.TeamLeaderRole = "yes";
                    db.SaveChanges();
                    message = "Success";
                    SendMail_Delegation_Validated(Name, UserMail, theAuthorityTransfer, Delegatedersonmail);
                }
            }
            else if (type == "Supervisor" || type == "Deputy Manager")
            {
                if (CheckDelegationAuthorityForSupervisorBackup(Name) == true)
                {
                    message = "You Can NOT Transfer the Delegation";
                }
                else if (alreadyDelegated == true)
                {
                    message = "You already Have Delegated Someone";
                }
                else if (checkifanyremaingPendingVacations == true)
                {
                    message = "Please Finish All Your Pending Vacations and/or Excuses";
                }
                else
                {
                    var subdepartment = (from p in db.SubDeps where p.DepTeamLeader == Name select p);
                    foreach (var item in subdepartment)
                    {
                        item.DepBackupSupervisor = theAuthorityTransfer;
                    }
                    var useraccount = (from p in db.accountTBs where p.EmpName == theAuthorityTransfer select p).SingleOrDefault();
                    var originaluser = (from p in db.accountTBs where p.EmpName == Name select p).SingleOrDefault();
                    useraccount.EmpType = "Supervisor";
                    originaluser.DelegatedAuthorities = "yes";
                    useraccount.SupervisorRole = "yes";
                    db.SaveChanges();
                    message = "Success";
                    SendMail_Delegation_Validated(Name, UserMail, theAuthorityTransfer, Delegatedersonmail);
                }

            }
            else if (type == "Manager")
            {
                if (alreadyDelegated == true)
                {
                    message = "You already Have Delegated Someone";
                }
                //else if (checkifanyremaingPendingVacations == true)
                //{
                //    message = "Please Finish All Your Pending Vacations and/or Excuses";
                //}
                else
                {
                    var Department = (from p in db.DepartmentTBs where p.DeptManager == Name select p).SingleOrDefault();
                    Department.DeptBackupManager = theAuthorityTransfer;
                    var useraccount = (from p in db.accountTBs where p.EmpName == Name select p).SingleOrDefault();
                    var userToTranfserDelegation = (from p in db.accountTBs where p.EmpName == theAuthorityTransfer select p).SingleOrDefault();
                    var PendingRequests = db.Requests.Where(a => a.ReqStatus != "ApprovedByManager").Where(a => a.ReqStatus != "Approved").Where(a => a.ReqStatus != "RejectedByManager").Where(a => a.ReqStatus != "Rejected").Where(a => a.ReqStatus != "RejectedByTeamLeader").Where(a => a.ReqStatus != "RejectedBySupervisor").Where(a => a.ReqStatus != "RevokeRejectedBySupervisor").Where(a => a.ReqStatus != "ApprovedOnRevokeBySupervisor").Where(a => a.ReqStatus != "ApprovedOnRevokeByManager").Where(a => a.ReqStatus != "RevokeRejectedByManager").ToList();
                    foreach (var req in PendingRequests)
                    {
                        req.MyBackupManager = theAuthorityTransfer;
                    }
                    if (userToTranfserDelegation.EmpType == "Deputy Manager")
                    {
                        userToTranfserDelegation.ManagerRole = "No";
                       

                    }
                    else if (userToTranfserDelegation.EmpType == "Normal")
                    {
                        userToTranfserDelegation.EmpType = "Deputy Manager";
                        userToTranfserDelegation.ManagerRole = "Yes";
                        userToTranfserDelegation.Signature = "yes";

                    }
                    else
                    {
                        userToTranfserDelegation.EmpType = "Deputy Manager";
                        userToTranfserDelegation.ManagerRole = "Yes";
                    }
                    useraccount.DelegatedAuthorities = "yes";
                    //Added By Ahmed Mahmoud
                    useraccount.DelegateTo = userToTranfserDelegation.EmpName;
                    db.SaveChanges();
                    message = "Success";
                    SendMail_Delegation_Validated(Name, UserMail, theAuthorityTransfer, Delegatedersonmail);
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteMyAuth()
        {
            string Name = (string)(Session["UserName"]);
            string type = (string)(Session["UserType"]);
            string Dep = (string)(Session["Dep"]);
            string subDep = (string)(Session["subDep"]);
            var departmentName = (from item in db.accountTBs where item.EmpName == Name select item.DepartmentName).SingleOrDefault();
            var SubdepartmentName = (from item in db.accountTBs where item.EmpName == Name select item.SubDepartmentName).SingleOrDefault();
            var UserMail = (from item in db.accountTBs where item.EmpName == Name select item.Email).SingleOrDefault();
            bool alreadyDelegated = CheckDelegationstatus(Name);
            string delegation = "";
            if (alreadyDelegated == true)
            {
                delegation = "yes";
            }
            else
            {
                delegation = null;
            }
            string message = "";
            if (type == "TeamLeader")
            {
                if (delegation == "yes")
                {
                    var subdepartments = (from p in db.SubDeps where p.DepBackupTeamLeader == Name select p);
                    var SeniorName = (from p in subdepartments select p.DepSenior).FirstOrDefault();
                    foreach (var item in subdepartments)
                    {
                        item.DepSenior = null;
                    }
                    var useraccount = (from p in db.accountTBs where p.EmpName == SeniorName select p).SingleOrDefault();
                    useraccount.EmpType = "Normal";
                    var OriginalUser = (from p in db.accountTBs where p.EmpName == Name select p).SingleOrDefault();
                    OriginalUser.DelegatedAuthorities = null;
                    OriginalUser.DelegateTo = null;
                    OriginalUser.DelegateTime = null;
                    OriginalUser.DelegationTimeTo = null;
                    useraccount.TeamLeaderRole = null;
                    db.SaveChanges();
                    message = "Success";
                    SendMail_Delegation_Removed(Name, UserMail, useraccount.EmpName, useraccount.Email);
                }
                else
                {
                    message = "You Have Not Delegated Anyone to cancel OR You Are Not Authorithed to Cancel The Delegation";

                }

            }
            else if (type == "Supervisor" || type == "Deputy Manager")
            {
                if (delegation == "yes")
                {
                    var subdepartments = (from p in db.SubDeps where p.DepTeamLeader == Name select p);
                    var teamleadername = (from p in subdepartments select p.DepBackupSupervisor).FirstOrDefault();
                    foreach (var item in subdepartments)
                    {
                        item.DepBackupSupervisor = null;
                    }
                    var useraccount = (from p in db.accountTBs where p.EmpName == teamleadername select p).SingleOrDefault();
                    useraccount.EmpType = "TeamLeader";
                    var OriginalUser = (from p in db.accountTBs where p.EmpName == Name select p).SingleOrDefault();
                    OriginalUser.DelegatedAuthorities = null;
                    OriginalUser.DelegateTo = null;
                    OriginalUser.DelegateTime = null;
                    OriginalUser.DelegationTimeTo = null;
                    useraccount.SupervisorRole = null;
                    db.SaveChanges();
                    message = "Success";
                    SendMail_Delegation_Removed(Name, UserMail, useraccount.EmpName, useraccount.Email);
                }
                else
                {
                    message = "You Have Not Delegated Anyone to cancel OR You Are Not Authorithed to Cancel The Delegation";
                }
            }
            else if (type == "Manager")
            {
                if (delegation == "yes")
                {
                    var Manager = (from p in db.accountTBs where p.EmpName == Name select p).SingleOrDefault();
                    var Department = (from p in db.DepartmentTBs where p.DeptManager == Manager.EmpName select p).SingleOrDefault();
                    Manager.DelegatedAuthorities = null;
                    Manager.DelegateTo = null;
                    Manager.DelegateTime = null;
                    Manager.DelegationTimeTo = null;
                    Session["ManagerDelegatedExist"] = false;
                    var DuputyManger = Department.DeptBackupManager;
                    var DuputyManagerAccount = (from p in db.accountTBs where p.EmpName == DuputyManger select p).SingleOrDefault();
                    if (DuputyManagerAccount.ManagerRole == "Yes")
                    {
                        if (DuputyManagerAccount.Signature == "yes")
                        {
                            DuputyManagerAccount.EmpType = "Normal";
                            DuputyManagerAccount.ManagerRole = null;
                        }
                        else
                        {
                            DuputyManagerAccount.EmpType = "Supervisor";
                            DuputyManagerAccount.ManagerRole = null;
                        }

                    }
                    else
                    {
                        DuputyManagerAccount.ManagerRole = null;

                    }

                    Department.DeptBackupManager = null;
                    db.SaveChanges();
                    message = "Success";
                    SendMail_Delegation_Removed(Name, UserMail, DuputyManagerAccount.EmpName, DuputyManagerAccount.Email);
                }
                else
                {
                    message = "You Have Not Delegated Anyone to cancel";
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdatePassword(string ID, string Password)
        {
            string message = "";
            int accountID = int.Parse(ID);
            accountTB acc = db.accountTBs.Find(accountID);
            try
            {
                string oldpassword = acc.Password;
                if (oldpassword == Password)
                {
                    message = "Same Password Nothing Changed !";
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    acc.Password = Password;
                    message = "Password Updated Successfully";
                    db.SaveChanges();
                    return Json(message, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                message = "Something Went Wrong Please Contact IT";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }
        public bool CheckDelegationstatus(string user)
        {
            var UserAccount = (from p in db.accountTBs where p.EmpName == user select p).SingleOrDefault();
            if (UserAccount.DelegatedAuthorities == "yes")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool checkifremainingPendings(string user, string userType)
        {
            bool result = false;
            if (userType == "TeamLeader")
            {
                var requests = (from p in db.Requests
                                where p.MyTeamLeader == user || p.MyBackupTeamLeader == user
                                where p.ReqStatus == "PendingTeamLeaderApproval" || p.ReqStatus == "PendingSeniorApproval"
                                select p).Count();
                if (requests == 0)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            else if (userType == "Supervisor")
            {
                var requests = (from p in db.Requests
                                where p.MySupervisor == user || p.MyBackupSupervisor == user
                                where p.ReqStatus == "PendingSupervisorApproval" || p.ReqStatus == "PendingSupervisorBackupApproval" || p.ReqStatus == "RevokedByUser" || p.ReqStatus == "RevokedByTeamLeader"
                                select p).Count();

                if (requests == 0)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            else if (userType == "Deputy Manager")
            {
                var requests = (from p in db.Requests
                                where p.MyBackupManager == user || p.MySupervisor == user
                                where p.ReqStatus == "PendingDuputyManagerApproval" || p.ReqStatus == "PendingDuputyManagerApprovalOnRevoke" || p.ReqStatus == "PendingSupervisorApproval" || p.ReqStatus == "PendingSupervisorBackupApproval" || p.ReqStatus == "RevokedByUser" || p.ReqStatus == "RevokedByTeamLeader"
                                select p).Count();
                if (requests == 0)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            else if (userType == "Manager")
            {
                var requests = (from p in db.Requests
                                where p.MyManager == user
                                where p.ReqStatus == "PendingManagerApproval" || p.ReqStatus == "PendingDuputyManagerApproval" || p.ReqStatus == "PendingDuputyManagerApprovalOnRevoke"
                                select p).Count();
                if (requests == 0)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            return result;
        }
        public bool CheckDelegationAuthorityForSenior(string user)
        {
            var UserAccount = (from p in db.accountTBs where p.EmpName == user select p).SingleOrDefault();
            var UserSubDepartment = UserAccount.SubDepartmentName;
            var supdepartmentSenior = (from p in db.SubDeps where p.SubDepartmentName == UserSubDepartment select p.DepSenior).SingleOrDefault();
            if (UserAccount.EmpName == supdepartmentSenior)
            {
                return true;
            }

            else
            {
                return false;
            }

        }
        public bool CheckDelegationAuthorityForSupervisorBackup(string user)
        {
            var UserAccount = (from p in db.accountTBs where p.EmpName == user select p).SingleOrDefault();
            var UserSubDepartment = UserAccount.SubDepartmentName;
            var supdepartmentSupervisorBackup = (from p in db.SubDeps where p.SubDepartmentName == UserSubDepartment select p.DepBackupSupervisor).SingleOrDefault();
            if (UserAccount.EmpName == supdepartmentSupervisorBackup)
            {
                return true;
            }

            else
            {
                return false;
            }

        }
        //public void AutoDelegate()
        //{
        //    myTimer = new Timer();
        //    // Tell the timer what to do when it elapses
        //    myTimer.Elapsed += new ElapsedEventHandler(Delegate);
        //    // Set it to go off every 1 hours
        //    myTimer.Interval = 150000;
        //    // And start it        
        //    myTimer.Enabled = true;
        //}
        //public void Delegate(object source, ElapsedEventArgs e) 
        //{
        //    var account = (from a in Delgate.accountTBs
        //                    select a).ToList();

        //    int daynow = DateTime.Now.Day ;
        //    int monthnow = DateTime.Now.Month;
        //    int yearnow = DateTime.Now.Year;   

        //    foreach (var item in account)
        //    {

        //        if (item.EmpCode== "1187")
        //        {
        //            int x = 0;
        //        }
        //        var requests = (from r in Delgate.Requests where  r.DurationFrom.Value.Day >= daynow && r.DurationFrom.Value.Month==monthnow && r.DurationFrom.Value.Year==yearnow && r.UserName == item.EmpName && item.DelegatedAuthorities == null orderby r.DurationFrom ascending select r).FirstOrDefault();
        //        if (requests != null)
        //        {
        //            if (requests.HalfDayVacationType == "1/2 Day AM")
        //            {
        //                item.DelegateTime = requests.DurationFrom;
        //                item.DelegationTimeTo = requests.DurationTo.Value.AddHours(13);
        //                item.DelegateTo = requests.DelegateTo;
        //            }
        //            else if (requests.HalfDayVacationType == "1/2 Day PM")
        //            {
        //                item.DelegateTime = requests.DurationFrom.Value.AddHours(13);
        //                item.DelegationTimeTo = requests.DurationTo.Value.AddHours(17);
        //                item.DelegateTo = requests.DelegateTo;
        //            }
        //            else
        //            {
        //                item.DelegateTime = requests.DurationFrom;
        //                item.DelegationTimeTo = requests.BacktoWorkDate;
        //                item.DelegateTo = requests.DelegateTo;
        //            }

        //            Delgate.SaveChanges();
        //        }
        //    }
        //    //Apply delegation
        //    var accounts = (from a in Delgate.accountTBs
        //                    where a.DelegateTo != null && a.DelegatedAuthorities == null && a.DelegateTo!= "----"
        //                    select a).ToList();
        //    foreach (var item in accounts)
        //    {
        //        string Name = item.EmpName;
        //        string DelegateToName = item.DelegateTo;
        //        string type = item.EmpType;
        //        if (DateTime.Now >= item.DelegateTime && DateTime.Now<item.DelegationTimeTo)
        //        {
        //            if (type == "TeamLeader")
        //            {
        //                var subdepartments = (from p in Delgate.SubDeps where p.DepBackupTeamLeader == Name select p);
        //                foreach (var Sub in subdepartments)
        //                {
        //                    Sub.DepSenior = DelegateToName;
        //                }
        //                var useraccount = (from p in Delgate.accountTBs where p.EmpName == DelegateToName select p).SingleOrDefault();
        //                var originaluser = (from p in Delgate.accountTBs where p.EmpName == Name select p).SingleOrDefault();
        //                useraccount.EmpType = "TeamLeader";
        //                originaluser.DelegatedAuthorities = "yes";
        //                useraccount.TeamLeaderRole = "yes";
        //                Delgate.SaveChanges();
        //                string UserMail = item.Email;
        //                string Delegatedersonmail = useraccount.Email;
        //                SendMail_AutoDelegation_Validated(Name, UserMail, DelegateToName, Delegatedersonmail);
        //            }
        //            else if (type == "Supervisor" || type == "Deputy Manager")
        //            {
        //                var subdepartment = (from p in Delgate.SubDeps where p.DepTeamLeader == Name select p);
        //                foreach (var sub in subdepartment)
        //                {
        //                    sub.DepBackupSupervisor = DelegateToName;
        //                }
        //                var useraccount = (from p in Delgate.accountTBs where p.EmpName == DelegateToName select p).SingleOrDefault();
        //                var originaluser = (from p in Delgate.accountTBs where p.EmpName == Name select p).SingleOrDefault();
        //                useraccount.EmpType = "Supervisor";
        //                originaluser.DelegatedAuthorities = "yes";
        //                useraccount.SupervisorRole = "yes";
        //                Delgate.SaveChanges();
        //                string UserMail = item.Email;
        //                string Delegatedersonmail = useraccount.Email;
        //                SendMail_AutoDelegation_Validated(Name, UserMail, DelegateToName, Delegatedersonmail);
        //            }
        //            else if (type == "Manager")
        //            {
        //                var Department = (from p in Delgate.DepartmentTBs where p.DeptManager == Name select p).SingleOrDefault();
        //                Department.DeptBackupManager = DelegateToName;
        //                var useraccount = (from p in Delgate.accountTBs where p.EmpName == Name select p).SingleOrDefault();
        //                var userToTranfserDelegation = (from p in Delgate.accountTBs where p.EmpName == DelegateToName select p).SingleOrDefault();
        //                var PendingRequests = Delgate.Requests.Where(a => a.ReqStatus != "ApprovedByManager").Where(a => a.ReqStatus != "Approved").Where(a => a.ReqStatus != "RejectedByManager").Where(a => a.ReqStatus != "Rejected").Where(a => a.ReqStatus != "RejectedByTeamLeader").Where(a => a.ReqStatus != "RejectedBySupervisor").Where(a => a.ReqStatus != "RevokeRejectedBySupervisor").Where(a => a.ReqStatus != "ApprovedOnRevokeBySupervisor").Where(a => a.ReqStatus != "ApprovedOnRevokeByManager").Where(a => a.ReqStatus != "RevokeRejectedByManager").ToList();
        //                foreach (var req in PendingRequests)
        //                {
        //                    req.MyBackupManager = DelegateToName;
        //                }
        //                if (userToTranfserDelegation.EmpType == "Deputy Manager")
        //                {
        //                    userToTranfserDelegation.ManagerRole = "No";
        //                }
        //                else if (userToTranfserDelegation.EmpType == "Normal")
        //                {
        //                    userToTranfserDelegation.EmpType = "Deputy Manager";
        //                    userToTranfserDelegation.ManagerRole = "Yes";
        //                    userToTranfserDelegation.Signature = "yes";
        //                }
        //                else
        //                {
        //                    userToTranfserDelegation.EmpType = "Deputy Manager";
        //                    userToTranfserDelegation.ManagerRole = "Yes";
        //                }
        //                useraccount.DelegatedAuthorities = "yes";
        //                Delgate.SaveChanges();
        //                string UserMail = item.Email;
        //                string Delegatedersonmail = userToTranfserDelegation.Email;
        //                SendMail_AutoDelegation_Validated(Name, UserMail, DelegateToName, Delegatedersonmail);
        //            }
        //        }
        //    }
        //    // Cancel delegation
        //    var accounts2 = (from a in Delgate.accountTBs
        //                     where a.DelegatedAuthorities == "yes"
        //                     select a).ToList();
        //    foreach (var item in accounts2)
        //    {
        //        string Name = item.EmpName;
        //        string type = item.EmpType;
        //        string UserMail = item.Email;

        //        if (DateTime.Now > item.DelegationTimeTo)
        //        {
        //            if (type == "TeamLeader")
        //            {

        //                var subdepartments = (from p in Delgate.SubDeps where p.DepBackupTeamLeader == Name select p);
        //                var SeniorName = (from p in subdepartments select p.DepSenior).FirstOrDefault();
        //                foreach (var item2 in subdepartments)
        //                {
        //                    item2.DepSenior = null;
        //                }
        //                var useraccount = (from p in Delgate.accountTBs where p.EmpName == SeniorName select p).SingleOrDefault();
        //                useraccount.EmpType = "Normal";
        //                var OriginalUser = (from p in Delgate.accountTBs where p.EmpName == Name select p).SingleOrDefault();
        //                OriginalUser.DelegatedAuthorities = null;
        //                OriginalUser.DelegateTo = null;
        //                OriginalUser.DelegateTime = null;
        //                OriginalUser.DelegationTimeTo = null;
        //                useraccount.TeamLeaderRole = null;
        //                Delgate.SaveChanges();

        //                SendMail_AutoDelegation_Removed(Name, UserMail, useraccount.EmpName, useraccount.Email);

        //            }
        //            else if (type == "Supervisor" || type == "Deputy Manager")
        //            {

        //                var subdepartments = (from p in Delgate.SubDeps where p.DepTeamLeader == Name select p);
        //                var teamleadername = (from p in subdepartments select p.DepBackupSupervisor).FirstOrDefault();
        //                foreach (var item2 in subdepartments)
        //                {
        //                    item2.DepBackupSupervisor = null;
        //                }
        //                var useraccount = (from p in Delgate.accountTBs where p.EmpName == teamleadername select p).SingleOrDefault();
        //                useraccount.EmpType = "TeamLeader";
        //                var OriginalUser = (from p in Delgate.accountTBs where p.EmpName == Name select p).SingleOrDefault();
        //                OriginalUser.DelegatedAuthorities = null;
        //                OriginalUser.DelegateTo = null;
        //                OriginalUser.DelegateTime = null;
        //                OriginalUser.DelegationTimeTo = null;
        //                useraccount.SupervisorRole = null;
        //                Delgate.SaveChanges();
        //                SendMail_AutoDelegation_Removed(Name, UserMail, useraccount.EmpName, useraccount.Email);

        //            }
        //            else if (type == "Manager")
        //            {
        //                var Manager = (from p in Delgate.accountTBs where p.EmpName == Name select p).SingleOrDefault();
        //                var Department = (from p in Delgate.DepartmentTBs where p.DeptManager == Manager.EmpName select p).SingleOrDefault();
        //                Manager.DelegatedAuthorities = null;
        //                Manager.DelegateTo = null;
        //                Manager.DelegateTime = null;
        //                Manager.DelegationTimeTo = null;
        //                var DuputyManger = Department.DeptBackupManager;
        //                var DuputyManagerAccount = (from p in Delgate.accountTBs where p.EmpName == DuputyManger select p).SingleOrDefault();
        //                if (DuputyManagerAccount.ManagerRole == "Yes")
        //                {
        //                    if (DuputyManagerAccount.Signature == "yes")
        //                    {
        //                        DuputyManagerAccount.EmpType = "Normal";
        //                        DuputyManagerAccount.ManagerRole = null;
        //                    }
        //                    else
        //                    {
        //                        DuputyManagerAccount.EmpType = "Supervisor";
        //                        DuputyManagerAccount.ManagerRole = null;
        //                    }

        //                }
        //                else
        //                {
        //                    DuputyManagerAccount.ManagerRole = null;

        //                }

        //                Department.DeptBackupManager = null;
        //                Delgate.SaveChanges();
        //                SendMail_AutoDelegation_Removed(Name, UserMail, DuputyManagerAccount.EmpName, DuputyManagerAccount.Email);

        //            }
        //        }
        //    }
        //}
        //public void SendMail_AutoDelegation_Validated(string username, string usermail, string delegatedperson, string delegatedpersonmail)
        //{
        //    try
        //    {
        //        ExchangeService service = new ExchangeService();
        //        service.AutodiscoverUrl("HrReply@prime-health.org");
        //        service.UseDefaultCredentials = false;
        //        service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
        //        EmailMessage message = new EmailMessage(service);
        //        message.Subject = "User's Authorities have been delegated to you";
        //        message.Body = "Dear  " + delegatedperson + " , " + "The authorites of " + username + " have been transfered to you automatically due to his vacation";
        //        message.From = "HrReply@Prime-Health.org";
        //        message.ToRecipients.Add(delegatedpersonmail);
        //        message.CcRecipients.Add(usermail);
        //        message.Save();
        //        message.SendAndSaveCopy();
        //    }
        //    catch (Exception ex)
        //    {


        //    }
        //}
        //public void SendMail_AutoDelegation_Removed(string username, string usermail, string delegatedperson, string delegatedpersonmail)
        //{
        //    try
        //    {
        //        ExchangeService service = new ExchangeService();
        //        service.AutodiscoverUrl("HrReply@prime-health.org");
        //        service.UseDefaultCredentials = false;
        //        service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
        //        EmailMessage message = new EmailMessage(service);
        //        message.Subject = "Your delegation has been canceled";
        //        message.Body = "Dear  " + username + " , " + "your delegation to " + delegatedperson + " have been canceled automatically";
        //        message.From = "HrReply@Prime-Health.org";
        //        message.ToRecipients.Add(delegatedpersonmail);
        //        message.CcRecipients.Add(usermail);
        //        message.Save();
        //        message.SendAndSaveCopy();
        //    }
        //    catch (Exception ex)
        //    {


        //    }
        //}
        public void SendMail_Delegation_Validated(string username, string usermail, string delegatedperson, string delegatedpersonmail)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "User's Authorities have been delegated to you";
                message.Body = "Dear  " + delegatedperson + " , " + username + " has just delegated his authorities to you.";
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(delegatedpersonmail);
                message.CcRecipients.Add(usermail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
        }
        public void SendMail_Delegation_Removed(string username, string usermail, string delegatedperson, string delegatedpersonmail)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Your delegation has been canceled";
                message.Body = "Dear  " + delegatedperson + " , " + username + " has just canceled his authorities to you.";
                message.From = "HrReply@Prime-Health.org";
                message.ToRecipients.Add(delegatedpersonmail);
                message.CcRecipients.Add(usermail);
                message.Save();
                message.SendAndSaveCopy();
            }
            catch (Exception ex)
            {


            }
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