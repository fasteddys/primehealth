using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CallCenterSystemReports.BLL;
using static CallCenterSystemReports.BLL.StaticData.StaticEnums;


namespace CallCenterSystemReports.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
           


            return View();
        }

       

        public JsonResult TicketscountGroupedbyWarnningType()
        {
            var ListOfTicketsCountByWarningType = UnitOfWork.EmailApprovalRequestsBLL.GetTicketscountGroupedbyWarnningType();
            return Json(ListOfTicketsCountByWarningType, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TicketscountGroupedbyWarnningTypeForOperators()
        {
            var ListOfTicketsCountByWarningTypeForOperators = UnitOfWork.EmailApprovalRequestsBLL.GetTicketscountGroupedbyWarnningTypeForOPerators();
            return Json(ListOfTicketsCountByWarningTypeForOperators, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RequestCountOverDay(string Date)
        {
            if (Date == ""|| Date==null)
            {
                var List = UnitOfWork.EmailApprovalRequestsBLL.GetRequestsCountOverDay();
                return Json(List, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var List = UnitOfWork.EmailApprovalRequestsBLL.GetRequestsCountOverDayByDate(Convert.ToDateTime(Date));
                return Json(List, JsonRequestBehavior.AllowGet);
            }
            
        }

        public JsonResult RequestAvarageTime()
        {
            var test = UnitOfWork.StoredFunctions.GetTicketsAverageTimeByType();
            //var List = UnitOfWork.EmailApprovalRequestsBLL.GetTicketsAvrageTime();
            return Json(test, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _RequestsRateOverDay()
        {
            return PartialView();
        }
        
    }
}