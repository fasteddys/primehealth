using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CallCenterSystemReports.Controllers
{
    public class TestController : BaseController
    {
        // GET: Test
        public ActionResult Index()
        {
            var x = UnitOfWork.EmailApprovalRequestsBLL.GetAll().ToList();
            return View();
        }
    }
}