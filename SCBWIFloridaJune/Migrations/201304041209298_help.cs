namespace SCBWIFloridaJune.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class help : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Total", c => c.Double(nullable: false));
            AddColumn("dbo.Users", "Account", c => c.String());
            DropColumn("dbo.Users", "AccountName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "AccountName", c => c.String());
            DropColumn("dbo.Users", "Account");
            DropColumn("dbo.Users", "Total");
        }
    }
}
