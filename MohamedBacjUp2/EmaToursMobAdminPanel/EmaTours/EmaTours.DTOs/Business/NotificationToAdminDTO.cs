using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.DTOs.Business
{
   public class NotificationToAdminDTO
    {
        public int? RequestID { get; set; }
        public int? ClientID { get; set; }
        public int? UserAssigneeID { get; set; }
        public string UserAssigneeName { get; set; }
        public string Type { get; set; }

        public int? TripID { get; set; }
        public DateTime? ClientDesiredTripDate { get; set; }
        public string CreationDate { get; set; }

        public int? NumberOfAdults { get; set; }
        public int? NumberOfChildren { get; set; }
        public string ClientNotes { get; set; }
        public int? StatusID { get; set; }
        public int? ClientVisitID { get; set; }
        public bool ReachedAgreement { get; set; }
        public string Status { get; set; }
        public string ClientName { get; set; }
        public string TripName { get; set; }


        public string UserComment { get; set; }



    }
}
