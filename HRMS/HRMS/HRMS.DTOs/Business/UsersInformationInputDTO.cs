using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
    public class UsersInformationInputDTO
    {
        public int FiliterUsersBy { get; set; }
        public List<int> ListUserstIDs { get; set; }
    }
}
