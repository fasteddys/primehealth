using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineApprovals.DTOs
{
  public  class ALLRequestDataDTO
    {
        public  RequestDTO Request { get; set; }
        public List<RequestDetailsDTO> RequestDetails { get; set; }
        public List<AttachmentDTO>Attachments { get; set; }
        public List<DrugsDetailDTO> DemandedDrugs { get; set; }
    }
}
