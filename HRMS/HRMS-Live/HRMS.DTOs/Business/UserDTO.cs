using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public int? AccessControlUserID { get; set; }
        public int? ModifiedByUserId { get; set; }
        public String UserName { get; set; }
        public String UserEmail { get; set; }
        public String ApprovalFlowName { get; set; }
        public String SubDepartmentName { get; set; }
        public String DepartmentName { get; set; }
        public String PositionName { get; set; }
        public String workingWeekName { get; set; }

        public String ContractTypeName { get; set; }
        public String Gender { get; set; }
        public String DirectManagerName { get; set; }
        public String TeamManagerName { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? ProbationDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public DateTime? creationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        

        public int? SeniorityBeforeHireMonth { get; set; }
        public int? SeniorityBeforeHireYears { get; set; }
        public bool IsFirstLogin { get; set; }
        public string MedicalID { get; set; }
        public string Address { get; set; }
        public string NationalID { get; set; }
        public string PrivatePhoneNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string CustomNote { get; set; }
        public string ManagerName { get; set; }
        public string IsActive { get; set; }
        public string ExNumber { get; set; }
        public string ArbicName { get; set; }

        //-------------------------------------------
        public virtual List<NormalUsersDTO> Users { get; set; }
        public virtual List< ManagerSubDepartmentsDTO> SubDepartments { get; set; }

        
    }
}
