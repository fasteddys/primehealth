//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CallCenterNotesApp.DLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmailApprovalsGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmailApprovalsGroup()
        {
            this.EmailApprovalsGroup_User = new HashSet<EmailApprovalsGroup_User>();
            this.EmailApprovalsUser_Email = new HashSet<EmailApprovalsUser_Email>();
        }
    
        public int Id { get; set; }
        public string GroupName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmailApprovalsGroup_User> EmailApprovalsGroup_User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmailApprovalsUser_Email> EmailApprovalsUser_Email { get; set; }
    }
}
