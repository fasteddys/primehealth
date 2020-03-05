using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNC.DAL.Repositories;
using BNC.Entities;
using BNC.DTOs;

namespace BNC.BLL.Logic.Tables
{
    public class SpReqReasonBLL : Repository<SpReqReason>
    {
        DateTime creationDate;
        public SpReqReasonBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }

        public void AddSpReqReason(int spReasonFK,int spReqFK)
        {
            SpReqReason spReqReason = new SpReqReason
            {
                SPReasonFK = spReasonFK,
                SPReqFK = spReqFK,
                IsActive = true,
                IsDeleted = false,
                CreationDate = DateTime.Now
           
               
            };

            this.Add(spReqReason);
        }

    }
}
