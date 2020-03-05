using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.Common.Utilites;
using WarhouseSystem.Validation;
using WarhouseSystem.Application.ProcessTransactionApp;
using Microsoft.AspNetCore.Mvc;

namespace WarhouseSystem.Controllers
{
    public class ProcessTransactionController : ControllerBase
    {
        IManageProcessTransaction manageProcessTransaction;

        public ProcessTransactionController()
        {
            manageProcessTransaction = new ManageProcessTransaction();
        }

        [HttpPost]
        public IActionResult AddProcessTransaction(ProcessTransactionViewModel model)
        {
            try
            {
                Utilites.CheckResult = manageProcessTransaction.AddProcessTransaction(model);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InternalServerError, "Add Process Transaction Is Faild"));
            }
        }

        [HttpGet]
        public IActionResult GetAllProcessTransactions()
        {
            try
            {
                IEnumerable<ProcessTransactionViewModel> models = manageProcessTransaction.GetAllProcessTransactions();
                return Ok(ApiResultFactory.Success(models));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.Empty, "No Data"));
            }
        }

        [HttpGet]
        public IActionResult GetProcessTransactionById(long Id)
        {
            try
            {
                ProcessTransactionViewModel model = manageProcessTransaction.GetProcessTransaction(Id);
                return Ok(ApiResultFactory.Success(model));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.Empty, "No Data"));
            }
        }

        [HttpDelete]
        public IActionResult DeleteProcessTransaction(long Id)
        {
            try
            {
                Utilites.CheckResult = manageProcessTransaction.DeleteProcessTransaction(Id);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "Cannot Delete Process Transaction"));
            }
        }

        [HttpPut]
        public IActionResult UpdateEmployee(long Id, ProcessTransactionViewModel model)
        {
            try
            {
                Utilites.CheckResult = manageProcessTransaction.UpdateProcessTransaction(Id, model);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "Cannot Modify Process Transaction"));
            }
        }
    }
}
