using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HRMS.DTOs;
using HRMS.Utilities;
using HRMS.Entities;
using System.Web.Http;
using HRMS.DTOs.Business;

namespace HRMS.API.Controllers
{
    public class TimeAttendanceController : BaseController
    {

  

        //[HttpGet]
        //public ResultViewModel GetAttendance()
        // {
        //     try
        //     {
        //         var Attendance = UnitOfWork.PunchLog.GetTimeAttancdance();
        //         return new ResultViewModel()
        //         {
        //             Success = true,
        //             Message = "Ahmed Rateb Attendance",
        //             Data = Attendance
        //         };

        //     }
        //     catch (Exception ex)
        //     {
        //         ExceptionHandlerConstants.CreateException(ex, "TimeAttendanceController", "GetAttendance");
        //         return null;
        //     }
        // }
    }
}