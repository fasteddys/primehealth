using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Runtime.Remoting.Contexts;

namespace WarhouseSystem.DB.Models
{
    
    public class DB : DbContext
    {
  

        public DB() : base("name=WarhouseSystemDB")
        {
        }

        public virtual DbSet<EmployeeRole> EmployeeRoles { get; set; }

        public virtual DbSet<StatusType> StatusTypes { get; set; }

        public virtual DbSet<TransactionType> TransactionTypes { get; set; }

        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<Job> Jobs { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<SubCategory> SubCategories { get; set; }

        public virtual DbSet<Item> Items { get; set; }

        public virtual DbSet<Stock> Stocks { get; set; }

        public virtual DbSet<ProcessTransaction> ProcessTransactions { get; set; }

        public virtual DbSet<OtherDevicePlace> OtherDevicePlaces { get; set; }
        public virtual DbSet<ItemAttachment> ItemAttachments { get; set; }



    }
}
