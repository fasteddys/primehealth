using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.DB.Models;
using WarhouseSystem.EF.GenericRepository;

namespace WarhouseSystem.EF
{
   public class ItemRepository : GenericRepositry<Item>
    {
        public DB.Models.DB _dbContext = null;
        public ItemRepository(DB.Models.DB dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ItemViewModel> GetAllItemsBySubCategory(long subCategoryId)
        {
            List<ItemViewModel> items = (from it in _dbContext.Items
                                         where it.SubCategoryId == subCategoryId
                                         select new ItemViewModel
                                         {
                                             Id = it.Id,
                                             BarCode = it.BarCode,
                                             SubCategoryId = it.SubCategoryId,
                                             SubCategoryName = it.SubCategory.Name,
                                             StatusTypeName = it.StatusType.Name,
                                             StockName = it.Stock.Name,
                                             StatusTypeId = it.StatusTypeId,
                                             OtherDevicePlaceId = it.OtherDevicePlaceId,
                                             OtherDevicePlaceName = null,
                                             CreationDate = it.CreationTime,
                                             StockId = it.StockId
                                         }).ToList();
            return items;
        }
    }
}
