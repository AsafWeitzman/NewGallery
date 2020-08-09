namespace NewGallery.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNametitlepaints : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Paints", "Paintname", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Paints", "Paintname");
        }
    }
}
