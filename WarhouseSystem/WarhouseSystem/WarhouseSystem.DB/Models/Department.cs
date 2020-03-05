using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhouseSystem.DB.Models
{
   public class Department: Base
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }

        public virtual ICollection<OtherDevicePlace> OtherDevicePlaces { get; set; }

    }
}
