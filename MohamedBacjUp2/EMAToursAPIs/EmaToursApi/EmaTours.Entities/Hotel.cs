//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmaTours.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Hotel
    {
        public int HotelID { get; set; }
        public string Name { get; set; }
        public Nullable<int> LanguageFK { get; set; }
        public Nullable<int> OperatingCountryFK { get; set; }
        public Nullable<int> OperatingLocationFK { get; set; }
    }
}
