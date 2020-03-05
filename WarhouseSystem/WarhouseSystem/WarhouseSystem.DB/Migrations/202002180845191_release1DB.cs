namespace WarhouseSystem.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class release1DB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        CategoryCount = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        DeletionTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        SubCategoryCount = c.Long(nullable: false),
                        Price = c.Double(nullable: false),
                        CategoryId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        DeletionTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.ProcessTransactions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SubCategoryId = c.Long(nullable: false),
                        AffectNumber = c.Long(nullable: false),
                        EmployeeId = c.Long(nullable: false),
                        TransactionTypeId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        DeletionTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.TransactionTypes", t => t.TransactionTypeId, cascadeDelete: true)
                .Index(t => t.SubCategoryId)
                .Index(t => t.EmployeeId)
                .Index(t => t.TransactionTypeId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Phone = c.String(),
                        JobId = c.Long(nullable: false),
                        EmployeeRoleId = c.Long(nullable: false),
                        HireDate = c.DateTime(nullable: false),
                        LoggedIn = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        DeletionTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmployeeRoles", t => t.EmployeeRoleId, cascadeDelete: true)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId)
                .Index(t => t.EmployeeRoleId);
            
            CreateTable(
                "dbo.EmployeeRoles",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        DeletionTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        DepartmentId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        DeletionTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Job_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Jobs", t => t.Job_Id)
                .Index(t => t.DepartmentId)
                .Index(t => t.Job_Id);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        CompanyId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        DeletionTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        DeletionTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OtherDevicePlaces",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        DepartmentId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        DeletionTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BarCode = c.String(),
                        SubCategoryId = c.Long(nullable: false),
                        StockId = c.Long(nullable: false),
                        OtherDevicePlaceId = c.Long(),
                        StatusTypeId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        DeletionTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OtherDevicePlaces", t => t.OtherDevicePlaceId)
                .ForeignKey("dbo.StatusTypes", t => t.StatusTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryId, cascadeDelete: true)
                .Index(t => t.SubCategoryId)
                .Index(t => t.StockId)
                .Index(t => t.OtherDevicePlaceId)
                .Index(t => t.StatusTypeId);
            
            CreateTable(
                "dbo.StatusTypes",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        DeletionTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        DepartmentId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        DeletionTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.TransactionTypes",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Name = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        DeletionTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProcessTransactions", "TransactionTypeId", "dbo.TransactionTypes");
            DropForeignKey("dbo.ProcessTransactions", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.ProcessTransactions", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.Jobs", "Job_Id", "dbo.Jobs");
            DropForeignKey("dbo.Items", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.Items", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.Stocks", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Items", "StatusTypeId", "dbo.StatusTypes");
            DropForeignKey("dbo.Items", "OtherDevicePlaceId", "dbo.OtherDevicePlaces");
            DropForeignKey("dbo.OtherDevicePlaces", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Jobs", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Departments", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Employees", "EmployeeRoleId", "dbo.EmployeeRoles");
            DropForeignKey("dbo.SubCategories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Stocks", new[] { "DepartmentId" });
            DropIndex("dbo.Items", new[] { "StatusTypeId" });
            DropIndex("dbo.Items", new[] { "OtherDevicePlaceId" });
            DropIndex("dbo.Items", new[] { "StockId" });
            DropIndex("dbo.Items", new[] { "SubCategoryId" });
            DropIndex("dbo.OtherDevicePlaces", new[] { "DepartmentId" });
            DropIndex("dbo.Departments", new[] { "CompanyId" });
            DropIndex("dbo.Jobs", new[] { "Job_Id" });
            DropIndex("dbo.Jobs", new[] { "DepartmentId" });
            DropIndex("dbo.Employees", new[] { "EmployeeRoleId" });
            DropIndex("dbo.Employees", new[] { "JobId" });
            DropIndex("dbo.ProcessTransactions", new[] { "TransactionTypeId" });
            DropIndex("dbo.ProcessTransactions", new[] { "EmployeeId" });
            DropIndex("dbo.ProcessTransactions", new[] { "SubCategoryId" });
            DropIndex("dbo.SubCategories", new[] { "CategoryId" });
            DropTable("dbo.TransactionTypes");
            DropTable("dbo.Stocks");
            DropTable("dbo.StatusTypes");
            DropTable("dbo.Items");
            DropTable("dbo.OtherDevicePlaces");
            DropTable("dbo.Companies");
            DropTable("dbo.Departments");
            DropTable("dbo.Jobs");
            DropTable("dbo.EmployeeRoles");
            DropTable("dbo.Employees");
            DropTable("dbo.ProcessTransactions");
            DropTable("dbo.SubCategories");
            DropTable("dbo.Categories");
        }
    }
}
