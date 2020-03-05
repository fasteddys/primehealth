using CallCenterAutoReply_WS.BLL;
using CallCenterAutoReply_WS.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CallCenterAutoReply_WS
{
    public partial class Service1 : ServiceBase
    {
        private Request RequestObj;
        private Timer Timer;
        public Service1()
        {
            RequestObj = new Request();
            Timer = new Timer();

            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            using (PhNetworkEntities Db = new PhNetworkEntities())
            {
                double ElapsedTime = int.Parse((from r in Db.EmailApprovalsConfigurations where r.ConfigurationKey == "NoReplyTimeElapsedTimeInms" select r.ConfigurationValue).SingleOrDefault());
                Email.WriteToFile("Service is Started at " + DateTime.Now);

                Timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);

                Timer.Interval = ElapsedTime; //number in milisecinds  

                Timer.Enabled = true;
            }
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            Email.WriteToFile(" Service Start Sending Emails at " + DateTime.Now);
            RequestObj.GetNewRequests();
            Email.WriteToFile(" Service End Sending Emails at " + DateTime.Now);
        }
        protected override void OnStop()
        {
            Email.WriteToFile("Service is Stopped at " + DateTime.Now);
        }
    }
}
