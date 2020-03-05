using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    public class DataEntryBatchAssigningRequestDTO
    {
        public int DataEntryBatchAssigningRequestID { get; set; }
        public int DataEntryAdministrationRequestFK { get; set; }
        public int? DataEntryBatchAssigningStatusFK { get; set; }
        public int DataEntryOfficerFK { get; set; }
        public string DataEntryOfficerName { get; set; }
        public string DataEntryOfficerReceivingComment { get; set; }
        public DateTime? ConfirmRecievingByOfficerTime { get; set; }
        public DateTime? DataEntryOfficerFinishedOnSystemTime { get; set; }
        public string DataEntryOfficerFinishedComment { get; set; }
        public int AssignedClaimsNumber { get; set; }
        public string DataEntryAdminComment { get; set; }
        public string DataEntryBatchAssigningStatusName { get; set; }
        public Nullable<System.DateTime> AssignedByDataEntryAssigningTime { get; set; }
        public string TransferedToComment { get; set; }

    }
}
