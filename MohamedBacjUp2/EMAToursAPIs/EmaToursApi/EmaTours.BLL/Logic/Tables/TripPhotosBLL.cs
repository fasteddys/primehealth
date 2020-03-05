using EmaTours.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmaTours.Entities;

namespace EmaTours.BLL.Logic.Tables
{
    public class TripPhotosBLL : Repository<TripPhoto>
    {
        DateTime creationDate;

        public TripPhotosBLL(EMAToursEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }

    }

}
