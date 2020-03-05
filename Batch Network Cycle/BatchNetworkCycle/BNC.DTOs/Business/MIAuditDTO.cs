﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
   public class MIAuditDTO
    {
        public int MIAuditRequestID { get; set; }
        public string MIOfficerComment { get; set; }
        public int BatchAuditRequestFK { get; set; }
        public int BatchRequestFK { get; set; }
        public Nullable<int> MIOfficerFK { get; set; }
        public string OfficerName { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<System.DateTime> AssignedTime { get; set; }
        public Nullable<System.DateTime> CompilationDate { get; set; }
        public int AuditOrder { get; set; }
        public int TotalClaimsNumber { get; set; }



    }
}
