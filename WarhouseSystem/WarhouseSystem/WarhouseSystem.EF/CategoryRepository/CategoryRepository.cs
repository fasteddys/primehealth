using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.DB.Models;
using WarhouseSystem.EF.GenericRepository;

namespace WarhouseSystem.EF
{
   public class CategoryRepository : GenericRepositry<Category>
    {
        public DB.Models.DB _dbContext = null;
        public CategoryRepository(DB.Models.DB dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public long? CountCategory(long Id)
        {
            return _dbContext.Categories.Where(d => d.IsDeleted == false && d.Id==Id).Select(e => e.CategoryCount).FirstOrDefault();
        }
    
    }
}
