//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CallCenterSystemReports.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmailApprovalsCategoryDIM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmailApprovalsCategoryDIM()
        {
            this.EmailApprovalRequests = new HashSet<EmailApprovalRequest>();
            this.CallCenterAppUsers = new HashSet<CallCenterAppUser>();
        }
    
        public int ID { get; set; }
        public string CategoryName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmailApprovalRequest> EmailApprovalRequests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CallCenterAppUser> CallCenterAppUsers { get; set; }
    }
}