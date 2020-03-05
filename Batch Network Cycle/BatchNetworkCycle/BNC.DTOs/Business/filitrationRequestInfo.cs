using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    public class filitrationRequestInfo
    {
        public BatchingFilterationDetailDTO BatchingFilterationDetails { get;set;}
        public List<BatchingRequestInfo> BatchingRequestInfoList { get; set; }
    }
}
