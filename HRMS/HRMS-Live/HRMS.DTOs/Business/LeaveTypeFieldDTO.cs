using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
  public  class LeaveTypeFieldDTO
    {
       public int? LeaveTypeFieldID { get; set; }
        public string LeaveTypeFieldName { get; set; }
        public string LeaveTypeFieldVisibilityName { get; set; }

      public int? LeaveTypeFieldVisibilityID { get; set; }

    }
}
