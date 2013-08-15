namespace SCBWIFloridaJune.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lisawheeler : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LisaWheelers", "Total", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LisaWheelers", "Total");
        }
    }
}
