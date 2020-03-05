using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.DB.Models;
using WarhouseSystem.Common.ViewModels;

namespace WarhouseSystem.Application.CategoryApp
{
   public class MappingCategory
    {
        public Category MapModelToEntity(CategoryViewModel Model, Category Entity)
        {
            Entity.Name = Model.Name;
            Entity.CategoryCount = Model.Count!=null?Model.Count :  0;
            return Entity;
        }
        public Category MapModelToEntity(CategoryViewModel Model)
        {
            return new Category()
            {
                Id = Model.Id,
                Name = Model.Name,
                CategoryCount = Model.Count,
                CreationTime = DateTime.Now
            };
        }

        public CategoryViewModel MapEntityToModel(Category Entity)
        {
            return new CategoryViewModel()
            {
                Id = Entity.Id,
                Name = Entity.Name,
                Count = Entity.CategoryCount!=null? Entity.CategoryCount : 0,
                CreationDate = Entity.CreationTime
            };
        }
    }
}
