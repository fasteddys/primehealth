using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.DTOs.Business
{
  public  class ClientPickUpDTO
    {
        public int PickUpRequestID { get; set; }
        public int? ClientID { get; set; }
        public string ClientName { get; set; }
        public int? StatusID { get; set; }

        public int? NumberOfMembers { get; set; }
        public DateTime? BackFlightTime { get; set; }
        public DateTime? DesiredPickupTime { get; set; }
        public DateTime? DeterminedPickupTime { get; set; }

        public string ClientNote { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? UserAssigneeID { get; set; }
        public string UserAssigneeName { get; set; }
        public string Status { get; set; }

        public string UserAssigneeComment { get; set; }


    }
}
