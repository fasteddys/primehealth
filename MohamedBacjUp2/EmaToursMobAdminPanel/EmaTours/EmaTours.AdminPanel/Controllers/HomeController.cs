using EmaTours.AdminPanel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmaTours.AdminPanel.Controllers
{
    public class HomeController : BaseController
    {
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index()
        {
            ViewBag.Title = UnitOfWork.EMAUsersBLL.GetAll();
            return View();
        }

    }
}