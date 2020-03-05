using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;

namespace WarhouseSystem.Application.DepartmentApp
{
    public interface IManageDepartment
    {
        bool AddDepartment(DepartmentViewModel model);

        bool UpdateDepartment(long Id, DepartmentViewModel model);

        bool DeleteDepartment(long Id);

        DepartmentViewModel GetDepartment(long Id);

        IEnumerable<DepartmentViewModel> GetAllDepartments();

        int GetDepartmentCount();
    }
}
