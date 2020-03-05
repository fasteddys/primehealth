using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
  public  class LeaveTypeDropDownsDTO
    {
        public List<LeaveTypeDTO> LeaveTypes { get; set; }
        public List<LeaveTypeConsiderationAsDTO> LeaveTypeConsiderationType { get; set; }
        public List<LeaveTypeDurationUnitDTO> LeaveDurationUnit { get; set; }
        public List<LeaveTypeAccuralPeriodsDTO> LeaveTypeAccuralPeriod { get; set; }
        public List<LeaveTypeEntitlementTypeDTO> LeaveTypeEntitlementType { get; set; }
        public List<LeaveTypeFirstAccuralMethodsDTO> LeaveTypeFirstAccuralMethod { get; set; }
        public List<LeaveTypeGainingEligibilityMethodDTO> LeaveTypeGainingEligibilityMethod { get; set; }
        public List<LeaveTypeMinOneDayDurationDTO> LeaveTypeMinOneDayDurations { get; set; }
        public List<LeaveTypeProrateCalculationModeDTO> LeaveTypeProrateCalculationMode { get; set; }
        public List<LeaveTypeProrateMethodDTO> LeaveTypeProrateMethod { get; set; }
        public List< LeaveTypeEntitlementSourceDTO> LeaveTypeEntitlementSource { get; set; }
        public List<ContractTypeDTO> ContractType { get; set; }
        public List<GenderTypeDTO> GenderType { get; set; }
        public List<LeaveTypeFieldDTO> LeaveTypeFields { get; set; }
        public List<LeaveTypeFieldsVisibilityDTO> LeaveTypeFieldsVisibility { get; set; }

        public List< LeaveTypeRestrictionDTO> LeaveTypeRestriction { get; set; }
        public List<LeaveTypeMonthlyAaccuralDaysDTO> LeaveTypeMonthlyAaccuralDays { get; set; }
    }
}
