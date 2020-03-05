using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhouseSystem.DB.Models
{
    public class Category : Base
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long? CategoryCount { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
