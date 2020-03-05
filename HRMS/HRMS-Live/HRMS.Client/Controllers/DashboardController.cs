using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRMS.Client.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: Dashboard
        [ClientAuthorization]
        public ActionResult Index()
        {
            return View();
        }
    }
}