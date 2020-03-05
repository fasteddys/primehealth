using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenterNotesApp.ModelView
{
    public class OlineApprovalsDetailsViewModel
    {
        public int RequestDetailsID { get; set; }
        public int RequestID { get; set; }
        public int RequestDetailsTypeID { get; set; }
        public string Notes { get; set; }
        public System.DateTime DetailsDate { get; set; }
        public bool Delivered { get; set; }
        public bool Seen { get; set; }
        public bool IsDeleted { get; set; }
        public int UserID { get; set; }
        public int UserTypeID { get; set; }
        public string DetailTypeName { get; set; }
        public string UserName { get; set; }
        
                    public string RequestDetailsTypeName { get; set; }


    }
}