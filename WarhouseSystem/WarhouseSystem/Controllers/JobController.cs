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
    public class JobController : ApiController
    {
        ManageEJob manageJob;
       public JobController()
        {
            manageJob = new ManageEJob();

        }

        [HttpPost]
        public IHttpActionResult AddJob(JobViewModel model)
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



        public IHttpActionResult GetAllJobs()
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
        public IHttpActionResult GetJobById(long Id)
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
        public IHttpActionResult DeleteEmployee(long Id)
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
        public IHttpActionResult UpdateEmployee(long Id, JobViewModel model)
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
