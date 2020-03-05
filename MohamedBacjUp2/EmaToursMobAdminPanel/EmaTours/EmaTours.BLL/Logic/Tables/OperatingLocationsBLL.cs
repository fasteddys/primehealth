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
    public class OperatingLocationsBLL : Repository<OperatingLocation>
    {
        DateTime creationDate;

        public OperatingLocationsBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
      public List<OperatingLocationDTO> GetAllOperatingLocations(OperatingLocationDTO OperatingCountry)
        {
            List<OperatingLocationDTO> ListOperatingLocation = new List<OperatingLocationDTO>();
         foreach(var item in    Find(x => x.LanguageFK == OperatingCountry.LanguageID && x.OperatingCountryFK == OperatingCountry.OperatingLocationID&&x.IsActive==true))
            {
                ListOperatingLocation.Add(new OperatingLocationDTO
                {
                    LanguageID = item.LanguageFK,
                    OperatingLocationID = item.LocationID,
                    OperatingLocationName = item.LocationName
                });
            }
            return ListOperatingLocation;


        }
        public List<OperatingLocationDTO> GetAllOperatingLocations()
        {
            List<OperatingLocationDTO> ListOperatingLocation = new List<OperatingLocationDTO>();
            foreach (var item in Find(x =>  x.IsActive == true))
            {
                ListOperatingLocation.Add(new OperatingLocationDTO
                {
                    LanguageID = item.LanguageFK,
                    OperatingLocationID = item.LocationID,
                    OperatingLocationName = item.LocationName
                });
            }
            return ListOperatingLocation;


        }


        public void AddOperatingLocation(OperatingLocationDTO OperatingLocationDTO)
        {
            Add(new OperatingLocation
            {
                IsActive = true,
                CreationDate = creationDate,
                LanguageFK = OperatingLocationDTO.LanguageID,
                LocationName = OperatingLocationDTO.OperatingLocationName,
                OperatingCountryFK = OperatingLocationDTO.CountryID,
            });

        }

        public OperatingLocationDTO GetOperatingLocation(int OperatingLocationID)
        {
           OperatingLocation OperatingLocation=  Find(x => x.LocationID == OperatingLocationID).FirstOrDefault();
            return new OperatingLocationDTO
            {
                CountryID = OperatingLocation.OperatingCountryFK,
                LanguageID = OperatingLocation.LanguageFK,
                OperatingLocationID = OperatingLocation.LocationID,
                OperatingLocationName = OperatingLocation.LocationName


            };
        }



        public void UpdateOperatingLocation(OperatingLocationDTO OperatingLocationDTO)
        {
            OperatingLocation OperatingLocation =Find(x=>x.LocationID== OperatingLocationDTO.OperatingLocationID).FirstOrDefault();
            OperatingLocation.IsActive = true;
            OperatingLocation.LanguageFK = OperatingLocationDTO.LanguageID;
            OperatingLocation.LocationName = OperatingLocationDTO.OperatingLocationName;
            OperatingLocation.OperatingCountryFK = OperatingLocationDTO.CountryID;
            
            Edit(OperatingLocation);

        }

        public void DeActivateOperatingLocation()
        {
            OperatingLocation OperatingLocation = new OperatingLocation();
            OperatingLocation.IsActive = false;
            Edit(OperatingLocation);
        }



    }

}
