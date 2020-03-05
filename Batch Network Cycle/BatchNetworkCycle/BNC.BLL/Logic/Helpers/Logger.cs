using System;
using System.Linq;
using BNC.Entities;
using BNC.Utilities;

namespace BNC.BLL
{
    public static class Logger
    {
    
        public static bool LogNBCEvent(int? RequestID, int LogTypeID, int UserID, string OldValue, string NewValue, string LogComment = null)
        {



            try
            {

                return true;
            }
            catch (Exception ex)
            {
                ExceptionHandlerConstants.CreatBNCException(ex, "Logger.cs", "LogNBCEvent", "NBCApplication-BLL");
                return false;
            }
        }

    }
}
