using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    public class SearchCriteriaDTO
    {
        public int TableID { get; set; }
        public string FieldsNames { get; set; }
        public int UserID { get; set; }
        public int StatusID { get; set; }
        public int RecordID { get; set; }
        public bool IsLocked { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
