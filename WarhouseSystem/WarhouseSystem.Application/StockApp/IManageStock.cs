using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;

namespace WarhouseSystem.Application.StockApp
{
    public interface IManageStock
    {
        bool AddStock(StockViewModel model);

        bool UpdateStock(long Id, StockViewModel model);

        bool DeleteStock(long Id);

        StockViewModel GetStock(long Id);

        IEnumerable<StockViewModel> GetAllStocks();
    }
}
