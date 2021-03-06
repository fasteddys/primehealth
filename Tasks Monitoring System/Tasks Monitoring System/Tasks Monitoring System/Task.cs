//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TasksMonitoringSystem
{
    using System;
    using System.Collections.Generic;
    
    public partial class Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Task()
        {
            this.UserDailsTasks = new HashSet<UserDailsTask>();
        }
    
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public Nullable<int> CompanyFK { get; set; }
        public Nullable<int> PriorityFK { get; set; }
        public Nullable<int> AddedByFK { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
    
        public virtual CompanyDIM CompanyDIM { get; set; }
        public virtual PriorityTypeDIM PriorityTypeDIM { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserDailsTask> UserDailsTasks { get; set; }
    }
}
