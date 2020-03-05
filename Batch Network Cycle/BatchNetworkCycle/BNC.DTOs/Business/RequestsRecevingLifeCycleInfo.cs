using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    public class RequestsRecevingLifeCycleInfo
    {
        public RecievingRequestDTOS RecievingRequest{ get;set;}
        public List<filitrationRequestInfo> filterationRquestlistInfo { get; set; }

    }
}
