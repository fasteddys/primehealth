namespace WarhouseSystem.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Attachment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemAttachments", "ItemId", c => c.Int(nullable: false));
            AddColumn("dbo.ItemAttachments", "Item_Id", c => c.Long());
            CreateIndex("dbo.ItemAttachments", "Item_Id");
            AddForeignKey("dbo.ItemAttachments", "Item_Id", "dbo.Items", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemAttachments", "Item_Id", "dbo.Items");
            DropIndex("dbo.ItemAttachments", new[] { "Item_Id" });
            DropColumn("dbo.ItemAttachments", "Item_Id");
            DropColumn("dbo.ItemAttachments", "ItemId");
        }
    }
}
