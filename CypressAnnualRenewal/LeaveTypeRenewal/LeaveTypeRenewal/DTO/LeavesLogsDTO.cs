using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTypeRenewal.DTO
{
    public class LeavesLogsDTO
    {
        public DateTime Date { get; set; }
        public string Period { get; set; }
        public decimal PeriodValue { get; set; }

        public string LeaveTypeName { get; set; }
        public int LeaveTypeID { get; set; }

        public string Note { get; set; }


    }
}
