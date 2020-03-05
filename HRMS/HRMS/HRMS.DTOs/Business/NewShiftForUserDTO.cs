using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class NewShiftForUserDTO
    {
        public int UserID { get; set; }
        public int LoggedUserID { get; set; }
        public DateTime ShiftDate { get; set; }
        public string ShiftName { get; set; }

    }
}
