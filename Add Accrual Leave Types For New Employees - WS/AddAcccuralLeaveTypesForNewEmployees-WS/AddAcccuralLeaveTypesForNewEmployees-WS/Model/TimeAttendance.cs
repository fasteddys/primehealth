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
    
    public partial class TimeAttendance
    {
        public int TimeAttendanceID { get; set; }
        public int UserAccessControlID { get; set; }
        public string PersonName { get; set; }
        public string DeviceName { get; set; }
        public string ActionTypeName { get; set; }
        public System.DateTime ActionDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
        public Nullable<int> DeviceFK { get; set; }
        public int TimeAttendanceSerial { get; set; }
    }
}
