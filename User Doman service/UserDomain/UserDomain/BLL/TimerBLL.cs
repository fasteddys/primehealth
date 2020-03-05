using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using UserDomain.DLL;

namespace UserDoman.BLL
{
  public  class TimerBLL
    {
     
        public async void ExecutAsync()
        {
           
            DomainBLL domainBLL = new DomainBLL();
            domainBLL.InsertUsersInHRMS(domainBLL.GetALLDomainUsers());
            int Time = 0;
                HRMSEntities HRMS = new HRMSEntities();
            {
                Time = Convert.ToInt32(HRMS.Configurations.Where(x => x.ConfigurationKey == "DelayGetUser").FirstOrDefault().ConfigurationValue);
            }
            await Task.Delay(Time);
            ExecutAsync();

                }



    }
}
