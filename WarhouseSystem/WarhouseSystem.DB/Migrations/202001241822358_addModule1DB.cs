namespace WarhouseSystem.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addModule1DB : DbMigration
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
            
            AddColumn("dbo.Companies", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Companies", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.Companies", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Companies", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Departments", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Departments", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.Departments", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Departments", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Jobs", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Jobs", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.Jobs", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Jobs", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.EmployeeRoles", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.EmployeeRoles", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.EmployeeRoles", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.EmployeeRoles", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Employees", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Employees", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.Employees", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.StatusTypes", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.StatusTypes", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.StatusTypes", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.StatusTypes", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.TransactionTypes", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.TransactionTypes", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.TransactionTypes", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.TransactionTypes", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProcessTransactions", "TransactionTypeId", "dbo.TransactionTypes");
            DropForeignKey("dbo.ProcessTransactions", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.ProcessTransactions", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Items", "SubCategoryId", "dbo.SubCategories");
            DropForeignKey("dbo.Items", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.Stocks", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Items", "StatusTypeId", "dbo.StatusTypes");
            DropForeignKey("dbo.Items", "OtherDevicePlaceId", "dbo.OtherDevicePlaces");
            DropForeignKey("dbo.OtherDevicePlaces", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.SubCategories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Stocks", new[] { "DepartmentId" });
            DropIndex("dbo.Items", new[] { "StatusTypeId" });
            DropIndex("dbo.Items", new[] { "OtherDevicePlaceId" });
            DropIndex("dbo.Items", new[] { "StockId" });
            DropIndex("dbo.Items", new[] { "SubCategoryId" });
            DropIndex("dbo.OtherDevicePlaces", new[] { "DepartmentId" });
            DropIndex("dbo.ProcessTransactions", new[] { "TransactionTypeId" });
            DropIndex("dbo.ProcessTransactions", new[] { "EmployeeId" });
            DropIndex("dbo.ProcessTransactions", new[] { "SubCategoryId" });
            DropIndex("dbo.SubCategories", new[] { "CategoryId" });
            DropColumn("dbo.TransactionTypes", "IsDeleted");
            DropColumn("dbo.TransactionTypes", "IsActive");
            DropColumn("dbo.TransactionTypes", "DeletionTime");
            DropColumn("dbo.TransactionTypes", "CreationTime");
            DropColumn("dbo.StatusTypes", "IsDeleted");
            DropColumn("dbo.StatusTypes", "IsActive");
            DropColumn("dbo.StatusTypes", "DeletionTime");
            DropColumn("dbo.StatusTypes", "CreationTime");
            DropColumn("dbo.Employees", "IsActive");
            DropColumn("dbo.Employees", "DeletionTime");
            DropColumn("dbo.Employees", "CreationTime");
            DropColumn("dbo.EmployeeRoles", "IsDeleted");
            DropColumn("dbo.EmployeeRoles", "IsActive");
            DropColumn("dbo.EmployeeRoles", "DeletionTime");
            DropColumn("dbo.EmployeeRoles", "CreationTime");
            DropColumn("dbo.Jobs", "IsDeleted");
            DropColumn("dbo.Jobs", "IsActive");
            DropColumn("dbo.Jobs", "DeletionTime");
            DropColumn("dbo.Jobs", "CreationTime");
            DropColumn("dbo.Departments", "IsDeleted");
            DropColumn("dbo.Departments", "IsActive");
            DropColumn("dbo.Departments", "DeletionTime");
            DropColumn("dbo.Departments", "CreationTime");
            DropColumn("dbo.Companies", "IsDeleted");
            DropColumn("dbo.Companies", "IsActive");
            DropColumn("dbo.Companies", "DeletionTime");
            DropColumn("dbo.Companies", "CreationTime");
            DropTable("dbo.Stocks");
            DropTable("dbo.Items");
            DropTable("dbo.OtherDevicePlaces");
            DropTable("dbo.ProcessTransactions");
            DropTable("dbo.SubCategories");
            DropTable("dbo.Categories");
        }
    }
}
