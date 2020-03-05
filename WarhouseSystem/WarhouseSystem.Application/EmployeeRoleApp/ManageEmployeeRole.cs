using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.EF.UnitOfWork;
using WarhouseSystem.DB.Models;

namespace WarhouseSystem.Application.EmployeeRoleApp
{
    public class ManageEmployeeRole : IManageEmployeeRole
    {
        private UnitOfWork unit = null;

        MappingEmployeeRole map = new MappingEmployeeRole();

        public ManageEmployeeRole()
        {
            unit = new UnitOfWork();
        }
        public IEnumerable<LockupViewModel> GetAllEmployeeRoles()
        {
            IEnumerable<EmployeeRole> AllEmployeeRoles = unit.employeeRoleRepository.GetAllData();
            IEnumerable<LockupViewModel> items = AllEmployeeRoles.Select(map.MapEntityToModel);
            return items;
        }
    }
}
