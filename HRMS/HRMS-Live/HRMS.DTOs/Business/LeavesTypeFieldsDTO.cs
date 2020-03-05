using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
   public class LeavesTypeFieldsDTO
    {
    public   List< LeaveTypeFieldDTO > LeaveTypeField { get; set; }
        public bool? HasAttachemnt { get; set; }
        public bool? AttachemntIsRequired { get; set; }




    }
}
