using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.DB.Models;
using WarhouseSystem.EF.GenericRepository;

namespace WarhouseSystem.EF
{
    public class StatusTypeRepository : GenericRepositry<StatusType>
    {
        public DB.Models.DB _dbContext;
        public StatusTypeRepository(DB.Models.DB dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
