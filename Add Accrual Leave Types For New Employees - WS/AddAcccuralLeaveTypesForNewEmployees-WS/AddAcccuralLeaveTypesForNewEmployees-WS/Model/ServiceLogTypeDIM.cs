//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AddAcccuralLeaveTypesForNewEmployees_WS.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ServiceLogTypeDIM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceLogTypeDIM()
        {
            this.ServicesLogs = new HashSet<ServicesLog>();
        }
    
        public int ServiceLogTypeID { get; set; }
        public string ServiceLogTypeName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServicesLog> ServicesLogs { get; set; }
    }
}
