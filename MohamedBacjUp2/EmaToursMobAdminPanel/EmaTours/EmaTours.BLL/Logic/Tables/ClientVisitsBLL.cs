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
    public class ClientVisitsBLL : Repository<ClientVisit>
    {
        DateTime creationDate;
        OperatingCountriesBLL operatingCountriesBLL;
        OperatingLocationsBLL operatingLocationsBLL;
        public ClientVisitsBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
            operatingCountriesBLL = new OperatingCountriesBLL(Context, CreationDate);
            operatingLocationsBLL = new OperatingLocationsBLL(Context, CreationDate);
        }
        public List<VisitsDTO> GetAllClientVisits(int ClientID)
        {
            List<VisitsDTO> ListVisits = new List<VisitsDTO>();
            
         foreach (var item in    Find(x => x.ClientFK == ClientID).ToList())
            {
                string CountryName = operatingCountriesBLL.Find(x => x.CountryID == item.OperatingCountryFK).FirstOrDefault()?.ConutryName;
                string LocationName  = operatingLocationsBLL.Find(x => x.LocationID == item.OperatingLocationFK).FirstOrDefault()?.LocationName;

                ListVisits.Add(new VisitsDTO
                {
                    ClientID = (int)item.ClientFK,
                    LocationName = LocationName,
                    CountryName = CountryName,
                    CountryID = item.OperatingCountryFK,
                    LocationID = item.OperatingLocationFK,
                    EndVisitDate = item.VisitEndDate,
                    StartVisitDate = item.VisitStartDate,
                    VisitID = item.ClientVisitID
                });


            }
            return ListVisits;
        }



    }

}
