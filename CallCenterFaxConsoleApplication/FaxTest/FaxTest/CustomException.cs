using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;

namespace FaxTest
{
    public class CustomException : Exception
    {
        public string exceptionClass { get; set; }
        public string exceptionMethod { get; set; }
        public string e { get; set; }
        public new string InnerException { get; set; }
        public new string Source { get; set; }

        public new string StackTrace { get; set; }
        public string exceptionType { get; set; }
    }
    public static class ExceptionHandlerConstants
    {  
        public static string ExceptionAppSettingKey = "ExceptionLogStorageMedia";
        public static string TextFileStorageKey = "TextFile";
        public static string DatabaseStorageKey = "Database";
        public static string DatabaseNameKey = "LogConnection";
        public static string ExceptionLogType = "Exception";
        private static string PrepareExceptionString(CustomException e)
        {
            StringBuilder exceptionString = new StringBuilder();
            exceptionString.AppendLine("Exception :");
            exceptionString.AppendLine("---------");
            exceptionString.AppendLine(e.e.ToString());
            if (e.InnerException != null)
            {
                exceptionString.AppendLine("InnerException :");
                exceptionString.AppendLine("---------");
                exceptionString.AppendLine(e.InnerException.ToString());
            }
            if (e.Source != null)
            {
                exceptionString.AppendLine("Source :");
                exceptionString.AppendLine("---------");
                exceptionString.AppendLine(e.Source.ToString());
            }
            if (e.StackTrace != null)
            {
                exceptionString.AppendLine("StackTrace :");
                exceptionString.AppendLine("---------");
                exceptionString.AppendLine(e.StackTrace.ToString());
                exceptionString.AppendLine("__________________________________");
            }
            return exceptionString.ToString();
        }
        public static CustomException CreateCustomException(string className, string methodName, Exception e)
        {
            CustomException customEx = new CustomException();

            customEx.exceptionClass = className;
            customEx.exceptionMethod = methodName;
            customEx.e = e.ToString();
            customEx.Source = e.Source;
            customEx.StackTrace = e.StackTrace;
            customEx.exceptionType = e.GetType().Name.ToString();
            return customEx;
        }
        public static void CreateException(Exception e, string methodname)
        {
            CustomException ex = CreateCustomException("ReportAgent", methodname, e);
            var connectionstring = ConfigurationManager.AppSettings["SaeedDBConnection"].ToString();

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                string query = "INSERT INTO [dbo].[SaaedLog] (LogDetails, LogType,ClassName,MethodName) VALUES (@LogDetails, @LogType,@ClassName,@MethodName) ";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@LogDetails", SqlDbType.VarChar).Value = PrepareExceptionString(ex);
                cmd.Parameters.Add("@LogType", SqlDbType.VarChar, 200).Value = ExceptionHandlerConstants.ExceptionLogType;
                cmd.Parameters.Add("@ClassName", SqlDbType.VarChar).Value = ex.exceptionClass;
                cmd.Parameters.Add("@MethodName", SqlDbType.VarChar, 200).Value = ex.exceptionMethod;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }
        public static void FaxException(Exception e, string className, string methodname,string ErrorSource)
        {
            
            try
            {
                CustomException ex = CreateCustomException(className, methodname, e);
                var connectionstring = ConfigurationManager.AppSettings["FaxApprovalConnectionString"].ToString();
                Console.WriteLine(connectionstring);
                using (SqlConnection con = new SqlConnection(connectionstring))
                {

                    Console.WriteLine("FailedTransactions class is run"); 
                    string query = "INSERT INTO [dbo].[FaildTransactions] (LogDetails, LogType,ClassName,MethodName,LogDate,ErrorSource) VALUES (@LogDetails, @LogType,@ClassName,@MethodName,@LogDate,@ErrorSource)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.Add("@LogDetails", SqlDbType.VarChar).Value = PrepareExceptionString(ex);
                    cmd.Parameters.Add("@LogType", SqlDbType.VarChar, 200).Value = ExceptionHandlerConstants.ExceptionLogType;
                    cmd.Parameters.Add("@ClassName", SqlDbType.VarChar).Value = ex.exceptionClass;
                    cmd.Parameters.Add("@MethodName", SqlDbType.VarChar, 200).Value = ex.exceptionMethod;
                    cmd.Parameters.Add("@ErrorSource", SqlDbType.VarChar, 200).Value = ErrorSource;
                    cmd.Parameters.Add("@LogDate", SqlDbType.DateTime, 200).Value = DateTime.Now;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            catch (Exception exx)
            {
                manipulationFile.WriteExceptionToFile("FaxException "+exx.Message);

            }


        }

    }


}
