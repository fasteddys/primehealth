using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
    public class AuditCategDTO
    {
        
        public List<int> medicalList;
        public List<int> FinincialList;
      public  AuditCategDTO()
        {
            medicalList = new List<int>();
            FinincialList = new List<int>();
        }
    }
}
