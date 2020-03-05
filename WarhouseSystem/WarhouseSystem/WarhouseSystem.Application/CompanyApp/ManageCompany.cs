using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.EF.UnitOfWork;
using WarhouseSystem.DB.Models;

namespace WarhouseSystem.Application.CompanyApp
{
    public class ManageCompany : IManageCompany
    {
        private UnitOfWork unit = null;
        
        MappingCompany map = new MappingCompany();

        public ManageCompany()
        {
            unit = new UnitOfWork();
        }
        public IEnumerable<LockupViewModel> GetAllComapnies()
        {
            IEnumerable<Company> AllCompanies = unit.companyRepository.GetAllData();
            IEnumerable<LockupViewModel> items = AllCompanies.Select(map.MapEntityToModel);
            return items.ToList();
        }
    }
}
