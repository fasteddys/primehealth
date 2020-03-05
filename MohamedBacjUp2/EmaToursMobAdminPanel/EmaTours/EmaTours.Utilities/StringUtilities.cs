using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.Utilities
{
   public static class StringUtilities
    {
        public static string NormalizeLanguageString(string str)
        {
            try
            {
                string result = string.Empty;
                result = str.ToLower();
                if (result != "ar" && result != "en")
                {
                    result = "ar"; //default Language if not sent or if there is error
                }
                return result;
            }
            catch (Exception ex)
            {
                return "ar";
            }
        }
    }
}
