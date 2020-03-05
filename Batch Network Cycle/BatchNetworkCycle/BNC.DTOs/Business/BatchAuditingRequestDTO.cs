using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    public class BatchAuditingRequestDTO
    {
        public int BatchAuditingRequestID { get; set; }

        public int BatchingRequestFK { get; set; }
        public int BatchAuditingStatusFK { get; set; }
        public Nullable<int> FilterationCategoryFK { get; set; }
        public Nullable<int> NumberOfAuditingBatchClaims { get; set; }
        public Nullable<int> TotalNumberOfApprovedClaims { get; set; }
        public Nullable<int> TotalNumberOfRejectedClaims { get; set; }
        public DateTime CreationDate { get; set; }
        public Nullable<DateTime> TransferToDataEntryOn { get; set; }

    }
}
