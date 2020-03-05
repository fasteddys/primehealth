using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.DB.Models;
using WarhouseSystem.EF.UnitOfWork;
using WarhouseSystem.Common.Utilites;

namespace WarhouseSystem.Application.ItemApp
{
    public class ManageItem : IManageItem
    {
        private UnitOfWork unit = null;

        MappingItem map = new MappingItem();

        public ManageItem()
        {
            unit = new UnitOfWork();
        }
        public bool AddItem(ItemViewModel model)
        {
            try
            {
                Item item = map.MapModelToEntity(model);
                Utilites.CheckResult = unit.itemRepository.Add(item);
                unit.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteItem(long Id)
        {
            try
            {
                Utilites.CheckResult = unit.itemRepository.Delete(Id);
                unit.Save();
                return Utilites.CheckResult;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<ItemViewModel> GetAllItems()
        {
            IEnumerable<Item> AllItems = unit.itemRepository.GetAllData();
            IEnumerable<ItemViewModel> items = AllItems.Select(map.MapEntityToModel);
            return items;
        }

        public IEnumerable<ItemViewModel> GetAllItemsBySubCategoryId(long subCategoryId)
        {
            IEnumerable<ItemViewModel> items = unit.itemRepository.GetAllItemsBySubCategory(subCategoryId);
            return items.ToList();
        }

        public ItemViewModel GetItem(long Id)
        {
            Item item = unit.itemRepository.GetDataById(Id);
            ItemViewModel catViewModel = map.MapEntityToModel(item);
            return catViewModel;
        }

        public bool UpdateItem( ItemViewModel model)
        {
            if (model.Id == 0)
            {
                Item item = map.MapModelToEntity(model);
                Utilites.CheckResult = unit.itemRepository.Add(item);
                unit.Save();
                return Utilites.CheckResult;
            }
            else
            {
                Item item = unit.itemRepository.GetDataById(model.Id);
                map.MapModelToEntity(model, item);
                Utilites.CheckResult = unit.itemRepository.Modify((int)model.Id, item);
                unit.Save();
                return Utilites.CheckResult;
            }
        }
    }
}
