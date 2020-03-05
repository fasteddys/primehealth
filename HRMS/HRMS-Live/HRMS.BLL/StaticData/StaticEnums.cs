using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BLL.StaticData
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
            AutoApproved = 3,
            SystemApprove=4,
            SystemReject=5
        }
        public enum RequestStatus
        {
            Approved = 1,
            Rejected = 2,
            Pending = 3,
            Deleted=4,
            PartialDeleted=5,
            SystemApproved = 6,
            SystemRejected=7

        }
        public enum Gender
        {
            Male = 1,
            Female = 2
        }
        public enum ContractTypes
        {
            FullTime = 1,
            PartTime = 2,
            Seasonal = 3
        }
        public enum LeaveTypeAccuralPeriod
        {
            FirstDayOfTheYear=1,
            RepeatEveryMonthAt = 2
        }
        public enum LeaveTypeConsiderationAs
        {
            TimeOff=1,
            RemoteWork=2
        }
        public enum LeaveDurationUnit
        {
            Days=1,
            Hours=2
        }
        public enum LeaveTypeEntitlementSource
        {
           HasOwnEntitlement=1,
           SubtypeAndUseEntitlementFromOtherType=2
        }
        public enum LeaveTypeFirstAccuralMethod
        {
            FullAmount=1,
            ProrateAmount=2
        }
        public enum LeaveTypeEntitlementType
        {
            Accured=1,
            Unlimited=2
        }
        public enum LeaveTypeGainingEligibilityMethod
        {
            FromTheFirstDayAtWork=1,
            AfterHire=2
        }
        public enum LeaveTypeMinOneDayDuration
        {
            WholeDay=1,
            HalfDay=2,
            QuarterDay=3
        }
        public enum LeaveTypeProrateCalculationMode
        {
            BasedOnDays=1,
            BasedOnFullMonths=2
        }
        public enum LeaveTypeProrateMethod
        {
            DoNotRoundResult=1,
            RoundToWhole=2,
            RoundAlwaysUp=3
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
            Approve=1,
            Reject=2,
            AutoApprove=3,
            Comment=4,
            Creation=5,
            RejectionReason=6,
            UploadAttach=7,
            DeleteRequest=8,
            DeletionReason=9,
            SystemApproveRequest=10,
            SystemRejectRequest = 11

        }
        public enum Days
        {
            Saturday=1,
            Sunday=2,
            Monday=3,
            Tuesday=4,
            Wednesday=5,
            Thursday=6,
            Friday=7

        }
        public enum RestrictionType
        {
            AbsenceNotLongerThan = 1,
            AbsenceNotShorterThan = 2,
            AbsenceRequestedUpTo = 3,
            IfAbsenceLongerThan = 4

        }
        public enum LeaveTypeField
        {
            Reason = 1,
            Substitute=2,
            Comment=3

        }
        public enum LeaveTypeFieldVisibility
        {
            Disabled=1,
Required=2,
Optional=3

        }
        public enum LeaveTypePartialDurationUnit
        {
         AMHalfDay =1,
         PMHalfDay=2

        }

        public enum LogTypes
        {
            Login=1,
            Logout=2,
            ForceLogOut=3,
            EditUserData=4,
            EditOfficialHolidays=5
        }
        public enum LeaveTypeConsideration
        {
            TimeOff = 1,
            RemoteWork = 2
        }





    }
}
