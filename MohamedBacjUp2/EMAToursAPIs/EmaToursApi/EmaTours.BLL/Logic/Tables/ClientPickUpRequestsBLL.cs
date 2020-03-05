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
  public  class ClientPickUpRequestsBLL : Repository<ClientPickUpRequest>
    {
        DateTime creationDate;
         
       public ClientPickUpRequestsBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
        public List<ClientPickUpDTO> UserAssignPickUpRequest()
        {
            List<ClientPickUpDTO> ListClientPickUp = new List<ClientPickUpDTO>();
            foreach (var item in GetAll()) {
                ListClientPickUp.Add(new ClientPickUpDTO
                {
                    BackFlightTime = (DateTime)item.ClientBackFlightTime,
                    ClientID = item.ClientFK,
                    ClientNote = item.ClientNotes,
                    CreationDate = (DateTime)item.CreationDate,
                    DesiredPickupTime = (DateTime)item.DeterminedPickupTime,
                    NumberOfMembers = (int)item.NumberOfMembersPerRequest,
                    PickUpRequestID = item.ClientPickUpRequestID,
                    UserAssigneeComment = item.AssignedUserComment,
                    UserAssigneeID = (int)item.AssignedUserFK
                });
            }
            return ListClientPickUp;

        }

        public void UserAssignPickUpRequest(ClientPickUpDTO ClientPickUp)
        {
            ClientPickUpRequest TripRequest = Find(x => x.ClientPickUpRequestID == ClientPickUp.PickUpRequestID).FirstOrDefault();
            TripRequest.AssignedAgentFK = ClientPickUp.UserAssigneeID;
            TripRequest.AssignTime = creationDate;
            TripRequest.StatusFK = (int)RequestStatus.Pending;
            Edit(TripRequest);

        }
        public void UserSetPickUpRequestTime(ClientPickUpDTO ClientPickUp)
        {
            ClientPickUpRequest TripRequest = Find(x => x.ClientPickUpRequestID == ClientPickUp.PickUpRequestID).FirstOrDefault();
            TripRequest.AssignedUserActionTime = creationDate;
            TripRequest.DeterminedPickupTime = ClientPickUp.DesiredPickupTime;
            TripRequest.NumberOfMembersPerRequest = ClientPickUp.NumberOfMembers;
            TripRequest.AssignedUserComment = ClientPickUp.UserAssigneeComment;
            TripRequest.AssignedUserActionFK =(int) UserAction.SetPickUpTime;
            TripRequest.StatusFK = (int)RequestStatus.New;

            Edit(TripRequest);

        }
        public void UserClosePickUpRequest(ClientPickUpDTO ClientPickUp)
        {
            ClientPickUpRequest TripRequest = Find(x => x.ClientPickUpRequestID == ClientPickUp.PickUpRequestID).FirstOrDefault();
            TripRequest.CloseTime = creationDate;
            TripRequest.StatusFK = (int)RequestStatus.Closed;
            Edit(TripRequest);

        }





    }
}
