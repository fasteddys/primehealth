using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddAcccuralLeaveTypesApp.DTO
{
    public class RequestDetailsDTO
    {
        public int RequestDetailsID { get; set; }
        public int RequestID { get; set; }
        public string DetailsCreator { get; set; }
        public string RequestDetailsComment { get; set; }
        public string Action { get; set; }
        public int RequestDetailsTypeID { get; set; }
        public string RequestDetailsTypeName { get; set; }
        public int? UserID { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
