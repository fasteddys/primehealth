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
    
    public partial class BatchingFilterationDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BatchingFilterationDetail()
        {
            this.BatchingRequests = new HashSet<BatchingRequest>();
        }
    
        public int BatchingFilterationDetailID { get; set; }
        public int RemainingClaimsCount { get; set; }
        public int TotalClaimsCount { get; set; }
        public int FilterationRequestFK { get; set; }
        public int FilterationCategoryFK { get; set; }
        public Nullable<int> BatchingFilterationDetailStatusFK { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsLocked { get; set; }
        public System.DateTime CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
    
        public virtual FilterationRequest FilterationRequest { get; set; }
        public virtual StatusDIM StatusDIM { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BatchingRequest> BatchingRequests { get; set; }
    }
}
