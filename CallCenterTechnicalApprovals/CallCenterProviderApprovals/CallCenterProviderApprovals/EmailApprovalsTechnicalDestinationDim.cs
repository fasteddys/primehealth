//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CallCenterProviderApprovals
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmailApprovalsTechnicalDestinationDim
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmailApprovalsTechnicalDestinationDim()
        {
            this.EmailRequestRequest_TechnicalDestination = new HashSet<EmailRequestRequest_TechnicalDestination>();
        }
    
        public int ID { get; set; }
        public string DepartmentName { get; set; }
        public Nullable<bool> Active { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmailRequestRequest_TechnicalDestination> EmailRequestRequest_TechnicalDestination { get; set; }
    }
}
