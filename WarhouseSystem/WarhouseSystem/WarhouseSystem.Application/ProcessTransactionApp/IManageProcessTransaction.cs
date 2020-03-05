using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;

namespace WarhouseSystem.Application.ProcessTransactionApp
{
    public interface IManageProcessTransaction
    {
        bool AddProcessTransaction(ProcessTransactionViewModel model);

        bool UpdateProcessTransaction(long Id, ProcessTransactionViewModel model);

        bool DeleteProcessTransaction(long Id);

        ProcessTransactionViewModel GetProcessTransaction(long Id);

        IEnumerable<ProcessTransactionViewModel> GetAllProcessTransactions(DateTime fromDate,DateTime toDate);
    }
}
