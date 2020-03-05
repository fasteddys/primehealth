using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Utilities
{

    public static class ServiceLogger
    {
        public static void LogInformation(string message)
        {
            try
            {
                message = message + " Time:" + DateTime.Now.ToString() + "\n";
                string path = AppDomain.CurrentDomain.BaseDirectory;
                String dir = Path.GetDirectoryName(path);
                var fileName = DateTime.Now.ToString("ddMMyyyy") + ".txt";
                var fullPath = dir + @"\" + fileName;
                if (File.Exists(fullPath) == false)
                {
                    var exceptionFile = File.Create(fullPath);
                    exceptionFile.Close();
                }
                using (TextWriter tw = new StreamWriter(fullPath, true))
                {
                    tw.WriteLine(message);
                    tw.Close();
                }
            }
            catch (Exception e)
            {

            }
        }
        public static int InsertServiceLog(string LogDetails)
        {
            ServiceLogger.LogInformation("Info " + LogDetails);
            return 1;
        }
        public static int InsertServiceErrorLog(string LogDetails)
        {
            ServiceLogger.LogInformation("Exception " + LogDetails);
            return 1;
        }
        public static int InsertServiceStart()
        {
            ServiceLogger.LogInformation("ServiceStart " + DateTime.Now.ToString());
            return 1;
        }
        public static int InsertServiceStop()
        {
            ServiceLogger.LogInformation("ServiceStop " + DateTime.Now.ToString());
            return 1;
        }
    }


}
