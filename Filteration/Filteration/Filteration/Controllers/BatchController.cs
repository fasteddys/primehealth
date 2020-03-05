using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using Filteration.Models;
using System.Data.Entity;
using System.Net;
using System.Web.Routing;

namespace Filteration.Controllers
{
    public class BatchController : Controller
    {
        private TpaManagerEntities db = new TpaManagerEntities();

        public ActionResult Index()
        {
            var batches = db.Batches.Include(batch => batch.Request);
            return View(batches);
        }

        //public ActionResult AddBatch()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public JsonResult AddNewBatch(int TicketNumber, string BatchNumber)
        //{
        //    Batch Newbatch = new Batch();
        //    Request req = new Request();
        //    req = db.Requests.SingleOrDefault(x => x.TicketNumber == TicketNumber);
        //    Newbatch.BatchNumber = BatchNumber;
        //    Newbatch.BatchStatus = "NEW";
        //    Newbatch.ReqId = req.Id;
        //    Newbatch.TicketNumber = TicketNumber;
        //    db.Batches.Add(Newbatch);
        //    db.SaveChanges();

        //    return Json(Newbatch, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult EditBatchNumber(int? id, string status)
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

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Status = status;
            Batch batch = db.Batches.Find(id);

            return View(batch);
        }

        [HttpPost]
        public JsonResult EditConfirmed(string id, string batchnumber, string NumberOfClaims)
        {
            int IdConverter = Convert.ToInt32(id);
            Batch batch = db.Batches.Find(IdConverter);

            batch.BatchNumber = batchnumber;

            batch.NumberOfClaims = int.Parse(NumberOfClaims);

            db.SaveChanges();
            return Json(batch, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditQualityConfirmed(int id, string batchNumber, int Numclaim, int QualityClaimsNum, int NumofbadStamp,
            int NumofnoStamp, int NumofbadPhoto, int NumofnoPhoto)
        {
            Batch batch = db.Batches.Find(id);

            batch.BatchNumber = batchNumber;
            batch.NumberOfClaims = Numclaim;//new
            batch.QualityClaimsNumber = QualityClaimsNum;
            batch.NumOfBadStamp = NumofbadStamp;
            batch.NumOfNoStamp = NumofnoStamp;
            batch.NumOfBadPhoto = NumofbadPhoto;
            batch.NumOfNoPhoto = NumofnoPhoto;

            db.SaveChanges();
            return Json(batch, JsonRequestBehavior.AllowGet);
        }



        public ActionResult DeleteBatchNumber(int? id, string status)
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = db.Batches.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }

            return View(batch);
        }

        [HttpPost, ActionName("DeleteBatchNumber")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Batch batch = db.Batches.Find(id);
            int? ID = batch.ReqId;
            Request request = db.Requests.Find(batch.ReqId);
            db.Batches.Remove(batch);
            request.NumberOfBatches -= 1;
            db.SaveChanges();
            return RedirectToAction("ListRequestedBatches", new RouteValueDictionary
                (new { controller = "Request", action = "ListRequestedBatches", Id = ID }));
        }




        public ActionResult AssignBatch_BatchDetails(int? id, int? RequestID, string status)
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


            Batch batch = db.Batches.Find(id);
            ViewBag.UserName = new SelectList(db.Users, "UserName", "UserName");

