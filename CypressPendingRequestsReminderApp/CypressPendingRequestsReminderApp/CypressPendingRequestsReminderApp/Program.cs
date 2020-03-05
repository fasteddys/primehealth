using CypressPendingRequestsReminderApp.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CypressPendingRequestsReminderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RequestBLL requestBLL = new RequestBLL();
            requestBLL.WriteToFile("App starts on " + DateTime.Now);
            requestBLL.GetAllLateRequestMailAsync();
            requestBLL.WriteToFile("App ends on " + DateTime.Now);
        }
    }
}
