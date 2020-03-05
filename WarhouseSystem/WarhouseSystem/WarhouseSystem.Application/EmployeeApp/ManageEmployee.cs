using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.Common.Utilites;
using WarhouseSystem.EF.UnitOfWork;
using WarhouseSystem.DB.Models;

namespace WarhouseSystem.Application.EmployeeApp
{
    public class ManageEmployee : IManageEmployee
    {
        private UnitOfWork unit = null;

        MappingEmployee map = new MappingEmployee();

        public ManageEmployee()
        {
            unit = new UnitOfWork();
        }

        public bool Login(EmployeeViewModel model,out long employeeId)
        {
            long empId = 0;
            Utilites.CheckResult = unit.employeeRepository.Login(model,out employeeId);
            empId = employeeId;
            return Utilites.CheckResult;
        }
        public bool AddEmployee(EmployeeViewModel model)
        {
            try
            {
                Employee emp = map.MapModelToEntity(model);
                Utilites.CheckResult = unit.employeeRepository.Add(emp);
                unit.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteEmployee(long Id)
        {
            try
            {
                Employee emp = unit.employeeRepository.GetDataById(Id);
                emp.IsDeleted = true;
                Utilites.CheckResult = unit.employeeRepository.Modify(Id, emp);
                unit.Save();
                return Utilites.CheckResult;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<EmployeeViewModel> GetAllEmployees()
        {
            IEnumerable<Employee> AllEmployees = unit.employeeRepository.GetAllData().Where(x=>x.IsDeleted==false);
            IEnumerable<EmployeeViewModel> items = AllEmployees.Select(map.MapEntityToModel);
            return items;
        }

        public EmployeeViewModel GetEmployee(long Id)
        {
            Employee emp = unit.employeeRepository.GetDataById(Id);
            EmployeeViewModel empViewModel = map.MapEntityToModel(emp);
            return empViewModel;
        }

        public bool UpdateEmployee(long Id,EmployeeViewModel model)
        {
            if (Id == 0)
            {
                Employee emp = map.MapModelToEntity(model);
                Utilites.CheckResult = unit.employeeRepository.Add(emp);
                unit.Save();
                return Utilites.CheckResult;
            }
            else
            {
                Employee emp = unit.employeeRepository.GetDataById(Id);
                map.MapModelToEntity(model, emp);
                Utilites.CheckResult = unit.employeeRepository.Modify(model.Id, emp);
                unit.Save();
                return Utilites.CheckResult;
            }
        }

        public int GetEmployeesCount()
        {
            return unit.employeeRepository.GetCount();
        }
    }
}
