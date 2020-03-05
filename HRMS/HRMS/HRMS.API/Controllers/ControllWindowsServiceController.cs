using HRMS.BLL.UnitOfWork;
using HRMS.DTOs;
using HRMS.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Web;
using System.Web.Http;

namespace HRMS.API.Controllers
{
    public class ControllWindowsServiceController : BaseController
    {

        [HttpGet]
        public ResultViewModel StartService(string ServiceName)
        {

            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                Start( ServiceName);

                return new ResultViewModel
                {
                    Data = ServiceStatus().ToString(),
                    Message = "Success",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false
                };
            }
        }

        [HttpGet]
        public ResultViewModel StopService(string ServiceName)
        {
            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {
                Stop(ServiceName);
                return new ResultViewModel
                {
                    Data = ServiceStatus().ToString(),
                    Message = "Success",
                    Success = true
                };

            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false
                };
            }
        }

        [HttpPost]
        public void Start(string ServiceName)
        {
            try
            {
               


                ServiceController sc = new ServiceController(ServiceName);
                if (sc.Status != ServiceControllerStatus.Running && sc.Status != ServiceControllerStatus.StartPending)
                {
                    var process = new Process();
                    process.StartInfo.FileName = "net";
                    process.StartInfo.Arguments = "start " + ServiceName;
                    process.StartInfo.Verb = "runas";//run as administrator
                    process.Start();
                    process.WaitForExit();
                    //sc.Start();
                    //sc.WaitForStatus(ServiceControllerStatus.Running);
                }
            }
            catch (Exception ex){
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

            }
        }

        public void Stop(string ServiceName)
        {
            try
            {
                ServiceController sc = new ServiceController(ServiceName);
                if (sc.Status == ServiceControllerStatus.Running)
                {
                    var process = new Process();
                    process.StartInfo.FileName = "net";
                    process.StartInfo.Arguments = "stop " + ServiceName;
                    process.StartInfo.Verb = "runas";//run as administrator
                    process.Start();
                    process.WaitForExit();
                    //sc.Stop();
                    //sc.WaitForStatus(ServiceControllerStatus.Stopped);
                }
            }
            catch (Exception ex) {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");



            }
        }



 
        public ServiceControllerStatus ServiceStatus()
        {
            ServiceController sc = new ServiceController("AccessControlService");
            return sc.Status;
        }


        [HttpPost]

        public ResultViewModel GEtAllServices()
        {
            try
            {

                var Services = UnitOfWork.ServiceBLL.GetServices();

                foreach (var item in Services)
                {
                    ServiceController sc = new ServiceController(item.ServiceName);
                    item.ServiceStatus = sc.Status.ToString();
                    item.ServiceStatusID = (int)sc.Status;
                }
                return new ResultViewModel
                {
                    Data = Services,
                    Message = "Success",
                    Success = true

                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, "CypressApplication-API");

                return new ResultViewModel
                {
                    Data = null,
                    Message = ex.Message,
                    Success = false

                };
            }
        }
    }
}