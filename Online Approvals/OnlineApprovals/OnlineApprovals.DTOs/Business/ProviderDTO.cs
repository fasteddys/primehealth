using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineApprovals.DTOs
{
    public class ProviderDTO
    {

        public string PharmAddressCode { get; set; }
        public string PharmAddress { get; set; }
        public int? LocID { get; set; }
        public string PharmSublocationName{ get; set; }
        public string PharmPhone { get; set; }
        public string PharmPhone1 { get; set; }
        public string PharmPhone2 { get; set; }
        public string PharmPhone3 { get; set; }
        public string PharmNotes { get; set; }
        public string PharmLong { get; set; }
        public string PharmLat { get; set; }
        public int? Discount { get; set; }
        public int ID { get; set; }
        public string AccountID { get; set; }
        public string Password { get; set; }
        public bool? IsDefaultPassowrd { get; set; }
        public bool? IsOnline { get; set; }
        public string PharmName { get; set; }

    }
}
