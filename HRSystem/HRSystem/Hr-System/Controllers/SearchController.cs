using Hr_System.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Hr_System.Controllers
{
    public class SearchController : Controller
    {
        public HRSystemEntities db = new HRSystemEntities();

        // GET: /Search/
      static  List<int> DepartmentIdList = new List<int>();

        public ActionResult RequestsReport()
        {
            //ViewBag.AllDepartments = db.DepartmentTBs.ToList();


            //var SubDepartment = from d in db.DepartmentTBs
            //                    join s in db.SubDeps on d.ID equals s.DepartmentID
            //                    select new { s.SubDepartmentName, s.ID };
            //ViewBag.SubDept = SubDepartment;
            return View();
        }


        public JsonResult GetDepartmentList(string searchTerm)
        {
            var DepartmentList = db.DepartmentTBs.ToList();

            if (searchTerm != null)
            {
                DepartmentList = db.DepartmentTBs.Where(x => x.DeptName.Contains(searchTerm)).ToList();
            }

            var modifiedData = DepartmentList.Select(x => new
            {
                id = x.ID,
                text = x.DeptName
            });
            return Json(modifiedData, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetSubDeparmentsList(string DeparmentIDs)
        {
            DepartmentIdList.Clear();

            DepartmentIdList = DeparmentIDs.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            List<SubDep> SubDepartmentList = new List<SubDep>();
            foreach (int DeptID in DepartmentIdList)
            {
                db.Configuration.ProxyCreationEnabled = false;
                var listDataByDepartmentID = db.SubDeps.Where(x => x.DepartmentID == DeptID).ToList();
                foreach (var item in listDataByDepartmentID)
                {
                    SubDepartmentList.Add(item);
                }
            }

            SubDep sub = new SubDep() ;
            sub = db.SubDeps.Where(x => x.SubDepartmentName == "None").FirstOrDefault();
            SubDepartmentList.Add(sub);
            return Json(SubDepartmentList, JsonRequestBehavior.AllowGet);

        }


        public JsonResult GetUsersList(string SubDeptIDs)
        {
            List<int> SubDepartmentIdList = new List<int>();

            SubDepartmentIdList = SubDeptIDs.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            var subDepartment = db.SubDeps.ToList().Where(x => SubDeptIDs.Contains(x.ID.ToString())).Select(p => p).ToList();

            
           var selectdepartmentnames = db.DepartmentTBs.ToList().Where(x => DepartmentIdList.Contains(x.ID)).Select(p => p).ToList();


            var Users = (from SubDepID in subDepartment
                         join UsersTB in db.accountTBs on SubDepID.SubDepartmentName equals (UsersTB.SubDepartmentName)
                         join depname in selectdepartmentnames on UsersTB.DepartmentName equals (depname.DeptName)
                         select UsersTB
                         
                         
                         ).ToList();
            return Json(Users, JsonRequestBehavior.AllowGet);
        }


        public PartialViewResult SubmitSearch( string Departments, string SubDepartments, string User, string RequestsType, string ReportStartingDate, string ReportEndingDate)
        {
            

              List<  string >RequestTypeNames=new List<string>();
                List<string> ListOfNames = new List<string>();

                DateTime StartingDate;
                DateTime EndingTime;


                RequestTypeNames = RequestsType.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

                if (Departments != "" && SubDepartments == "" && User == "")
                {

                    var DepartmentsIds = Departments.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                  var DepartmentNames=  db.DepartmentTBs.Where(item => DepartmentsIds.Contains(item.ID)).Select(p => p.DeptName).ToList();
                    ListOfNames = db.accountTBs.Where(item => DepartmentNames.Contains(item.DepartmentName)&&item.SubDepartmentName!= "Deleted").Select(p => p.EmpName).ToList();
                }

              else  if (SubDepartments!=""&&User == "")
                {

                    var SubDepartmentsIds = SubDepartments.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                    var SubDepartmentNames = db.SubDeps.Where(item => SubDepartmentsIds.Contains(item.ID)).Select(p => p.SubDepartmentName).ToList();

                    ListOfNames = db.accountTBs.Where(item => SubDepartmentNames.Contains(item.SubDepartmentName) && item.SubDepartmentName != "Deleted").Select(p => p.EmpName).ToList();

                }
               else
                {
                    var UsersIds = User.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();


                    ListOfNames = db.accountTBs.Where(item => UsersIds.Contains(item.ID)).Select(p => p.EmpName).ToList();


                }


                if (RequestsType == "null" || RequestsType == null || RequestsType == "")
                {
                    RequestTypeNames.Clear();
                    RequestTypeNames.Add("Excuse");

                    RequestTypeNames.Add("إجازة");

                }


                if (ReportStartingDate == "" || ReportStartingDate == null || ReportStartingDate == "null")
                {
                    StartingDate = DateTime.MinValue;
                }
                else
                {
                    StartingDate = Convert.ToDateTime(ReportStartingDate);
                }
                if (ReportEndingDate == "" || ReportEndingDate == null || ReportEndingDate == "null")
                {
                    EndingTime = DateTime.Now;
                }
                else
                {
                    EndingTime = Convert.ToDateTime(ReportEndingDate);
                }




                  var RequestHandlerData = db.RequestHandlers.Where(
                      p => p.Offday >= StartingDate.Date && p.Offday <= EndingTime.Date &&p.RequestType== "إجازة").Select(p => p.OriginalRequestID.Value).ToList();
                if (RequestsType.Contains("Excuse")|| RequestTypeNames.Contains("Excuse"))
                {
                    RequestHandlerData.AddRange(db.Requests.Where(x=>x.ReqType== "Excuse").ToList().Where(p => Convert.ToDateTime(p.ExcuseDay) >=
                   StartingDate && Convert.ToDateTime(p.ExcuseDay) <= EndingTime).Select(p => p.ID).ToList());
                }
            List<Request> Result =  db.Requests
                                   .Where(item => ListOfNames.Contains(item.UserName))
                                   .Where(p => p.ReqStatus == "ApprovedByManager" || p.ReqStatus == "Approved")
                                  .Where(item => RequestHandlerData.Contains(item.ID))
                                   .Where(item => RequestTypeNames.Contains(item.ReqType))

                                   .ToList().OrderBy(x=>x.ID).ToList();
            return PartialView("_ViewHrReports", Result);



                //if (SubDepartments=="")

                //{

                //     UsersIds = User.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();


                //}





                //var DeparmentsNames = db.DepartmentTBs.Where(item => DepartmentsIds.Contains(item.ID)).Select(p => p.DeptName).ToList();
                //  var SubDeparmentsNames = db.SubDeps.Where(item => SubDepartmentsIds.Contains(item.ID)).Select(p => p.SubDepartmentName).ToList();



                //var annualIds = Account.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                //var annualVac = db.accountTBs.Where(item => annualIds.Contains(item.ID)).Select(p => p.AnnualVac).ToList();



                //DateTime StartingDate;
                //DateTime EndingTime;

                //if (ReportStartingDate == "" || ReportStartingDate == null || ReportStartingDate == "null")
                //{
                //    StartingDate = DateTime.MinValue;
                //}
                //else
                //{
                //    StartingDate = Convert.ToDateTime(ReportStartingDate);
                //}
                //if (ReportEndingDate == "" || ReportEndingDate == null || ReportEndingDate == "null")
                //{
                //    EndingTime = DateTime.Now;
                //}
                //else
                //{
                //    EndingTime = Convert.ToDateTime(ReportEndingDate);
                //}
                //if (SubDepartments == "null" || SubDepartments == null || SubDepartments == "")
                //{
                //    SubDeparmentsNames.Clear();
                //    SubDeparmentsNames = db.SubDeps.Where(item => DepartmentsIds.Contains(item.DepartmentID.Value)).Select(p => p.SubDepartmentName).ToList();

                //}

                //if (User == "null" || User == null || User == "")
                //{
                //    UserNames.Clear();
                //    UserNames = db.accountTBs.Where(item => SubDeparmentsNames.Contains(item.SubDepartmentName)).Select(p => p.EmpName).ToList();

                //}


           





                //.Where(item => DeparmentsNames.Contains(item.ManagmentName))
                //.Where(item => annualVac.Contains(item.CasualVacation))

                //Result = Result.Select(x=>x).Where(item => SubDeparmentsNames.Contains(item.DepartmentName)).ToList();
                // Result= Result
                // Result = Result.Where(item => RequestsType.Contains(item.ReqType)).ToList();
                // Result = Result.Where(p => p.ReqStatus == "ApprovedByManager" || p.ReqStatus == "Approved").ToList();


                //var Result = db.Requests.Where(item => DeparmentsNames.Contains(item.ManagmentName))
                //    .Where(item => SubDeparmentsNames.Contains(item.DepartmentName))
                //    .Where(item => UserNames.Contains(item.UserName))
                //    .Where(item => RequestTypeNames.Contains(item.ReqType))
                //    .Where(p => p.ReqStatus == "ApprovedByManager" || p.ReqStatus == "Approved").ToList();



            
         

        }


    }
}