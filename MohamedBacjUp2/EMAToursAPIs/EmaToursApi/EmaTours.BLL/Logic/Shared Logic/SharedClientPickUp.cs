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
using static EmaTours.BLL.StaticData.StaticEnums;

namespace EmaTours.BLL.Logic.Shared_Logic
{
   public class SharedClientPickUp
    {
        DateTime creationDate;
        readonly ClientPickUpRequestsBLL clientPickUpRequestsBLL;
        public SharedClientPickUp(EMAToursEntities Context, DateTime CreationDate)
        {
            creationDate = CreationDate;
            clientPickUpRequestsBLL = new ClientPickUpRequestsBLL(Context, CreationDate);
        }
        public ClientPickUpRequest BookPickUp(ClientPickUpDTO PickUpDTO)
        {
            ClientPickUpRequest ClientPickUpRequest = new ClientPickUpRequest
            {
                ClientBackFlightTime = PickUpDTO.BackFlightTime,
                //ClientDesiredPickupTime = PickUpDTO.DesiredPickupTime,
                NumberOfMembersPerRequest = PickUpDTO.NumberOfMembers,
                ClientVisitFK = PickUpDTO.ClientVisitFK,
                StatusFK = (int)RequestStatus.New,
                IsActive = true,
                CreationDate = creationDate,
                ClientNotes = PickUpDTO.ClientNote,
                ClientFK = PickUpDTO.ClientID,

            };

            clientPickUpRequestsBLL.Add(ClientPickUpRequest);
            return ClientPickUpRequest;
        }

        public ClientPickUpDTO GetPickUp(int PickUpID)
        {
            ClientPickUpRequest clientPickUpRequest = clientPickUpRequestsBLL.Find(x => x.ClientPickUpRequestID == PickUpID).FirstOrDefault();

            ClientPickUpDTO clientPickUpDTO = new ClientPickUpDTO
            {
                BackFlightTime = clientPickUpRequest.ClientBackFlightTime.Value,
                ClientNote = clientPickUpRequest.ClientNotes,
                NumberOfMembers = clientPickUpRequest.NumberOfMembersPerRequest.Value
            };

            return clientPickUpDTO;
        }

        public void EditPickUp(ClientPickUpDTO PickUpDTO)
        {
            ClientPickUpRequest clientPickUpRequest = clientPickUpRequestsBLL.Find(x => x.ClientPickUpRequestID == PickUpDTO.PickUpRequestID).FirstOrDefault();
            var oldclientPickUpRequest = XMLObjectConverter.Serialize(clientPickUpRequest);

            clientPickUpRequest.ClientBackFlightTime = PickUpDTO.BackFlightTime;
            clientPickUpRequest.NumberOfMembersPerRequest = PickUpDTO.NumberOfMembers;
            clientPickUpRequest.ClientNotes = PickUpDTO.ClientNote;

            clientPickUpRequestsBLL.Edit(clientPickUpRequest);
            Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.EditClientData, (int)PickUpDTO.LoggedUser, oldclientPickUpRequest, XMLObjectConverter.Serialize(clientPickUpRequest), "Edit PickUp Data");
        }



    }
}
