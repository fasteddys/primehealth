using CypressPendingRequestsReminder.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CypressPendingRequestsReminder
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        
            System.Threading.Thread.Sleep(20000);

            RequestBLL Request = new RequestBLL();

            Task.Factory.StartNew(async () =>
            {
                await Request.ScheduleOfExecutionAsync();
                
            });


        }

        protected override void OnStop()
        {
        }
    }
}
