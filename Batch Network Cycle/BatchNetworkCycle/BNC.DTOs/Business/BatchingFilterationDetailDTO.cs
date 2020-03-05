using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    public class BatchingFilterationDetailDTO
    {
        public int BatchingFilterationDetailID { get; set; }
        public int RemainingClaimsCount { get; set; }
        public int TotalClaimsCount { get; set; }
        public int FilterationRequestID { get; set; }
        public int FilterationCategoryID { get; set; }
        public int ReceivingRequestIFK { get; set; }

        public Nullable<DateTime> CreationDate { get; set; }

        public string CategoryName { get; set; }

    }
}
