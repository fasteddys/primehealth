//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRMS.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Device
    {
        public int DeviceID { get; set; }
        public string DeviceName { get; set; }
        public string IPAddress { get; set; }
        public int DeviceTypeFK { get; set; }
        public string ArName { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> ModificationDate { get; set; }
        public Nullable<System.DateTime> DeletionDate { get; set; }
        public int DeviceOwnerFK { get; set; }
    
        public virtual DeviceOwner DeviceOwner { get; set; }
        public virtual DeviceTypeDIM DeviceTypeDIM { get; set; }
    }
}