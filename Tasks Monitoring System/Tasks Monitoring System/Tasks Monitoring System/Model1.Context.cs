﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TaskMSEntities : DbContext
    {
        public TaskMSEntities()
            : base("name=TaskMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CompanyDIM> CompanyDIMs { get; set; }
        public virtual DbSet<PriorityTypeDIM> PriorityTypeDIMs { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TasksStatusDIM> TasksStatusDIMs { get; set; }
        public virtual DbSet<UserDailsTask> UserDailsTasks { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
