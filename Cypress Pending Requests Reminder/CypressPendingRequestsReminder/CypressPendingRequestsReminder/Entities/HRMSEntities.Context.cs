//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CypressPendingRequestsReminder.Entities
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
    
        public virtual DbSet<ApprovalFlowDetail> ApprovalFlowDetails { get; set; }
        public virtual DbSet<ApprovalFlowRequestDetail> ApprovalFlowRequestDetails { get; set; }
        public virtual DbSet<ApprovalFlow> ApprovalFlows { get; set; }
        public virtual DbSet<ApprovalFlowUserDetail> ApprovalFlowUserDetails { get; set; }
        public virtual DbSet<LeaveType> LeaveTypes { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<ServiceLogTypeDIM> ServiceLogTypeDIMs { get; set; }
        public virtual DbSet<ServicesLog> ServicesLogs { get; set; }
        public virtual DbSet<SubDepartment> SubDepartments { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ServicesDIM> ServicesDIMs { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; }
    }
}
