using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class ListWorkingDayDetailsDTO
    {
        public List<WorkingDayDetailsDTO> WorkingDayDetailsDTO { get; set; }
        public bool IsOffDay { get; set; }
        public string WorkingMode { get; set; }



    }
}
