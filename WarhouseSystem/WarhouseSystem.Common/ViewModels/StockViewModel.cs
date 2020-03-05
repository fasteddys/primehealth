using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhouseSystem.Common.ViewModels
{
    public class StockViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long DepartmentId { get; set; }

        public string DepartmentName{ get; set; }


    }
}
