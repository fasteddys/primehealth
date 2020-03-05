using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.DTOs.Business
{
    public class ClientTripDTO
    {

        public int? ClientTripID { get; set; }
        public int ClientTripRequestID { get; set; }

        
        public int? ClientID { get; set; }
        public int? UserAssigneeID { get; set; }
        public string TripName { get; set; }        
        public int? TripID { get; set; }
        public DateTime ClientDesiredTripDate { get; set; }
        public int? NumberOfAdults { get; set; }
        public int? NumberOfChildren { get; set; }
        public string ClientNotes { get; set; }
        public int? StatusID { get; set; }
        public int? ClientVisitID { get; set; }
        public bool? ReachedAgreement { get; set; }
        public int? LoggedUser { get; set; }
        public string UserComment { get; set; }



    }
}
