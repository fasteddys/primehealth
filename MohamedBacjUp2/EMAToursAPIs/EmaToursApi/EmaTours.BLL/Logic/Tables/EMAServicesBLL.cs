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
    public class EMAServicesBLL : Repository<EMAService>
    {
        DateTime creationDate;

        public EMAServicesBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
        public void GetAllServices(int LangaugeID)
        {
            List<ServiceDTO> FeedBackDTO = new List<ServiceDTO>();
          foreach (var item in  Find(x => x.LangauageFK == LangaugeID).ToList())
            {
                FeedBackDTO.Add(new ServiceDTO
                {
                    ServiceID = item.ServiceID,
                    ServiceName = item.ServiceName
                });


            }
        }








    }

}
