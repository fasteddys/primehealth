using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhouseSystem.DB.Models
{
   public class OtherDevicePlace : Base
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
