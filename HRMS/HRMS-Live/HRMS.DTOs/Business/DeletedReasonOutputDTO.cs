using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class DeletedReasonOutputDTO
    {
        public int RequestID { get; set; }//1
        public string UserName { get; set; }//2
        public Nullable<DateTime> RequestCreateDate { get; set; }//3
        public string RequestStartDate { get; set; }//4
        public string RequestEndDate { get; set; }//5
        public string LeaveTypeName { get; set; }//6
        public string ManagerName { get; set; }//7
        public string DeletedReasonComment { get; set; }//8
        public string RequestDurationUnitName { get; set; }//9
        public decimal RequestDuration { get; set; }//10
        public string PendingFrom { get; set; }
        public int? AccessControlID { get; set; }


    }
}
