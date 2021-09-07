namespace JobWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mod : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Jobs", new[] { "User_Id" });
            DropColumn("dbo.Jobs", "UserId");
            RenameColumn(table: "dbo.Jobs", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Jobs", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Jobs", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Jobs", new[] { "UserId" });
            AlterColumn("dbo.Jobs", "UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Jobs", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Jobs", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobs", "User_Id");
        }
    }
}
