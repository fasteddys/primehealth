using EmaTours.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmaTours.Entities;
using EmaTours.DTOs.Business;
using static EmaTours.BLL.StaticData.StaticEnums;
using EmaTours.Utilities;
using EmaTours.BLL.Logic.Helpers;
using EmaTours.BLL.StaticData;

namespace EmaTours.BLL.Logic.Tables
{
    public class ClientTripRequestsBLL : Repository<ClientTripRequest>
    {
        DateTime creationDate;
        TripsBLL tripsBLL;
        public ClientTripRequestsBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
            tripsBLL = new TripsBLL(Context, CreationDate);
        }
        public void UserAssignTripRequest(ClientTripDTO ClientTrip)
        {
            ClientTripRequest TripRequest = Find(x => x.ClientTripRequestID == ClientTrip.ClientTripID).FirstOrDefault();
            TripRequest.AssignedAgentFK = ClientTrip.UserAssigneeID;
            TripRequest.AssignTime = creationDate;
            TripRequest.StatusFK = (int)RequestStatus.Pending;
            Edit(TripRequest);

        }
        public void UserCloseTripRequest(ClientTripDTO ClientTrip)
        {
            ClientTripRequest TripRequest = Find(x => x.ClientTripRequestID == ClientTrip.ClientTripID).FirstOrDefault();
            TripRequest.CloseTime = creationDate;
            TripRequest.StatusFK = (int)RequestStatus.Closed;
            Edit(TripRequest);


        }
        public void EditTripRequest(ClientTripDTO ClientTrip)
        {
            ClientTripRequest TripRequest = Find(x => x.ClientTripRequestID == ClientTrip.ClientTripRequestID).FirstOrDefault();
            var oldClientTripRequest = XMLObjectConverter.Serialize(TripRequest);

            TripRequest.ClientDesiredTripDate = ClientTrip.ClientDesiredTripDate;
            TripRequest.NumberOfAdults = ClientTrip.NumberOfAdults;
            TripRequest.NumberOfChildren = ClientTrip.NumberOfChildren;
            TripRequest.ClientNotes = ClientTrip.ClientNotes;

            Edit(TripRequest);
            Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.EditTripData, (int)ClientTrip.LoggedUser, oldClientTripRequest, XMLObjectConverter.Serialize(TripRequest), "Edit Trip Data");
        }
        public void UserContactTripRequester(ClientTripDTO ClientTrip)
        {
            ClientTripRequest TripRequest = Find(x => x.ClientTripRequestID == ClientTrip.ClientTripID).FirstOrDefault();
            TripRequest.ReachedAgreement = ClientTrip.ReachedAgreement;
            TripRequest.AssignedUserComment = ClientTrip.UserComment;
            TripRequest.AssignedUserActionTime = creationDate;
            TripRequest.AssignedUserActionFK = (int)UserAction.ContactClientForTrip;
            Edit(TripRequest);

        }
        public List<ClientTripDTO> GetAllTripRequest()
        {
            List<ClientTripDTO> ListTrips = new List<ClientTripDTO>();
            foreach (var item in GetAll())
            {
                ListTrips.Add(new ClientTripDTO
                {
                    ClientDesiredTripDate = (DateTime)item.ClientDesiredTripDate,
                    ClientID = item.ClientFK,
                    ClientNotes = item.ClientNotes,
                    ClientTripID = item.ClientTripRequestID,
                    ClientVisitID = item.ClientVisitFK,
                    NumberOfAdults = item.NumberOfAdults,
                    NumberOfChildren = item.NumberOfChildren,
                    ReachedAgreement = item.ReachedAgreement,
                    StatusID = item.StatusFK,
                    TripID = item.TripFK,
                    UserAssigneeID = item.AssignedAgentFK
                });


            }
            return ListTrips;
        }

    }

}
