namespace SCBWIFloridaJune.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hasadigaeebowai : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LisaWheelers", "Critiquer", c => c.String());
            AddColumn("dbo.LisaWheelers", "WaitingList", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LisaWheelers", "WaitingList");
            DropColumn("dbo.LisaWheelers", "Critiquer");
        }
    }
}
