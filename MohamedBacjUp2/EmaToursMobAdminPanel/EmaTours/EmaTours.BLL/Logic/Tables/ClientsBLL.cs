using EmaTours.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmaTours.Entities;
using EmaTours.DTOs.Business;

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
                CountryName= countriesBLL.Find(x=>x.CountryID== Client.ClientCountryFK).FirstOrDefault()?.CountryName            };

        }

        public List<ClientDTO> GetAllClients()
        {
            List<ClientDTO> Client = new List<ClientDTO>();
            foreach(var item in GetAll())
            {
                Client.Add(new ClientDTO
                {
                     ClientEmail= item.ClientEmail,
                      ClientID= item.ClientID,
                       ClientName= item.ClientName,
                        ClientPassport= item.ClientPassportNumber,
                          ClientPhone= item.ClientPhone


                });

            }
            return Client;

        }

    }

}
