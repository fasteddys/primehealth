using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CallCenterSystemReports.BLL;
using static CallCenterSystemReports.BLL.StaticData.StaticEnums;

namespace CallCenterSystemReports.Controllers

{
    public class SecondDashboardController : BaseController
    {
        // GET: SecondDashboard
        public ActionResult Index()
        {
            DateTime StartOfWeek = UnitOfWork.EmailApprovalRequestsBLL.StartOfWeek(DateTime.Now);
            var AllOperatorsTicketsCount = UnitOfWork.EmailApprovalRequestsBLL.GetAll().Where(p => p.OperatorAssignee != null&& p.CreationDate > StartOfWeek).Where(p => p.RequstStatusID != (int)RequestStatus.AssignedByOpeartor).Count();
            ViewBag.TotaloperatorsTickets = AllOperatorsTicketsCount;

            var AllDoctorsTicketsCount = UnitOfWork.EmailApprovalRequestsBLL.GetAll().Where(p => p.DoctorAssignee != null && p.CreationDate > StartOfWeek).Where(p => p.RequstStatusID != (int)RequestStatus.AssignedByDoctor).Count();
            ViewBag.TotalDoctorsTickets = AllDoctorsTicketsCount;

            var AllAuditDoctorsTicketsCount = UnitOfWork.EmailApprovalRequestsBLL.GetAll().Where(p => p.AuditAssignee != null && p.CreationDate > StartOfWeek).Where(p => p.RequstStatusID != (int)RequestStatus.AssignedByAudit).Count();
            ViewBag.TotalAuditDoctorsTickets = AllAuditDoctorsTicketsCount;

            return View();
        }
        public JsonResult GetTopFiveOperators()
        {

            var TopOperators = UnitOfWork.EmailApprovalRequestsBLL.GetTopAssignees("CallCenterUser", 5);
            return Json(TopOperators, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTopFiveDoctors()
        {
            var TopDoctors = UnitOfWork.EmailApprovalRequestsBLL.GetTopAssignees("CallCenterDoctor", 5);
            return Json(TopDoctors, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTopFiveAuditDoctors()
        {
            var TopAuditDoctors = UnitOfWork.EmailApprovalRequestsBLL.GetTopAssignees("CallCenterAuditDoctor", 5);
            return Json(TopAuditDoctors, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLeastFiveOperators()
        {
            var LeastOperators = UnitOfWork.EmailApprovalRequestsBLL.GetLeastAssignees("CallCenterUser", 5);
            return Json(LeastOperators, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLeastFiveDoctors()
        {
            var LeastDoctors = UnitOfWork.EmailApprovalRequestsBLL.GetLeastAssignees("CallCenterDoctor", 5);
            return Json(LeastDoctors, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLeastFiveAuditDoctors()
        {
            var LeastAuditDoctor = UnitOfWork.EmailApprovalRequestsBLL.GetLeastAssignees("CallCenterAuditDoctor", 5);
            return Json(LeastAuditDoctor, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTicketsAverageTimeBySatsus()
        {
            var List = UnitOfWork.StoredFunctions.GetTicketsAverageTimeByStatus();
            return Json(List, JsonRequestBehavior.AllowGet);
        }
    }
}