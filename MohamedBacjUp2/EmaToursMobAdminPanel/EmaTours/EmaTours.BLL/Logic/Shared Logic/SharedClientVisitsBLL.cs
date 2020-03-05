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
    class SharedClientVisitsBLL
    {
        DateTime creationDate;
        ClientVisitsBLL clientVisitsBLL;
        ClientsBLL clientsBLL;
            
        public ClientVisitsDTO  GetAllClientVisits(int ClientID)
        {
            ClientVisitsDTO ClientVisitsDTO = new ClientVisitsDTO();
            ClientVisitsDTO.ClientID = ClientID;
            foreach(var item in clientVisitsBLL.Find(x => x.ClientFK == ClientID))
            {
                ClientVisitsDTO.ListVisits.Add(new VisitsDTO
                { CountryID= item.OperatingCountryFK,
                  StartVisitDate= item.CreationDate,
                  EndVisitDate = item.VisitEndDate,
                  VisitID= item.ClientVisitID
                   
                });
            }
            return ClientVisitsDTO;

        }
    }

}
