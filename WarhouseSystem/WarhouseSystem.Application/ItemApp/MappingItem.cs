using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.DB.Models;

namespace WarhouseSystem.Application.ItemApp
{
   public class MappingItem
    {

        public Item MapModelToEntity(ItemViewModel Model, Item Entity)
        {
            Entity.BarCode = Model.BarCode;
            Entity.StockId = Model.StockId;
            Entity.SubCategoryId = Model.SubCategoryId;
            Entity.OtherDevicePlaceId = Model.OtherDevicePlaceId;
            Entity.StatusTypeId = Model.StatusTypeId;
            return Entity;
        }
        public Item MapModelToEntity(ItemViewModel Model)
        {
            return new Item()
            {
                Id = (int)Model.Id,
                BarCode = Model.BarCode,
                StockId = Model.StockId,
                SubCategoryId = Model.SubCategoryId,
                StatusTypeId = Model.StatusTypeId,
                OtherDevicePlaceId = Model.OtherDevicePlaceId,
                CreationTime = DateTime.Now
            };
        }

        public ItemViewModel MapEntityToModel(Item Entity)
        {
            return new ItemViewModel()
            {
                Id = Entity.Id,
                BarCode = Entity.BarCode,
                StockId = Entity.StockId,
                SubCategoryId = Entity.SubCategoryId,
                SubCategoryName = Entity.SubCategory.Name,
                StatusTypeId = Entity.StatusTypeId,
                StatusTypeName = Entity.StatusType.Name,
                OtherDevicePlaceId = Entity.OtherDevicePlaceId,
                CreationDate = Entity.CreationTime
            };
        }
    }
}
