using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRMS.Client.Controllers
{
    public class HomeController : BaseController
    {
        [ClientAuthorization]
        public ActionResult Index()
        {
           
            return View();
        }
    }
}