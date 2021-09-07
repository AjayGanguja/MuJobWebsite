namespace JobWebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplyForJobs", "Message", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplyForJobs", "Message", c => c.Int(nullable: false));
        }
    }
}
