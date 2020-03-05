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
    public class TripsBLL : Repository<Trip>
    {
        DateTime creationDate;
        OperatingCountriesBLL operatingCountriesBLL;
        CurrenciesBLL currenciesBLL;
        OperatingLocationsBLL operatingLocationsBLL;
        TripPhotosBLL tripPhotosBLL;
        TripCurrencyBLL tripCurrencyBLL;
        public TripsBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
            operatingCountriesBLL = new OperatingCountriesBLL(Context, CreationDate);
            currenciesBLL = new CurrenciesBLL(Context, CreationDate);
            operatingLocationsBLL = new OperatingLocationsBLL(Context, CreationDate);
            tripPhotosBLL = new TripPhotosBLL(Context, CreationDate);
            tripCurrencyBLL = new TripCurrencyBLL(Context, CreationDate);
        }
        public List<TripDTO> GetAllTripsData(int LanagugeID)
        {
            List<Trip> ListClientTrips =GetAll().ToList();
            List<TripDTO> TripDTO = new List<TripDTO>();
            foreach (var item in ListClientTrips)
            {
                int? CountryID = operatingCountriesBLL.Find(x => x.CountryID == item.CountryFK).FirstOrDefault()?.CountryID;
                int? CurrencyID = currenciesBLL.Find(x => x.CurrencyID == item.CurrencyFK).FirstOrDefault()?.CurrencyID;
                int? LocationID = operatingLocationsBLL.Find(x => x.LocationID == item.LocationFK).FirstOrDefault()?.LocationID;
                List<PhotoDTO> ListPhotos = new List<PhotoDTO>();
                foreach (var Photo in tripPhotosBLL.Find(x => x.TripFK == item.TripID && x.IsActive == true && x.LanguageFK == LanagugeID).ToList())
                    ListPhotos.Add(new PhotoDTO
                    {
                        PhotoName = Photo.PhotoInfo,
                        PhotoPath = Photo.PhotoPath,

                    });

                TripDTO.Add(new TripDTO
                {
                    TripID = item.TripID,
                    CountryID = CountryID,
                    CurrencyID = CurrencyID,
                    LocationID = LocationID,
                    TripDetailedDesc = item.TripDetailedDesc,
                    TripName = item.TripName,
                    TripShortDesc = item.TripShortDesc,
                    Photos = ListPhotos,

                });


            }

            return TripDTO;

        }
        public List<TripDTO> GetAllTripsData()
        {
            List<Trip> ListClientTrips = GetAll().ToList();
            List<TripDTO> TripDTO = new List<TripDTO>();
            foreach (var item in ListClientTrips)
            {
                int? CountryID = operatingCountriesBLL.Find(x => x.CountryID == item.CountryFK).FirstOrDefault()?.CountryID;
                int? CurrencyID = currenciesBLL.Find(x => x.CurrencyID == item.CurrencyFK).FirstOrDefault()?.CurrencyID;
                int? LocationID = operatingLocationsBLL.Find(x => x.LocationID == item.LocationFK).FirstOrDefault()?.LocationID;
                List<PhotoDTO> ListPhotos = new List<PhotoDTO>();
                foreach (var Photo in tripPhotosBLL.Find(x => x.TripFK == item.TripID && x.IsActive == true ).ToList())
                    ListPhotos.Add(new PhotoDTO
                    {
                        PhotoName = Photo.PhotoInfo,
                        PhotoPath = Photo.PhotoPath,

                    });

                TripDTO.Add(new TripDTO
                {
                    TripID = item.TripID,
                    CountryID = CountryID,
                    CurrencyID = CurrencyID,
                    LocationID = LocationID,
                   // Price = item.Price,
                    TripDetailedDesc = item.TripDetailedDesc,
                    TripName = item.TripName,
                    TripShortDesc = item.TripShortDesc,
                    Photos = ListPhotos,
                     CountryName= operatingCountriesBLL.Find(x => x.CountryID == item.CountryFK).FirstOrDefault()?.ConutryName
                     ,
                      LocationName= operatingLocationsBLL.Find(x => x.LocationID == item.LocationFK).FirstOrDefault()?.LocationName
                     
                });


            }

            return TripDTO;

        }

        public Trip AddNewTrip(TripDTO TripDTO)
        {
            Trip NewTrip = new Trip
            {
                CountryFK = TripDTO.CountryID,
                CreationDate = creationDate,
                CurrencyFK = TripDTO.CurrencyID,
                IsActive = true,
                LocationFK = TripDTO.LocationID,
                TripName = TripDTO.TripName,
                TripShortDesc = TripDTO.TripShortDesc,
                TripDetailedDesc = TripDTO.TripDetailedDesc,
                LanguageFK = TripDTO.LangugeID,
                 
                
            };
            Add(NewTrip);

            tripCurrencyBLL.Add(new TripCurrency
            { CurrencyFK=(int) TripDTO.CurrencyID,
              Price= TripDTO.Price,
                Trip= NewTrip

            });
            if (TripDTO.Photos == null)
            {
                TripDTO.Photos = new List<PhotoDTO>();

            }

            foreach (var item in TripDTO.Photos)
            {
                tripPhotosBLL.Add(new TripPhoto
                {
                    CreationDate = creationDate,
                    IsActive = true,
                     PhotoName = item.PhotoName,
                    PhotoPath = item.PhotoPath,
                    TripFK = NewTrip.TripID,
                    LanguageFK = item.LangugeID
                });

            }
            return NewTrip;
        }
        public void EditTrip(TripDTO TripDTO)
        {

            Trip Trip=  Find(x => x.TripID == TripDTO.TripID).FirstOrDefault();

            Trip. CountryFK = TripDTO.CountryID;
             Trip.CurrencyFK = TripDTO.CurrencyID;
             Trip.IsActive = true;
             //Trip.Price = TripDTO.Price;
             Trip.LocationFK = TripDTO.LocationID;
              Trip.TripName = TripDTO.TripName;
             Trip.TripShortDesc = TripDTO.TripShortDesc;
              Trip.TripDetailedDesc = TripDTO.TripDetailedDesc;
            Trip.LanguageFK = TripDTO.LangugeID;
            Edit(Trip);
            if (TripDTO.Photos == null)
            {
                TripDTO.Photos = new List<PhotoDTO>();

            }

            foreach (var item in TripDTO.Photos.Where(x=>x.IsActive==false))
            {
                TripPhoto TripPhoto= tripPhotosBLL.Find(x => x.TripPhotoID == item.PhotoID).FirstOrDefault();
                TripPhoto.IsActive = false;
                tripPhotosBLL. Edit(TripPhoto);
            }
        }

        public void DeactivateTrip(int TripID)
        {
            Trip Trip = Find(x => x.TripID == TripID).FirstOrDefault();
            Trip.IsActive = false;
            Edit(Trip);

        }
        public TripDTO GetTripByID(int TripID)
        {
            Trip trip= Find(x => x.TripID == TripID).FirstOrDefault();
            TripDTO Trip =
                new TripDTO
                {
                    CountryID = trip.CountryFK,
                    CurrencyID = trip.CurrencyFK,
                    LangugeID = trip.LanguageFK,
                    LocationID = trip.LocationFK,
                   // Price = trip.Price,
                    TripDetailedDesc = trip.TripDetailedDesc,
                    TripID = trip.TripID,
                    TripName = trip.TripName,
                    TripShortDesc = trip.TripShortDesc

                };

           var tripCurrency= tripCurrencyBLL.Find(x => x.TripFK == TripID).FirstOrDefault();
            Trip.TripCurrency = new TripCurrencyDTO
            {
                TripID = tripCurrency.TripFK,
                Price = tripCurrency.Price,
                TripCurrencyID = tripCurrency.TripCurrencyID,
                CurrencyID = tripCurrency.CurrencyFK


            };

            return Trip;

        }

        public void AddNewPhotoToTrip(PhotoDTO Photo)
        {


            tripPhotosBLL.Add(new TripPhoto
            {
                CreationDate = creationDate,
                IsActive = true,
                LanguageFK = Photo.LangugeID,
                TripFK = Photo.TripID,
                PhotoPath = Photo.PhotoPath,
                PhotoName = Photo.PhotoName
            });

        }


    }
}
