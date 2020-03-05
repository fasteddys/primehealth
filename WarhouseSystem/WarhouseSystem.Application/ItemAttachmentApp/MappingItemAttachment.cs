using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.DB.Models;
using WarhouseSystem.Common.ViewModels;

namespace WarhouseSystem.Application.CategoryApp
{
   public class MappingItemAttachment
    {
        public ItemAttachment MapModelToEntity(ItemAttachmentModel Model, ItemAttachment Entity)
        {
            Entity.Name = Model.Name;
            Entity.AttachmentName = Model.AttachmentName;
            Entity.Id = Model.Id;
            Entity.ItemId = Model.ItemId;
            Entity.Path = Model.Path;
            return Entity;
        }
        public ItemAttachment MapModelToEntity(ItemAttachmentModel Model)
        {
            return new ItemAttachment()
            {
                Id = Model.Id,
                Name = Model.Name,
                AttachmentName = Model.AttachmentName,
                ItemId = Model.ItemId,
                Path = Model.Path,
                CreationTime = DateTime.Now
            };
        }

        public ItemAttachmentModel MapEntityToModel(ItemAttachment Entity)
        {
            return new ItemAttachmentModel()
            {
                Id = Entity.Id,
                Name = Entity.Name,
                AttachmentName = Entity.AttachmentName,
                ItemId = Entity.ItemId,
                Path = Entity.Path,
            };
        }
    }
}
