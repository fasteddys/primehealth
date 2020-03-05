using EmaTours.BLL.Logic.Tables;
using EmaTours.DTOs.Business;
using EmaTours.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmaTours.BLL.StaticData.StaticEnums;

namespace EmaTours.BLL.Logic.Shared_Logic
{
 public   class SharedClientTripsBLL
    {
        DateTime creationDate;
        ClientTripRequestsBLL ClientTripRequestsBLL;
        TripsBLL tripsBLL;
        public SharedClientTripsBLL(EMAToursEntities Context, DateTime CreationDate)
        {
            creationDate = CreationDate;
            ClientTripRequestsBLL = new ClientTripRequestsBLL(Context, CreationDate);
            tripsBLL = new TripsBLL(Context, CreationDate);
        }
        public ClientTripRequest StartNewTrip(ClientTripDTO ClientTripDTO)
        {
            ClientTripDTO.TripName= tripsBLL.Find(x => x.TripID == ClientTripDTO.TripID).FirstOrDefault().TripName;

            ClientTripRequest ClientTripRequest = new ClientTripRequest
            {
                ClientFK = ClientTripDTO.ClientID,
                ClientDesiredTripDate = ClientTripDTO.ClientDesiredTripDate,
                ClientNotes = ClientTripDTO.ClientNotes,
                ClientVisitFK = ClientTripDTO.ClientVisitID,
                NumberOfAdults = ClientTripDTO.NumberOfAdults,
                NumberOfChildren = ClientTripDTO.NumberOfChildren,
                RequestTypeFK = (int)ClientRequestType.TripRequest,
                TripFK = ClientTripDTO.TripID,
                StatusFK = (int)RequestStatus.New,
                IsActive = true,
                CreationDate = creationDate,
                
                
            };
            ClientTripRequestsBLL.Add(ClientTripRequest);
            return ClientTripRequest;

        }
        
    }
}
