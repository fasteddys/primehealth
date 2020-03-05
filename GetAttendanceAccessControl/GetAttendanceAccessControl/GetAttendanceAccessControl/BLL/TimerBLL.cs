using GetAttendanceAccessControl.DLL;
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
      //  private  System.Timers.Timer Timer;
        //public  void SetTimer()
        //{
        //    // Create a timer with a two second interval.
        //    Timer = new System.Timers.Timer(10000);
        //    // Hook up the Elapsed event for the timer. 
        //    Timer.Elapsed += OnTimedEvent;
        //    Timer.AutoReset = true;
        //    Timer.Enabled = true;
        //}
        //public void OnTimedEvent(Object source, ElapsedEventArgs e)
        //{
        //    //her is the code 
        
        //}
        public async void ExecutAsync()
        {
            using (HRMSEntities HRMS = new HRMSEntities())
            {
                AttendanceBLL Attendance = new AttendanceBLL();
                Attendance.InsertNewAttendanceInHRMS(Attendance.GetLeatestAccess());
                var Time = Convert.ToInt32(HRMS.Configurations.Where(x => x.ConfigurationKey == "Delay").FirstOrDefault().ConfigurationValue);

                await Task.Delay(Time);
                ExecutAsync();

            }
        }

    }
}
