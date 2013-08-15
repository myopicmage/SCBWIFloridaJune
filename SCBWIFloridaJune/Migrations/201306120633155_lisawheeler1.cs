namespace SCBWIFloridaJune.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lisawheeler1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LisaWheelers", "Created", c => c.DateTime());
            AlterColumn("dbo.LisaWheelers", "Paid", c => c.DateTime());
            AlterColumn("dbo.LisaWheelers", "Cleared", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LisaWheelers", "Cleared", c => c.DateTime(nullable: false));
            AlterColumn("dbo.LisaWheelers", "Paid", c => c.DateTime(nullable: false));
            AlterColumn("dbo.LisaWheelers", "Created", c => c.DateTime(nullable: false));
        }
    }
}
