using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaTours.DTOs.Business
{
    public class InternalApplicationTextDTO
    {
        public int InternalApplicationTextID { get; set; }
        public int? LangugeFk { get; set; }
        public string Text { get; set; }
        public string SelectorName { get; set; }
        public bool? SelectorIsTageID { get; set; }



        

    }
}
