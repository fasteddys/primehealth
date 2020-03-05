using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class ServicesDTO
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceStatus { get; set; }
        public int? ServiceStatusID { get; set; }

    }
}
