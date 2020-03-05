using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    public class ShowBatchClosingRequestDTO
    {
        public int RequestID { get; set; }
        public int AdminstrationRequestID { get; set; }
        public string ClosingOfficerAssignee { get; set; }
        public string Action { get; set; }
    }
}
