using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    public class BatchClosingRequestDTO
    {
        public int BatchClosingRequestID { get; set; }
        public int DataEntryAdminstrationRequestFK { get; set; }
        public Nullable<int> ClosingOfficerAssigneeFK { get; set; }
        public Nullable<DateTime> ClosingOfficerAssignedTime { get; set; }
        public string ConfirmReceivingComment { get; set; }
        public Nullable<DateTime> ConfirmReceivingTime { get; set; }
        public string FinishedReviewingComment { get; set; }
        public Nullable<DateTime> FinishedReviewingTime { get; set; }
        public Nullable<DateTime> ClosedOnSystemTime { get; set; }
        public int BatchClosingStatusFK { get; set; }
        public string BatchClosingStatusName { get; set; }
        public Nullable< DateTime> TransferredBackToAdminTime { get; set; }
        public string ClosingOfficerAssignedComment { get; set; }
        public string OfficerName { get; set; }
        public int TotalClaimsCount { get; set; }
        public string TransferredBackToAdminComment { get; set; }
    }
}
