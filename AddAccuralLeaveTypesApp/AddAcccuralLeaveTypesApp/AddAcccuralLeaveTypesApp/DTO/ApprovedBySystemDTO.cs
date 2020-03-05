using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddAcccuralLeaveTypesApp.DTO
{
    public class ApprovedBySystemDTO
    {
        public int RequestID { get; set; }
        public string RequestStatusName { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Exception { get; set; }
        public bool HasException { get; set; }
        public string AccessControlID { get; set; }
        

    }
}
