using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhouseSystem.Common.ViewModels
{
    public class CategoryViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long? Count { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
