using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using EmaTours.Entities;
using static EmaTours.BLL.StaticData.StaticEnums;
using EmaTours.DTOs;
using System.Reflection;
using EmaTours.Utilities;
using EmaTours.BLL.Logic.Tables;
using EmaTours.DTOs.Business;

namespace EmaTours.API.Controllers
{
    public class NotificationController : BaseController
    {
        [HttpPost]
        public ResultViewModel GetClientNotifications(UserNotificationDTO UserNotification)
        {
 
            ResultViewModel Result = new ResultViewModel();
            Result.Success = true;
            try
            {                
                Result =new ResultViewModel
                { Data = UnitOfWork.NotificationsBLL.GetClientNotifications(UserNotification),
                 Message="Success",
                  Success=true


                };
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateException(ex, MethodBase.GetCurrentMethod().Name);
                Result.Message = ex.Message;
                Result.Success = false;

            }
            return Result;

        }

      
    }
}