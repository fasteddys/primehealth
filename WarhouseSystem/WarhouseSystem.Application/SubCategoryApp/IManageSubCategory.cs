using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;

namespace WarhouseSystem.Application.SubCategoryApp
{
    public interface IManageSubCategory
    {
        bool AddSubCategory(SubCategoryViewModel model);

        bool UpdateSubCategory(long Id, SubCategoryViewModel model);

        bool DeleteSubCategory(long Id);

        SubCategoryViewModel GetSubCategory(long Id);

        IEnumerable<SubCategoryViewModel> GetAllSubCategories();

        IEnumerable<SubCategoryViewModel> GetSubCategoriesByCategoryId(long CategoryId);
    }
}
