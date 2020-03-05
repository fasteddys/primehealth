using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    public class CheckLockedRequestDTO
    {
        public bool IsLockedRequest{ get; set; }
        public bool RequestCanLocked { get; set; }


    }
}
