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

        public enum UserAction
        {
            ContactClientForTrip = 1,
            SetPickUpTime = 2,
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

        public enum LogTypes
        {
            LogIn = 1,
            LogOut = 2,
            EditClientData = 3,
            EditTripData = 4,
            EditRequestData = 5
        }
    }
}
