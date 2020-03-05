using Hr_System.Classes;
using Hr_System.Models;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Timers;


namespace Hr_System.Controllers
{
    public class AdminPanelController : Controller
    {
        private static Timer myTimer;
        HRSystemEntities Delgate = new HRSystemEntities();
        HRSystemEntities hr = new HRSystemEntities();
        Helpers obj = new Helpers();
        int month = System.DateTime.Now.Month;
        DateTime realtime = DateTime.Now;
        string Name, type;
      
      
        [HttpGet]
        public ActionResult userDetails(int ID)
        {
            //AutoDelegate();
            try
            {
                
                var data = obj.getUserDataById(ID);
                var uname = data.First().EmpName;
                var Pointsdata = hr.DailyPointsReports.Where(a => a.UserName.Equals(uname)).Take(6).ToList();
                ViewBag.Pointsdata = Pointsdata;
                ViewBag.ID = ID;
                ViewBag.Name = data.First().EmpName;
                ViewBag.Adress = data.First().Address;
                ViewBag.HireDate = data.First().HireDate.Value.ToShortDateString();
                ViewBag.Department = data.First().SubDepartmentName;
                ViewBag.Management = data.First().DepartmentName;
                
                //new
                var PointGreensSum = (from point in hr.DailyPointsReports
                                    where point.UserName == uname
                                    select point.GreenPoints).Sum();
                ViewBag.Green = PointGreensSum;

                var PointRedsSum = (from point in hr.DailyPointsReports
                                 where point.UserName == uname
                                 select point.RedPoints).Sum();


                ViewBag.Red = PointRedsSum;
                ViewBag.RemainingExcuseHours = data.First().NumberOfExcuseHours;
                //end
                //ViewBag.RemainigExcuse = data.First().NumberOfRemainingPermissions;
                
                ViewBag.RemainigVacations = data.First().CasualVac;
                ViewBag.RemainigAnnaulVacations = data.First().AnnualVac;
                ViewBag.OldVacation = data.First().OldLeavesCount;
                int gid = obj.getIdForUserInGreenPoints(data.First().EmpName);
                int rid = obj.getIdForUserInRedPoints(data.First().EmpName);
                RedPointsReport rp = hr.RedPointsReports.Find(rid);
                ViewBag.Rjan = rp.Jan;
                ViewBag.Rfeb = rp.Feb;
                ViewBag.Rmarch = rp.March;
                ViewBag.Rapril = rp.April;
                ViewBag.Rmay = rp.May;
                ViewBag.RJune = rp.June;
                ViewBag.RJuly = rp.July;
                ViewBag.RAug = rp.Aug;
                ViewBag.RSep = rp.Sep;
                ViewBag.ROct = rp.Oct;
                ViewBag.RNov = rp.Nov;
                ViewBag.RDec = rp.Dec;
                GreenPointsReport gp = hr.GreenPointsReports.Find(gid);
                ViewBag.Gjan = gp.Jan;
                ViewBag.Gfeb = gp.Feb;
                ViewBag.Gmarch = gp.March;
                ViewBag.Gapril = gp.April;
                ViewBag.Gmay = gp.May;
                ViewBag.GJune = gp.June;
                ViewBag.GJuly = gp.July;
                ViewBag.GAug = gp.Aug;
                ViewBag.GSep = gp.Sep;
                ViewBag.GOct = gp.Oct;
                ViewBag.GNov = gp.Nov;
                ViewBag.GDec = gp.Dec;
                ViewBag.ProjectsNo = hr.ProjectMembers.Where(x => x.ProjMembs.Equals(uname)).Count().ToString();
            }
            catch
            {

            }
         
       return View();
        }


        public ActionResult ExecuseView()
        {

            var users_name = hr.accountTBs.Where(A => A.SubDepartmentName != "Deleted").ToList();

            return View(users_name);
        }

        public ActionResult VacationView()
        {
            var users_name = hr.accountTBs.Where(A => A.SubDepartmentName != "Deleted").ToList();

            return View(users_name);

        }
      
 public ActionResult EventView()
        {
            var users_name = hr.accountTBs.Where(A => A.SubDepartmentName != "Deleted").ToList();

            return View(users_name);

        }

