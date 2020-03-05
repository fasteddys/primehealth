using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
   public class LeaveTypeRestrictionDTO
    {
        public int LeaveTypeRestrictionTypeID { get; set; }
        public string RestrictionName { get; set; }
        public string ArName { get; set; }
        public string RestrictionDescription { get; set; }
        public string RestrictionDescriptionAr { get; set; }
    }
}
