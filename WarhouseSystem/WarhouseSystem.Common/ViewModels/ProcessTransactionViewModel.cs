using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhouseSystem.Common.ViewModels
{
    public class ProcessTransactionViewModel
    {
        public long Id { get; set; }

        public long SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        public long AffectNumber { get; set; }

        public long EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public long TransactionTypeId { get; set; }

        public string TransactionTypeName { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