        [HttpPost]
        public ActionResult ExcuseReportData(string fromDate, string ToDate, string EmpName)
        {

            DateTime from = new DateTime();
            from = DateTime.Parse(fromDate);
            DateTime To = new DateTime();
            To = DateTime.Parse(ToDate);
            int sum_half_hour = hr.Requests.Where(t => t.UserName.Contains(EmpName) & t.ExcuseDuration.Contains("نصف ساعة") & t.ReqDate >= from & t.ReqDate <= To & t.ReqStatus.Contains("Approved")).Count();
            int sum_hour = hr.Requests.Where(t => t.UserName.Contains(EmpName) & t.ExcuseDuration.Contains("ساعة") & t.ReqDate >= from & t.ReqDate <= To & t.ReqStatus.Contains("Approved")).Count();
            int sum_Two_hour = hr.Requests.Where(t => t.UserName.Contains(EmpName) & t.ExcuseDuration.Contains("ساعتين") & t.ReqDate >= from & t.ReqDate <= To & t.ReqStatus.Contains("Approved")).Count();
            int sum_hour_and_half = hr.Requests.Where(t => t.UserName.Contains(EmpName) & t.ExcuseDuration.Contains("ساعة ونصف") & t.ReqDate >= from & t.ReqDate <= To & t.ReqStatus.Contains("Approved")).Count();
            List<int> sum = new List<int>();
            sum.Add(sum_half_hour);
            sum.Add(sum_hour);
            sum.Add(sum_Two_hour);
            sum.Add(sum_hour_and_half);
            //ViewBag.sum_half_hour = sum_half_hour;
            //ViewBag.sum_hour = sum_hour;
            //ViewBag.sum_Two_hour = sum_Two_hour;
            //ViewBag.sum_hour_and_half = sum_hour_and_half;
            return Json(sum, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EventReportData(string fromDate, string ToDate, string EmpName)
        {

            DateTime From = new DateTime();
            From = DateTime.Parse(fromDate);
            DateTime To = new DateTime();
            To = DateTime.Parse(ToDate);
            int sum_Accept = (from i in hr.EmpEvents
                                  join u in hr.Events on i.EventID equals u.EventID
                                  join e in hr.accountTBs on i.EmpID equals e.ID
                                  where e.EmpName.Contains(EmpName) & u.EventDay >= From & u.EventDay <= To & i.Response.Contains("Accept")
                                  select i).Count();
            int sum_Reject = (from i in hr.EmpEvents
                              join u in hr.Events on i.EventID equals u.EventID
                              join e in hr.accountTBs on i.EmpID equals e.ID
                              where e.EmpName.Contains(EmpName) & u.EventDay >= From & u.EventDay <= To & i.Response.Contains("Reject")
                              select i).Count();
          
            List<int> sum = new List<int>();
            sum.Add(sum_Accept);
            sum.Add(sum_Reject);
            
            //ViewBag.sum_half_hour = sum_half_hour;
            //ViewBag.sum_hour = sum_hour;
            //ViewBag.sum_Two_hour = sum_Two_hour;
            //ViewBag.sum_hour_and_half = sum_hour_and_half;
            return Json(sum, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult vactionReportData(string fromDate, string ToDate, string EmpName)
        {

            DateTime from2 = new DateTime();
            from2 = DateTime.Parse(fromDate);
            DateTime To = new DateTime();
            To = DateTime.Parse(ToDate);
            string Casual_Vacation = (from p in hr.Requests
                                      join u in hr.accountTBs on p.EmployeeCode equals u.EmpCode
                                      where p.UserName.Contains(EmpName) && p.ReqSubType == "عارضه" && p.ReqDate >= from2 & p.ReqDate <= To && p.ReqStatus.Contains("ApprovedByManager")
                                      select p.ReqDuration).Sum().ToString();
            string Annual_vacation = (from p in hr.Requests
                                      join u in hr.accountTBs on p.EmployeeCode equals u.EmpCode
                                      where p.UserName.Contains(EmpName) && p.ReqSubType == "اعتيادية" && p.ReqDate >= from2 & p.ReqDate <= To && p.ReqStatus.Contains("ApprovedByManager")
                                      select p.ReqDuration).Sum().ToString();
            string Another_Casual_vacation = (from p in hr.Requests
                                              join u in hr.accountTBs on p.EmployeeCode equals u.EmpCode
                                              where p.UserName.Contains(EmpName) && p.ReqSubType == "عارضه - أخري" && p.ReqDate >= from2 & p.ReqDate <= To && p.ReqStatus.Contains("ApprovedByManager")
                                              select p.ReqDuration).Sum().ToString();
            string sick_vacation = (from p in hr.Requests
                                    join u in hr.accountTBs on p.EmployeeCode equals u.EmpCode
                                    where p.UserName.Contains(EmpName) && p.ReqSubType == "مرضية" && p.ReqDate >= from2 & p.ReqDate <= To && p.ReqStatus.Contains("ApprovedByManager")
                                    select p.ReqDuration).Sum().ToString();
            string sick_Upaid_vacation = (from p in hr.Requests
                                          join u in hr.accountTBs on p.EmployeeCode equals u.EmpCode
                                          where p.UserName.Contains(EmpName) && p.ReqSubType == "مرضية - بالخصم" && p.ReqDate >= from2 & p.ReqDate <= To && p.ReqStatus.Contains("ApprovedByManager")
                                          select p.ReqDuration).Sum().ToString();
            string UnPaid_vacation = (from p in hr.Requests
                                      join u in hr.accountTBs on p.EmployeeCode equals u.EmpCode
                                      where p.UserName.Contains(EmpName) && p.ReqSubType == "بالخصم" && p.ReqDate >= from2 & p.ReqDate <= To && p.ReqStatus.Contains("ApprovedByManager")
                                      select p.ReqDuration).Sum().ToString();
          if(Casual_Vacation=="")
          {
              Casual_Vacation = "0.00000000";
          }
          if(Annual_vacation=="")
          {
              Annual_vacation = "0.00000000";
          }
           if(Another_Casual_vacation=="")
          {
              Another_Casual_vacation = "0.00000000";
          }
          if (sick_vacation == "")
          {
              sick_vacation = "0.00000000";
          }
        if (sick_Upaid_vacation == "")
          {
              sick_Upaid_vacation = "0.00000000";
          }
        if (UnPaid_vacation == "")
          {
              UnPaid_vacation = "0.00000000";
          }
            List<string> sum = new List<string>();
            sum.Add(Casual_Vacation);
            sum.Add(Annual_vacation);
            sum.Add(Another_Casual_vacation);
            sum.Add(sick_vacation);
            sum.Add(sick_Upaid_vacation);
            sum.Add(UnPaid_vacation);
            //ViewBag.sum_half_hour = sum_half_hour;
            //ViewBag.sum_hour = sum_hour;
            //ViewBag.sum_Two_hour = sum_Two_hour;
            //ViewBag.sum_hour_and_half = sum_hour_and_half;
            return Json(sum, JsonRequestBehavior.AllowGet);
        }
        //end updated //
        [HttpPost]
        public ActionResult userDetails(int ID , int RedPoints, int GreenPoints, string rvalue, string gvalue)
        {
            string message = "";
            if (ModelState.IsValid)
            {
              accountTB  acc = hr.accountTBs.Find(ID);
               acc.GreenPoints = GreenPoints;
               acc.RedPoints = RedPoints;
              hr.SaveChanges();
             var data = obj.getUserDataById(ID);
              string Name = data.First().EmpName;
              int r = int.Parse(rvalue);
              int g = int.Parse(gvalue);
              editPoints(Name, r, g);
              message = "Points Edited Sucssefully";
            }
            else
            {
                message = "Points not Valid";
            }
            if (Request.IsAjaxRequest())
            {
                return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                ViewBag.Message = message;
                return RedirectToAction("getAllPoints");
            }   
          }
        [HttpGet]
        public ActionResult PointsHistory(string User )
        {
            ViewBag.User = User;
            var data = hr.DailyPointsReports.Where(a => a.UserName.Equals(User)).ToList();
            return View(data);
        }
       [HttpGet]
        public ActionResult getAllPoints(string SubDep)
        {
          
            Name = (string)(Session["UserName"]);
            type = (string)(Session["UserType"]);
            dynamic data="";
            if (type == "Manager")
            {
               
                data = obj.getUserDataForManager(SubDep);
            }
            else if (type == "Supervisor"||type== "Deputy Manager")
            {
              
                data = obj.getUserDataForTeamLeader(SubDep);
            }
           
            return View(data);
        
        }
        [HttpGet]
       public ActionResult getAllDepartments()
       {
           Name = (string)(Session["UserName"]);
           type = (string)(Session["UserType"]);
            string Department = (string)(Session["Dep"]);
            string SubDepartment = (string)(Session["subDep"]);
            dynamic data = "";
            if ((Department == "HR Department" && SubDepartment == "None") || (Department == "HR Department" && SubDepartment == "HR-Administration"))
            {
                data = obj.getAllSubDepsForHR();
            }
            else   if (type == "Manager")
           {

               data = obj.getSubDepsForManager(Name);
           }
           else if (type == "Supervisor")
           {
               data = obj.getSubDepsForDuputyOrSupervisors(Name);
           }
           else if (type == "Deputy Manager")
           {
               data = obj.getSubDepsForDuputyManger(Name);
           }
          

            //check if manager delegated or not to view the manager suspended tab for deputy manager only.
            Session["ManagerDelegatedExist"] = obj.CheckifManagerDelegated(Name);
           return View(data);
       }    
        [HttpGet]
        public ActionResult AddProject()
        {
            Name = (string)(Session["UserName"]);
            type = (string)(Session["UserType"]);
            var data = obj.getSubDepsForManager(Name);
            if (type == "Manager")
            {
              data = obj.getSubDepsForManager(Name);
            }
            else if (type == "Supervisor"||type== "Deputy Manager")
            {
             string subDep = obj.getSubDepName(Name);
             data = obj.getSubDepsSupervisor(Name);
            }
            ViewBag.Dep = new SelectList(data, "SubDepartmentName", "SubDepartmentName");

            return View();
        }
        [HttpPost]
        public ActionResult AddProject(string ProjectName, string ProjDep, string ProjManager, string ProjDescription, string StartFrom, string EndIn, string Duration, string BackupProject)
        {
           Project proj = new Project();
           ProjectMember proMem = new ProjectMember();
           Name = (string)(Session["UserName"]);
           type = (string)(Session["UserType"]);
       
           string message = "";
           if (type == "Supervisor" || type == "Manager"||type== "Deputy Manager")
            {
                if (ModelState.IsValid)
                {
                    proj.ProjectName = ProjectName;
                    proj.ProjDep = ProjDep;
                    proj.ProjManager = ProjManager;
                    proj.ProjDescription = ProjDescription;
                    proj.StartFrom = StartFrom;
                    proj.EndIn = EndIn;
                    proj.ProjectDuration = Duration;
                    proj.ProjDepTeamLeader = obj.getSubDepTeamLeaderName(ProjDep);
                    proj.PojDepManager = obj.getSubDepManagerName(ProjDep);
                    proj.ProjStatus = "Initializing ....";
                    proj.Creator = Name;
                    proj.BackupProject = BackupProject;
                    proj.CreationDate = realtime;
                    hr.Projects.Add(proj);
                    //hr.SaveChanges();
                    //foreach (var item in Members)
                    //{
                    //    proMem.ProjectName = ProjectName;
                    //    proMem.ProjMembs = item;
                    //}
                    // hr.Projects.Add(proj);
                   // hr.ProjectMembers.Add(proMem);
                    hr.SaveChanges();
                    message = "Successfully Saved!";
                }
                else
                {
                    message = "Please provide required fields value";
                }
            }
            else
            {
                return Redirect("/Login/Login");
            }
           if (Request.IsAjaxRequest())
           {
               return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

           }
           else
           {
               ViewBag.Message = message;
               return View();
           }
         
        }
        [HttpGet]
        public ActionResult GetProjects ()
        {
            Name = (string)(Session["UserName"]);
            type =(string)(Session["UserType"]);
            var data = hr.Projects.Where(a => a.PojDepManager.Equals(Name));
            if (type == "Manager")
            {
                 data = hr.Projects.Where(a => a.PojDepManager.Equals(Name));
                
            }
            else if (type == "Supervisor"|| type == "Deputy Manager")
            {
                 data = hr.Projects.Where(a => a.ProjDepTeamLeader.Equals(Name));
            }
            else
            {
                data = (from f in hr.Projects
                        where f.ProjManager == Name || f.BackupProject == Name
                        select f); 
            }
            return View(data);
            }
        [HttpGet]
        public ActionResult ProjectDetails(string projectname)
        {
            int ID = (from f in hr.Projects where f.ProjectName == projectname select f.Id).FirstOrDefault();
            Project pro = hr.Projects.Find(ID);
            var projname = pro.ProjectName;
            ViewBag.ProjectName = pro.ProjectName;
            ViewBag.ProjectManager = pro.ProjManager;
            ViewBag.ProjectStart = pro.StartFrom;
            ViewBag.ProjectEnd = pro.EndIn;
            ViewBag.ProjectPlan = pro.ProjPlan;
            ViewBag.ProjectDescription = pro.ProjDescription;
            ViewBag.ProjectStatus = pro.ProjStatus;
            ViewBag.ProjectBackup = pro.BackupProject;
            ViewBag.projectDep = pro.ProjDep;
            ViewBag.ProjectMembers = (from f in hr.ProjectMembers
                                      where f.ProjectName == projname
                                      select f.ProjMembs).ToList();
            ViewBag.ProjectTasks = (from f in hr.ProjectsTasks
                                    where f.ProjectName == projname
                                    select f);
            ViewBag.ProjectRecentActivity = (from f in hr.ProjectsTasks
                                             where f.ProjectName == projname
                                    select f.Notification).ToList();
            return View();
        }
        [HttpGet]
        public ActionResult ProjectManagment(int ID )
        {
            Project pro = hr.Projects.Find(ID);
            ViewBag.ProjectName = pro.ProjectName;
            ViewBag.ID = pro.Id;
            ViewBag.ProjectMembers = new SelectList(obj.getProjectMembers(pro.ProjectName), "ProjMembs", "ProjMembs");
            return View();

        }
        [HttpPost]
        public ActionResult ProjectManagment( string Plan, int ID )
        {
            string message = "";
            if(ModelState.IsValid)
            {
                Project pro = hr.Projects.Find(ID);
                pro.ProjPlan = Plan;
                hr.SaveChanges();
                 message = "Successfully Saved!";
            }
            else
            {
                 message = "Failed!";

            }
            
            if (Request.IsAjaxRequest())
            {
                return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
            else
            {
                return View();
            }

        }
        [HttpGet]
        public ActionResult TaskDetails(int ID )
        {
            ProjectsTask pro = hr.ProjectsTasks.Find(ID);
            ViewBag.ProjectName = pro.ProjectName;
            ViewBag.TaskName = pro.TaskName;
            ViewBag.TaskDescription = pro.TaskDescription;
            ViewBag.TaskFrom = pro.TaskFrom;
            ViewBag.TaskTo = pro.TaskTo;
            ViewBag.TaskAssignee = pro.TaskAssignee;
            ViewBag.TaskBackup = pro.TaskBackup;
            ViewBag.ProjectNotifications = (from f in hr.ProjectsTasks
                                            where f.ProjectName == pro.ProjectName && f.TaskName == pro.TaskName
                                    select f.Notification).ToList();
           
           return View();
        }
        [HttpPost]
        public ActionResult TaskDetails( ProjectsTask pro,int ID )
        {
            
             Name = (string)(Session["UserName"]);
            type = (string)(Session["UserType"]);
             pro = hr.ProjectsTasks.Find(ID);
            string task = pro.TaskName;
            pro.Notification = Name + "has finished task" + task;
             hr.SaveChanges();
            return RedirectToAction("ProjectDetails");
        }
        //Json Methods   
        public void viewBags(string user)
        {
            int gid = obj.getIdForUserInGreenPoints(user);
            int rid = obj.getIdForUserInRedPoints(user);
            RedPointsReport rp = hr.RedPointsReports.Find(rid);
            ViewBag.Rjan = rp.Jan;
            ViewBag.Rfeb = rp.Feb;
            ViewBag.Rmarch = rp.March;
            ViewBag.Rapril = rp.April;
            ViewBag.Rmay = rp.May;
            ViewBag.RJune = rp.June;
            ViewBag.RJuly = rp.July;
            ViewBag.RAug = rp.Aug;
            ViewBag.RSep = rp.Sep;
            ViewBag.ROct = rp.Oct;
            ViewBag.RNov = rp.Nov;
            ViewBag.RDec = rp.Dec;
            GreenPointsReport gp = hr.GreenPointsReports.Find(gid);
            ViewBag.Gjan = gp.Jan;
            ViewBag.Gfeb = gp.Feb;
            ViewBag.Gmarch = gp.March;
            ViewBag.Gapril = gp.April;
            ViewBag.Gmay = gp.May;
            ViewBag.GJune = gp.June;
            ViewBag.GJuly = gp.July;
            ViewBag.GAug = gp.Aug;
            ViewBag.GSep = gp.Sep;
            ViewBag.GOct = gp.Oct;
            ViewBag.GNov = gp.Nov;
            ViewBag.GDec = gp.Dec;

        }
        public void editPoints(string user, int? rvalue, int? gvalue)
        {
            int gid = obj.getIdForUserInGreenPoints(user);
            int rid = obj.getIdForUserInRedPoints(user);
            RedPointsReport rp = hr.RedPointsReports.Find(rid);
            GreenPointsReport gp = hr.GreenPointsReports.Find(gid);
            if (month == 12)
            {
                rp.Dec = rvalue;
                gp.Dec = gvalue;
            }
            else if (month == 11)
            {
                rp.Nov = rvalue;
                gp.Nov = gvalue;
            }
            else if (month == 10)
            {
                rp.Oct = rvalue;
                gp.Oct = gvalue;
            }
            else if (month == 9)
            {
                rp.Sep = rvalue;
                gp.Sep = gvalue;
            }
            else if (month == 8)
            {
                rp.Aug = rvalue;
                gp.Aug = gvalue;
            }
            else if (month == 7)
            {
                rp.July = rvalue;
                gp.July = gvalue;
            }
            else if (month == 6)
            {
                rp.June = rvalue;
                gp.June = gvalue;
            }
            else if (month == 5)
            {
                rp.May = rvalue;
                gp.May = gvalue;
            }
            else if (month == 4)
            {
                rp.April = rvalue;
                gp.April = gvalue;
            }
            else if (month == 3)
            {
                rp.March = rvalue;
                gp.March = gvalue;
            }
            else if (month == 2)
            {
                rp.Feb = rvalue;
                gp.Feb = gvalue;
            }
            else if (month == 1)
            {
                rp.Jan = rvalue;
                gp.Jan = gvalue;
            }
            hr.SaveChanges();

        }
        public JsonResult getAllUserBySubDep(string SubDep)
        {
            List<accountTB> allUser = new List<accountTB>();
            allUser = hr.accountTBs.Where(a => a.SubDepartmentName.Equals(SubDep)).ToList();
            return new JsonResult { Data = allUser, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getAllUser()
        {
          
            List<accountTB> allUser = new List<accountTB>();
            allUser = hr.accountTBs.ToList();
            return new JsonResult { Data = allUser, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult AddProjectMembers (string ProjectName,string [] proMembers)
        {
            string message="";
            Name = (string)(Session["UserName"]);
            ProjectMember pm = new ProjectMember();
            for (int i = 0; i < proMembers.Length; i++)
            {
                pm.ProjectName = ProjectName;
                pm.ProjNotifications = Name + " has created a new project named " + ProjectName + " and you are member of this project ";
                pm.ProjMembs = proMembers[i].ToString();
                hr.ProjectMembers.Add(pm);
                hr.SaveChanges();
            }
            message = "Success";
            return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult AddProjectTask (ProjectsTask prota)
        {
            Name = (string)(Session["UserName"]);
            string message = "";
            if (ModelState.IsValid)
            {
             
                prota.TaskCreationDate = realtime;
                hr.ProjectsTasks.Add(prota);
                hr.SaveChanges();
                message = "Success";
            }
            else
            {
                message = "Failed";
            }
            return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public JsonResult AddDailyPointsReport(int ID, int? RedPoints, int? GreenPoints, string Comment, string UserName, string Creator)
        {
            string message = "";
            //NEW
            string Employeemail = (from item in hr.accountTBs where item.EmpName == UserName select item.Email).SingleOrDefault();
            string Creatormail = (from item in hr.accountTBs where item.EmpName == Creator select item.Email).SingleOrDefault();
            //END
           
                    DailyPointsReport dpr = new DailyPointsReport();
                    dpr.EvaluationDate = realtime;
                    dpr.RatingDate = realtime.ToShortDateString();
                    dpr.RedPoints = RedPoints;
                    dpr.GreenPoints =GreenPoints;
                    dpr.Comment = Comment;
                    dpr.UserName = UserName;
                    dpr.Creator = Creator;
                    hr.DailyPointsReports.Add(dpr);

                    accountTB acc = hr.accountTBs.Find(ID);
                    acc.GreenPoints =GreenPoints;
                    acc.RedPoints = RedPoints;
                    hr.SaveChanges();

                    var data = obj.getUserDataById(ID);
                    string Name = data.First().EmpName;
                    int? r = RedPoints;
                    int? g = GreenPoints;
                    editPoints(Name, r, g);
                    hr.SaveChanges();
                    message = "Point Edited Sucssefully";
            //new
                    SendMail_AddedPoints(UserName, Creator, RedPoints, GreenPoints, Employeemail, Creatormail);
            
            //end
       
            //}
            //else
            //{
            //    message = "Points not Valid";
            //}
            return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        //new mail
        public void SendMail_AddedPoints(string username, string Creator, int? RedPoints, int? GreenPoints, string usermail, string Creatormail)
        {
            try
            {
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl("HrReply@prime-health.org");
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials("HrReply", "NoP@ssw0rd", "primehealth.local");
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Points added to you!";
               // if (RedPoints != null || RedPoints !=0)
               // {
               //     message.Body = "Dear  " + username + " , " + Creator + " added " + GreenPoints + " GreenPoints to you, please check your profile for further details";               
               // }
               //else if (GreenPoints != null || GreenPoints != 0)
               // {
               //     message.Body = "Dear  " + username + " , " + Creator + " added " + RedPoints + " RedPoints to you, please check your profile for further details";

               // }
               // else
               // {
                    message.Body = "Dear  " + username + " , " + Creator + " added " + RedPoints + " RedPoints and " + GreenPoints + " GreenPoints to you, please check your profile for further details";
                    message.From = "HrReply@Prime-Health.org";
                    message.ToRecipients.Add(usermail);
                    message.CcRecipients.Add(Creatormail);
                    message.Save();
                    message.SendAndSaveCopy();
                //}
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>alert('The following errors have occurred: \n" + ex + " .');</script>");

            }
        }
        //end
        public JsonResult GetDailyPointsGraph (string username)
        {
            List<DailyPointsReport> pointsReports = new List<DailyPointsReport>();
            pointsReports = hr.DailyPointsReports.Where(a => a.UserName.Equals(username)).Take(12).ToList();
            return new JsonResult { Data = pointsReports, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
         }
        public ActionResult GetMyApprovedManagerVacationForReview(int ID)
        {
            var userName = hr.accountTBs.Where(item => item.ID == ID).Select(x=>x.EmpName).FirstOrDefault();
            ViewBag.UserNameForReport = userName;
            ViewBag.UserLeaveAccount = hr.accountTBs.Where(item => item.ID == ID).Select(x => x.AnnualVac).FirstOrDefault();
            decimal leavescount = Convert.ToDecimal(hr.accountTBs.Where(item => item.ID == ID).Select(x => x.AnnualVac).FirstOrDefault());
            ViewBag.UserCasualLeaveAccount = hr.accountTBs.Where(item => item.ID == ID).Select(x => x.CasualVac).FirstOrDefault();
            string lastcasual=Convert.ToDateTime(hr.accountTBs.Where(item => item.ID == ID).Select(x => x.LastCasualVacation).FirstOrDefault()).ToString("dd/MM/yyyy");
            DateTime test = Convert.ToDateTime(hr.accountTBs.Where(item => item.ID == ID).Select(x => x.LastCasualVacation).FirstOrDefault());
            if (test==DateTime.MinValue)
            {
                lastcasual = string.Empty;   
            }
            ViewBag.lastcasualvacation = lastcasual;
            ViewBag.TransferredVacations = hr.accountTBs.Where(item => item.ID == ID).Select(x => x.OldLeavesCount).FirstOrDefault();
            string exception = hr.accountTBs.Where(item => item.ID == ID).Select(x => x.Exception).FirstOrDefault();
            int count=0;
            if (exception=="true")
            {
                ViewBag.LeavesCount=30;
                count=30;
               
            }
            else
            {
                ViewBag.LeavesCount = 21;
                count=21;
            }
            decimal leavecountbybeginofyear = (decimal)ViewBag.TransferredVacations+ count;
            ViewBag.LeaveCountBeginingofYear = leavecountbybeginofyear;
            ViewBag.UserLeaves = count;
            decimal n =Convert.ToDecimal( hr.Requests.AsEnumerable().Where(item => item.ReqType == "إجازة").Where(p => p.UserName == userName).Where(p => p.ReqStatus == "ApprovedByManager").Sum(r=>r.ReqDuration));
            string spent = n.ToString("0.00");
            ViewBag.SpentHolidays = spent;
            return View("Requests", hr.Requests.Where(item => item.ReqType == "إجازة").Where(p => p.UserName == userName).Where(p => p.ReqStatus == "ApprovedByManager").Where(p=>p.ReqDate.Value.Year==DateTime.Now.Year).OrderByDescending(p=>p.ID));
        }
        public ActionResult GetMyApprovedManagerExcuseForReview(int ID)
        {
            var userName = hr.accountTBs.Where(item => item.ID == ID).Select(x => x.EmpName).FirstOrDefault();
            return View("Excuses", hr.Requests.Where(item => item.ReqType == "Excuse").Where(p => p.UserName == userName).Where(p => p.ReqDate.Value.Year == DateTime.Now.Year).Where(p => p.ReqStatus == "Approved").OrderByDescending(p => p.ID));

        }
        public ActionResult GetAllViewReportByTeamLeader(string EmpName) 
        {
            return View("Reports", hr.DailyReports.Where(p => p.UserName == EmpName).Where(p => p.RequestStatus == "ReviewedByTeamLeader" || p.RequestStatus == "ReviewedBySupervisor" || p.RequestStatus == "ReviewedByManager").Where(p=>p.Day.Value.Year==DateTime.Now.Year).OrderByDescending(p => p.ID).OrderByDescending(p=>p.ID));

        }
        public ActionResult DetailedReport()
        {
            var subdep = (from s in hr.SubDeps
                         select s.SubDepartmentName).ToList();
            var employees = (from s in hr.accountTBs
                             where s.EmpName!="Admin" && s.SubDepartmentName!= "Deleted"
                             select s.EmpName).ToList();
            ViewBag.subdep = subdep;
            ViewBag.employees = employees;
            return View();
        }
        public JsonResult ReportResult(string SubDep, string EmpName, string RequestType, string LeaveType, string From, string To)
        {
            var report = from r in hr.Requests select r;

            if (SubDep == "All" && RequestType == "All" && LeaveType == "All" && From == "" && To == "")
            {
                report = from r in report
                         where r.ID == 0
                         select r;
            }
            else
            {
                if (SubDep != "All")
                {
                   report = from r in report
                             where r.DepartmentName == SubDep
                             select r;
                }
                if (EmpName != "All")
                {
                    report = from r in report
                             where r.UserName == EmpName
                             select r;
                }
                if (RequestType != "All")
                {
                    report = from r in report
                             where r.ReqType == RequestType
                             select r;
                }
                if (LeaveType != "All")
                {
                    report = from r in report
                             where r.ReqSubType == LeaveType
                             select r;
                }
                if (From != "")
                {
                    DateTime ReqFrom = DateTime.Parse(From).Date;
                    DateTime ReqTo = DateTime.Parse(To).Date;
                    report = from r in report
                             where r.ReqDate >= ReqFrom
                             select r;
                }
                if (To != "")
                {
                    DateTime ReqFrom = DateTime.Parse(From).Date;
                    DateTime ReqTo = DateTime.Parse(To).Date;
                    report = from r in report
                             where r.ReqDate <= ReqTo
                             select r;
                }
            }
            return Json(report, JsonRequestBehavior.AllowGet);
        }
       
        
    }
}
