using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
  public  class LeaveTypeDTO
    {
        public int LeaveTypeID { get; set; }
        public string LeaveTypeName { get; set; }
        public decimal? EmployeeEarnsNumberOfUnits { get; set; }
        public decimal? OverSeniorityEmployeeEarns { get; set; }
        public string Unit { get; set; }



    }
}
