using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.DB.Models;

namespace WarhouseSystem.Application.DepartmentApp
{
   public  class MappingDepartment
    {
        public Department MapModelToEntity(DepartmentViewModel Model, Department Entity)
        {
            Entity.Name = Model.Name;
            Entity.Id = Model.Id;
            Entity.CompanyId = Model.CompanyId;
            return Entity;
        }
        public Department MapModelToEntity(DepartmentViewModel Model)
        {
            return new Department()
            {
                Id = Model.Id,
                Name = Model.Name,
                CompanyId= Model.CompanyId,
                CreationTime=DateTime.Now,
                IsActive=true,
                IsDeleted=false,
                                    
            };
        }

        public DepartmentViewModel MapEntityToModel(Department Entity)
        {
            return new DepartmentViewModel()
            {
                Id = Entity.Id,
                Name = Entity.Name,
                 CompanyName=Entity.Company.Name
            };
        }
    }
}
