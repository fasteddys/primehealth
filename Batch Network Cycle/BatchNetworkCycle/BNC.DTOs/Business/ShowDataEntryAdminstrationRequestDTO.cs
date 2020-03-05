using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    public class ShowDataEntryAdminstrationRequestDTO
    {
        public int RequestID { get; set; }
        public int BatchRequestID { get; set; }
        public string DataEntryAdminAssignee { get; set; }
        public int? ApprovedClaimsNumber { get; set; }
        public string Action { get; set; }
    }
}
