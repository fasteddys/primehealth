using AddAcccuralLeaveTypesForNewEmployees_WS.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace AddAcccuralLeaveTypesForNewEmployees_WS
{
    public partial class Service1 : ServiceBase
    {  
        public Service1()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            LeaveTypesBLL leaveTypesBLL = new LeaveTypesBLL();
            System.Threading.Thread.Sleep(20000);
            leaveTypesBLL. WriteToFile("Start of Service On " + DateTime.Now);
            Task.Factory.StartNew(async () =>
            {
                await leaveTypesBLL.ScheduleOfExecutionAsync();
            });
            leaveTypesBLL.WriteToFile("End of Service On " + DateTime.Now);
        }
        protected override void OnStop()
        {
            //WriteToFile("Service is stopped at " + DateTime.Now);
        }
        
    }
}
