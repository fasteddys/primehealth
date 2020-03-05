using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhouseSystem.DB.Models
{
   public class Item : Base
    {
        public long Id { get; set; }

        public string BarCode { get; set; }

        public long SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public long StockId { get; set; }

        public virtual Stock Stock { get; set; }

        public long? OtherDevicePlaceId { get; set; }

        public virtual OtherDevicePlace OtherDevicePlace { get; set; }

        public long StatusTypeId { get; set; }

        public virtual StatusType StatusType { get; set; }
        public virtual List<ItemAttachment> ItemAttachments { get; set; }

    }
}
