using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineApprovals.BLL.StaticData
{
    public static class StaticEnums
    {
        public enum RequestStatus
        {
            New = 1,
            AssignedByCallCenter = 2,
            Onhold = 3,
            Reopend = 4,
            Closed = 5,
            Approved = 6,
            Rejected = 7,
            PendingProviderAction = 8,
            Terminated = 9,
            PendingCallCenterAction = 10
        }
        public enum StatusColor
        {
            lime = 1,
            gold = 2,
            yellow = 3,
            violet = 4,
            red = 5,
            green = 6,
            darkred  = 7,
            darkgray = 8,
            black = 9,
            orange  = 10
        }




        public enum RequestDetailsType
        {
            Assign=1,
            Approve=2,
            ReplyFromCallCenter = 3,
            Reject= 4,
            Reopen=5,
            OpenNewRequest=6,
            ReplyFromProvider=7,
            Terminate=8,
            Onhold = 9,
            EndOnhold = 10
        }
        public enum UserType
        {
            CallCeneter=1,
            Provider=2
        }

        public enum ProviderType
        {
            Pharmacy=1
        }
        public enum RequestType
        {
            Medication = 1

        }
    }
}
