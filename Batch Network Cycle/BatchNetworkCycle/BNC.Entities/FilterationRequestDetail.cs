//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BNC.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class FilterationRequestDetail
    {
        public int FilterationRequestDetailID { get; set; }
        public int FilterationRequestFK { get; set; }
        public Nullable<int> FilterationOfficerClaimsCount { get; set; }
        public string FilterationOfficerComment { get; set; }
        public Nullable<System.DateTime> FilterationOfficerAssignDate { get; set; }
        public Nullable<System.DateTime> FilterationRequestClosingDate { get; set; }
        public int FilterationCategoryFK { get; set; }
        public Nullable<int> FilterationOfficerFK { get; set; }
        public int FilterationDetailsStatusFK { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
    }
}
