using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
   public class UserprofileDTO
    {
        public int UserID { get; set; }
        public int? ModifiedByUserId { get; set; }

        public string UserName { get; set; }
        public string LDAPUserName { get; set; }
        public string UserArabicName { get; set; }
        public string HireDate { get; set; }
        public string DepartmentName { get; set; }
        public int? DepartmentID { get; set; }
        public string SubDepartmentName { get; set; }
        public int? SubDepartmentID { get; set; }
        public string UserEmail { get; set; }
        public string ProfilePictureURL { get; set; }
        public string Gender { get; set; }
        public int? GenderID { get; set; }
        public string ProbationDate { get; set; }
        public string TerminationDate { get; set; }
        public string BirthDate { get; set; }

        public int? SeniorityBeforeHireYear { get; set; }
        public int? SeniorityBeforeHireMonth { get; set; }

        public string DirectManagerName { get; set; }
        public int? DirectManagerID { get; set; }
        public string TeamManagerName { get; set; }
        public int? TeamManagerID { get; set; }
        public string positionName { get; set; }
        public int? positionID { get; set; }
        public string PersonalEmail { get; set; }
        public string ContractTypeName { get; set; }
        public int? ContractTypeID { get; set; }
        public string BusinessPhone { get; set; }
        public int? EmployeeNumber { get; set; }
        public string CustomeNote { get; set; }
        public int? ApprovalFlowID { get; set; }
        public string ApprovalFlowName { get; set; }

        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsNew { get; set; }

        public bool IsFirstLogin { get; set; }
        public string MedicalID { get; set; }
        public string HomeAddress { get; set; }
        public string CurrentAddress { get; set; }

        public string NationalID { get; set; }
        public string PrivatePhoneNumber { get; set; }

        public int? CompanyID { get; set; }
        public string CompanyName { get; set; }

        public List<DailyAttendanceDTO> ListDailyAttendance { get; set; }
        public List<LeaveTypeEntitlementDTO> LeaveTypesEntitlements { get; set; }
        public List<UserEntitlementChangesDTO> EntitlementChanges { get; set; }

    }
}
