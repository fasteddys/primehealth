using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    public class ShowDataEntryBatchAssigningRequestDTO
    {
        public int RequestID { get; set; }
        public int AdministrationRequestID { get; set; }
        public int AssignedClaimsNumber { get; set; }
        public string OfficerName { get; set; }
        public string Action { get; set; }
    }
}
