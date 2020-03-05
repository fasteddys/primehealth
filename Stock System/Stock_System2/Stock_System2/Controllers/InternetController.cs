using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stock_System2.Models;

namespace Stock_System2.Controllers
{
    public class InternetController : Controller
    {
        StockDB DB = new StockDB();
        Internet_Request Internet = new Internet_Request();
        public ActionResult Internet2()
        {
            return View();
        }
    }
}
