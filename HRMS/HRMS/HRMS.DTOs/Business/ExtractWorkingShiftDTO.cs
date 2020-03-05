using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class ExtractWorkingShiftDTO
    {
        public int ManagerID { get; set; }
        public string ShiftFrom { get; set; }
        public string ShiftTo { get; set; }
    }
}
