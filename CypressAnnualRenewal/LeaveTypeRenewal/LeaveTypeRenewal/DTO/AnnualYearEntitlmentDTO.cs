using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTypeRenewal.DTO
{
  public class AnnualYearEntitlmentDTO
    {
        public StartEntitlmentDTO StartEntitlment { get; set; }
        public EndEntitlmentDTO EndEntitlment { get; set; }
        public List<LeavesLogsDTO> LeavesLogs { get; set; }
        public UserDTO User { get; set; }
        public string ReportYear { get; set;}
    }
}
