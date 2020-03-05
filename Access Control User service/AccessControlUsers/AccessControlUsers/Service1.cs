using AccessControlAttendance.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace AccessControlUsers
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            TimerBLL time = new TimerBLL();
            Task.Factory.StartNew(async () =>
            {
                System.Threading.Thread.Sleep(8000);
                await time.ExecutAsync();
            });


     
        }

        protected override void OnStop()
        {
        }
    }
}
