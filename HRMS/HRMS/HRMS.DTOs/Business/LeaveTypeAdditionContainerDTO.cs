using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class LeaveTypeAdditionContainerDTO
    {

        public string LeaveName { get; set; }
        public int? EntitlementTypeID { get; set; }
        public int? LeaveTypeConsiderationsID { get; set; }
        public int? DurationUnitID { get; set; }
        public int? TakeEntitlementFromID { get; set; }
        public List<int> LeavePartailUnit { get; set; }
        public decimal TakemaxamountFromParentLeaveType { get; set; }
        public int? AccuralID { get; set; }
        public int? EmployeeGainsEligibilityAfterID { get; set; }
        public decimal? EmployeeEarnsNumberOfUnits { get; set; }
        public int? OverSeniorityYears { get; set; }
        public decimal? OverSeniorityEmployeeEarns { get; set; }
        public int? NumberOfMonthsTogainseligibility { get; set; }
        public int? ProrateCalculationModeID { get; set; }
        public int? ProrateMethodID { get; set; }
        public bool EnableCarryOverUnusedEntitlement { get; set; }
        public bool EnableCarryoverNegativeEntitlement { get; set; }
        public bool ExpireCarriedOverAfterTime { get; set; }
        public decimal? MaxNumberOfDaysToCarriedOver { get; set; }

        public int NumberOfMonthsToExpireCarriedOverLeave { get; set; }
        public bool EnableAttachments { get; set; }
        public bool AttachmentsRequired { get; set; }        
        public bool AbsenceNotLongerThan { get; set; }
        public bool AbsenceNotShorterThan { get; set; }
        public bool IfAbsenceLongerThan { get; set; }
        public bool EnablePartailUnit { get; set; }
        public bool AbsenceRequestedUpTo { get; set; }
        public decimal? NumberDaysRequestedUpTo { get; set; }

        public decimal? NumberAbsenceNotLongerThan { get; set; }
        public decimal? NumberAbsenceNotShorterThan { get; set; }
        public decimal NumberOfDayIfAbsenceLongerThan { get; set; }
        public LeaveTypeRequestDurationLimitDTO LeaveTypeRequestDurationLimit { get; set; }
        public List<LeaveTypeFieldDTO> LeaveTypeFields { get; set; }
        public string EffectiveDate { get; set; }
        public RestrictedEntity RestrictedEntitys { get; set; }

        public bool AllowNegativeentitlement { get; set; }
        public decimal MaxmimAllowNegativeEntitlement { get; set; }
        public int MinLeaveDurationWithinOneDay { get; set; }
       public int AccuralPeriodID { get; set; }
        public int FirstAccuralMethodForNewEmployeesID { get; set; }
        public decimal NumberOfDaysAllowedToMakeRequestInThePast { get; set; }
        public int MonthlyAaccuralDays { get; set; }
        public bool IsSuspend { get; set; }
        public int LeaveTypeFK { get; set; }


    }
}
