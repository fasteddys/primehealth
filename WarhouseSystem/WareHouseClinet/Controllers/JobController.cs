using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WarhouseSystem.Application.JobApp;
using WarhouseSystem.Common.Utilites;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.Validation;

namespace WarhouseSystem.Controllers
{
    public class JobController : ControllerBase
    {
        ManageEJob manageJob;
       public JobController()
        {
            manageJob = new ManageEJob();

        }

        [HttpPost]
        public IActionResult AddJob(JobViewModel model)
        {
            try
            {
                Utilites.CheckResult = manageJob.AddJob(model);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InternalServerError, "Registeration Is Faild"));
            }
        }



        public IActionResult GetAllJobs()
        {
            try
            {
                IEnumerable<JobViewModel> models = manageJob.GetAllJobs();
                return Ok(ApiResultFactory.Success(models));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.Empty, "No Data"));
            }
        }
        [HttpGet]
        public IActionResult GetJobById(long Id)
        {
            try
            {
                JobViewModel model = manageJob.GetJob(Id);
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
                Utilites.CheckResult = manageJob.DeleteJob(Id);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "Cannot Delete Job"));
            }
        }
        [HttpPut]
        public IActionResult UpdateEmployee(long Id, JobViewModel model)
        {
            try
            {
                Utilites.CheckResult = manageJob.UpdateJob(Id, model);
                return Ok(ApiResultFactory.Success(Utilites.CheckResult));
            }
            catch
            {
                return Ok(ApiResultFactory.Failure(ErrorCode.InvalidOperation, "Cannot Modify Job"));
            }
        }



    }
}
