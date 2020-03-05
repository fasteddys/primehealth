using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WarhouseSystem.Application.DepartmentApp;
using WarhouseSystem.Common.Utilites;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.Validation;

namespace WarhouseSystem.Controllers
{
    public class DepartmentController : ApiController
    {
        ManageEDepartment manageDepartment;

        public DepartmentController()
        {
            manageDepartment = new ManageEDepartment();
        }

        [HttpPost]
        public IHttpActionResult AddDepartment(DepartmentViewModel model)
        {
            try
            {
                Utilites.CheckResult = manageDepartment.AddDepartment(model);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InternalServerError, "Add New Department Is Faild"));
            }
        }

        public IHttpActionResult GetDepartmentById(long Id)
        {
            try
            {
                DepartmentViewModel model = manageDepartment.GetDepartment(Id);
                return Ok(ApiResultFactory.Success(model));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.Empty, "No Data"));
            }
        }

        [HttpGet]
        public IHttpActionResult GetAllDepartments()
        {
            try
            {
                IEnumerable<DepartmentViewModel> models = manageDepartment.GetAllDepartments();
                return Ok(ApiResultFactory.Success(models));
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
                Utilites.CheckResult = manageDepartment.DeleteDepartment(Id);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "Cannot Delete Department"));
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateEmployee(long Id, DepartmentViewModel model)
        {
            try
            {
                Utilites.CheckResult = manageDepartment.UpdateDepartment(Id, model);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "Cannot Modify Department"));
            }
        }

        public IHttpActionResult GetDepartmentsCount()
        {
            try
            {
                Utilites.Count = manageDepartment.GetDepartmentCount();
                return Ok(ApiResultFactory.Success(Utilites.Count));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.NotFound, "Not Found"));


            }
        }
    }
}
