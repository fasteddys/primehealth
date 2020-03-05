using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.DB.Models;
using WarhouseSystem.EF.GenericRepository;
using WarhouseSystem.Common.ViewModels;

namespace WarhouseSystem.EF
{
    public class SubCategoryRepository : GenericRepositry<SubCategory>
    {
        public DB.Models.DB _dbContext = null;
        public SubCategoryRepository(DB.Models.DB dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<SubCategoryViewModel> GetSubCategoriesByCategoryId(long categoryId)
        {
            List<SubCategoryViewModel> subCategories = (from sC in _dbContext.SubCategories
                                                        where sC.CategoryId == categoryId
                                                        select new SubCategoryViewModel
                                                        {
                                                            Id = sC.Id,
                                                            Name = sC.Name,
                                                            CategoryName = sC.Category.Name,
                                                            Count = sC.SubCategoryCount,
                                                            Price = sC.Price,
                                                            CategoryId = sC.CategoryId,
                                                            IsDeleted = sC.IsDeleted
                                                        }).ToList();
            return subCategories;
        }

        public long getCountSubcategoriesbycategoryid(long categoryId)
        {
            long count = _dbContext.SubCategories.Where(r => r.IsDeleted == false && r.CategoryId == categoryId).Count();
            return count;
        }
    
    }
}
