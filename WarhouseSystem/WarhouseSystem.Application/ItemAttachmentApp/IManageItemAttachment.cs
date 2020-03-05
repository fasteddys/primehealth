using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarhouseSystem.Common.ViewModels;

namespace WarhouseSystem.Application.CategoryApp
{
    public interface IManageItemAttachment
    {
        bool AddItemAttachment(ItemAttachmentModel model);
        IEnumerable<ItemAttachmentModel> GetAllItemAttachment(int ItemId);
    }
}
