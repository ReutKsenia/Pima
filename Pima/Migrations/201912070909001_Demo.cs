namespace Pima.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Demo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "PROGRAMMER.Demo",
                c => new
                    {
                        DemoId = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        Record = c.Binary(),
                        Date = c.DateTime(nullable: false),
                        UserId_Demo = c.Decimal(precision: 10, scale: 0),
                    })
                .PrimaryKey(t => t.DemoId)
                .ForeignKey("PROGRAMMER.User", t => t.UserId_Demo)
                .Index(t => t.UserId_Demo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("PROGRAMMER.Demo", "UserId_Demo", "PROGRAMMER.User");
            DropIndex("PROGRAMMER.Demo", new[] { "UserId_Demo" });
            DropTable("PROGRAMMER.Demo");
        }
    }
}
