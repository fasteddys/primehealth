using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class WorkingShiftDTO
    {
        public int ShiftID { get; set; }
        public string ShiftName { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan ShiftEnd { get; set; }
        public int GraceIn { get; set; }
        public int GraceOut { get; set; }
        public int DepartmentID { get; set; }
        public int AddedByUser { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
