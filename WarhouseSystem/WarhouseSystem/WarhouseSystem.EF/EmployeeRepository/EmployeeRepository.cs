using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.DB.Models;
using WarhouseSystem.Common.ViewModels;

namespace WarhouseSystem.EF
{
    public class EmployeeRepository : GenericRepository.GenericRepositry<Employee>
    {
        public DB.Models.DB _dbContext;
        public EmployeeRepository(DB.Models.DB dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Login(EmployeeViewModel model,out long EmployeeId)
        {
            var empCount = _dbContext.Employees.Where(x => x.Name == model.Name && x.Password == model.Password && x.LoggedIn == true && x.IsDeleted == false).SingleOrDefault();
            EmployeeId =empCount.Id; 
            return empCount != null ? true : false;
        }
    }
}
