using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    public class ShowClosedBatchReAdministrationRequestDTO
    {
        public int RequestID { get; set; }
        public int BatchClosingRequestID { get; set; }
        public string OfficerAssignee { get; set; }
        public string Action { get; set; }

    }
}
