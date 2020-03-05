using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Diagnostics; // Can remove this reference if you remove all the Debug.Writeline entries
using System.Timers;
using DAL.CRUD;
using DAL.DataBase;
using Harkos;

namespace TickitingSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
      static  User_DAL userDAL = new User_DAL();
        static Deparments_DAL departmentDAL = new Deparments_DAL();

        private static double TimerIntervalInMilliseconds = Convert.ToDouble(WebConfigurationManager.AppSettings["TimerIntervalInMilliseconds"]);
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Timer timer = new Timer(TimerIntervalInMilliseconds);
            timer.Enabled = true;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new OutputCacheAttribute
            {
                VaryByParam = "*",
                Duration = 0,
                NoStore = true,
            });
            // the rest of your global filters here
        }




        // Added the following procedure:
        static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DateTime MyScheduledRunTime = DateTime.Parse(WebConfigurationManager.AppSettings["TimerStartTime"]);
            DateTime CurrentSystemTime = DateTime.Now;
            DateTime LatestRunTime = MyScheduledRunTime.AddMilliseconds(TimerIntervalInMilliseconds);
            if ((CurrentSystemTime.CompareTo(MyScheduledRunTime) >= 0) && (CurrentSystemTime.CompareTo(LatestRunTime) <= 0))
            {
                // RUN YOUR PROCESSES HERE
                List<UserLDAP> users = new List<UserLDAP>();
                users= getall.GetADUsers();

                foreach (var item in users)
                {


                    if (userDAL.UserExist(item) == true)
                    {
                        continue;


                    }
                    else
                    {
                        if (departmentDAL.EepartmentExist(item.Department) == null)
                        {
                            Department newdepartment = new Department();
                            newdepartment.Dept_Name = item.Department;
                            departmentDAL.ADD(newdepartment);
                            User newuser = new User();
                            newuser.User_Name = item.Name;
                            newuser.E_mail = item.Email;
                            newuser.Dept_ID = newdepartment.Dept_Id;
                            newuser.User_Status = "active";
                            userDAL.ADD(newuser);


                        }
                        else
                        {
                            Department department = new Department();
                            department = departmentDAL.EepartmentExist(item.Department);

                            User user = new User();
                            user.User_Name = item.Name;
                            user.E_mail = item.Email;
                            user.Dept_ID = department.Dept_Id;
                            user.User_Status = "active";

                            userDAL.ADD(user);
                        }
                    }


                }
































            }
        }
        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }
    }
}
