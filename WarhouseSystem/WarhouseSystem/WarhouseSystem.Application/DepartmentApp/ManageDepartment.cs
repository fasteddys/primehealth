using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.Common.Utilites;
using WarhouseSystem.EF.UnitOfWork;
using WarhouseSystem.DB.Models;

namespace WarhouseSystem.Application.DepartmentApp
{
    public class ManageEDepartment : IManageDepartment
    {
        private UnitOfWork unit = null;

        MappingDepartment map = new MappingDepartment();

        public ManageEDepartment()
        {
            unit = new UnitOfWork();
        }
        public bool AddDepartment(DepartmentViewModel model)
        {
            try
            {
                Department department = map.MapModelToEntity(model);
                Utilites.CheckResult = unit.departmentRepository.Add(department);
                unit.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateDepartment(long Id, DepartmentViewModel model)
        {
            if (Id == 0)
            {
                Department department = map.MapModelToEntity(model);
                Utilites.CheckResult = unit.departmentRepository.Add(department);
                unit.Save();
                return Utilites.CheckResult;
            }
            else
            {
                Department department = unit.departmentRepository.GetDataById(Id);
                map.MapModelToEntity(model, department);
                Utilites.CheckResult = unit.departmentRepository.Modify(model.Id, department);
                unit.Save();
                return Utilites.CheckResult;
            }
        }

        public bool DeleteDepartment(long Id)
        {
            try
            {
                Utilites.CheckResult = unit.departmentRepository.Delete(Id);
                unit.Save();
                return Utilites.CheckResult;
            }
            catch
            {
                return false;
            }
        }

        public DepartmentViewModel GetDepartment(long Id)
        {
            Department department = unit.departmentRepository.GetDataById(Id);
            DepartmentViewModel departmentViewModel = map.MapEntityToModel(department);
            return departmentViewModel;
        }

        public IEnumerable<DepartmentViewModel> GetAllDepartments()
        {
            IEnumerable<Department> AllDepartments = unit.departmentRepository.GetAllData();
            IEnumerable<DepartmentViewModel> items = AllDepartments.Select(map.MapEntityToModel);
            return items;
        }

        public int GetDepartmentCount()
        {
            return unit.departmentRepository.GetCount();
        }
    }
}
