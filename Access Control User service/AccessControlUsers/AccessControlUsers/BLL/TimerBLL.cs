using AccessControlUsers.DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AccessControlAttendance.BLL
{
  public  class TimerBLL
    {
        //private System.Timers.Timer Timer;
        //public void SetTimer()
        //{
   
        //    // Create a timer with a two second interval.
        //    Timer = new System.Timers.Timer(43200000);//12 Hours
        //    // Hook up the Elapsed event for the timer. 
        //    Timer.Elapsed += OnTimedEvent;
        //    Timer.AutoReset = true;
        //    Timer.Enabled = true;
        //}
        //public void OnTimedEvent(Object source, ElapsedEventArgs e)
        //{
        //    //her is the code 
        //    AttendanceBLL Attendance = new AttendanceBLL();
        //    Attendance.InsertUsersInHRMS(Attendance.GetUsers());
        //}

        public async Task ExecutAsync()
        {
            AttendanceBLL Attendance = new AttendanceBLL();
            Attendance.InsertUsersInHRMS(Attendance.GetUsers());
            int Time = 0;
            using (HRMSEntities HRMS = new HRMSEntities())
            {
                 Time = Convert.ToInt32(HRMS.Configurations.Where(x => x.ConfigurationKey == "DelayGetUser").FirstOrDefault().ConfigurationValue);
            }
          await  Task.Delay(Time);
            await ExecutAsync();
        }

    }
}
