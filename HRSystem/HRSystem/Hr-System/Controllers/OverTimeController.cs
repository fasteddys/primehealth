using Hr_System.Classes;
using Hr_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
namespace Hr_System.Controllers
{
    public class OverTimeController : Controller
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

        public JsonResult getAllHolidays()
        {
            List<OffHoliday> allholidays = new List<OffHoliday>();
            allholidays = hr.OffHolidays.ToList();
            return new JsonResult { Data = allholidays, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpGet]
        public ActionResult AddNewOTRequest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewOTRequest(DateTime StartDate, DateTime EndDate, string overEnd, string overStart, int hoursno, string causeofover)
        {
            Name = (string)(Session["UserName"]);
            type = (string)(Session["UserType"]);
            Dep = (string)(Session["Dep"]);
            subDep = (string)(Session["subDep"]);
            arabicName = (string)(Session["ArabicName"]);
            string MyManager = obj.getDepManagerName(Dep);
            string MyTeamLeader = obj.getSubDepTeamLeaderName(subDep);
            string message = "";
            string overStatus;
            if (type == "Normal")
            {
                overStatus = "PendingTeamLeaderApproval";
            }
            else
            {
                overStatus = "PendingManagerApproval";
            }
            if (ModelState.IsValid)
            {

                OverTime ot = new OverTime();
                ot.StartOverDate = StartDate;
                ot.EndOverDate = EndDate;
                ot.OverEnd = overEnd;
                ot.OverStart = overStart;
                ot.OverStatus = overStatus;
                ot.NoOfHours = hoursno;
                ot.CauseOfOverTime = causeofover;
                ot.Creator = Name;
                ot.CreationTime = ReqDate;
                ot.MyTeamLeader = MyTeamLeader;
                ot.MyManager = MyManager;
                ot.ArabicName = arabicName;
                hr.OverTimes.Add(ot);
                hr.SaveChanges();
                message = "Successfully Saved!";
            }
            else
            {

                message = "Please provide required fields value";
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
        public ActionResult GetAllOverTimeRequestsForTeamLeader()
        {
            Name = (string)(Session["UserName"]);

            var data = (from f in hr.OverTimes
                        where f.OverStatus == "PendingTeamLeaderApproval" && f.MyTeamLeader == Name
                        select f).ToList();

            return View(data);
        }
        [HttpGet]
        public ActionResult GetAllOverTimeRequestsForManager(string user, string start, string end)
        {
            Name = (string)(Session["UserName"]);
            DateTime startdate = DateTime.Parse(start);
            DateTime enddate = DateTime.Parse(end);
            var data = (from f in hr.OverTimes
                        where f.OverStatus == "PendingManagerApproval" && f.MyManager == Name && f.Creator == user && (f.StartOverDate > startdate || f.StartOverDate == startdate) && (enddate > f.EndOverDate || enddate == f.EndOverDate)
                        select f).ToList();
            return View(data);
        }
        [HttpGet]
        public ActionResult GetAllOverTimeRequests()
        {
            Name = (string)(Session["UserName"]);
            var data = (from f in hr.OverTimes
                        where f.Creator == Name
                        select f).ToList();

            return View(data);
        }
        [HttpGet]
        public ActionResult UserOverTimeMonthly()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllApprovedOverTimeRequests(string sdate, string edate)
        {
            Name = (string)(Session["UserName"]);
            type = (string)(Session["UserType"]);
            DateTime startdate = DateTime.Parse(sdate);
            DateTime enddate = DateTime.Parse(edate);
            ViewBag.stdate = sdate;
            ViewBag.eddate = edate;
            ViewBag.start = startdate.ToShortDateString();
            ViewBag.end = enddate.ToShortDateString();
            var data = (from f in hr.OverTimes
                        where f.Creator == Name  && f.OverStatus == "ApprovedByManager" && (f.StartOverDate > startdate || f.StartOverDate == startdate) && (enddate > f.EndOverDate || enddate == f.EndOverDate)
                        select f).ToList();

            return View(data);
        }
        [HttpGet]
        public ActionResult GetAllRejectedOverTimeRequests()
        {
            Name = (string)(Session["UserName"]);
            var data = (from f in hr.OverTimes
                        where f.Creator == Name &&( f.OverStatus == "RejectedByManager"  || f.OverStatus == "RejectedByTeamLeader")
                        select f).ToList();

            return View(data);
        }
        [HttpGet]
        public ActionResult GetAllPendingOverTimeRequests()
        {
            Name = (string)(Session["UserName"]);
            var data = (from f in hr.OverTimes
                        where f.Creator == Name && (f.OverStatus == "PendingManagerApproval" || f.OverStatus == "PendingTeamLeaderApproval")
                        select f).ToList();

            return View(data);
        }
        [HttpGet]
        public ActionResult OverTimeDetails(int id)
        {
            OverTime ot = hr.OverTimes.Find(id);
            ViewBag.id = id;
            ViewBag.startdate = ot.StartOverDate.Value.ToShortDateString();
            ViewBag.enddate = ot.EndOverDate.Value.ToShortDateString();
            ViewBag.creator = ot.Creator;
            ViewBag.from = ot.OverStart;
            ViewBag.to = ot.OverEnd;
            ViewBag.cause = ot.CauseOfOverTime;
            return View();
        }
        [HttpPost]
        public ActionResult OverTimeDetails(string status, string comments, int id)
        {
            Name = (string)(Session["UserName"]);
            type = (string)(Session["UserType"]);
            string Message = "";
            if (ModelState.IsValid)
            {
                OverTime ot = hr.OverTimes.Find(id);
                ot.OverStatus = status;
                if (type == "TeamLeader")
                {
                    ot.RejectionTeamLeadercomment = comments;

                }
                else
                {
                    ot.RejectionManagerComment = comments;
                }
                hr.SaveChanges();
            }
            else
            {

            }
            if (Request.IsAjaxRequest())
            {
                Message = "Done";
                return new JsonResult { Data = Message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                Message = "Failed";
                return View();
            }

        }
        [HttpGet]
        public ActionResult OverTimePerMonths()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ManagerOverTime(string sdate, string edate)
        {
            Name = (string)(Session["UserName"]);
            type = (string)(Session["UserType"]);
            DateTime startdate = DateTime.Parse(sdate);
            DateTime enddate = DateTime.Parse(edate);
            ViewBag.stdate = sdate;
            ViewBag.eddate = edate;
            ViewBag.start = startdate.ToShortDateString();
            ViewBag.end = enddate.ToShortDateString();
            var data = (from f in hr.OverTimes
                        where f.MyManager == Name && f.OverStatus == "PendingManagerApproval" && (f.StartOverDate > startdate || f.StartOverDate == startdate) && (enddate > f.EndOverDate || enddate == f.EndOverDate)
                        select f.Creator).Distinct().ToList();
            ViewBag.users = data;
            return View();
        }

        public ActionResult getMyOverTime()
        {

            return View();
        }

        [HttpPost]
        public ActionResult getMyOverTime(int id = 1)
        {
           
                Name = (string)(Session["UserName"]);
                var data = hr.OverTimes.Where(p => (p.Creator == Name && p.OverStatus == "ApprovedByManager")).ToList();
                string path = @"~/OverTime/report.xlsx";
                string month = "2,3";
                xlbodArch(path, month, data, Name);
                return View();

           

            
        }

        private void xlbodArch(String x, string month, List<OverTime> ot, string Name)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.DisplayRightToLeft = true;

            int i = 0;
            int j = 0;
            Excel.Range rng = xlApp.get_Range("A1:G48");
            rng.Style = "Normal";
            rng.EntireRow.Font.Bold = true;
            rng.EntireRow.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            rng.EntireRow.Font.Size = 16;

            //First and second rows
            xlWorkSheet.Cells[3, 4] = "كشف ببيان ساعات العمل الإضافيه";
            xlWorkSheet.Cells[5, 4] = "عن شهر";
            xlWorkSheet.Cells[5, 5] = month;
            xlWorkSheet.Cells[7, 1] = "اسم الموظف";
            xlWorkSheet.Cells[7, 3] = Name;
            xlWorkSheet.Cells[8, 1] = "القـــســــم";
            xlWorkSheet.Cells[8, 3] = "IT";
            xlWorkSheet.Cells[8, 5] = "IT:الاداره ";
            xlWorkSheet.Cells[10, 1] = "اليوم";
            xlWorkSheet.Cells[10, 2] = "من";
            xlWorkSheet.Cells[10, 3] = "الي";
            xlWorkSheet.Cells[10, 4] = "اجمالي الساعات";
            xlWorkSheet.Cells[10, 5] = "سبب العمل الإضافي";
            xlWorkSheet.Cells[47, 1] = "الإجمالي ";
            xlWorkSheet.Cells[49, 1] = " الموارد البشريه  ";
            xlWorkSheet.Cells[49, 5] = "مدير الشئون القانونيه والموارد البشريه ";

            for (i = 0; i <= ot.Count() - 1; i++)
            {
                for (j = 0; j <= ot.Count()-4; j++)
                {

                    xlWorkSheet.Cells[i + 11, j + 1] = ot[i].StartOverDate.Value.Day;
                    xlWorkSheet.Cells[i + 11, j + 2] = ot[i].OverStart;
                    xlWorkSheet.Cells[i + 11, j + 3] = ot[i].OverEnd;
                    xlWorkSheet.Cells[i + 11, j + 5] = ot[i].CauseOfOverTime;
                }
            }


            for (int u = 10; u <= 46; u++)
            {

                xlWorkSheet.Range["A" + u].BorderAround2();
                xlWorkSheet.Range["A" + u].HorizontalAlignment = -4108;
                xlWorkSheet.Range["A" + u].VerticalAlignment = -4108;
                xlWorkSheet.Range["B" + u].BorderAround2();
                xlWorkSheet.Range["B" + u].HorizontalAlignment = -4108;
                xlWorkSheet.Range["B" + u].VerticalAlignment = -4108;
                xlWorkSheet.Range["C" + u].BorderAround2();
                xlWorkSheet.Range["C" + u].HorizontalAlignment = -4108;
                xlWorkSheet.Range["C" + u].VerticalAlignment = -4108;
                xlWorkSheet.Range["D" + u].BorderAround2();
                xlWorkSheet.Range["D" + u].HorizontalAlignment = -4108;
                xlWorkSheet.Range["D" + u].VerticalAlignment = -4108;
                xlWorkSheet.Range["E" + u].BorderAround2();
                xlWorkSheet.Range["E" + u].HorizontalAlignment = -4108;
                xlWorkSheet.Range["E" + u].VerticalAlignment = -4108;


            }
            Excel.Range rng2 = xlApp.get_Range("A47:A48");
            rng2.Style = "Normal";
            rng2.EntireRow.Font.Bold = true;
            rng2.EntireRow.ColumnWidth = 50;
            rng2.EntireRow.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            rng2.EntireRow.Font.Size = 11;

            Excel.Range rng3 = xlApp.get_Range("A49:E49");
            rng3.Style = "Normal";
            rng3.EntireRow.Font.Bold = true;
            rng3.EntireRow.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
            rng3.EntireRow.Font.Size = 16;



            xlWorkSheet.Range["A:R"].EntireColumn.AutoFit();

            //Response.ClearContent();
            //Response.AddHeader("Content-Disposition", "attachment; filename=Report.xlsx");
            //Response.Flush();
            xlWorkBook.Close();
             xlApp.Quit();
         //   releaseObject(xlWorkSheet);
           // releaseObject(xlWorkBook);
            //releaseObject(xlApp);

        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;

            }
            finally
            {
                GC.Collect();
            }
        }


        public JsonResult GetUserHaveOverTime(DateTime startdate, DateTime enddate, string Manager)
        {
            var user = (from f in hr.OverTimes
                        where f.MyManager == Manager && f.OverStatus == "PendingManagerApproval" && (f.StartOverDate < startdate && f.EndOverDate > enddate)
                        select f).Distinct().ToList();
            return new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public JsonResult updatefieldsbyname(string name, string start, string end, string status)
        {
            DateTime startdate = DateTime.Parse(start);
            DateTime enddate = DateTime.Parse(end);
            string message = "";
            var list = (from f in hr.OverTimes
                        where f.Creator == name && f.OverStatus == "PendingManagerApproval" && (f.StartOverDate > startdate || f.StartOverDate == startdate) && (enddate > f.EndOverDate || enddate == f.EndOverDate)
                        select f).ToList();
            if (ModelState.IsValid)
            {

                foreach (OverTime item in list)
                {
                    item.OverStatus = status;
                    hr.SaveChanges();

                }
                message = "Success";
            }
            else
            {
                message = "Failed";
            }
            return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult getOverTimeStatus(int id)
        {

            string message = "";
            var list = (from f in hr.OverTimes
                        where f.id == id 
                        select f).ToList().FirstOrDefault();
            if (ModelState.IsValid)
            {

                message = "Success";
            }
            else
            {
                message = "Failed";
            }
            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

   
    }
}
