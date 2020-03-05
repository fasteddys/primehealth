using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.Utilites;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.DB.Models;
using WarhouseSystem.EF.UnitOfWork;

namespace WarhouseSystem.Application.ProcessTransactionApp
{
    public class ManageProcessTransaction : IManageProcessTransaction
    {
        private UnitOfWork unit = null;

        MappingProcessTransaction map = new MappingProcessTransaction();

        public ManageProcessTransaction()
        {
            unit = new UnitOfWork();
        }
        public bool AddProcessTransaction(ProcessTransactionViewModel model)
        {
            try
            {
                ProcessTransaction proc = map.MapModelToEntity(model);
                Utilites.CheckResult = unit.processTransactionRepository.Add(proc);
                unit.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteProcessTransaction(long Id)
        {
            try
            {
                Utilites.CheckResult = unit.processTransactionRepository.Delete(Id);
                unit.Save();
                return Utilites.CheckResult;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<ProcessTransactionViewModel> GetAllProcessTransactions(DateTime fromDate, DateTime toDate)
        {
            IEnumerable<ProcessTransaction> AllProcessTransactions = unit.processTransactionRepository.GetDataByCondition(x=>x.CreationTime >= fromDate && x.CreationTime<toDate);
            IEnumerable<ProcessTransactionViewModel> items = AllProcessTransactions.Select(map.MapEntityToModel);
            return items;
        }

        public ProcessTransactionViewModel GetProcessTransaction(long Id)
        {
            ProcessTransaction proc = unit.processTransactionRepository.GetDataById(Id);
            ProcessTransactionViewModel catViewModel = map.MapEntityToModel(proc);
            return catViewModel;
        }

        public bool UpdateProcessTransaction(long Id, ProcessTransactionViewModel model)
        {
            if (Id == 0)
            {
                ProcessTransaction proc = map.MapModelToEntity(model);
                Utilites.CheckResult = unit.processTransactionRepository.Add(proc);
                unit.Save();
                return Utilites.CheckResult;
            }
            else
            {
                ProcessTransaction proc = unit.processTransactionRepository.GetDataById(Id);
                map.MapModelToEntity(model, proc);
                Utilites.CheckResult = unit.processTransactionRepository.Modify(model.Id, proc);
                unit.Save();
                return Utilites.CheckResult;
            }
        }
    }
}
