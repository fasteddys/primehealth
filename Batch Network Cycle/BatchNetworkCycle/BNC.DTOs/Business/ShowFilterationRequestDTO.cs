using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    public class ShowFilterationRequestDTO
    {
        public int RequestID { get; set; }
        public int ClaimsCount { get; set; }
        public string Category { get; set; }
        public string Action { get; set; }
    }
}
