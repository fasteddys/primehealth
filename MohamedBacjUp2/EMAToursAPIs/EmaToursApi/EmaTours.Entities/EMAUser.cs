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
    
    public partial class EMAUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMAUser()
        {
            this.ClientPickUpRequests = new HashSet<ClientPickUpRequest>();
            this.ClientPickUpRequests1 = new HashSet<ClientPickUpRequest>();
            this.ClientTripRequests = new HashSet<ClientTripRequest>();
            this.ClientTripRequests1 = new HashSet<ClientTripRequest>();
        }
    
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> UserTypeFK { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientPickUpRequest> ClientPickUpRequests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientPickUpRequest> ClientPickUpRequests1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientTripRequest> ClientTripRequests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientTripRequest> ClientTripRequests1 { get; set; }
        public virtual EMAUserTypesDIM EMAUserTypesDIM { get; set; }
    }
}
