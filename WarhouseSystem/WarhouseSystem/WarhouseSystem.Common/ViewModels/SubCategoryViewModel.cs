using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhouseSystem.Common.ViewModels
{
    public class SubCategoryViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long Count { get; set; }

        public double Price { get; set; }

        public long CategoryId { get; set; }

        public string CategoryName { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
