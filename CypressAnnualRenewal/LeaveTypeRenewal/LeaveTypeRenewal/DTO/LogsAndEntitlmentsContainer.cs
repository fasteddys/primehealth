using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTypeRenewal.DTO
{
public    class LogsAndEntitlmentsContainer
    {
        public EndEntitlmentDTO EndEntitlment { get; set; }
        public StartEntitlmentDTO StartEntitlmentDTO { get; set; }
        public List<LeavesLogsDTO> LeavesLogs { get; set; }
    }
}
