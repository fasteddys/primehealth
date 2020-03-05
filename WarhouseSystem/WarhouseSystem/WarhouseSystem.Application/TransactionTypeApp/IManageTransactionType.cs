using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;

namespace WarhouseSystem.Application.TransactionTypeApp
{
    public interface IManageTransactionType
    {
        IEnumerable<LockupViewModel> GetAllTransactionTypes();
    }
}
