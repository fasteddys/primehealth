using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTypeRenewal.BLL
{
    class StaticEnums
    {
        public enum LeaveTypeAccuralPeriod
        {
            FirstDayOfTheYear = 1,
            RepeatEveryMonthAt = 2
        }
        public enum LeaveTypeFirstAccuralMethod
        {
            FullAmount = 1,
            ProrateAmount = 2
        }
        public enum LeaveTypeEntitlementType
        {
            Accured = 1,
            Unlimited = 2
        }
        public enum LeaveTypeGainingEligibilityMethod
        {
            FromTheFirstDayAtWork = 1,
            AfterHire = 2
        }
        public enum LeaveTypeProrateCalculationMode
        {
            BasedOnDays = 1,
            BasedOnFullMonths = 2
        }
        public enum LeaveTypeProrateMethod
        {
            DoNotRoundResult = 1,
            RoundToWhole = 2,
            RoundAlwaysUp = 3
        }
        public enum EntitlementChangedBy
        {
            System = 1,
            Admin = 2,
            HR = 3,
            Request = 4
        }
        public enum LeaveDurationUnit
        {
            Days = 1,
            Hours = 2
        }
        public enum RequestDetailsTypes
        {
            Approve = 1,
            Reject = 2,
            AutoApprove = 3,
            Comment = 4,
            Creation = 5,
            RejectionReason = 6,
            UploadAttach = 7,
            DeleteRequest = 8,
            DeletionReason = 9,
            SystemApproveRequest = 10,
            SystemRejectRequest = 11

        }
        public enum ActionType
        {
            Approved = 1,
            Rejected = 2,
            AutoApproved = 3,
            SystemApprove = 4,
            SystemReject = 5
        }
        public enum RequestStatus
        {
            Approved = 1,
            Rejected = 2,
            Pending = 3,
            Deleted = 4,
            PartialDeleted = 5,
            SystemApproved = 6,
            SystemRejected = 7
        }
        public enum ContractTypes
        {
            FullTime = 1,
            PartTime = 2
        }
        public enum LeaveTypes
        {
  	Annual= 5,
    Casual=6 ,
	Excuse=7 ,
	Sick=8 ,
	WorkMission_Days=36,
	WorkMission_Hours=37,
	Deduction=38,
	Allowance=39,
	Awarding=41,
	Maternity=42,
	DeductedSick=43,
	Part_TimeAnnual=44,
	Part_TimeCasual=45,
	Part_TimeSick=46
        }
        public enum Company
        {
	MedGulf=1,
	PrimeHealth=2
        }
        public enum SelectFillter
        {
            MedGulf = 1,
            PrimeHealth = 2,
            ALL=3
        }

    }
}
