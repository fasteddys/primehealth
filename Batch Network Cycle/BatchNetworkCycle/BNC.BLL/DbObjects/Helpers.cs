using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BNC.Entities;

namespace BNC.BLL.DbObjects
{
   public static class Helpers
    {
        public static bool LogNBCEvent(int LogTypeID, int RequestFK, int UserFK, string UserName, string OldValue, string NewValue, string LogComment = null)
        {

            try
            {
                using (BNCEntities Db = new BNCEntities())
                {
                    //LogsDetail log = new LogsDetail();
                    //log.UserName = UserName;
                    //log.Comment = LogComment;
                    //log.OldValue = OldValue;
                    //log.NewValue = NewValue;
                    //log.LogTime = DateTime.Now;
                    //log.UserFK = UserFK;
                    //log.RequestFK = RequestFK;
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
