using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.DB.Models;

namespace WarhouseSystem.Application.EmployeeApp
{
    public interface IManageEmployee
    {
        bool AddEmployee(EmployeeViewModel model);
        bool SetUserPassword(EmployeeViewModel model);
        bool UpdateEmployee(long Id,EmployeeViewModel model);

        bool DeleteEmployee(long Id);

        EmployeeViewModel GetEmployee(long Id);

        IEnumerable<EmployeeViewModel> GetAllEmployees();

        EmployeeViewModel Login(EmployeeViewModel model);
    }
}
