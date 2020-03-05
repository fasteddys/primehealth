using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarhouseSystem.Common.ViewModels
{
    public class ItemAttachmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AttachmentName { get; set; }
        public string Path { get; set; }
        public int ItemId { get; set; }
    }
}
