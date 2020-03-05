using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.BLL.StaticData
{
    public static class StaticEnums
    {

        public enum ClientRequestType
        {
            TripRequest = 1,
            PickupRequest = 2
        }
        public enum RequestStatus
        {
            New = 1,
            Pending = 2,
            Closed = 3
        }

    

        public enum UserType
        {
            Admin = 1,
            Emplyee = 2,
        }
        public enum UserAction
        {
            Assign = 1,
            AddTripDetails = 2,
            Close = 3,
            AddPickupTime = 4,
        }
        public enum NotificationMethod
        {
            Email = 1,
            SMS = 2,
            InApplication = 3
        }

     
        public enum NotificationDirection
        {
            FromAgentToClient = 1,
            FromClientToAgent = 2
        }


    }
}
