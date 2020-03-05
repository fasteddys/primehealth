using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendHappyBirthDayToEmployees
{
    class Program
    {
        // depertment Information count and users
        public static List<User> GetDeptEmpsTodayIsBirthDay(int departmentId)
        {
            using (HRMSEntities hr = new HRMSEntities())
            {
                return hr.Users.Where(us => us.DepartmentFK == departmentId && us.IsActive == true && us.UserEmail != String.Empty && us.UserEmail != null).ToList();

            }

        }
        public static int GetNumDeptEmpsTodayIsBirthDay(int departmentId)
        {
            using (HRMSEntities hr = new HRMSEntities())
            {
                return hr.Users.Where(us => us.DepartmentFK == departmentId && us.IsActive == true && us.UserEmail != String.Empty && us.UserEmail != null).Count();

            }

        }
        //---------------------------------------------------------------------------------------
        //Sub depertment Information count and users
        public static List<User> GetSubDeptEmpsTodayIsBirthDay(int subDepartmentId)
        {
            using (HRMSEntities hr = new HRMSEntities())
            {
                return hr.Users.Where(us => us.SubDepartmentFK == subDepartmentId && us.IsActive == true && us.UserEmail != String.Empty && us.UserEmail != null).ToList();

            }

        }
        public static int GetNumSubDeptEmpsTodayIsBirthDay(int subDepartmentId)
        {
            using (HRMSEntities hr = new HRMSEntities())
            {
                return hr.Users.Where(us => us.SubDepartmentFK == subDepartmentId && us.IsActive == true && us.UserEmail != String.Empty && us.UserEmail != null).Count();

            }

        }
        //---------------------------------------------------------------------------------------
        // Users Information count and users
        public static int GetEmpsCountThatActive()
        {
            using (HRMSEntities hr = new HRMSEntities())
            {
                return hr.Users.Where(u => u.IsActive == true).Count();

            }
        }
        public static List<User> GetEmpsIsTodayBirthDay(int day, int month)
        {
            using (HRMSEntities hr = new HRMSEntities())
            {
                List<User> usersThatTodayBirthDayList = new List<User>();
                var usersThatTodayBirthDay = hr.Users.Where(us => us.BirthDate.Value.Day == day && us.BirthDate.Value.Month == month && us.IsActive == true);
                foreach (var user in usersThatTodayBirthDay)
                {
                    usersThatTodayBirthDayList.Add(user);
                }
                return usersThatTodayBirthDayList;
            }
        }
        public static int GetNumEmpsIsTodayBirthDay(int day, int month)
        {
            HRMSEntities hr = new HRMSEntities();

            return hr.Users.Where(us => us.BirthDate.Value.Day == day && us.BirthDate.Value.Month == month && us.IsActive == true).Count();

        }
        //---------------------------------------------------------------------------------------
        public static void SendMailTest2(string UsersTo,string EmployeeName)
        {
            using (HRMSEntities HRMS = new HRMSEntities())
            {
                WriteToFile("Maill will sent to " + EmployeeName + " at " + DateTime.Now);
                string AutodiscoverUrl = HRMS.Configurations.Where(x => x.ConfigurationKey == "AutodiscoverUrl" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
                string MailingUserName = HRMS.Configurations.Where(x => x.ConfigurationKey == "MailingUserName" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
                string MailingPassword = HRMS.Configurations.Where(x => x.ConfigurationKey == "MailingPassword" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
                string MailingDomain = HRMS.Configurations.Where(x => x.ConfigurationKey == "MailingDomain" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
                string MailingBody = HRMS.Configurations.Where(x => x.ConfigurationKey == "BirthdayEmailTemplate" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl(AutodiscoverUrl);
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials(MailingUserName, MailingPassword, MailingDomain);
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Happy Birthday " + EmployeeName + "!";
                message.Body = MailingBody;

                if (UsersTo!="")
                {
                    message.ToRecipients.Add("Ahmed.Zakaria@Medgulf.com.eg");
                    message.CcRecipients.Add("IT-Developers@Prime-Health.org");
                }
                else
                {
                    message.ToRecipients.Add("IT-Developers@Prime-Health.org");
                }
                //message.BccRecipients.Add("IT-Developers@Prime-Health.org");

                message.Save();
                message.SendAndSaveCopy();

            }


        }
        //---------------------------------------------------------------------------------------
        public static void SendMail2(string UsersTo, string EmployeeName)
        {
            using (HRMSEntities HRMS = new HRMSEntities())
            {
                WriteToFile("Maill will sent to " + EmployeeName + " at " + DateTime.Now);
                string AutodiscoverUrl = HRMS.Configurations.Where(x => x.ConfigurationKey == "AutodiscoverUrl" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
                string MailingUserName = HRMS.Configurations.Where(x => x.ConfigurationKey == "MailingUserName" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
                string MailingPassword = HRMS.Configurations.Where(x => x.ConfigurationKey == "MailingPassword" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
                string MailingDomain = HRMS.Configurations.Where(x => x.ConfigurationKey == "MailingDomain" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
                string MailingBody = HRMS.Configurations.Where(x => x.ConfigurationKey == "BirthdayEmailTemplate" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
                string StaffEmail = HRMS.Configurations.Where(x => x.ConfigurationKey == "StaffEmail" && x.IsActive == true && x.IsDeleted == false).FirstOrDefault().ConfigurationValue;
                ExchangeService service = new ExchangeService();
                service.AutodiscoverUrl(AutodiscoverUrl);
                service.UseDefaultCredentials = false;
                service.Credentials = new WebCredentials(MailingUserName, MailingPassword, MailingDomain);
                EmailMessage message = new EmailMessage(service);
                message.Subject = "Happy Birthday " + EmployeeName + "!";
                message.Body = MailingBody;

                if (UsersTo!="")
                {
                 
                    message.ToRecipients.Add(UsersTo);
                    message.CcRecipients.Add(StaffEmail);
                }
                else
                {
                    message.ToRecipients.Add(StaffEmail);
                }
                message.BccRecipients.Add("IT-Developers@Prime-Health.org");

                message.Save();
                message.SendAndSaveCopy();
            }


        }
        //-------------------------------------------------------------------------------------------------
        public static void CheckIfMailWorkGood(int _day, int _month)
        {
            List<User> UsersTo;
            List<User> UsersCss;
            bool userHaveMail = false;
            int day = _day, month = _month;
            Console.WriteLine("Number Of Employess have BirtDay: " + GetNumEmpsIsTodayBirthDay(day, month));
            Console.WriteLine("______________________________________");
            foreach (var user in GetEmpsIsTodayBirthDay(day, month))
            {
                UsersTo = new List<User>();
                UsersCss = new List<User>();
                Console.WriteLine(user.UserName + ": " + user.UserID);
                if (user.UserEmail != String.Empty)
                {
                    UsersTo.Add(user);
                    userHaveMail = true;
                }
                if (user.SubDepartmentFK != null)
                {
                    int numOfEmpAtSDep = GetNumSubDeptEmpsTodayIsBirthDay(user.SubDepartmentFK.Value) - 1;

                    if (numOfEmpAtSDep > 0)
                    {
                        Console.WriteLine("number of Emlployee at Sub Department " + user.SubDepartmentFK.Value + ": " + numOfEmpAtSDep);

                        foreach (var userAtSubSameDepartment in GetSubDeptEmpsTodayIsBirthDay(user.SubDepartmentFK.Value))
                        {
                            if (user.UserID != userAtSubSameDepartment.UserID)
                            {
                                Console.WriteLine(userAtSubSameDepartment.UserName + ": " + userAtSubSameDepartment.UserID);
                                if (userHaveMail)
                                {
                                    UsersCss.Add(userAtSubSameDepartment);
                                }
                                else
                                {
                                    UsersTo.Add(userAtSubSameDepartment);
                                }
                            }

                        }
                        Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("number of Emlployee at Department " + user.DepartmentFK.Value + ": " + (GetNumDeptEmpsTodayIsBirthDay(user.DepartmentFK.Value) - 1));
                        foreach (var userAtSameDepartment in GetDeptEmpsTodayIsBirthDay(user.DepartmentFK.Value))
                        {

                            if (userAtSameDepartment.UserID != user.UserID)
                            {
                                Console.WriteLine(userAtSameDepartment.UserName + ": " + userAtSameDepartment.UserID);
                                if (userHaveMail)
                                {
                                    UsersCss.Add(userAtSameDepartment);
                                }
                                else
                                {
                                    UsersTo.Add(userAtSameDepartment);
                                }
                            }
                        }
                        Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("number of Emlployee at Department " + user.DepartmentFK.Value + ": " + GetNumDeptEmpsTodayIsBirthDay(user.DepartmentFK.Value));
                    foreach (var userAtSameDepartment in GetDeptEmpsTodayIsBirthDay(user.DepartmentFK.Value))
                    {
                        if (userAtSameDepartment.UserID != user.UserID)
                        {
                            Console.WriteLine(userAtSameDepartment.UserName + ": " + userAtSameDepartment.UserID);
                            if (userHaveMail)
                            {
                                UsersCss.Add(userAtSameDepartment);
                            }
                            else
                            {
                                UsersTo.Add(userAtSameDepartment);
                            }
                        }

                    }
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
                }
                Console.WriteLine("number of user at to: " + UsersTo.Count);
                Console.WriteLine("number of user at css: " + UsersCss.Count);
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------");

            }
        }
        //-------------------------------------------------------------------------------------------------
        public static void sendMaillForUserAndDepartmentOrSubDepartment()
        {
            List<string> UsersTo;
            List<string> UsersCC;
            bool userHaveMail = false;
            int day = DateTime.Now.Day, month = DateTime.Now.Month;

            foreach (var user in GetEmpsIsTodayBirthDay(day, month))
            {
                UsersTo = new List<string>();
                UsersCC = new List<string>();
                if (user.UserEmail != String.Empty && user.UserEmail != null)
                {
                    UsersTo.Add(user.UserEmail);
                    userHaveMail = true;
                }
                if (user.SubDepartmentFK != null)
                {
                    int numOfEmpAtSDep = GetNumSubDeptEmpsTodayIsBirthDay(user.SubDepartmentFK.Value) - 1;
                    if (numOfEmpAtSDep > 0)
                    {

                        foreach (var userAtSubSameDepartment in GetSubDeptEmpsTodayIsBirthDay(user.SubDepartmentFK.Value))
                        {
                            if (user.UserID != userAtSubSameDepartment.UserID)
                            {
                                if (userHaveMail)
                                {
                                    UsersCC.Add(userAtSubSameDepartment.UserEmail);
                                }
                                else
                                {
                                    UsersTo.Add(userAtSubSameDepartment.UserEmail);
                                }
                            }

                        }
                    }
                    else
                    {
                        foreach (var userAtSameDepartment in GetDeptEmpsTodayIsBirthDay(user.DepartmentFK.Value))
                        {

                            if (userAtSameDepartment.UserID != user.UserID)
                            {
                                if (userHaveMail)
                                {
                                    UsersCC.Add(userAtSameDepartment.UserEmail);
                                }
                                else
                                {
                                    UsersTo.Add(userAtSameDepartment.UserEmail);
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (var userAtSameDepartment in GetDeptEmpsTodayIsBirthDay(user.DepartmentFK.Value))
                    {
                        if (userAtSameDepartment.UserID != user.UserID)
                        {
                            if (userHaveMail)
                            {
                                UsersCC.Add(userAtSameDepartment.UserEmail);
                            }
                            else
                            {
                                UsersTo.Add(userAtSameDepartment.UserEmail);
                            }
                        }

                    }
                }
                //SendMail(UsersTo, UsersCC, user.UserName);
            }
        }
        public static void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog " + DateTime.Now.Date.ToShortDateString().Replace('/', '-') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
        static void Main(string[] args)
        {

            //CheckIfMailWorkGood(3, 7);
            string UsersTo = "";
            int day = DateTime.Now.Day, month = DateTime.Now.Month;
            WriteToFile("Service starts at " + DateTime.Now);
            foreach (var user in GetEmpsIsTodayBirthDay(day, month))
            {
                if (user.UserEmail != String.Empty && user.UserEmail != null)
                {
                    UsersTo = user.UserEmail;
                }
                try
                {
                    WriteToFile("current user is " + user.UserName);
                    SendMail2(UsersTo, user.UserName);

                    WriteToFile("Mail Sucessfuly sent to " + user.UserName+ " at "+DateTime.Now);

                }
                catch (Exception ex)
                {
                    WriteToFile("Error " + ex.Message);
                    throw;
                }

                UsersTo = "";

            }
            WriteToFile("Service Ends at " + DateTime.Now);
            WriteToFile("----------------------------------------------------");



        }
    }
}

// tempMaill
