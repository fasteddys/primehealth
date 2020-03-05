using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.DB.Models;
using WarhouseSystem.Common.ViewModels;

namespace WarhouseSystem.Application.StatusTypeApp
{
    public class MappingStatusType
    {
        public StatusType MapModelToEntity(LockupViewModel Model, StatusType Entity)
        {
            Entity.Name = Model.Name;
            return Entity;
        }
        public StatusType MapModelToEntity(LockupViewModel Model)
        {
            return new StatusType()
            {
                Id = Model.Id,
                Name = Model.Name
            };
        }

        public LockupViewModel MapEntityToModel(StatusType Entity)
        {
            return new LockupViewModel()
            {
                Id = Entity.Id,
                Name = Entity.Name
            };
        }
    }
}
