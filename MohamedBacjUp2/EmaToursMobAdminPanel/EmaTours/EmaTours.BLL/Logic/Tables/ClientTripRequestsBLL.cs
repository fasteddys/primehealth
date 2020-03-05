using EmaTours.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmaTours.Entities;
using EmaTours.DTOs.Business;
using static EmaTours.BLL.StaticData.StaticEnums;

namespace EmaTours.BLL.Logic.Tables
{
    public class ClientTripRequestsBLL : Repository<ClientTripRequest>
    {
        DateTime creationDate;
        ClientsBLL clientsBLL;
        EMAUsersBLL usersBLL;
        TripsBLL tripsBLL;
        public ClientTripRequestsBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
            clientsBLL = new ClientsBLL(Context, CreationDate);
            usersBLL = new EMAUsersBLL(Context, CreationDate);
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
        public void UserContactTripRequester(ClientTripDTO ClientTrip)
        {
            ClientTripRequest TripRequest = Find(x => x.ClientTripRequestID == ClientTrip.ClientTripID).FirstOrDefault();
            TripRequest.ReachedAgreement = ClientTrip.ReachedAgreement;
            TripRequest.AssignedUserComment = ClientTrip.UserComment;
            TripRequest.AssignedUserActionTime = creationDate;
            TripRequest.AssignedUserActionFK = (int)UserAction.AddTripDetails;
            ClientTrip.ClientVisitID = TripRequest.ClientVisitFK;
            Edit(TripRequest);

        }

        public List<ClientTripDTO> GetAllTripRequest() {
            List<ClientTripDTO> ListTrips = new List<ClientTripDTO>();
          foreach (var item in   GetAll())
            {
                ListTrips.Add(new ClientTripDTO
                {
                    ClientDesiredTripDate = (DateTime)item.ClientDesiredTripDate,
                    ClientID = item?.ClientFK,
                    ClientNotes = item.ClientNotes,
                   ClientTripID = item.ClientTripRequestID,
                    ClientVisitID =item.ClientVisitFK,
                    NumberOfAdults = item.NumberOfAdults,
                    NumberOfChildren = item.NumberOfChildren,
                    ReachedAgreement = item.ReachedAgreement == null ? false :(bool) item.ReachedAgreement,
                    StatusID =item.StatusFK,
                    TripID = item?.TripFK,
                    UserAssigneeID = item.AssignedAgentFK,
                    // Status= ((RequestStatus)Enum.Parse(typeof(RequestStatus), item.StatusFK.ToString())).ToString()
                });


            }
            return ListTrips;
        }

        public List<ClientTripDTO> GetTripRequest(int RequestStatusID)
        {
            List<ClientTripDTO> ListTrips = new List<ClientTripDTO>();
            foreach (var item in Find(x=>x.StatusFK== RequestStatusID))
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
                    ReachedAgreement = item.ReachedAgreement == null ? false : (bool)item.ReachedAgreement,
                    StatusID =item.StatusFK,
                    TripID =item.TripFK,
                    UserAssigneeID = item.AssignedAgentFK
                });


            }
            return ListTrips;
        }

        public List<ClientTripDTO> GetAssignedTripRequest(int UserID)
        {

            List<ClientTripDTO> ListTrips = new List<ClientTripDTO>();
            foreach (var item in Find(x => x.AssigneeUserFK == UserID))
            {
                ListTrips.Add(new ClientTripDTO
                {
                    ClientDesiredTripDate = (DateTime)item.ClientDesiredTripDate,
                    ClientID = item.ClientFK,
                    ClientNotes = item.ClientNotes,
                    ClientTripID = item.ClientTripRequestID,
                    ClientVisitID = (int)item.ClientVisitFK,
                    NumberOfAdults = (int)item.NumberOfAdults,
                    NumberOfChildren = (int)item.NumberOfChildren,
                    ReachedAgreement = item.ReachedAgreement == null ? false : (bool)item.ReachedAgreement,
                    StatusID = (int)item.StatusFK,
                    TripID = (int)item.TripFK,
                    UserAssigneeID = item.AssignedAgentFK
                });


            }
            return ListTrips;
        }


        public ClientTripDTO ManageTripRequest(int TripRequestID)
        {
          ClientTripRequest ClientTrip=   Find(x => x.ClientTripRequestID == TripRequestID).FirstOrDefault();


            ClientTripDTO clienttripdto = new ClientTripDTO {
                ClientDesiredTripDate = ClientTrip.ClientDesiredTripDate,
                ClientID = ClientTrip.ClientFK,
                ClientNotes = ClientTrip.ClientNotes,
                ClientTripID = ClientTrip.ClientTripRequestID,
                ClientVisitID = ClientTrip.ClientVisitFK,
                NumberOfAdults = ClientTrip.NumberOfAdults,
                NumberOfChildren = ClientTrip.NumberOfChildren,
                StatusID = ClientTrip.StatusFK,
                TripID = ClientTrip.TripFK,
                ReachedAgreement = ClientTrip.ReachedAgreement == null ? false : (bool)ClientTrip.ReachedAgreement,
                UserAssigneeID = ClientTrip.AssigneeUserFK,
                UserComment = ClientTrip.AssignedUserComment,
                ClientName = clientsBLL.Find(x => x.ClientID == ClientTrip.ClientFK).FirstOrDefault()?.ClientName,
                Status = ((RequestStatus)Enum.Parse(typeof(RequestStatus), ClientTrip.StatusFK.ToString())).ToString(),
                UserAssigneeName = usersBLL.Find(x => x.UserID == ClientTrip.AssignedAgentFK).FirstOrDefault()?.UserName,
                 TripName= tripsBLL.Find(x=>x.TripID== ClientTrip.TripFK).FirstOrDefault()?.TripName

            };

            return clienttripdto;
        }
    }

}
