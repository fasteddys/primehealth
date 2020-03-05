using OnlineApprovals.DTOs.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static OnlineApprovals.BLL.StaticData.StaticEnums;

namespace OnlineApprovals.Client.Controllers
{
    public class HomeController : BaseController
    {

        [ClientAuthorization]

        public ActionResult Index()
        {

            DashboardDTO Dash = new DashboardDTO()
            {
                TotalTickets = UnitOfWork.OnlineApprovalRequestBLL.CountOfALLRequestByProvideID(),
                ApprovedTickets = UnitOfWork.OnlineApprovalRequestBLL.CountOfALLRequestByProvideIDAndStatus( (int)RequestStatus.Approved),
                ClaimsPerCurrentMonth = UnitOfWork.OnlineApprovalRequestBLL.CountClaimsPerCurrentMonth(),
                PendingApprovalTickets = UnitOfWork.OnlineApprovalRequestBLL.CountOfALLRequestByProvideIDAndStatus( (int)RequestStatus.PendingCallCenterAction),
                RejectedTickets = UnitOfWork.OnlineApprovalRequestBLL.CountOfALLRequestByProvideIDAndStatus( (int)RequestStatus.Rejected),
                TotalMembers = UnitOfWork.OnlineApprovalRequestBLL.CountOfTotal_Members()

            };



            return View(Dash);
        }
        [ClientAuthorization]

        public JsonResult GetCountofRequest()
        {
            DashboardDTO Dash = new DashboardDTO()
            {
                TotalTickets = UnitOfWork.OnlineApprovalRequestBLL.CountOfALLRequestByProvideID(),
                ApprovedTickets = UnitOfWork.OnlineApprovalRequestBLL.CountOfALLRequestByProvideIDAndStatus( (int)RequestStatus.Approved),
                ClaimsPerCurrentMonth = UnitOfWork.OnlineApprovalRequestBLL.CountClaimsPerCurrentMonth(),
                PendingApprovalTickets = UnitOfWork.OnlineApprovalRequestBLL.CountOfALLRequestByProvideIDAndStatus( (int)RequestStatus.PendingCallCenterAction),
                RejectedTickets = UnitOfWork.OnlineApprovalRequestBLL.CountOfALLRequestByProvideIDAndStatus( (int)RequestStatus.Rejected),
                TotalMembers = UnitOfWork.OnlineApprovalRequestBLL.CountOfTotal_Members(),
                TerminateTickets = UnitOfWork.OnlineApprovalRequestBLL.CountOfALLRequestByProvideIDAndStatus( (int)RequestStatus.Terminated),
                AssignedTickets = UnitOfWork.OnlineApprovalRequestBLL.CountOfALLRequestByProvideIDAndStatus( (int)RequestStatus.AssignedByCallCenter),

                PendingProvidersTickets = UnitOfWork.OnlineApprovalRequestBLL.CountOfALLRequestByProvideIDAndStatus( (int)RequestStatus.PendingProviderAction),
                ReopendTickets = UnitOfWork.OnlineApprovalRequestBLL.CountOfALLRequestByProvideIDAndStatus( (int)RequestStatus.Reopend),
                NewTickets = UnitOfWork.OnlineApprovalRequestBLL.CountOfALLRequestByProvideIDAndStatus( (int)RequestStatus.New),
                UserName = UnitOfWork.OnlineApprovalsProviderBLL.GetById(ProviderID).PharmName,
                Location = UnitOfWork.OnlineApprovalsProviderBLL.GetById(ProviderID).PharmSublocationName
            };
            return Json(Dash);
        }
        [ClientAuthorization]


        public int? TotalRequestNumber(int ProviderID)
        {
            return UnitOfWork.OnlineApprovalRequestBLL.CountOfALLRequestByProvideID();



        }
        [ClientAuthorization]

        public int? TotalRequestProvideIDAndStatus(int ProviderID, int StatusID)
        {
            return UnitOfWork.OnlineApprovalRequestBLL.CountOfALLRequestByProvideIDAndStatus( StatusID);



        }
        [ClientAuthorization]

        public int? TotalRequestProvideIDAndStatus(int ProviderID)
        {
            return UnitOfWork.OnlineApprovalRequestBLL.CountOfTotal_Members();



        }
        [ClientAuthorization]


        public int? CountOfTotal_Members(int ProviderID)
        {
            return UnitOfWork.OnlineApprovalRequestBLL.CountOfTotal_Members();



        }
        [ClientAuthorization]

        public int? CountClaimsPerCurrentMonth(int ProviderID)
        {
            return UnitOfWork.OnlineApprovalRequestBLL.CountClaimsPerCurrentMonth();



        }
        [ClientAuthorization]


        [HttpPost]
        public void Test(int ID)
        {
            // return View();
        }



    }
}