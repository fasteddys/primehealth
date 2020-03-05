using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DTOs.Business
{
  public  class LoginUserDTO
    {
        public int UserID { get; set; }
        public String UserName { get; set; }
        public String UserImgURL { get; set; }
        public int? ApprovalFlowFK { get; set; }
        public bool HasCompletedUserProfileData { get; set; }
        public bool IsHR { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsManager { get; set; }
    }
}
