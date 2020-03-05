using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.DB.Models;
using WarhouseSystem.Common.ViewModels;

namespace WarhouseSystem.Application.EmployeeRoleApp
{
    public class MappingEmployeeRole
    {
        public EmployeeRole MapModelToEntity(LockupViewModel Model, EmployeeRole Entity)
        {
            Entity.Name = Model.Name;
            return Entity;
        }
        public EmployeeRole MapModelToEntity(LockupViewModel Model)
        {
            return new EmployeeRole()
            {
                Id = Model.Id,
                Name = Model.Name
            };
        }

        public LockupViewModel MapEntityToModel(EmployeeRole Entity)
        {
            return new LockupViewModel()
            {
                Id = Entity.Id,
                Name = Entity.Name
            };
        }
    }
}
