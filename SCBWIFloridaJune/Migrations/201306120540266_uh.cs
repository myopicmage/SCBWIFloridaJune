namespace SCBWIFloridaJune.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LisaWheelers",
                c => new
                    {
                        LisaWheelerID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Address1 = c.String(nullable: false),
                        Address2 = c.String(),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false, maxLength: 2),
                        Zip = c.String(nullable: false, maxLength: 5),
                        Phone = c.String(nullable: false, maxLength: 15),
                        Email = c.String(nullable: false),
                        Member = c.String(),
                        Critique = c.String(),
                        PayPalID = c.String(),
                        Account = c.String(),
                        Created = c.DateTime(nullable: false),
                        Paid = c.DateTime(nullable: false),
                        Cleared = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LisaWheelerID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LisaWheelers");
        }
    }
}
