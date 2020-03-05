using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common;
using WarhouseSystem.Common.ViewModels;

namespace WarhouseSystem.Application.ItemApp
{
    public interface IManageItem
    {
        bool AddItem(ItemViewModel model);

        bool UpdateItem( ItemViewModel model);

        bool DeleteItem(long Id);

        ItemViewModel GetItem(long Id);

        IEnumerable<ItemViewModel> GetAllItems();

        IEnumerable<ItemViewModel> GetAllItemsBySubCategoryId(long subCategoryId);
    }
}
