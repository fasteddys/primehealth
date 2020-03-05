using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Web;
using System.Web.Mvc;

namespace HRMS.Client.Controllers
{
    public class ConfigurationController : BaseController
    {

        // GET: Configuration
        [ClientAuthorization]
        public ActionResult Index()
        {
            return View();
        }
        [ClientAuthorization]
        public ActionResult LinkUsersToDepartment()
        {
            return View();
        }
        [ClientAuthorization]
        public ActionResult LeaveType()
        {
            return View();
        }
        [ClientAuthorization]
        public ActionResult AddLeaveType()
        {
            return View();
        }
      //  [ClientAuthorization]
        public ActionResult WorkingWeekData()
        {
            return View();
        }
        [ClientAuthorization]
        public ActionResult EditLeaveType()
        {
            return View();
        }
        [ClientAuthorization]
        public ActionResult Employees()
        {
            return View();
        }
        [ClientAuthorization]
        public ActionResult EditEmployees(int ID)
        {
            //ViewBag.UserID = ID;
            return View();
        }
        [ClientAuthorization]
        public ActionResult AdjustEmployeesEntitlement()
        {
            return View();
        }
        [ClientAuthorization]
        public ActionResult Positions()
        {
            return View();
        }
        [ClientAuthorization]
        public ActionResult AddPositions()
        {
            return View();
        }
        [ClientAuthorization]
        public ActionResult CompanyHierarchy()
        {
            return View();
        }
        [ClientAuthorization]
        public ActionResult LinkUSersToSubDepartment()
        {
            return View();
        }
        [ClientAuthorization]

        public ActionResult Managers()
        {
            return View();
        }
        [ClientAuthorization]

        public ActionResult Permistions()
        {
            return View();
        }
    
        [ClientAuthorization]

        public ActionResult WorkingWeek()
        {
            return View();
        }
        [ClientAuthorization]

        public ActionResult ApprovalFlow()
        {
            return View();
        }
        [ClientAuthorization]

        public ActionResult AddApprovalFlow()
        {
            return View();
        }
        [ClientAuthorization]

        public ActionResult Import()
        {
           
            return View();
          
        }

        [ClientAuthorization]

        public ActionResult LinkToAC()
        {
            return View();
        }
        [ClientAuthorization]

        public PartialViewResult _LinkUserData(int ID)
        {
            ViewBag.ID = ID;
            return PartialView();
        }

        [ClientAuthorization]

        public PartialViewResult _AddAlternativeFlow()
        {
          
            return PartialView("_AddAlternativeFlow");
        }
        public ActionResult ViewWorkingWeek()
        {
            return View();
        }

        public ActionResult Entitlement()
        {
          
            return View();
        }

        /*Official Holidays */
        [ClientAuthorization]

        public ActionResult Holidays()
        {
            return View();
        }
        public ActionResult EditOfficialHolidays()
        {

            return View();
        }
        public ActionResult CreateOfficialHolidays()
        {

            return View();
        }
        /*------------------------------------*/
    }
}