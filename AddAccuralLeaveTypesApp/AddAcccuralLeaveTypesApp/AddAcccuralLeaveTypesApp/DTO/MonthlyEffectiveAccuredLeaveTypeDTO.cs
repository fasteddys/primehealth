using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddAcccuralLeaveTypesApp.DTO
{
    class MonthlyEffectiveAccuredLeaveTypeDTO
    {
        public DateTime EffectiveDateFrom { get; set; }
        public DateTime EffectiveDateTo { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
