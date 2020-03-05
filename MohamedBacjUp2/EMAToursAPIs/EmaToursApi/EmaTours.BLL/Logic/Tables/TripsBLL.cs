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
        ConfigurationsBLL configurationsBLL;
        TripCurrencyBLL tripCurrencyBLL;
        public TripsBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
            operatingCountriesBLL = new OperatingCountriesBLL(Context, CreationDate);
            currenciesBLL = new CurrenciesBLL(Context, CreationDate);
            operatingLocationsBLL = new OperatingLocationsBLL(Context, CreationDate);
            tripPhotosBLL = new TripPhotosBLL(Context, CreationDate);
            configurationsBLL = new ConfigurationsBLL(Context, CreationDate);
            tripCurrencyBLL = new TripCurrencyBLL(Context, CreationDate);
        }
        public List<TripDTO> GetAllTripsData(int languageID)
        {
            List<Trip> ListClientTrips = GetAll().ToList();
            List<TripDTO> TripDTO = new List<TripDTO>();
            foreach (var item in ListClientTrips)
            {
                int? CountryID = operatingCountriesBLL.Find(x => x.CountryID == item.CountryFK).FirstOrDefault()?.CountryID;
                var Currency = currenciesBLL.Find(x => x.CurrencyID == item.CurrencyFK).FirstOrDefault();
                int? LocationID = operatingLocationsBLL.Find(x => x.LocationID == item.LocationFK).FirstOrDefault()?.LocationID;
                List<PhotoDTO> ListPhotos = new List<PhotoDTO>();
                foreach (var Photo in tripPhotosBLL.Find(x => x.TripFK == item.TripID && x.IsActive == true && x.LanguageFK == languageID).ToList())
                    ListPhotos.Add(new PhotoDTO
                    {
                        PhotoName = Photo.PhotoInfo,
                        PhotoPath = Photo.PhotoPath,

                    });

                List<TripCurrencyDTO> tripCurrencies = new List<TripCurrencyDTO>();
                foreach (var currencyItem in tripCurrencyBLL.Find(x => x.TripFK == item.TripID).ToList())
                {
                    tripCurrencies.Add(new TripCurrencyDTO
                    {
                        CurrencyName = Currency.CurrencyName,
                        Price = currencyItem.Price,
                        TripID = item.TripID,
                        CurrencyID = Currency.CurrencyID,
                         CurrencySign= Currency.CurrencySign


                    });
                }


                TripDTO.Add(new TripDTO
                {
                    TripID = item.TripID,
                    CountryID = CountryID,
                    CurrencyID = Currency. CurrencyID,
                    LocationID = LocationID,
                    //Price = item.Price,
                    TripDetailedDesc = item.TripDetailedDesc,
                    TripName = item.TripName,
                    TripShortDesc = item.TripShortDesc,
                    Photos = ListPhotos,
                    TripCurrency = tripCurrencies


                });


            }

            return TripDTO;

        }
        public List<TripDTO> GetAllTripsDataByLocation(int languageID,int LocationID)
        {
            List<Trip> ListClientTrips = Find(x=>x.LanguageFK== languageID&&x.LocationFK== LocationID).ToList();
            List<TripDTO> TripDTO = new List<TripDTO>();
            foreach (var item in ListClientTrips)
            {
                int? Country = operatingCountriesBLL.Find(x => x.CountryID == item.CountryFK).FirstOrDefault()?.CountryID;
                var Currency = currenciesBLL.Find(x => x.CurrencyID == item.CurrencyFK).FirstOrDefault();
                int? Location = operatingLocationsBLL.Find(x => x.LocationID == item.LocationFK).FirstOrDefault()?.LocationID;
                List<PhotoDTO> ListPhotos = new List<PhotoDTO>();
                foreach (var Photo in tripPhotosBLL.Find(x => x.TripFK == item.TripID && x.IsActive == true && x.LanguageFK == languageID).ToList())
                    ListPhotos.Add(new PhotoDTO
                    {
                        PhotoName = Photo.PhotoInfo,
                        PhotoPath = configurationsBLL.GetConfigrationByKey("TripsPhotosUrl") + Photo.PhotoPath,

                    });
                List<TripCurrencyDTO> tripCurrencies = new List<TripCurrencyDTO>();
                foreach (var currencyItem in tripCurrencyBLL.Find(x => x.TripFK == item.TripID).ToList())
                {
                    tripCurrencies.Add(new TripCurrencyDTO
                    {
                        CurrencyName = Currency.CurrencyName,
                        Price = currencyItem.Price,
                        TripID = item.TripID,
                        CurrencyID = Currency.CurrencyID,
                        CurrencySign = Currency.CurrencySign


                    });
                }
                TripDTO.Add(new TripDTO
                {
                    TripID = item.TripID,
                    CountryID = Country,
                    CurrencyID = Currency.CurrencyID,
                    LocationID = Location,
                    //Price = item.Price,
                    TripDetailedDesc = item.TripDetailedDesc,
                    TripName = item.TripName,
                    TripShortDesc = item.TripShortDesc,
                    Photos = ListPhotos,
                    TripCurrency = tripCurrencies

                });


            }

            return TripDTO;

        }

        public TripDTO GetTripData(int TripID,int languageID)
        {
           Trip Trip = Find(x=>x.TripID== TripID&&x.LanguageFK== languageID).FirstOrDefault();
           TripDTO TripDTO = new TripDTO();
         
                string CountryName = operatingCountriesBLL.Find(x => x.CountryID == Trip.CountryFK).FirstOrDefault()?.ConutryName;
            var Currency = currenciesBLL.Find(x => x.CurrencyID == Trip.CurrencyFK).FirstOrDefault();
            string LocationName = operatingLocationsBLL.Find(x => x.LocationID == Trip.LocationFK).FirstOrDefault()?.LocationName;
                List<PhotoDTO> ListPhotos = new List<PhotoDTO>();
                foreach (var Photo in tripPhotosBLL.Find(x => x.TripFK == Trip.TripID && x.IsActive == true && x.LanguageFK == languageID).ToList())
                    ListPhotos.Add(new PhotoDTO
                    {
                        PhotoName = Photo.PhotoInfo,
                        PhotoPath = configurationsBLL.GetConfigrationByKey("TripsPhotosUrl")+Photo.PhotoPath,

                    });
            List<TripCurrencyDTO> tripCurrencies = new List<TripCurrencyDTO>();
            foreach (var currencyItem in tripCurrencyBLL.Find(x => x.TripFK == TripID).ToList())
            {
                tripCurrencies.Add(new TripCurrencyDTO
                {
                    CurrencyName = Currency.CurrencyName,
                    Price = currencyItem.Price,
                    TripID = TripID,
                    CurrencyID = Currency.CurrencyID,
                    CurrencySign = Currency.CurrencySign


                });
            }
            TripDTO =new TripDTO
                {
                    TripID = Trip.TripID,
                    CountryName = CountryName,
                    CurrencyName = Currency.CurrencyName,
                    LocationName = LocationName,
                   // Price = Trip.Price,
                    TripDetailedDesc = Trip.TripDetailedDesc,
                    TripName = Trip.TripName,
                    TripShortDesc = Trip.TripShortDesc,
                    Photos = ListPhotos,
                    TripCurrency = tripCurrencies

                };

            return TripDTO;

        }


        //public void AddNewTrip(TripDTO TripDTO)
        //{
        //    Trip NewTrip = new Trip
        //    {
        //        CountryFK = TripDTO.CountryID,
        //        CreationDate = creationDate,
        //        CurrencyFK = TripDTO.CurrencyID,
        //        IsActive = true,
        //        Price = TripDTO.Price,
        //        LocationFK = TripDTO.LocationID,
        //        TripName = TripDTO.TripName,
        //        TripShortDesc = TripDTO.TripShortDesc,
        //        TripDetailedDesc = TripDTO.TripDetailedDesc,
        //        LanguageFK = TripDTO.LangugeID,
        //        TripCurrency = tripCurrencies

        //    };
        //    Add(NewTrip);
        //    foreach (var item in TripDTO.Photos)
        //    {
        //        tripPhotosBLL.Add(new TripPhoto
        //        {
        //            CreationDate = creationDate,
        //            IsActive = true,
        //             PhotoName = item.PhotoName,
        //            PhotoPath = item.PhotoPath,
        //            TripFK = NewTrip.TripID,
        //            LanguageFK = item.LangugeID
        //        });

        //    }
        //}
        public void DeactivateTrip(int TripID)
        {
            Trip Trip = Find(x => x.TripID == TripID).FirstOrDefault();
            Trip.IsActive = false;
            Edit(Trip);

        }
        //public void EditTrip(TripDTO TripDTO)
        //{
        //    Trip NewTrip = Find(x => x.TripID == TripDTO.TripID).FirstOrDefault();

        //    NewTrip.CountryFK = TripDTO.CountryID;
        //    NewTrip.CreationDate = creationDate;
        //    NewTrip.CurrencyFK = TripDTO.CurrencyID;
        //    NewTrip.Price = TripDTO.Price;
        //    NewTrip.LocationFK = TripDTO.LocationID;
        //    NewTrip.TripName = TripDTO.TripName;
        //    NewTrip.TripShortDesc = TripDTO.TripShortDesc;
        //    NewTrip.TripDetailedDesc = TripDTO.TripDetailedDesc;
        //    NewTrip.LanguageFK = TripDTO.LangugeID;

        //    foreach (var item in TripDTO.Photos)
        //    {
        //        TripPhoto TripPhoto = tripPhotosBLL.Find(x => x.TripPhotoID == item.PhotoID).FirstOrDefault();
        //        TripPhoto.CreationDate = creationDate;
        //        TripPhoto.IsActive = true;
        //        TripPhoto.PhotoName = item.PhotoName;
        //        TripPhoto.PhotoPath = item.PhotoPath;
        //        TripPhoto.TripFK = NewTrip.TripID;
        //        TripPhoto.LanguageFK = item.LangugeID;
        //        tripPhotosBLL.Edit(TripPhoto);

        //    }





        //}
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
