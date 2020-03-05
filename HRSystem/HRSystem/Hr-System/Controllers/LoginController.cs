using Hr_System.Models;
using HrSystem.Classes;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HrSystem.Controllers
{

    public class LoginController : Controller
    {
        HRSystemEntities db = new HRSystemEntities();

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            //DateTime datenow1 = DateTime.Now.Date;
            
            //foreach (var item in db.accountTBs.ToList())
            //{
            //    var requests = (from r in db.Requests where r.DurationFrom >= datenow1 && r.UserName == item.EmpName && item.DelegatedAuthorities!="yes" orderby r.DurationFrom ascending select r).FirstOrDefault();
            //    if (requests != null)
            //    {
            //        item.DelegateTime = requests.DurationFrom;
            //        item.DelegationTimeTo = requests.DurationTo;
            //        item.DelegateTo = requests.DelegateTo;
            //        db.SaveChanges();
            //    }
            //}
            ////Apply delegation
            //var accounts = (from a in db.accountTBs
            //                where a.DelegateTo != null && a.DelegatedAuthorities == null
            //                select a).ToList();
            
            //foreach (var item in accounts)
            //{
            //    string Name = item.EmpName;
            //    string DelegateToName = item.DelegateTo;
            //    string type = item.EmpType;
            //    if (DateTime.Now >= item.DelegateTime)
            //    {
            //        if (type == "TeamLeader")
            //        {
            //            var subdepartments = (from p in db.SubDeps where p.DepBackupTeamLeader == Name select p);
            //            foreach (var Sub in subdepartments)
            //            {
            //                Sub.DepSenior = DelegateToName;
            //            }
            //            var useraccount = (from p in db.accountTBs where p.EmpName == DelegateToName select p).SingleOrDefault();
            //            var originaluser = (from p in db.accountTBs where p.EmpName == Name select p).SingleOrDefault();
            //            useraccount.EmpType = "TeamLeader";
            //            originaluser.DelegatedAuthorities = "yes";
            //            useraccount.TeamLeaderRole = "yes";
            //            db.SaveChanges();
            //            string UserMail = item.Email;
            //            string Delegatedersonmail = useraccount.Email;
            //            SendMail_Delegation_Validated(Name, UserMail, DelegateToName, Delegatedersonmail);
            //        }
            //        else if (type == "Supervisor" || type == "Deputy Manager")
            //        {
            //            var subdepartment = (from p in db.SubDeps where p.DepTeamLeader == Name select p);
            //            foreach (var sub in subdepartment)
            //            {
            //                sub.DepBackupSupervisor = DelegateToName;
            //            }
            //            var useraccount = (from p in db.accountTBs where p.EmpName == DelegateToName select p).SingleOrDefault();
            //            var originaluser = (from p in db.accountTBs where p.EmpName == Name select p).SingleOrDefault();
            //            useraccount.EmpType = "Supervisor";
            //            originaluser.DelegatedAuthorities = "yes";
            //            useraccount.SupervisorRole = "yes";
            //            db.SaveChanges();
            //            string UserMail = item.Email;
            //            string Delegatedersonmail = useraccount.Email;
            //            SendMail_Delegation_Validated(Name, UserMail, DelegateToName, Delegatedersonmail);
            //        }
            //        else if (type == "Manager")
            //        {
            //            var Department = (from p in db.DepartmentTBs where p.DeptManager == Name select p).SingleOrDefault();
            //            Department.DeptBackupManager = DelegateToName;
            //            var useraccount = (from p in db.accountTBs where p.EmpName == Name select p).SingleOrDefault();
            //            var userToTranfserDelegation = (from p in db.accountTBs where p.EmpName == DelegateToName select p).SingleOrDefault();
            //            var PendingRequests = db.Requests.Where(a => a.ReqStatus != "ApprovedByManager").Where(a => a.ReqStatus != "Approved").Where(a => a.ReqStatus != "RejectedByManager").Where(a => a.ReqStatus != "Rejected").Where(a => a.ReqStatus != "RejectedByTeamLeader").Where(a => a.ReqStatus != "RejectedBySupervisor").Where(a => a.ReqStatus != "RevokeRejectedBySupervisor").Where(a => a.ReqStatus != "ApprovedOnRevokeBySupervisor").Where(a => a.ReqStatus != "ApprovedOnRevokeByManager").Where(a => a.ReqStatus != "RevokeRejectedByManager").ToList();
            //            foreach (var req in PendingRequests)
            //            {
            //                req.MyBackupManager = DelegateToName;
            //            }
            //            if (userToTranfserDelegation.EmpType == "Deputy Manager")
            //            {
            //                userToTranfserDelegation.ManagerRole = "No";
            //            }
            //            else if (userToTranfserDelegation.EmpType == "Normal")
            //            {
            //                userToTranfserDelegation.EmpType = "Deputy Manager";
            //                userToTranfserDelegation.ManagerRole = "Yes";
            //                userToTranfserDelegation.Signature = "yes";
            //            }
            //            else
            //            {
            //                userToTranfserDelegation.EmpType = "Deputy Manager";
            //                userToTranfserDelegation.ManagerRole = "Yes";
            //            }
            //            useraccount.DelegatedAuthorities = "yes";
            //            db.SaveChanges();
            //            string UserMail = item.Email;
            //            string Delegatedersonmail = userToTranfserDelegation.Email;
            //            SendMail_Delegation_Validated(Name, UserMail, DelegateToName, Delegatedersonmail);
            //        }
            //    }
            //}
            //// Cancel delegation
            //var accounts2 = (from a in db.accountTBs
            //                 where a.DelegatedAuthorities == "yes"
            //                 select a).ToList();
            //foreach (var item in accounts2)
            //{
            //    string Name = item.EmpName;
            //    string type = item.EmpType;
            //    string UserMail = item.Email;
                              
            //    if (DateTime.Now>item.DelegationTimeTo) {
            //        if (type == "TeamLeader")
            //        {

            //            var subdepartments = (from p in db.SubDeps where p.DepBackupTeamLeader == Name select p);
            //            var SeniorName = (from p in subdepartments select p.DepSenior).FirstOrDefault();
            //            foreach (var item2 in subdepartments)
            //            {
            //                item2.DepSenior = null;
            //            }
            //            var useraccount = (from p in db.accountTBs where p.EmpName == SeniorName select p).SingleOrDefault();
            //            useraccount.EmpType = "Normal";
            //            var OriginalUser = (from p in db.accountTBs where p.EmpName == Name select p).SingleOrDefault();
            //            OriginalUser.DelegatedAuthorities = null;
            //            OriginalUser.DelegateTo = null;
            //            OriginalUser.DelegateTime = null;
            //            OriginalUser.DelegationTimeTo = null;
            //            useraccount.TeamLeaderRole = null;
            //            db.SaveChanges();

            //            SendMail_Delegation_Removed(Name, UserMail, useraccount.EmpName, useraccount.Email);

            //        }
            //        else if (type == "Supervisor" || type == "Deputy Manager")
            //        {

            //            var subdepartments = (from p in db.SubDeps where p.DepTeamLeader == Name select p);
            //            var teamleadername = (from p in subdepartments select p.DepBackupSupervisor).FirstOrDefault();
            //            foreach (var item2 in subdepartments)
            //            {
            //                item2.DepBackupSupervisor = null;
            //            }
            //            var useraccount = (from p in db.accountTBs where p.EmpName == teamleadername select p).SingleOrDefault();
            //            useraccount.EmpType = "TeamLeader";
            //            var OriginalUser = (from p in db.accountTBs where p.EmpName == Name select p).SingleOrDefault();
            //            OriginalUser.DelegatedAuthorities = null;
            //            OriginalUser.DelegateTo = null;
            //            OriginalUser.DelegateTime = null;
            //            OriginalUser.DelegationTimeTo = null;
            //            useraccount.SupervisorRole = null;
            //            db.SaveChanges();
            //            SendMail_Delegation_Removed(Name, UserMail, useraccount.EmpName, useraccount.Email);

            //        }
            //        else if (type == "Manager")
            //        {
            //            var Manager = (from p in db.accountTBs where p.EmpName == Name select p).SingleOrDefault();
            //            var Department = (from p in db.DepartmentTBs where p.DeptManager == Manager.EmpName select p).SingleOrDefault();
            //            Manager.DelegatedAuthorities = null;
            //            Manager.DelegateTo = null;
            //            Manager.DelegateTime = null;
            //            Manager.DelegationTimeTo = null;
            //            var DuputyManger = Department.DeptBackupManager;
            //            var DuputyManagerAccount = (from p in db.accountTBs where p.EmpName == DuputyManger select p).SingleOrDefault();
            //            if (DuputyManagerAccount.ManagerRole == "Yes")
            //            {
            //                if (DuputyManagerAccount.Signature == "yes")
            //                {
            //                    DuputyManagerAccount.EmpType = "Normal";
            //                    DuputyManagerAccount.ManagerRole = null;
            //                }
            //                else
            //                {
            //                    DuputyManagerAccount.EmpType = "Supervisor";
            //                    DuputyManagerAccount.ManagerRole = null;
            //                }

            //            }
            //            else
            //            {
            //                DuputyManagerAccount.ManagerRole = null;

            //            }

            //            Department.DeptBackupManager = null;
            //            db.SaveChanges();
            //            SendMail_Delegation_Removed(Name, UserMail, DuputyManagerAccount.EmpName, DuputyManagerAccount.Email);

            //        }
            //    }
            //}

            return View();
        }
        [HttpPost]
        public ActionResult Login(User login)
        {
            //if (ModelState.IsValid)
            //{
                
                var user = (from userlist in db.accountTBs
                            where userlist.EmpName == login.EmpName && userlist.Password == login.Password
                            select new
                            {
                                userlist.ID,
                                userlist.EmpName,
                                userlist.EmpType,
                                userlist.DepartmentName,
                                userlist.SubDepartmentName,
                                userlist.ArabicName,
                                userlist.DelegatedAuthorities,
                            }).ToList();
                if (user.FirstOrDefault() != null)
                {
                    Session["UserName"] = user.FirstOrDefault().EmpName;
                    Session["UserID"] = user.FirstOrDefault().ID;
                    Session["UserType"] = user.FirstOrDefault().EmpType;
                    Session["Dep"] = user.FirstOrDefault().DepartmentName;
                    Session["subDep"] = user.FirstOrDefault().SubDepartmentName;
                    Session["ArabicName"] = user.FirstOrDefault().ArabicName;
                    Session["UserDelegation"] = user.FirstOrDefault().DelegatedAuthorities;
  
                    var type = (string)Session["UserType"];
                    ViewBag.user = user.FirstOrDefault().EmpName;
                    ViewBag.ID = user.FirstOrDefault().ID;
                    ViewBag.DepartmentName = user.FirstOrDefault().DepartmentName;
                    ViewBag.SubDepartmentName = user.FirstOrDefault().SubDepartmentName;
                    ViewBag.ArabicName = user.FirstOrDefault().ArabicName;
                    ViewBag.type = type;
                    if (type == "Manager"||type=="Supervisor"||type== "Deputy Manager" || type== "HR - Administration" ||(type== "None"&& user.FirstOrDefault().DepartmentName== "HR - Administration"))
                    {
                        return RedirectToAction("getAllDepartments", "AdminPanel", new { ID = Session["UserID"] });

                    }
                    else{
                    return RedirectToAction("userDetails", "AdminPanel", new { ID = Session["UserID"] });

                    }                           

                }
                else
                {
                    ModelState.AddModelError("", "Login Failed.");
                }
            //}
            return View(login);
        }
        //public void SendMail_Delegation_Validated(string username, string usermail, string delegatedperson, string delegatedpersonmail)
        //{
        //    try
        //    {
        //        ExchangeService service = new ExchangeService();
        //        service.AutodiscoverUrl("HrReply@prime-health.org");
        //        service.UseDefaultCredentials = false;
        //        service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
        //        EmailMessage message = new EmailMessage(service);
        //        message.Subject = "User's Authorities have been delegated to you";
        //        message.Body = "Dear  " + delegatedperson + " , " + "The authorites of " + username + " have been transfered to you automatically due to his vacation" ;
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
        //public void SendMail_Delegation_Removed(string username, string usermail, string delegatedperson, string delegatedpersonmail)
        //{
        //    try
        //    {
        //        ExchangeService service = new ExchangeService();
        //        service.AutodiscoverUrl("HrReply@prime-health.org");
        //        service.UseDefaultCredentials = false;
        //        service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
        //        EmailMessage message = new EmailMessage(service);
        //        message.Subject = "Your delegation has been canceled";
        //        message.Body = "Dear  " + username + " , " + "your delegation to " + delegatedperson + "have been canceled automatically";
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
    }
}
