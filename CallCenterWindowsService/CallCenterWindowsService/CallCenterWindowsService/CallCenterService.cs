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
using static CallCenterNotesApp.Enums.Enums;

namespace CallCenterWindowsService
{
    public partial class CallCenterService : ServiceBase
    {
        Timer timer = new Timer(); // name space(using System.Timers;)  
        public CallCenterService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            WriteToFile("Service is started at " + DateTime.Now);

            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);

            timer.Interval = 120000; //number in milisecinds  

            timer.Enabled = true;

        }
        protected override void OnStop()
        {
            //Mailing.Send_Mail_Service_Stoped();
            WriteToFile("Service is stopped at " + DateTime.Now);

        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {

            WriteToFile("color change cycle is initiated" + DateTime.Now);

             
            
                PhNetworkEntities db = new PhNetworkEntities();

                List<EmailApprovalRequest> data = (from p in db.EmailApprovalRequests
                                                   where p.RequstStatusID != (int)RequestStatus.Closed ||
                                                      (p.IsFirstNotifySent == true && p.IsSecondNotifySent == true && p.IsThirdNotifySent == true)
                                                      orderby p.ID descending
                                                      select p).ToList();


            foreach (var ItemEmailApproval in data)
            {
                Int64 LateMenits = Convert.ToInt64((DateTime.Now - ItemEmailApproval.CreationDate.Value).TotalMinutes);
                //string ClintType = ItemEmailApproval.TicketTypeID;
                //int Level = (int)ItemEmailApproval.IsFirstNotifySent + (int)ItemEmailApproval.IsScenodNotifySent + (int)ItemEmailApproval.IsThirdNotifySent;
                foreach (var ColorItem in db.ColorAlerts)

                {
                    if (ColorItem.BeginTime <= LateMenits && (LateMenits < ColorItem.EndTime || ColorItem.EndTime == null) &&
                        ItemEmailApproval.TicketTypeID == ColorItem.TicketType)

                    {
                        if (ColorItem.TimeLevel == 0)
                        {
                            //Send Mail First Level
                        }

                        else if (ColorItem.TimeLevel == 1)
                        {
                            ItemEmailApproval.IsFirstNotifySent = true;
                            //Send Mail First Level
                        }
                        else if (ColorItem.TimeLevel == 2)
                        {
                            ItemEmailApproval.IsSecondNotifySent = true;

                        }
                        else if (ColorItem.TimeLevel == 3)
                        {
                            ItemEmailApproval.IsThirdNotifySent = true;

                        }
                        ItemEmailApproval.ColorID = ColorItem.ID;
                        

                    }


                }


            }

            db.SaveChanges();

            WriteToFile("color change cycle is done successfully" + DateTime.Now);

        }
        public class Enums
        {
            public enum TicketType
            {
                Normal = 1,
                Special = 2,
                Inquiries = 3,
                None = 4
            }
        }

        public void WriteToFile(string Message)
        {

            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";

            if (!Directory.Exists(path))
            {

                Directory.CreateDirectory(path);

            }

            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

            if (!File.Exists(filepath))
            {

                // Create a file to write to.   

                using (StreamWriter sw = File.CreateText(filepath))
                {

                    sw.WriteLine(Message);

                }

            }
            else
            {

                using (StreamWriter sw = File.AppendText(filepath))
                {

                    sw.WriteLine(Message);

                }

            }

        }

    }

}


