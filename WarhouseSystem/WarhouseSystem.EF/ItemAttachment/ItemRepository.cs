using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;
using WarhouseSystem.DB.Models;
using WarhouseSystem.EF.GenericRepository;

namespace WarhouseSystem.EF
{
   public class ItemAttachmentRepository : GenericRepositry<ItemAttachment>
    {
        public DB.Models.DB _dbContext = null;
        public ItemAttachmentRepository(DB.Models.DB dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ItemAttachment> GetAllItemAttachmentsByItemId(long ItemId)
        {
            List<ItemAttachment> itemAttachment =
                _dbContext.ItemAttachments.Where(x => x.ItemId == ItemId).ToList();
            return itemAttachment;
        }
    }
}
