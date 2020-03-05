using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class ManagerDTO
    {
        public int ManagerID { get; set; }
        public int? ModifiedByUserId { get; set; }

        
        public String ManagerName { get; set; }
        public int ManagerUserFK { get; set; }
        public virtual List< ManagerUsersDTO> Users { get; set; }
        public virtual List< ManagerSubDepartmentsDTO> SubDepartments { get; set; }

        
    }
}
