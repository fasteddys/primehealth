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
        ClientsBLL clientsBLL;
        EMAUsersBLL usersBLL;
        StatusBLL statusBLL;
       public ClientPickUpRequestsBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
            clientsBLL = new ClientsBLL(Context, CreationDate);
            usersBLL = new EMAUsersBLL(Context, CreationDate);
            statusBLL = new StatusBLL(Context, CreationDate);
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
        public ClientPickUpRequest UserSetPickUpRequestTime(ClientPickUpDTO ClientPickUp)
        {
            ClientPickUpRequest TripRequest = Find(x => x.ClientPickUpRequestID == ClientPickUp.PickUpRequestID).FirstOrDefault();
            TripRequest.AssignedUserActionTime = creationDate;
            TripRequest.NumberOfMembersPerRequest = ClientPickUp.NumberOfMembers;
            TripRequest.AssignedUserComment = ClientPickUp.UserAssigneeComment;
            TripRequest.AssignedUserActionFK =(int) UserAction.AddPickupTime;
            TripRequest.DeterminedPickupTime = ClientPickUp.DeterminedPickupTime;
            Edit(TripRequest);
            return TripRequest;
        }
        public void UserClosePickUpRequest(ClientPickUpDTO ClientPickUp)
        {
            ClientPickUpRequest TripRequest = Find(x => x.ClientPickUpRequestID == ClientPickUp.PickUpRequestID).FirstOrDefault();
            TripRequest.CloseTime = creationDate;
            TripRequest.StatusFK = (int)RequestStatus.Closed;
            Edit(TripRequest);

        }

        public List<ClientPickUpDTO> GetAllPickUpRequest()
        {

            List<ClientPickUpDTO> ClientPickUpDTO = new List<ClientPickUpDTO>();
          foreach (var item in  GetAll())
            {
                try
                {
                    ClientPickUpDTO.Add(
                         new ClientPickUpDTO
                         {
                             CreationDate = creationDate,
                             NumberOfMembers = item.NumberOfMembersPerRequest,
                         // DesiredPickupTime = (DateTime)item.ClientDesiredPickupTime,
                         ClientNote = item.ClientNotes,
                             ClientName = clientsBLL.Find(x => x.ClientID == item.ClientFK).FirstOrDefault().ClientName,
                             PickUpRequestID = item.ClientPickUpRequestID,
                             BackFlightTime = item.ClientBackFlightTime,
                             ClientID = item.ClientFK,
                             StatusID = item.StatusFK,
                             DesiredPickupTime = item.ClientDesiredPickupTime,
                             UserAssigneeComment = item.AssignedUserComment,
                             UserAssigneeID = item.AssignedUserFK
                                    ,
                             UserAssigneeName =

                                     usersBLL.Find(x => x.UserID == item.AssignedAgentFK).FirstOrDefault()?.UserName
                         }

                        );

                }
                catch (Exception ex) { continue; }
            }
            return ClientPickUpDTO;


        }

        public List<ClientPickUpDTO> GetPickUpRequest(int RequestStatusID)
        {

            List<ClientPickUpDTO> ClientPickUpDTO = new List<ClientPickUpDTO>();
            foreach (var item in Find(x=>x.StatusFK== RequestStatusID))
            {
                ClientPickUpDTO.Add(
                     new ClientPickUpDTO
                     {
                         CreationDate = creationDate,
                         NumberOfMembers =item.NumberOfMembersPerRequest,
                         DesiredPickupTime = item.ClientDesiredPickupTime,
                         ClientNote = item.ClientNotes,
                         ClientName = clientsBLL.Find(x => x.ClientID == item.ClientFK).FirstOrDefault().ClientName



                     }

                    );


            }
            return ClientPickUpDTO;


        }

        public List<ClientPickUpDTO> GetAssignedPickUpRequest(int UserID)
        {

            List<ClientPickUpDTO> ClientPickUpDTO = new List<ClientPickUpDTO>();
            foreach (var item in Find(x => x.AssignedAgentFK == UserID))
            {
                ClientPickUpDTO.Add(
                     new ClientPickUpDTO
                     {
                         CreationDate = creationDate,
                         NumberOfMembers = item.NumberOfMembersPerRequest,
                         DesiredPickupTime = item.ClientDesiredPickupTime,
                         ClientNote = item.ClientNotes,
                         ClientName = clientsBLL.Find(x => x.ClientID == item.ClientFK).FirstOrDefault().ClientName



                     }

                    );


            }
            return ClientPickUpDTO;


        }
        public ClientPickUpDTO GetPickupRequestByID(int PickupRequestID)
        {
         ClientPickUpRequest Request=   Find(x => x.ClientPickUpRequestID == PickupRequestID).FirstOrDefault();
            ClientPickUpDTO ClientPickUpDTO = new ClientPickUpDTO
            {
                BackFlightTime = Request.ClientBackFlightTime,
                ClientID = Request.ClientFK,
                ClientNote= Request.ClientNotes,
                CreationDate = Request.CreationDate,
                DesiredPickupTime = Request.ClientDesiredPickupTime,
                NumberOfMembers = Request.NumberOfMembersPerRequest,
                PickUpRequestID = Request.ClientPickUpRequestID,
                UserAssigneeComment = Request.AssignedUserComment,
                UserAssigneeID = Request.AssignedUserFK
            };

            return ClientPickUpDTO;
        }

        public ClientPickUpDTO ManagePickUpRequest(int PickUpRequestID)
        {
            ClientPickUpRequest ClientPickUp = Find(x => x.ClientPickUpRequestID == PickUpRequestID).FirstOrDefault();


            ClientPickUpDTO clienttripdto = new ClientPickUpDTO
            {
                BackFlightTime = ClientPickUp.ClientBackFlightTime,
                ClientID = ClientPickUp.ClientFK,
                ClientNote = ClientPickUp.ClientNotes,
                DesiredPickupTime = ClientPickUp.DeterminedPickupTime,
                CreationDate = ClientPickUp.CreationDate,
                NumberOfMembers = ClientPickUp.NumberOfMembersPerRequest,
                PickUpRequestID = ClientPickUp.ClientPickUpRequestID,
                UserAssigneeComment = ClientPickUp.AssignedUserComment,
                StatusID = ClientPickUp.StatusFK,
                UserAssigneeID = ClientPickUp.AssignedAgentFK,
                DeterminedPickupTime = ClientPickUp.DeterminedPickupTime,
                ClientName = clientsBLL.Find(x => x.ClientID == ClientPickUp.ClientFK).FirstOrDefault()?.ClientName,
                 Status= statusBLL.Find(x=>x.StatusID== ClientPickUp.StatusFK).FirstOrDefault()?.StatusName,
                  UserAssigneeName= usersBLL.Find(x=>x.UserID== ClientPickUp.AssignedAgentFK).FirstOrDefault()?.UserName

            };

            return clienttripdto;
        }

    }
}
