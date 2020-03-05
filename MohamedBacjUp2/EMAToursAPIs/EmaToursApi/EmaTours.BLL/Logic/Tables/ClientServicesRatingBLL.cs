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
    public class ClientServicesRatingBLL : Repository<ClientServicesRating>
    {
        DateTime creationDate;

        public ClientServicesRatingBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }

       public void AddFeedBack(List<FeedBackDTO> FeedBack)
        {
            foreach(var item in FeedBack)
            {
                Add(new ClientServicesRating
                {
                    ClientFK = item.ClientID,
                    ClientCommentPerService = item.ClientComment,
                    ClientVisitFK = item.ClientVisitID,
                    CreationDate = creationDate,
                    IsActive = true,
                    RatingScale = item.RatingScale,
                    ServiceFK = item.ServiceID,
                });
            }
        }




    }

}