            return View(batch);
        }

        [HttpPost]
        public JsonResult Assign(Batch batch, int id, int RequestID)
        {

            batch = db.Batches.Find(id);
            batch.AssignedPerson = Request.Cookies["TPAUserNameCookie"].Value;


            batch.BatchStatus = "Assigned";
            batch.AssignedDate = DateTime.Now;


            db.SaveChanges();
            return Json(batch, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult TransferBatch(Batch batch, string UserName, int id)
        {
            batch = db.Batches.Find(id);
            batch.AssignedPerson = UserName;
            batch.AssignedDate = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("AssignBatch_BatchDetails", new RouteValueDictionary
                (new { controller = "Batch", action = "AssignBatch_BatchDetails", Id = id }));
           
        }
        public ActionResult AssignedBatches(string sortOrder, string username, string status)
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
            var today = DateTime.Today;
            if (Request.Cookies["UserTypeCookie"].Value == "TPA" || Request.Cookies["UserTypeCookie"].Value == "Quality")
            {
                var item = (from person in db.Batches
                            where person.AssignedPerson == username && person.AssignedDate >= today
                            select person).ToList();
                ViewBag.Status = status;
                return View(item);
            }


            if (Request.Cookies["UserTypeCookie"].Value == "TPAadmin" || Request.Cookies["UserTypeCookie"].Value == "QualityAdmin")
            {
                var item = (from worker in db.Batches
                            where (worker.BatchStatus == "Assigned" || worker.BatchStatus == "QualityFinished"
                            || worker.BatchStatus == "ReadyToQuality" || worker.BatchStatus == "AssignedByQuality") && worker.AssignedDate >= today
                            select worker).ToList();
                ViewBag.Status = status;

                //NEW
                ViewBag.Nusername = username;
                ViewBag.SortOrder = sortOrder;
                ViewBag.SortNameParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.SortDateParam = sortOrder == "Date" ? "date_desc" : "Date";

                switch (sortOrder)
                {
                    case "name_desc":
                        item = item.OrderByDescending(s => s.AssignedPerson).ToList();
                        break;
                    case "Date":
                        item = item.OrderBy(p => p.AssignedDate).ToList();
                        break;
                    case "date_desc":
                        item = item.OrderByDescending(x => x.AssignedDate).ToList();
                        break;
                    default:
                        item = item.OrderBy(a => a.AssignedPerson).ToList();
                        break;
                }

                return View(item);
            }
            else
            {
                return View();
            }

        }

        public ActionResult AssignedBatchesPerQuality(string UserName, string status)
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
            var today = DateTime.Today;
            if (Request.Cookies["UserTypeCookie"].Value == "Quality")
            {
                var data = (from Someone in db.Batches
                            where Someone.AssignedQuality == UserName && Someone.AssignedDate >= today
                            select Someone).ToList();
                return View(data);
            }
            else if (Request.Cookies["UserTypeCookie"].Value == "QualityAdmin")
            {
                var data = (from SomeBody in db.Batches
                            where (SomeBody.BatchStatus == "AssignedByQuality" || SomeBody.BatchStatus == "QualityFinished")
                            && SomeBody.AssignedQualityDate >= today
                            select SomeBody).ToList();
                return View(data);
            }
            else
            {
                return View();
            }
        }

        public JsonResult SendToQuality(Batch batch, int id, string txt,string TXXT)
        {
      
            batch = db.Batches.Find(id);

            batch.BatchStatus = "ReadyToQuality";

            batch.TPAFinshedTime = DateTime.Now;

            batch.NumberOfClaims = int.Parse(txt);
            batch.Boxes = TXXT;
            
            db.SaveChanges();
            return Json(batch, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AcceptBatchQuality(int? id, string status)
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
            Batch batch = db.Batches.Find(id);
            return View(batch);
        }

        public JsonResult AssignQualityBatch(Batch batch, int id)
        {
            batch = db.Batches.Find(id);
            batch.AssignedQuality = Request.Cookies["TPAUserNameCookie"].Value;
            batch.BatchStatus = "AssignedByQuality";
            batch.AssignedQualityDate = DateTime.Now;

            db.SaveChanges();

            return Json(batch, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult FinishBatch(Batch batch, int id, int QualityClaimNum, int NumberOfBadStamp,
            int NumberOfNoStamp, int NumberOfBadPhoto, int NumberOfNoPhoto)
        {
            batch = db.Batches.Find(id);

            batch.BatchStatus = "QualityFinished";
            batch.ClosedDate = DateTime.Now;
            batch.ClosedBy = Request.Cookies["TPAUserNameCookie"].Value;

            batch.QualityClaimsNumber = QualityClaimNum;
            batch.NumOfBadStamp = NumberOfBadStamp;
            batch.NumOfNoStamp = NumberOfNoStamp;
            batch.NumOfBadPhoto = NumberOfBadPhoto;
            batch.NumOfNoPhoto = NumberOfNoPhoto;



            db.SaveChanges();
            return Json(batch, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ReviewBatch(Batch batch, int id, int REvQualityClaimNum, int REvNumberOfBadstamps, int REvNumberofNoStamps,
            int REvNumberOfBadPhoto, int REvNumberOfNophoto)
        {
            batch = db.Batches.Find(id);
            batch.BatchStatus = "UnderReview";
            batch.QualityClaimsNumber = REvQualityClaimNum;
            batch.NumOfBadStamp = REvNumberOfBadstamps;
            batch.NumOfNoStamp = REvNumberofNoStamps;
            batch.NumOfBadPhoto = REvNumberOfBadPhoto;
            batch.NumOfNoPhoto = REvNumberOfNophoto;

            db.SaveChanges();

            return Json(batch, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Review(string status)
        {
            ViewBag.Status = status;
            var data = (from item in db.Batches
                        where item.BatchStatus == "UnderReview"
                        select item).ToList();

            return View(data);
        }

        public ActionResult Review_Finish(int? id, string status)
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
            Batch batch = db.Batches.Find(id);

            return View(batch);
        }

        public JsonResult ReviewAndFinishBatch(Batch batch, int id, int RevClaimNum)
        {
            batch = db.Batches.Find(id);
            batch.BatchStatus = "QualityFinished";
            batch.ClosedDate = DateTime.Now;
            batch.ReviewedClaimNumber = RevClaimNum;
            batch.ClosedBy = Request.Cookies["TPAUserNameCookie"].Value;

            db.SaveChanges();

            return Json(batch, JsonRequestBehavior.AllowGet);
        }


    }
}
