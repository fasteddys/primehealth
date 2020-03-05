using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.Common.Utilites;
using WarhouseSystem.EF.UnitOfWork;
using WarhouseSystem.DB.Models;

namespace WarhouseSystem.Application.StockApp
{
    public class ManageEStock : IManageStock
    {
        private UnitOfWork unit = null;

        MappingStock map = new MappingStock();

        public ManageEStock()
        {
            unit = new UnitOfWork();
        }
        public bool AddStock(StockViewModel model)
        {
            try
            {
                Stock Stock = map.MapModelToEntity(model);
                Utilites.CheckResult = unit.stockRepository.Add(Stock);
                unit.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateStock(long Id, StockViewModel model)
        {
            if (Id == 0)
            {
                Stock Stock = map.MapModelToEntity(model);
                Utilites.CheckResult = unit.stockRepository.Add(Stock);
                unit.Save();
                return Utilites.CheckResult;
            }
            else
            {
                Stock Stock = unit.stockRepository.GetDataById(Id);
                map.MapModelToEntity(model, Stock);
                Utilites.CheckResult = unit.stockRepository.Modify(model.Id, Stock);
                unit.Save();
                return Utilites.CheckResult;
            }
        }

        public bool DeleteStock(long Id)
        {
            try
            {
                Utilites.CheckResult = unit.stockRepository.Delete(Id);
                unit.Save();
                return Utilites.CheckResult;
            }
            catch
            {
                return false;
            }
        }

        public StockViewModel GetStock(long Id)
        {
            Stock Stock = unit.stockRepository.GetDataById(Id);
            StockViewModel StockViewModel = map.MapEntityToModel(Stock);
            return StockViewModel;
        }

        public IEnumerable<StockViewModel> GetAllStocks()
        {
            IEnumerable<Stock> AllStocks = unit.stockRepository.GetAllData();
            IEnumerable<StockViewModel> items = AllStocks.Select(map.MapEntityToModel);
            return items;
        }
    }
}
