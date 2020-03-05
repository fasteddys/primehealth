using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
  public  class CategoryClaimsCountDTO
    {
        public int ApprovedClaims { get; set; }
        public int RejectedClaims { get; set; }
        public int PendingClaims { get; set; }
        public int BatchingID { get; set; }
        public int BatchAuditRequestID { get; set; }

        

    }
}
