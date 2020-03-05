using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineApprovals.DTOs
{
    class RequestInsertionDTO
    {
        public RequestDTO Request { get; set; }
        public List<RequestDetailsDTO> RequestDetails { get; set; }


        
    }
}
