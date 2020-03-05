using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.EF.UnitOfWork;
using WarhouseSystem.DB.Models;
using WarhouseSystem.Common.Utilites;

namespace WarhouseSystem.Application.CategoryApp
{
    public class ManageCategory : IManageCategory
    {
        private UnitOfWork unit = null;

        MappingCategory map = new MappingCategory();

        public ManageCategory()
        {
            unit = new UnitOfWork();
        }
        public bool AddCategory(CategoryViewModel model)
        {
            try
            {
                Category cat = map.MapModelToEntity(model);
                Utilites.CheckResult = unit.categoryRepository.Add(cat);
                unit.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCategory(long Id)
        {
            try
            {
                Category cat = unit.categoryRepository.GetDataById(Id);
                cat.IsDeleted = true;
                Utilites.CheckResult = unit.categoryRepository.Modify(Id, cat);
                unit.Save();
                return Utilites.CheckResult;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<CategoryViewModel> GetAllCategorys()
        {
            IEnumerable<Category> AllCategories = unit.categoryRepository.GetAllData().Where(s=>s.IsDeleted==false);
            IEnumerable<CategoryViewModel> items = AllCategories.Select(map.MapEntityToModel);
            return items;
        }

        public CategoryViewModel GetCategory(long Id)
        {
            Category cat = unit.categoryRepository.GetDataById(Id);
            CategoryViewModel catViewModel = map.MapEntityToModel(cat);
            return catViewModel;
        }

        public bool UpdateCategory(long Id, CategoryViewModel model)
        {
            if (Id == 0)
            {
                Category cat = map.MapModelToEntity(model);
                Utilites.CheckResult = unit.categoryRepository.Add(cat);
                unit.Save();
                return Utilites.CheckResult;
            }
            else
            {
                Category cat = unit.categoryRepository.GetDataById(Id);
                map.MapModelToEntity(model, cat);
                Utilites.CheckResult = unit.categoryRepository.Modify(model.Id, cat);
                unit.Save();
                return Utilites.CheckResult;
            }
        }

        public int GetCategoryCount()
        {
            return unit.categoryRepository.GetCount();
        }

        public long? GetSubCategoryCountById(long Id)
        {
            return unit.categoryRepository.CountCategory(Id);
        }
    }
}
