namespace SCBWIFloridaJune.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class minorchanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Country", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Users", "Phone", c => c.String(nullable: false, maxLength: 16));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Phone", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Users", "Country", c => c.String(nullable: false, maxLength: 6));
        }
    }
}
