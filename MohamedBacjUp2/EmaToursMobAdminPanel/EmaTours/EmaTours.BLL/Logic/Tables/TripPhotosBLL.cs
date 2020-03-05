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
    public class TripPhotosBLL : Repository<TripPhoto>
    {
        DateTime creationDate;

        public TripPhotosBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
        public List<PhotoDTO> GetAllTripPhotos(int TripID)
        {
           List< PhotoDTO> ListPhotoDTO = new  List<PhotoDTO>();
          foreach (var item in  Find(x=>x.TripFK== TripID&&x.IsActive==true))
            {
                ListPhotoDTO.Add(new PhotoDTO
                { LangugeID= item.LanguageFK,
                 PhotoPath= item.PhotoPath,
                  PhotoID= item.TripPhotoID,
                   PhotoName= item.PhotoName,
                    TripID=(int) item.TripFK,
                     IsActive=(bool) item.IsActive




                });
            }

            return ListPhotoDTO;

        }
        public void AddTripPhotos(PhotoDTO Photo)
        {


                Add (new  TripPhoto
                {
                     LanguageFK = Photo.LangugeID,
                    PhotoPath = Photo.PhotoPath,
                    PhotoName = Photo.PhotoName,
                     TripFK = (int)Photo.TripID,
                    IsActive = true




                });
            
        }

    }

}
