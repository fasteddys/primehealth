//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.DataBase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TicketingDBEntities : DbContext
    {
        public TicketingDBEntities()
            : base("name=TicketingDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<SubDeparment> SubDeparments { get; set; }
        public virtual DbSet<Ticket_Details> Ticket_Details { get; set; }
        public virtual DbSet<Ticket_Types> Ticket_Types { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
