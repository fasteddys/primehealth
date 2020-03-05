using EmaTours.BLL.Logic.Tables;
using EmaTours.DTOs.Business;
using EmaTours.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.BLL.Logic.Shared_Logic
{
    class SharedVisitTrips
    {
        DateTime creationDate;
        ClientTripRequestsBLL clientTripRequestsBLL;
        ClientVisitsBLL clientVisitsBLL;
        public SharedVisitTrips(EMAToursEntities Context, DateTime CreationDate)
        {
            creationDate = CreationDate;
            clientTripRequestsBLL = new ClientTripRequestsBLL(Context, CreationDate);
            clientVisitsBLL = new ClientVisitsBLL(Context, CreationDate);

        }
        public VisitsDTO GetVisitDetails(int VisitID)
        {
            ClientVisit Visit =   clientVisitsBLL.Find(x => x.ClientVisitID == VisitID).FirstOrDefault();
           List<ClientTripRequest> ListClientTrips=clientTripRequestsBLL.Find(x => x.ClientVisitFK == Visit.ClientVisitID).ToList();
            List<ClientTripDTO> Trips = new List<ClientTripDTO>();
            foreach (var item in ListClientTrips)
            {
                Trips.Add(new ClientTripDTO
                {  TripID=(int) item.TripFK,
                 ClientID= item.ClientFK,
                   ClientTripID= item.ClientTripRequestID


                });


            }

            return new VisitsDTO
            {
                ClientID = Visit.ClientVisitID,
                CountryID = Visit.OperatingCountryFK,
                EndVisitDate = Visit.VisitEndDate,
                HotelName = Visit.ClientHotelName,
                LocationID = Visit.OperatingLocationFK,
                StartVisitDate = Visit.VisitStartDate,
                VisitID = Visit.ClientVisitID,
                 ListTrips= Trips
            };


        }


        }
}
