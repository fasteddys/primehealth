using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineApprovals.DTOs
{
    public class RequestDetailsDTO
    {
        public int ID { get; set; }
        public int RequestID { get; set; }
        public int RequestDetailsType { get; set; }
        public string Notes { get; set; }
        public DateTime DetailsDate { get; set; }
        public bool Delivered { get; set; }
        public bool Seen { get; set; }
        public bool IsDeleted { get; set; }
        public int? UserID { get; set; }
        public int UserTypeID { get; set; }


    }
}
