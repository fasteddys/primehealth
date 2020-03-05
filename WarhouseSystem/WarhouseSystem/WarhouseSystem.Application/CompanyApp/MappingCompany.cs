using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.DB.Models;
namespace WarhouseSystem.Application.CompanyApp
{
    public class MappingCompany
    {
        public Company MapModelToEntity(LockupViewModel Model, Company Entity)
        {
            Entity.Name = Model.Name;
            return Entity;
        }
        public Company MapModelToEntity(LockupViewModel Model)
        {
            return new Company()
            {
                Id = Model.Id,
                Name = Model.Name
            };
        }

        public LockupViewModel MapEntityToModel(Company Entity)
        {
            return new LockupViewModel()
            {
                Id = Entity.Id,
                Name = Entity.Name
            };
        }
    }
}
