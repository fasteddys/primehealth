using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
  public  class RequestDetailsView
    {
        public string UserName { get; set; }
        public string LeaveTypeName { get; set; }
        public DateTime DurationFrom { get; set; }
        public DateTime DurationTo { get; set; }
        public DateTime BackToWork { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal RequestDuration { get; set; }
        public string SubDepartmentName { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public string DurationUnit { get; set; }
        public string Comment { get; set; }
        public string RequestStatus { get; set; }
        public string Reason { get; set; }
        public string SubstituteName { get; set; }
        public decimal? EntitlementTotal { get; set; }
        public int Order { get; set; }



    }
}
