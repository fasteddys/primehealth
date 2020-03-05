using EmaTours.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmaTours.Entities;
using EmaTours.DTOs.Business;
using EmaTours.Utilities;
using EmaTours.BLL.Logic.Helpers;
using EmaTours.BLL.StaticData;

namespace EmaTours.BLL.Logic.Tables
{
    public class ClientsBLL : Repository<Client>
    {
        DateTime creationDate;
        CountriesBLL countriesBLL;
        public ClientsBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
            countriesBLL = new CountriesBLL(Context, CreationDate);
        }


        public Client SignUp(ClientDTO ClientDTO)
        {
          string CountryCode= countriesBLL.Find(x => x.CountryID == ClientDTO.CountryID).FirstOrDefault().CountryCode;

            Client Client=new Client
            {
                ClientCountryFK = ClientDTO.CountryID,
                ClientDeviceID = ClientDTO.ClientDeviceID,
                ClientEmail = ClientDTO.ClientEmail,
                ClientName = ClientDTO.ClientName,
                ClientPassportNumber = ClientDTO.ClientPassport,
                ClientPhone = ClientDTO.ClientPhone,
                CreationDate = creationDate,
                IsActive = true,
                ClientPreferredNotificationMethodFK = ClientDTO.NotificationMethodID,
                
            };
            Add(Client);
            return Client;

        }
        public ClientDTO CheckIfClientSignUp(ClientDTO ClientDTO)
        {
            Client Client = Find(x => x.ClientPhone == ClientDTO.ClientPhone).FirstOrDefault();
            if (Client == null)
            {
                Client = new Client();
            }
            return new ClientDTO
            {
                ClientEmail = Client.ClientEmail,
                ClientID = Client.ClientID,
                ClientPhone = Client.ClientPhone,
                ClientPassport = Client.ClientPassportNumber,
                ClientName = Client.ClientName,
                CountryID = Client.ClientCountryFK
            };
        }
        public ClientDTO GetAllClientData(int ClientID)
        {
            Client Client = Find(x => x.ClientID == ClientID).FirstOrDefault(); ;
            return new ClientDTO
            {
                ClientEmail = Client.ClientEmail,
                ClientID = Client.ClientID,
                ClientPhone = Client.ClientPhone,
                ClientPassport = Client.ClientPhone,
                ClientName = Client.ClientName,
            };

        }

        public void EditClientData(ClientDTO ClientDTO)
        {
            Client Client = Find(x => x.ClientID == ClientDTO.ClientID).FirstOrDefault();
            var oldClient = XMLObjectConverter.Serialize(Client);

            Client.ClientName = ClientDTO.ClientName;
            Client.ClientEmail = ClientDTO.ClientEmail;
            Client.ClientPhone = ClientDTO.ClientPhone;
            Client.ClientPassportNumber = ClientDTO.ClientPassport;
            Client.ClientCountryFK = ClientDTO.CountryID;

            Edit(Client);
            Logger.LogCypressEvent(null, (int)StaticEnums.LogTypes.EditClientData, (int)ClientDTO.LoggedUser, oldClient, XMLObjectConverter.Serialize(Client), "Edit Client Data");
        }

    }

}
