namespace WarhouseSystem.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "PasswordHash", c => c.Binary());
            AddColumn("dbo.Employees", "PasswordSalt", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "PasswordSalt");
            DropColumn("dbo.Employees", "PasswordHash");
        }
    }
}
