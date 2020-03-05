using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.DB.Models;
using WarhouseSystem.EF.GenericRepository;

namespace WarhouseSystem.EF
{
    public class CompanyRepository : GenericRepositry<Company>
    {
        public DB.Models.DB _dbContext = null;
        public CompanyRepository(DB.Models.DB dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
