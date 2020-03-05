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

namespace WarhouseSystem.Controllers
{
    public class ProcessTransactionController : ApiController
    {
        IManageProcessTransaction manageProcessTransaction;

        public ProcessTransactionController()
        {
            manageProcessTransaction = new ManageProcessTransaction();
        }

        [HttpPost]
        public IHttpActionResult AddProcessTransaction(ProcessTransactionViewModel model)
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
        public IHttpActionResult GetAllProcessTransactions(DateTime fromDate,DateTime toDate)
        {
            try
            {
                 fromDate =Convert.ToDateTime(Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd 00:00:00"));
                 toDate = Convert.ToDateTime( Convert.ToDateTime(toDate).ToString("yyyy-MM-dd 23:59:59"));
                IEnumerable<ProcessTransactionViewModel> models = manageProcessTransaction.GetAllProcessTransactions(fromDate,toDate);
                return Ok(ApiResultFactory.Success(models));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.Empty, "No Data"));
            }
        }

        [HttpGet]
        public IHttpActionResult GetProcessTransactionById(long Id)
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
        public IHttpActionResult DeleteProcessTransaction(long Id)
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
        public IHttpActionResult UpdateEmployee(long Id, ProcessTransactionViewModel model)
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
