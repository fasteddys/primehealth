using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.DB.Models;

namespace WarhouseSystem.Application.EmployeeApp
{
   public  class MappingEmployee
    {
        public Employee MapModelToEntity(EmployeeViewModel Model, Employee Entity)
        {
            Entity.Name = Model.Name;
            Entity.Email = Model.Email;
            Entity.EmployeeRoleId = Model.EmployeeRoleId;
            Entity.HireDate = Model.HireDate;
            Entity.IsDeleted = Model.IsDeleted;
            return Entity;
        }
        public Employee MapModelToEntity(EmployeeViewModel Model)
        {
            return new Employee()
            {
                Id = Model.Id,
                Name = Model.Name,
                CreationTime = DateTime.Now,
                IsActive = true,
                IsDeleted = false,
                EmployeeRoleId = Model.EmployeeRoleId,
                Email = Model.Email,
                //Password = Model.Password,
                HireDate = Model.HireDate,
            };
        }

        public EmployeeViewModel MapEntityToModel(Employee Entity)
        {
            return new EmployeeViewModel()
            {
                Id = Entity.Id,
                Name = Entity.Name,
                CreationDate = Entity.CreationTime,
                Email = Entity.Email,
                //Password = Entity.Password,
                HireDate = Entity.HireDate,
                IsDeleted = Entity.IsDeleted,
                EmployeeRoleId = Entity.EmployeeRoleId,
                EmployeeRoleName = Entity.EmployeeRole.Name,
                
            };
        }
    }
}
