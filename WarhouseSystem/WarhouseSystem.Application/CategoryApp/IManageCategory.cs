using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;

namespace WarhouseSystem.Application.CategoryApp
{
    public interface IManageCategory
    {
        bool AddCategory(CategoryViewModel model);

        bool UpdateCategory(long Id, CategoryViewModel model);

        bool DeleteCategory(long Id);

        CategoryViewModel GetCategory(long Id);

        IEnumerable<CategoryViewModel> GetAllCategorys();
    }
}
