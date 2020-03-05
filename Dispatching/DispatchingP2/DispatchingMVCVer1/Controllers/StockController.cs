using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DispatchingMVCVer1.Models;
using PagedList;

namespace DispatchingMVCVer1.Controllers
{
    public class StockController : Controller
    {
        DisPatchingDBEntities db = new DisPatchingDBEntities();
        // GET: Stock
        public ActionResult AddToStock()
        {
            return View();
        }
        public JsonResult AddItem(string ItemType , string NumOfBocklets , string StartClaims)
        {
            int ClaimsInStock = 0;
            string message="";
            string ActiveUser = Request.Cookies["UserNameCookie"].Value;
            StockRequest Req = new StockRequest();

            int claims = int.Parse(StartClaims);
            int? EndClaim = claims + (int.Parse(NumOfBocklets) * 25) - 1;
            for (int i = claims; i <= EndClaim; i += 25)
            {

                var ClaimInStock = (from r in db.StockRequests
                                    where r.StartClaimNo <= i && r.EndClaimNo >= i
                                    select r).Count();

                if (ClaimInStock > 0)
                {
                    ClaimsInStock += 1;
                }
            }
            if (ClaimsInStock > 0)
            {
                message = "ClaimIsIn";

            }
            else
            {
                Req.ItemName = ItemType;
                Req.NumOfBocklets = int.Parse(NumOfBocklets);
                Req.StartClaimNo = int.Parse(StartClaims);
                Req.EndClaimNo = int.Parse(StartClaims) + (int.Parse(NumOfBocklets) * 25) - 1;
                Req.RequestBy = ActiveUser;
                Req.RequestTime = DateTime.Now;
                db.StockRequests.Add(Req);
                var item = (from i in db.StockItems
                            where i.ItemName == ItemType
                            select i).SingleOrDefault();
                item.AvailableQuantity = item.AvailableQuantity + int.Parse(NumOfBocklets);
                item.LastAdd = DateTime.Now;
                db.SaveChanges();
                message = "Success";
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult StockView()
        {
            var items = (from i in db.StockItems
                         select i).ToList();
            ViewBag.Items = items;
            return View();
        }
        public ActionResult StockRequests(int page=1,int Pagesize=15)
        {
            var requests = (from r in db.StockRequests
                            select r).ToList();
            PagedList<StockRequest> Paging = new PagedList<StockRequest>(requests, page, Pagesize);
            return View(Paging);
        }
    }
}