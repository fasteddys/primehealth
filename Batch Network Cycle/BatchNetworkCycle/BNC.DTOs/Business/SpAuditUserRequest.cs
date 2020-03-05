using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
   public class SpAuditUserRequest
    {
        public int PendingClaimCount { get; set; }
        public int ApprovedClaimCount { get; set; }
        public int RejectClaimCount { get; set; }

        public int SpUserFk { get; set; }
        public int SpReqFk { get; set; }
        public int spAuditReqFk { get; set; }
        public Nullable<int> SpActionFK { get; set; }

        public string spUserComment { get; set; }

    }
}
