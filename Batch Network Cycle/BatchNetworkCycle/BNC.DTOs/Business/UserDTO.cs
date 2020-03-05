using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
  public  class UserDTO
    {
        public int UserID { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public int TeamID { get; set; }
        public string TeamName { get; set; }

    }
}
