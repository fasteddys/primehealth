using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace UserDoman.BLL
{
  public  class TimerBLL
    {
        private System.Timers.Timer Timer;
        public void SetTimer()
        {
            // Create a timer with a two second interval.
            Timer = new System.Timers.Timer(43200000);//12 Hours
            // Hook up the Elapsed event for the timer. 
            Timer.Elapsed += OnTimedEvent;
            Timer.AutoReset = true;
            Timer.Enabled = true;
        }
        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            //her is the code 
            DomanBLL Attendance = new DomanBLL();
            Attendance.InsertUsersInHRMS(Attendance.GetALLDomainUsers());
        }


    }
}
