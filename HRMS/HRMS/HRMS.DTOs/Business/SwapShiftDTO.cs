using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class SwapShiftDTO
    {
        public int UserID { get; set; }
        public string ShiftDate { get; set; }
        public string OldShiftName { get; set; }
        public int NewShiftID { get; set; }
        public int LoggedUserID { get; set; }
    }
}
