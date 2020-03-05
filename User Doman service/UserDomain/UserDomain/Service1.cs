using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using UserDoman.BLL;

namespace UserDomain
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
            Task.Factory.StartNew(() =>
            {
                System.Threading.Thread.Sleep(8000);
                time.ExecutAsync();
            });



        }
      

        protected override void OnStop()
        {
        }
    }
}
