using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HRMS.Utilities
{
   public static class  GetDateFromString
    {
        public static string getDateFromStr(string str)
        {
            string s = str;
            Regex rx = new Regex(@"(?<day>\d{2})\/(?<month>\d{2})\/(?<year>\d{4})");
            Match m = rx.Match(s);
            if (m.Success)
            {
                return m.Value;
            }
            else
            {
                return "";
            }
        }
    }
}
