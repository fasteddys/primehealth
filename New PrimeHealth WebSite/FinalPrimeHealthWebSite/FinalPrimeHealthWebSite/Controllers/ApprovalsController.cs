using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinalPrimeHealthWebSite.Controllers
{
    public class ApprovalsController : Controller
    {
         
        // GET: Approvals
        public ActionResult Login()
        {

            ViewBag.ServiceUrl = ConfigurationManager.AppSettings["ServiceUrl"]+ "api/data/";
            return View();
        }

        public ActionResult Index()
        {


            if(HttpContext.Request.Cookies["Name"]==null || HttpContext.Request.Cookies["Name"].ToString()==string.Empty)
            {
                return RedirectToAction("Login");


            }

            ViewBag.ServiceUrl = ConfigurationManager.AppSettings["ServiceUrl"] + "api/data/";

            return View();
        }

        public ActionResult AddRequest()
        {
            if (HttpContext.Request.Cookies["Name"] == null || HttpContext.Request.Cookies["Name"].ToString() == string.Empty)
            {
                return RedirectToAction("Login");


            }
            ViewBag.ServiceUrl = ConfigurationManager.AppSettings["ServiceUrl"] + "api/data/";

            return View();
        }

        public ActionResult ShowAllRequests()
        {
            if (HttpContext.Request.Cookies["Name"] == null || HttpContext.Request.Cookies["Name"].ToString() == string.Empty)
            {
                return RedirectToAction("Login");


            }
            ViewBag.ServiceUrl = ConfigurationManager.AppSettings["ServiceUrl"] + "api/data/";

            return View();

        }

        public ActionResult SearchRequest()

        {
            if (HttpContext.Request.Cookies["Name"] == null || HttpContext.Request.Cookies["Name"].ToString() == string.Empty)
            {
                return RedirectToAction("Login");


            }
            ViewBag.ServiceUrl = ConfigurationManager.AppSettings["ServiceUrl"] + "api/data/";

            return View();
        }

        public ActionResult RequestDetails(string Id)
        {
            if (HttpContext.Request.Cookies["Name"] == null || HttpContext.Request.Cookies["Name"].ToString() == string.Empty)
            {
                return RedirectToAction("Login");


            }
            ViewBag.ServiceUrl = ConfigurationManager.AppSettings["ServiceUrl"] + "api/data/";
            ViewBag.ImgesUrl = ConfigurationManager.AppSettings["ImgesURL"];
            ViewBag.Id = Id;

            return View();
        }
     

   

    }
}