﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AddAcccuralLeaveTypesApp.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HRMSEntities : DbContext
    {
        public HRMSEntities()
            : base("name=HRMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<LeaveTypeAccuralRule> LeaveTypeAccuralRules { get; set; }
        public virtual DbSet<LeaveTypeRestrictedContractType> LeaveTypeRestrictedContractTypes { get; set; }
        public virtual DbSet<LeaveTypeRestrictedDep> LeaveTypeRestrictedDeps { get; set; }
        public virtual DbSet<LeaveTypeRestrictedEmployee> LeaveTypeRestrictedEmployees { get; set; }
        public virtual DbSet<LeaveTypeRestrictedGender> LeaveTypeRestrictedGenders { get; set; }
        public virtual DbSet<LeaveTypeRestrictedSubDep> LeaveTypeRestrictedSubDeps { get; set; }
        public virtual DbSet<LeaveType> LeaveTypes { get; set; }
        public virtual DbSet<MonthlyEffectiveAccuredLeaveType> MonthlyEffectiveAccuredLeaveTypes { get; set; }
        public virtual DbSet<UserEntitlementChange> UserEntitlementChanges { get; set; }
        public virtual DbSet<UserEntitlement> UserEntitlements { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<ApprovalFlowRequestDetail> ApprovalFlowRequestDetails { get; set; }
        public virtual DbSet<RequestDetail> RequestDetails { get; set; }
        public virtual DbSet<RequestStatu> RequestStatus { get; set; }
        public virtual DbSet<RequestHandler> RequestHandlers { get; set; }
        public virtual DbSet<RequestHoursHandler> RequestHoursHandlers { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}