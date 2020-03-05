using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.EF.UnitOfWork;
using WarhouseSystem.DB.Models;
using WarhouseSystem.Common.Utilites;

namespace WarhouseSystem.Application.CategoryApp
{
    public class ManageItemAttachment : IManageItemAttachment
    {
        private UnitOfWork unit = null;

        MappingItemAttachment map = new MappingItemAttachment();

        public ManageItemAttachment()
        {
            unit = new UnitOfWork();
        }
       

        public bool AddItemAttachment(ItemAttachmentModel model)
        {
            try
            {
                ItemAttachment ItemAttachment = map.MapModelToEntity(model);
                Utilites.CheckResult = unit.itemAttachmentRepository.Add(ItemAttachment);
                unit.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<ItemAttachmentModel> GetAllItemAttachment(int ItemId)
        {
            IEnumerable<ItemAttachment> AllCategories = unit.itemAttachmentRepository.GetAllItemAttachmentsByItemId(ItemId);
            IEnumerable<ItemAttachmentModel> items = AllCategories.Select(map.MapEntityToModel);
            return items;
        }

    }
}
