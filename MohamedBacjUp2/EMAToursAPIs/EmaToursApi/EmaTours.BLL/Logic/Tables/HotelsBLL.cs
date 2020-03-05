using EmaTours.DAL.Repositories;
using EmaTours.DTOs.Business;
using EmaTours.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.BLL.Logic.Tables
{
    public class HotelsBLL : Repository<Hotel>
    {
        DateTime creationDate;
        public HotelsBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;

        }
        public List<HotelDTO> SearchHotels(int HoteId = 0, int LanguageID = 0, int OperatingCountryId = 0, int OperatingLocationId = 0)
        {
            var Hotels = GetAll().Where(x => x.HotelID == HoteId || HoteId == 0).Where(x => x.LanguageFK == LanguageID || LanguageID == 0)
                               .Where(x => x.OperatingCountryFK == OperatingCountryId || OperatingCountryId == null || OperatingCountryId == 0)
                               .Where(x => x.OperatingLocationFK == OperatingLocationId || OperatingLocationId == null || OperatingLocationId == 0).ToList();

            List<HotelDTO> hotelDTOs = new List<HotelDTO>();
            foreach (var hotel in Hotels)
            {
                HotelDTO Temp = new HotelDTO();
                Temp.HotelID = hotel.HotelID;
                Temp.Name = hotel.Name;
                Temp.LanguageFK = hotel.LanguageFK;
                Temp.OperatingCountryFK = hotel.OperatingCountryFK;
                Temp.OperatingLocationFK = hotel.OperatingLocationFK;

                hotelDTOs.Add(Temp);
            }

            return hotelDTOs;
        }
        public void AddHotel(HotelDTO hotel)
        {
            Add(new Hotel
            {
                Name = hotel.Name,
                LanguageFK = hotel.LanguageFK,
                OperatingCountryFK = hotel.OperatingCountryFK,
                OperatingLocationFK = hotel.OperatingLocationFK,
            });
        }
        public void EditHotel(HotelDTO hotel)
        {
            Hotel Obj = new Hotel();
            Obj = Find(x => x.HotelID == hotel.HotelID).SingleOrDefault();
            Obj.Name = hotel.Name;
            Obj.LanguageFK = hotel.LanguageFK;
            Obj.OperatingCountryFK = hotel.OperatingCountryFK;
            Obj.OperatingLocationFK = hotel.OperatingLocationFK;

            Edit(Obj);

        }
        public Hotel MapTOHotel(HotelDTO hotelDTO)
        {
            Hotel hotelObj = new Hotel();
            hotelObj.HotelID = hotelDTO.HotelID;
            hotelObj.Name = hotelDTO.Name;
            hotelObj.OperatingCountryFK = hotelDTO.OperatingCountryFK;
            hotelObj.OperatingLocationFK = hotelDTO.OperatingLocationFK;
            hotelObj.LanguageFK = hotelDTO.LanguageFK;

            return hotelObj;
        }
        public HotelDTO MapTOHotelDTO(Hotel hotel)
        {
            HotelDTO hotelDTOObj = new HotelDTO();
            hotelDTOObj.HotelID = hotel.HotelID;
            hotelDTOObj.Name = hotel.Name;
            hotelDTOObj.OperatingCountryFK = hotel.OperatingCountryFK;
            hotelDTOObj.OperatingLocationFK = hotel.OperatingLocationFK;
            hotelDTOObj.LanguageFK = hotel.LanguageFK;

            return hotelDTOObj;
        }
    }
}
