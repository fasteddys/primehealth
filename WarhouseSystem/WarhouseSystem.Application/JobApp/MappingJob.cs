using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.DB.Models;

namespace WarhouseSystem.Application.StockApp
{
   public  class MappingStock
    {
        public Stock MapModelToEntity(StockViewModel Model, Stock Entity)
        {
            Entity.Name = Model.Name;
            Entity.Id = Model.Id;
            return Entity;
        }
        public Stock MapModelToEntity(StockViewModel Model)
        {
            return new Stock()
            {
                Id = Model.Id,
                Name = Model.Name,
                CreationTime = DateTime.Now,
                IsActive = true,
                IsDeleted = false,
                DepartmentId = Model.DepartmentId
            };
        }
        public StockViewModel MapEntityToModel(Stock Entity)
        {
            return new StockViewModel()
            {
                Id = Entity.Id,
                Name = Entity.Name,
                DepartmentName = Entity.Department.Name
            };
        }
    }
}
