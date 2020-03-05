using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class RequestHandlerDTO
    {
       public int RequestHandlerID { get; set; }
        public decimal RequestHandlerDuration {get;set;}
        public DateTime Offday { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string DurationType { get; set; }



    }
}
