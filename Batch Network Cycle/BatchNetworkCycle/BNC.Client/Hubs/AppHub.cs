using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BNC.Client.Hubs
{
    //[HubName("appHub")]
    public class AppHub : Hub 
    {        
        public static void Show()
        {
            //var clients = Hub.GetClients<AppHub>();
            //Clients.serverChange();
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<AppHub>();
            context.Clients.All.displayRows();
        }
    }
}