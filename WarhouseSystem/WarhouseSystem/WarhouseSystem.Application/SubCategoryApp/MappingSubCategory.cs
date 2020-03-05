using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.DB.Models;
using WarhouseSystem.Common.ViewModels;

namespace WarhouseSystem.Application.SubCategoryApp
{
    public class MappingSubCategory
    {
        public SubCategory MapModelToEntity(SubCategoryViewModel Model, SubCategory Entity)
        {
            Entity.Name = Model.Name;
            Entity.SubCategoryCount = Model.Count;
            Entity.Price = Model.Price;
            Entity.CategoryId = Model.CategoryId;
            return Entity;
        }
        public SubCategory MapModelToEntity(SubCategoryViewModel Model)
        {
            return new SubCategory()
            {
                Id = Model.Id,
                Name = Model.Name,
                Price = Model.Price,
                CategoryId = Model.CategoryId,
                SubCategoryCount = Model.Count,
                CreationTime = DateTime.Now
            };
        }

        public SubCategoryViewModel MapEntityToModel(SubCategory Entity)
        {
            return new SubCategoryViewModel()
            {
                Id = Entity.Id,
                Name = Entity.Name,
                Price = Entity.Price,
                CategoryId = Entity.CategoryId,
                CategoryName = Entity.Category.Name,
                Count = Entity.SubCategoryCount,
                CreationDate = Entity.CreationTime
            };
        }
    }
}
