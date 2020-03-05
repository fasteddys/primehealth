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
        public EmployeeViewModel Login(EmployeeViewModel model)
        {
            var emp = unit.employeeRepository.Login(model);
            return new EmployeeViewModel {  Id= emp.Id, Name= emp.Name };
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
                Utilites.CheckResult = unit.employeeRepository.Delete(Id);
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
            try
            {
                IEnumerable<Employee> AllEmployees = unit.employeeRepository.GetAllData();

                IEnumerable<EmployeeViewModel> items = AllEmployees.Select(map.MapEntityToModel);
                return items;

            }
            catch (Exception ex)
            {
                return null;

            }
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

        public bool SetUserPassword(EmployeeViewModel model)
        {
            byte[] PasswordHash, PasswordSalt;
            using(var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));
            }
            Employee emp = unit.employeeRepository.GetDataById(model.Id);
            emp.PasswordHash = PasswordHash;
            emp.PasswordSalt = PasswordSalt;
            Utilites.CheckResult = unit.employeeRepository.Modify(model.Id, emp);
            unit.Save();
            return Utilites.CheckResult;
        }
   


    }
}
