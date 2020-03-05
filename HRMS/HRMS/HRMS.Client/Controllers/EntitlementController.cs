using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRMS.Client.Controllers
{
    public class EntitlementController : BaseController
    {
        // GET: Entitlement
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [ClientAuthorization]
        public ActionResult ViewAllEntitlementChanges()
        {
            return View();
        }

        [ClientAuthorization]
        public ActionResult ViewAllEntitlement()
        {
            return View();
        }
    }
}