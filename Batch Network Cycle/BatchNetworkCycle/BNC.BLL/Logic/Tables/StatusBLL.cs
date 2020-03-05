using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNC.DAL.Repositories;
using BNC.Entities;
using BNC.DTOs;
using BNC.DTOs.Business;

namespace BNC.BLL.Logic.Tables
{
    public class StatusBLL : Repository<StatusDIM>
    {
        DateTime creationDate;

        public StatusBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
        
        public List<StatusDTO> GetStatusOfEntity(int EntityID)
        {
            List<StatusDTO> statuses = new List<StatusDTO>(); 
            foreach (var items in Find(x => x.SystemEntityFK == EntityID && x.CanLocked == true).ToList())
            {
                statuses.Add(new StatusDTO {
                    StatusID = items.StatusID,
                    StatusName = items.StatusName
                });
            }
            return statuses;
        }
        public string GetStatusName(int StatusID)
        {
            return Find(x => x.StatusID == StatusID).FirstOrDefault().StatusName;
        }
    }
}
