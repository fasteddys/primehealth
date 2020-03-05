using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
  public  class UserDataDropDownDTO
    {
        public  List<ManagerDTO> ManagerDTO { get; set; }
        public List<UserDTO> UserDTO { get; set; }
        public List<DepartmentDTO> Departments { get; set; }
        public  List<SubDepartmentDTO> SubDepartments { get; set; }
        public  List<PositionDTO> Positios { get; set; }
        public  List<ApprovalFlowContainerDTO> ApprovalFlow { get; set; }
        public List<ContractTypeDTO> ContractTypes { get; set; }
        public List<GenderTypeDTO> GenderTypes { get; set; }
        public List<CompanyDTO> Companies { get; set; }
        public List<WorkingModeDTO> WorkingMode { get; set; }
        public List<WorkingWeekDTO> WorkingWeek { get; set; }
    }
}
