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
    public class SystemEntitiesBLL : Repository<SystemEntitiesDIM>
    {
        DateTime creationDate;

        public SystemEntitiesBLL(BNCEntities Context, DateTime CreationDate) : base(Context)
        {
            creationDate = CreationDate;
        }
        
        public List<SystemEntitiesDTO> GetAllEntites()
        {
            List<SystemEntitiesDTO> SystemEntities = new List<SystemEntitiesDTO>();
            foreach (var items in GetAll())
            {
                SystemEntities.Add(new SystemEntitiesDTO {
                    SystemEntityID = items.SystemEntityID,
                    SystemEntityName = items.SystemEntityName
                });
            }
            return SystemEntities;
        }
        public List<SystemEntitiesDTO> GetEntitesForLock()
        {
            List<SystemEntitiesDTO> SystemEntities = new List<SystemEntitiesDTO>();

            foreach (var items in Find(x => x.CanLocked == true).ToList())
            {
                SystemEntities.Add(new SystemEntitiesDTO
                {
                    SystemEntityID = items.SystemEntityID,
                    SystemEntityName = items.SystemEntityName
                });
            }
            return SystemEntities;
        }

        public string GetEntityName(int EntityID)
        {
            return Find(x => x.SystemEntityID == EntityID).FirstOrDefault().SystemEntityName;
        }


    }
}
