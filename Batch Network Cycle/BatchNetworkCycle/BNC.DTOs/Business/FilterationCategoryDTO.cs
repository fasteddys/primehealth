using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNC.DTOs.Business
{
   public class FilterationCategoryDTO
    {
        public int FilterationCategoryID { get; set; }
        public string FilterationCategoryName { get; set; }
        public int ClaimsActCategoryID { get; set; }
        public int ClaimsCount { get; set; }


    }
}
