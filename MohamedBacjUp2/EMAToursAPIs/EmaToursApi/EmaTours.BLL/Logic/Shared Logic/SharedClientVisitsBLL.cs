using EmaTours.BLL.Logic.Helpers;
using EmaTours.BLL.Logic.Tables;
using EmaTours.BLL.StaticData;
using EmaTours.DTOs.Business;
using EmaTours.Entities;
using EmaTours.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.BLL.Logic.Shared_Logic
{
 public   class SharedClientVisitsBLL
    {
        DateTime creationDate;
        ClientVisitsBLL clientVisitsBLL;
        ClientsBLL clientsBLL;
        OperatingCountriesBLL operatingCountriesBLL;
        OperatingLocationsBLL operatingLocationsBLL;
        ClientTripRequestsBLL clientTripRequestsBLL;
        TripsBLL tripsBLL;
        HotelsBLL hotelsBLL;

        public SharedClientVisitsBLL(EMAToursEntities Context, DateTime CreationDate)
        {
            creationDate = CreationDate;
            clientVisitsBLL = new ClientVisitsBLL(Context, CreationDate);
            clientsBLL = new ClientsBLL(Context, CreationDate);
            operatingCountriesBLL = new OperatingCountriesBLL(Context, CreationDate);
            operatingLocationsBLL = new OperatingLocationsBLL(Context, CreationDate);
            clientTripRequestsBLL = new ClientTripRequestsBLL(Context, CreationDate);
            tripsBLL = new TripsBLL(Context, CreationDate);
            hotelsBLL = new HotelsBLL(Context, CreationDate);
        }         
        public ClientVisitsDTO  GetAllClientVisits(int ClientID)
        {
            ClientVisitsDTO ClientVisitsDTO = new ClientVisitsDTO();
            ClientVisitsDTO.ClientID = ClientID;
            foreach(var item in clientVisitsBLL.Find(x => x.ClientFK == ClientID))
            {
                ClientVisitsDTO.ListVisits.Add(new VisitsDTO
                { CountryID= item.OperatingCountryFK,
                  StartVisitDate= item.VisitStartDate,
                  EndVisitDate = item.VisitEndDate,
                  VisitID= item.ClientVisitID
                   
                });
            }
            return ClientVisitsDTO;

        }
        public ClientVisit StartNewVisits(VisitsDTO Visit) {
            ClientVisit ClientVisit = new ClientVisit
            {
                ClientFK = Visit.ClientID,
                ClientHotelName = Visit.HotelName,
                CreationDate = creationDate,
                IsActive = true,
                OperatingCountryFK = Visit.CountryID,
                OperatingLocationFK = Visit.LocationID,
                VisitEndDate = Visit.EndVisitDate.Value.Date,
                VisitStartDate = Visit.StartVisitDate,
                HotelID= Visit.HotelID

            };
            clientVisitsBLL.Add(ClientVisit);
            return ClientVisit;

        }
        public void EndVisit(VisitsDTO VisitID)
        {
            ClientVisit Visit=clientVisitsBLL.Find(x => x.ClientVisitID == VisitID.VisitID).FirstOrDefault();
            Visit.IsActive = false;
            Visit.VisitEndDate = creationDate;
            Visit.ClientOverallVisitComment = VisitID.ClientOverallVisitComment;
            clientVisitsBLL.Edit(Visit);
        }
        public void EditVisit(VisitsDTO VisitID)
        {
            ClientVisit Visit = clientVisitsBLL.Find(x => x.ClientVisitID == VisitID.VisitID).FirstOrDefault();
            var oldVisit = XMLObjectConverter.Serialize(Visit);

            Visit.VisitStartDate = VisitID.StartVisitDate;
            Visit.VisitEndDate = VisitID.EndVisitDate;
            Visit.HotelID = VisitID.HotelID;
            VisitID.HotelName = hotelsBLL.Find(x => x.HotelID == Visit.HotelID).FirstOrDefault().Name;
            //Visit.ClientHotelName = hotelsBLL.Find(x => x.HotelID == Visit.HotelID).FirstOrDefault().Name;

            clientVisitsBLL.Edit(Visit);
            Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.EditClientData, (int)VisitID.LoggedUser, oldVisit, XMLObjectConverter.Serialize(Visit), "Edit Visit Data");
        }
        public VisitsDTO GetClientVisitDetails(int ClientVisitID)
        {
            ClientVisit ClientVisit = clientVisitsBLL. Find(x => x.ClientVisitID == ClientVisitID).FirstOrDefault();
            string CountryName = operatingCountriesBLL.Find(x => x.CountryID == ClientVisit.OperatingCountryFK).FirstOrDefault()?.ConutryName;
            string LocationName = operatingLocationsBLL.Find(x => x.LocationID == ClientVisit.OperatingLocationFK).FirstOrDefault()?.LocationName;
            VisitsDTO VisitsDTO = new VisitsDTO
            {
                ClientID = (int)ClientVisit.ClientFK,
                LocationName = LocationName,
                CountryName = CountryName,
                CountryID = ClientVisit.OperatingCountryFK,
                LocationID = ClientVisit.OperatingLocationFK,
                EndVisitDate = ClientVisit.VisitEndDate,
                StartVisitDate = ClientVisit.VisitStartDate,
                VisitID = ClientVisit.ClientVisitID,
                ClientName = clientsBLL.Find(x => x.ClientID == ClientVisit.ClientFK).FirstOrDefault()?.ClientName
                , HotelName = ClientVisit.ClientHotelName

            };
            VisitsDTO.ListTrips = new List<ClientTripDTO>();
            VisitsDTO.ListTrips = GetAllTripRequest(ClientVisitID);

            return VisitsDTO;
        }

        public List<ClientTripDTO> GetAllTripRequest(int ClientVisitID)
        {
            List<ClientTripDTO> ListTrips = new List<ClientTripDTO>();
            foreach (var item in clientTripRequestsBLL.Find(x => x.ClientVisitFK == ClientVisitID))
            {
                ListTrips.Add(new ClientTripDTO
                {
                    ClientDesiredTripDate = (DateTime)item.ClientDesiredTripDate,
                    ClientID = item.ClientFK,
                    ClientNotes = item.ClientNotes,
                    ClientTripID = item.TripFK,
                    ClientVisitID = item.ClientVisitFK,
                    ClientTripRequestID = item.ClientTripRequestID,
                    NumberOfAdults = item.NumberOfAdults,
                    NumberOfChildren = item.NumberOfChildren,
                    ReachedAgreement = item.ReachedAgreement,
                    StatusID = item.StatusFK,
                    TripID = item.TripFK,
                    UserAssigneeID = item.AssignedAgentFK,
                    TripName = tripsBLL.Find(x => x.TripID == item.TripFK).FirstOrDefault()?.TripName
                });


            }
            return ListTrips;
        }


    }
}
