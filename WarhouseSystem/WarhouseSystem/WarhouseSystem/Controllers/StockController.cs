using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WarhouseSystem.Application.StockApp;
using WarhouseSystem.Common.Utilites;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.Validation;

namespace WarhouseSystem.Controllers
{
    public class StockController : ApiController
    {
        ManageEStock manageStock;

        public StockController()
        {
            manageStock = new ManageEStock();

        }
        [HttpPost]
        public IHttpActionResult AddStock(StockViewModel model)
        {
            try
            {
                Utilites.CheckResult = manageStock.AddStock(model);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InternalServerError, "Add New Stock Is Faild"));
            }
        }

        public IHttpActionResult GetAllStock()
        {
            try
            {
                IEnumerable<StockViewModel> models = manageStock.GetAllStocks();
                return Ok(ApiResultFactory.Success(models));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.Empty, "No Data"));
            }
        }
        
        [HttpGet]
        public IHttpActionResult GetStockById(long Id)
        {
            try
            {
                StockViewModel model = manageStock.GetStock(Id);
                return Ok(ApiResultFactory.Success(model));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.Empty, "No Data"));
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteEmployee(long Id)
        {
            try
            {
                Utilites.CheckResult = manageStock.DeleteStock(Id);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "Cannot Delete Stock"));
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateEmployee(long Id, StockViewModel model)
        {
            try
            {
                Utilites.CheckResult = manageStock.UpdateStock(Id, model);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "Cannot Modify Stock"));
            }
        }

        public IHttpActionResult GetStocksCount()
        {
            try
            {
                Utilites.Count = manageStock.GetStockCount();
                return Ok(ApiResultFactory.Success(Utilites.Count));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.NotFound, "Not Found"));


            }
        }

    }
}
