using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddAcccuralLeaveTypesApp.DTO
{
    public class RequestEntitlmentDTO
    {
        public int UserID { get; set; }
        public int? ModifiedByUserId { get; set; }
        public int? LeaveTypeID { get; set; }
        public string LeaveTypeName { get; set; }
        public int UnitID { get; set; }
        public int? RequestID { get; set; }
        public decimal RequestDuration { get; set; }
        public decimal TotalOffDays { get; set; }
        public DateTime DurationFrom { get; set; }
        public DateTime DurationTo { get; set; }
        public DateTime RequestBackToworkDate { get; set; }
    }
}
