using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhouseSystem.Common.ViewModels
{
   public class ItemViewModel
    {
        public long? Id { get; set; }

        public string BarCode { get; set; }

        public long SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        public long StockId { get; set; }

        public string StockName { get; set; }

        public long? OtherDevicePlaceId { get; set; }

        public string OtherDevicePlaceName { get; set; }

        public long StatusTypeId { get; set; }

        public string StatusTypeName { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
