using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
  public  class UserCardDTO
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string SubDepartment { get; set; }

        public string ProfilePictureURL { get; set; }
        public string DirectlyReportsTo { get; set; }
        public string TeamLeader { get; set; }
        public string CompanyName { get; set; }
        public string ExtNumber { get; set; }
    }
}
