using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Application.ProcessTransactionApp;
using WarhouseSystem.Common.Utilites;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.DB.Models;
using WarhouseSystem.EF.UnitOfWork;

namespace WarhouseSystem.Application.SubCategoryApp
{
    public class ManageSubCategory : IManageSubCategory
    {
        private UnitOfWork unit = null;

        MappingSubCategory map = new MappingSubCategory();

        public ManageSubCategory()
        {
            unit = new UnitOfWork();
        }
        public bool AddSubCategory(SubCategoryViewModel model,out long subCategoryId)
        {
            try
            {
                SubCategory cat = map.MapModelToEntity(model);
                Utilites.CheckResult = unit.subCategoryRepository.Add(cat);
                unit.Save();
                subCategoryId = cat.Id;
                return true;
            }
            catch
            {
                subCategoryId = 0;
                return false;
            }
        }

        public bool DeleteSubCategory(long Id)
        {
            try
            {
                SubCategory cat = unit.subCategoryRepository.GetDataById(Id);
                cat.IsDeleted = true;
                Utilites.CheckResult = unit.subCategoryRepository.Modify(cat.Id, cat);
                unit.Save();
                return Utilites.CheckResult;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<SubCategoryViewModel> GetAllSubCategories()
        {
            IEnumerable<SubCategory> AllSubCategories = unit.subCategoryRepository.GetAllData();
            IEnumerable<SubCategoryViewModel> items = AllSubCategories.Select(map.MapEntityToModel);
            return items;
        }

        public IEnumerable<SubCategoryViewModel> GetSubCategoriesByCategoryId(long CategoryId)
        {
            IEnumerable<SubCategoryViewModel> items = unit.subCategoryRepository.GetSubCategoriesByCategoryId(CategoryId).Where(s => s.IsDeleted == false);
            return items;
        }

        public SubCategoryViewModel GetSubCategory(long Id)
        {
            SubCategory cat = unit.subCategoryRepository.GetDataById(Id);
            SubCategoryViewModel catViewModel = map.MapEntityToModel(cat);
            return catViewModel;
        }

        public bool UpdateSubCategory(long Id, SubCategoryViewModel model)
        {
            if (Id == 0)
            {
                SubCategory cat = map.MapModelToEntity(model);
                Utilites.CheckResult = unit.subCategoryRepository.Add(cat);
                unit.Save();
                return Utilites.CheckResult;
            }
            else
            {
                SubCategory cat = unit.subCategoryRepository.GetDataById(Id);
                map.MapModelToEntity(model, cat);
                Utilites.CheckResult = unit.subCategoryRepository.Modify(model.Id, cat);
                unit.Save();
                return Utilites.CheckResult;
            }
        }

        public long GetCount(long categoryId)
        {
            return unit.subCategoryRepository.getCountSubcategoriesbycategoryid(categoryId);
        }
    }
}
