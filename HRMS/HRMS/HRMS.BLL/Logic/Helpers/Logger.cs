using System;
using System.Linq;
using HRMS.Entities;
using HRMS.Utilities;

namespace HRMS.BLL
{
    public static class Logger
    {
    
        public static bool LogCypressEvent(int? RequestID, int LogTypeID, int? UserID, string OldValue, string NewValue, string LogComment = null)
        {



            try
            {
                using (HRMSEntities hRMSEntities=new HRMSEntities())
                {
                    string UserName = hRMSEntities.Users.Where(x => x.UserID == UserID).FirstOrDefault().UserName;


                    LogsDetail LogObj = new LogsDetail();
                    LogObj.RequestFK = RequestID;
                    LogObj.LogTypeFK = LogTypeID;
                    LogObj.UserFK = UserID;
                    LogObj.UserName = UserName;
                    LogObj.OldValue = OldValue;
                    LogObj.NewValue = NewValue;
                    LogObj.LogTime = DateTime.Now;
                    LogObj.Comment = LogComment;
                    hRMSEntities.LogsDetails.Add(LogObj);
                    hRMSEntities.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateCypressException(ex, "Logger.cs", "LogCypressEvent", "CypressApplication-BLL");
                return false;
            }
        }

    }
}
