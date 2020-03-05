using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Filteration.Models;
using System.Threading.Tasks;
using System.IO;
using ClosedXML.Excel;
using System.Data;
using System.Web.Routing;
using PagedList;
using PagedList.Mvc;
using System.Text.RegularExpressions;
using System.Web.UI;
using Newtonsoft.Json;
using System.Collections;


namespace Filteration.Controllers
{
    public class RequestController : Controller
    {
        TpaManagerEntities db = new TpaManagerEntities();

        public ActionResult QualityHome()
        {
            /*prevent browser back button after logout*/
            Response.Cache.SetCacheability(HttpCacheability.NoCache);// HTTP 1.1.
            Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");// HTTP 1.0.
            Response.AppendHeader("Expires", "0");// Proxies.

            if (Request.Cookies.Get("TPAUserNameCookie") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
                return View();
        }

        public ActionResult Dashboard(string status)
        {

            /*prevent browser back button after logout*/
            Response.Cache.SetCacheability(HttpCacheability.NoCache);// HTTP 1.1.
            Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");// HTTP 1.0.
            Response.AppendHeader("Expires", "0");// Proxies.


            if (Request.Cookies.Get("TPAUserNameCookie") != null)
            {
                var today = DateTime.Today;
                string UserName = Request.Cookies["TPAUserNameCookie"].Value;

                //.............................................................................

                var info = (from item in db.Batches
                            where item.AssignedDate >= today && item.AssignedPerson == UserName
                            select item).ToList();
               
                ViewBag.NumofBatches = info.Count();

                foreach (var Batchitem in info)
                {
                    if (Batchitem.BatchStatus == "Assigned" || Batchitem.BatchStatus == "ReadyToQuality"
                        || Batchitem.BatchStatus == "AssignedByQuality" || Batchitem.BatchStatus == "QualityFinished")
                    {
                        var dat = (from claim in db.Batches
                                   where claim.AssignedPerson == UserName
                                   && claim.AssignedDate >= today
                                   select claim.NumberOfClaims).Sum();

                        ViewBag.SumofClaims = dat;

                    }

                }
                //end

                if (Request.Cookies["UserTypeCookie"].Value == "TPA" || Request.Cookies["UserTypeCookie"].Value == "TPAadmin" || status == "TPA")
                {
                    var data = (from item in db.Requests
                                where item.RequestStatus == "New"
                                select item).ToList();
                    ViewBag.Status = status;
                    return View(data);
                }

                else if (Request.Cookies["UserTypeCookie"].Value == "Quality" || Request.Cookies["UserTypeCookie"].Value == "QualityAdmin"
                    || Request.Cookies["UserTypeCookie"].Value == "Deputy Manager")
                {
                    var data = (from item in db.Requests
                                where item.RequestStatus == "PendingQuality"
                                select item).ToList();
                    ViewBag.Status = status;
                    return View(data);
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpGet]
        public ActionResult ListRequestedBatches(Batch batch, int? id, string status)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);// HTTP 1.1.
            Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");// HTTP 1.0.
            Response.AppendHeader("Expires", "0");// Proxies.

            if (Request.Cookies.Get("TPAUserNameCookie") == null)
            {
                return RedirectToAction("Login", "Login");
            }


            var data = (from item in db.Batches
                        where item.ReqId == id
                        select item).ToList();
            ViewBag.Status = status;
            ViewBag.RequestID = id;

            int NewBatchesCounter = 0;
            int FinishedBatch = 0;

            ViewBag.BatchesNumber = data.Count();



            if (status == "TPA" || Request.Cookies["UserTypeCookie"].Value == "TPA" || Request.Cookies["UserTypeCookie"].Value == "TPAadmin")
            {
                foreach (var item in data)
                {
                    if (item.BatchStatus == "NEW")
                    {
                        NewBatchesCounter++;
                    }
                    if (item.BatchStatus == "Assigned" || item.BatchStatus == "ReadyToQuality")
                    {
                        FinishedBatch++;
                    }

                }
                ViewBag.Status = status; //new
            }

            if (status == "Quality")
            {
                foreach (var item in data)
                {
                    if (item.BatchStatus == "ReadyToQuality")
                    {
                        NewBatchesCounter++;
                    }
                    if (item.BatchStatus == "AssignedByQuality" || item.BatchStatus == "QualityFinished")
                    {
                        FinishedBatch++;
                    }
                }



                int item_Counter = 0;

                foreach (var item in data)
                {
                    if (item.BatchStatus == "QualityFinished")
                    {
                        item_Counter++;
                    }

                }

                if (item_Counter == ViewBag.BatchesNumber)
                {
                    Request request = new Request();
                    request = db.Requests.Find(batch.ReqId);
                    ViewBag.Quality = "ShowCloseTicketButton";

                }

                ViewBag.Status = status; //new
            }




            ViewBag.DynamicFinishedBatch = FinishedBatch;
            ViewBag.DynamicNewBatchesCounter = NewBatchesCounter;



            string x = logic.ViewOrHide(batch, id);
            Session["CheckStatus"] = x;




            int itemCounter = 0;

            foreach (var item in data)
            {
                if (item.BatchStatus == "ReadyToQuality")
                {
                    itemCounter++;
                }

            }

            if (itemCounter == ViewBag.BatchesNumber)
            {
                Request request = new Request();
                request = db.Requests.Find(batch.ReqId);
                ViewBag.Ready = "ShowButton";

            }


            return View(data);

        }


        public JsonResult AddnewBatch(int id, string batchNumber)
        {
            using (TpaManagerEntities dbcontext=new TpaManagerEntities())
            {
                Batch bat = new Batch();
                Request req = dbcontext.Requests.Where(p => p.Id == id).FirstOrDefault();
                bat.BatchNumber = batchNumber;
                bat.BatchStatus = "NEW";
                bat.ReqId = req.Id;
                bat.TicketNumber = req.TicketNumber;
                dbcontext.Batches.Add(bat);
                req.NumberOfBatches += 1;
                dbcontext.SaveChanges();
                return Json(bat, JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult PendingQuality(int? id)
        {

            Request request = db.Requests.Find(id);

            request.RequestStatus = "PendingQuality";
            request.SentToQualityBy = Request.Cookies["TPAUserNameCookie"].Value;
            request.RequestClosedTicketDate = DateTime.Now;
            db.SaveChanges();
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult TPAClosedRequest(string status)
        {
            /*prevent browser back button after logout*/
            Response.Cache.SetCacheability(HttpCacheability.NoCache);// HTTP 1.1.
            Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");// HTTP 1.0.
            Response.AppendHeader("Expires", "0");// Proxies.

            if (Request.Cookies.Get("TPAUserNameCookie") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.Status = status;
            var data = (from item in db.Requests
                        where item.RequestStatus == "PendingQuality"
                        select item).ToList();
            return View(data);
        }


        public JsonResult CloseQuality(int? id)
        {
            Request request = db.Requests.Find(id);
            request.ClosedBy = Request.Cookies["TPAUserNameCookie"].Value;
            request.RequestStatus = "ClosedByQuality";
            request.ReqClosedQualityDate = DateTime.Now;


            db.SaveChanges();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        //[HttpGet]
        //public ActionResult Search(string BatchNumber, string status)
        //{
        //    var yesterday = DateTime.Today.AddDays(-1);
        //    var data = (from item in db.Batches
        //                    where item.AssignedDate == yesterday
        //                    select item).ToList();
        //    ViewBag.Status = status;
        //    return View(data);
        //}
        //[HttpPost]




        //public ActionResult Search(string BatchNumber, int? Reqid, string status)
        //{
        //    /* prevent browser back button after logout */
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);// HTTP 1.1.
        //    Response.Cache.AppendCacheExtension("no-store, must-revalidate");
        //    Response.AddHeader("Pragma", "no-cache");// HTTP 1.0.
        //    Response.AppendHeader("Expires", "0");// Proxies.

        //    if (Request.Cookies.Get("TPAUserNameCookie") == null)
        //    {
        //        return RedirectToAction("Login", "Login");
        //    }

        //    ViewBag.Status = status;

        //    /*remove tabs and new lines before search item*/
        //    string BatchNum = Regex.Replace(BatchNumber, "\\s+", "");


        //    var data = (from item in db.Batches
        //                where item.BatchNumber == BatchNum
        //                select item).ToList();

        //    var Tickets = (from R in db.Requests
        //                   join B in db.Batches
        //                   on R.Id equals B.ReqId
        //                   where B.BatchNumber == BatchNum
        //                   select R.RequestStatus).ToList();


        //    foreach (string Ticket in Tickets)
        //    {
        //        ViewBag.TicketStatus = Ticket;
        //    }


        //    ViewBag.CountBatches = data.Count();
        //    ViewBag.RetrievedBatches = data;
        //    var anyBatches = new SelectList(data, "Id", "BatchNumber");

        //    ViewBag.RetrievedBatchess = JsonConvert.SerializeObject(anyBatches, Formatting.Indented, new JsonSerializerSettings
        //    { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
        //    ViewBag.RetrievedBatchess = JsonConvert.SerializeObject(anyBatches);
        //    var countbatch = data.Count();
        //    if (countbatch == 0)
        //    {

        //        return HttpNotFound("This Batch doesn't exist, Are you sure You entered the correct batch number?");
        //    }

        //    return View(data);
        //}


        public ActionResult NewSearch(string SearchBy, string SearchValue)
        {

            var Result = new List<Request>();

            if (SearchBy == "BatchNumber")
            {
                if (SearchBy == "BatchNumber")
                {
                    var data = (from item in db.Batches
                                where item.BatchNumber == SearchValue
                                select item).ToList();

                    var Tickets = (from R in db.Requests
                                   join B in db.Batches
                                   on R.Id equals B.ReqId
                                   where B.BatchNumber == SearchValue
                                   select R.RequestStatus);

                    foreach (string Ticket in Tickets)
                    {
                        ViewBag.TicketStatus = Ticket;
                    }
                    ViewBag.CountBatches = data.Count();
                    ViewBag.RetrievedBatches = data;
                    var anyBatches = new SelectList(data, "Id", "BatchNumber");
                    ViewBag.RetrievedBatchess = JsonConvert.SerializeObject(anyBatches);
                    if (data.Count == 0)
                    {
                        return new EmptyResult();
                    }

                    else
                    {
                        return View("Search", data);
                    }
                }

            }

            else if (SearchBy == "TicketNumber")
            {
                try
                {
                    int TicketNumber = Convert.ToInt32(SearchValue);
                    Result.Add(db.Requests.Where(x => x.TicketNumber == TicketNumber).FirstOrDefault());
                }
                catch (FormatException)
                {
                }
            }

            else if (SearchBy == "Month")
            {
                try
                {
                    int Month = Convert.ToInt32(SearchValue);
                    Result.AddRange(db.Requests.Where(x => x.Month == Month).ToList());

                }
                catch (FormatException)
                {
                }
            }

            else if (SearchBy == "Year")
            {
                try
                {
                    int Year = Convert.ToInt32(SearchValue);
                    Result.AddRange(db.Requests.Where(x => x.Year == Year).ToList());
                }
                catch (FormatException)
                {
                }
            }

            else if (SearchBy == "Assign Person")
            {
                try
                {
                    string AssignPerson = Convert.ToString(SearchValue);
                    var AllBatchesByPerson = db.Batches.Where(x => x.AssignedPerson == AssignPerson).ToList();
                    List<Request> allRequests = new List<Request>();
                    foreach (var item in AllBatchesByPerson)
                    {
                        var Requests = db.Requests.Where(p => p.TicketNumber == item.TicketNumber).ToList();
                        Result.AddRange(Requests);
                    }
                }
                catch (FormatException)
                {
                }
            }

            else if (SearchValue == " " || SearchValue == null || SearchValue != "TicketNumber" || SearchValue != "Month" || SearchValue != "BatchNumber")
            {
                return new EmptyResult();
            }

            return View("Dashboard", Result);           
        }





        public ActionResult AddRequest()
        {
            /*prevent browser back button after logout*/
            Response.Cache.SetCacheability(HttpCacheability.NoCache);// HTTP 1.1.
            Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");// HTTP 1.0.
            Response.AppendHeader("Expires", "0");// Proxies.

            if (Request.Cookies.Get("TPAUserNameCookie") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        [HttpPost]
        public JsonResult AddNewRequest(string title, int ticketNum, int month, int year)
        {
            string[] lines = title.Split(new string[] { "\n", "\n\r\n\r " }, StringSplitOptions.None);
            string[] test = lines.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            long[] Numbers = test.Select(s => long.Parse(s)).ToArray();
            string msg = "";

            var ListOfTickets = (from ticket in db.Requests
                                 select ticket.TicketNumber).ToList();

            for (int i = 0; i < ListOfTickets.Count; i++)
            {
                if (ListOfTickets[i].Value == ticketNum)
                {

                    ListOfTickets.RemoveAt(i);
                    msg = string.Format("Ticket {0} already exists, enter a different ticket number", ticketNum);
                    return Json(msg, JsonRequestBehavior.AllowGet);


                }

            }

            Request request = new Request();
            request.NumberOfBatches = Numbers.Count();
            request.RequestDate = DateTime.Now;
            request.StartBatch = Numbers.Min();
            request.EndBatch = Numbers.Max();
            request.TicketNumber = ticketNum;
            request.UploaderName = Request.Cookies["TPAUserNameCookie"].Value;
            request.RequestStatus = "New";
            request.Month = month;
            request.Year = year;



            for (int i = 0; i < ListOfTickets.Count; i++)
            {
                if (ListOfTickets[i].Value != ticketNum)
                {
                    db.Requests.Add(request);
                    db.SaveChanges();

                    TempData["InsertedId"] = request.Id;
                    TempData["Ticket"] = request.TicketNumber;

                    string Ticket_num = TempData["Ticket"].ToString();
                    string Ticket_ID = TempData["InsertedId"].ToString();

                    logic.insertbatch(request, Numbers, int.Parse(Ticket_ID), int.Parse(Ticket_num));
                    msg = "You created the ticket successfully";

                    return Json(msg, JsonRequestBehavior.AllowGet);
                }

            }

            return Json("");

        }

        public ActionResult QualityClosedRequest(string status)
        {
            /*prevent browser back button after logout*/
            Response.Cache.SetCacheability(HttpCacheability.NoCache);// HTTP 1.1.
            Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");// HTTP 1.0.
            Response.AppendHeader("Expires", "0");// Proxies.

            if (Request.Cookies.Get("TPAUserNameCookie") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.Status = status;
            var data = (from item in db.Requests
                        where item.RequestStatus == "ClosedByQuality"
                        select item).ToList();
            return View(data);
        }


        public ActionResult EditProfile(int id, string status)
        {
            /*prevent browser back button after logout*/
            Response.Cache.SetCacheability(HttpCacheability.NoCache);// HTTP 1.1.
            Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");// HTTP 1.0.
            Response.AppendHeader("Expires", "0");// Proxies.

            if (Request.Cookies.Get("TPAUserNameCookie") == null)
            {
                return RedirectToAction("Login", "Login");
            }
            ViewBag.Status = status;
            User user = db.Users.Find(id);
            return View(user);
        }
        public JsonResult ChangePassword(int id, string CurrentPassword, string NewPassword, string ConfirmNewPassword)
        {

            User CurrentUser = db.Users.Find(id);
            string msg = "";
            ViewBag.CurPass = CurrentUser.Password;
            try
            {


                if (CurrentPassword != CurrentUser.Password)
                {

                    return Json(CurrentUser, JsonRequestBehavior.AllowGet);
                }

                else if (CurrentPassword == NewPassword)
                {

                    return Json(CurrentUser, JsonRequestBehavior.AllowGet);
                }
                else if (ConfirmNewPassword != NewPassword)
                {

                    return Json(CurrentUser, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    CurrentUser.Password = NewPassword;

                    db.SaveChanges();
                    return Json(CurrentUser, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                msg = "Something Went Wrong Please Contact IT";
                return Json(msg, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
       
        public ActionResult Export(int id)
        {
            Request request = db.Requests.Find(id);
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[7] {new DataColumn("TicketNumber"), new DataColumn("BatchNumber"),
               new DataColumn("BatchStatus"),new DataColumn("NumberOfClaims"),new DataColumn("Boxes"), new DataColumn("AssignedPerson"),
               new DataColumn("AssignedDate")});

            var batchees = (from item in db.Batches
                            where item.ReqId == id
                            select item);

            foreach (var thing in batchees)
            {
                dt.Rows.Add(thing.TicketNumber, thing.BatchNumber, thing.BatchStatus, thing.NumberOfClaims, thing.Boxes,
                    thing.AssignedPerson, thing.AssignedDate);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "sheet1");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "utf-8";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Sheet.xlsx");
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    stream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("Dashboard", "Request");

        }

        [HttpPost]

        public ActionResult ExportReport(string status)
        {
            // new { info = ViewBag.ComplexInfo },
            if (status == "TPA" || Request.Cookies["UserTypeCookie"].Value == "TPAadmin")
            {
                ViewBag.Status = status;
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[7] {new DataColumn("TicketNumber"), new DataColumn("BatchNumber"),
               new DataColumn("AssignedPerson"),new DataColumn("AssignedDate"), new DataColumn("TPAFinshedTime"), 
               new DataColumn("NumberOfClaims"), new DataColumn("BatchStatus")});

                IEnumerable<Batch> info = (List<Batch>)TempData["TPAReport"];

                foreach (var item in info)
                {
                    dt.Rows.Add(item.TicketNumber, item.BatchNumber, item.AssignedPerson, item.AssignedDate,
                    item.TPAFinshedTime, item.NumberOfClaims, item.BatchStatus);
                }
                using (XLWorkbook Rep = new XLWorkbook())
                {
                    Rep.Worksheets.Add(dt, "sheet1");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "utf-8";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Sheet.xlsx");
                    using (MemoryStream stream = new MemoryStream())
                    {
                        Rep.SaveAs(stream);
                        stream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            else if (status == "Quality")
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[7] {new DataColumn("TicketNumber"), new DataColumn("BatchNumber"),
               new DataColumn("	AssignedQuality"),new DataColumn("AssignedQualityDate"), new DataColumn("QualityClaimsNumber"), 
               new DataColumn("NumberOfClaims"), new DataColumn("BatchStatus")});

                IEnumerable<Batch> Qualityinfo = (List<Batch>)TempData["QualityReport"];

                foreach (var item in Qualityinfo)
                {
                    dt.Rows.Add(item.TicketNumber, item.BatchNumber, item.AssignedQuality, item.AssignedQualityDate,
                    item.QualityClaimsNumber, item.NumberOfClaims, item.BatchStatus);
                }
                using (XLWorkbook Rep = new XLWorkbook())
                {
                    Rep.Worksheets.Add(dt, "sheet1");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "utf-8";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Sheet.xlsx");
                    using (MemoryStream stream = new MemoryStream())
                    {
                        Rep.SaveAs(stream);
                        stream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }

            // return RedirectToAction("Report", "Request", new { status = ViewBag.Status });
            return RedirectToAction("Report", new RouteValueDictionary(new { controller = "Request", action = "Report", status = ViewBag.Status }));
        }

        [HttpGet]
        public ActionResult Report(string status)
        {
            /*prevent browser back button after logout*/
            Response.Cache.SetCacheability(HttpCacheability.NoCache);// HTTP 1.1.
            Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            Response.AddHeader("Pragma", "no-cache");// HTTP 1.0.
            Response.AppendHeader("Expires", "0");// Proxies.

            if (Request.Cookies.Get("TPAUserNameCookie") == null)
            {
                return RedirectToAction("Login", "Login");
            }

            ViewBag.Status = status;
            ViewBag.UserName = new SelectList(db.Users, "UserName", "UserName");

            //new
            //IEnumerable Getinfo = (IEnumerable)TempData["SortingInto"];
            //if(TempData["userName"] != null)
            //{
            //    var UserName = TempData["userName"].ToString();
            //}
            
            //ViewBag.SortOrder = sortOrder;
            //ViewBag.NameSorting = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewBag.DateSorting = sortOrder == "Date" ? "date_desc" : "Date";

            //switch (sortOrder)
            //{
            //    case "name_desc":
            //        Getinfo = Getinfo.OrderByDescending(s => s.AssignedPerson).ToList();
            //        break;
            //    case "Date":
            //        Getinfo = Getinfo.OrderBy(p => p.AssignedDate).ToList();
            //        break;
            //    case "date_desc":
            //        Getinfo = Getinfo.OrderByDescending(x => x.AssignedDate).ToList();
            //        break;
            //    default:
            //        Getinfo = Getinfo.OrderBy(a => a.AssignedPerson).ToList();
            //        break;
            //}
            //End

            return View();
        }

        [HttpPost]
        public ViewResult Report(string UserName, string FromDate, string ToDate, string status)
        {
            DateTime From = DateTime.Parse(FromDate);
            DateTime to = DateTime.Parse(ToDate);


            TimeSpan TotaDays = to - From;

            double TotalDays = TotaDays.TotalDays;
            ViewBag.TotalDays = TotalDays;
            ViewBag.SelectedPerson = UserName;
            ViewBag.Status = status;
            ViewBag.UserName = new SelectList(db.Users, "UserName", "UserName");

            
            if (status == "TPA" || Request.Cookies["UserTypeCookie"].Value == "TPAadmin")
            {

                var info = (from item in db.Batches
                            where (item.AssignedPerson == UserName)
                            && item.AssignedDate >= From && item.AssignedDate <= to
                            orderby item.AssignedDate
                            select item).ToList();
                //NEW
                //TempData["SortingInto"] = info;
                //TempData["userName"] = UserName;
               
                //END
                TempData["TPAReport"] = info;
                ViewBag.ComplexInfo = info;

                var TotalBatches = info.Count();
                ViewBag.TotlBatch = TotalBatches;


                var newdat = (from claimNum in info
                              where (claimNum.AssignedPerson == UserName)
                              && claimNum.AssignedDate >= From && claimNum.AssignedDate <= to
                              select claimNum.NumberOfClaims).Sum();

                ViewBag.Totalclaim = newdat;


                return View(info);
            }
            //new 
            else if (status == "Quality")
            {
                var QualityInfo = (from QualityItem in db.Batches
                                   where (QualityItem.AssignedQuality == UserName)
                                   && QualityItem.AssignedQualityDate >= From && QualityItem.AssignedQualityDate <= to
                                   orderby QualityItem.AssignedQualityDate
                                   select QualityItem).ToList();

                TempData["QualityReport"] = QualityInfo;
                ViewBag.ComplexQualityInfo = QualityInfo;
                var TotalQualityBatches = QualityInfo.Count();
                ViewBag.TotalQualitylBatch = TotalQualityBatches;

                var NewData = (from claimNums in QualityInfo
                               where (claimNums.AssignedQuality == UserName)
                              && claimNums.AssignedQualityDate >= From && claimNums.AssignedQualityDate <= to
                               select claimNums.NumberOfClaims).Sum();

                ViewBag.TotalQualityclaim = NewData;
                return View(QualityInfo);

            }
            return View();
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
