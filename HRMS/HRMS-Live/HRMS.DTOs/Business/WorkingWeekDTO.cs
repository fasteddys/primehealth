using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class WorkingWeekDTO
    {
        public int WorkingWeekID { get; set; }

        public string WorkingWeekName { get; set; }
        public List<WorkingWeekDetailsDTO> ListWorkingWeekDetails { get; set; }


    }
}
