using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterProviderApprovals.Helper
{
    public class Helpers
    {
        public bool LogEmailApprovalEvent(int? RequestID, int LogTypeID, int UserID, string UserName, string OldValue, string NewValue, string LogComment = null)
        {

            try
            {
                using (PhNetworkEntities Db = new PhNetworkEntities())
                {
                    EmailApprovalLogsDetail LogObj = new EmailApprovalLogsDetail();

                    LogObj.RequestID = RequestID;
                    LogObj.LogTypeID = LogTypeID;
                    LogObj.UserID = UserID;
                    LogObj.UserName = UserName;
                    LogObj.OldValue = OldValue;
                    LogObj.NewValue = NewValue;
                    LogObj.LogTime = DateTime.Now;
                    LogObj.Comment = LogComment;
                    Db.EmailApprovalLogsDetails.Add(LogObj);
                    Db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }



    }
}