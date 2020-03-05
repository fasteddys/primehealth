namespace WarhouseSystem.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HRMSVs1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "PasswordHash", c => c.String());
            AddColumn("dbo.Employees", "PasswordSalt", c => c.String());
            DropColumn("dbo.Employees", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Password", c => c.String());
            DropColumn("dbo.Employees", "PasswordSalt");
            DropColumn("dbo.Employees", "PasswordHash");
        }
    }
}
