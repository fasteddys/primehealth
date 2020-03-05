using EmaTours.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.BLL.Logic.Shared_Logic
{
 public   class SharedVisitPickUpBLL
    {
        DateTime creationDate;

        public SharedVisitPickUpBLL(EMAToursEntities Context, DateTime CreationDate)
        {
            creationDate = CreationDate;

        }
    }
}
