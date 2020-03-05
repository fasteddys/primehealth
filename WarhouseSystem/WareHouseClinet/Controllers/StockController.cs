using Microsoft.AspNetCore.Mvc;
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
    public class StockController : ControllerBase
    {
        ManageEStock manageStock;

        public StockController()
        {
            manageStock = new ManageEStock();

        }
        [HttpPost]
        public IActionResult AddStock(StockViewModel model)
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

        public IActionResult GetAllStock()
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
        public IActionResult GetStockById(long Id)
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
        public IActionResult DeleteEmployee(long Id)
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
        public IActionResult UpdateEmployee(long Id, StockViewModel model)
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



    }
}
