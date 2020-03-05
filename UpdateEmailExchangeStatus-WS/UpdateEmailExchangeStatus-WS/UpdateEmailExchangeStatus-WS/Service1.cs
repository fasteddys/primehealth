using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using UpdateEmailExchangeStatus_WS.BLL;
using UpdateEmailExchangeStatus_WS.Model;

namespace UpdateEmailExchangeStatus_WS
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;
        //private EmailBLL emailBLL;
        public Service1()
        {
            timer = new Timer();
            //emailBLL = new EmailBLL();
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            using (PhNetworkEntities Db = new PhNetworkEntities())
            {
                double ElapsedTime = double.Parse(Db.EmailApprovalsConfigurations.Where(x => x.ConfigurationKey == "UpdateExchangeEmailStatusTimeElapsed").FirstOrDefault().ConfigurationValue);

                FileBLL.WriteToFile("Service is started at " + DateTime.Now);

                timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
                timer.Interval = ElapsedTime; //number in milisecinds  
                timer.Enabled = true;
            }
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            EmailBLL emailBLL = new EmailBLL();
            try
            {
                FileBLL.WriteToFile(" Function is started at " + DateTime.Now);
                emailBLL.UpdateEmail();
                emailBLL.Context?.Dispose();
                GC.SuppressFinalize(this);
                FileBLL.WriteToFile(" Function is finished at " + DateTime.Now);
            }
            catch (Exception ex)
            {
                FileBLL.WriteToFile("Error " + ex.Message);
                throw;
            }
            
        }
        protected override void OnStop()
        {
            FileBLL.WriteToFile("Service is stopped at " + DateTime.Now);
        }

        //public void WriteToFile(string Message)
        //{
        //    string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
        //    if (!Directory.Exists(path))
        //    {
        //        Directory.CreateDirectory(path);
        //    }

        //    string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog " + DateTime.Now.Date.ToShortDateString().Replace('/', '-') + ".txt";
        //    if (!File.Exists(filepath))
        //    {
        //        // Create a file to write to.   
        //        using (StreamWriter sw = File.CreateText(filepath))
        //        {
        //            sw.WriteLine(Message);
        //        }
        //    }
        //    else
        //    {
        //        using (StreamWriter sw = File.AppendText(filepath))
        //        {
        //            sw.WriteLine(Message);
        //        }
        //    }
        //}
    }
}
