//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Stock_System2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Category
    {
        public Category()
        {
            this.Item = new HashSet<Item>();
        }
    
        public int Category_id { get; set; }
        public string Category_name { get; set; }
        public string Department_name { get; set; }
        public string Department_email { get; set; }
    
        public virtual ICollection<Item> Item { get; set; }
    }
}
