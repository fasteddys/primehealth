using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineApprovals.DTOs
{

    public class Invoice_DrugDetailsDTO
    {
        public InovicesDTO Invoice { get; set; }

        public List< DrugsDetailDTO> ListDrugsDetail { get; set; }

    }


}
