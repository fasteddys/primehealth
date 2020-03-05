using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Application.CompanyApp;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.EF.UnitOfWork;
using WarhouseSystem.DB.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace WarhouseSystem.Application.TransactionTypeApp
{
    //[DbConfigurationType(typeof(DbContextConfiguration))]
    public class ManageTransactionType : IManageTransactionType
    {
        private UnitOfWork unit = null;

        MappingTransactionType map = new MappingTransactionType();

        public ManageTransactionType()
        {
            unit = new UnitOfWork();
        }
        public IEnumerable<LockupViewModel> GetAllTransactionTypes()
        {
            IEnumerable<TransactionType> AllTransactionTypes = unit.transactionTypeRepository.GetAllData();
            IEnumerable<LockupViewModel> items = AllTransactionTypes.Select(map.MapEntityToModel);
            return items;
        }
    }
}
