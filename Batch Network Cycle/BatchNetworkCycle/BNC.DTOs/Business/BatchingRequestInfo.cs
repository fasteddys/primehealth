using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    public  class BatchingRequestInfo
    {
        public BatchingRequestDTO BatchingRequest { get; set; }
        public LifeCycleBatchtRequestDTO LifeCycleBatchtRequest { get; set; }
    }
}
