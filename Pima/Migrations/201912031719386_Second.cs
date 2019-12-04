namespace Pima.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            AddColumn("PROGRAMMER.Songs", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("PROGRAMMER.Songs", "Image");
        }
    }
}
