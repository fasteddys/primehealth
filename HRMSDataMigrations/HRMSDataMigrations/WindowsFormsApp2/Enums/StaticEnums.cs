using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2.Enums
{
    public static class StaticEnums
    {
        public enum DeviceType
        {
            IN = 1,
            OUT = 2
        }



        public enum ActionType
        {
            Approved = 1,
            Rejected = 2,
            AutoApproved = 3
        }
        public enum RequestStatus
        {
            Approved = 1,
            Rejected = 2,
            Pending = 3
        }
        public enum Gender
        {
            Male = 1,
            Female = 2
        }
        public enum ContractName
        {
            FullTime = 1,

            PartTime = 2

        }

        public enum LeaveAccuralPeriod
        {
            Month = 1,
            Year = 2
        }

        public enum LeaveCalculationType
        {
            TimeOff = 1,
            RemoteWork = 2
        }

        public enum LeaveDurationUnit
        {
            Days = 1,
            Hours = 2
        }

        public enum LeaveEarningType
        {
            EachAmount = 1,
            EachPeriod = 2
        }

        public enum LeaveEligibilityUnitType
        {
            Years = 1,
            Month = 2,
            Weeks = 3,
            Days = 4
        }

        public enum LeaveEntitlementType
        {
            Accured = 1,
            Unlimited = 2,
            Undefined = 3
        }

        public enum LeaveOneDayMinDuration
        {
            OneDay = 1,
            HalfDay = 2,
            QuarterDay = 3
        }

        public enum LeaveTypeSameDayDuration
        {
            AllDay = 1,
            HalfDayAM = 2,
            HalfDayPM = 3
        }


        public enum EntitlementChangedBy
        {
            System = 1,
            Admin = 2,
            HR = 3,
            Request = 4

        }


        public enum RequestDetailsTypes
        {
            Approve = 1,
            Reject = 2,
            AutoApprove = 3,
            Comment = 4,
            Creation = 5,
            RejectionReason = 6

        }
        public enum Days
        {
            saturday = 1,
            sunday = 2,
            monday = 3,
            tuesday = 4,
            wednesday = 5,
            thursday = 6,
            friday = 7

        }
        public enum CompanyName
        {
            MedGulf =1,
	        PrimeHealth=2
        }
    }

}
