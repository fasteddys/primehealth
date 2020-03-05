using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
  public  class RestrictedEntity
    {
        public    List<int> DepartmentIDS { get; set; }
        public List<int> SubDepartmentIDS { get; set; }
        public List<int> UserIDS { get; set; }
        public List<int> ContractTypeIDS { get; set; }
        public List<int> GenderIDS { get; set; }


    }
}
