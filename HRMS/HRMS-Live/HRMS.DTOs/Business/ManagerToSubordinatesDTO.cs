using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class ManagerToSubordinatesDTO
    {
        public int ManagerID { get; set; }
        public int UserID { get; set; }

        public List<int> ListUsersIDs { get; set; }

        public List<int> ListSubDepartmentIDs { get; set; }
    


    }
}
