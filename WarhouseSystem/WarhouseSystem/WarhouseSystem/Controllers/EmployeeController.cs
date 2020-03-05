using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WarhouseSystem.Application.CompanyApp;
using WarhouseSystem.Application.EmployeeApp;
using WarhouseSystem.Application.StatusTypeApp;
using WarhouseSystem.Application.TransactionTypeApp;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.Common.Utilites;
using WarhouseSystem.Validation;

namespace WarhouseSystem.Controllers
{
    public class EmployeeController : ApiController
    {
        IManageEmployee manageEmployee;
        public EmployeeController()
        {
            manageEmployee = new ManageEmployee();
        }

        [HttpPost]
        public IHttpActionResult Login(EmployeeViewModel model)
        {
            try
            {
                long empId = 0;
                Utilites.CheckResult = manageEmployee.Login(model,out empId);
                Tuple<bool, long> customData = new Tuple<bool, long>(Utilites.CheckResult, empId);
                return Ok(ApiResultFactory.Success(customData));
            }
            catch(Exception ex)
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InternalServerError, "Login Is Faild"));
            }
        }

        [HttpPost]
        public IHttpActionResult Register(EmployeeViewModel model)
        {
            try
            {
                Utilites.CheckResult = manageEmployee.AddEmployee(model);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InternalServerError, "Registeration Is Faild"));
            }
        }

        [HttpGet]
        public IHttpActionResult GetAllEmployees()
        {
            try
            {
                IEnumerable<EmployeeViewModel> models = manageEmployee.GetAllEmployees();
                return Ok(ApiResultFactory.Success(models));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.Empty, "No Data"));
            }
        }

        [HttpGet]
        public IHttpActionResult GetEmployeeById(long Id)
        {
            try
            {
                EmployeeViewModel model = manageEmployee.GetEmployee(Id);
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
                Utilites.CheckResult = manageEmployee.DeleteEmployee(Id);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "Cannot Delete Employee"));
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateEmployee(long Id,EmployeeViewModel model)
        {
            try
            {
                Utilites.CheckResult = manageEmployee.UpdateEmployee(Id,model);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "Cannot Modify Employee"));


            }
        }

        public IHttpActionResult GetEmployeesCount()
        {
            try
            {
                Utilites.Count = manageEmployee.GetEmployeesCount();
                return Ok(ApiResultFactory.Success(Utilites.Count));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.NotFound, "Not Found"));


            }
        }

    }
}
