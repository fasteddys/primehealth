using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhouseSystem.DB.Models
{
    public class ProcessTransaction : Base
    {
        public long Id { get; set; }

        public long SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public long AffectNumber { get; set; }

        public long EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public long TransactionTypeId { get; set; }

        public virtual TransactionType TransactionType { get; set;}
    }
}
