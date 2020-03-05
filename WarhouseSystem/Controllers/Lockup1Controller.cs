using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WarhouseSystem.Application.CompanyApp;
using WarhouseSystem.Application.EmployeeRoleApp;
using WarhouseSystem.Application.StatusTypeApp;
using WarhouseSystem.Application.TransactionTypeApp;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.Validation;

namespace WarhouseSystem.Controllers
{
    public class Lockup1Controller : ApiController
    {
        IManageCompany manageCompany;
        IManageEmployeeRole manageEmployeeRole;
        IManageStatusType manageStatusType;
        IManageTransactionType manageTransactionType;
        public Lockup1Controller()
        {
            manageTransactionType = new ManageTransactionType();
            manageEmployeeRole = new ManageEmployeeRole();
            manageStatusType = new ManageStatusType();
            manageCompany = new ManageCompany();
        }

        [HttpGet]

        public IHttpActionResult LoadCompanies()
        {
            try
            {
                return Ok(ApiResult.Success(manageCompany.GetAllComapnies()));
            }
            catch
            {
                return Ok(ApiResult.Failure(ErrorCode.Empty, "Data is not found"));
            }
        }

        [HttpGet]

        public IHttpActionResult LoadStatusTypes()
        {
            try
            {
                return Ok(ApiResult.Success(manageStatusType.GetAllStatusTypes()));
            }
            catch
            {
                return Ok(ApiResult.Failure(ErrorCode.Empty, "Data is not found"));
            }
        }

        [HttpGet]

        public IHttpActionResult LoadEmployeeRoles()
        {
            try
            {
                return Ok(ApiResult.Success(manageEmployeeRole.GetAllEmployeeRoles()));
            }
            catch
            {
                return Ok(ApiResult.Failure(ErrorCode.Empty, "Data is not found"));
            }
        }


        [HttpGet]

        public IHttpActionResult LoadTransactionTypes()
        {
            try
            {
                return Ok(ApiResult.Success(manageTransactionType.GetAllTransactionTypes()));
            }
            catch
            {
                return Ok(ApiResult.Failure(ErrorCode.Empty, "Data is not found"));
            }
        }
    }
}