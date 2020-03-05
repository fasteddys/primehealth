using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhouseSystem.DB.Models
{
   public class SubCategory : Base
    {
        public long Id { get; set; }
         
        public string Name { get; set; }

        public long SubCategoryCount { get; set; }

        public double Price { get; set; }

        public long CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<ProcessTransaction> ProcessTransactions { get; set; }
    }
}
