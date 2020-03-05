using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs
{
   public class BatchingRequestDTO
    {
        public int BatchingRequestID { get; set; }
        public int NumberOfBatchClaims { get; set; }
        public int BatchSystemFK { get; set; }
        public string BatchSystemName { get; set; }
        public string BatchNumber { get; set; }
        public int BatchingOfficerFK { get; set; }
        public string OfficerName { get; set; }
        public string BatchingOfficerComment { get; set; }
        public int BatchingFilterationDetailsFK { get; set; }
        public Nullable<DateTime> TransferredToAuditDate { get; set; }
        public Nullable<int> FilterationCategoryFK { get; set; }
        public string FilterationCategoryName { get; set; }
        public DateTime CreationDate { get; set; }
        public int StatusFK { get; set; }



    }
}
