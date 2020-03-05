using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmaTours.Entities;

namespace EmaTours.BLL.DbObjects
{
   public static class Helpers
    {
        public static bool LogEmaToursEvent(int LogTypeID, int RequestFK, int UserFK, string UserName, string OldValue, string NewValue, string LogComment = null)
        {

            return true;
        }

    }

}
