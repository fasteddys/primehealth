using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
   public class UserInformationOutputDTO
    {
        public int UserID { get; set; }//1
        public int? AccessControlUserID { get; set; }//2
        public String UserName { get; set; }//3
        public string ArbicName { get; set; }//4
        public string CompanyName { get; set; }//5
        public string ManagerName { get; set; }//6
        public String TeamManagerName { get; set; }//7
        public String ApprovalFlowName { get; set; }//8
        public String SubDepartmentName { get; set; }//9
        public String DepartmentName { get; set; }//10
        public String PositionName { get; set; }//11
        public String workingWeekName { get; set; }//12
        public String ContractTypeName { get; set; }//13
        public String Gender { get; set; }//14
        public String HireDate { get; set; }//15
        public String BirthDate { get; set; }//16
        public String ProbationDate { get; set; }//17
        public String TerminationDate { get; set; }//18
        public DateTime? creationDate { get; set; }//19
        public DateTime? ModificationDate { get; set; }//20
        public int? SeniorityBeforeHireMonth { get; set; }//21
        public int? SeniorityBeforeHireYears { get; set; }//22
        public string MedicalID { get; set; }//23
        public string Address { get; set; }//24
        public string HomeAddress { get; set; }//25
        public string IsOutDomainUser { get; set; }//26
        public string NationalID { get; set; }//27
        public string PhoneNumber { get; set; }//28
        public string CustomNote { get; set; }//29
        public string IsActive { get; set; }//30


   


   
   
                       
                       
       
                       
    }
}
