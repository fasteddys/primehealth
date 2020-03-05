using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmaTours.Entities;
using EmaTours.Utilities;

namespace EmaTours.BLL.Logic.Helpers
{
    public static class Logger
    {
        public static bool LogCypressEvent(int? RequestID, int LogTypeID, int? UserID, string OldValue, string NewValue, string LogComment = null)
        {
            try
            {
                using (EMAToursEntities eMAToursEntities = new EMAToursEntities())
                {
                    string UserName = eMAToursEntities.Clients.Where(x => x.ClientID == UserID).FirstOrDefault().ClientName;

                    LogsDetail LogObj = new LogsDetail();
                    LogObj.RequestFK = RequestID;
                    LogObj.LogTypeFK = LogTypeID;
                    LogObj.UserFK = UserID;
                    LogObj.UserName = UserName;
                    LogObj.OldValue = OldValue;
                    LogObj.NewValue = NewValue;
                    LogObj.LogTime = DateTime.Now;
                    LogObj.Comment = LogComment;
                    eMAToursEntities.LogsDetails.Add(LogObj);
                    eMAToursEntities.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreateEmaToursException(ex, "Logger.cs", "LogCypressEvent", "CypressApplication-BLL");
                return false;
            }
        }
    }
}
