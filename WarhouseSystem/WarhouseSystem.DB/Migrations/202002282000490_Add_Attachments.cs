namespace WarhouseSystem.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Attachments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemAttachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AttachmentName = c.String(),
                        Path = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        DeletionTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ItemAttachments");
        }
    }
}
